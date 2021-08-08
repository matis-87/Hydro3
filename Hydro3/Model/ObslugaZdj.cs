using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hydro3.Model
{
    public enum nrStref : byte
    {
        strefa1 = 1,
        strefa2 = 2,
        strefa3 = 3,
        strefa4 = 4,
        strefa5 = 5


    }

    public static class PlikCfg
    {
        const string fileres = "hydrocfg.xml";
        const string file = "cfg3.xml";
        static readonly string pfile;
        public static XDocument cfg;
        static PlikCfg()
        {
         pfile = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, file);

        }

        public static void LadujCfg()
        {
            if (File.Exists(pfile))
            {
                cfg = XDocument.Load(pfile);
            }
            else
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ObslugaZdj)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("Hydro3.Resources.hydrocfg.xml");


                using (var reader = new System.IO.StreamReader(stream))
                {

                    // = reader.ReadToEnd();



                    string Test = reader.ReadToEnd();
                    cfg = XDocument.Load(new StringReader(Test), LoadOptions.PreserveWhitespace);
                }
            }
        }

        public static string PobierzNazwe(nrStref strefa)
        {

            return cfg.Root.Element(strefa.ToString()).Attribute("name").Value;
        }

        public static void ZmienNazwe(nrStref strefa, string nazwa)
        {
            cfg.Root.Element(strefa.ToString()).Attribute("name").Value = nazwa;
        }

        public static void ZapiszCfg()
        {
            cfg.Save(pfile);
        }
    }
    public static  class ObslugaZdj
    {
        const string file1 = "st1.jpg";
        const string file2 = "st2.jpg";
        const string file3 = "st3.jpg";
        static readonly string s1file;
        static readonly string s2file;
        static readonly string s3file;


        static ObslugaZdj()
        {
          s1file = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, file1);
          s2file = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, file2);
          s3file = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, file3);
        }

        public static ImageSource LadujCfg()
        {
            ImageSource Zdj;
            string tempFile = string.Empty;

            
            if (File.Exists(tempFile))
            {
                Zdj = ImageSource.FromFile(tempFile);
            }
            else
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ObslugaZdj)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("Hydro3.Resources.Strefa.jpg");


                Zdj = ImageSource.FromStream(() => stream);
            }

            return Zdj;

        }

        public static ImageSource LadujZdj(nrStref strefa)
        {
            ImageSource Zdj;
            string tempFile = string.Empty;

            switch(strefa)
            {
                case nrStref.strefa1 :
                    tempFile = s1file;
                    break;
                case nrStref.strefa2 :
                    tempFile = s2file;
                    break;
                case nrStref.strefa3 :
                    tempFile = s3file;
                    break;
            }
            if (File.Exists(tempFile))
            {
                Zdj = ImageSource.FromFile(tempFile);
            }
            else
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ObslugaZdj)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("Hydro3.Resources.Strefa.jpg");


                Zdj = ImageSource.FromStream(() => stream);
            }

            return Zdj;

        }

        public async static Task<Stream> WybierzZdj()
        {
            ImageSource Zdj;
            bool wybrano = false;
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                MaxWidthHeight = Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height),
                PhotoSize = PhotoSize.MaxWidthHeight


            });
            Stream st;
            st = file.GetStream();
            return st;
        }

        public static void ZapiszZdj(nrStref strefa, Stream st)
        {
            string tempFile = string.Empty;

            switch (strefa)
            {
                case nrStref.strefa1:
                    tempFile = s1file;
                    break;
                case nrStref.strefa2:
                    tempFile = s2file;
                    break;
                case nrStref.strefa3:
                    tempFile = s3file;
                    break;
            }
            using (var fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
            {
               
                st.CopyTo(fileStream);
            }

        }
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using Hydro3.Model;
using System.Threading.Tasks;
using Hydro3.Services;
using Prism.AppModel;
using System.IO;
using Xamarin.Essentials;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;

namespace Hydro3.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        bool Pobieraj;


        public MainViewHelper Helper { get; set; }



        private ImageSource _zdj;


        public ImageSource Zdj
        {
            get { return _zdj; }
            set
            {
                SetProperty(ref _zdj, value);

                /*    if ((value == true) && (!polaczony_trig))
                        WznowPobieranie();
                    polaczony_trig = value;*/
            }
        }


        private string _test;


        public string Test
        {
            get { return _test; }
            set
            {
                SetProperty(ref _test, value);

                /*    if ((value == true) && (!polaczony_trig))
                        WznowPobieranie();
                    polaczony_trig = value;*/
            }
        }







        public DelegateCommand NiePodlewajCommand { get; set; }
        public DelegateCommand PolaczCommand { get; set; }
        public DelegateCommand ManualCommand { get; set; }
        public DelegateCommand DalejCommand { get; set; }
        public DelegateCommand ConnectionViewCommand { get; set; }

        public  MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            PolaczCommand = new DelegateCommand(Polacz); // () => Helper.Polaczony).ObservesProperty(() => Helper.Polaczony);
            ManualCommand = new DelegateCommand(Manual);
            DalejCommand = new DelegateCommand(Dalej);
            NiePodlewajCommand = new DelegateCommand(fNiePodlewaj);
            ConnectionViewCommand = new DelegateCommand(ToConnectionView);
            _navigationService = navigationService;
            //   PLC = new PLCConn();
            Helper = new MainViewHelper();
            Pobieraj = true;
            Test = "tutaj";
            
        }
        //   _navigationService.NavigateAsync("PageB");

            void ToConnectionView()
        {
            var navigationParams = new NavigationParameters();
            Object client = Helper.Connection.GetClient() as Object;
            navigationParams.Add("conn", null);
            _navigationService.NavigateAsync("Connection", navigationParams, useModalNavigation: false);
        }
            private async  void Manual()
        {
               var navigationParams = new NavigationParameters();
                Object client = Helper.Connection.GetClient();
                navigationParams.Add("conn", client);
               _navigationService.NavigateAsync("ManualView", navigationParams, useModalNavigation: false);
                /*

            //            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            //          Zdj =  ImageSource.FromStream(() => stream);
            //        Image Zd = new Image();
            // Get Metrics
            //*****Pobieranie zdjec*******
              /* var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                       MaxWidthHeight =Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height),
                      PhotoSize = PhotoSize.MaxWidthHeight


                  });

                //  Zdj = ImageSource.FromStream(()=>file.GetStream());
                  var mainDisplayInfo = DeviceDisplay.MainDisplayInfo.Height;
                  MediaFile dd;


            */
            

                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(HarmonogramViewModel)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("Hydro3.Resources.pic3.png");

                Zdj = ImageSource.FromStream(() => stream);  

        /*    var backingFile = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "st2.png");

            using (var fileStream = new FileStream(backingFile, FileMode.Create, FileAccess.Write))
            {
                Stream st = file.GetStream();

                st.CopyTo(fileStream);
                 }
            

         /*   using (var writer = new System.IO.StreamWriter(backingFile))
            {

                writer.Write(file.GetStream());

            }
            */



        //    Zdj = ImageSource.FromFile(backingFile);

           
            
            /*
            

            var backingFile2 = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "cfg2.xml");

                using (var reader = new System.IO.StreamReader(backingFile2))
                {

                    // = reader.ReadToEnd();



                    Test= reader.ReadToEnd();
           XDocument     dok = XDocument.Parse(Test, LoadOptions.PreserveWhitespace);

                int j = 0;
            }*/

                
         /*        using (var fileStream = new FileStream(backingFile, FileMode.Create, FileAccess.Write))
                 {

                     fileStream.Write(file,0,file.)
                 }*/
            //   dok.Save("hydro.xml");
        }
        private void Dalej()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("conn", Helper.Connection.GetClient());
            _navigationService.NavigateAsync("Harmonogram", navigationParams, useModalNavigation: false);
          //  Helper.AktywneZadanie1.Remaining--;
        }

        private void Connecting()
        {



            try
            {

                if (!Helper.Polaczony)
                {
                   
                        Helper.Polacz();
                  


                }
            
            }
            catch (Exception e)
            {

            }
            // return Task.CompletedTask;
        }


        void Polacz()
        {
            Task t;
            Connecting();
            int g = 0;
        }

        public async void fNiePodlewaj()
        {
            byte[] buffer = new byte[4];

            buffer[0] = 1;
           
          //  await Helper.Connection.SendData(buffer);
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
           
            int h = 0;
            Helper.WznowPobieranie();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Helper.WstrzymajPobieranie();
        }



    }
}

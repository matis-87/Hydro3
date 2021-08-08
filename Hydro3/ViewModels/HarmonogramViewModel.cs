using Hydro3.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Hydro3.Model;
using System.Threading.Tasks;
using Prism.Services;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Hydro3.ViewModels
{
    public class HarmonogramViewModel : ViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private HarmoHelper _helper;
        public HarmoHelper Helper { get; set; }
        /*{
            get { return _helper; }
            set { SetProperty(ref _helper, value); }
        }*/


        private bool _ladowanie;
        public bool Ladowanie
        {
            get { return _ladowanie; }
            set
            {
                SetProperty(ref _ladowanie, value);

                /*    if ((value == true) && (!polaczony_trig))
                        WznowPobieranie();
                    polaczony_trig = value;*/
            }
        }

        public DelegateCommand<object> DniS1Command { get; set; }
        public DelegateCommand<object> DniS2Command { get; set; }
        public DelegateCommand<object> DniS3Command { get; set; }
        public DelegateCommand<object> DniS4Command { get; set; }
        public DelegateCommand<object> DniS5Command { get; set; }
        public DelegateCommand ZapiszS1Command { get; set; }
        public DelegateCommand ZapiszS2Command { get; set; }
        public DelegateCommand ZapiszS3Command { get; set; }
        public DelegateCommand ZapiszS4Command { get; set; }
        public DelegateCommand ZapiszS5Command { get; set; }
        public DelegateCommand <object>WybierzZdjCommand { get; set; }

        IPageDialogService _dialogService;

        private ImageSource _zdjS1;
        public ImageSource ZdjS1
        {
            get { return _zdjS1; }
            set
            {
                SetProperty(ref _zdjS1, value);
            }
        }

        private ImageSource _zdjS2;
        public ImageSource ZdjS2
        {
            get { return _zdjS2; }
            set
            {
                SetProperty(ref _zdjS2, value);
            }
        }

        private ImageSource _zdjS3;
        public ImageSource ZdjS3
        {
            get { return _zdjS3; }
            set
            {
                SetProperty(ref _zdjS3, value);
            }
        }

        HarmonogramViewModel(IPageDialogService dialogService, INavigationService navigationService) : base(navigationService)
        {
            _dialogService = dialogService;
            Helper = new HarmoHelper(dialogService);
            DniS1Command = new DelegateCommand<object>(WyborDniS1);
            DniS2Command = new DelegateCommand<object>(WyborDniS2);
            DniS3Command = new DelegateCommand<object>(WyborDniS3);
            DniS4Command = new DelegateCommand<object>(WyborDniS4);
            DniS5Command = new DelegateCommand<object>(WyborDniS5);
            ZapiszS1Command = new DelegateCommand(ZapisS1,() => Helper.Polaczony).ObservesProperty(() => Helper.Polaczony);
            ZapiszS2Command = new DelegateCommand(ZapisS2, () => Helper.Polaczony).ObservesProperty(() => Helper.Polaczony);
            ZapiszS3Command = new DelegateCommand(ZapisS3, () => Helper.Polaczony).ObservesProperty(() => Helper.Polaczony);
            ZapiszS4Command = new DelegateCommand(ZapisS4, () => Helper.Polaczony).ObservesProperty(() => Helper.Polaczony);
            ZapiszS5Command = new DelegateCommand(ZapisS5, () => Helper.Polaczony).ObservesProperty(() => Helper.Polaczony);
            WybierzZdjCommand = new DelegateCommand<object>(WyborZdj);
            _navigationService = navigationService;

        }

        private void SprawdzenieZdjec()
        {
          ZdjS1 = ObslugaZdj.LadujZdj(nrStref.strefa1);
          ZdjS2 = ObslugaZdj.LadujZdj(nrStref.strefa2);
          ZdjS3 = ObslugaZdj.LadujZdj(nrStref.strefa3);






        }

        private void OdczytUstawien()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(HarmonogramViewModel)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Hydro3.Resources.hydrocfg.xml");
            XDocument dok = new XDocument();

            using (var reader = new System.IO.StreamReader(stream))
            {

                dok = XDocument.Load(stream, LoadOptions.PreserveWhitespace);

            }

            var backingFile = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "cfg2.xml");

            if (!File.Exists(backingFile))
                dok.Save(backingFile);
            /*   using (var writer = new System.IO.StreamWriter(backingFile))
               {

                       writer.Write(stream);

               }*/
            else
                using (var reader = new System.IO.StreamReader(backingFile))
                {

                    // = reader.ReadToEnd();



                    dok = XDocument.Parse(reader.ReadToEnd(), LoadOptions.PreserveWhitespace);

                }

        }
        async void  ZapisS1()
        {

            byte[] dane = new byte[10];
            dane[0] = 11;
            byte dni = Helper.DniStrefyNaByte(Helper.Strefa1);



            dane[1] = Convert.ToByte(dni);
            dane[2] = Convert.ToByte(Helper.Strefa1.Czas.Hours);
            dane[3] = Convert.ToByte(Helper.Strefa1.Czas.Minutes);
            dane[4] = Convert.ToByte(Helper.Strefa1.Dlugosc);
            string res = Helper.SprawdzKolizje(Helper.Strefa1, Helper.Strefa2, Helper.Strefa3);
            if (res != null)
            {
                await _dialogService.DisplayAlertAsync("Uwaga!", $"Ryzyko pracy równoczesnej stref: {Helper.Strefa1.Nazwa} i {res}. \nPopraw dane i zapisz ponownie.", "OK");
            }
            else
            {
                Helper.Zapisz(dane);
            }
            
        }

      async  void ZapisS2()
        {

            byte[] dane = new byte[10];
            dane[0] = 12;
            byte dni = Helper.DniStrefyNaByte(Helper.Strefa2);



            dane[1] = Convert.ToByte(dni);
            dane[2] = Convert.ToByte(Helper.Strefa2.Czas.Hours);
            dane[3] = Convert.ToByte(Helper.Strefa2.Czas.Minutes);
            dane[4] = Convert.ToByte(Helper.Strefa2.Dlugosc);

            string res = Helper.SprawdzKolizje(Helper.Strefa2, Helper.Strefa1, Helper.Strefa3);
            if (res != null)
            {
                await _dialogService.DisplayAlertAsync("Uwaga!", $"Ryzyko pracy równoczesnej stref: {Helper.Strefa2.Nazwa} i {res}. \nPopraw dane i zapisz ponownie.", "OK");
            }
            else
            {
                Helper.Zapisz(dane);
            }
        }

       async  void ZapisS3()
        {

            byte[] dane = new byte[10];
            dane[0] = 13;
            byte dni = Helper.DniStrefyNaByte(Helper.Strefa3);



            dane[1] = Convert.ToByte(dni);
            dane[2] = Convert.ToByte(Helper.Strefa3.Czas.Hours);
            dane[3] = Convert.ToByte(Helper.Strefa3.Czas.Minutes);
            dane[4] = Convert.ToByte(Helper.Strefa3.Dlugosc);

            string res = Helper.SprawdzKolizje(Helper.Strefa3, Helper.Strefa1, Helper.Strefa2);
            if (res != null)
            {
                await _dialogService.DisplayAlertAsync("Uwaga!", $"Ryzyko pracy równoczesnej stref: {Helper.Strefa3.Nazwa} i {res}. \nPopraw dane i zapisz ponownie.", "OK");
            }
            else
            {
                Helper.Zapisz(dane);
            }
        }

        async void ZapisS4()
        {

            byte[] dane = new byte[10];
            dane[0] = 14;
            byte dni = Helper.DniStrefyNaByte(Helper.Strefa4);



            dane[1] = Convert.ToByte(dni);
            dane[2] = Convert.ToByte(Helper.Strefa4.Czas.Hours);
            dane[3] = Convert.ToByte(Helper.Strefa4.Czas.Minutes);
            dane[4] = Convert.ToByte(Helper.Strefa4.Dlugosc);

            await Helper.Connection.SendData(dane);
        }

        async void ZapisS5()
        {

            byte[] dane = new byte[10];
            dane[0] = 15;
            byte dni = Helper.DniStrefyNaByte(Helper.Strefa5);



            dane[1] = Convert.ToByte(dni);
            dane[2] = Convert.ToByte(Helper.Strefa5.Czas.Hours);
            dane[3] = Convert.ToByte(Helper.Strefa5.Czas.Minutes);
            dane[4] = Convert.ToByte(Helper.Strefa5.Dlugosc);

            await Helper.Connection.SendData(dane);
        }
        void  WyborDniS1(object par)
        {
            int dzien = Convert.ToInt32(par);
            Helper.Strefa1.WyborDni(dzien);
           
           
        }
        void WyborDniS2(object par)
        {
            int dzien = Convert.ToInt32(par);
            Helper.Strefa2.WyborDni(dzien);


        }
        void WyborDniS3(object par)
        {
            int dzien = Convert.ToInt32(par);
            Helper.Strefa3.WyborDni(dzien);


        }
        void WyborDniS4(object par)
        {
            int dzien = Convert.ToInt32(par);
            Helper.Strefa4.WyborDni(dzien);


        }
        void WyborDniS5(object par)
        {
            int dzien = Convert.ToInt32(par);
            Helper.Strefa5.WyborDni(dzien);


        }


        private void WyborZdj(object par)
        {
            string nr = par.ToString();
            int val = Convert.ToInt32(nr);

            nrStref strefa = new nrStref();
            switch(val)
            {
                case 1:
                    strefa = nrStref.strefa1;
                    break;
                case 2:
                    strefa = nrStref.strefa2;
                    break;
                case 3:
                    strefa = nrStref.strefa3;
                    break;
            }
            var navigationParams = new NavigationParameters();
            navigationParams.Add("strefa", strefa);
            _navigationService.NavigateAsync("WyborZdj",navigationParams, useModalNavigation: false);
            //  Helper.AktywneZadanie1.Remaining--;
        }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Helper.WstrzymajPobieranie();
            Ladowanie = false;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            SprawdzenieZdjec();
            OdczytUstawien();
            PlikCfg.LadujCfg();
            Helper.Strefa1.Nazwa = PlikCfg.PobierzNazwe(nrStref.strefa1);
            Helper.Strefa2.Nazwa = PlikCfg.PobierzNazwe(nrStref.strefa2);
            Helper.Strefa3.Nazwa = PlikCfg.PobierzNazwe(nrStref.strefa3);
            Object client;
            client = parameters["conn"];
            if (client != null)
            {
                Helper.Connection.SetClient(client);
                Helper.Polaczony = Helper.Connection.SprawdzPolaczenie();
            }
            //   Helper.OdczytujDane();
            if(!Helper.Polaczony)
             await   Helper.Polacz();
           
            if (Helper.Polaczony)
            {
                object ob = new object();
                do
                {
                    ob = await Helper.Connection.PobierzDane(30);
                    Helper.Polaczony = Helper.Connection.SprawdzPolaczenie();
                } 
                while ((!Helper.Polaczony)||(ob==null));

                byte[] dane = ob as byte[];
                Helper.Strefa1.NdChcecked = BitManipulation.GetBitAt(dane[0], 0);
                Helper.Strefa1.PnChcecked = BitManipulation.GetBitAt(dane[0], 1);
                Helper.Strefa1.WtChcecked = BitManipulation.GetBitAt(dane[0], 2);
                Helper.Strefa1.SrChcecked = BitManipulation.GetBitAt(dane[0], 3);
                Helper.Strefa1.CzwChcecked = BitManipulation.GetBitAt(dane[0], 4);
                Helper.Strefa1.PtChcecked = BitManipulation.GetBitAt(dane[0], 5);
                Helper.Strefa1.SbChcecked = BitManipulation.GetBitAt(dane[0], 6);
                TimeSpan czas = new TimeSpan(dane[1], dane[2], 0);
                Helper.Strefa1.Czas = czas;
                Helper.Strefa1.Dlugosc = dane[3];

                Helper.Strefa2.NdChcecked = BitManipulation.GetBitAt(dane[4], 0);
                Helper.Strefa2.PnChcecked = BitManipulation.GetBitAt(dane[4], 1);
                Helper.Strefa2.WtChcecked = BitManipulation.GetBitAt(dane[4], 2);
                Helper.Strefa2.SrChcecked = BitManipulation.GetBitAt(dane[4], 3);
                Helper.Strefa2.CzwChcecked = BitManipulation.GetBitAt(dane[4], 4);
                Helper.Strefa2.PtChcecked = BitManipulation.GetBitAt(dane[4], 5);
                Helper.Strefa2.SbChcecked = BitManipulation.GetBitAt(dane[4], 6);
                czas = new TimeSpan(dane[5], dane[6], 0);
                Helper.Strefa2.Czas = czas;
                Helper.Strefa2.Dlugosc = dane[7];

                Helper.Strefa3.NdChcecked = BitManipulation.GetBitAt(dane[8], 0);
                Helper.Strefa3.PnChcecked = BitManipulation.GetBitAt(dane[8], 1);
                Helper.Strefa3.WtChcecked = BitManipulation.GetBitAt(dane[8], 2);
                Helper.Strefa3.SrChcecked = BitManipulation.GetBitAt(dane[8], 3);
                Helper.Strefa3.CzwChcecked = BitManipulation.GetBitAt(dane[8], 4);
                Helper.Strefa3.PtChcecked = BitManipulation.GetBitAt(dane[8], 5);
                Helper.Strefa3.SbChcecked = BitManipulation.GetBitAt(dane[8], 6);
                czas = new TimeSpan(dane[9], dane[10], 0);
                Helper.Strefa3.Czas = czas;
                Helper.Strefa3.Dlugosc = dane[11];

                Helper.Strefa4.NdChcecked = BitManipulation.GetBitAt(dane[12], 0);
                Helper.Strefa4.PnChcecked = BitManipulation.GetBitAt(dane[12], 1);
                Helper.Strefa4.WtChcecked = BitManipulation.GetBitAt(dane[12], 2);
                Helper.Strefa4.SrChcecked = BitManipulation.GetBitAt(dane[12], 3);
                Helper.Strefa4.CzwChcecked = BitManipulation.GetBitAt(dane[12], 4);
                Helper.Strefa4.PtChcecked = BitManipulation.GetBitAt(dane[12], 5);
                Helper.Strefa4.SbChcecked = BitManipulation.GetBitAt(dane[12], 6);
                czas = new TimeSpan(dane[13], dane[14], 0);
                Helper.Strefa4.Czas = czas;
                Helper.Strefa4.Dlugosc = dane[15];

                Helper.Strefa5.NdChcecked = BitManipulation.GetBitAt(dane[16], 0);
                Helper.Strefa5.PnChcecked = BitManipulation.GetBitAt(dane[16], 1);
                Helper.Strefa5.WtChcecked = BitManipulation.GetBitAt(dane[16], 2);
                Helper.Strefa5.SrChcecked = BitManipulation.GetBitAt(dane[16], 3);
                Helper.Strefa5.CzwChcecked = BitManipulation.GetBitAt(dane[16], 4);
                Helper.Strefa5.PtChcecked = BitManipulation.GetBitAt(dane[16], 5);
                Helper.Strefa5.SbChcecked = BitManipulation.GetBitAt(dane[16], 6);
                czas = new TimeSpan(dane[17], dane[18], 0);
                Helper.Strefa5.Czas = czas;
                Helper.Strefa5.Dlugosc = dane[19];
                Ladowanie = true;
                Helper.WznowPobieranie();
            }
      

        }
    }
}

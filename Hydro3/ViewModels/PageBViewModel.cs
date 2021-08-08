using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Hydro3.Kontrolki;
using Hydro3.Model;
using Hydro3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Hydro3.ViewModels
{

    public class PageBViewModel : ViewModelBase, INavigationAware, IPageLifecycleAware
    {

        private PageBViewHelper _helper;

        public PageBViewHelper Helper
        {
            get
            {
                return _helper;
            }
            set
            {

                SetProperty(ref _helper, value);

            }
        }


        private readonly INavigationService _navigationService;

        public DelegateCommand<string> LocalButton { get; private set; }
        public DelegateCommand<string> RunFwrButton { get; private set; }
        public DelegateCommand<string> RunBwrButton { get; private set; }
        public DelegateCommand<string> StopButton { get; private set; }
        public DelegateCommand<string> ResetButton { get; private set; }
        public DelegateCommand<string> InvResetButton { get; private set; }
        public PageBViewModel(INavigationService navigationService) : base(navigationService)
        {


            //  Tytul = "PWE-1.40 100U1";
            LocalButton = new DelegateCommand<string>(ExecuteLocal);
            RunFwrButton = new DelegateCommand<string>(ExecuteRunFwr);
            RunBwrButton = new DelegateCommand<string>(ExecuteStartRun);
            StopButton = new DelegateCommand<string>(ExecuteStopRun);
            ResetButton = new DelegateCommand<string>(ExecuteFault);
            InvResetButton = new DelegateCommand<string>(ExecuteFault);
            //    PolaczCommand = new DelegateCommand<string>(Polacz);

            Helper = new PageBViewHelper(navigationService);
            _navigationService = navigationService;

        }




        private string _tytul;
        public string Tytul
        {
            get
            {

                return _tytul;
            }
            set { SetProperty(ref _tytul, value); }
        }

        #region Komendy po przycisku

        private void ExecuteLocal(string parameter)
        {

        }

        private void ExecuteStartRun(string parameter)
        {

        }

        private void ExecuteRunFwr(string parameter)
        {
            AdresS7 adres = new AdresS7();
            adres.DB = Helper.urzadzenie.DBKomand;
            adres.Start = Helper.urzadzenie.OffsetKomand;
            adres.Pos = 0;
            Helper.ClickButton(adres);
        }


        private void ExecuteStopRun(string parameter)
        {

        }

        private void ExecuteFault(string parameter)
        {

        }



        #endregion


        public override void OnNavigatedTo(INavigationParameters parameters)
        {


            Object client;
            client = parameters["conn"];
            DaneUrzadzenia tempDane = parameters["dev"] as DaneUrzadzenia;
            Helper.PobierzUrzadzenie(tempDane);
            Helper.KonfigurujCzujniki();
            if (client != null)
                Helper.Connection.SetClient(client);
            Helper.Polaczony = Helper.Connection.SprawdzPolaczenie();
            if (Helper.Polaczony)
                Helper.WznowPobieranie();





        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Helper.WstrzymajPobieranie();

        }

        public void OnAppearing()
        {



        }

        public void OnDisappearing()
        {


        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;
using Hydro3.Kontrolki;
using Hydro3.Model;

namespace Hydro3.Services
{
    public class PageBViewHelper : s7ConnectionHelper
    {


        public readonly INavigationService _navigationService;
        public DaneUrzadzenia urzadzenie;
        public PageBViewHelper(INavigationService navi)
        {
            Connection = new ArduGlowneDane();
            _navigationService = navi;
            urzadzenie = new DaneUrzadzenia();

            Cz1wl = false;
            Cz2wl = false;
        }


        private bool _diodaAuto;
        public bool DiodaAuto
        {
            get { return _diodaAuto; }
            set { SetProperty(ref _diodaAuto, value); }
        }

        private bool _diodaBlad;
        public bool DiodaBlad
        {
            get { return _diodaBlad; }
            set { SetProperty(ref _diodaBlad, value); }
        }

        private bool _diodaRun;
        public bool DiodaRun
        {
            get { return _diodaRun; }
            set { SetProperty(ref _diodaRun, value); }
        }
        private string _cykl;
        public string Cykl
        {
            get { return _cykl; }
            set { SetProperty(ref _cykl, value); }
        }

        private string _prad;
        public string Prad
        {
            get { return _prad; }
            set { SetProperty(ref _prad, value); }
        }

        private bool _cz1;
        public bool Cz1
        {
            get { return _cz1; }
            set { SetProperty(ref _cz1, value); }
        }

        private bool _cz1wl;
        public bool Cz1wl
        {
            get { return _cz1wl; }
            set { SetProperty(ref _cz1wl, value); }
        }

        private bool _cz2wl;
        public bool Cz2wl
        {
            get { return _cz2wl; }
            set { SetProperty(ref _cz2wl, value); }
        }

        private bool _cz2;
        public bool Cz2
        {
            get { return _cz2; }
            set { SetProperty(ref _cz2, value); }
        }



        private bool _cz3wl;
        public bool Cz3wl
        {
            get { return _cz3wl; }
            set { SetProperty(ref _cz3wl, value); }
        }

        private bool _cz3;
        public bool Cz3
        {
            get { return _cz3; }
            set { SetProperty(ref _cz3, value); }
        }


        private bool _cz4wl;
        public bool Cz4wl
        {
            get { return _cz4wl; }
            set { SetProperty(ref _cz4wl, value); }
        }


        private bool _cz4;
        public bool Cz4
        {
            get { return _cz4; }
            set { SetProperty(ref _cz4, value); }
        }


        private string _cz1Opis;
        public string Cz1Opis
        {
            get { return _cz1Opis; }
            set { SetProperty(ref _cz1Opis, value); }
        }

        private string _cz2Opis;
        public string Cz2Opis
        {
            get { return _cz2Opis; }
            set { SetProperty(ref _cz2Opis, value); }
        }

        private string _cz3Opis;
        public string Cz3Opis
        {
            get { return _cz3Opis; }
            set { SetProperty(ref _cz3Opis, value); }
        }

        private string _cz4Opis;
        public string Cz4Opis
        {
            get { return _cz4Opis; }
            set { SetProperty(ref _cz4Opis, value); }
        }


        private string _tytul;
        public string Tytul
        {
            get { return _tytul; }
            set { SetProperty(ref _tytul, value); }
        }


        public override void StanOffline()
        {
            DiodaAuto = false;
            DiodaBlad = false;
            DiodaRun = false;
            Cykl = "n/d";
        }

        public override Task OdczytDanych()
        {
            byte[] Dane;
            Dane = new byte[1];
            if (Polaczony)
            {

                StatusyUrzadzenia dane = new StatusyUrzadzenia();
                byte[] Statusy;
                Statusy = new byte[1];
                // dane = Connection.PobierzDane(urzadzenie) as StatusyUrzadzenia;
                Polaczony = Connection.SprawdzPolaczenie();
                DekodujDane(dane);

            }

            return Task.CompletedTask;
        }

        public void DekodujDane(StatusyUrzadzenia dane)
        {
            //Tytul = dane.
            DiodaAuto = dane.Auto;
            DiodaBlad = dane.Blad;
            Cykl = dane.KrokCyklu;
            Prad = dane.Prad + " A";
            Cz1 = dane.Cz1;
            Cz2 = dane.Cz2;
            Cz3 = dane.Cz3;
            Cz4 = dane.Cz4;


        }

        public override void PrzejdzDoRoota()
        {
            _navigationService.NavigateAsync("/MainPage");


        }

        public void PobierzUrzadzenie(DaneUrzadzenia dane)
        {
            urzadzenie = dane;
            Tytul = urzadzenie.Opis;
        }

        public void KonfigurujCzujniki()
        {
            int ilosci = urzadzenie.IloscCzujnikow;
            switch (ilosci)
            {
                case 1:
                    Cz1Opis = "Czujnik wyjazdowy";
                    Cz1wl = true;
                    break;
                case 2:
                    Cz1Opis = "Czujnik wjazdowy";
                    Cz1wl = true;
                    Cz2Opis = "Czujnik wyjazdowy";
                    Cz2wl = true;
                    break;
                case 3:
                    Cz1Opis = "Czujnik wjazdowy";
                    Cz1wl = true;
                    Cz2Opis = "Czujnik wyjazdowy";
                    Cz2wl = true;
                    Cz3Opis = "Czujnik po przekątnej";
                    Cz3wl = true;
                    break;
                case 4:
                    Cz1Opis = "Czujnik zatrzymania poz 0";
                    Cz1wl = true;
                    Cz2Opis = "Czujnik zwolnienia poz 0";
                    Cz2wl = true;
                    Cz3Opis = "Czujnik zwolnienia poz 1";
                    Cz3wl = true;
                    Cz4Opis = "Czujnik zatrzymania poz 1";
                    Cz4wl = true;
                    break;
            }
        }
    }




}

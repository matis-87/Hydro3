using Prism.Mvvm;
using Hydro3.Kontrolki;
using Hydro3.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Prism.Commands;

namespace Hydro3.Services
{
    public class MainViewHelper : s7ConnectionHelper
    {

        string[] dni;
        public MainViewHelper()
        {
            Connection = new RemoteConnection(); //ArduGlowneDane();
            AktywneZadanie1 = new AktywneZadanie();
            AktywneZadanie2 = new AktywneZadanie();
            AktywneZadanie3 = new AktywneZadanie();
            AktywneZadanie4 = new AktywneZadanie();
            AktywneZadanie5 = new AktywneZadanie();
            NastepneZadanie = new NastepneZadanie();

            NiePodlewaj = "Nie podlewaj";
            dni = new string[7];
            dni[0] = "Nd";
            dni[1] = "Pn";
            dni[2] = "Wt";
            dni[3] = "Sr";
            dni[4] = "Czw";
            dni[5] = "Pt";
            dni[6] = "Sb";
           

        }


        private String _niePodlewaj;
        public String NiePodlewaj
        {
            get { return _niePodlewaj; }
            set { SetProperty(ref _niePodlewaj, value); }
        }


        private bool _low;
        public bool Low
        {
            get { return _low; }
            set { SetProperty(ref _low, value); }
        }

        private bool _med;
        public bool Med
        {
            get { return _med; }
            set { SetProperty(ref _med, value); }
        }

        private bool _high;
        public bool High
        {
            get { return _high; }
            set { SetProperty(ref _high, value); }
        }





        private double _wodaProgress;
        public double WodaProgress
        {
            get { return _wodaProgress; }
            set { SetProperty(ref _wodaProgress, value); }
        }


        private String _wilgGleby;
        public String WilgGleby
        {
            get { return _wilgGleby; }
            set { SetProperty(ref _wilgGleby, value); }
        }



        private String _temperatura;
        public String Temperatura
        {
            get { return _temperatura; }
            set { SetProperty(ref _temperatura, value); }
        }

        private String _sygnalwifi;
        public String SygnalWifi
        {
            get { return _sygnalwifi; }
            set { SetProperty(ref _sygnalwifi, value); }
        }


        private String _wilgotnosc;
        public String Wilgotnosc
        {
            get { return _wilgotnosc; }
            set { SetProperty(ref _wilgotnosc, value); }
        }

        private bool _brakZadan;
        public bool BrakZadan
        {
            get { return _brakZadan; }
            set { SetProperty(ref _brakZadan, value); }
        }

        private int _licznik;
        private int Licznik
        {
            get { return _licznik; }
            set { 
                _licznik = value;
                        }
        }

        private String _czas;
        public String Czas
        {
            get { return _czas; }
            set { SetProperty(ref _czas, value); }
        }

        private String _dzienTygodnia;
        public String DzienTygodnia
        {
            get { return _dzienTygodnia; }
            set { SetProperty(ref _dzienTygodnia, value); }
        }


        private String _nastepnydzienTygodnia;
        public String NastepnyDzienTygodnia
        {
            get { return _nastepnydzienTygodnia; }
            set { SetProperty(ref _nastepnydzienTygodnia, value); }
        }



        public AktywneZadanie AktywneZadanie1 { get; set; }


        public AktywneZadanie AktywneZadanie2 { get; set; }


        public AktywneZadanie AktywneZadanie3 { get; set; }



        public AktywneZadanie AktywneZadanie4 { get; set; }


        public AktywneZadanie AktywneZadanie5 { get; set; }

        public NastepneZadanie NastepneZadanie { get; set; }


        public void DekodujDane(GlowneDane dane)
        {
   
        }

        public override void StanOffline()
        {
  
        }

        public async override Task OdczytDanych()
        {
            byte[] Dane;
            Dane = new byte[1];
            if (Polaczony)
            {
                AdresS7 adres = new AdresS7();
                byte[] dane;//  GlowneDane dane = new GlowneDane();

                byte[] Statusy;
                Statusy = new byte[1];
                Object d = await Connection.PobierzDane(0);
                Polaczony = Connection.SprawdzPolaczenie();
                if (d != null)
                {
                    dane = d as byte[];
                    TimeSpan Godz;
                    TimeSpan GodzRozpoczecia;

                    //                DekodujDane(dane);
                    AktywneZadanie1.IsActive = BitManipulation.GetBitAt(dane[0], 0);
                    AktywneZadanie1.Remaining = dane[1];
                    AktywneZadanie1.WateringTime = dane[2];
                    Temperatura = dane[3].ToString() + "." + dane[4].ToString().Substring(0, 1) + "°C";
                    Wilgotnosc = dane[5].ToString() + "." + dane[6].ToString().Substring(0, 1) + "%";
                    Godz = new TimeSpan(dane[7], dane[8], 0);
                    int tempDzien = dane[28];
                    byte wifi = dane[29];
                    int tempNastepnyDzien = dane[30];
                    SygnalWifi = "Wifi: " + wifi.ToString() + " %";
                    if (tempDzien > 0)
                        DzienTygodnia = dni[tempDzien - 1];
                    else
                        DzienTygodnia = dni[0];
               //     NastepnyDzienTygodnia = dni[tempNastepnyDzien - 1];
                    Czas = DzienTygodnia + " " + Godz.ToString(@"hh\:mm");
                    int strefa = dane[9];
                    GodzRozpoczecia = new TimeSpan(dane[10], dane[11], 0);
                    int h = 0;
                    if (strefa > 0)
                    {
                        NastepneZadanie.Aktywne = true;
                        NastepneZadanie.NazwaStrefy = "Strefa " + strefa.ToString();
                        String t = GodzRozpoczecia.ToString(@"hh\:mm");
                        NastepneZadanie.GodzinaNawadniania = dni[tempNastepnyDzien]+" "+t;
                        NastepneZadanie.CzasTrwania = Convert.ToInt32(dane[12]);
                        NastepneZadanie.RozpoczecieNawadniania = (GodzRozpoczecia.Subtract(Godz)).ToString(@"hh\:mm");
                    }
                    else
                        NastepneZadanie.Aktywne = false;
                    AktywneZadanie2.IsActive = BitManipulation.GetBitAt(dane[13], 0);
                    AktywneZadanie2.Remaining = dane[14];
                    AktywneZadanie2.WateringTime = dane[15];

                    AktywneZadanie3.IsActive = BitManipulation.GetBitAt(dane[16], 0);
                    AktywneZadanie3.Remaining = dane[17];
                    AktywneZadanie3.WateringTime = dane[18];

                    AktywneZadanie4.IsActive = BitManipulation.GetBitAt(dane[19], 0);
                    AktywneZadanie4.Remaining = dane[20];
                    AktywneZadanie4.WateringTime = dane[21];

                    AktywneZadanie5.IsActive = BitManipulation.GetBitAt(dane[22], 0);
                    AktywneZadanie5.Remaining = dane[23];
                    AktywneZadanie5.WateringTime = dane[24];
                    byte[] byt = new byte[2];
                    byt[0] = dane[26];
                    byt[1] = dane[27];
                    int gleba = BitConverter.ToInt16(dane, 25);
                    

                    double tgleba = Convert.ToDouble(gleba);
                    WodaProgress = tgleba / 672;
                    WilgGleby = Convert.ToInt32(WodaProgress * 100).ToString() + " %";
                    if (WodaProgress < 0.3)
                    {
                        Low = true;
                        Med = false;
                        High = false;
                    }
                    if ((WodaProgress >= 0.3) && ((WodaProgress < 0.6)))
                    {
                        Low = false;
                        Med = true;
                        High = false;
                    }

                    if (WodaProgress >= 0.6)
                    {
                        Low = false;
                        Med = false;
                        High = true;
                    }

                    byte niepodlewaj = dane[27];
                    if (niepodlewaj == 0)
                    {
                        NiePodlewaj = "Nie podlewaj";
                    }
                    if (niepodlewaj == 1)
                    {
                        NiePodlewaj = "Podlewaj";
                    }
                }
            }
             
            if ((!AktywneZadanie1.IsActive) && (!AktywneZadanie2.IsActive) && (!AktywneZadanie3.IsActive) &&
                (!AktywneZadanie4.IsActive) && (!AktywneZadanie5.IsActive))
                BrakZadan = true;
            else
                BrakZadan = false;
            // return Task.CompletedTask;
        }

        public async void fNiePodlewaj()
        {
            byte[] buffer = new byte[4];

            buffer[0] = 1;

            await Connection.SendData(buffer);
        }
        public override void PrzejdzDoRoota()
        {

        }


    }
}

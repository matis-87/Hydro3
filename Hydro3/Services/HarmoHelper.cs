using Hydro3.Kontrolki;
using Hydro3.Model;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace Hydro3.Services
{
    public class HarmoHelper : s7ConnectionHelper
    {

        public Strefa Strefa1 { get; set; }
        public Strefa Strefa2 { get; set; }
        public Strefa Strefa3 { get; set; }
        public Strefa Strefa4 { get; set; }
        public Strefa Strefa5 { get; set; }

        IPageDialogService _dialogService;
        public byte DniStrefyNaByte(Strefa st)
        {
            int dni = 0;
           
            dni += Convert.ToByte(st.NdChcecked) * 1;
            dni += Convert.ToByte(st.PnChcecked) * 2;
            dni += Convert.ToByte(st.WtChcecked) * 4;
            dni += Convert.ToByte(st.SrChcecked) * 8;
            dni += Convert.ToByte(st.CzwChcecked) * 16;
            dni += Convert.ToByte(st.PtChcecked) * 32;
            dni += Convert.ToByte(st.SbChcecked) * 64;

            return Convert.ToByte(dni);
        }
        public HarmoHelper(IPageDialogService dialogService)
        {
            Connection = new RemoteConnection(); //ArduHarmo();
            Strefa1 = new Strefa("Strefa 1");
            Strefa2 = new Strefa("Strefa 2");
            Strefa3 = new Strefa("Strefa 3");
            Strefa4 = new Strefa("Strefa 4");
            Strefa5 = new Strefa("Strefa 5");
            _dialogService = dialogService;
        }

        public async void Zapisz(object data)
        {
  
            object ores = await Connection.SendData(data);
            bool? res = ores as bool?;
            if (res == true)
            {
                await _dialogService.DisplayAlertAsync("Info", "Nowy harmonogram zapisany.", "OK");
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Info", "Nie można zapisać harmonogramu", "OK");

            }
        }

        public string SprawdzKolizje(Strefa StrefaZmieniana, params Strefa[] Strefy)
        {
            for(int i=0; i<Strefy.Length;i++)
            {
               if(((StrefaZmieniana.Czas>Strefy[i].Czas)&&(StrefaZmieniana.Czas<Strefy[i].CzasZakonczenia))||
                 ((StrefaZmieniana.CzasZakonczenia>Strefy[i].Czas)&&(StrefaZmieniana.CzasZakonczenia<Strefy[i].CzasZakonczenia)))  
                {
                    byte t1 = DniStrefyNaByte(StrefaZmieniana);
                    byte t2 = DniStrefyNaByte(Strefy[i]);
                    int t3 = t1&t2;                
                    if ( t3 > 0)
                        return Strefy[i].Nazwa;
                }
            }

            return null;
        }
        public async override Task OdczytDanych()
        {
            byte[] Dane;
            Dane = new byte[1];
            if (Polaczony)
            {
                AdresS7 adres = new AdresS7();
              //  byte[] dane;//  GlowneDane dane = new GlowneDane();

            
        
                Object d = await Connection.PobierzDane(30);

               
                Polaczony = Connection.SprawdzPolaczenie();

            }
        }
            }
}

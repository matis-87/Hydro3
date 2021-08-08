using Prism.Mvvm;
using Prism.Navigation;
using Hydro3.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;

namespace Hydro3.Services
{
    public class s7ConnectionHelper : BindableBase
    {
        public bool Wstrzymaj;
        public IPLCConn Connection;
        protected bool offline;
        private bool _polaczony;
        private bool polaczony_trig;
        public bool Polaczony
        {
            get { return _polaczony; }
            set
            {
                SetProperty(ref _polaczony, value);
                if (value == false)
                    StanOffline();
            /*    if ((value == true) && (!polaczony_trig))
                    WznowPobieranie();
                polaczony_trig = value;*/
            }
        }





        public s7ConnectionHelper()
        {



        }

        public async  Task<bool> Polacz()
        {

            /*   while (!Polaczony && !Wstrzymaj) 
               {*/
                  Polaczony = await Connection.Polacz();
               /*    Polaczony = Connection.SprawdzPolaczenie();
                   if (!Polaczony)
                   {
                       Thread.Sleep(10000);
                   }

               }*/

            int i = 0;


            return Polaczony;
        }

        public void Rozlacz()
        {
            Connection.Rozlacz();
            Polaczony = false; // Connection.SprawdzPolaczenie();
            offline = true;
        }

        public void WstrzymajPobieranie()
        {
            Wstrzymaj = true;
        }

        public void WznowPobieranie()
        {
            Wstrzymaj = false;
        
                OdczytujDane();
        }

        public async void OdczytujDane()
        {

            await Task.Run(async () =>
           {
               do
               {
                   if (!Polaczony)
                   {
                       bool g = await Polacz();

                   }
                   else
                   {
                       await OdczytDanych();

                   }
                   Connection.PrzeslijLifebit();

                   Thread.Sleep(5000);
               }
               while (!Wstrzymaj);
           });
            int h = 0;
                 


           
             
                 }

         async Task<int>   Sprawdz()
        {


            int g = 0;
            return g;
        }

        public void SetButton(object obj)
        {
            Connection.SetBit(obj, true);
        }

        public void ResetButton(object obj)
        {
            Connection.SetBit(obj, false);
        }

        public async void ClickButton(object obj)
        {
            await Connection.PulseBit(obj);
        }

        public void ToggleButton(object obj)
        {
            Connection.ToggleBit(obj);
        }

        public virtual void PrzejdzDoRoota()
        {

        }
        public virtual Task OdczytDanych()
        {
            return Task.CompletedTask;
        }

        public virtual void StanOffline()
        {

        }
    }
}

using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hydro3.Model
{
   public class Strefa: BindableBase
    {
        private bool _PnChcecked;
        public bool PnChcecked
        {
            get { return _PnChcecked; }
            set { SetProperty(ref _PnChcecked, value); }
        }

        private bool _WtChcecked;
        public bool WtChcecked
        {
            get { return _WtChcecked; }
            set { SetProperty(ref _WtChcecked, value); }
        }

        private bool _SrChcecked;
        public bool SrChcecked
        {
            get { return _SrChcecked; }
            set { SetProperty(ref _SrChcecked, value); }
        }

        private bool _CzwChcecked;
        public bool CzwChcecked
        {
            get { return _CzwChcecked; }
            set { SetProperty(ref _CzwChcecked, value); }
        }

        private bool _PtChcecked;
        public bool PtChcecked
        {
            get { return _PtChcecked; }
            set { SetProperty(ref _PtChcecked, value); }
        }

        private bool _SbChcecked;
        public bool SbChcecked
        {
            get { return _SbChcecked; }
            set { SetProperty(ref _SbChcecked, value); }
        }

        private bool _NdChcecked;
        public bool NdChcecked
        {
            get { return _NdChcecked; }
            set { SetProperty(ref _NdChcecked, value); }
        }

        private int _dlugosc;
        public int Dlugosc
        {
            get { return _dlugosc; }
            set 
            {
                CzasDo = ObliczKoniec(Czas, value);
                SetProperty(ref _dlugosc, value); 
            }
        }

        private TimeSpan _czas;
        public TimeSpan Czas
        {
            get { return _czas; }
            set 
            {
                CzasDo = ObliczKoniec(value, Dlugosc);
                SetProperty(ref _czas, value); }
            }

        public TimeSpan CzasZakonczenia;
        private  string ObliczKoniec(TimeSpan poczatek, int dlugosc)
        {

            TimeSpan temp = new TimeSpan(0, dlugosc, 0);
            TimeSpan res = (poczatek + temp);
            CzasZakonczenia = res;
            return $"(Zakończenie o {res:hh\\:mm})";
        }
        private String _czasDo;
        public String CzasDo
        {
            get { return _czasDo; }
            set { SetProperty(ref _czasDo, value); }
        }

        private String _nazwa;
        public String Nazwa
        {
            get { return _nazwa; }
            set { SetProperty(ref _nazwa, value); }
        }


        public void WyborDni(int dzien)
        {
            switch (dzien)
            {
                case 0:
                    PnChcecked = !PnChcecked;
                    break;
                case 1:
                    WtChcecked = !WtChcecked;
                    break;
                case 2:
                    SrChcecked = !SrChcecked;
                    break;
                case 3:
                    CzwChcecked = !CzwChcecked;
                    break;
                case 4:
                    PtChcecked = !PtChcecked;
                    break;
                case 5:
                    SbChcecked = !SbChcecked;
                    break;
                case 6:
                    NdChcecked = !NdChcecked;
                    break;

            }
        }

        public Strefa(string name="Strefa")
        {
            Nazwa = name;
        }
    }
}

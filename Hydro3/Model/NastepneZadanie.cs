using System;
using System.Collections.Generic;
using System.Text;

namespace Hydro3.Model
{
  public  class NastepneZadanie: Prism.Mvvm.BindableBase
    {
        private bool _aktywne;

        public bool Aktywne
        {
            get { return _aktywne; }
            set { SetProperty(ref _aktywne, value); }
        }

        private String _nazwaStrefy;
        public String NazwaStrefy
        {
            get { return _nazwaStrefy; }
            set { SetProperty(ref _nazwaStrefy, value); }
        }

        private String _godzinaNawadniania;
        public String GodzinaNawadniania
        {
            get { return _godzinaNawadniania; }
            set { SetProperty(ref _godzinaNawadniania, value); }

        }

        private String _rozpoczecieNawadniania;
        public String RozpoczecieNawadniania
        {
            get { return _rozpoczecieNawadniania; }
            set { SetProperty(ref _rozpoczecieNawadniania, value); }
        }

        private int _czasTrwania;
        public int CzasTrwania
        {
            get { return _czasTrwania; }
            set { SetProperty(ref _czasTrwania, value); }
        }
    }
}

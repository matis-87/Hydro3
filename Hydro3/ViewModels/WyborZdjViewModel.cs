using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Hydro3.Model;
using System.IO;
using Prism.Services;

namespace Hydro3.ViewModels
{

    public class WyborZdjViewModel : ViewModelBase, INavigationAware
    {

        public DelegateCommand WyborCommand { get; set; }
        public DelegateCommand ZapiszCommand { get; set; }

        private ImageSource _zdj;
        public ImageSource Zdj
        {
            get { return _zdj; }
            set
            {
                SetProperty(ref _zdj, value);
            }
        }

        private string _nazwaStrefy;
        public string NazwaStrefy
        {
            get { return _nazwaStrefy; }
            set
            {
                if((!value.Equals(_nazwaStrefy))&&(_nazwaStrefy!=null)&&(!NowaNazwa))
                 NowaNazwa = true;
                SetProperty(ref _nazwaStrefy, value);
            }
        }

        private string _nrStrefy;
        public string NrStrefy
        {
            get { return _nrStrefy; }
            set
            {
                SetProperty(ref _nrStrefy, value);
            }
        }

        nrStref strefa;
        Stream st;
        MemoryStream memory;
        bool NoweZdj;
        bool NowaNazwa;
        IPageDialogService _dialogService;
        public WyborZdjViewModel(IPageDialogService dialogService, INavigationService navigationService) : base(navigationService)
        {
            _dialogService = dialogService;
            WyborCommand = new DelegateCommand(WyborZdj);
            ZapiszCommand = new DelegateCommand(ZapisZdj);
            PlikCfg.LadujCfg();
            memory = new MemoryStream();
            NrStrefy = nrStref.strefa1.ToString();
        }

        public async void WyborZdj()
        {
            NoweZdj = false;
            st=await ObslugaZdj.WybierzZdj();
            if (st != null)
            {
                NoweZdj = true;
                st.CopyTo(memory);
                st.Position = 0;
                //memory = st;
                long f = memory.Position;
                memory.Position = 0;
                Stream str = Stream.Null;
                //  memory.CopyTo(st);
                Zdj = ImageSource.FromStream(() => st);
            }
           
        }


        public async void ZapisZdj()
        {
            if(NoweZdj)
                ObslugaZdj.ZapiszZdj(strefa, memory);
            if (NowaNazwa)
            {
                PlikCfg.ZmienNazwe(strefa, NazwaStrefy);
                PlikCfg.ZapiszCfg();
            }
            await _dialogService.DisplayAlertAsync("Info", $"Nowe ustawienia zapisane.", "OK");

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            nrStref? temp = parameters["strefa"] as nrStref?;
            NowaNazwa = false;
            strefa = nrStref.strefa1;
            if (temp.HasValue)
                strefa = temp.Value;
            string pierwszy = strefa.ToString()[0].ToString().ToUpper();
            NrStrefy =pierwszy + strefa.ToString().Substring(1);
            NazwaStrefy = PlikCfg.PobierzNazwe(strefa);
            Zdj = ObslugaZdj.LadujZdj(strefa);
        }
    }
}

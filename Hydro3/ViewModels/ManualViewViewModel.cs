using Hydro3.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hydro3.ViewModels
{
    public class ManualViewViewModel : ViewModelBase
    {
        public ManualHelper Helper { get; set; }
        public DelegateCommand S1Command { get; set; }
        public DelegateCommand S2Command { get; set; }
        public DelegateCommand S3Command { get; set; }
        public DelegateCommand S4Command { get; set; }
        public DelegateCommand S5Command { get; set; }

        public ManualViewViewModel(INavigationService navigationService) : base(navigationService)
        {
            Helper = new ManualHelper();
            S1Command = new DelegateCommand(ZapiszS1);
            S2Command = new DelegateCommand(ZapiszS2);
            S3Command = new DelegateCommand(ZapiszS3);
            S4Command = new DelegateCommand(ZapiszS4);
            S5Command = new DelegateCommand(ZapiszS5);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Object client;
            RaisePropertyChanged("IsScanning");
            client = parameters["conn"];
            if (client != null)
            {
                Helper.Connection.SetClient(client);
                Helper.WznowPobieranie();
            }
            }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Helper.WstrzymajPobieranie();
        }

        public async void ZapiszS1()
        {
            byte[] buffer = new byte[4];
           
            buffer[0] = 30;
           if (!Helper.Strefa1.IsActive)
            {
              buffer[1] = Convert.ToByte(Helper.Strefa1.WateringTime); 
            }
           else
            {
                buffer[1] = 0;
            }
           await Helper.Connection.SendData(buffer);
            
        }

        public async void ZapiszS2()
        {
            byte[] buffer = new byte[4];

            buffer[0] = 31;
            if (!Helper.Strefa2.IsActive)
            {
                buffer[1] = Convert.ToByte(Helper.Strefa2.WateringTime);
            }
            else
            {
                buffer[1] = 0;
            }
            await Helper.Connection.SendData(buffer);

        }

        public async void ZapiszS3()
        {
            byte[] buffer = new byte[4];

            buffer[0] = 32;
            if (!Helper.Strefa3.IsActive)
            {
                buffer[1] = Convert.ToByte(Helper.Strefa3.WateringTime);
            }
            else
            {
                buffer[1] = 0;
            }
            await Helper.Connection.SendData(buffer);

        }

        public async void ZapiszS4()
        {
            byte[] buffer = new byte[4];

            buffer[0] = 33;
            if (!Helper.Strefa4.IsActive)
            {
                buffer[1] = Convert.ToByte(Helper.Strefa4.WateringTime);
            }
            else
            {
                buffer[1] = 0;
            }
            await Helper.Connection.SendData(buffer);

        }

        public async void ZapiszS5()
        {
            byte[] buffer = new byte[4];

            buffer[0] = 34;
            if (!Helper.Strefa5.IsActive)
            {
                buffer[1] = Convert.ToByte(Helper.Strefa5.WateringTime);
            }
            else
            {
                buffer[1] = 0;
            }
            await Helper.Connection.SendData(buffer);

        }
    }
}

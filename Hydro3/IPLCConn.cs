using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hydro3
{
    public interface IPLCConn
    {
        Task<bool> Polacz();
        bool SprawdzPolaczenie();
        void Rozlacz();
        void PrzeslijLifebit();

        Task<Object> PobierzDane(Object obj);

        Object GetClient();
        void SetClient(Object obj);
        void SetBit(Object data, bool value);
        Task PulseBit(Object data);
        void ToggleBit(Object data);
        Task<Object> SendData(Object data);
    }

    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}

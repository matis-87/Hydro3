using Hydro3.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hydro3.Model
{
    public class ArduConn : IPLCConn
    {
        protected Sharpduino Client;
        private String IP = "192.168.0.13";
        public int ConnResult = 0;
        public bool Lifebit;
        protected bool Sim = true;
        public byte[] buffor;

        public ArduConn()
        {
            Client = new Sharpduino();
            buffor = new byte[12];
        }
        public async Task<bool> Polacz()
        {
            Client.Connect(IP, 8080);
            if (Sim)
                ConnResult = 0;

            return true;
          
        }

        public bool SprawdzPolaczenie()
        {
            return Client.connected;
        }

        public void Rozlacz()
        {
            Client.Disconnect();
            
        }

        public void PrzeslijLifebit()
        {
            int i;
            i = 0;

        }

        public virtual async Task<Object> PobierzDane(object obj)
        {

            byte[] buff = new byte[10];
            
            Client.ReadData(2, 10, ref buff);
            Object d = new object();
            return d;
        }


   
        public object GetClient()
        {
            return Client;
        }

        public void SetClient(object obj)
        {
            Client = obj as Sharpduino;
            
        }

        public void SetBit(object data, bool value)
        {
            throw new NotImplementedException();
        }

        public Task PulseBit(object data)
        {
            throw new NotImplementedException();
        }

        public void ToggleBit(object data)
        {
            throw new NotImplementedException();
        }

    public virtual async Task<Object>SendData( object data)
        {
            byte[] dat = data as byte[];
            byte[] buff = new byte[dat.Length - 1];
            for (int i=0;i<buff.Length;i++)
            {
                buff[i] = dat[i + 1];
            }
            Client.WriteData(dat[0], buff.Length, buff); 
            return data;
        }
    }
}

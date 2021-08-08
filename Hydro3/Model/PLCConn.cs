using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hydro3.Kontrolki;
using Sharp7;

namespace Hydro3.Model
{
    public class PLCConn : IPLCConn
    {
        protected S7Client Client;
        private String PLCIP = "192.168.0.13";
        public int ConnResult = 0;
        public bool Lifebit;
        protected bool Sim = true;
        public byte[] buffor;
        public PLCConn()
        {
            Client = new S7Client();
            Client.SetConnectionParams(PLCIP, 0, 0);
        }
        public PLCConn(S7Client client)
        {

            Client = new S7Client();
            Client = client;
            Client.SetConnectionParams(PLCIP, 0, 0);
        }
        public async Task<bool>  Polacz()
        {

            ConnResult = Client.ConnectTo(PLCIP, 0, 0);
            if (Sim)
                ConnResult = 0;
            //  return true;
            return true;
        }

        public void Rozlacz()
        {
            Client.Disconnect();
        }




        public void PrzeslijLifebit()
        {
            const int LifebitDB = 2;
            const int LifebitStart = 0;
            byte[] buf = new byte[1];

            Client.ReadArea(S7Consts.S7AreaDB, LifebitDB, LifebitStart, 1, S7Consts.S7WLBit, buf);
            Lifebit = S7.GetBitAt(buf, 0, 0);
            Lifebit = !Lifebit;
            S7.SetBitAt(ref buf, 0, 0, Lifebit);
            if (Client.Connected)
                Client.WriteArea(S7Consts.S7AreaDB, LifebitDB, LifebitStart, 1, S7Consts.S7WLBit, buf);


        }

        public bool SprawdzPolaczenie()
        {
            if (!Sim)
                return Client.Connected;


            return true;
        }

        public Object GetClient()
        {

            return Client;


        }

        public void SetClient(Object client)
        {
            Client = client as S7Client;
        }

        public virtual Task<Object> PobierzDane(object obj)
        {
            throw new NotImplementedException();
        }

        public void SetBit(Object data, bool value)
        {
            AdresS7 dane = data as AdresS7;

            byte[] buf = new byte[1];
            S7.SetBitAt(ref buf, 0, dane.Pos, value);
            if (Client.Connected)
                Client.WriteArea(S7Consts.S7AreaDB, dane.DB, dane.Start, 1, S7Consts.S7WLBit, buf);
        }

        public Task PulseBit(Object data)
        {
            AdresS7 dane = data as AdresS7;
            byte[] buf = new byte[1];

            S7.SetBitAt(ref buf, 0, dane.Pos, true);
            if (Client.Connected)
            {
                Client.WriteArea(S7Consts.S7AreaDB, dane.DB, dane.Start, 1, S7Consts.S7WLBit, buf);
                Task.Delay(500);
                S7.SetBitAt(ref buf, 0, dane.Pos, false);
                Client.WriteArea(S7Consts.S7AreaDB, dane.DB, dane.Start, 1, S7Consts.S7WLBit, buf);
            }
            return Task.CompletedTask;
        }

        public void ToggleBit(Object data)
        {
            AdresS7 dane = data as AdresS7;
            byte[] buf = new byte[1];
            bool tempBit;
            Client.ReadArea(S7Consts.S7AreaDB, dane.DB, dane.Start, 1, S7Consts.S7WLBit, buf);
            tempBit = S7.GetBitAt(buf, 0, dane.Pos);
            tempBit = !tempBit;
            S7.SetBitAt(ref buf, 0, dane.Pos, tempBit);
            if (Client.Connected)
                Client.WriteArea(S7Consts.S7AreaDB, dane.DB, dane.Start, 1, S7Consts.S7WLBit, buf);
        }

        public virtual Task<Object> SendData(Object data)
        {
            return null;
        }
    }
}

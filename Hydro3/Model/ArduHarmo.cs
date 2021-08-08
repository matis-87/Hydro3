using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hydro3.Model
{
    class ArduHarmo : ArduConn
    {
        public async override Task<Object> PobierzDane(object obj)
        {

            return await Task.Run<Object>(() =>
            {
                byte[] buff = new byte[32];
             int res =   Client.ReadData(30, 32, ref buff);
                //   buff[0] = 30;
                Object d = buff;
                if (res == 1)
                    return d;
                else
                    return null;

            });

        }

        public override async Task<Object> SendData(object data)
        {

            return await Task.Run<Object>(() =>
            {
                bool res;
                byte[] dat = data as byte[];
                byte[] buff = new byte[dat.Length - 1];
                for (int i = 0; i < buff.Length; i++)
                {
                    buff[i] = dat[i + 1];
                }
                res = Client.WriteData(dat[0], buff.Length, buff);
                return res;
            });
        }

    }
}

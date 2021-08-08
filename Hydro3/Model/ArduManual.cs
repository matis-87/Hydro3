using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hydro3.Model
{
public    class ArduManual : ArduConn
    {
        public async override Task<Object> PobierzDane(object obj)
        {

            return await Task.Run<Object>(() =>
            {
                byte[] buff = new byte[15];
                Client.ReadData(3, 15, ref buff);
             //   buff[0] = 30;
                Object d = buff;
                return d;
               
            });
            
        }

        public override async Task<Object> SendData(object data)
        {

            return await Task.Run<Object>(() =>
            {
                byte[] dat = data as byte[];
                byte[] buff = new byte[dat.Length - 1];
                for (int i = 0; i < buff.Length; i++)
                {
                    buff[i] = dat[i + 1];
                }
                Client.WriteData(dat[0], buff.Length, buff);
                return data;
        });
        }


    }
}

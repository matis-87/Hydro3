using Hydro3.Kontrolki;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hydro3.Model
{
    class ArduGlowneDane : ArduConn
    {
        public async override Task<Object> PobierzDane(object obj)
        {
            AdresS7 adres = obj as AdresS7;
            GlowneDane dane = new GlowneDane();
            return await Task.Run<Object>(() =>
             {
                 byte[] buff = new byte[34];
               int res =  Client.ReadData(0, 34, ref buff);
                 if (res == 1)
                 {
                     Object d = buff;
                     return d;
                 }
                 else
                     return null;
             });
        }


    }
}

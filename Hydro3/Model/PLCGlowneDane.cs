using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hydro3.Kontrolki;
using Sharp7;

namespace Hydro3.Model
{
    public class PLCGlowneDane : PLCConn
    {

        public PLCGlowneDane()
        {

        }

        public async override Task<Object> PobierzDane(object obj)
        {
            AdresS7 adres = obj as AdresS7;
            GlowneDane dane = new GlowneDane();
            return await Task.Run<Object>(() =>
            {
                byte[] buff = new byte[adres.Len];
                Client.DBRead(adres.DB, adres.Start, adres.Len, buff);
                dane.Auto = S7.GetBitAt(buff, 0, 0);
                dane.Blad = S7.GetBitAt(buff, 0, 1);
                dane.Safety = S7.GetBitAt(buff, 0, 2);
                dane.LicznikPalet = S7.GetIntAt(buff, 1).ToString();
                int i = 0;
                Object d = dane;
                return d;
            });
        }
    }
}

using Hydro3.Kontrolki;
using Sharp7;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hydro3.Model
{
    public class PLCUrzadzenie : PLCConn
    {

        public PLCUrzadzenie()
        {

        }

        public async override Task<Object> PobierzDane(object obj)
        {
            DaneUrzadzenia urzadzenie;
            StatusyUrzadzenia dane = new StatusyUrzadzenia();
            urzadzenie = obj as DaneUrzadzenia;
            return await Task.Run<Object>(() =>
            {
                //Pobranie statusow
                byte[] buff = new byte[9];
                Client.DBRead(urzadzenie.DBStatUrzadzenia, urzadzenie.OffsetStats, 5, buff);
                dane.Auto = S7.GetBitAt(buff, 0, 0);
                dane.Blad = S7.GetBitAt(buff, 0, 1);
                dane.KrokCyklu = S7.GetIntAt(buff, 1).ToString();
                dane.Prad = S7.GetIntAt(buff, 5).ToString();
                //Pobranie czujnikow
                buff = new byte[1];
                Client.DBRead(urzadzenie.DBCzujnikow, urzadzenie.OffsetCzujnikow, 1, buff);
                dane.Cz1 = S7.GetBitAt(buff, 0, 0);
                dane.Cz2 = S7.GetBitAt(buff, 0, 1);
                dane.Cz3 = S7.GetBitAt(buff, 0, 2);
                dane.Cz4 = S7.GetBitAt(buff, 0, 3);

                return dane;
            });
        }
    }
}

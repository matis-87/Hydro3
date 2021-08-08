using System;
using System.Collections.Generic;
using System.Text;

namespace Hydro3.Model
{
    public class SysPal
    {
        public int Ilosc;
        public int DBSysStat;
        public DaneUrzadzenia[] Urzadzenia;
        public int Znak;
        public int Dopelnienie;
        String Kod;

        public SysPal()
        {

        }

        public void Dekoduj(String InKod)
        {
            int dl;
            dl = InKod.Length;
            Ilosc = Convert.ToInt32(InKod.Substring(0, 1));
            Urzadzenia = new DaneUrzadzenia[Ilosc];
            for (int i = 0; i < Ilosc; i++)
            {
                Urzadzenia[i] = new DaneUrzadzenia();
            }
            DBSysStat = Convert.ToInt32(InKod.Substring(1, 2));
            int j = 3;
            for (int i = 0; i < Ilosc; i++)
            {
                Urzadzenia[i].DlugoscOpisu = Convert.ToInt32(InKod.Substring(j, 3));
                j += 3;
                Urzadzenia[i].Opis = InKod.Substring(j, Urzadzenia[i].DlugoscOpisu);
                j += Urzadzenia[i].DlugoscOpisu;
                Urzadzenia[i].DBStatUrzadzenia = Convert.ToInt32(InKod.Substring(j, 2));
                j += 2;
                Urzadzenia[i].OffsetStats = Convert.ToInt32(InKod.Substring(j, 3));
                j += 3;
                Urzadzenia[i].IloscCzujnikow = Convert.ToInt32(InKod.Substring(j, 2));
                j += 2;
                Urzadzenia[i].DBCzujnikow = Convert.ToInt32(InKod.Substring(j, 2));
                j += 2;
                Urzadzenia[i].OffsetCzujnikow = Convert.ToInt32(InKod.Substring(j, 3));
                j += 3;
                Urzadzenia[i].DBKomand = Convert.ToInt32(InKod.Substring(j, 2));
                j += 2;
                Urzadzenia[i].OffsetKomand = Convert.ToInt32(InKod.Substring(j, 3));
                j += 3;

            }
        }

        public int SprawdzKod(String InKod)
        {
            int znak = 0;
            int dopelnienie = 0;
            int dlugosc = 0;
            int suma = 0;

            dlugosc = InKod.Length;
            if (dlugosc < 6)
                return 1;
            try
            {
                znak = Convert.ToInt32(InKod.Substring(dlugosc - 3, 1));
            }
            catch (Exception e)
            {
                return 1;
            }
            dopelnienie = Convert.ToInt32(InKod.Substring(dlugosc - 2, 2));
            if (znak == 1)
                dopelnienie = dopelnienie * (-1);
            suma = dlugosc - 3 + dopelnienie;
            if (suma == 69)
                return 0;
            else
                return 1;



        }
    }
}

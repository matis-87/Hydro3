using System;
using System.Collections.Generic;
using System.Text;

namespace Hydro3.Kontrolki
{
    class AdresS7
    {
        public AdresS7()
        {
            DB = 2;
            Start = 0;
            Len = 5;
        }
        public int DB { get; set; }
        public int Start { get; set; }
        public int Len { get; set; }
        public int Pos { get; set; }


    }
}

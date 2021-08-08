using System;
using System.Collections.Generic;
using System.Text;

namespace Hydro3.Model
{
   public class  BitManipulation
    {
        public static bool GetBitAt(byte data, int BitPos)
        {
            bool res;
            res = Convert.ToBoolean((data & (1 << BitPos)));


            return res;
        }


    }
}

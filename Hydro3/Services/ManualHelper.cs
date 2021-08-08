using Hydro3.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hydro3.Services
{
    public class ManualHelper : s7ConnectionHelper
    {
        public TrybRecznyStrefy Strefa1 { get; set; }
        public TrybRecznyStrefy Strefa2 { get; set; }
        public TrybRecznyStrefy Strefa3 { get; set; }
        public TrybRecznyStrefy Strefa4 { get; set; }
        public TrybRecznyStrefy Strefa5 { get; set; }

        public ManualHelper()
        {
            Connection = new RemoteConnection(); //ArduManual();
            Strefa1 = new TrybRecznyStrefy();
            Strefa2 = new TrybRecznyStrefy();
            Strefa3 = new TrybRecznyStrefy();
            Strefa4 = new TrybRecznyStrefy();
            Strefa5 = new TrybRecznyStrefy();

            Strefa1.IsActive = false;
            Strefa1.WateringTime = 20;

            Strefa2.IsActive = false;
            Strefa2.WateringTime = 20;

            Strefa3.IsActive = false;
            Strefa3.WateringTime = 20;

            Strefa4.IsActive = false;
            Strefa4.WateringTime = 20;

            Strefa5.IsActive = false;
            Strefa5.WateringTime = 20;
        }
        public override async Task OdczytDanych()
        {
            object temp = new object();
            byte[] buff = new byte[15];
            temp = await Connection.PobierzDane(3);
            buff = temp as byte[];
            Strefa1.IsActive = BitManipulation.GetBitAt(buff[0], 0);
            Strefa1.NonChangable = !Strefa1.IsActive;

            Strefa1.Remaining = buff[1];
            if (Strefa1.IsActive)
            {

                Strefa1.WateringTime = buff[2];
            }

            Strefa2.IsActive = BitManipulation.GetBitAt(buff[3], 0);
            Strefa2.Remaining = buff[4];
            Strefa2.NonChangable = !Strefa2.IsActive;
            if (Strefa2.IsActive)
            {
                
                Strefa2.WateringTime = buff[5];
                
            }
            Strefa3.IsActive = BitManipulation.GetBitAt(buff[6], 0);
            Strefa3.Remaining = buff[7];
            Strefa3.NonChangable = !Strefa3.IsActive;
            if (Strefa3.IsActive)
            {
                
                Strefa3.WateringTime = buff[8];
                
            }
            Strefa4.IsActive = BitManipulation.GetBitAt(buff[9], 0);
            Strefa4.Remaining = buff[10];
            Strefa4.NonChangable = !Strefa4.IsActive;
            if (Strefa4.IsActive)
            {
                
                Strefa4.WateringTime = buff[11];
                
            }
           Strefa5.NonChangable = !Strefa5.IsActive;
            Strefa5.IsActive = BitManipulation.GetBitAt(buff[12], 0);
            Strefa5.Remaining = buff[13];
            if (Strefa5.IsActive)
            {
               
                Strefa5.WateringTime = buff[14];
                
            }
            //return Task.CompletedTask;
        }
    }
}

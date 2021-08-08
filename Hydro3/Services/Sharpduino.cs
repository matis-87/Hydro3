using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hydro3.Services
{
    public class Sharpduino
    {
        bool semaphore;
        UdpClient client;
        string adressIP;
        int port;
        public bool connected;
        public int Timeout;
        IPEndPoint RemoteIpEndPoint;
        public byte[] buffer;

        public Sharpduino()
        {
            client = new UdpClient();
            port = 8080;
            RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, port);
            Timeout = 10;
            buffer = new byte[10];
            client.Client.ReceiveTimeout = 5000;
        }
        public void Connect(string IP, int port)
        {
            adressIP = IP;
            this.port = port;

            byte[] buff = new byte[1];
            buff[0] = 1;
            int result;
            client.Connect(adressIP, this.port);
            result = client.Send(buff, buff.Length);
            int TempTime = 0;
            
            try
            {
                buff = client.Receive(ref RemoteIpEndPoint);
                if (buff.Length > 2)
                    connected = true;
            }
            catch
            {
                connected = false;
            }
            //  else
            //         connected = false;
            //return Task.CompletedTask;
            // return Task.CompletedTask;
        }


        public int Disconnect()
        {

            connected = false;
            return 0;
        }

        public  bool WriteData(byte Adres, int Length, byte[] buff)
        {
            while(semaphore)
            {
                Task.Delay(800);
            }
            semaphore = true;
            byte[] ramka = new byte[buff.Length + 2];
            for (int i = 0; i < buff.Length; i++)
            {
                ramka[i + 2] = buff[i];
            }
            ramka[0] = 1;
            ramka[1] = Adres;

            client.Send(ramka, ramka.Length);
            int tdl = ramka.Length;
            try
            {
                ramka = client.Receive(ref RemoteIpEndPoint);
                if (ramka.Length >10)
                {
                    connected = true;
                    if ((ramka[0] == 3) && (ramka[1] == Adres))
                    {
                        if(ramka[2]==1)
                        connected = true;
                        semaphore = false;
                        return true;
                    }
                }
            }
            catch
            {
                connected = false;
                semaphore = false;
                return false;
            }

            semaphore = false;
            return false;
        }

        public int ReadData(int Adres, int Length, ref byte[] buff)
        {

            if (!semaphore)
            {

                semaphore = true;
                byte[] ramka = new byte[Length + 2];
                ramka[0] = 2;
                ramka[1] = Convert.ToByte(Adres);
                buff = new byte[20];
                client.Send(ramka, ramka.Length);
                try
                {
                    ramka = client.Receive(ref RemoteIpEndPoint);
                    if (ramka.Length >= 12)
                    {
                        if ((ramka[0] == 3) && (ramka[1] == Adres))
                        {
                            buff = new byte[ramka.Length - 2];
                            for (int i = 0; i < buff.Length; i++)
                            {
                                buff[i] = ramka[i + 2];

                            }
                            connected = true;

                            semaphore = false;
                            return 1;
                        }
                    }
                }
                catch(Exception e)
                {
                    semaphore = false;
                    connected = false;
                    return 0;
                }

               
              
            }
            semaphore = false;
            return 0;
            }
        
    }
}

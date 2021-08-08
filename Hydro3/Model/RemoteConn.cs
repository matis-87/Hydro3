using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hydro3.Model
{
    public class RemData
    {
        public decimal temp;
        public decimal hum;
        public int soilmost;
    }
    public class RemoteConn : IPLCConn
    {

      RemData data;
      public  RemoteConn()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            data = new RemData();
        }
       static HttpClient client;
        public object GetClient()
        {
            return null;
        }

        public async Task<object> PobierzDane(object obj)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            return await Task.Run<Object>(async () =>
            {
                Object cos = await  GetProductAsync("http://www.hydroremote.somee.com/T3/remote/sterownik");
                return cos;
            });
     
        }


        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        async Task<Object> GetProductAsync(string path)
        {
            byte[] dane = new byte[36];
            //     Product product = null;

            try
            {
             
                    // var jsonString = JsonConvert.SerializeObject(new { id= "000001" });
                    HttpContent arg = new StringContent("jsonString");
                    var jsonString = "{\"temp\":90.15,\"hum\":40.00,\"soilmost\":40}";
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    //  arg.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await client.GetAsync(path);
                

                    if (response.IsSuccessStatusCode)
                    {
                    client.CancelPendingRequests();
                    var product = await response.Content.ReadAsStringAsync();

                        RemData g = JsonConvert.DeserializeObject<RemData>(product);
                        int temp = Convert.ToInt32(g.temp);
                    int hum = Convert.ToInt32(g.hum);
                        dane[3] = Convert.ToByte(temp);
                        dane[5] = Convert.ToByte(hum);
                    byte[] moist = BitConverter.GetBytes(g.soilmost);
                    dane[25] = moist[0];
                    dane[26] = moist[1];
                    Object d = dane;
                        return d;
                    //26,  27

                    }
                 

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }
        public async Task<bool> Polacz()
        {
            return true;
        }

        public void PrzeslijLifebit()
        {
          //  throw new NotImplementedException();
        }

        public Task PulseBit(object data)
        {
            throw new NotImplementedException();
        }

        public void Rozlacz()
        {
            throw new NotImplementedException();
        }

        public Task<object> SendData(object data)
        {
            throw new NotImplementedException();
        }

        public void SetBit(object data, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetClient(object obj)
        {
            throw new NotImplementedException();
        }

        public bool SprawdzPolaczenie()
        {
            return true;
        }

        public void ToggleBit(object data)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Hydro3.Model
{
    class RemoteConnection: IPLCConn
    {
        public const String DevURI = "http://hydroremote.somee.com/T3/remote/sterownik/";
      static  HttpClient client;
        public bool isConnected;
        string token;
        public RemoteConnection()
        {
            client = new HttpClient();
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        }


        public async void GetConnectionStatus()
        {

            StringBuilder sb = new StringBuilder(DevURI);
            sb.Append("connstat/");
            sb.Append(token);

            string uri = sb.ToString();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                int g = 0;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<Object>();
                    isConnected = (data as bool?).GetValueOrDefault();
                }
                else
                    isConnected = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                isConnected = false;
            }
        
        }

        public async Task<Object> Read(int cmd)
        {

            StringBuilder sb = new StringBuilder(DevURI);
            sb.Append("Data/Stats/");
            sb.Append(cmd + "/");
            sb.Append(token);

            string uri = sb.ToString();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                int g = 0;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<Object>();
                    if (data == null)
                        isConnected = false;
                    else
                    isConnected = true;
                    return data;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        private async  Task CzekajAsync()
        {
           await  Task.Run(() => Czekaj());
        }

        private int Czekaj()
        {
            int h = 0;
            Thread.Sleep(3000);
            h = 1;
            return h;
        }

        public   void Czek()
        {
          
        }
        public  async void Polacz2Async()
        {


      
                StringBuilder sb = new StringBuilder(DevURI);
                sb.Append("login");
                string uri = sb.ToString();
                userData user = new userData();
                user.User = "matis";
                user.Password = "haslo";
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                HttpContent arg = new StringContent("jsonString");
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await client.PutAsync(uri, httpContent);
                    int h = 1;  
                    if (response.IsSuccessStatusCode)
                    {
                        var toke = await response.Content.ReadAsAsync<Object>();
                        token = toke as String;
                        Console.WriteLine("===Uzyskano token:{0} ====", token);
                        isConnected = true;

                    }
                    else
                        isConnected = false;

                }
                catch (Exception e)
                {

                }
       


        
      


        }

        public async Task<bool> Polacz()
        {
            try
            {
                StringBuilder sb = new StringBuilder(DevURI);
                sb.Append("login");
                string uri = sb.ToString();
                userData user = new userData();
                user.User = "matis";
                user.Password = "haslo";
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                HttpContent arg = new StringContent("jsonString");
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var toke = await response.Content.ReadAsAsync<Object>();
                    token = toke as String;
                    Console.WriteLine("===Uzyskano token:{0} ====", token);
                    isConnected = true;
                
                }
                else
                    isConnected = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                isConnected = false;
              //  return -1;
            }

            return isConnected;
        }

        public bool SprawdzPolaczenie()
        {
           // GetConnectionStatus();
            return isConnected;
                
        }

        public void Rozlacz()
        {
           // throw new NotImplementedException();
        }

        public void PrzeslijLifebit()
        {
            //throw new NotImplementedException();
        }

        public async Task<object> PobierzDane(object obj)
        {
            //return await Task.Run<Object>(async () =>
            // {
            byte[] result;
                int cmd = (obj as int?).GetValueOrDefault();
                object ob = await Read(cmd);
            if (ob != null)
            {
                byte[] buff = Convert.FromBase64String((ob as String));
                result = new byte[buff.Length - 2];
                for (int i = 2; i < buff.Length; i++)
                {
                    result[i - 2] = buff[i];
                }
            }
            else
            {
                result = null;
            }

//                buff.CopyTo(result, 2);

                return result;
               
          //  });
        }
    

        public object GetClient()
        {
            // throw new NotImplementedException();
            return this;
        }

        public void SetClient(object obj)
        {
            RemoteConnection temp = obj as RemoteConnection;
            this.token = temp.token;
            this.isConnected = temp.isConnected;
           
            //this = obj;
        }

        public void SetBit(object data, bool value)
        {
          //  throw new NotImplementedException();
        }

        public Task PulseBit(object data)
        {
            throw new NotImplementedException();
        }

        public void ToggleBit(object data)
        {
            throw new NotImplementedException();
        }

        public Task<object> SendData(object data)
        {
            // throw new NotImplementedException();
            return null;
        }
    }
}

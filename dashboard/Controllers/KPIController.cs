using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace dashboard.Controllers
{
    public class KPIController : ApiController
    {
        public async Task<JArray> GetKPIs()
        {
            var client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            string authInfo = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(String.Format("{0}:{1}", "pischool\\student01", "student"))); //user account and password
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authInfo);

            try
            {
                //this will cause the server call to ingore invailid SSL Cert - should remove for production
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                //example PI Web API call for plot data
                string uri = @"https://pisrv01.pischool.int/piwebapi/streams/A0EOfPAkD5FdUOZvMMwGLQ7fAieBh7H_k5RGAxwANOjH2Zg7HNRPBjbLVcYCJZMO5tH4AUElTUlYwMVxEQVRVUkFcQU5URVJPU3w1TUlOUFJPRFVDVElPTl9PVVRQVVQ/plot";
                HttpResponseMessage response = await client.GetAsync(uri);

                string content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var responseMessage = "Response status code does not indicate success: " + (int)response.StatusCode + " (" + response.StatusCode + " ). ";
                    throw new HttpRequestException(responseMessage + Environment.NewLine + content);
                }

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();
                foreach (var item in data)
                {
                    if (item["Good"].Value<bool>())
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp", item["Timestamp"].Value<string>());
                        dataPair.Add("Value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

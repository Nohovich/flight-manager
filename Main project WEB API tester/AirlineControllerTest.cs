using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Main_Project.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Main_project_WEB_API_tester
{
    [TestClass]
    public class AirlineControllerTest
    {
        private const string URL = "http://localhost:9001/api/airlinecompany/modifyairlinedetails";
        [TestMethod]
        public async Task ModifyAirlineDetailsTest()
        {
            // ... Use HttpClient. 
            using (HttpClient client = new HttpClient())
            {
                AirlineCompany airline = new AirlineCompany()
                {
                    AirlineName = "Concepcion",
                    CountryCode = 10620,
                    ID = 277,
                    User_Repository_ID = 11391
                };

                var byteArray = Encoding.ASCII.GetBytes("lazygoose645:737373");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var json = JsonConvert.SerializeObject(airline);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PutAsync(URL, data))
                {
                    HttpContent content = response.Content;

                    // ... Check Status Code 
                    if ((int)response.StatusCode != 200 )
                    {
                        Assert.Fail();
                    }

                }
            }
        }
    }
}

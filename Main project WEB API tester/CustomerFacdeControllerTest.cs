using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Main_Project.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Main_project_WEB_API_tester
{
    [TestClass]
    public class CustomerFacdeControllerTest
    {
        private const string URL = "http://localhost:9001/api/customer/getallmyflights";
        [TestMethod]
        public async Task GetAllMyFlightsTest()
        {
            // ... Use HttpClient. 
            using (HttpClient client = new HttpClient())
            {

                var byteArray = Encoding.ASCII.GetBytes("crazyleopard551:uuuu");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                using (HttpResponseMessage response = await client.GetAsync(URL))
                {
                    HttpContent content = response.Content;
                    
                    // ... Read the JObject.
                    List<Flight> flights = await response.Content.ReadAsAsync<List<Flight>>();

                    // ... Check Status Code 
                    if ((int)response.StatusCode != 200 || flights.Count <= 0)
                    {
                        Assert.Fail();
                    }

                }
            }
        }
    }
}

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Main_Project.POCO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Main_project_WEB_API_tester
{
    [TestClass]
    public class AnonymousFacdeControllerTestcs
    {
        private const string URL = "http://localhost:9001/api/anonymoususer/getallairlinecompanies";
        [TestMethod]
        public async Task GetAllAirlineCompaniesTest()
        {
                 // ... Use HttpClient. 
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(URL))
                    {
                        HttpContent content = response.Content;

                        // ... Read the JObject.
                        
                       List<AirlineCompany> airlineCompanies = await response.Content.ReadAsAsync<List<AirlineCompany>>();

                        // ... Check Status Code 
                        if ((int)response.StatusCode != 200 || airlineCompanies.Count <= 0)
                        {
                            Assert.Fail();
                        }

                    }
                }   
        }
    }
}

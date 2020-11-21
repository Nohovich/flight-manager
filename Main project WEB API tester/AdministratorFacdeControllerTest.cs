using System;
using System.Collections.Generic;
using System.Net.Http;
using Main_Project.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Formatting;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Main_project_WEB_API_tester
{
    [TestClass]
    public class AdministratorFacdeControllerTest
    {
        private const string URL = "http://localhost:9001/api/administrator/getadminbyid/10284";

        [TestMethod]
        public async Task GetAdminByIdTest()
        {       // ... Use HttpClient. 
            using (HttpClient client = new HttpClient())
            {
                          
                var byteArray = Encoding.ASCII.GetBytes("ticklishwolf360:yeah");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                using (HttpResponseMessage response = await client.GetAsync(URL))
                {
                    HttpContent content = response.Content;

                    // ... Read the JObject.
                    Admin admin = await response.Content.ReadAsAsync<Admin>();

                    // ... Check Status Code 
                    if ((int)response.StatusCode != 200 || admin == null)
                    {
                        Assert.Fail();
                    }
                       
                }

            }
        }
    }
}

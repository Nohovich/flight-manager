using Main_Project.Facade;
using Main_Project.POCO;
using Main_Project_WPF.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project_WPF
{
    public static class DataGeneratorService
    {
        static FlightCentreViewModel flightCentreViewModel = new FlightCentreViewModel();
        private static Random random = new Random();

        #region Create random information on this computer
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        /// <summary>
        /// Create random information on this computer
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string GenerateRandomString(int length)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #region Create random information from the web
        /// <summary>
        /// getting random information from the web and putting it inside a string
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomInformation()
        {
            string read_str = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://randomuser.me/api");

            // read data
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                read_str = reader.ReadToEnd();
            }
            return read_str;
        }
        /// <summary>
        /// using the former string to extract the field we need 
        /// </summary>
        /// <param name="fullIformation"></param>
        /// <param name="fieldFather"></param>
        /// <param name="fieldChild"></param>
        /// <returns></returns>
        public static string getThefieldYouNeed(string fullIformation, string fieldFather, string fieldChild)
        {

            JObject data = JObject.Parse(fullIformation);
            var ja = JArray.Parse(data.GetValue("results").ToString());
            var first = JObject.Parse(ja.Children<JObject>().First().ToString());
            var location = first.GetValue(fieldFather).ToString();
            var field = JObject.Parse(location).GetValue(fieldChild).ToString();

            return field.ToString();
        }
        #endregion

        #region  Generate a Random country
        /// <summary>
        /// Generate Random country
        /// </summary>
        /// <returns></returns>
        public static Country GenerateRandomCountry()
        {
            Country randomACountry = new Country();
            randomACountry.CountryName = getThefieldYouNeed(GenerateRandomInformation(), "location", "country");
            randomACountry.ID = 1;
            return randomACountry;
        }
        #endregion

        #region  Generate a random userRepository for admin
        /// <summary>
        /// Generate random userRepository For admin
        /// </summary>
        /// <returns></returns>
        public static UserRepository GenerateRandomUserRepositoryForAdmin()
        {
            UserRepository randomUserRepository = new UserRepository();
            randomUserRepository.UserRoleID = Main_Project.RolesEnum.admin;
            randomUserRepository.ID = 1;
            randomUserRepository.UserName = getThefieldYouNeed(GenerateRandomInformation(), "login", "username");
            randomUserRepository.Password = getThefieldYouNeed(GenerateRandomInformation(), "login", "password");
            return randomUserRepository;
        }
        #endregion

        #region  Generate a random admin
        /// <summary>
        /// Generate random admin
        /// </summary>
        /// <returns></returns>
        public static Admin GenerateRandomAdmin()
        {
            Admin randomAdmin = new Admin();
            randomAdmin.FirstName = getThefieldYouNeed(GenerateRandomInformation(), "name", "first");
            randomAdmin.LastName = getThefieldYouNeed(GenerateRandomInformation(), "name", "last");
            randomAdmin.ID = 1;
            randomAdmin.UserRepositoryID = 1;
            return randomAdmin;
        }
        #endregion

        #region  Generate a Random userRepository for airlineCompany
        /// <summary>
        /// Generate Random userRepository for airlineCompany
        /// </summary>
        /// <returns></returns>
        public static UserRepository GenerateRandomUserRepositoryForAirlineCompany()
        {
            UserRepository randomUserRepository = new UserRepository();
        randomUserRepository.UserRoleID = Main_Project.RolesEnum.airline;
            randomUserRepository.ID = 1;
            randomUserRepository.UserName = getThefieldYouNeed(GenerateRandomInformation(), "login", "username");
        randomUserRepository.Password = getThefieldYouNeed(GenerateRandomInformation(), "login", "password");
            return randomUserRepository;
        }
        #endregion

        #region Generate a Random airlineCompany
        /// <summary>
        /// Generate Random airlineCompany
        /// </summary>
        /// <returns></returns>
        public static AirlineCompany GenerateRandomAirlineCompany()
        {
            AirlineCompany randomAirline = new AirlineCompany();
            randomAirline.AirlineName = getThefieldYouNeed(GenerateRandomInformation(), "name", "first");
            randomAirline.User_Repository_ID = 1;
            randomAirline.CountryCode = 1;
            return randomAirline;

        }
        #endregion

        #region Generate a Random flight
        /// <summary>
        /// Generate Random flight
        /// </summary>
        /// <returns></returns>
        public static Flight GenerateRandomFlight()
        {
            Flight randomflight = new Flight();
            randomflight.ID = 1;
            randomflight.AirlineCompanyID = 1;
            randomflight.DestinationCountryCode = 1;
            randomflight.OriginCountryCode = 1;
            randomflight.RemainingTickets = 50;
            randomflight.DepartureTime = DateTime.Now;
            randomflight.LandingTime = DateTime.Today;
            return randomflight;

        }
        #endregion

        #region Generate a Random user repository for customer
        /// <summary>
        /// Generate Random user repository for customer
        /// </summary>
        /// <returns></returns>
        public static UserRepository GenerateRandomUserRepositoryForCustomer()
        {
            UserRepository randomUserRepository = new UserRepository();
            randomUserRepository.UserRoleID = Main_Project.RolesEnum.customer;
            randomUserRepository.ID = 1;
            randomUserRepository.UserName = getThefieldYouNeed(GenerateRandomInformation(), "login", "username");
            randomUserRepository.Password = getThefieldYouNeed(GenerateRandomInformation(), "login", "password");
            return randomUserRepository;
        }
        #endregion

        #region Generate a Random customer
        /// <summary>
        /// Generate Random customer
        /// </summary>
        /// <returns></returns>
        public static Customer GenerateRandomCustomer()
        {
            Customer randomCustomer = new Customer();
            randomCustomer.FirstName = getThefieldYouNeed(GenerateRandomInformation(), "name", "first");
            randomCustomer.LastName = getThefieldYouNeed(GenerateRandomInformation(), "name", "last");
            randomCustomer.phoneNumber = getThefieldYouNeed(GenerateRandomInformation(), "location", "postcode");
            randomCustomer.CreditCard = getThefieldYouNeed(GenerateRandomInformation(), "location", "postcode");
            randomCustomer.Address = getThefieldYouNeed(GenerateRandomInformation(), "location", "country");
            randomCustomer.User_Repository_ID = 1;
            return randomCustomer;
        }
        #endregion
    }
}

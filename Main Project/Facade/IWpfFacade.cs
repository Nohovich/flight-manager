using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Facade
{
    public interface IWpfFacade
    {
        void CreateNewAdmin(UserRepository userRepository, Admin admin);
        void CreateNewCountry(Country country);
        IList<Country> GetAllCountries();
        void CreateNewAirline(UserRepository userRepository, AirlineCompany airlineCompany, Country country);
        IList<AirlineCompany> GetAllAirlineCompanies();
        void CreateFlight(AirlineCompany airlineCompany, Flight flight);
        IList<Flight> GetAllFlightsOfAnAirlineCompany(AirlineCompany airlineCompany);
        IList<Flight> GetAllFlights();
        IList<Customer> GetAllCustomers();
        void CreateCustomerAndUserRepository(UserRepository userRepository, Customer customer);
        void PurchaseTicket(Customer customer, Flight flight);
    }
}

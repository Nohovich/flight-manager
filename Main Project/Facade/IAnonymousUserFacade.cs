using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Facade
{
    interface IAnonymousUserFacade
    {
        IList<FlightRazor> GetFlightsByOriginCountryAndDestination(long origin, long destination);
        IList<FlightRazor> FilterFlights(string filter, string flightType, string value);
        IList<FlightRazor> RazorAllFlights();
        IList<FlightRazor> DeparturesFlights();
        IList<FlightRazor> LandingFlights();
        IList<Flight> GetAllFlights();
        IList<AirlineCompany> GetAllAirlineCompanies();
        Dictionary<Flight, int> GetAllFlightsVacancy();
        Flight GetFlightById(long id);
        IList<Flight> GetFlightsByOriginCountry(long countryCode);
        IList<Flight> GetFlightsByDestinationCountry(long countryCode);
        IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        void CreateCustomerAndUserRepository(UserRepository userRepository, Customer customer);
    }
}

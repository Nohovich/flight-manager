using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.IDAO
{
    public interface IFlightDAO : IBasicDB<Flight>
    {
        IList<FlightRazor> GetFlightsByOriginCountryAndDestination(long origin, long destination);
        IList<FlightRazor> RazorAllFlights();
        IList<FlightRazor> LandingFlights();
        IList<FlightRazor> DeparturesFlights();
        IList<Flight> GetAllFlightsIdOfAnAirlineCompany(long airlineCompanyID);
        IList<Flight> GetAllFlightsOfAnAirlineCompany(AirlineCompany airlineCompany);
        void UpdateFlightsHistoryThatHaveLanded();
        Dictionary<Flight, int> GetAllFlightsVacancy();
        IList<Flight> GetFlightsByOriginCountry(long countryCode);
        IList<Flight> GetFlightsByDestinationCountry(long countryCode);
        IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        IList<Flight> GetFlightsByCustomer(Customer customer);
        void RemoveFlightsByAirlineCompanyID(long ID);
        void AddAndUpdateFlightTicket(long ID);
        void ReductionFlightTicket(long ID);

    }
}

using Main_Project.DAO;
using Main_Project.IDAO;
using Main_Project.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Facade
{
    public abstract class FacadeBase
    {
        protected IAirlineDAO _airlineDAO;
        protected ICountryDAO _countryDAO;
        protected ICustomerDAO _customerDAO;
        protected IFlightDAO _flightDAO;
        protected ITicketDAO _ticketDAO;
        protected IUserRoleDAO _userRoleDAO;
        protected IUserRepositoryDAO _userRepositoryDAO;
        protected IAdminDAO _adminDAO;

        public FacadeBase()
        {
            _airlineDAO = new AirlineDAO();
            _countryDAO = new CountryDAO();
            _customerDAO = new CustomerDAO();
            _flightDAO = new FlightDAO();
            _ticketDAO = new TicketDAO();
            _userRoleDAO = new UserRoleDAO();
            _userRepositoryDAO = new UserRepositoryDAO();
            _adminDAO = new AdminDAO();
        }

    }
}

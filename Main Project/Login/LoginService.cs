using Main_Project.DAO;
using Main_Project.Exceptions;
using Main_Project.Facade;
using Main_Project.IDAO;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Login
{
    public class LoginService : ILoginService
    {

        private UserRepositoryDAO _userRepositoryDAO = new UserRepositoryDAO();

        /// <summary>
        /// Login for the flight center system
        /// For anonymous facade username should be null
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="password">password</param>
        /// <param name="login">login token</param>
        /// <param name="facadeBase">proxy facade</param>
        public void TryLogin(string userName, string password, out ILogin login, out FacadeBase facadeBase)
        {

            login = null;

            if (userName == null)
            {
                facadeBase = new AnonymousUserFacade();
                return;
            }

            facadeBase = null;

            UserRepository userRepository = _userRepositoryDAO.GetUserByUserName(userName);

            if (userRepository == null)
                throw new DataNotFoundException($"User {userName} not found. Login failed");

            if (userRepository.UserName == userName && userRepository.Password == password)
            {
                switch (userRepository.UserRoleID)
                {
                    case RolesEnum.admin:
                        Admin admin = new AdminDAO().GetAdminByUserRepositoryID((int)userRepository.ID);
                        login = new LoginToken<Admin>()
                        {
                            TokenUserRepository = userRepository,
                            User = admin
                        };
                        facadeBase = new LoggedInAdministratorFacade();
                        break;

                    case RolesEnum.airline:
                        AirlineCompany airlineCompany = new AirlineDAO().GetAirlineCompanyByUserRepositoryID((int)userRepository.ID);
                        login = new LoginToken<AirlineCompany>()
                        {
                            TokenUserRepository = userRepository,
                            User = airlineCompany
                        };
                        facadeBase = new LoggedInAirlineFacade();
                        break;
                    case RolesEnum.customer:
                        Customer customer = new CustomerDAO().GetCustomerByUserRepositoryID((int)userRepository.ID);
                        login = new LoginToken<Customer>()
                        {
                            TokenUserRepository = userRepository,
                            User = customer
                        };
                        facadeBase = new LoggedInCustomerFacade();
                        break;
                    default:
                        throw new FlightSystemUnexpectedError($"User id {userRepository.ID} in roll {userRepository.UserRoleID} does not exist");

                }
            }
            else
            {
                throw new WrongPasswordException($"User {userName} entered wrong password.Login failed");
            }

        }
    }
}



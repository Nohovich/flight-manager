using Main_Project.Facade;
using Main_Project.Login;
using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Main_Project
{
    public class FlyingCenterSystem
    {
        
        
        public static UserRepository ur = new UserRepository("mainAdmin", "9999", RolesEnum.admin);
        public static LoginToken<Admin> mainAdmin = new LoginToken<Admin> { TokenUserRepository = ur, User = new Admin("David","Nohovich",ur.ID) };
        public static LoggedInAdministratorFacade centralFacade = new LoggedInAdministratorFacade();
        LoginService loginService = new LoginService();
        private static FlyingCenterSystem _instance;
        private static object key = new object();
        private FlyingCenterSystem()
        {
            Task updateFlightListTask = new Task(() =>
            {
                while (true)
                {
                    Thread.Sleep(FlyingCenterConfig._24hours);
                    centralFacade.UpdateHistoryInfo();
                }

            });
        }
        public static FlyingCenterSystem GetInstance()
        {
            if (_instance == null)
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new FlyingCenterSystem();
                    }
                }
            }
            return _instance;
        }

        public void TryLogin(string userName, string password, out ILogin login, out FacadeBase facadeBase)
        {
            loginService.TryLogin(userName, password, out login, out facadeBase);
        }
    }
}

using Main_Project.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Login
{
    public interface ILoginService
    {
        void TryLogin(string userName, string password, out ILogin login, out FacadeBase facadeBase );
    }
}

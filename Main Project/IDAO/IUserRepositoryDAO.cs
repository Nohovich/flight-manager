using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.IDAO
{
    public interface IUserRepositoryDAO : IBasicDB<UserRepository>
    {
        UserRepository GetUserByUserName(String userName);
        void CreateAdminIndUserRepository(UserRepository t);
        void CreateAirlineIndUserRepository(UserRepository t);
        void CreateCustomerInUserRepository(UserRepository t);
    }
}

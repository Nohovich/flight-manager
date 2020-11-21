using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.Login
{

    public class LoginToken<T> : ILogin where T : IUser
    {
        public UserRepository TokenUserRepository { get; set; } 
        public T User { get; set; }
    }
}

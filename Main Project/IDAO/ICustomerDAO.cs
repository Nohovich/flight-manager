using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.IDAO
{
    public interface ICustomerDAO : IBasicDB<Customer>
    {
        Customer GetCustomerByUserame(string name);
        Customer GetCustomerByUserRepositoryID(long ID);
    }
}

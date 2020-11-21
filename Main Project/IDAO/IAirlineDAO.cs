using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.IDAO
{
    public interface IAirlineDAO : IBasicDB<AirlineCompany>
    {
        AirlineCompany GetAirlineByUserame(string name);
        IList<AirlineCompany> GetAllAirlinesByCountry(long countryId);
        AirlineCompany GetAirlineCompanyByUserRepositoryID(long ID);
    }
}

using Main_Project.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project.IDAO
{
    public interface ITicketDAO : IBasicDB<Ticket>
    {
        //virtual void Add(Ticket ticket, Flight flight);
        void UpdateTicketsHistoryThatHaveLanded();
        void RemoveTicketsByAirlineCompanyID(long ID);
        void RemoveTicketsByCustomerID(long ID);
        void RemoveTicketsByFlightID(long ID);
        IList<Ticket> GetAllTicketsOfACusomer(long ID);
        IList<Ticket> GetAllTicketsOfAirlineCompany(long ID);

        void AddTicket(Ticket ticket, Flight f);
    }
}

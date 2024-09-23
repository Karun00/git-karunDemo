using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
   public interface IRosterRepository
    {
        List<Roster> GetRosterList();
        List<Shift> GetshiftList(string portCode);
        List<Months> GetMonthDetails();
        List<Months> GetYearDetails(); 

    }
}

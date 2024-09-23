using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;


namespace IPMS.Repository
{
    public class RosterRepository : IRosterRepository
    {
        private IUnitOfWork _unitOfWork;
        public RosterRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Roster> GetRosterList()
        {
            var Roster = (from r in _unitOfWork.Repository<Roster>().Query().Include(r => r.RosterGroups).Select()
                          where r.RecordStatus == "A"
                          select r).ToList();

            return Roster;

        }

        public List<Shift> GetshiftList(string portCode)
        {
            var shift = (from r in _unitOfWork.Repository<Shift>().Queryable()
                         where r.RecordStatus == "A" && r.PortCode==portCode
                         select r).ToList();

            return shift;

        }

        public List<Months> GetMonthDetails()
        {
            var shifts = _unitOfWork.SqlQuery<Months>("usp_GetMonthDetails").ToList();
            return shifts;
            
        }

        public List<Months> GetYearDetails()
        {
            var Years = _unitOfWork.SqlQuery<Months>("usp_GetYears").ToList();
            return Years;
            
        }
        
    }
        
}

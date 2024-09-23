using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using System.Collections.Generic;
using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.Models;
using System.Linq;

namespace IPMS.Repository
{
    public class ShiftRepository : IShiftRepository
    {
        protected IUnitOfWork _unitOfWork;

        public ShiftRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ShiftVO> GetShiftsByPortCode(string portCode)
        {
            var shifts = (from s in _unitOfWork.Repository<Shift>().Queryable()

                          where s.PortCode == portCode
                          select s).ToList();

            return shifts.MapToDTO();
        }

        public List<ShiftVO> GetActiveShiftsByPortCode(string portCode)
        {
            var shifts = (from s in _unitOfWork.Repository<Shift>().Query().Select()

                          where s.PortCode == portCode && s.IsShiftOff == "N" && s.IsContinuousShift == "N"
                          select s).ToList();

            return shifts.MapToDTO();
        }
    }
}

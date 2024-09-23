using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public class DepartmentRepository : IPMS.Repository.IDepartmentRepository
    {
        private IUnitOfWork _unitOfWork;

        public DepartmentRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<DepartmentVO> GetDepartments()
        {
            var Department = (from deptvo in _unitOfWork.Repository<Department>().Queryable()
                         select new DepartmentVO
                         {                         
                             DepartmentID = deptvo.DepartmentID,
                             DepartmentName = deptvo.DepartmentName,
                             DepartmentDescription = deptvo.DepartmentDescription,
                             RecordStatus = deptvo.RecordStatus,
                             CreatedBy = deptvo.CreatedBy,
                             CreatedDate = deptvo.CreatedDate,
                             ModifiedBy = deptvo.ModifiedBy,
                             ModifiedDate = deptvo.ModifiedDate

                         });

            return Department.ToList();
        }
    }
}

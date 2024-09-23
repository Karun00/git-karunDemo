using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using Core.Repository.Providers.EntityFramework;
using IPMS.Domain.ValueObjects;
using System.Data.Entity;
using IPMS.Domain.DTOS;


namespace IPMS.Repository
{
    public class PilotRepository : IPMS.Repository.IPilotRepository
    {
        private IUnitOfWork _unitOfWork;
        public PilotRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<PioltVO> GetListofPilots(string portCode)
        {
            var pilots = (from pilot in _unitOfWork.Repository<Pilot>().Queryable().Include(t => t.ResidentialAddress).Include(t => t.PostalAddress)
                          where pilot.PortCode == portCode
                          select new PioltVO
                           {
                               PilotID = pilot.PilotID,
                               PortCode = pilot.PortCode,
                               FirstName = pilot.FirstName,
                               Surname = pilot.Surname,
                               //ResidentialAddress = pilot.ResidentialAddress.MapToDTO,
                               //PostalAddress = pilot.PostalAddress.MapToDTO,
                               LastName = pilot.LastName,
                               DateofBirth = pilot.DateofBirth.ToString(),
                               IDNo = pilot.IDNo,
                               RecordStatus = pilot.RecordStatus,
                               CreatedBy = pilot.CreatedBy,
                               CreatedDate = pilot.CreatedDate,
                               ModifiedBy = pilot.ModifiedBy,
                               ModifiedDate = pilot.ModifiedDate
                           });
            return pilots.ToList<PioltVO>();
        }

        public List<PioltVO> GetApprovedPilotsList(string portCode)
        {
            /*
           var pilots = (from pilot in _unitOfWork.Repository<Pilot>().Query().Include(t => t.ResidentialAddress).Include(t => t.PostalAddress).Select()
                         where pilot.PortCode == portCode
                         select new PioltVO
                         {
                             PilotID = pilot.PilotID,
                             PortCode = pilot.PortCode,
                             FirstName = pilot.FirstName,
                             Surname = pilot.Surname,
                             LastName = pilot.LastName,
                             DateofBirth = pilot.DateofBirth,
                             IDNo = pilot.IDNo,
                             RecordStatus = pilot.RecordStatus,
                             CreatedBy = pilot.CreatedBy,
                             CreatedDate = pilot.CreatedDate,
                             ModifiedBy = pilot.ModifiedBy,
                             ModifiedDate = pilot.ModifiedDate
                         });
           return pilots.ToList<PioltVO>();
           */


            var pilots = (from p in _unitOfWork.Repository<Pilot>().Query().Select()
                          join w in _unitOfWork.Repository<WorkflowInstance>().Query().Select() on p.WorkflowInstanceId equals w.WorkflowInstanceId
                          where w.WorkflowTaskCode == "WFSA" && p.RecordStatus == "A"
                          orderby p.PilotID descending
                          select new PioltVO
                          {
                              PilotID = p.PilotID,
                              PortCode = p.PortCode,
                              FirstName = p.Surname +' '+ p.FirstName +' '+p.LastName,
                              Surname = p.Surname,
                              LastName = p.LastName,
                              DateofBirth = p.DateofBirth.ToString(),
                              IDNo = p.IDNo,
                              RecordStatus = p.RecordStatus,
                              CreatedBy = p.CreatedBy,
                              CreatedDate = p.CreatedDate,
                              ModifiedBy = p.ModifiedBy,
                              ModifiedDate = p.ModifiedDate

                          });

            return pilots.ToList<PioltVO>();
           




        }


    }
}

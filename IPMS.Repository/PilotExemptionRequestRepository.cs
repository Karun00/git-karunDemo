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
    public class PilotExemptionRequestRepository : IPilotExemptionRequestRepository
    {
        private IUnitOfWork _unitOfWork;

        public PilotExemptionRequestRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GetPilotRequestDetailsByID
        public Pilot GetPilotRequestDetailsByid(int pilotreqid)
        {
            var pilotRequest = (from t in _unitOfWork.Repository<Pilot>().Query()
                                    .Include(t => t.ResidentialAddress)
                                    .Include(t => t.PostalAddress)
                                    .Include(t => t.PilotExemptionRequests)
                                    .Include(b => b.PilotExemptionRequests.Select(d => d.Vessel))
                                    .Include(t => t.PilotExemptionRequestDocuments)
                                    .Include(t => t.Port).Select()
                                where t.PilotID == pilotreqid
                                select new Pilot
                                {
                                    FullName = t.FirstName + "" + t.LastName + "" + t.Surname,
                                    FirstName = t.FirstName,
                                    LastName = t.LastName,
                                    Surname = t.Surname,
                                    EmailID = t.EmailID,
                                    CellNo = t.CellNo,
                                    IssuedDate = t.IssuedDate,
                                    IssueDate = t.IssueDate,
                                    CreatedDate = t.CreatedDate,
                                    PortCode = t.Port.PortCode,
                                    PortName = t.Port.PortName,
                                    RenewalDate = t.RenewalDate,
                                    RenewDate = t.RenewDate,
                                    IDNo = t.IDNo,
                                    IssuedApprovedDate = t.IssuedApprovedDate,
                                    PilotID = t.PilotID
                                }).FirstOrDefault<Pilot>();
            return pilotRequest;
        }
        #endregion

        #region GetPilotRequestDetailsforFormByID
        public Pilot GetPilotRequestDetailsforFormByid(int PilotReqId)
        {
            var pilotRequest = (from t in _unitOfWork.Repository<Pilot>().Query()
                                    .Include(t => t.ResidentialAddress)
                                    .Include(t => t.PostalAddress)
                                    .Include(t => t.PilotExemptionRequests)
                                    .Include(b => b.PilotExemptionRequests.Select(d => d.Vessel))
                                    .Include(t => t.PilotExemptionRequestDocuments).Select()
                                where t.PilotID == PilotReqId
                                select t).FirstOrDefault<Pilot>();
            return pilotRequest;
        }
        #endregion

        #region GetPilotExemptionRequestlist
        public List<Pilot> GetPilotExemptionRequestlist(string portcode)
        {
            var query = (from p in _unitOfWork.Repository<Pilot>().Query()
                              .Include(p => p.ResidentialAddress)
                              .Include(p => p.PostalAddress)
                              .Include(p => p.PilotExemptionRequests)
                              .Include(b => b.PilotExemptionRequests.Select(d => d.Vessel))
                              .Include(p => p.PilotExemptionRequestDocuments)
                              .Include(w => w.WorkflowInstance.SubCategory)
                              .Select()
                       where p.RecordStatus == "A" && p.WorkflowInstance.WorkflowTaskCode == "WFSA" && p.PortCode == portcode
                      //   where p.RecordStatus == "A" && p.PortCode == portcode
                         orderby p.PilotID descending
                         select new
                         {
                             pilot = p,
                             ResidentialAddress = p.ResidentialAddress,
                             PostalAddress = p.PostalAddress,

                             PilotExemptionRequests = p.PilotExemptionRequests.Where(r => r.RecordStatus == "A"),
                             PilotExemptionRequestDocuments = p.PilotExemptionRequestDocuments.Where(q => q.RecordStatus == "A")
                         }
                       );

            var pilots = query.ToArray().Select(o => o.pilot).ToList<Pilot>();
            return pilots;
        }
        #endregion
    }
}



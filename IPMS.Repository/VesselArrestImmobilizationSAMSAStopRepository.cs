using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Globalization;

namespace IPMS.Repository
{
    public class VesselArrestImmobilizationSAMSAStopRepository : IVesselArrestImmobilizationSAMSAStopRepository
    {
        private IUnitOfWork _unitOfWork;
        //  private IUserRepository _userRepository;

        public VesselArrestImmobilizationSAMSAStopRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //  _userRepository = new UserRepository(_unitOfWork);
        }

        #region GetVesselArrestImmobilizationSAMSAStopbyID
        /// <summary>
        /// Method to Get VesselArrestImmobilizationSAMSAStopbyID
        /// </summary>
        /// <param name="vasId"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public VesselArrestImmobilizationSAMSAStopVO GetVesselArrestImmobilizationSamsaStopById(int vasId, string portCode)
        {
            var servicedtls = (from t in _unitOfWork.Repository<VesselArrestImmobilizationSAMSA>().Query()
                               .Include(t => t.ArrivalNotification)
                               .Include(t => t.ArrivalNotification.Vessel)
                               .Include(t => t.ArrivalNotification.Agent)
                               .Include(t => t.VesselArrestDocuments.Select(p => p.Document))
                               .Include(t => t.VesselSAMSAStopDocuments.Select(p => p.Document)).Select()
                               where t.ArrivalNotification.PortCode == portCode
                               select t
                        ).Where(s => s.VAISID == vasId).FirstOrDefault();

            return servicedtls.MapToDTO();
        }
        #endregion

        #region GetVesselArrestImmobilizationSAMSAStopList
        /// <summary>
        /// Method to Get VesselArrest ImmobilizationSAMSAStop List
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<VesselArrestImmobilizationSAMSAStopVO> GetVesselArrestImmobilizationSamsaStopList(string portCode)
        {
            var servicedtls = (from t in _unitOfWork.Repository<VesselArrestImmobilizationSAMSA>().Queryable()
                               .Include(t => t.ArrivalNotification)
                               .Include(t => t.ArrivalNotification.Vessel)
                               .Include(t => t.ArrivalNotification.Agent)
                               .Include(t => t.VesselArrestDocuments.Select(p => p.Document))
                               .Include(t => t.VesselSAMSAStopDocuments.Select(p => p.Document))
                               where t.ArrivalNotification.PortCode == portCode && t.ArrivalNotification.RecordStatus == RecordStatus.Active
                               select t

                        ).ToList();

            return servicedtls.MapToDTO();
        }
        #endregion

        #region AddVesselArrestImmobilizationSAMSAStop
        /// <summary>
        /// Add the VesselArrestImmobilizationSAMSAStop data
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="_LoginName"></param>
        /// <returns></returns>
        public int AddVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO entity, int userId)
        {
            
            VesselArrestImmobilizationSAMSA VesselArrestImmobilizationSAMSAStop = new VesselArrestImmobilizationSAMSA();

            VesselArrestImmobilizationSAMSAStop = VesselArrestImmobilizationSAMSAStopMapExtension.MapToEntity(entity);

            VesselArrestImmobilizationSAMSAStop.CreatedBy = userId;
            VesselArrestImmobilizationSAMSAStop.CreatedDate = DateTime.Now;
            VesselArrestImmobilizationSAMSAStop.ModifiedBy = userId;
            VesselArrestImmobilizationSAMSAStop.ModifiedDate = DateTime.Now;

            List<VesselArrestDocument> VesselArrestDocumentlist = VesselArrestImmobilizationSAMSAStop.VesselArrestDocuments.ToList();
            if (entity != null)
            {
                foreach (var VesselArrestDocuments in VesselArrestDocumentlist)
                {
                    VesselArrestDocuments.RecordStatus = entity.RecordStatus;
                    VesselArrestDocuments.CreatedDate = VesselArrestImmobilizationSAMSAStop.CreatedDate;
                    VesselArrestDocuments.CreatedBy = VesselArrestImmobilizationSAMSAStop.CreatedBy;
                    VesselArrestDocuments.ModifiedBy = VesselArrestImmobilizationSAMSAStop.ModifiedBy;
                    VesselArrestDocuments.ModifiedDate = VesselArrestImmobilizationSAMSAStop.ModifiedDate;
                    VesselArrestDocuments.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<VesselArrestDocument>().Insert(VesselArrestDocuments);
                }
            }

            List<VesselSAMSAStopDocument> VesselSAMSAStopDocumentlist = VesselArrestImmobilizationSAMSAStop.VesselSAMSAStopDocuments.ToList();
            if (entity != null)
            {
                foreach (var VesselSAMSAStopDocuments in VesselSAMSAStopDocumentlist)
                {
                    VesselSAMSAStopDocuments.RecordStatus = entity.RecordStatus;
                    VesselSAMSAStopDocuments.CreatedBy = VesselArrestImmobilizationSAMSAStop.CreatedBy;
                    VesselSAMSAStopDocuments.CreatedDate = VesselArrestImmobilizationSAMSAStop.CreatedDate;
                    VesselSAMSAStopDocuments.ModifiedBy = VesselArrestImmobilizationSAMSAStop.ModifiedBy;
                    VesselSAMSAStopDocuments.ModifiedDate = VesselArrestImmobilizationSAMSAStop.ModifiedDate;
                    VesselSAMSAStopDocuments.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<VesselSAMSAStopDocument>().Insert(VesselSAMSAStopDocuments);
                }
            }

            string strvcn = VesselArrestImmobilizationSAMSAStop.VCN;
            string vcnValue = "";
            if (strvcn.Contains("_"))
            {
                string[] words = strvcn.Split('_');
                vcnValue = words[0];
            }
            else
            {
                vcnValue = strvcn;
            }
            VesselArrestImmobilizationSAMSAStop.VCN = vcnValue;
            _unitOfWork.Repository<VesselArrestImmobilizationSAMSA>().Insert(VesselArrestImmobilizationSAMSAStop);
            _unitOfWork.SaveChanges();

            return VesselArrestImmobilizationSAMSAStop.VAISID;
        }
        #endregion

        #region ModifyVesselArrestImmobilizationSAMSAStop
        /// <summary>
        /// Modify the VesselArrestImmobilizationSAMSAStop Data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void ModifyVesselArrestImmobilizationSamsaStop(VesselArrestImmobilizationSAMSAStopVO entity, int userId)
        {
            VesselArrestImmobilizationSAMSA VesselArrestImmobilizationSAMSAStop = new VesselArrestImmobilizationSAMSA();
            VesselArrestImmobilizationSAMSAStop = VesselArrestImmobilizationSAMSAStopMapExtension.MapToEntity(entity);

            VesselArrestImmobilizationSAMSAStop.ModifiedBy = userId;
            VesselArrestImmobilizationSAMSAStop.ModifiedDate = DateTime.Now;

            List<VesselArrestDocument> listofVesselArrestDocument = _unitOfWork.Repository<VesselArrestDocument>().Queryable()
                .Where(e => e.VAISID == VesselArrestImmobilizationSAMSAStop.VAISID).ToList();

            foreach (VesselArrestDocument vesselarrestdocument in listofVesselArrestDocument)
            {
                _unitOfWork.Repository<VesselArrestDocument>().Delete(vesselarrestdocument);
                _unitOfWork.SaveChanges();
            }

            List<VesselArrestDocument> VesselArrestDocumentlist = VesselArrestImmobilizationSAMSAStop.VesselArrestDocuments.ToList();
            if (entity != null)
            {
                foreach (var VesselArrestDocuments in VesselArrestDocumentlist)
                {
                    VesselArrestDocuments.VAISID = entity.VAISID;
                    VesselArrestDocuments.RecordStatus = entity.RecordStatus;
                    VesselArrestDocuments.CreatedDate = VesselArrestImmobilizationSAMSAStop.CreatedDate;
                    VesselArrestDocuments.CreatedBy = VesselArrestImmobilizationSAMSAStop.CreatedBy;
                    VesselArrestDocuments.ModifiedBy = VesselArrestImmobilizationSAMSAStop.ModifiedBy;
                    VesselArrestDocuments.ModifiedDate = VesselArrestImmobilizationSAMSAStop.ModifiedDate;
                    VesselArrestDocuments.ObjectState = ObjectState.Added;

                    _unitOfWork.Repository<VesselArrestDocument>().Insert(VesselArrestDocuments);
                }
            }

            List<VesselSAMSAStopDocument> listofVesselSAMSAStopDocument = _unitOfWork.Repository<VesselSAMSAStopDocument>().Queryable()
                .Where(e => e.VAISID == VesselArrestImmobilizationSAMSAStop.VAISID).ToList();

            foreach (VesselSAMSAStopDocument vesselSAMSAStopdocument in listofVesselSAMSAStopDocument)
            {
                _unitOfWork.Repository<VesselSAMSAStopDocument>().Delete(vesselSAMSAStopdocument);
                _unitOfWork.SaveChanges();
            }

            List<VesselSAMSAStopDocument> VesselSAMSAStopDocumentlist = VesselArrestImmobilizationSAMSAStop.VesselSAMSAStopDocuments.ToList();
            if (entity != null)
            {
                foreach (var VesselSAMSAStopDocuments in VesselSAMSAStopDocumentlist)
                {
                    VesselSAMSAStopDocuments.VAISID = entity.VAISID;
                    VesselSAMSAStopDocuments.RecordStatus = entity.RecordStatus;
                    VesselSAMSAStopDocuments.CreatedBy = VesselArrestImmobilizationSAMSAStop.CreatedBy;
                    VesselSAMSAStopDocuments.CreatedDate = VesselArrestImmobilizationSAMSAStop.CreatedDate;
                    VesselSAMSAStopDocuments.ModifiedBy = VesselArrestImmobilizationSAMSAStop.ModifiedBy;
                    VesselSAMSAStopDocuments.ModifiedDate = VesselArrestImmobilizationSAMSAStop.ModifiedDate;
                    VesselSAMSAStopDocuments.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<VesselSAMSAStopDocument>().Insert(VesselSAMSAStopDocuments);
                }
            }

            _unitOfWork.Repository<VesselArrestImmobilizationSAMSA>().Update(VesselArrestImmobilizationSAMSAStop);
            _unitOfWork.SaveChanges();
        }
        #endregion

        #region GetVcnDetails
        /// <summary>
        /// Method to Get VcnDetails
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<ArrivalNotificationVO> GetVcnDetails(string portCode)
        {
            var Vcndtls = (from an in _unitOfWork.Repository<ArrivalNotification>().Queryable()
                           .Include(t => t.WorkflowInstance)
                           .Include(t => t.Vessel)
                           .Include(t => t.VesselCalls)
                           where an.PortCode == portCode && an.RecordStatus == RecordStatus.Active && an.WorkflowInstanceId != null
                           && an.WorkflowInstance.WorkflowTaskCode == WFStatus.Approved && an.VesselCalls.Count > 0 && an.VesselCalls.FirstOrDefault().ATD == null
                           select an).ToList();
            return Vcndtls.MapToDto();
        }
        #endregion

        #region GetVesselArrestImmobilizationSAMSAStopbyID
        /// <summary>
        /// Method to Get VesselArrestImmobilizationSAMSAStopbyID
        /// </summary>
        /// <param name="vasId"></param>
        /// <param name="_PortCode"></param>
        /// <returns></returns>
        public VesselArrestImmobilizationSAMSAStopVO GetVesselArrestImmobilizationSamsaStopById(string vasId)
        {
            var servicedtls = (from t in _unitOfWork.Repository<VesselArrestImmobilizationSAMSA>().Query().Tracking(true)
                               .Include(t => t.ArrivalNotification)
                               .Include(t => t.ArrivalNotification.Vessel)
                               .Include(t => t.ArrivalNotification.Agent)

                               .Include(t => t.ArrivalNotification.Port)
                               .Include(t => t.VesselArrestDocuments.Select(p => p.Document))
                               .Include(t => t.VesselSAMSAStopDocuments.Select(p => p.Document)).Select()
                               where t.VAISID == int.Parse(vasId, CultureInfo.InvariantCulture)
                               select new VesselArrestImmobilizationSAMSAStopVO
                                      {
                                          VCN = t.ArrivalNotification.VCN,
                                          VesselName = t.ArrivalNotification.Vessel.VesselName,
                                          AgentID = t.ArrivalNotification.Agent.AgentID,
                                          AgentName = t.ArrivalNotification.Agent.RegisteredName,
                                          AgentContactNo = t.ArrivalNotification.Agent.TelephoneNo1,
                                          PortCode = t.ArrivalNotification.PortCode,
                                          PortName = t.ArrivalNotification.Port.PortName,
                                          PortofRegistry = t.ArrivalNotification.Vessel.PortOfRegistry,
                                          MaintenanceTypeCode = "",
                                          FromBollard = "",
                                          ToBollard = "",
                                          PeriodFrom = "",
                                          PeriodTo = "",
                                          VesselArrested = (t.VesselArrested == "Y" ? "Yes" : "No"),
                                          ArrestedDate = t.ArrestedDate.ToString(),
                                          ArrestedRemarks = t.ArrestedRemarks,
                                          VesselReleased = (t.VesselReleased == "Y" ? "Yes" : "No"),
                                          ReleasedDate = t.ReleasedDate.ToString(),
                                          ReleasedRemarks = t.ReleasedRemarks,
                                          Immobilization = (t.Immobilization == "Y" ? "Yes" : "No"),
                                          ImmobilizationStartDate = t.ImmobilizationStartDate.ToString(),
                                          ImmobilizationEndDate = t.ImmobilizationEndDate.ToString(),
                                          SAMSACleared = (t.SAMSACleared == "Y" ? "Yes" : "No"),
                                          SAMSAClearedDate = t.SAMSAClearedDate.ToString(),
                                          SAMSAClearedRemarks = t.SAMSAClearedRemarks,
                                          SAMSAStop = (t.SAMSAStop == "Y" ? "Yes" : "No"),
                                          SAMSAStopDate = t.SAMSAStopDate.ToString(),
                                          SAMSAStopRemarks = t.SAMSAStopRemarks,
                                          ModifiedBy = t.ModifiedBy,
                                          CreatedBy = t.CreatedBy
                                      }).First();

            return servicedtls;
        }
        #endregion
    }
}

using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class VesselSAPPostingService : ServiceBase, IVesselSAPPostingService
    {
        private IVesselSAPPostingRepository _sappostingRepository;

        public VesselSAPPostingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sappostingRepository = new VesselSAPPostingRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }
        public VesselSAPPostingService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _sappostingRepository = new VesselSAPPostingRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);

        }

        /// <summary>
        ///  To Get SAP Posting Details by VCN
        /// </summary>
        /// <returns></returns>
        public List<VesselSAPPostingVO> GetVesselsList(string SearchColumn, string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _sappostingRepository.GetVesselsList(SearchColumn, searchValue, _PortCode);
            });
        }

        /// <summary>
        /// To Post Vessel SAP Details
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public VesselSAPPostingVO PostVesselSAP(VesselSAPPostingVO vesselsapposting)
        {
            return ExecuteFaultHandledOperation(() =>
            {
           
                SAPPosting sapdata = new SAPPosting();
                sapdata.MessageType = SAPMessageTypes.VesselCreate;
                //sapdata.NotificationTemplateCode = "VSCR"; not updated

                sapdata.ReferenceNo = vesselsapposting.VesselID.ToString();
                sapdata.PostingStatus = SAPPostingStatus.New;
                sapdata.TransmitData = vesselsapposting.TransmitData;
                sapdata.Remarks = "";
                sapdata.RecordStatus = "A";
                sapdata.EmailStatus = "O";
                sapdata.SMSStatus = "O";
                sapdata.SystemNotificationStatus = "O";
                sapdata.PortCode = _PortCode;
                sapdata.CreatedBy = _UserId;
                sapdata.CreatedDate = DateTime.Now;
                sapdata.ModifiedBy = _UserId;
                sapdata.ModifiedDate = DateTime.Now;
                sapdata.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<SAPPosting>().Insert(sapdata);
                _unitOfWork.SaveChanges();
                return vesselsapposting;
                //return _sappostingRepository.PostVesselSAP(value, _PortCode);
            });
        }

        public List<SAPPostingVO> GetSAPVesselPostGrid()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _sappostingRepository.GetSAPVesselPostGrid( _PortCode);
            });
        }
    }
}

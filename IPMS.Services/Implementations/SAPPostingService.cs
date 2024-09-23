using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml.Linq;


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SAPPostingService : ServiceBase, ISAPPostingService
    {
        private ISAPPostingRepository _sappostingRepository;
        private ISubCategoryRepository _subcategoryRepository;  

        public SAPPostingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sappostingRepository = new SAPPostingRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }
        public SAPPostingService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _sappostingRepository = new SAPPostingRepository(_unitOfWork);
            _subcategoryRepository = new SubCategoryRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);

        }

        /// <summary>
        ///  To Get SAP Posting Reference data While initialization
        /// </summary>
        /// <returns></returns>
        public SAPPostingVO GetSAPPostingReferenceVO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                SAPPostingVO VO = new SAPPostingVO();
                VO.Reasons = _subcategoryRepository.GetReasons().MapToDto();
          

                return VO;
            });
        }

        /// <summary>
        ///  To Get VCN Details
        /// </summary>
        /// <returns></returns>
        public List<SAPPostingVO> GetSAPPostingVCN(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _sappostingRepository.GetSAPPostingVCN(searchValue, _PortCode);
            });
        }

        /// <summary>
        /// To Add SAP Posting Data
        /// </summary>
        /// <param name="supcatdata"></param>
        /// <returns></returns>
        public SAPPostingVO AddSAPPosting(SAPPostingVO sapdata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                sapdata.CreatedBy = _UserId;
                sapdata.CreatedDate = DateTime.Now;
                sapdata.ModifiedBy = _UserId;
                sapdata.ModifiedDate = DateTime.Now;
                sapdata.RecordStatus = "A";
                sapdata.PostingStatus = SAPPostingStatus.New;
                sapdata.PortCode = _PortCode;
                sapdata.EmailStatus = "O";
                sapdata.SMSStatus = "O";
                sapdata.SystemNotificationStatus = "O";
                SAPPosting sapposting = new SAPPosting();
                sapposting = SAPPostingMapExtension.MapToEntity(sapdata);                 
                if (sapposting.SAPPostingID > 0)
                {
                    sapposting.Remarks = "";
                    sapposting.SAPReferenceNo = null;
                    sapposting.ObjectState = ObjectState.Modified;
                    _unitOfWork.Repository<SAPPosting>().Update(sapposting);
                }
                else
                {
                    sapposting.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<SAPPosting>().Insert(sapposting);
                }
                _unitOfWork.SaveChanges();
                return sapdata;
            });
        }  

        /// <summary>
        ///  To Get SAP Posting Details by VCN
        /// </summary>
        /// <returns></returns>
        public List<SAPPostingVO> GetSAPPostingDetailsByVCN(string VCN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _sappostingRepository.GetSAPPostingDetailsByVCN(VCN);
            });
        }

        /// <summary>
        ///  To Get SAP Posting Add Details by VCN
        /// </summary>
        /// <returns></returns>
        public string GetSAPPostingDetailsAddDetails(string VCN, string MsgType, string ReceavedARRNO, string MarineAccNo, int MarinePostingId, string PostingStatus, string isRevenueUpd, string RevAgentAccNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {               
                return _sappostingRepository.GetSAPPostingDetailsAddDetails(VCN, _PortCode, MsgType, ReceavedARRNO, MarineAccNo, MarinePostingId, PostingStatus, isRevenueUpd,RevAgentAccNo);
            });
        }


        /// <summary>
        ///  To Get SAP Posting Account Details by VCN
        /// </summary>
        /// <returns></returns>
        public List<SAPPostingVO> GetSAPPostingAccountDetails(string VCN)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _sappostingRepository.GetSAPPostingAccountDetails(VCN);
            });
        }

        /// <summary>
        ///  To Get SAP Posting Details by SAPPostingID
        /// </summary>
        /// <returns></returns>
        public SAPPostingVO GetSAPPostingDetails(int SAPPostingID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _sappostingRepository.GetSAPPostingDetails(SAPPostingID);
            });
        }

        /// <summary>
        /// To Add SAP Posting Invoice Data
        /// </summary>
        /// <param name="supcatdata"></param>
        /// <returns></returns>
        public SAPPostingVO AddSAPPostingInvoice(SAPPostingVO sapdata)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                sapdata.CreatedBy = _UserId;
                sapdata.CreatedDate = DateTime.Now;
                sapdata.ModifiedBy = _UserId;
                sapdata.ModifiedDate = DateTime.Now;
                sapdata.RecordStatus = "A";
                sapdata.PostingStatus = SAPPostingStatus.New;
                sapdata.MessageType = SAPMessageTypes.InvoiceCreate;
                sapdata.PortCode = _PortCode;
                sapdata.EmailStatus = "O";
                sapdata.SMSStatus = "O";
                sapdata.SystemNotificationStatus = "O";                
                SAPPosting sapposting = new SAPPosting();
                sapposting = SAPPostingMapExtension.MapToEntity(sapdata);
                sapposting.ObjectState = ObjectState.Added;
                sapposting.TransmitData = sapdata.SAPInvoice;
                _unitOfWork.Repository<SAPPosting>().Insert(sapposting);
                _unitOfWork.SaveChanges();
                return sapdata;
            });
        }

        /// <summary>
        ///  To Get SAP Invoice Response Details by MarineOrderNo
        /// </summary>
        /// <returns></returns>
        public SAPInvoiceItem GetSAPInvoiceResponseDetails(string MarineOrderNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _sappostingRepository.GetSAPInvoiceResponseDetails(MarineOrderNo);
            });
        }


    }
}

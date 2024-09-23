using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using IPMS.Domain;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ReportBuilderService : ServiceBase, IReportBuilderService
    {
        private IReportBuilderRepository _reportbuilderRepository;
        private IAccountRepository _accountRepository;
        private IPortGeneralConfigsRepository _portGeneralConfigRepository;

        public ReportBuilderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        public ReportBuilderService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _reportbuilderRepository = new ReportBuilderRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            _portGeneralConfigRepository = new PortGeneralConfigsRepository(_unitOfWork);
        }

        public IEnumerable<ReportBuilderVO> GetReportCategory()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var reportbuilder = _reportbuilderRepository.GetReportCategory();

                return reportbuilder;

            });

        }

        public List<ReportQueryOperatorVO> GetReportQueryOperator(string datatype)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var reportoperator = _reportbuilderRepository.GetReportQueryOperator(datatype).MapToDTO();

                return reportoperator;

            });

        }

        public IEnumerable<ReportCategoryColumnVO> GetReportCategoryColumn(int reportbilderid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var rccolumn = _reportbuilderRepository.GetReportCategoryColumn(reportbilderid);

                return rccolumn;

            });

        }

        public IEnumerable<ReportBuilderVO> GetReportBuilderGridData(ReportBuilderVO rbitem)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var rbgriddata = _reportbuilderRepository.GetReportBuilderGridData(rbitem);

                return rbgriddata;

            });

        }

        public List<ReportQueryTemplateVO> GetReportFilterData()
        {
            return EncloseTransactionAndHandleException(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                return _reportbuilderRepository.GetReportFilterData(userid).MapToDTO();
            });

        }

        public ReportQueryTemplate DeleteReportQueryTemplate(int ID)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _reportbuilderRepository.DeleteReportQueryTemplate(ID);//.MapToDTO();
            });

        }

        public List<ReportLookUpVO> GetLookUpData(string columnName, string searchValue)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _reportbuilderRepository.GetLookUpData(columnName, searchValue);
            });
        }

        public ReportQueryTemplateVO AddReportQueryTemplate(ReportQueryTemplateVO reportdata)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                int userid = _accountRepository.GetUserId(_LoginName);
                ReportQueryTemplate reportTemplate = null;
                reportTemplate = reportdata.MapToEntity();
                if (reportdata.Flag == "I")
                {
                    reportTemplate.CreatedBy = userid;
                    reportTemplate.CreatedDate = DateTime.Now;
                    reportTemplate.ModifiedBy = userid;
                    reportTemplate.ModifiedDate = DateTime.Now;
                    reportTemplate.RecordStatus = "A";

                    _unitOfWork.Repository<ReportQueryTemplate>().Insert(reportTemplate);
                }
                else
                {
                    reportTemplate.CreatedBy = userid;
                    reportTemplate.CreatedDate = DateTime.Now;
                    reportTemplate.ModifiedBy = userid;
                    reportTemplate.ModifiedDate = DateTime.Now;
                    reportTemplate.RecordStatus = "A";

                    _unitOfWork.Repository<ReportQueryTemplate>().Update(reportTemplate);
                }
                _unitOfWork.SaveChanges();



                return reportdata;

            });
        }

        public string GetDateFormatConfig(string DateFormatType, string PortCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var reportbuilder = _portGeneralConfigRepository.GetPortConfiguration(PortCode, ConfigName.DateFormat);

                return reportbuilder;
            });
        }
    }
}

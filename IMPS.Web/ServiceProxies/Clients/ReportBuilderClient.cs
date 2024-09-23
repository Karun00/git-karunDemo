using System.Collections.Generic;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.ValueObjects;
using System.Threading.Tasks;


namespace IPMS.ServiceProxies.Clients
{
    public class ReportBuilderClient : UserClientBase<IReportBuilderService>, IReportBuilderService
    {
        public IEnumerable<ReportBuilderVO> GetReportCategory()
        {
            return WrapOperationWithException(() => Channel.GetReportCategory());
        }

        //public Task<IEnumerable<ReportBuilderVO>> GetReportCategoryAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetReportCategoryAsync());
        //}


        public List<ReportQueryOperatorVO> GetReportQueryOperator(string datatype)
        {
            return WrapOperationWithException(() => Channel.GetReportQueryOperator(datatype));
        }

        //public Task<List<ReportQueryOperatorVO>> GetReportQueryOperatorAsync(string datatype)
        //{
        //    return WrapOperationWithException(() => Channel.GetReportQueryOperatorAsync(datatype));
        //}


        public IEnumerable<ReportCategoryColumnVO> GetReportCategoryColumn(int reportbilderid)
        {
            return WrapOperationWithException(() => Channel.GetReportCategoryColumn(reportbilderid));
        }

        //public Task<IEnumerable<ReportCategoryColumnVO>> GetReportCategoryColumnAsync(int reportbilderid)
        //{
        //    return WrapOperationWithException(() => Channel.GetReportCategoryColumnAsync(reportbilderid));
        //}
        public IEnumerable<ReportBuilderVO> GetReportBuilderGridData(ReportBuilderVO rbitem)
        {
            return WrapOperationWithException(() => Channel.GetReportBuilderGridData(rbitem));
        }

        //public Task<IEnumerable<ReportBuilderVO>> GetReportBuilderGridDataAsync(ReportBuilderVO rbitem)
        //{
        //    return WrapOperationWithException(() => Channel.GetReportBuilderGridDataAsync(rbitem));
        //}

        public ReportQueryTemplateVO AddReportQueryTemplate(ReportQueryTemplateVO reportdata)
        {
            return WrapOperationWithException(() => Channel.AddReportQueryTemplate(reportdata));
        }

        //public Task<ReportQueryTemplateVO> AddReportQueryTemplateAsync(ReportQueryTemplateVO reportdata)
        //{
        //    return WrapOperationWithException(() => Channel.AddReportQueryTemplateAsync(reportdata));
        //}

        public List<ReportQueryTemplateVO> GetReportFilterData()
        {
            return WrapOperationWithException(() => Channel.GetReportFilterData());
        }

        public ReportQueryTemplate DeleteReportQueryTemplate(int ID)
        {
            return WrapOperationWithException(() => Channel.DeleteReportQueryTemplate(ID));
        }

        //public Task<List<ReportQueryTemplateVO>> GetReportFilterDataAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetReportFilterDataAsync());
        //}

        public List<ReportLookUpVO> GetLookUpData(string columnName, string searchValue)
        {
            return WrapOperationWithException(() => Channel.GetLookUpData(columnName, searchValue));
        }

        //public Task<List<ReportLookUpVO>> GetLookUpDataAsync(string columnName, string searchValue)
        //{
        //    return WrapOperationWithException(() => Channel.GetLookUpDataAsync(columnName, searchValue));
        //}

        public string GetDateFormatConfig(string DateFormatType, string PortCode)
        {
            return WrapOperationWithException(() => Channel.GetDateFormatConfig(DateFormatType, PortCode));
        }
    }
}
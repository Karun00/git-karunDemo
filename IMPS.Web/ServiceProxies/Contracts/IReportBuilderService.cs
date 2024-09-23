using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System;
using IPMS.Domain.ValueObjects;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IReportBuilderService : IDisposable
    {
        [OperationContract]
        IEnumerable<ReportBuilderVO> GetReportCategory();

        //[OperationContract]
        //Task<IEnumerable<ReportBuilderVO>> GetReportCategoryAsync();

        [OperationContract]
        List<ReportQueryOperatorVO> GetReportQueryOperator(string datatype);

        //[OperationContract]
        //Task<List<ReportQueryOperatorVO>> GetReportQueryOperatorAsync(string datatype);

        [OperationContract]
        IEnumerable<ReportCategoryColumnVO> GetReportCategoryColumn(int reportbilderid);

        [OperationContract]
        ReportQueryTemplate DeleteReportQueryTemplate(int ID);

        //[OperationContract]
        //Task<IEnumerable<ReportCategoryColumnVO>> GetReportCategoryColumnAsync(int reportbilderid);

        [OperationContract]
        IEnumerable<ReportBuilderVO> GetReportBuilderGridData(ReportBuilderVO rbitem);

        //[OperationContract]
        //Task<IEnumerable<ReportBuilderVO>> GetReportBuilderGridDataAsync(ReportBuilderVO rbitem);

        [OperationContract]
        ReportQueryTemplateVO AddReportQueryTemplate(ReportQueryTemplateVO reportdata);

        //[OperationContract]
        //Task<ReportQueryTemplateVO> AddReportQueryTemplateAsync(ReportQueryTemplateVO reportdata);

        [OperationContract]
        List<ReportQueryTemplateVO> GetReportFilterData();

        //[OperationContract]
        //Task<List<ReportQueryTemplateVO>> GetReportFilterDataAsync();

        [OperationContract]
        List<ReportLookUpVO> GetLookUpData(string columnname, string searchvalue);

        //[OperationContract]
        //Task<List<ReportLookUpVO>> GetLookUpDataAsync(string columnname, string searchvalue);

        [OperationContract]
        string GetDateFormatConfig(string dateformattype, string portcode);

    }
}
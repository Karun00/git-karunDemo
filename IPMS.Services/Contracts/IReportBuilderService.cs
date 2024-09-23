using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IReportBuilderService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<ReportBuilderVO> GetReportCategory();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ReportQueryOperatorVO> GetReportQueryOperator(string datatype);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<ReportCategoryColumnVO> GetReportCategoryColumn(int reportbilderid);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<ReportBuilderVO> GetReportBuilderGridData(ReportBuilderVO rbitem);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ReportQueryTemplateVO AddReportQueryTemplate(ReportQueryTemplateVO reportdata);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ReportQueryTemplateVO> GetReportFilterData();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<ReportLookUpVO> GetLookUpData(string columnname, string searchvalue);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetDateFormatConfig(string dateformattype, string portcode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ReportQueryTemplate DeleteReportQueryTemplate(int ID);


    }
}

using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface IReportBuilderRepository
    {
        IEnumerable<ReportBuilderVO> GetReportCategory();
        List<ReportQueryOperator> GetReportQueryOperator(string datatype);
        IEnumerable<ReportCategoryColumnVO> GetReportCategoryColumn(int reportbilderid);
        IEnumerable<ReportBuilderVO> GetReportBuilderGridData(ReportBuilderVO rbitem);
        List<ReportQueryTemplate> GetReportFilterData(int userid);
        List<ReportLookUpVO> GetLookUpData(string columnName, string searchValue);
        ReportQueryTemplate DeleteReportQueryTemplate(int ID);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class DredgingPriorityMapExtension
    {
        public static List<DredgingPriorityVO> MapToDto(this List<DredgingPriority> dredgingPrioritys)
        {
            List<DredgingPriorityVO> dredgingpriorityvos = new List<DredgingPriorityVO>();
            if (dredgingPrioritys != null)
            {
                foreach (var dredgingpriority in dredgingPrioritys)
                {
                    dredgingpriorityvos.Add(dredgingpriority.MapToDto());
                }
            }
            return dredgingpriorityvos;
        }

        public static DredgingPriorityVO MapToDto(this DredgingPriority data)
        {
            DredgingPriorityVO dredgingpriorityvo = new DredgingPriorityVO();
            if (data != null)
            {
                dredgingpriorityvo.DredgingPriorityID = data.DredgingPriorityID;
                dredgingpriorityvo.FromDate = Convert.ToString(data.FromDate, CultureInfo.InvariantCulture);
                dredgingpriorityvo.ToDate = Convert.ToString(data.ToDate, CultureInfo.InvariantCulture);
                dredgingpriorityvo.RecordStatus = data.RecordStatus;
                dredgingpriorityvo.CreatedBy = data.CreatedBy;
                // dredgingpriorityvo.CreatedDate = Convert.ToString(data.CreatedDate);
                dredgingpriorityvo.CreatedDate = data.CreatedDate;
                dredgingpriorityvo.ModifiedBy = data.ModifiedBy;
                // dredgingpriorityvo.ModifiedDate = Convert.ToString(data.ModifiedDate);
                dredgingpriorityvo.ModifiedDate = data.ModifiedDate;
                dredgingpriorityvo.FinancialYearID = data.FinancialYearID;
                dredgingpriorityvo.DredgingPriorityDocumentsVO = data.DredgingPriorityDocuments.MapToDto();
                dredgingpriorityvo.DredgingPriorityDocumentsVO = (data.DredgingPriorityDocuments != null && data.DredgingPriorityDocuments.Count > 0) ? data.DredgingPriorityDocuments.MapToDto() : null;
                dredgingpriorityvo.MonthValue = Convert.ToString(data.FromDate.ToString("yyyy-MM", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                dredgingpriorityvo.Month = Convert.ToString(data.FromDate.ToString("MMMM", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                // Commented by sandeep on 24-12-2014
                //dredgingpriorityvo.DredgingOperationsVO = data.DredgingOperations.MapToDTO(); 
                //dredgingpriorityvo.BerthOccupationVO = data.BerthOccupations.ToList().MapToDTO();
                // -- end

                // changed by sandeep on 30-12-2014  
                dredgingpriorityvo.FinancialYearDate = data.FinancialYear != null ? data.FinancialYear.StartDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture) + " to " + data.FinancialYear.EndDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture) : null;
                dredgingpriorityvo.DredgingOperationsVO = (data.DredgingOperations != null && data.DredgingOperations.Count > 0) ? data.DredgingOperations.ToList().MapToDto() : null;

                // -- end

                //dredgingpriorityvo.DredgingPriorityDocumentsVO = data.DredgingPriorityDocuments.MapToDTO();
            }
            return dredgingpriorityvo;
        }

        public static DredgingPriority MapToEntity(this DredgingPriorityVO dredgingPriorityVO)
        {
            DredgingPriority data = new DredgingPriority();
            if (dredgingPriorityVO != null)
            {
                data.DredgingPriorityID = dredgingPriorityVO.DredgingPriorityID;
                data.DeploymentPlanID = dredgingPriorityVO.DeploymentPlanID;
                data.FromDate = Convert.ToDateTime(dredgingPriorityVO.FromDate, CultureInfo.InvariantCulture);
                data.ToDate = Convert.ToDateTime(dredgingPriorityVO.ToDate, CultureInfo.InvariantCulture);
                data.RecordStatus = dredgingPriorityVO.RecordStatus;
                data.CreatedBy = dredgingPriorityVO.CreatedBy;
                data.CreatedDate = dredgingPriorityVO.CreatedDate;
                data.ModifiedBy = dredgingPriorityVO.ModifiedBy;
                data.ModifiedDate = Convert.ToDateTime(dredgingPriorityVO.ModifiedDate);
                data.ModifiedDate = dredgingPriorityVO.ModifiedDate;
                data.FinancialYearID = dredgingPriorityVO.FinancialYearID;
                data.Month = dredgingPriorityVO.Month;
                data.DredgingPriorityDocuments = dredgingPriorityVO.DredgingPriorityDocumentsVO.MapToEntity();
                data.DredgingOperations = dredgingPriorityVO.DredgingOperationsVO.MapToEntity();
                //data.DockingPlanDocuments = VO.DockingPlanDocumentsVO.MapToEntity();
            }
            return data;
        }
    }
}

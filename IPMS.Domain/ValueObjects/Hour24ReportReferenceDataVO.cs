using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.ValueObjects
{
     public class Hour24ReportReferenceDataVO
    {
         //public List<SubCategoryCodeNameVO> Nuturetypes { get; set; }
         public List<SubCategoryCodeNameVO> Naturetypes { get; set; }
         public List<SubCategoryCodeNameVO> RecordingofIncident  { get; set; }
         public List<SubCategoryCodeNameVO> GeneralAgencies  { get; set; }
         public List<SubCategoryCodeNameVO> OccupationalHygieneAgencies  { get; set; }
         public List<SubCategoryCodeNameVO> TypeofContact  { get; set; }
         public List<SubCategoryCodeNameVO> StandardActs  { get; set; }
         public List<SubCategoryCodeNameVO> SubstandardConditions  { get; set; }
         public List<SubCategoryCodeNameVO> PersonalFactors  { get; set; }
         public List<SubCategoryCodeNameVO> JobFactors  { get; set; }
         public List<SubCategoryCodeNameVO> ControlStepsToPreventRecurrence  { get; set; }

         public List<SubCategoryCodeNameVO> FireDepartment { get; set; }
         public List<SubCategoryCodeNameVO> IncidentClassification { get; set; }
         public List<SubCategoryCodeNameVO> DiscriptionofExposedRisk { get; set; }
         public List<SubCategoryCodeNameVO> Section_Record_Incident { get; set; }
         public List<SubCategoryCodeNameVO> WeatherTypes { get; set; }
         public List<PortVO> Ports { get; set; }
      
    }
}

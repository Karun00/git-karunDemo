using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
   public class DredgingPriorityVO
    {
        [DataMember]
        public int DredgingPriorityID { get; set; }
        [DataMember]
        public int DeploymentPlanID { get; set; }
        [DataMember]
        public int DredgingOperationID { get; set; }
        [DataMember]
        public string FromDate { get; set; }
        [DataMember]
        public string ToDate { get; set; }
        [DataMember]
        public string MonthValue { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public Nullable<int> FinancialYearID { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string PortCode { get; set; }

        [DataMember]
        public string BerthKey { get; set; }
       
        [DataMember]
        public string LocationName { get; set; }
        [DataMember]
        public string SubCatCode { get; set; }
        [DataMember]
        public string SubCatName { get; set; }
        [DataMember]
        public string FinancialYearDate { get; set; }
        [DataMember]
        public string Month { get; set; }

        [DataMember]
        public Nullable<int> DPAWorkflowInstanceID { get; set; }
        //[DataMember]
        //public System.DateTime EndDate { get; set; }
        [DataMember]
        public int LocationID { get; set; }

        [DataMember]
        public string DredgingType { get; set; }
        [DataMember]
        public int Budget { get; set; }
        [DataMember]
        public Nullable<decimal> Volume { get; set; }



        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public int DredgingPriorityDocumentID { get; set; }


        [DataMember]
        public ICollection<SubCategoryVO> FinancialYears { get; set; }
        [DataMember]
        public List<DredgingPriorityDocumentVO> DredgingPriorityDocumentsVO { get; set; }

        [DataMember]
        public List<DredgingOperationVO> DredgingOperationsVO { get; set; }
        //[DataMember]
        //public List<DredgingPriorityAreaVO> DredgingPriorityAreaVO { get; set; }
        [DataMember]
        public ICollection<SubCategoryVO> DredgingTypes { get; set; }
       
        //[DataMember]
        //public List<BerthOccupationDocumentVO> BerthOccupationDocumentVO { get; set; }
        [DataMember]
        public List<DredgingPriorityVO> BerthTypes { get; set; }
        [DataMember]
        public List<DredgingPriorityVO> LocationTypes { get; set; }
       
    }
     [DataContract]
    public class DredgingPriorityDocumentVO
    {

        [DataMember]
        public int DredgingPriorityDocumentID { get; set; }
        [DataMember]
        public int DredgingPriorityID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
       
    }
     
     [DataContract]
    public class DredgingPriorityVolumeVO
    {
        [DataMember]
        public string DredgingType { get; set; }
        [DataMember]
        public int DredgPlan { get; set; }
       [DataMember]
        public Nullable<decimal> Volume { get; set; }
        [DataMember]
        public int DeploymentPlanID { get; set; }
        [DataMember]
        public List<SubCategoryVO> SubCategoryVO { get; set; }
        [DataMember]
      //  public int DredgingPriorityID { get; set; }
       // [DataMember]
        public string TypeCode { get; set; }
        [DataMember]
        public List<int> DredgingPriorityID { get; set; }
        [DataMember]
        public List<decimal> compVolume { get; set; }
    }
}

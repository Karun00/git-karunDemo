using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class DredgingPriority : EntityBase
    {
        public DredgingPriority()
        {

            //this.BerthOccupationDocuments = new List<BerthOccupationDocument>();

            this.DredgingPriorityDocuments = new List<DredgingPriorityDocument>();
            // -- Added by sandeep on 29-12-2014
            this.DredgingOperations = new List<DredgingOperation>();
            // -- end
        }
        [DataMember]
        public int DredgingPriorityID { get; set; }
        [DataMember]
        public int DeploymentPlanID { get; set; }
        [DataMember]
        public System.DateTime FromDate { get; set; }
        [DataMember]
        public System.DateTime ToDate { get; set; }
      
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

        //[DataMember]
        //public  ICollection<BerthOccupationDocument> BerthOccupationDocuments { get; set; }
        [DataMember]
        public  DeploymentPlan DeploymentPlan { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  FinancialYear FinancialYear { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<DredgingPriorityDocument> DredgingPriorityDocuments { get; set; }
        // -- Added by sandeep on 29-12-2014
        [DataMember]
        public  ICollection<DredgingOperation> DredgingOperations { get; set; }
        // -- end
        [NotMapped]
        public string Month { get; set; }
    }
}

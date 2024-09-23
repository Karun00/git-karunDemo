using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    public class BerthMaintenanceVO
    {
        [DataMember]
        public int BerthMaintenanceID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string ProjectNo { get; set; }
        [DataMember]
        public string MaintenanceTypeCode { get; set; }
        [DataMember]
        public string MaintPortCode { get; set; }
        [DataMember]
        public string MaintQuayCode { get; set; }
        [DataMember]
        public string MaintBerthCode { get; set; }
        [DataMember]
        public string FromPortCode { get; set; }
        [DataMember]
        public string FromQuayCode { get; set; }
        [DataMember]
        public string FromBerthCode { get; set; }
        [DataMember]
        public string FromBollard { get; set; }
        [DataMember]
        public string ToPortCode { get; set; }
        [DataMember]
        public string ToQuayCode { get; set; }
        [DataMember]
        public string ToBerthCode { get; set; }
        [DataMember]
        public string ToBollard { get; set; }
        [DataMember]
        public string PeriodFrom { get; set; }
        [DataMember]
        public string PeriodTo { get; set; }
        [DataMember]
        public string OccupationTypeCode { get; set; }
        [DataMember]
        public string Precinct { get; set; }
        [DataMember]
        public string DisciplineCode { get; set; }
        [DataMember]
        public string SpecialConditions { get; set; }
        [DataMember]
        public string Description { get; set; }
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
        public string BerthKey { get; set; }
        [DataMember]
        public string FromBollardKey { get; set; }
        [DataMember]
        public string ToBollardKey { get; set; }
        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public string BollardsFrom { get; set; }
        [DataMember]
        public string BollardsTo { get; set; }
        [DataMember]
        public string MaintenanceType { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string BerthMaintenanceNo { get; set; } 
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }

        [DataMember]
        public string ReferenceNo { get; set; } 
        


        [DataMember]
        public ICollection<BerthVO> Berths { get; set; }
        [DataMember]
        public ICollection<SubCategoryVO> MaintenanceTypes { get; set; }
        [DataMember]
        public ICollection<SubCategoryVO> OccupationTypes { get; set; }
        [DataMember]
        public ICollection<BollardMaintenanceVO> Bollards { get; set; }
        [DataMember]
        public ICollection<SubCategoryVO> DepartmentTypes { get; set; }

    }

    public class BerthMaintenanceReferenceVO
    {
        [DataMember]
        public ICollection<BerthVO> Berths { get; set; }
        [DataMember]
        public ICollection<SubCategoryVO> MaintenanceTypes { get; set; }
        [DataMember]
        public ICollection<SubCategoryVO> OccupationTypes { get; set; }
        [DataMember]
        public ICollection<BollardMaintenanceVO> Bollards { get; set; }
        [DataMember]
        public ICollection<SubCategoryVO> DepartmentTypes { get; set; }

        [DataMember]
        public string BerthKey { get; set; }
        [DataMember]
        public string FromBollardKey { get; set; }
        [DataMember]
        public string ToBollardKey { get; set; }


    }
    public class BollardMaintenanceVO
    {
        [DataMember]
        public string BollardCode { get; set; }
        [DataMember]
        public string BollardName { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string QuayCode { get; set; }
        [DataMember]
        public string QuayName { get; set; }
        [DataMember]
        public string BerthCode { get; set; }
        [DataMember]
        public string BerthName { get; set; }

        [DataMember]
        public string BollardKey { get; set; }

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
}

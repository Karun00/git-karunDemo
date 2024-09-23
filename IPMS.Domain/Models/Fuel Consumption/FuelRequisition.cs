using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    public partial class FuelRequisition : EntityBase
    {
        public FuelRequisition()
        {
            this.FuelReceipts = new List<FuelReceipt>();
            this.FuelRequisitionApprovals = new List<FuelRequisitionApproval>();
        }

        public int FuelRequisitionID { get; set; }
        public string PortCode { get; set; }
        public string FuelRequistionNo { get; set; }
        public int CraftID { get; set; }
        public System.DateTime RequisitionDate { get; set; }
        public string OilTypeCode { get; set; }
        public string GradeCode { get; set; }
        public string UOMCode { get; set; }
        public decimal Quantity { get; set; }
        public System.DateTime RequiredDate { get; set; }
        public string Remarks { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> WorkflowInstanceId { get; set; }
        public Craft Craft { get; set; }
        public ICollection<FuelReceipt> FuelReceipts { get; set; }
        public User User { get; set; }
        public SubCategory SubCategory { get; set; }
        public User User1 { get; set; }
        public SubCategory SubCategory1 { get; set; }
        public Port Port { get; set; }
        public SubCategory SubCategory2 { get; set; }
        public WorkflowInstance WorkflowInstance { get; set; }
        public ICollection<FuelRequisitionApproval> FuelRequisitionApprovals { get; set; }
        public string FuelRequistionType { get; set; }
        [NotMapped]
        public string CraftName { get; set; }
        [NotMapped]
        public string OilType { get; set; }
        [NotMapped]
        public string Grade { get; set; }
        [NotMapped]
        public string RequisitionNo { get; set; }




    }
}


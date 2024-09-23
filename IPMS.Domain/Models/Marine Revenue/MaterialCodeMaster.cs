using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
    public partial class MaterialCodeMaster : EntityBase 
    {
        public MaterialCodeMaster()
        {
            this.MaterialCodePorts = new List<MaterialCodePort>();
        }

        public int MaterialCodeMasterid { get; set; }
        public string GroupCode { get; set; }
        public string MaterialCode { get; set; }
        public string MovementType { get; set; }
        public string ServiceType { get; set; }
        public string MaterialDescription { get; set; }
        public string UOM { get; set; }
        public string Remarks { get; set; }
        public string RecordStatus { get; set; }
        public string IsCalculated { get; set; }
        public string Chargedas { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public  ICollection<MaterialCodePort> MaterialCodePorts { get; set; }
    }
}

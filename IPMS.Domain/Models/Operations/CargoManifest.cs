using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class CargoManifest: EntityBase
    {
        public CargoManifest()
        {
            this.CargoManifestDtls = new List<CargoManifestDtl>();
        }
        [DataMember]
        public int CargoManifestID { get; set; }
        [DataMember]
        public System.DateTime FirstMoveDateTime { get; set; }
        [DataMember]
        public System.DateTime LastMoveDateTime { get; set; }
        [DataMember]
        public string UOMCode { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public  ArrivalNotification ArrivalNotification { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  ICollection<CargoManifestDtl> CargoManifestDtls { get; set; }
    }
}



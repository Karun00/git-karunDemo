using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ArrivalReason : EntityBase
    {
        public int ArrivalReasonID { get; set; }
        public string VCN { get; set; }
        public string Reason { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public virtual ArrivalNotification ArrivalNotification { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}

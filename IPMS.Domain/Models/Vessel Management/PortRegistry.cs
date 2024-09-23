using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.Models
{
    [DataContract]
     public partial class PortRegistry : EntityBase
    {
        public PortRegistry()
        {
            this.Vessels = new List<Vessel>();
            this.LastArrivalNotifications = new List<ArrivalNotification>();
            this.NextArrivalNotifications = new List<ArrivalNotification>();
            this.LastWasteDeclarationPorts = new List<ArrivalNotification>();
            this.NextWasteDeclarationPorts = new List<ArrivalNotification>();
        }

        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string PortName { get; set; }
        [DataMember]
        public string IsSA { get; set; }
        [DataMember]
        public string IsTNPA { get; set; }
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
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<Vessel> Vessels { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> LastArrivalNotifications { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> NextArrivalNotifications { get; set; }
        [DataMember]
        public ICollection<ArrivalNotification> LastWasteDeclarationPorts { get; set; }
        [DataMember]
        public ICollection<ArrivalNotification> NextWasteDeclarationPorts { get; set; }
    }
}

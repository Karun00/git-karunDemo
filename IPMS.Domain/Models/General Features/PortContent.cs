using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class PortContent : EntityBase
    {
        public PortContent()
        {
            this.PortContent1 = new List<PortContent>();
            this.PortContentRoles = new List<PortContentRole>();
        }

        [DataMember]
        public int PortContentID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string ContentType { get; set; }
        [DataMember]
        public string ContentName { get; set; }
        [DataMember]
        public string LinkVisibility { get; set; }
        [DataMember]
        public string LinkType { get; set; }
        [DataMember]
        public string LinkContent { get; set; }
        [DataMember]
        public Nullable<int> DocumentID { get; set; }
        [DataMember]
        public Nullable<int> ParentPortContentID { get; set; }
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
        public  Document Document { get; set; }
        [DataMember]
        public  Port Port { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<PortContent> PortContent1 { get; set; }
        [DataMember]
        public  PortContent PortContent2 { get; set; }
        [DataMember]
        public  ICollection<PortContentRole> PortContentRoles { get; set; }
    }
}

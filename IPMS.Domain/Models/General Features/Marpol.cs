using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class Marpol : EntityBase
    {
        public Marpol()
        {
            this.WasteDeclarations = new List<WasteDeclaration>();
        }
        [DataMember]
        public string ClassCode { get; set; }
        [DataMember]
        public string ClassName { get; set; }
        [DataMember]
        public string MarpolCode { get; set; }        
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
        [NotMapped]
        public string MarpolName { get; set; }
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public User User1 { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory { get; set; }
        [DataMember]
        public ICollection<WasteDeclaration> WasteDeclarations { get; set; }

    }

}
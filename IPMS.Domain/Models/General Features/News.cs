//using Core.Repository;
//using System;
using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPMS.Domain.Models
{
   [DataContract]
	public partial class News : EntityBase
    {
        public News()
        {
            this.NewsPorts = new List<NewsPort>();
        }
       
        [DataMember]
		public int NewsID { get; set; }
        [DataMember]
		public string Title { get; set; }
        [DataMember]
		public string NewsContent { get; set; }
        [DataMember]
		public System.DateTime StartDate { get; set; }
        [DataMember]
        public string NewsUrl { get; set; }
        [DataMember]
		public System.DateTime EndDate { get; set; }
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
		public virtual User User { get; set; }
         [DataMember]
		public virtual User User1 { get; set; }              
        
        [DataMember]
        public ICollection<NewsPort> NewsPorts { get; set; }
      
	}
       
}

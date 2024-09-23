using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
namespace IPMS.Domain.Models
{
	
	public partial class SuperCategory : EntityBase
	{

        public SuperCategory()
        {
            this.SubCategories = new List<SubCategory>();
            this.PermitRequestSubAreas = new List<PermitRequestSubArea>();
        }
        
	 public string SupCatCode { get; set; }	 
	 public string SupCatName { get; set; }
	 public string RecordStatus { get; set; }	
     public int CreatedBy { get; set; }	
     public System.DateTime CreatedDate { get; set; }	
     public int ModifiedBy { get; set; }	 
     public System.DateTime ModifiedDate { get; set; }    
     public  ICollection<SubCategory> SubCategories { get; set; }
     [DataMember]
     public ICollection<PermitRequestSubArea> PermitRequestSubAreas { get; set; }

        



	}
}

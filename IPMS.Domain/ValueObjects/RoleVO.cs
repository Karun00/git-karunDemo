using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using IPMS.Domain.Models;

namespace IPMS.Domain.ValueObjects
{

    public  class RoleVO
    {               
        public int RoleID { get; set; }       
        public string RoleCode { get; set; }       
        public string RoleName { get; set; }       
        public string RoleDescription { get; set; }       
        public string RecordStatus { get; set; }       
        public int CreatedBy { get; set; }
        public int ModuleID { get; set; }
        public int SubModuleID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }       
        public Nullable<int> ModifiedBy { get; set; }       
        public Nullable<System.DateTime> ModifiedDate { get; set; }          
        public  List<RolePrivilegeVO> RolePrivileges { get; set; }            
    }

    public class RolePrivilegeVO
    {
        [DataMember]
        public int RoleID { get; set; }      
        [DataMember]
        public int EntityID { get; set; }
        [DataMember]
        public string SubCatCode { get; set; }       
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

    }

    // Refernce VO

   public class ReferenceDataVO
   {
       public List<ModuleVO> getModules { get; set; }

       public List<ModuleVO> getSubModules { get; set; }

       public List<EntityListPrivlegesVO> getEntitiesListWithPrivleges { get; set; }      
   }

   public class EntityListPrivlegesVO { 
   
 // EntityID
       [DataMember]
       public int EntityID { get; set; }

       [DataMember]
       public string EntityName { get; set; }

       [DataMember]
       public List<PrivelgeVO> EntityPrivileges { get; set; }

   }

   public class PrivelgeVO {
       
       [DataMember]
       public string SubCatCode { get; set; }       
       [DataMember]
       public string SubCatName { get; set; }
       [DataMember]
       public Boolean IsRole { get; set; }
   
   }





   public class RolePrivlegeListVO
   {
       [DataMember]
       public int RoleID { get; set; }
       
       [DataMember]
       public int EntityID { get; set; }

       [DataMember]
       public List<SubCategoryVO> RolePrivEditList { get; set; }

   }

   public class ModulesSubModulesVO
   {
       public int ModuleID { get; set; }
       public Nullable<int> ParentModuleID { get; set; }
       public string ModuleName { get; set; }        
       public ICollection<SubModulesVO> SubModulesModule { get; set; }
   }


   public class SubModulesVO
   {
       public int ModuleID { get; set; }
       public Nullable<int> ParentModuleID { get; set; }
       public string ModuleName { get; set; }  
   }


   


}

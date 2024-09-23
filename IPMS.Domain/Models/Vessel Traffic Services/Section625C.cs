using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models 
{
    public partial class Section625C:EntityBase
    {
        public Section625C()
        {
            this.Section625CDetail = new List<Section625CDetail>();
            this.Section625CPrevent = new List<Section625CPrevent>();
            this.Section625CRecommended = new List<Section625CRecommended>();
        }

        public int Section625CID { get; set; }
        public int Section625ABCDID { get; set; }
        public System.DateTime IDIncidentDateTime { get; set; }
        public System.DateTime IDTimeReported { get; set; }
        public string IDIncidentSpecificLocation { get; set; }
        public string WIWitnessName1 { get; set; }
        public string WITelephoneNo1 { get; set; }
        public string WIWitnessName2 { get; set; }
        public string WITelephoneNo2 { get; set; }
        public string PIName { get; set; }
        public string PISurname { get; set; }
        public string PIEmployeeNo { get; set; }
        public Nullable<int> PINoOfDaysLost { get; set; }
        public string PIGender { get; set; }
        public Nullable<int> PIAge { get; set; }
        public string PIGradePosition { get; set; }
        public string PIPartOfBody { get; set; }
        public string IncidentDescription { get; set; }
        public string IIInvestigatorName { get; set; }
        public string IIDesignation { get; set; }
        public Nullable<System.DateTime> IIInvestigationDate { get; set; }
        public string GAOthersSpecify { get; set; }
        public string GAOHAOthersSpecify { get; set; }
        public string IDCOthersSpecify { get; set; }
        public string CurrentControls { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int Hour24Report625ID { get; set; }
        public  Hour24Report625 Hour24Report625 { get; set; }
        public  Section625ABCD Section625ABCD { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  ICollection<Section625CDetail> Section625CDetail { get; set; }
        public  ICollection<Section625CPrevent> Section625CPrevent { get; set; }
        public  ICollection<Section625CRecommended> Section625CRecommended { get; set; }
    }
}

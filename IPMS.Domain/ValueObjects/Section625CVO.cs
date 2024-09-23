using System;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class Section625CVO
    {
        [DataMember]
        public int Section625CID { get; set; }
        [DataMember]
        public int Section625ABCDID { get; set; }
        [DataMember]
        public int Hour24Report625ID { get; set; }
        [DataMember]
        public System.DateTime IDIncidentDateTime { get; set; }
        [DataMember]
        //public System.DateTime IDTimeReported { get; set; }
        public string IDTimeReported { get; set; }
        [DataMember]
        public string IDIncidentSpecificLocation { get; set; }
        [DataMember]
        public string WIWitnessName1 { get; set; }
        [DataMember]
        public string WITelephoneNo1 { get; set; }
        [DataMember]
        public string WIWitnessName2 { get; set; }
        [DataMember]
        public string WITelephoneNo2 { get; set; }
        [DataMember]
        public string PIName { get; set; }
        [DataMember]
        public string PISurname { get; set; }
        [DataMember]
        public string PIEmployeeNo { get; set; }
        [DataMember]
        public Nullable<int> PINoOfDaysLost { get; set; }
        [DataMember]
        public string PIGender { get; set; }
        [DataMember]
        public Nullable<int> PIAge { get; set; }
        [DataMember]
        public string PIGradePosition { get; set; }
        [DataMember]
        public string PIPartOfBody { get; set; }
        [DataMember]
        public string IncidentDescription { get; set; }
        [DataMember]
        public string IIInvestigatorName { get; set; }
        [DataMember]
        public string IIDesignation { get; set; }
        [DataMember]
        public Nullable<System.DateTime> IIInvestigationDate { get; set; }
        [DataMember]
        public string GAOthersSpecify { get; set; }
        [DataMember]
        public string GAOHAOthersSpecify { get; set; }
        [DataMember]
        public string IDCOthersSpecify { get; set; }
        [DataMember]
        public string CurrentControls { get; set; }
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
    }
}

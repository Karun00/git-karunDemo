using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class Hour24Report625VO
    {
        [DataMember]
        public int Hour24Report625ID { get; set; }
        [DataMember]
        public string OperatorName { get; set; }
        [DataMember]
        public string LincseNumber { get; set; }
        [DataMember]
        public string CDName { get; set; }
        [DataMember]
        public string CDDesignation { get; set; }
        [DataMember]
        public string CDContactNumber { get; set; }
        [DataMember]
        public string CDMobileNumber { get; set; }
        [DataMember]
        public string CDEmailID { get; set; }
        [DataMember]
        public string Timeperiod { get; set; }
        [DataMember]
        public string CDAddress { get; set; }
        [DataMember]
        public string NONatureCode { get; set; }
        [DataMember]
        public string NatureofOccuranceName { get; set; }
        [DataMember]
        public Nullable<System.DateTime> IODOccuranceDateTime { get; set; }
        [DataMember]
        public string IODSpecificLocation { get; set; }
        [DataMember]
        public string IODOccuranceBriefDescription { get; set; }
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
        public string PortCode { get; set; }
        [DataMember]
        public Section625ABCDVO section625abcd { get; set; }
        //[DataMember]
        //public List<Section625ABCDVO> section625abcd { get; set; }
        [DataMember]
        public Section625BVO section625b { get; set; }
        [DataMember]
        public Section625CVO section625c { get; set; }

        [DataMember]
        public Section625DVO section625D { get; set; }

        [DataMember]
        public Section625EVO section625E { get; set; }
        [DataMember]
        public Section625GVO section625G { get; set; }

        [DataMember]
        public ICollection<Section625BUnionVO> Section625BUnion { get; set; }
        [DataMember]
        public ICollection<Section625EDetailVO> Section625EDetail { get; set; }



        [DataMember]
        public ICollection<Section625DDetailVO> Section625DDetail { get; set; }
        [DataMember]
        public ICollection<Section625GDetail1VO> Section625GDetail1 { get; set; }
        [DataMember]
        public ICollection<Section625GDetail2VO> Section625GDetail2 { get; set; }

        [DataMember]
        public ICollection<Section625CDetailVO> Section625CDetail { get; set; }
        [DataMember]
        public ICollection<Section625CPreventVO> Section625CPrevent { get; set; }
        [DataMember]
        public ICollection<Section625CRecommendedVO> Section625CRecommended { get; set; }



        [DataMember]
        public List<string> selectedrecordingofIncidentdetails { get; set; }
        [DataMember]
        public List<string> selectedgeneralagenciesdetails { get; set; }
        [DataMember]
        public List<string> selectedOccupationalHygieneAgencies { get; set; }
        [DataMember]
        public List<string> selectedTypeofContact { get; set; }
        [DataMember]
        public List<string> selectedStandardAct { get; set; }
        [DataMember]
        public List<string> selectedSubstandardCondition { get; set; }
        [DataMember]
        public List<string> selectedPersonalFactors { get; set; }
        [DataMember]
        public List<string> selectedJobFactors { get; set; }
        [DataMember]
        public List<string> selectedControlStepsToPreventRecurrence { get; set; }
       
        
        
        
        
        [DataMember]
        public List<string> selectedFireDepartment { get; set; }
        [DataMember]
        public List<string> selectedIncidentClassification { get; set; }
        [DataMember]
        public List<string> selectedDiscriptionofExposedRisk { get; set; }
         [DataMember]
        public List<string> selectedRecordingofIncident { get; set; }

    }
}
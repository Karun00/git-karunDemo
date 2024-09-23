using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public class Section625DVO
    {
        [DataMember]
        public int Section625DID { get; set; }
        [DataMember]
        public int Hour24Report625ID { get; set; }
        [DataMember]
        public int Section625ABCDID { get; set; }
        [DataMember]
        public System.DateTime IncidentDateTime { get; set; }
        [DataMember]
        //public System.DateTime TimeReported { get; set; }
        public string TimeReported { get; set; }
        [DataMember]
        public string SpecifyLocationOfFire { get; set; }
        [DataMember]
        public string FireDepartmentAttend { get; set; }
        [DataMember]
        public string OthersSpecify { get; set; }
        [DataMember]
        public string FICommercial { get; set; }
        [DataMember]
        public string FIStorage { get; set; }
        [DataMember]
        public string FIIndustry { get; set; }
        [DataMember]
        public string FITransport { get; set; }
        [DataMember]
        public string FIOthers { get; set; }
        [DataMember]
        public string FIMiscillaniousSpecify { get; set; }
        [DataMember]
        public string ICOthersSpecify { get; set; }
        [DataMember]
        public string DEROthersSpecify { get; set; }
        [DataMember]
        public string APDDamage { get; set; }
        [DataMember]
        public Nullable<int> APDMaximumEstimatedFinancialLoss { get; set; }
        [DataMember]
        public Nullable<int> APDActualLoss { get; set; }
        [DataMember]
        public string MEByWhom { get; set; }
        [DataMember]
        public string MEWithWhatBeforeFire { get; set; }
        [DataMember]
        public string MEWithWhatAfterFire { get; set; }
        [DataMember]
        public string FurtherInformation { get; set; }
        [DataMember]
        public string WCWeather { get; set; }
        [DataMember]
        public string WCTemperature { get; set; }
        [DataMember]
        public string WCWindSpeed { get; set; }
        [DataMember]
        public string WCWindDirection { get; set; }
        [DataMember]
        public string Remarks { get; set; }
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

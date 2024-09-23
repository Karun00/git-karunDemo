using Core.Repository;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models 
{
    public partial class Section625D:EntityBase
    {
        public Section625D()
        {
            this.Section625DDetail = new List<Section625DDetail>();
        }

        public int Section625DID { get; set; }
        public int Section625ABCDID { get; set; }
        public System.DateTime IncidentDateTime { get; set; }
        public System.DateTime TimeReported { get; set; }
        public string SpecifyLocationOfFire { get; set; }
        public string FireDepartmentAttend { get; set; }
        public string OthersSpecify { get; set; }
        public string FICommercial { get; set; }
        public string FIStorage { get; set; }
        public string FIIndustry { get; set; }
        public string FITransport { get; set; }
        public string FIOthers { get; set; }
        public string FIMiscillaniousSpecify { get; set; }
        public string ICOthersSpecify { get; set; }
        public string DEROthersSpecify { get; set; }
        public string APDDamage { get; set; }
        public Nullable<int> APDMaximumEstimatedFinancialLoss { get; set; }
        public Nullable<int> APDActualLoss { get; set; }
        public string MEByWhom { get; set; }
        public string MEWithWhatBeforeFire { get; set; }
        public string MEWithWhatAfterFire { get; set; }
        public string FurtherInformation { get; set; }
        public string WCWeather { get; set; }
        public string WCTemperature { get; set; }
        public string WCWindSpeed { get; set; }
        public string WCWindDirection { get; set; }
        public string Remarks { get; set; }
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
        public  ICollection<Section625DDetail> Section625DDetail { get; set; }
    }
}

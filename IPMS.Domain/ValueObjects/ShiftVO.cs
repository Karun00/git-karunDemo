using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    [DataContract]
    public partial class ShiftVO
    {
        [DataMember]
        public int ShiftID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string ShiftName { get; set; }
        [DataMember]
        public string StartTime { get; set; }
        [DataMember]
        public string EndTime { get; set; }
        [DataMember]
        public string IsShiftOff { get; set; }
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
        public string ShiftFormat { get; set; }

        [DataMember]
        public string RollOverOn { get; set; }
        

        // -- Added by sandeep on 07-01-2015
        [DataMember]
        public Nullable<int> FirstShiftID { get; set; }
        [DataMember]
        public Nullable<int> SecondShiftID { get; set; }
        [DataMember]
        public string IsContinuousShift { get; set; }
        // -- end
    }

}

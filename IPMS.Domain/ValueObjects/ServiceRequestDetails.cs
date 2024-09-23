
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class ServiceRequestDetails : EntityBase
    {        
        [DataMember]
        public string VCND { get; set; } 
        [DataMember]
        public int VesselID { get; set; } 
        [DataMember]
        public string VesselName { get; set; }   
        [DataMember]
        public string MovementType { get; set; }
        [DataMember]
        public DateTime MovementDatetime { get; set; }
        [DataMember]
        public DateTime SubmittedDateTime { get; set; }
        [DataMember]
        public string MovementName { get; set; }
        [DataMember]
        public string SieAlongSideName { get; set; }
        /// <summary>
        /// ////////////sR//////////////
        /// </summary>
        [DataMember]
        public int ServiceRequestID { get; set; }
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public string SideAlongSideCode { get; set; }
        [DataMember]
        public string OwnSteam { get; set; }
        [DataMember]
        public string NoMainEngine { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }

        ////////////SRSlng///////////////////

        [DataMember]
        public string MarineRevenueCleared { get; set; }
        [DataMember]
        public int DocumentID { get; set; }

        //////////SRshftng///////////////////

        [DataMember]
        public string ToPortCode { get; set; }
        [DataMember]
        public string ToQuayCode { get; set; }
        [DataMember]
        public string ToBerthCode { get; set; }
        [DataMember]
        public string FromPositionPortCode { get; set; }
        [DataMember]
        public string FromPositionQuayCode { get; set; }
        [DataMember]
        public string FromPositionBerthCode { get; set; }
        [DataMember]
        public string FromPositionBollardCode { get; set; }
        [DataMember]
        public string ToPositionPortCode { get; set; }
        [DataMember]
        public string ToPositionQuayCode { get; set; }
        [DataMember]
        public string ToPositionBerthCode { get; set; }
        [DataMember]
        public string ToPositionBollardCode { get; set; }
        [DataMember]
        public decimal DraftFWD { get; set; }
        [DataMember]
        public decimal DraftAFT { get; set; }

        //////////////SRWrpng/////////////////////

        //[DataMember]
        //public string FromPositionPortCode { get; set; }
        //[DataMember]
        //public string FromPositionQuayCode { get; set; }
        //[DataMember]
        //public string FromPositionBerthCode { get; set; }
        //[DataMember]
        //public string FromPositionBollardCode { get; set; }
        //[DataMember]
        //public string ToPositionPortCode { get; set; }
        //[DataMember]
        //public string ToPositionQuayCode { get; set; }
        //[DataMember]
        //public string ToPositionBerthCode { get; set; }
        //[DataMember]
        //public string ToPositionBollardCode { get; set; }

        ///////////////////Document/////////////////

        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string DocumentPath { get; set; }
        [DataMember]
        public string FileName { get; set; }
        ///////////////Others///////////////////////

        [DataMember]
        public string BerthName { get; set; }
        [DataMember]
        public string FromPositionBollard { get; set; }
        [DataMember]
        public string ToPositionBollard { get; set; }

    }
}

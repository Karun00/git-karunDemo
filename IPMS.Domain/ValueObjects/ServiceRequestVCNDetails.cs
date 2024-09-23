
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
    public partial class ServiceRequestVCNDetails : EntityBase
    {
        [DataMember]
        public string VCN { get; set; }
        [DataMember]
        public int VesselID { get; set; }
        [DataMember]
        public string VesselName { get; set; }
        [DataMember]
        public string ReasonForVisit { get; set; }
        [DataMember]
        public string VesselType { get; set; }
        [DataMember]
        public string CallSign { get; set; }
        [DataMember]
        public System.DateTime ETA { get; set; }
        [DataMember]
        public System.DateTime ETD { get; set; }
        [DataMember]
        public string IMONo { get; set; }
        //[DataMember]
        //public Nullable<long> LengthOverallInM { get; set; } 
        [DataMember]
        public Nullable<decimal> LengthOverallInM { get; set; }
        //[DataMember]
        //public Nullable<long> BeamInM { get; set; } 
        [DataMember]
        public Nullable<decimal> BeamInM { get; set; }
        //[DataMember]
        //public Nullable<long> GrossRegisteredTonnageInMT { get; set; }
        [DataMember]
        public Nullable<decimal> GrossRegisteredTonnageInMT { get; set; }
        //[DataMember]
        //public Nullable<long> DeadWeightTonnageInMT { get; set; } 
        [DataMember]
        public Nullable<decimal> DeadWeightTonnageInMT { get; set; }
        [DataMember]
        public string LastPortOfCall { get; set; }
        [DataMember]
        public string NextPortOfCall { get; set; }
        [DataMember]
        public string Tidal { get; set; }
        [DataMember]
        public string DaylightRestriction { get; set; }
        [DataMember]
        public string VesselNationality { get; set; }
        [DataMember]
        public string ArrDraft { get; set; }
        [DataMember]
        public string VoyageIn { get; set; }
        [DataMember]
        public string VoyageOut { get; set; }
        [DataMember]
        public string PilotExemption { get; set; }

        //////////////////////////////////////////
        [DataMember]
        public string IsPHANFinal { get; set; }
        [DataMember]
        public string IsISPSANFinal { get; set; }
        [DataMember]
        public string IsIMDGANFinal { get; set; }
        [DataMember]
        public string TidalStatus { get; set; }

        [DataMember]
        public string RegisteredName { get; set; }
        [DataMember]
        public string TelephoneNo1 { get; set; }
        [DataMember]
        public string FaxNo { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string SurName { get; set; }
        [DataMember]
        public string CellularNo { get; set; }
        [DataMember]
        public string EmailID { get; set; }



        // -- Added by sandeep on 24-8-2014
        [DataMember]
        public string AnyDangerousGoodsonBoard { get; set; }
        [DataMember]
        public string DangerousGoodsClass { get; set; }
        [DataMember]
        public string UNNo { get; set; }

        // -- end

        [DataMember]
        public string CurrentBerth { get; set; }
        [DataMember]
        public string CurrentFromBollardName { get; set; }
        [DataMember]
        public string CurrentToBollardName { get; set; }
        [DataMember]
        public string CurrentBerthCode { get; set; }


        [DataMember]
        public Nullable<System.DateTime> ETB { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ETUB { get; set; }
        [DataMember]
        public int SuppDryDockID { get; set; }
        [DataMember]
        public string IsSpecialNature { get; set; }
        [DataMember]
        public string VesselTypeCode { get; set; }
        [DataMember]
        public string Reasons { get; set; }

    }
}

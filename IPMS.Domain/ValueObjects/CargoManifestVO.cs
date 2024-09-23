using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace IPMS.Domain.ValueObjects
{
    public class CargoManifestVO
    {
        [DataMember]
        public int CargoManifestID { get; set; }
        [DataMember]
        public string FirstMoveDateTime { get; set; }
        [DataMember]
        public string LastMoveDateTime { get; set; }
        [DataMember]
        public string UOMCode { get; set; }
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
        [DataMember]
        public string VCN { get; set; }

        public List<CargoManifestDtlVO> CargoManifests { get; set; }
    }

    public class CargoManifestDtlVO
    {
        [DataMember]
        public int CargoManifestDtlID { get; set; }
        [DataMember]
        public int CargoManifestID { get; set; }
        [DataMember]
        public string CargoTypeCode { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public string UOMCode { get; set; }
        [DataMember]
        public Nullable<decimal> OutTurn { get; set; }

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

    public class VCNData
    {
        public Nullable<int> CargoManifestID { get; set; }
        public string VCN { get; set; }
        public string VesselName { get; set; }
        public string VesselType { get; set; }
        public string Agent { get; set; }
        public Nullable<decimal> LengthOverallInM { get; set; }
        public string MaxDraft { get; set; }
        public string IMDG { get; set; }
        public Nullable<DateTime> NominationDate { get; set; }
        public Nullable<DateTime> ETA { get; set; }
        public Nullable<DateTime> ETD { get; set; }
        public string PreferredBerth { get; set; }
        public string AlternateBerth { get; set; }
        public string ReasonforVisit { get; set; }
        public string CargoType { get; set; }
        public Nullable<DateTime> ETB { get; set; }
        public Nullable<DateTime> ETUB { get; set; }
        public string Berth { get; set; }
        public Nullable<DateTime> FirstMoveDateTime { get; set; }
        public Nullable<DateTime> LastMoveDateTime { get; set; }
        public Nullable<DateTime> ATA { get; set; }
        public Nullable<DateTime> ATD { get; set; }

        public List<ArrivalCommoditiesList> CargoManifests { get; set; }
    }

    public class ArrivalCommoditiesList
    {
        public int CargoManifestDtlID { get; set; }
        public int CargoManifestID { get; set; }
        public string CargoTypeCode { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string UOMCode { get; set; }
        public Nullable<decimal> OutTurn { get; set; }

        public string VCN { get; set; }
        public string CargoTypeName { get; set; }
        public string UOM { get; set; }
        public Nullable<System.DateTime> ATA { get; set; }
        public Nullable<System.DateTime> ATD { get; set; }

        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

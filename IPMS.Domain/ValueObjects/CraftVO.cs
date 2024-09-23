using System;
using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for Craft Details
    /// </summary>
    public partial class CraftVO
    {
        public int CraftID { get; set; }
        public string CraftCode { get; set; }
        public string CraftName { get; set; }
        public string IMONo { get; set; }
        public string CallSign { get; set; }
        public string ExCallSign { get; set; }
        public string CraftType { get; set; }
        public Nullable<System.DateTime> CraftBuildDate { get; set; }
        public Nullable<System.DateTime> DateOfDelivery { get; set; }
        public string CraftNationality { get; set; }
        public string ClassificationSociety { get; set; }
        public Nullable<System.DateTime> CommissionDate { get; set; }
        public decimal AFCInMetricTon { get; set; }
        public string FuelType { get; set; }
        public string PortCode { get; set; }
        public string PortOfRegistry { get; set; }
        public Nullable<int> EnginePower { get; set; }
        public string EngineType { get; set; }
        public string PropulsionType { get; set; }
        public Nullable<int> NoOfPropellers { get; set; }
        public Nullable<int> MaxManeuveringSpeed { get; set; }
        public long BeamM { get; set; }
        public long RegisteredLengthM { get; set; }
        public long ForwardDraftM { get; set; }
        public long AftDraftM { get; set; }
        public long GrossRegisteredTonnageMT { get; set; }
        public long NetRegisteredTonnageMT { get; set; }
        public Nullable<long> DeadWeightTonnageMT { get; set; }
        public Nullable<long> BollardPullMT { get; set; }
        public string OwnersName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailID { get; set; }
        public Nullable<long> InitialFuelQuantityMT { get; set; }
        public Nullable<long> LOROBMT { get; set; }
        public Nullable<long> HYDROBMT { get; set; }
        public Nullable<long> FreshwaterROBMT { get; set; }
        public string CraftCommissionStatus { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        //public List<FuelConsumptionDailyLogVO> FuelConsumptionDailyLogs { get; set; } 
        public string BuildDate { get; set; }
        public string FuelTypeName { get; set; }
        public string CraftTypeName { get; set; }
        public string CommissionStatus { get; set; }
        public string DredgerColorCode { get; set; }
        public List<CraftReminderConfigVO> CraftReminderConfigs { get; set; }

    }
}

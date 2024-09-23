using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models
{
    public partial class Shift : EntityBase
    {
        public Shift()
        {
            this.ResourceAttendances = new List<ResourceAttendance>();
            this.ResourceAttendancesdtl = new List<ResourceAttendanceDtl>();
            this.ResourceRosters = new List<ResourceRoster>();
            this.RosterDtls = new List<RosterDtl>();
            this.RosterGroups = new List<RosterGroup>();
            this.RosterGroups1 = new List<RosterGroup>();
            this.RosterGroups2 = new List<RosterGroup>();
            this.RosterGroups3 = new List<RosterGroup>();
            this.RosterGroups4 = new List<RosterGroup>();
            this.RosterGroups5 = new List<RosterGroup>();
            this.RosterGroups6 = new List<RosterGroup>();

            // -- Added by sandeep on 08-01-2015
            this.RosterDtls1 = new List<RosterDtl>();
            // -- end
        }

        public int ShiftID { get; set; }
        public string PortCode { get; set; }
        public string ShiftName { get; set; }

        // -- Changed by sandeep on 07-01-2015
        //public System.DateTime StartTime { get; set; }
        //public System.DateTime EndTime { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> FirstShiftID { get; set; }
        public Nullable<int> SecondShiftID { get; set; }
        public string IsContinuousShift { get; set; }
        // -- end

        public string IsShiftOff { get; set; }
        public string RollOverOn { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public  Port Port { get; set; }
        public  ICollection<ResourceAttendance> ResourceAttendances { get; set; }
        public ICollection<ResourceAttendanceDtl> ResourceAttendancesdtl { get; set; }
        
        public  ICollection<ResourceRoster> ResourceRosters { get; set; }
        public  ICollection<RosterDtl> RosterDtls { get; set; }
        public  ICollection<RosterGroup> RosterGroups { get; set; }
        public  ICollection<RosterGroup> RosterGroups1 { get; set; }
        public  ICollection<RosterGroup> RosterGroups2 { get; set; }
        public  ICollection<RosterGroup> RosterGroups3 { get; set; }
        public  ICollection<RosterGroup> RosterGroups4 { get; set; }
        public  ICollection<RosterGroup> RosterGroups5 { get; set; }
        public  ICollection<RosterGroup> RosterGroups6 { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }

        // -- Added by sandeep on 08-01-2015
        public  ICollection<RosterDtl> RosterDtls1 { get; set; }
        // -- end


    }
}

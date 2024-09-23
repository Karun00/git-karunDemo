using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Repository.Providers.EntityFramework;
using System.Data;
namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class WorkflowInstance : EntityBase
    {
        public WorkflowInstance()
        {
            // this.Agent = new List<Agent>();
            this.ServiceRequests = new List<ServiceRequest>();
            this.ArrivalNotifications = new List<ArrivalNotification>();
            this.BerthMaintenances = new List<BerthMaintenance>();
            this.BerthMaintenanceCompletions = new List<BerthMaintenanceCompletion>();
            this.DockingPlans = new List<DockingPlan>();
            // this.DredgingPriorities = new List<DredgingPriority>();

            this.FuelRequisitions = new List<FuelRequisition>();
            this.FuelReceipts = new List<FuelReceipt>();
            this.WorkflowProcess = new List<WorkflowProcess>();
            this.LicenseRequestPorts = new List<LicenseRequestPort>();
            this.Pilots = new List<Pilot>();

            // -- Added by Sandeep on 29-07-2014

            this.DivingOccupationApprovals = new List<DivingOccupationApproval>();

            // -- end
            this.Vessels = new List<Vessel>();

            // -- Added by sandeep on 21-8-2014
            this.SuppServiceRequests = new List<SuppServiceRequest>();
            // -- end

            this.EventScheduleTracks = new List<EventScheduleTrack>();
            this.PermitRequests = new List<PermitRequest>();

            // -- Sandeep Added on 06-11-2014
            this.SuppDryDocks = new List<SuppDryDock>();
            // -- end

            // -- Added By Santosh on 05-12-2014
            this.DepartureNotices = new List<DepartureNotice>();
            // -- end

            // -- Added By Omprakash on 22nd Dec 2014
            this.SuppDryDockExtensions = new List<SuppDryDockExtension>();
            // -- end

            // -- Added by sandeep on 29-12-2014
            this.DredgingOperations = new List<DredgingOperation>();
            this.DredgingOperations1 = new List<DredgingOperation>();
            this.DredgingOperations2 = new List<DredgingOperation>();
            // -- end
            this.ServiceRequests1 = new List<ServiceRequest>();
            
        }

        [DataMember]
        public int WorkflowInstanceId { get; set; }
        [DataMember]
        public int EntityID { get; set; }
        [DataMember]
        public string PortCode { get; set; }
        [DataMember]
        public string ReferenceID { get; set; }
        [DataMember]
        public string WorkflowTaskCode { get; set; }
        [DataMember]
        public string RecordStatus { get; set; }
        [DataMember]
        public int CreatedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedDate { get; set; }
        [DataMember]
        public Nullable<int> ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime ModifiedDate { get; set; }
        [DataMember]
        public Nullable<int> WorkflowProcessId { get; set; }
        [DataMember]
        public  Entity Entity { get; set; }
        [DataMember]
        public  Port Port { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  ICollection<WorkflowProcess> WorkflowProcess { get; set; }
        [DataMember]
        public  ICollection<ArrivalNotification> ArrivalNotifications { get; set; }
        [DataMember]
        public  ICollection<Pilot> Pilots { get; set; }
        [DataMember]
        public  ICollection<VesselAgentChange> VesselAgentChanges { get; set; }
        [DataMember]
        public  ICollection<ServiceRequest> ServiceRequests { get; set; }
        [DataMember]
        public int UserTypeId { get; set; }
        [DataMember]
        public  ICollection<PermitRequest> PermitRequests { get; set; }
        [DataMember]
        public  ICollection<LicenseRequestPort> LicenseRequestPorts { get; set; }
        [DataMember]
        public  ICollection<Vessel> Vessels { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenance> BerthMaintenances { get; set; }
        [DataMember]
        public  ICollection<BerthMaintenanceCompletion> BerthMaintenanceCompletions { get; set; }
        [DataMember]
        public  ICollection<DockingPlan> DockingPlans { get; set; }

        // public  ICollection<DredgingPriority> DredgingPriorities { get; set; }

        [DataMember]
        public  ICollection<FuelRequisition> FuelRequisitions { get; set; }
        [DataMember]
        public  ICollection<FuelReceipt> FuelReceipts { get; set; }


        [DataMember]
        public string UserType { get; set; }

        [DataMember]
        public  ICollection<EventScheduleTrack> EventScheduleTracks { get; set; }

        // -- Added by Sandeep on 29-07-2014

        public  ICollection<DivingOccupationApproval> DivingOccupationApprovals { get; set; }

        // -- end

        [NotMapped]
        [StoredProcedure("usp_WorkflowInstance_dml")]
        public class WorkflowInstance_Dml_Proc
        {
            private WorkflowInstance _objWorkflowInstance;
            public WorkflowInstance_Dml_Proc(WorkflowInstance p_WorkflowInstance)
            {
                _objWorkflowInstance = p_WorkflowInstance;
            }

            [StoredProcedureParameter(System.Data.SqlDbType.Int, Direction = ParameterDirection.InputOutput)]
            public int WorkflowInstanceId
            {
                get
                {
                    return _objWorkflowInstance.WorkflowInstanceId;
                }
                set
                {
                    _objWorkflowInstance.WorkflowInstanceId = value;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public int EntityID
            {
                get
                {
                    return _objWorkflowInstance.EntityID;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string PortCode
            {
                get
                {
                    return _objWorkflowInstance.PortCode;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string ReferenceID
            {
                get
                {
                    return _objWorkflowInstance.ReferenceID;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string WorkflowTaskCode
            {
                get
                {
                    return _objWorkflowInstance.WorkflowTaskCode;
                }
            }


            [StoredProcedureParameter(SqlDbType.Int, Direction = ParameterDirection.Input)]
            public int UserTypeId
            {
                get
                {
                    return _objWorkflowInstance.UserTypeId;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string UserType
            {
                get
                {
                    return _objWorkflowInstance.UserType;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string RecordStatus
            {
                get
                {
                    return _objWorkflowInstance.RecordStatus;
                }
            }


            [StoredProcedureParameter(SqlDbType.Int, Direction = ParameterDirection.Input)]
            public int CreatedBy
            {
                get
                {
                    return _objWorkflowInstance.CreatedBy;
                }
            }


            [StoredProcedureParameter(SqlDbType.DateTime2, Direction = ParameterDirection.Input)]
            public System.DateTime CreatedDate
            {
                get
                {
                    return _objWorkflowInstance.CreatedDate;
                }
            }


            [StoredProcedureParameter(SqlDbType.Int, Direction = ParameterDirection.Input)]
            public Nullable<int> ModifiedBy
            {
                get
                {
                    return _objWorkflowInstance.ModifiedBy;
                }
            }


            [StoredProcedureParameter(SqlDbType.DateTime2, Direction = ParameterDirection.Input)]
            public System.DateTime ModifiedDate
            {
                get
                {
                    return _objWorkflowInstance.ModifiedDate;
                }
            }

        }

        // -- Added by sandeep on 21-8-2014
        [DataMember]
        public  ICollection<SuppServiceRequest> SuppServiceRequests { get; set; }
        // -- end

        // -- Sandeep Added on 06-11-2014
        [DataMember]
        public  ICollection<SuppDryDock> SuppDryDocks { get; set; }
        // -- end


        // -- Added By Santosh on 05-12-2014
        [DataMember]
        public  ICollection<DepartureNotice> DepartureNotices { get; set; }
        // -- end


        // -- Omprakash Added on 22nd Dec 2014
        [DataMember]
        public  ICollection<SuppDryDockExtension> SuppDryDockExtensions { get; set; }
        // -- end

        // -- Added by sandeep on 29-12-2014
        public  ICollection<DredgingOperation> DredgingOperations { get; set; }
        public  ICollection<DredgingOperation> DredgingOperations1 { get; set; }
        public  ICollection<DredgingOperation> DredgingOperations2 { get; set; }
        // -- end

        public ICollection<ServiceRequest> ServiceRequests1 { get; set; }
    }
}

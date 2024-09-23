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
    public partial class WorkflowProcess : EntityBase
    {

        public WorkflowProcess()
        {
            this.EventScheduleTracks = new List<EventScheduleTrack>();
        }
        [DataMember]
        public int WorkflowProcessId { get; set; }
        [DataMember]
        public Nullable<int> WorkflowInstanceId { get; set; }
        [DataMember]
        public int RoleId { get; set; }
        [DataMember]
        public string FromTaskCode { get; set; }
        [DataMember]
        public string ToTaskCode { get; set; }
        [DataMember]
        public string ReferenceData { get; set; }
        [DataMember]
        public string Remarks { get; set; }
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
        public  Role Role { get; set; }
        [DataMember]
        public  SubCategory SubCategory { get; set; }
        [DataMember]
        public  SubCategory SubCategory1 { get; set; }
        [DataMember]
        public  User User { get; set; }
        [DataMember]
        public  User User1 { get; set; }
        [DataMember]
        public  WorkflowInstance WorkflowInstance { get; set; }
        [DataMember]
        public  ICollection<EventScheduleTrack> EventScheduleTracks { get; set; }

        [NotMapped]
        [StoredProcedure("usp_WorkflowProcess_dml")]
        public class WorkflowProcess_Dml_Proc
        {
            private WorkflowProcess _objWorkflowProcess;
            public WorkflowProcess_Dml_Proc(WorkflowProcess p_WorkflowProcess)
            {
                _objWorkflowProcess = p_WorkflowProcess;
            }

            [StoredProcedureParameter(System.Data.SqlDbType.Int, Direction = ParameterDirection.InputOutput)]
            public int WorkflowProcessId
            {
                get
                {
                    return _objWorkflowProcess.WorkflowProcessId;
                }
                set
                {
                    _objWorkflowProcess.WorkflowProcessId = value;
                }
            }

            [StoredProcedureParameter(System.Data.SqlDbType.Int, Direction = ParameterDirection.Input)]
            public Nullable<int> WorkflowInstanceId
            {
                get
                {
                    return _objWorkflowProcess.WorkflowInstanceId;
                }
                set
                {
                    _objWorkflowProcess.WorkflowInstanceId = value;
                }
            }

            [StoredProcedureParameter(SqlDbType.Int, Direction = ParameterDirection.Input)]
            public int RoleId
            {
                get
                {
                    return _objWorkflowProcess.RoleId;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string FromTaskCode
            {
                get
                {
                    return _objWorkflowProcess.FromTaskCode;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string ToTaskCode
            {
                get
                {
                    return _objWorkflowProcess.ToTaskCode;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string ReferenceData
            {
                get
                {
                    return _objWorkflowProcess.ReferenceData;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string Remarks
            {
                get
                {
                    return _objWorkflowProcess.Remarks;
                }
            }


            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string RecordStatus
            {
                get
                {
                    return _objWorkflowProcess.RecordStatus;
                }
            }


            [StoredProcedureParameter(SqlDbType.Int, Direction = ParameterDirection.Input)]
            public int CreatedBy
            {
                get
                {
                    return _objWorkflowProcess.CreatedBy;
                }
            }


            [StoredProcedureParameter(SqlDbType.DateTime2, Direction = ParameterDirection.Input)]
            public System.DateTime CreatedDate
            {
                get
                {
                    return _objWorkflowProcess.CreatedDate;
                }
            }


            [StoredProcedureParameter(SqlDbType.Int, Direction = ParameterDirection.Input)]
            public Nullable<int> ModifiedBy
            {
                get
                {
                    return _objWorkflowProcess.ModifiedBy;
                }
            }


            [StoredProcedureParameter(SqlDbType.DateTime2, Direction = ParameterDirection.Input)]
            public System.DateTime ModifiedDate
            {
                get
                {
                    return _objWorkflowProcess.ModifiedDate;
                }
            }

        }

    }
}

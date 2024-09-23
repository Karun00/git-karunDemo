ALTER PROCEDURE [dbo].[usp_GetResourceAllocationByID]
   @resourceAllocationId INT
AS
   BEGIN
      SELECT RA.ResourceAllocationID,
             RA.ServiceReferenceType,
             RA.ServiceReferenceID,
             RA.OperationType,
             RA.ResourceID,
             RA.ResourceType,
             RA.StartTime,
             RA.EndTime,
             RA.TaskStatus,
             RA.RecordStatus,
             RA.CreatedBy,
             RA.CreatedDate,
             RA.ModifiedBy,
             RA.ModifiedDate,
             RA.AllocSlot,
             RA.Remarks,
             RA.AcknowledgeDate,
             RA.AllocationDate,
             RA.CraftID,
             RA.IsConfirm,
             RM.UserID,
             RM.UserName,
             RM.UserType,
             RM.UserTypeID,
             RM.FirstName,
             RM.LastName,
             RM.ContactNo,
             RM.EmailID,
             dbo.udf_GetEmployeeNameByUserId (RM.UserID) AS Name,
             COALESCE (UP.WorkflowInstanceId, 0) WorkflowInstanceId,
             RM.RecordStatus,
             RM.AnonymousUserYn,
             AN.VCN,
             VM.VesselName,
             DATEDIFF (mi, RA.ModifiedDate, getdate ())
                AcknowledgeTimeMinutes
        FROM ResourceAllocation RA
             INNER JOIN Users RM ON RM.UserID = RA.ResourceID
             INNER JOIN UserPort UP ON UP.UserID = RM.UserID
             LEFT JOIN ServiceRequest SR ON SR.ServiceRequestID = RA.ServiceReferenceID AND RA.ServiceReferenceType = 'VTSR' 
             LEFT JOIN SuppServiceRequest SSR ON  SSR.SuppServiceRequestID = RA.ServiceReferenceID AND RA.ServiceReferenceType = 'SUPP'
             LEFT JOIN ArrivalNotification AN ON (AN.VCN = SR.VCN OR AN.VCN = SSR.VCN)
             LEFT JOIN Vessel VM ON VM.VesselID = AN.VesselID
       WHERE     RA.TaskStatus = 'CFRI'
             AND RA.ResourceID IS NOT NULL
             AND RA.ResourceAllocationID = @resourceAllocationId
   END
GO

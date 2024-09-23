Update Entity set Tokens = 'VCN,VesselName,MovementName,MovementDateTime,SubmittedDateTime,SubmitDateTime,PortName,PortCode' 
where EntityCode IN ('SERVREQ','SERVSHFT')
GO

Update NotificationTemplate set EmailTemplate = replace(EmailTemplate, 'SubmittedDateTime', 'SubmitDateTime')
where EntityID IN (select EntityId from Entity where EntityCode IN ('SERVREQ','SERVSHFT'))
GO


ALTER PROCEDURE [dbo].[usp_GetServiceRequestByID]
   @ServiceRequestID INT
AS
   DECLARE @errmsg   NVARCHAR (MAX);
   BEGIN
      SET NOCOUNT ON
      BEGIN TRY
         SELECT SR.ServiceRequestID,
                SR.VCN,
                SC.SubCatName AS MovementName,
                Format (SR.MovementDateTime, 'M/d/yyyy h:m:s tt') AS MovementDateTime,               
                Format (SR.CreatedDate, 'M/d/yyyy h:m:s tt')  AS SubmitDateTime,                
                V.VesselName AS VesselName,
                dbo.udf_GetWorkflowPreviousRemarks (SR.WorkflowInstanceID) as Comments,
                SR.CreatedBy
           FROM ServiceRequest SR
                INNER JOIN SubCategory SC ON SC.SubCatCode = SR.MovementType
                INNER JOIN ArrivalNotification AN ON AN.VCN = SR.VCN
                INNER JOIN Vessel V ON V.Vesselid = AN.vesselId
          WHERE SR.ServiceRequestID = @ServiceRequestID;         
      END TRY
      BEGIN CATCH
         SELECT @errmsg =
                     ' ErrorMessage = '
                   + ERROR_MESSAGE ()

         INSERT INTO [Log] (Date,
                            Thread,
                            Level,
                            Logger,
                            Message,
                            Exception)
         VALUES (getdate (),
                 9999,
                 N'ERROR',
                 N'Failed to execute usp_GetServiceRequestByID',
                 @errmsg,
                 N'')
      END CATCH
   END
GO


IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = 'ServiceRequest' AND  COLUMN_NAME = 'IsRecordingCompleted')
  ALTER TABLE [dbo].[ServiceRequest] DROP COLUMN [IsRecordingCompleted]
GO

IF OBJECT_ID (N'dbo.udf_GetRequestRecordingStatus') IS NOT NULL
   DROP FUNCTION [dbo].[udf_GetRequestRecordingStatus]
GO

CREATE FUNCTION [dbo].[udf_GetRequestRecordingStatus] (
   @p_ServiceRequestId    INT)
   RETURNS NVARCHAR (2)
   WITH
   EXEC AS CALLER,
   ENCRYPTION
AS
   BEGIN
      DECLARE @result   NVARCHAR (2)
      SET @result = 'N'
  SELECT @result= CASE WHEN vcm.ATB IS NOT NULL THEN 'Y' ELSE CASE WHEN (    VCM.MovementType = 'SGMV' AND VCM.ATUB IS NOT NULL) THEN 'Y' ELSE 'N' END END
  FROM VesselCallMovement VCM
 WHERE coalesce(VCM.ServiceRequestID, 0)=coalesce(@p_ServiceRequestId,0);
 return @result
   END
GO
ALTER TABLE [dbo].[ServiceRequest] ADD [IsRecordingCompleted] AS (dbo.udf_GetRequestRecordingStatus(ServiceRequestID))
GO
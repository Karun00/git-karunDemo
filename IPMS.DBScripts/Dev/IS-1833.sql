Update AutomatedSlotConfiguration set OperationalPeriod = '60', Duration = '120' where PortCode IN ('CT')
GO

Update AutomatedSlotConfiguration set OperationalPeriod = '0', Duration = '120' where PortCode IN ('DB', 'EL','MB','NG','SB')
GO

Update AutomatedSlotConfiguration set OperationalPeriod = '0', Duration = '90' where PortCode IN ('RB','PE')
GO


Insert Into SubCategory
(SubCatCode, SupCatCode, SubCatName, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES
('ROTR', 'RESN', 'Others', 'A', 2, getDate(), 2,  getDate())

GO


CREATE FUNCTION [dbo].[udf_GetCurrentSlotDateTime]
(@Port VARCHAR (50))
RETURNS datetime
WITH EXEC AS CALLER
AS
BEGIN
begin

DECLARE @curenttime   INT
DECLARE @Starttime   INT
 DECLARE @Duration   INT
 DECLARE @aCTSTART   INT
 declare @result datetime
   SET @Starttime =
             (select top 1 OperationalPeriod from AutomatedSlotConfiguration
             where PortCode = @Port order by EffectiveFrm desc)
 
      SET @Duration =
             (select top 1 duration from AutomatedSlotConfiguration
             where PortCode = @Port order by EffectiveFrm desc)
             
   SET  @curenttime = (SELECT DATEPART(hour,GETDATE())*60  + DATEPART(minute,GETDATE()) 'Total Minute Part' )
 
  IF @Starttime > @curenttime 
  BEGIN 
    SET @aCTSTART = @Starttime
END
ELSE
      BEGIN 
      WHILE (@Starttime < @curenttime)
      BEGIN
      DECLARE @TAB TABLE (ID INT IDENTITY(1,1),STARTTIME INT )
      INSERT INTO @TAB (STARTTIME) 
        select  @Starttime 
          SET @Starttime = @Starttime + @Duration
      END
     SET @aCTSTART = (select  STARTTIME FROM @TAB WHERE ID IN (SELECT MAX(id) FROM @TAB ))
      END


  select @result = DATEADD(MINUTE,@aCTSTART ,CONVERT(DATETIME,CONVERT(VARCHAR(10), GETDATE(), 112)));
  
    
    
    return @result
end
END
GO

GO


ALTER PROCEDURE [dbo].[usp_GetPendingMovementsForAllocation]
-- Add the parameters for the stored procedure here
@PortCode VARCHAR(4)
AS
BEGIN
SET NOCOUNT ON;

DECLARE @CurrentSlotDate datetime

SET @CurrentSlotDate = dbo.udf_GetCurrentSlotDateTime(@PortCode);

SELECT vcm.ServiceRequestID ,vcm.VCN, v.VesselName, VesselType, sr.MovementType ServiceRequest,
        Slot,SlotDate,SlotStatus, vc.ETB, vc.ETUB, sr.ServiceRequestID,
        ISNULL(LengthOverallInM,0) LengthOverAll, ISNULL(GrossRegisteredTonnageInMT,0) GrossRegisteredTonnage,-- SUB3.SubCatName CargoType, Quantity,
        SUB1.SubCatName MovementType
		,dbo.udf_GetArrivalReasonForVisit(vcm.VCN) AS ReasonForVisit 
		,an.AnyDangerousGoodsonBoard, ISNULL(curberth.BerthName,'NA') CurrentBerth, ISNULL(toberth.BerthName,'NA') ToBerth,
		sa.SubCatName SideAlongSide, an.Tidal TidalCondition, an.DaylightRestriction DayLightCondition
    FROM VesselCallMovement vcm 
        INNER JOIN  ServiceRequest sr on sr.ServiceRequestID = vcm.ServiceRequestID
        INNER JOIN ArrivalNotification an on an.VCN = sr.VCN
        INNER JOIN Vesselcall vc on vc.VCN = an.VCN        
        INNER JOIN Vessel v on v.VesselID = an.VesselID
        INNER JOIN SubCategory SUB1 on SUB1.SubCatCode  = sr.MovementType        
		INNER JOIN SubCategory sa on sa.SubCatCode  = sr.SideAlongSideCode
		LEFT JOIN Berth curberth on curberth.BerthCode=vc.FromPositionBerthCode and curberth.QuayCode = vc.FromPositionQuayCode and curberth.PortCode = @PortCode
		LEFT JOIN Berth toberth on toberth.BerthCode=vc.ToPositionBerthCode and toberth.QuayCode = vc.ToPositionQuayCode and toberth.PortCode = @PortCode
    WHERE VCM.RecordStatus = 'A' AND VCM.SlotStatus='CNFR' 
     AND  VCM.SerViceRequestID in    
    ( SELECT ServiceRequestID FROM VesselCallMovement WHERE ServiceRequestID  NOT IN(select  ServiceReferenceID From ResourceAllocation where ServiceReferenceType = 'VTSR' and RecordStatus = 'A') )
     AND an.PortCode= @PortCode AND slotdate between @CurrentSlotDate and DATEADD(hour,6,@CurrentSlotDate)	 
END
GO





IF EXISTS
      (SELECT *
         FROM sys.objects
        WHERE object_id = OBJECT_ID (N'[dbo].[usp_GetDockingVesselByID]')
              AND type IN (N'P'))
DROP PROCEDURE [dbo].[usp_GetDockingVesselByID]
GO
CREATE PROCEDURE [usp_GetDockingVesselByID]
@p_vesselId Int
WITH 
EXECUTE AS CALLER
AS
BEGIN SET  NOCOUNT ON;

select vn.VesselID, vn.VesselName, vn.IMONo, sb.SubCatName as  VesselType,
vn.LengthOverallInM,
vn.BeamInM, pb.PortName as PortOfRegistry, GrossRegisteredTonnageInMT VesselGRT
from Vessel vn
inner join SubCategory sb on vn.VesselType = sb.SubCatCode
inner join PortRegistry pb on vn.PortOfRegistry = pb.PortCode
where vn.VesselID = @p_vesselId and vn.RecordStatus = 'A'

                
END
GO

Update Entity set Tokens = 'VCN,VesselName,PortLimitIn,BreakWaterIn,BreakWaterOut,PortLimitOut,AnchorDropTime,AnchorAweighTime,BearingDistanceFromBreakWater,Reason' where EntityCode='VSLCALLANC'

INSERT INTO NotificationTemplate(NotificationTemplateCode, NotificationTemplateName, EntityID, WorkflowTaskCode, PortCode, IsEmail, EmailSubject,EmailTemplate, IsSMS, SMSTemplate, IsSysMessage, SysMessageTemplate, NotificationTemplateBase, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'CADT','Arrival / Departure Details', (select EntityId from Entity where EntityCode='VSLCALLANC'),'NEW', NULL,'Y','Arrival / Departure Details','<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Arrival / Departure with the below details are captured.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Arrival / Departure Details</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Port Limit In</td><td width="62%">%PortLimitIn%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Break Water In</td><td width="62%">%BreakWaterIn%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Break Water Out</td><td width="62%">%BreakWaterOut%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Port Limit Out</td><td width="62%">%PortLimitOut%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
,'Y','This is to inform you that your Arrival / Departure with the below details are captured for Vessel Name %VesselName% VCN %VCN%.','Y','This is to inform you that your Arrival / Departure with the below details are captured for Vessel Name %VesselName% VCN %VCN%','R','A',1,getDate(),1,getDate()

INSERT INTO NotificationPort(NotificationTemplateCode ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT 'CADT',Portcode,'A' as RecordStatus,1 as CreatedBy, getDate() as CreatedDate,1 as ModifiedBy,getDate() as ModifiedDate from Port;

INSERT INTO NotificationRole(NotificationTemplateCode, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) select  'CADT' as NotificationTemplateCode,Roleid,'A' as RecordStatus,1 as CreatedBy, getDate() as Createddate, 1 as ModifiedBy, getDate() as ModifiedDate from (select RoleID from Role where Rolecode in (select value from udf_SplitString('AGNT,TO,SVTC,OPCO,OPM,OU',',')))  A; 



INSERT INTO NotificationTemplate(NotificationTemplateCode, NotificationTemplateName, EntityID, WorkflowTaskCode, PortCode, IsEmail, EmailSubject,EmailTemplate, IsSMS, SMSTemplate, IsSysMessage, SysMessageTemplate, NotificationTemplateBase, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'CANH','Vessel Call Anchorage Details', (select EntityId from Entity where EntityCode='VSLCALLANC'),'WFSA', NULL,'Y','Vessel Call Anchorage Details','<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Vessel Call Anchorage with the below details are captured.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Vessel Call Anchorage Details</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Anchor Drop Time</td><td width="62%">%AnchorDropTime%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Anchor Aweigh Time</td><td width="62%">%AnchorAweighTime%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Bearing Distance</td><td width="62%">%BearingDistanceFromBreakWater%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Reason for Anchorage</td><td width="62%">%Reason%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>','Y','This is to inform you that your Vessel Call Anchorage with the below details are captured for Vessel Name %VesselName% VCN %VCN%.','Y','This is to inform you that your Vessel Call Anchorage with the below details are captured for Vessel Name %VesselName% VCN %VCN%','R','A',1,getDate(),1,getDate()

INSERT INTO NotificationPort(NotificationTemplateCode ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT 'CANH',Portcode,'A' as RecordStatus,1 as CreatedBy, getDate() as CreatedDate,1 as ModifiedBy,getDate() as ModifiedDate from Port;

INSERT INTO NotificationRole(NotificationTemplateCode, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) select  'CANH' as NotificationTemplateCode,Roleid,'A' as RecordStatus,1 as CreatedBy, getDate() as Createddate, 1 as ModifiedBy, getDate() as ModifiedDate from (select RoleID from Role where Rolecode in (select value from udf_SplitString('AGNT,TO,SVTC,OPCO,OPM,OU',',')))  A;  

GO


ALTER PROCEDURE [dbo].[usp_AnchorageData_dml] (
   @AnchorageId       INT OUTPUT,
   @PORTCODE           NVARCHAR(2) OUTPUT,
   @PortId            NVARCHAR (5),
   @MMSI              NVARCHAR (10),
   @Area              NVARCHAR (50),
   @Assignment        NVARCHAR (25),
   @DateTime          DATETIME,
   @IMONo             NVARCHAR (15),
   @CallSign          NVARCHAR (200),
   @VesselName        NVARCHAR (50))
AS
   BEGIN
      INSERT INTO AnchorageData (PortId,
                                 MMSI,
                                 Area,
                                 Assignment,
                                 DateTime,
                                 IMONo,
                                 CallSign,
                                 VesselName)
      VALUES (@PortId,
              @MMSI,
              @Area,
              @Assignment,
              @DateTime,
              @IMONo,
              @CallSign,
              @VesselName)
   END
   
   
    DECLARE @VCN AS  nvarchar(12)
 --DECLARE @PORTCODE AS  nvarchar(2)
 --DECLARE @ATACONFIG  AS  nvarchar(200)
 DECLARE @ANCHORID AS INT


  SET @PORTCODE =  ( SELECT P.PortCode FROM PortRegistry PR  
                    inner join Port P on   RTRIM(LTRIM(UPPER(P.PortName))) = RTRIM(LTRIM(UPPER(PR.PortName)))
                    where CONCAT(PR.CountryCode,PR.InternationalPortCode)= @PortId );
  /*
   SET @ATACONFIG =  (  SELECT ConfigValue FROM PortGeneralConfig 
                      WHERE Configname = 'ATA/ATD Configuration'
                      and PortCode =@PORTCODE );
     */

SET @VCN =  ( SELECT TOP(1) WITH TIES AN.VCN FROM  ArrivalNotification AN
                inner join Vessel V on AN.VesselID = V.VesselID
                inner join VesselCall VC on AN.VCN = VC.VCN
                where AN.RecordStatus = 'A' and V.MMSINumber = @mmsi and AN.PortCode =@PORTCODE
                order by AN.NominationDate desc );
                
    IF( RTRIM(LTRIM(UPPER(@Assignment))) = 'ANCHORED')
       BEGIN
        
       insert into VesselCallAnchorage (VCN,AnchorDropTime,AnchorPosition,Remarks ,BearingDistanceFromBreakWater,
                                        RecordStatus  ,CreatedBy  ,CreatedDate  ,ModifiedBy  ,ModifiedDate) 
                                Values (@VCN,@DateTime,@Assignment,'NA', 1,
                                        'A', 1, GETDATE(), 1, GETDATE() )
       END
    ELSE
       BEGIN
      
              SET @ANCHORID = (SELECT TOP(1) VesselCallAnchorageid FROM (       
                                SELECT V.VesselCallAnchorageid , V.VCN, V.AnchorDropTime, 
                                    DATEDIFF(mi, V.AnchorDropTime  ,@DateTime ) as minets
                                from VesselCallAnchorage V where V.vcn =@VCN
                                and V.AnchorAweighTime is null AND V.AnchorDropTime < @DateTime
                                )T  WHERE minets > 0 ORDER BY minets ASC)
             
              UPDATE VesselCallAnchorage 
                      SET AnchorAweighTime= @DateTime, ModifiedBy = 1, ModifiedDate=GETDATE()
                      WHERE VesselCallAnchorageID = @ANCHORID
                  
       END
   SELECT @AnchorageId = SCOPE_IDENTITY ();   
   SELECT @PORTCODE;
   
   RETURN;
GO



ALTER PROCEDURE [dbo].[usp_PortLimitData_dml] (
   @PortlimitId       INT OUTPUT,   
   @PORTCODE    NVARCHAR(2) OUTPUT,
   @VCN         NVARCHAR(12) OUTPUT,
   @PortId            NVARCHAR (5),
   @mmsi              NVARCHAR (10),
   @area              NVARCHAR (50),
   @movement          NVARCHAR (25),
   @datetime          DATETIME,
   @IMONo             NVARCHAR (15),
   @callsign          NVARCHAR (200),
   @name              NVARCHAR (50))
AS
   BEGIN
      INSERT INTO PortLimitData (PortId,
                                 mmsi,
                                 area,
                                 movement,
                                 datetime,
                                 IMONo,
                                 callsign,
                                 name)
      VALUES (@PortId,
              @mmsi,
              @area,
              @movement,
              @datetime,
              @IMONo,
              @callsign,
              @name)
              
 --DECLARE @VCN AS  nvarchar(12)
--DECLARE @PORTCODE AS  nvarchar(2)
DECLARE @ATACONFIG  AS  nvarchar(200)
DECLARE @DynamicQuery AS NVARCHAR(MAX)  
  
  SET @PORTCODE =  ( SELECT P.PortCode FROM PortRegistry PR  
                    inner join Port P on P.PortName = PR.PortName
                    where CONCAT(PR.CountryCode,PR.InternationalPortCode)= @PortId );
  
   SET @ATACONFIG =  (  SELECT ConfigValue FROM PortGeneralConfig 
                      WHERE Configname = 'ATA/ATD Configuration'
                      and PortCode =@PORTCODE );
     

SET @VCN =  ( SELECT TOP(1) WITH TIES AN.VCN FROM  ArrivalNotification AN
                inner join Vessel V on AN.VesselID = V.VesselID
                inner join VesselCall VC on AN.VCN = VC.VCN
                where AN.RecordStatus = 'A' and V.MMSINumber = @mmsi and AN.PortCode =@PORTCODE
                order by AN.NominationDate desc );




          if (@movement = 'P_Arrival') 
              begin
                    update VesselCall SET PortLimitIn =  @datetime WHERE VCN = @VCN and PortLimitIn is null;
              end       
          else if(@movement = 'P_Sailing') 
              begin
                    update VesselCall SET PortLimitOut =  @datetime WHERE VCN = @VCN and BreakWaterOut is not null;
                 
              end 
          else if(@movement = 'B_Arrival')
              begin 
                    update VesselCall SET BreakWaterIn =  @datetime WHERE VCN = @VCN and BreakWaterIn is null;
              end        
          else     
              begin
                    update VesselCall SET BreakWaterOut =  @datetime WHERE VCN = @VCN;    
              end
          end ; 
      
      
      
          if(@movement = 'P_Arrival' and @ATACONFIG = 'PortLimit') 
              begin
                   update VesselCall SET ATA =  @datetime WHERE VCN = @VCN and ATA is null;
              end       
          else if(@movement = 'B_Arrival' and @ATACONFIG != 'PortLimit') 
              begin
                    update VesselCall SET ATA =  @datetime WHERE VCN = @VCN and ATA is null;
              end 
          else if(@movement = 'P_Sailing' and @ATACONFIG = 'PortLimit')
              begin 
                    update VesselCall SET ATD =  @datetime WHERE VCN = @VCN and BreakWaterOut is not null;
              end        
           else if(@movement = 'B_Sailing' and @ATACONFIG != 'PortLimit') 
              begin
                    update VesselCall SET ATD =  @datetime WHERE VCN = @VCN;
              end
       
        SELECT @PortlimitId = SCOPE_IDENTITY ();
        SELECT @PORTCODE;
        SELECT @VCN;
        
        RETURN;
GO



Update Entity set Tokens = 'VCN,VesselName,PortLimitIn,BreakWaterIn,BreakWaterOut,PortLimitOut,AnchorDropDateTime,AnchorAweighDateTime,BearingDistanceFromBreakWater,Reason' where EntityCode='VSLCALLANC'

Update NotificationTemplate set EmailTemplate = '<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Vessel Call Anchorage with the below details are captured.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Vessel Call Anchorage Details</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Anchor Drop Time</td><td width="62%">%AnchorDropDateTime%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Anchor Aweigh Time</td><td width="62%">%AnchorAweighDateTime%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Bearing Distance</td><td width="62%">%BearingDistanceFromBreakWater%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Reason for Anchorage</td><td width="62%">%Reason%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
where NotificationTemplateCode = 'CANH'


Update Entity set Tokens = 'VCN,VesselName,PilotOnBoard,PilotOff,FirstLineIn,LastLineIn,FirstLineOut,LastLineOut' where EntityCode='SERVICERECORDING'



INSERT INTO NotificationTemplate(NotificationTemplateCode, NotificationTemplateName, EntityID, WorkflowTaskCode, PortCode, IsEmail, EmailSubject,EmailTemplate, IsSMS, SMSTemplate, IsSysMessage, SysMessageTemplate, NotificationTemplateBase, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'SRBA','Service Recording Details', (select EntityId from Entity where EntityCode='SERVICERECORDING'),'WFSA', NULL,'Y','Service Recording Details - Arrival','<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Service Recording with the below details are captured for Arrival.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Service Recording Details - Arrival</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">First Line In</td><td width="62%">%FirstLineIn%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Last Line In</td><td width="62%">%LastLineIn%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
,'Y','This is to inform you that your Service Recording - Arrival details are captured for Vessel Name %VesselName% VCN %VCN%.','Y','This is to inform you that your Service Recording - Arrival details are captured for Vessel Name %VesselName% VCN %VCN%','R','A',1,getDate(),1,getDate()

INSERT INTO NotificationPort(NotificationTemplateCode ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT 'SRBA',Portcode,'A' as RecordStatus,1 as CreatedBy, getDate() as CreatedDate,1 as ModifiedBy,getDate() as ModifiedDate from Port;

INSERT INTO NotificationRole(NotificationTemplateCode, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) SELECT  'SRBA' as NotificationTemplateCode,Roleid,'A' as RecordStatus,1 as CreatedBy, getDate() as Createddate, 1 as ModifiedBy, getDate() as ModifiedDate from (select RoleID from Role where Rolecode in (select value from udf_SplitString('AGNT,TO,SVTC,OPCO,OPM,OU',',')))  A; 

---

INSERT INTO NotificationTemplate(NotificationTemplateCode, NotificationTemplateName, EntityID, WorkflowTaskCode, PortCode, IsEmail, EmailSubject,EmailTemplate, IsSMS, SMSTemplate, IsSysMessage, SysMessageTemplate, NotificationTemplateBase, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'SRBS','Service Recording Details', (select EntityId from Entity where EntityCode='SERVICERECORDING'),'WFVE', NULL,'Y','Service Recording Details - Shifting','<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Service Recording with the below details are captured for Shifting.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Service Recording Details - Shifting</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">First Line In</td><td width="62%">%FirstLineIn%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Last Line In</td><td width="62%">%LastLineIn%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">First Line Off</td><td width="62%">%FirstLineOut%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Last Line Off</td><td width="62%">%LastLineOut%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
,'Y','This is to inform you that your Service Recording - Shifting details are captured for Vessel Name %VesselName% VCN %VCN%.','Y','This is to inform you that your Service Recording - Shifting details are captured for Vessel Name %VesselName% VCN %VCN%','R','A',1,getDate(),1,getDate()

INSERT INTO NotificationPort(NotificationTemplateCode ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT 'SRBS',Portcode,'A' as RecordStatus,1 as CreatedBy, getDate() as CreatedDate,1 as ModifiedBy,getDate() as ModifiedDate from Port;

INSERT INTO NotificationRole(NotificationTemplateCode, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) SELECT  'SRBS' as NotificationTemplateCode,Roleid,'A' as RecordStatus,1 as CreatedBy, getDate() as Createddate, 1 as ModifiedBy, getDate() as ModifiedDate from (select RoleID from Role where Rolecode in (select value from udf_SplitString('AGNT,TO,SVTC,OPCO,OPM,OU',',')))  A; 

----


INSERT INTO NotificationTemplate(NotificationTemplateCode, NotificationTemplateName, EntityID, WorkflowTaskCode, PortCode, IsEmail, EmailSubject,EmailTemplate, IsSMS, SMSTemplate, IsSysMessage, SysMessageTemplate, NotificationTemplateBase, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'SRBW','Service Recording Details', (select EntityId from Entity where EntityCode='SERVICERECORDING'),'WFCO', NULL,'Y','Service Recording Details - Warping','<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Service Recording with the below details are captured for Warping.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Service Recording Details - Warping</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">First Line In</td><td width="62%">%FirstLineIn%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Last Line In</td><td width="62%">%LastLineIn%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">First Line Off</td><td width="62%">%FirstLineOut%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Last Line Off</td><td width="62%">%LastLineOut%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
,'Y','This is to inform you that your Service Recording - Warping details are captured for Vessel Name %VesselName% VCN %VCN%.','Y','This is to inform you that your Service Recording - Warping details are captured for Vessel Name %VesselName% VCN %VCN%','R','A',1,getDate(),1,getDate()

INSERT INTO NotificationPort(NotificationTemplateCode ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT 'SRBW',Portcode,'A' as RecordStatus,1 as CreatedBy, getDate() as CreatedDate,1 as ModifiedBy,getDate() as ModifiedDate from Port;

INSERT INTO NotificationRole(NotificationTemplateCode, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) SELECT  'SRBW' as NotificationTemplateCode,Roleid,'A' as RecordStatus,1 as CreatedBy, getDate() as Createddate, 1 as ModifiedBy, getDate() as ModifiedDate from (select RoleID from Role where Rolecode in (select value from udf_SplitString('AGNT,TO,SVTC,OPCO,OPM,OU',',')))  A; 


------

INSERT INTO NotificationTemplate(NotificationTemplateCode, NotificationTemplateName, EntityID, WorkflowTaskCode, PortCode, IsEmail, EmailSubject,EmailTemplate, IsSMS, SMSTemplate, IsSysMessage, SysMessageTemplate, NotificationTemplateBase, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'SRBL','Service Recording Details', (select EntityId from Entity where EntityCode='SERVICERECORDING'),'VUPD', NULL,'Y','Service Recording Details - Sailing','<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Service Recording with the below details are captured for Sailing.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Service Recording Details - Sailing</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">First Line Off</td><td width="62%">%FirstLineOut%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Last Line Off</td><td width="62%">%LastLineOut%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
,'Y','This is to inform you that your Service Recording - Sailing details are captured for Vessel Name %VesselName% VCN %VCN%.','Y','This is to inform you that your Service Recording - Sailinge details are captured for Vessel Name %VesselName% VCN %VCN%','R','A',1,getDate(),1,getDate()

INSERT INTO NotificationPort(NotificationTemplateCode ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT 'SRBL',Portcode,'A' as RecordStatus,1 as CreatedBy, getDate() as CreatedDate,1 as ModifiedBy,getDate() as ModifiedDate from Port;

INSERT INTO NotificationRole(NotificationTemplateCode, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) SELECT  'SRBL' as NotificationTemplateCode,Roleid,'A' as RecordStatus,1 as CreatedBy, getDate() as Createddate, 1 as ModifiedBy, getDate() as ModifiedDate from (select RoleID from Role where Rolecode in (select value from udf_SplitString('AGNT,TO,SVTC,OPCO,OPM,OU',',')))  A; 




INSERT INTO NotificationTemplate(NotificationTemplateCode, NotificationTemplateName, EntityID, WorkflowTaskCode, PortCode, IsEmail, EmailSubject,EmailTemplate, IsSMS, SMSTemplate, IsSysMessage, SysMessageTemplate, NotificationTemplateBase, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'SRPA','Pilotage Service Recording Details', (select EntityId from Entity where EntityCode='SERVICERECORDING'),'NEW', NULL,'Y','Pilotage Service Recording - Arrival','<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Pilotage Service Recording with the below details are captured for Arrival.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Pilotage Service Recording Details</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Pilot On Board</td><td width="62%">%PilotOnBoard%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Pilot Off Board</td><td width="62%">%PilotOff%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
,'Y','This is to inform you that your Pilotage Service Recording details are captured for Vessel Name %VesselName% VCN %VCN%.','Y','This is to inform you that your Pilotage Service Recording details are captured for Vessel Name %VesselName% VCN %VCN%.','R','A',1,getDate(),1,getDate()


INSERT INTO NotificationPort(NotificationTemplateCode ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT 'SRPA',Portcode,'A' as RecordStatus,1 as CreatedBy, getDate() as CreatedDate,1 as ModifiedBy,getDate() as ModifiedDate from Port;

INSERT INTO NotificationRole(NotificationTemplateCode, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) SELECT  'SRPA' as NotificationTemplateCode,Roleid,'A' as RecordStatus,1 as CreatedBy, getDate() as Createddate, 1 as ModifiedBy, getDate() as ModifiedDate from (select RoleID from Role where Rolecode in (select value from udf_SplitString('AGNT,TO,SVTC,OPCO,OPM,OU',',')))  A; 


INSERT INTO NotificationTemplate(NotificationTemplateCode, NotificationTemplateName, EntityID, WorkflowTaskCode, PortCode, IsEmail, EmailSubject,EmailTemplate, IsSMS, SMSTemplate, IsSysMessage, SysMessageTemplate, NotificationTemplateBase, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT 'SRPS','Pilotage Service Recording Details', (select EntityId from Entity where EntityCode='SERVICERECORDING'),'VRES', NULL,'Y','Pilotage Service Recording - Sailing','<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Pilotage Service Recording with the below details are captured for Sailing.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Pilotage Service Recording Details</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Pilot On Board</td><td width="62%">%PilotOnBoard%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Pilot Off Board</td><td width="62%">%PilotOff%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
,'Y','This is to inform you that your Pilotage Service Recording details are captured for Vessel Name %VesselName% VCN %VCN%.','Y','This is to inform you that your Pilotage Service Recording details are captured for Vessel Name %VesselName% VCN %VCN%.','R','A',1,getDate(),1,getDate()

INSERT INTO NotificationPort(NotificationTemplateCode ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT 'SRPS',Portcode,'A' as RecordStatus,1 as CreatedBy, getDate() as CreatedDate,1 as ModifiedBy,getDate() as ModifiedDate from Port;

INSERT INTO NotificationRole(NotificationTemplateCode, RoleID, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) SELECT  'SRPS' as NotificationTemplateCode,Roleid,'A' as RecordStatus,1 as CreatedBy, getDate() as Createddate, 1 as ModifiedBy, getDate() as ModifiedDate from (select RoleID from Role where Rolecode in (select value from udf_SplitString('AGNT,TO,SVTC,OPCO,OPM,OU',',')))  A; 










ALTER TABLE ExternalDivingRegister 
DROP CONSTRAINT FK_ExternalDivingRegister_LocationID
GO

DROP INDEX ExternalDivingRegister.IDX_ExternalDivingRegister_LocationID;

GO
ALTER TABLE ExternalDivingRegister 
DROP COLUMN LocationID
GO

ALTER TABLE dbo.ExternalDivingRegister
ADD [PortCode] [nvarchar](2) NOT NULL;
GO

ALTER TABLE dbo.ExternalDivingRegister
ADD [QuayCode] [nvarchar](4) NOT NULL;
GO

ALTER TABLE dbo.ExternalDivingRegister
ADD [BerthCode] [nvarchar](4) NOT NULL;
GO

ALTER TABLE [dbo].[ExternalDivingRegister]  WITH CHECK ADD  CONSTRAINT [FK_ExternalDivingRegister_PortCodeQuayCodeBerthCode] FOREIGN KEY([PortCode], [QuayCode], [BerthCode])
REFERENCES [dbo].[Berth] ([PortCode], [QuayCode], [BerthCode])
GO

ALTER TABLE ExternalDivingRegister 
ALTER COLUMN CompanyName	int NOT NULL;
GO

Update Entity set Tokens = 'PortCode,CompanyNameDisplay,BerthName,VesselName,StartTime,StopTime,PermissionObtained,PortName'
where EntityCode = 'EXDREG'
GO

Update NotificationTemplate set EmailTemplate = '<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">This is to inform you that your Diving Operation has been received with below details.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Diving Task Completion Details(External)</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="46%">External Company</td><td width="62%">%CompanyNameDisplay%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Dive Location</td><td width="62%">%BerthName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Start Time</td><td width="62%">%StartTime%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Stop Time</td><td width="62%">%StopTime%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Permission Obtained from SVTC</td><td width="62%">%PermissionObtained%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>' ,
SMSTemplate = 'This is to inform you that Diving operation has been successfully completed near Dive Location: %BerthName%  by External company name: %CompanyNameDisplay% at End date/time: %StopTime%,IPMS Admin', SysMessageTemplate = 'This is to inform you that Diving Operation has been successfully completed. Regards,IPMS Admin'   where NotificationTemplateCode = 'EXCP' 

GO
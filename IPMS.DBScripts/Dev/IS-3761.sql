 GO
 insert into Entity(EntityCode,Moduleid,Entityname,PageUrl,OrderNo,Tokens,HasWorkflow,HasMenuItem,RecordStatus,CreatedBy,CreatedDate,
  ModifiedBy,ModifiedDate,PendingTaskColumns,ControllerName)
  values('REPWSTDCLN',32,'Waste Declaration Report','Report/WasteDeclarationReport',20,null,'N','Y','A',2,getdate(),2,getdate(),null,null)

  GO
 insert into EntityPrivilege(EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
 values(200,'ADD','A',1,getdate(),1,getdate())

 GO
 insert into EntityPrivilege(EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
 values(200,'DEL','A',1,getdate(),1,getdate())

 GO
 insert into EntityPrivilege(EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
 values(200,'EDIT','A',1,getdate(),1,getdate())

 GO
 insert into EntityPrivilege(EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
 values(200,'VIEW','A',1,getdate(),1,getdate())


 GO 
 insert into RolePrivilege(RoleID,EntityID,SubCatCode,RecordStatus,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)
 values(1,200,'VIEW','A',1,getdate(),1,getdate())

GO
Create PROCEDURE [dbo].[usp_rpt_WasteDeclaration]
   @Port VARCHAR (50), @FromDate DATETIME, @ToDate DATETIME
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
    
    SELECT DISTINCT 
                         an.VCN, v.VesselName, a.RegisteredName AS 'Vessel Agent Name', an.ETA, an.ETD, b.BerthName, s.SubCatName AS 'Marpol', m.ClassName AS 'Class', 
                         l.RegisteredName AS 'Service Provider', w.Quantity, an.CreatedDate
FROM            ArrivalNotification AS an INNER JOIN
                         Vessel AS v ON an.VesselID = v.VesselID INNER JOIN
                         WasteDeclaration AS w ON an.VCN = w.VCN INNER JOIN
                         Agent AS a ON an.AgentID = a.AgentID INNER JOIN
                         Berth AS b ON an.PreferredBerthCode = b.BerthCode INNER JOIN
                         Marpol AS m ON w.ClassCode = m.ClassCode INNER JOIN
                         SubCategory AS s ON m.MarpolCode = s.SubCatCode INNER JOIN
                         LicenseRequest AS l ON w.LicenseRequestID = l.LicenseRequestID
WHERE                (an.RecordStatus = 'A') AND (an.Isdraft <> 'Y' OR an.Isdraft IS NULL)
                   AND  (an.PortCode = @Port OR @Port IS NULL)       
                       AND (cast(an.ETA as datetime) BETWEEN @FromDate and @ToDate)
					   -- or cast(an.ETD as datetime) BETWEEN @FromDate and @ToDate)  
						and an.IsANFinal='Y'
					   order by ETA desc

--exec [dbo].[usp_rpt_WasteDeclaration] @Port=null,@FromDate='2018-08-13 17:36:16.720',@ToDate='2018-08-14 17:36:16.720'
 
 END

  

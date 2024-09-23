GO
ALTER PROCEDURE [dbo].[usp_RevenueVTSRDues]
@portcode nvarchar(2),
@VCN nvarchar(15)
WITH 
EXECUTE AS CALLER
AS
BEGIN

 DECLARE @postedon AS datetime
 DECLARE @postedBerthdueson AS datetime
 DECLARE @postedRefuseRemovalon AS datetime 
  DECLARE @SAPACCOUNT AS  nvarchar(16)
  DECLARE @SAPACCOUNTBerth AS  nvarchar(16)
  DECLARE @SAPACCOUNTRefuse AS  nvarchar(16)
 DECLARE @Dry12postedon AS datetime
  DECLARE @Dry12SAPACCOUNT AS  nvarchar(16)
DECLARE @cntAll AS int
 DECLARE @cntBnk AS int
 DECLARE @BERTDUEVCN AS  nvarchar(15)

SET @cntAll = ( SELECT count(ANN.VCN) FROM ArrivalNotification ANN
                    inner join dbo.ArrivalReason ANR ON ANR.VCN = ANN.VCN
                    INNER JOIN SubCategory ASB ON ANR.Reason = ASB.SubCatCode
                    WHERE ANN.VCN NOT IN (
                    select An.VCN
                    from ArrivalNotification An
                    inner join dbo.ArrivalReason AR ON AR.VCN = An.VCN
                    INNER JOIN SubCategory SB ON AR.Reason = SB.SubCatCode
                    WHERE 
                     An.VCN = @VCN AND SB.SubCatCode  IN (
                                    SELECT SubCatCode FROM SubCategory WHERE SupCatCode = 'RSV'
                                     AND SubCatCode NOT IN ('LABY','BUNK','REPA'))
                    ) AND  ANN.VCN  = @VCN group by  ANN.VCN )

SET @cntBnk = ( SELECT count(ANN.VCN) FROM ArrivalNotification ANN
                    inner join dbo.ArrivalReason ANR ON ANR.VCN = ANN.VCN
                    INNER JOIN SubCategory ASB ON ANR.Reason = ASB.SubCatCode
                    WHERE ANN.VCN NOT IN (
                    select An.VCN
                    from ArrivalNotification An
                    inner join dbo.ArrivalReason AR ON AR.VCN = An.VCN
                    INNER JOIN SubCategory SB ON AR.Reason = SB.SubCatCode
                    WHERE 
                    An.VCN  = @VCN
                    AND SB.SubCatCode  IN (
                    SELECT SubCatCode FROM SubCategory WHERE SupCatCode = 'RSV'
                    AND SubCatCode NOT IN ('BUNK')
                    )
                    )
                    AND
                     ANN.VCN  = @VCN 
                    group by  ANN.VCN
                    )

 if @cntAll > 0 
 BEGIN
     SET @BERTDUEVCN = @VCN
     
    if @cntAll = 1 and @cntBnk =1 
    begin
    SET @cntBnk = 2
    end
    else
    SET @cntBnk = 0

  END
else
BEGIN
    SET @cntBnk = 0
    SET @BERTDUEVCN = ''
END

SET @postedon =  ( select top(1)  dateadd(mi,+1,RD.PostedOn )
           from RevenuePostingDtl RD
           inner join RevenuePosting RH on RH.RevenuePostingID = RD.RevenuePostingID
           inner join ArrivalNotification an on an.VCN = RH.vcn
           inner join MaterialCodePort mp on mp.PortCode = an.PortCode
           inner join MaterialCodeMaster mc on mc.MaterialCodeMasterid = mp.MaterialCodeMasterid
            AND RD.MaterialCode = mc.MaterialCode AND RD.GroupCode = mc.GroupCode
            where  mc.ChargedFor ='PODU'
                  and RH.vcn = @VCN  order by RH.RevenuePostingID desc );

SET @postedBerthdueson =  ( select top(1)  dateadd(mi,+1,RD.PostedOn )
           from RevenuePostingDtl RD
           inner join RevenuePosting RH on RH.RevenuePostingID = RD.RevenuePostingID
           inner join ArrivalNotification an on an.VCN = RH.vcn
           inner join MaterialCodePort mp on mp.PortCode = an.PortCode
           inner join MaterialCodeMaster mc on mc.MaterialCodeMasterid = mp.MaterialCodeMasterid
            AND RD.MaterialCode = mc.MaterialCode AND RD.GroupCode = mc.GroupCode
            where  mc.ChargedFor ='BRTH'
                  and RH.vcn = @VCN  order by RH.RevenuePostingID desc );
				  
SET @postedRefuseRemovalon =  ( select top(1)  dateadd(mi,+1,RD.PostedOn )
           from RevenuePostingDtl RD
           inner join RevenuePosting RH on RH.RevenuePostingID = RD.RevenuePostingID
           inner join ArrivalNotification an on an.VCN = RH.vcn
           inner join MaterialCodePort mp on mp.PortCode = an.PortCode
           inner join MaterialCodeMaster mc on mc.MaterialCodeMasterid = mp.MaterialCodeMasterid
            AND RD.MaterialCode = mc.MaterialCode AND RD.GroupCode = mc.GroupCode
            where  mc.ChargedFor ='REFU'
                  and RH.vcn = @VCN  order by RH.RevenuePostingID desc );

SET @SAPACCOUNT =  ( select top(1)  RH.SAPAccNo 
            from RevenuePostingDtl RD
               inner join RevenuePosting RH on RH.RevenuePostingID = RD.RevenuePostingID
           inner join ArrivalNotification an on an.VCN = RH.vcn
           inner join MaterialCodePort mp on mp.PortCode = an.PortCode
           inner join MaterialCodeMaster mc on mc.MaterialCodeMasterid = mp.MaterialCodeMasterid
            AND RD.MaterialCode = mc.MaterialCode AND RD.GroupCode = mc.GroupCode
            where  mc.ChargedFor ='PODU'
                  and RH.vcn = @VCN  order by RH.RevenuePostingID desc );
SET @SAPACCOUNTBerth =  ( select top(1)  RH.SAPAccNo 
            from RevenuePostingDtl RD
               inner join RevenuePosting RH on RH.RevenuePostingID = RD.RevenuePostingID
           inner join ArrivalNotification an on an.VCN = RH.vcn
           inner join MaterialCodePort mp on mp.PortCode = an.PortCode
           inner join MaterialCodeMaster mc on mc.MaterialCodeMasterid = mp.MaterialCodeMasterid
            AND RD.MaterialCode = mc.MaterialCode AND RD.GroupCode = mc.GroupCode
            where  mc.ChargedFor ='BRTH'
                  and RH.vcn = @VCN  order by RH.RevenuePostingID desc );
 SET @SAPACCOUNTRefuse =  ( select top(1)  RH.SAPAccNo 
            from RevenuePostingDtl RD
               inner join RevenuePosting RH on RH.RevenuePostingID = RD.RevenuePostingID
           inner join ArrivalNotification an on an.VCN = RH.vcn
           inner join MaterialCodePort mp on mp.PortCode = an.PortCode
           inner join MaterialCodeMaster mc on mc.MaterialCodeMasterid = mp.MaterialCodeMasterid
            AND RD.MaterialCode = mc.MaterialCode AND RD.GroupCode = mc.GroupCode
            where  mc.ChargedFor ='REFU'
                  and RH.vcn = @VCN  order by RH.RevenuePostingID desc );



 

DECLARE @postednewdate AS datetime
set @postednewdate = (select getdate());
--set @postednewdate = (select CAST(CAST(getdate() AS Date) As datetime));
                
                  
SET @Dry12postedon =( select top(1)  RPD.PostedOn from SuppDryDock SD
                          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                                     and M.ChargedFor = 'DR12'
                          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                          inner join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                    and rpd.GroupCode = M.GroupCode and rpd.MaterialCode = M.MaterialCode    
                          inner JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                     and RPD.VCN = SD.VCN
                          WHERE  SD.VCN  = @VCN and MP.PortCode = @portcode order by RPH.RevenuePostingID desc)
                   
SET @Dry12SAPACCOUNT =( select top(1)  RPH.SAPAccNo from SuppDryDock SD
                         inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                                     and M.ChargedFor = 'DR12'
                          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                          inner join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                    and rpd.GroupCode = M.GroupCode and rpd.MaterialCode = M.MaterialCode    
                          inner JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                     and RPD.VCN = SD.VCN
                          WHERE  SD.VCN  = @VCN and MP.PortCode = @portcode order by RPH.RevenuePostingID desc)
               
      -- FOR PORT DUES VIEWS ONLYY       
              
             select  '0' ISPOSTED , RevenuePostingDtlID ResourceAllocationID,V.VCN,
              'PORTDUESVIEW' as MovementName,'PORT DUES' as ServiceName,
              M.GroupCode GroupCode,M.MaterialCode MaterialCode,
              upper(M.MaterialDescription) MaterialDescription ,
                    RH.SAPAccNo AS AccountNo,
                     RH.CreatedDate as StartTime,
                     V.BreakWaterOut  as Endtime,'N' as IsCalculated, M.Chargedas as Chargedas,M.UOM ,
                     '' as MovementType,'' as ServiceType,
                     '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                    rd.PostingFrom  RecentlyPostedDate,
                    RD.PostedOn PostingDateTime,
                    REPLACE(RD.Units ,',','.')   AS 'DueHours',
               REPLACE(RD.Units ,',','.') AS 'TotalHours',
              '0' CloseMterReding, 
              '0' as startmtrreding,
              '' MeterSerialNo, '' BerthName
              from VesselCall V 
              inner join ArrivalNotification an on an.VCN = V.VCN
              INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
              INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='PODU'
              INNER JOIN RevenuePostingDtl RD on RD.GroupCode = M.GroupCode and RD.MaterialCode = M.MaterialCode
                                              and an.VCN = RD.VCN
              inner join RevenuePosting RH on RH.RevenuePostingID = RD.RevenuePostingID and RH.vcn = an.VCN
              where  V.VCN  = @VCN  
      ----
	  UNION
	 -- FOR BERTH DUES VIEWS ONLYY   
	             select  '0' ISPOSTED , RevenuePostingDtlID ResourceAllocationID,V.VCN,
              'PORTDUESVIEW' as MovementName,'BERTH DUES' as ServiceName,
              M.GroupCode GroupCode,M.MaterialCode MaterialCode,
              upper(M.MaterialDescription) MaterialDescription ,
                    RH.SAPAccNo AS AccountNo,
                     RH.CreatedDate as StartTime,
                     V.BreakWaterOut  as Endtime,'N' as IsCalculated, M.Chargedas as Chargedas,M.UOM ,
                     '' as MovementType,'' as ServiceType,
                     '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                    rd.PostingFrom  RecentlyPostedDate,
                    RD.PostedOn PostingDateTime,
                    REPLACE(RD.Units ,',','.')   AS 'DueHours',
               REPLACE(RD.Units ,',','.') AS 'TotalHours',
              '0' CloseMterReding, 
              '0' as startmtrreding,
              '' MeterSerialNo, '' BerthName
              from VesselCall V 
              inner join ArrivalNotification an on an.VCN = V.VCN
              INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
              INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='BRTH'
              INNER JOIN RevenuePostingDtl RD on RD.GroupCode = M.GroupCode and RD.MaterialCode = M.MaterialCode
                                              and an.VCN = RD.VCN
              inner join RevenuePosting RH on RH.RevenuePostingID = RD.RevenuePostingID and RH.vcn = an.VCN
              where  V.VCN  = @VCN 
			  
 --For Refuse Removal View only 
			  union
			   select  '0' ISPOSTED , RevenuePostingDtlID ResourceAllocationID,V.VCN,
              'PORTDUESVIEW' as MovementName,'REFUSE REMOVE' as ServiceName,
              M.GroupCode GroupCode,M.MaterialCode MaterialCode,
              upper(M.MaterialDescription) MaterialDescription ,
                    RH.SAPAccNo AS AccountNo,
                     RH.CreatedDate as StartTime,
                     V.BreakWaterOut  as Endtime,'N' as IsCalculated, M.Chargedas as Chargedas,M.UOM ,
                     '' as MovementType,'' as ServiceType,
                     '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                    rd.PostingFrom  RecentlyPostedDate,
                    RD.PostedOn PostingDateTime,
                    REPLACE(RD.Units ,',','.')   AS 'DueHours',
               REPLACE(RD.Units ,',','.') AS 'TotalHours',
              '0' CloseMterReding, 
              '0' as startmtrreding,
              '' MeterSerialNo, '' BerthName
              from VesselCall V 
              inner join ArrivalNotification an on an.VCN = V.VCN
              INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
              INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='REFU'
              INNER JOIN RevenuePostingDtl RD on RD.GroupCode = M.GroupCode and RD.MaterialCode = M.MaterialCode
                                              and an.VCN = RD.VCN
              inner join RevenuePosting RH on RH.RevenuePostingID = RD.RevenuePostingID and RH.vcn = an.VCN
              where  V.VCN  = @VCN 
			  -----
			 

              
       union        
               
      -- FOR PORT DUES                            
    select CASE DueHours
              WHEN '0' THEN  '1' 
              ELSE '0'
              END ISPOSTED,* from (
              select  VesselCallID ResourceAllocationID,V.VCN,
              'PORT DUES' as MovementName,'PORT DUES' as ServiceName,
              M.GroupCode GroupCode,M.MaterialCode MaterialCode, upper(M.MaterialDescription) MaterialDescription ,
                    @SAPACCOUNT AS AccountNo,
                     V.BreakWaterIn  as StartTime,V.BreakWaterOut  as Endtime,'N' as IsCalculated, M.Chargedas as Chargedas,M.UOM ,
                     '' as MovementType,'' as ServiceType,
                     '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                    isnull(@postedon,V.BreakWaterIn) RecentlyPostedDate,
                    
              isnull(V.BreakWaterOut, @postednewdate) PostingDateTime,
              
        
              CAST(ROUND( DATEDIFF(mi,isnull(@postedon,V.BreakWaterIn) ,  isnull(V.BreakWaterOut, @postednewdate))/1440.0
                     , 3, 3) AS DECIMAL(18, 3))
             AS 'DueHours',
              
                            '0' AS 'TotalHours',
                                    
                      '0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
              from VesselCall V 
               inner join ArrivalNotification an on an.VCN = V.VCN
               
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='PODU'
              where V.BreakWaterIn is not null 
              --and V.BreakWaterOut  >= ISNULL(@postedon, V.BreakWaterOut)
              --and V.BreakWaterIn  < @postednewdate
              and V.VCN  = @VCN  -- 'VCNDB2014190' 'VCNDB2014289'
              ) a where DueHours > 0 
              
  
	     union 
    -- BERTH DUES
    select CASE DueHours
              WHEN '0' THEN  '1' 
              ELSE '0'
              END ISPOSTED,* from (
              select  VesselCallID ResourceAllocationID,V.VCN,
              'BERTH DUES' as MovementName,'BERTH DUES' as ServiceName,
              M.GroupCode GroupCode,M.MaterialCode MaterialCode, upper(M.MaterialDescription) MaterialDescription ,
                    @SAPACCOUNTBerth AS AccountNo,
                     V.BreakWaterIn  as StartTime,V.BreakWaterOut  as Endtime,'N' as IsCalculated, M.Chargedas as Chargedas,M.UOM ,
                     '' as MovementType,'' as ServiceType,
                     '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                    isnull(@postedBerthdueson,V.BreakWaterIn) RecentlyPostedDate,
                    
              isnull(V.BreakWaterOut, @postednewdate) PostingDateTime,
              
        
              CAST(ROUND( DATEDIFF(mi,isnull(@postedBerthdueson,V.BreakWaterIn) ,  isnull(V.BreakWaterOut, @postednewdate))/1440.0
                     , 3, 3) AS DECIMAL(18, 3))
             AS 'DueHours',
              
                            '0' AS 'TotalHours',
                                    
                      '0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
              from VesselCall V 
               inner join ArrivalNotification an on an.VCN = V.VCN
               
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='BRTH'
              where V.BreakWaterIn is not null AND V.ATB IS NOT NULL 
               and V.VCN  = @BERTDUEVCN 
			   -- and convert(varchar, CEILING (ROUND(cast((datediff(hour,BreakWaterIn,isnull(BreakWaterOut, @postednewdate))  / 24.0) as FLOAT),2))) 
			  and  DATEDIFF(hour, BreakWaterIn, BreakWaterOut) 
						 >  case when @cntBnk> 0 then  48 else 0 end 
              ) a where DueHours > 0 
             
	 
        union
        -- FOR VTS CHARGES ONLY FOR DURBON PORT
        select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,VesselCallID ResourceAllocationID,
        V.VCN, 'Arrival' as MovementName,upper(M.MaterialDescription) as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, upper(M.MaterialDescription) MaterialDescription ,
        CASE
        WHEN (RPD.RevenuePostingDtlID IS NULL)
           THEN ''
        WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
           THEN RP.SAPAccNo
        END AS AccountNo,
          v.ATA as StartTime,v.ATD as Endtime,'N' as IsCalculated,M.Chargedas ,M.UOM  ,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RPD.PostedOn RecentlyPostedDate, getdate() PostingDateTime,
         M.Chargedas   AS 'DueHours',  M.Chargedas   AS 'TotalHours'
         , '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
        from VesselCall V 
        inner join ArrivalNotification an on an.VCN = V.VCN
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='VTCH'
       left join RevenuePostingDtl RPD on RPD.vcn = V.VCN  
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where   V.VCN  =    @VCN  -- 'VCNDB2014190' 'VCNDB2014289'
      UNION 
      
       
      --  REFUSE REMOVAL
      
	 select  CASE DueHours
              WHEN '0' THEN  '1' 
              ELSE '0'
              END ISPOSTED,* from (
		   select 	 VC.VesselCallid ResourceAllocationID,
                 VC.VCN,'REFUSE REMOVAL' MovementName , upper(M.MaterialDescription) ServiceName,
                   M.GroupCode, M.MaterialCode  ,upper(M.MaterialDescription) MaterialDescription  ,                  
				    @SAPACCOUNTRefuse AS AccountNo,
                           VC.BreakWaterIn StartTime, VC.BreakWaterOut Endtime,
                            M.IsCalculated,                          
                            convert(varchar, CEILING (ROUND(cast((datediff(hour,VC.BreakWaterIn,VC.BreakWaterOut)  / 24.0) as FLOAT),2))) Chargedas,
                             
 
                            
                            M.UOM  ,
                          '' MovementType  , '' ServiceType   ,
                          '' ServiceReferenceType, '' OperationType, '' TaskStatus,
                          --PostedOn RecentlyPostedDate, getdate() PostingDateTime,--commented by divya on 7Nov
						     isnull(@postedRefuseRemovalon,VC.BreakWaterIn) RecentlyPostedDate,                    
                             isnull(VC.BreakWaterOut, @postednewdate) PostingDateTime, 
					    CAST(ROUND( DATEDIFF(mi,isnull(@postedRefuseRemovalon,VC.BreakWaterIn) ,  isnull(VC.BreakWaterOut, @postednewdate))/1440.0
                     , 3, 3) AS DECIMAL(18, 3))
             AS 'DueHours',
              
                            '0' AS 'TotalHours',
                       
                          
                           '0' CloseMterReding, 
                                 '0' as startmtrreding,
                             ''  MeterSerialNo, '' BerthName
                   
                from VesselCall VC
                inner join ArrivalNotification AN ON AN.VCN = VC.VCN
                INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
                INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid 
                                                   AND M.ChargedFor='REFU'
                left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                              and rpd.ReferenceID =  VC.VesselCallid
                                                              and rpd.GroupCode =M.GroupCode
                                                              and rpd.MaterialCode = M.MaterialCode 
                left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID    
                WHERE VC.BreakWaterIn IS NOT NULL and VC.BreakWaterOut is not null AND VC.VCN  =  @VCN
				) a where DueHours > 0 
           --select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,  VC.VesselCallid ResourceAllocationID,
           --      VC.VCN,'Arrival' MovementName , upper(M.MaterialDescription) ServiceName,
           --        M.GroupCode, M.MaterialCode  ,upper(M.MaterialDescription) MaterialDescription  ,
           --        CASE
           --               WHEN (RPD.RevenuePostingDtlID IS NULL)
           --                  THEN ''
           --               WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
           --                  THEN RP.SAPAccNo
           --               END AS AccountNo,
           --                VC.BreakWaterIn StartTime, VC.BreakWaterOut Endtime,
           --                 M.IsCalculated, 
           --                 --convert(varchar, (datediff(day,VC.BreakWaterIn,VC.BreakWaterOut ))) Chargedas,
                            
           --                 convert(varchar, CEILING (ROUND(cast((datediff(hour,VC.BreakWaterIn,VC.BreakWaterOut)  / 24.0) as FLOAT),2))) Chargedas,
                             
 
                            
           --                 M.UOM  ,
           --               '' MovementType  , '' ServiceType   ,
           --               '' ServiceReferenceType, '' OperationType, '' TaskStatus,
           --               PostedOn RecentlyPostedDate, getdate() PostingDateTime,
           --             --   convert(varchar, (datediff(day,VC.BreakWaterIn,VC.BreakWaterOut ))) AS 'DueHours',  
           --               --  convert(varchar, (datediff(day,VC.BreakWaterIn ,VC.BreakWaterOut ))) AS 'TotalHours'
                          
           --           convert(varchar, CEILING (ROUND(cast((datediff(hour,VC.BreakWaterIn,VC.BreakWaterOut)  / 24.0) as FLOAT),2))) AS 'DueHours',  
           --           convert(varchar, CEILING (ROUND(cast((datediff(hour,VC.BreakWaterIn,VC.BreakWaterOut)  / 24.0) as FLOAT),2))) AS 'TotalHours'
                       
                          
           --               , '0' CloseMterReding, 
           --                      '0' as startmtrreding,
           --                  ''  MeterSerialNo, '' BerthName
                   
           --     from VesselCall VC
           --     inner join ArrivalNotification AN ON AN.VCN = VC.VCN
           --     INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
           --     INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid 
           --                                        AND M.ChargedFor='REFU'
           --     left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
           --                                                   and rpd.ReferenceID =  VC.VesselCallid
           --                                                   and rpd.GroupCode =M.GroupCode
           --                                                   and rpd.MaterialCode = M.MaterialCode 
           --     left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID    
           --     WHERE VC.BreakWaterIn IS NOT NULL and VC.BreakWaterOut is not null AND VC.VCN  =  @VCN 
      
      /*
                select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,  VC.VesselCallid ResourceAllocationID,
                 VC.VCN,'Arrival' MovementName , upper(M.MaterialDescription) ServiceName,
                   M.GroupCode, M.MaterialCode  ,upper(M.MaterialDescription) MaterialDescription  ,
                   CASE
                          WHEN (RPD.RevenuePostingDtlID IS NULL)
                             THEN ''
                          WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
                             THEN RP.SAPAccNo
                          END AS AccountNo,
                           VC.BreakWaterIn StartTime, VC.BreakWaterOut Endtime,
                            M.IsCalculated, convert(varchar, (datediff(day,VC.BreakWaterIn,VC.BreakWaterOut ))) Chargedas,M.UOM  ,
                          '' MovementType  , '' ServiceType   ,
                          '' ServiceReferenceType, '' OperationType, '' TaskStatus,
                          PostedOn RecentlyPostedDate, getdate() PostingDateTime,
                           convert(varchar, (datediff(day,VC.BreakWaterIn,VC.BreakWaterOut ))) AS 'DueHours',  
                            convert(varchar, (datediff(day,VC.BreakWaterIn ,VC.BreakWaterOut ))) AS 'TotalHours'
                          , '0' CloseMterReding, 
                                 '0' as startmtrreding,
                             ''  MeterSerialNo, '' BerthName
                   
                from VesselCall VC
                inner join ArrivalNotification AN ON AN.VCN = VC.VCN
                INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
                INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid 
                                                   AND M.ChargedFor='REFU'
                left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                              and rpd.ReferenceID =  VC.VesselCallid
                                                              and rpd.GroupCode =M.GroupCode
                                                              and rpd.MaterialCode = M.MaterialCode 
                left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID    
                WHERE VC.BreakWaterIn IS NOT NULL and VC.BreakWaterOut is not null AND VC.VCN  =    @VCN  
    */
      
      UNION 
      /*  -- As per the Discussion with Vasu and Vinoda commented on 06-oct-2015 
             -- Fire Protection Only
             
                        select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , VC.VesselCallMovementID ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                   upper(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,upper(M.MaterialDescription) MaterialDescription ,
                     CASE
                            WHEN (RPD.RevenuePostingDtlID IS NULL)
                               THEN ''
                            WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
                               THEN RP.SAPAccNo
                            END AS AccountNo,
                             VC.ATB  StartTime, VC.ATUB  Endtime,
                              M.IsCalculated, 
                                convert(varchar,ceiling(DATEDIFF(mi,VC.ATB , VC.ATUB )/60.0)) Chargedas,M.UOM  ,
                            VC.MovementType  ,M.ServiceType   ,
                            'VTSR' ServiceReferenceType,''  OperationType,'' TaskStatus,
                            PostedOn RecentlyPostedDate, getdate() PostingDateTime,
                            convert(varchar,ceiling(DATEDIFF(mi,VC.ATB , VC.ATUB )/60.0)) AS 'DueHours',  1 AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
              from VesselCallMovement VC 
               INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN AND AN.IsIMDGANFinal = 'Y'
               INNER JOIN MaterialCodePort MP on AN.PortCode = MP.PortCode
               inner join MaterialCodeMaster M on  MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
                                            and M.ChargedFor='FIPR' 
               inner join SubCategory S on S.SubCatCode = VC.MovementType
               inner join SubCategory SB1 on SB1.SubCatCode = M.ServiceType
               left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  VC.VesselCallMovementID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
               left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
               WHERE 
                    AN.PortCode = @portcode
                   AND VC.ATUB  is not null 
                 AND VC.VCN  =   @VCN
                  AND (VC.MovementType ='ARMV' or  VC.MovementType ='SHMV' or  VC.MovementType ='WRMV')
                   AND (VC.FromPositionQuayCode  != 'IV'
                   AND ( VC.FromPositionBerthCode != 'IV1' 
                   OR VC.FromPositionBerthCode != 'IV2'
                   OR VC.FromPositionBerthCode != 'IV3'
                   OR VC.FromPositionBerthCode != 'IV4'
                   OR VC.FromPositionBerthCode != 'IV5'
                   OR VC.FromPositionBerthCode != 'IV6'
                   OR VC.FromPositionBerthCode != 'IV7'
                   OR VC.FromPositionBerthCode != 'IV8'
                   OR VC.FromPositionBerthCode != 'IV9'))
                   
             
     
      UNION 
      */
           -- Fire Protection plus Security
           
           select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , VC.VesselCallMovementID ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                   --SB1.SubCatName as ServiceName,
                   upper(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,upper(M.MaterialDescription) MaterialDescription  ,
                     CASE
                            WHEN (RPD.RevenuePostingDtlID IS NULL)
                               THEN ''
                            WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
                               THEN RP.SAPAccNo
                            END AS AccountNo,
                             VC.ATB  StartTime, VC.ATUB  Endtime,
                              M.IsCalculated, 
                                convert(varchar,ceiling(DATEDIFF(mi,VC.ATB , VC.ATUB )/60.0)) Chargedas,M.UOM  ,
                            VC.MovementType  ,M.ServiceType   ,
                            'VTSR' ServiceReferenceType,''  OperationType,'' TaskStatus,
                            PostedOn RecentlyPostedDate, getdate() PostingDateTime,
                             convert(varchar,ceiling(DATEDIFF(mi,VC.ATB , VC.ATUB )/60.0)) AS 'DueHours', 
                             1 AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
              from VesselCallMovement VC 
                  INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN AND AN.IsIMDGANFinal = 'Y'
                  inner join MaterialCodeMaster M on m.PortCode = VC.FromPositionPortCode and
                                            M.QuayCode = VC.FromPositionQuayCode  and
                                            M.BerthCode= VC.FromPositionBerthCode  and
                                            M.ChargedFor='FPPS' 
                  INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
                  AND MP.PortCode =@portcode
                   inner join SubCategory S on S.SubCatCode = VC.MovementType
                   inner join SubCategory SB1 on SB1.SubCatCode = M.ServiceType
                   left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  VC.VesselCallMovementID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
                  left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
                           
                  WHERE 
                   VC.ATUB  is not null 
                 AND VC.VCN  =   @VCN
                  AND (VC.MovementType ='ARMV' or  VC.MovementType ='SHMV' or  VC.MovementType ='WRMV')
      UNION
      -- CREW TRANS PORTATION
                  select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , RA.ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                   upper(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,upper(M.MaterialDescription) MaterialDescription  ,
                     CASE
                            WHEN (RPD.RevenuePostingDtlID IS NULL)
                               THEN ''
                            WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
                               THEN RP.SAPAccNo
                            END AS AccountNo,
                             VC.ATB StartTime, VC.ATUB Endtime,
                              M.IsCalculated, 
                  convert(varchar, CEILING (ROUND(cast((datediff(hour,VC.ATB,VC.ATUB)  / 24.0) as FLOAT),2))) Chargedas,
                            --  convert(varchar, (datediff(day,VC.ATB,VC.ATUB)+1)) Chargedas,
                              M.UOM  ,
                            VC.MovementType  ,M.ServiceType   ,
                            RA.ServiceReferenceType,RA.OperationType,RA.TaskStatus,
                            PostedOn RecentlyPostedDate, getdate() PostingDateTime,
                            convert(varchar, CEILING (ROUND(cast((datediff(hour,VC.ATB,VC.ATUB)  / 24.0) as FLOAT),2))) AS 'DueHours',
                          --  convert(varchar, (datediff(day,VC.ATB,VC.ATUB)+1)) AS 'DueHours',
                            1 AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
                     
                  from ShiftingBerthingTaskExecution SB
                  inner join ResourceAllocation RA on RA.ResourceAllocationID = SB.ResourceAllocationID
                  Inner join VesselCallMovement VC on VC.ServiceRequestID = RA.ServiceReferenceID 
                  inner join MaterialCodeMaster M on m.PortCode = SB.FromBerthPortCode  and
                                            M.QuayCode = SB.FromBerthQuayCode  and
                                            M.BerthCode= SB.FromBerthCode and
                                            M.ChargedFor='ILND' 
                  INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid AND MP.PortCode =@portcode
                   inner join SubCategory S on S.SubCatCode = VC.MovementType
                   inner join SubCategory SB1 on SB1.SubCatCode = M.ServiceType
                   left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  RA.ResourceAllocationID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
                  left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
                           
                  WHERE (RA.TaskStatus = 'COMP' or RA.TaskStatus ='VERF' ) and RA.OperationType = 'BRTH' and VC.ATUB is not null 
                  AND VC.VCN  =   @VCN  
                  AND (VC.MovementType ='ARMV' or  VC.MovementType ='SHMV' or  VC.MovementType ='WRMV')
      UNION
      
       -- Running of Lines
                  select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , RA.ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                    upper(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,upper(M.MaterialDescription) MaterialDescription  ,
                     CASE
                            WHEN (RPD.RevenuePostingDtlID IS NULL)
                               THEN ''
                            WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
                               THEN RP.SAPAccNo
                            END AS AccountNo,
                             VC.ATB StartTime, VC.ATUB Endtime,
                              M.IsCalculated, m.Chargedas ,M.UOM  ,
                            VC.MovementType  ,M.ServiceType   ,
                            RA.ServiceReferenceType,RA.OperationType,RA.TaskStatus,
                            PostedOn RecentlyPostedDate, getdate() PostingDateTime,
                             m.Chargedas  AS 'DueHours',  1 AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
                     
                  from ShiftingBerthingTaskExecution SB
                  inner join ResourceAllocation RA on RA.ResourceAllocationID = SB.ResourceAllocationID
                  Inner join VesselCallMovement VC on VC.ServiceRequestID = RA.ServiceReferenceID 
                  inner join MaterialCodeMaster M on m.PortCode = SB.FromBerthPortCode and
                                            M.QuayCode = SB.FromBerthQuayCode  and
                                            M.BerthCode= SB.FromBerthCode and
                                            M.ChargedFor='RNOF' 
                  INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid AND MP.PortCode =@portcode
                   inner join SubCategory S on S.SubCatCode = VC.MovementType
                   inner join SubCategory SB1 on SB1.SubCatCode = M.ServiceType
                   left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  RA.ResourceAllocationID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
                  left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
                  WHERE (RA.TaskStatus = 'COMP' or RA.TaskStatus ='VERF' ) and  RA.OperationType = 'BRTH' 
                 AND VC.VCN  =   @VCN  
      
      UNION
   
       -- FOR SAMSA LEVY CHARGE
      select 
          isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,
        VesselCallID ResourceAllocationID,
        V.VCN, 'Arrival' as MovementName,upper(M.MaterialDescription) as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, upper(M.MaterialDescription) MaterialDescription  ,
        CASE
        WHEN (RPD.RevenuePostingDtlID IS NULL)
           THEN ''
        WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
           THEN RP.SAPAccNo
        END AS AccountNo,
          v.ATA as StartTime,v.ATD as Endtime,'N' as IsCalculated, m.Chargedas , M.UOM as UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RPD.PostedOn RecentlyPostedDate, getdate() PostingDateTime,
         m.Chargedas  AS 'DueHours',  '1'  AS 'TotalHours'
         , '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
        from VesselCall V 
        inner join ArrivalNotification an on an.VCN = V.VCN 
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='SMSA'
       Inner join PortRegistry po ON po.PortCode = an.LastPortOfCall 
       left join RevenuePostingDtl RPD on RPD.vcn = V.VCN  
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where    V.VCN  =    @VCN  --  'VCNDB1500300'-- 'VCNDB2014289'
            and isnull( po.IsSA , 'N') = 'N'
            
      UNION
       -- FOR LIGHT DUES CHARGE
      select 
          isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,
        VesselCallID ResourceAllocationID,
        V.VCN, 'Arrival' as MovementName,upper(M.MaterialDescription) as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, upper(M.MaterialDescription) MaterialDescription  ,
        CASE
        WHEN (RPD.RevenuePostingDtlID IS NULL)
           THEN ''
        WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
           THEN RP.SAPAccNo
        END AS AccountNo,
          v.ATA as StartTime,v.ATD as Endtime,'N' as IsCalculated, M.Chargedas,M.UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RPD.PostedOn RecentlyPostedDate, getdate() PostingDateTime,
         m.Chargedas  AS 'DueHours',  '1'  AS 'TotalHours'
         , '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
        from VesselCall V 
        inner join ArrivalNotification an on an.VCN = V.VCN 
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='LIDC'
       Inner join PortRegistry po ON po.PortCode = an.LastPortOfCall 
       left join RevenuePostingDtl RPD on RPD.vcn = V.VCN  
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where    V.VCN  =    @VCN  --  'VCNDB1500300'-- 'VCNDB2014289'
            and isnull( po.IsSA , 'N') = 'N'
      union 
      -- IS SPECIAL NATURE
         
        select 
          isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,
        VesselCallID ResourceAllocationID,
        V.VCN, 'Arrival' as MovementName,upper(M.MaterialDescription) as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, upper(M.MaterialDescription) MaterialDescription  ,
        CASE
        WHEN (RPD.RevenuePostingDtlID IS NULL)
           THEN ''
        WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
           THEN RP.SAPAccNo
        END AS AccountNo,
          v.ATA as StartTime,v.ATD as Endtime,'N' as IsCalculated, M.Chargedas,M.UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RPD.PostedOn RecentlyPostedDate, getdate() PostingDateTime,
         m.Chargedas  AS 'DueHours',  m.Chargedas AS 'TotalHours'
         , '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
        from VesselCall V 
        inner join ArrivalNotification an on an.VCN = V.VCN 
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='SPCL'
       left join RevenuePostingDtl RPD on RPD.vcn = V.VCN  
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where  an.IsSpecialNature = 'A' and 
            V.VCN  =  @VCN
            
      union 
           -- FOR VTS SERVICES ARRV,SHIFT,WARP,SAIL 
          SELECT isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , T.ResourceAllocationID,
          T.VCN,SB.SubCatName AS MovementName ,upper(SB1.SubCatName) as ServiceName,
          M.GroupCode, M.MaterialCode  ,upper(M.MaterialDescription) MaterialDescription  ,
          CASE
          WHEN (RPD.RevenuePostingDtlID IS NULL)
             THEN ''
          WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
             THEN RP.SAPAccNo
          END AS AccountNo,

          T.StartTime,T.Endtime,
            M.IsCalculated, M.Chargedas,M.UOM  ,
          M.MovementType  ,M.ServiceType   ,
          T.ServiceReferenceType,T.OperationType,T.TaskStatus,
          PostedOn RecentlyPostedDate, getdate() PostingDateTime, m.Chargedas  AS 'DueHours',  m.Chargedas AS 'TotalHours'
          , '0' CloseMterReding, 
                 '0' as startmtrreding,
               --   '' MeterSerialNo, '' BerthName
             TB.BerthName  MeterSerialNo, FB.BerthName BerthName
          from (
           select  sbr.FromBerthCode ,sbr.FromBerthQuayCode ,sbr.FromBerthPortCode ,
         sbr.ToBerthCode ,sbr.ToBerthQuayCode ,sbr.ToBerthPortCode ,  RA.* , 
           CASE RA.ServiceReferenceType
                                  WHEN 'SUPP' THEN 
                                        (select VCN from dbo.SuppServiceRequest  where SuppServiceRequestID = RA.ServiceReferenceID)  
                                  ELSE 
                                        (select VCN from dbo.ServiceRequest where ServiceRequestID =  RA.ServiceReferenceID)
                                  END VCN,
          CASE RA.ServiceReferenceType
                                  WHEN 'SUPP' THEN 
                                        (select ServiceType from dbo.SuppServiceRequest  where SuppServiceRequestID = RA.ServiceReferenceID)  
                                  ELSE 
                                        (select MovementType from dbo.ServiceRequest where ServiceRequestID =  RA.ServiceReferenceID)
                                  END MovementType                        
           from dbo.ResourceAllocation RA
               LEFT JOIN ShiftingBerthingTaskExecution sbr ON  ra.ResourceAllocationID = sbr.ResourceAllocationID
          ) T 
          INNER JOIN MaterialCodeMaster M ON M.MovementType = T.MovementType AND M.ServiceType = T.OperationType
          INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          inner join SubCategory SB on SB.SubCatCode = M.MovementType
          inner join SubCategory SB1 on SB1.SubCatCode = M.ServiceType
          left join RevenuePostingDtl RPD on RPD.vcn = t.VCN  
                                              and rpd.ReferenceID =  T.ResourceAllocationID
                                              and rpd.GroupCode =M.GroupCode
                                              and rpd.MaterialCode = M.MaterialCode 
                                              and rpd.MomentType = M.MovementType 
                                              and rpd.ServiceType = M.ServiceType
          left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
          left join Berth FB ON FB.BerthCode = T.FromBerthCode and FB.QuayCode = T.FromBerthQuayCode 
                                  AND FB.PortCode =  T.FromBerthPortCode 
          left join Berth TB ON TB.BerthCode = T.ToBerthCode and TB.QuayCode = T.ToBerthQuayCode 
                    AND TB.PortCode =  T.ToBerthPortCode 
                                              
          WHERE  T.VCN  = @VCN  -- 'VCNDB2014190' 'VCNDB2014289'
          and MP.PortCode = @portcode
          and  (taskstatus='COMP' or taskstatus ='VERF' )
       
          
      

    UNION
             -- DRY DOCK BOOKING FEES
                    select 
          isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , SuppDryDockID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKSERVICES' as MovementName,upper(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
          RPH.SAPAccNo AS AccountNo,
             SD.FromDate  as StartTime,SD.ToDate as Endtime,
             M.IsCalculated as IsCalculated,M.Chargedas ,M.UOM ,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
                m.Chargedas 'DueHours', '1'  AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '1' MeterSerialNo, '' BerthName
          from SuppDryDock SD
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'DKBK'
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode    
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
               WHERE  SD.VCN  = @VCN   -- 'VCNDB2014190' 'VCNDB2014289'
                    and MP.PortCode = @portcode
                    
    UNION 
               -- PREPARATION COST OF DRY DOCK
           select 
              isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , SuppDryDockID ResourceAllocationID,
              SD.VCN ,  'DRYDOCKSERVICES' as MovementName,upper(M.MaterialDescription) as ServiceName,
              M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
              RPH.SAPAccNo AS AccountNo,
                 SD.FromDate  as StartTime,SD.ToDate as Endtime,
                 M.IsCalculated as IsCalculated,M.Chargedas ,M.UOM ,
                  '' as MovementType,'' as ServiceType,
                  '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                  RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
                  m.Chargedas 'DueHours', '1'  AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '2' MeterSerialNo, '' BerthName
              from SuppDryDock SD
              inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                         AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                         and  M.ChargedFor = 'PREP'
              inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
              Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                          and rpd.GroupCode = M.GroupCode
                                                        and rpd.MaterialCode = M.MaterialCode    
              LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                              and RPD.VCN = SD.VCN
                   WHERE  SD.VCN  =  @VCN  --'VCNDB2014229' -- 'VCNDB2014190' 
                        and MP.PortCode = @portcode
    
    
    
    
    UNION 
    -- DOCKING FEES
       select 
          isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , SuppDryDockID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKSERVICES' as MovementName,upper(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
          RPH.SAPAccNo AS AccountNo,
             SD.EnteredDockDateTime  as StartTime,SD.LeftDockDateTime as Endtime,
             M.IsCalculated as IsCalculated,M.Chargedas ,M.UOM  as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
                m.Chargedas 'DueHours', '1'  AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '3' MeterSerialNo, '' BerthName
          from SuppDryDock SD
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'DOCK'
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode    
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE  SD.VCN  = @VCN   -- 'VCNDB2014190' 'VCNDB2014289'
                    and MP.PortCode = @portcode  and SD.EnteredDockDateTime is not null
                    
    union 
        
   
    -- DRY DOCK 24 HOURS
             select 
            isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , SuppDryDockID ResourceAllocationID,
            SD.VCN ,  'DRYDOCKSERVICES' as MovementName,'FIRST 24 HR PERIOD' as ServiceName,
            M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
            RPH.SAPAccNo AS AccountNo,
               SD.EnteredDockDateTime  as StartTime,SD.LeftDockDateTime as Endtime,
               M.IsCalculated as IsCalculated,M.Chargedas ,M.UOM ,
                '' as MovementType,'' as ServiceType,
                '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
                m.Chargedas AS 'DueHours' ,  '1'  AS 'TotalHours'
                 , '0' CloseMterReding, '0' as startmtrreding, '4' MeterSerialNo, '' BerthName
            from SuppDryDock SD
            inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                       AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                       and M.ChargedFor = 'DR24'
            inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
            left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                        and rpd.GroupCode = M.GroupCode
                                                        and rpd.MaterialCode = M.MaterialCode    
            LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                            and RPD.VCN = SD.VCN
                 WHERE  SD.VCN  = @VCN  -- 'VCNDB2014190' 'VCNDB2014289'
                      and MP.PortCode = @portcode
                      and SD.EnteredDockDateTime is not null
                      
     UNION 
   
         -- DRY DOCK 12 Hours      
         -- isnull    @Dry12postedon  @Dry12SAPACCOUNT   
            
              
       
       
          select  CASE DueHours
                    WHEN '0' THEN  '1' 
                    ELSE '0'
                    END ISPOSTED,* from (
                    select ResourceAllocationID,VCN ,   MovementName, ServiceName,
                    GroupCode,MaterialCode,MaterialDescription,AccountNo,
                     StartTime,Endtime,IsCalculated, Chargedas, UOM, MovementType,ServiceType,
                     ServiceReferenceType,OperationType, TaskStatus,RecentlyPostedDate,
                    (isnull(RecentlyPostedDate, StartTime) + ceiling(DATEDIFF(mi,isnull(RecentlyPostedDate, StartTime) ,isnull(Endtime, getdate()) )/720.0)/2.0) PostingDateTime,
                    ceiling(DATEDIFF(mi,isnull(RecentlyPostedDate, StartTime) ,isnull(Endtime, getdate()) )/720.0) AS 'DueHours',
                    ceiling(DATEDIFF(mi,StartTime ,isnull(Endtime, getdate()) )/720.0) AS 'TotalHours'
                    , '0' CloseMterReding, 
                 '0' as startmtrreding,
                 '5' MeterSerialNo, '' BerthName
                    from (
                    select 
                     SuppDryDockID ResourceAllocationID,
                    SD.VCN ,  'DRYDOCK12HRSSERVICES' as MovementName,upper(M.MaterialDescription) as ServiceName,
                    M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
                    @Dry12SAPACCOUNT AS AccountNo,
                       SD.EnteredDockDateTime+1  as StartTime,SD.LeftDockDateTime as Endtime,
                       M.IsCalculated as IsCalculated,M.Chargedas as Chargedas,M.UOM  as UOM,
                        '' as MovementType,'' as ServiceType,
                        '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                        @Dry12postedon  RecentlyPostedDate,
                           CASE WHEN  sd.Todate >= getdate() 
                                THEN  getdate()
                                ELSE sd.Todate 
                           END AS calculateddate
                    from SuppDryDock SD
                    inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                               AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                               and M.ChargedFor = 'DR12'
                               and SD.EnteredDockDateTime +1  <= getdate()
                    inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                    left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                                and rpd.GroupCode = M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode    
                    LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                                    and RPD.VCN = SD.VCN
                    WHERE  SD.VCN  = @VCN   -- 'VCNDB2014190' 'VCNDB2014289'
                   and MP.PortCode = @portcode
                    and  ceiling(DATEDIFF(mi,SD.EnteredDockDateTime, SD.ToDate  )/60.0) > 24
                      and SD.EnteredDockDateTime is not null
          ) n)k
 
        
   UNION 
    -- UNDOCKING FEES
       select 
          isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , SuppDryDockID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKSERVICES' as MovementName,upper(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
          RPH.SAPAccNo AS AccountNo,
             SD.EnteredDockDateTime  as StartTime,SD.LeftDockDateTime as Endtime,
             M.IsCalculated as IsCalculated,M.Chargedas ,M.UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
               M.Chargedas 'DueHours', '1'  AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '6' MeterSerialNo, '' BerthName
          from SuppDryDock SD
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'UNDK'
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode    
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE  SD.VCN  = @VCN   -- 'VCNDB2014190' 'VCNDB2014289'
                    and MP.PortCode = @portcode  and SD.LeftDockDateTime is not null
        
     
                    
   
    union 
     -- SUPPLIMANTORY SERVICE FOR WATER AND FLOOTING CRANES      
       select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,a.ResourceAllocationID,
             sup.VCN, b.ServiceTypeName as MovementName ,
                    'SUPPLIMANT' as ServiceName,M.GroupCode, M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
                    RPH.SAPAccNo AccountNo, 
            oth.StartTime as StartTime,oth.EndTime as Endtime,M.IsCalculated, M.Chargedas,M.UOM,
            '' as MovementType,a.OperationType as ServiceType,
             '' as ServiceReferenceType,a.OperationType as OperationType, '' as TaskStatus,
                  RPD.PostedOn RecentlyPostedDate, getdate() PostingDateTime,
                  
                   CASE
                WHEN (a.OperationType = 'WTST')
                   THEN isnull(oth.ClosingMeterReading,0)-isnull(oth.OpeningMeterReading,0)  
                WHEN (a.OperationType = 'FCST')
                   THEN   ceiling(DATEDIFF(mi, oth.StartTime , oth.EndTime)/60.0) 
                END AS 'DueHours',
                '0'  AS 'TotalHours',
                 isnull(oth.ClosingMeterReading,0) as CloseMterReding, 
                 isnull(oth.OpeningMeterReading,0) as startmtrreding,
                 isnull(oth.MeterSerialNo, ' ') as MeterSerialNo,
                 BT.BerthName
            from dbo.ResourceAllocation a 
            inner join ServiceType b on b.ServiceTypeCode = a.OperationType 
            inner join dbo.SuppServiceRequest sup on  sup.SuppServiceRequestID = a.ServiceReferenceID
            inner join OtherServiceRecording oth on oth.ResourceAllocationID = a.ResourceAllocationID
            inner join MaterialCodeMaster M ON M.ServiceType = a.OperationType
            inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
            left join dbo.Berth BT ON BT.PortCode = sup.PortCode AND BT.QuayCode = sup.QuayCode  AND BT.BerthCode = sup.BerthCode
            Left join RevenuePostingDtl RPD on RPD.vcn = sup.VCN  
                                           and rpd.GroupCode = M.GroupCode
                                           and rpd.MaterialCode = M.MaterialCode 
                                            and RPD.ReferenceID = a.ResourceAllocationID
            LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                           and RPD.VCN = sup.VCN
            where a.ServiceReferenceType = 'SUPP'  and (a.TaskStatus = 'COMP'  or a.TaskStatus  ='VERF')
                      and  sup.VCN  = @VCN   -- 'VCNDB2014226' 'VCNDB2014289'
                       and MP.PortCode = @portcode
  UNION 
              -- FLOATING CRANES - MOBILISATION & DEMO.


--        select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,a.ResourceAllocationID,
--             sup.VCN, b.ServiceTypeName as MovementName ,
--                    'SUPPLIMANT' as ServiceName,M.GroupCode, M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
--                    RPH.SAPAccNo AccountNo, 
--            oth.StartTime as StartTime,oth.EndTime as Endtime,M.IsCalculated, M.Chargedas,M.UOM,
--            '' as MovementType,a.OperationType as ServiceType,
--             '' as ServiceReferenceType,a.OperationType as OperationType, '' as TaskStatus,
--                  RPD.PostedOn RecentlyPostedDate, getdate() PostingDateTime,
--                M.Chargedas AS 'DueHours',
--                '1'  AS 'TotalHours', '0' CloseMterReding, 
--                 '0' as startmtrreding,
--                  '' MeterSerialNo, '' BerthName
--        from dbo.ResourceAllocation a 
--        inner join ServiceType b on b.ServiceTypeCode = a.OperationType 
--        inner join dbo.SuppServiceRequest sup on  sup.SuppServiceRequestID = a.ServiceReferenceID
--        inner join OtherServiceRecording oth on oth.ResourceAllocationID = a.ResourceAllocationID
--        inner join MaterialCodeMaster M ON M.ChargedFor = a.OperationType
--        inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
--        Left join RevenuePostingDtl RPD on RPD.vcn = sup.VCN  
--                                       and rpd.GroupCode = M.GroupCode
--                                       and rpd.MaterialCode = M.MaterialCode   
--                                        and RPD.ReferenceID = a.ResourceAllocationID
--        LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
--                                       and RPD.VCN = sup.VCN
--        where a.ServiceReferenceType = 'SUPP' and ( a.TaskStatus ='COMP' or a.TaskStatus  ='VERF')
--         and  sup.VCN  = @VCN   -- 'VCNDB2014226' 'VCNDB2014289'
--                       and MP.PortCode = @portcode


            select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,a.ResourceAllocationID,
             sup.VCN, b.ServiceTypeName as MovementName ,
                    'SUPPLIMANT' as ServiceName,M.GroupCode, M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
                    RPH.SAPAccNo AccountNo, 
            oth.StartTime as StartTime,oth.EndTime as Endtime,M.IsCalculated, M.Chargedas,M.UOM,
            '' as MovementType,a.OperationType as ServiceType,
             '' as ServiceReferenceType,a.OperationType as OperationType, '' as TaskStatus,
                  RPD.PostedOn RecentlyPostedDate, getdate() PostingDateTime,
                M.Chargedas AS 'DueHours',
                '1'  AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
     
        from  dbo.SuppServiceRequest sup 
        inner join dbo.ResourceAllocation a  on  sup.SuppServiceRequestID = a.ServiceReferenceID
        and a.ResourceAllocationID = 
          (select top(1) RAA.ResourceAllocationID from ResourceAllocation RAA where RAA.ServiceReferenceID =  sup.SuppServiceRequestID
          and RAA.ServiceReferenceType='SUPP'  order by ResourceAllocationID ASC )
        inner join ServiceType b on b.ServiceTypeCode = a.OperationType 
        inner join OtherServiceRecording oth on oth.ResourceAllocationID = a.ResourceAllocationID
        inner join MaterialCodeMaster M ON M.ChargedFor = a.OperationType
        inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
        Left join RevenuePostingDtl RPD on RPD.vcn = sup.VCN  
                                       and rpd.GroupCode = M.GroupCode
                                       and rpd.MaterialCode = M.MaterialCode   
                                        and RPD.ReferenceID = a.ResourceAllocationID
        LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                       and RPD.VCN = sup.VCN
        where a.ServiceReferenceType = 'SUPP' and ( a.TaskStatus ='COMP' or a.TaskStatus  ='VERF')
         and  sup.VCN  = @VCN   -- 'VCNDB2014226' 'VCNDB2014289'
                       and MP.PortCode = @portcode
                       
        
        
   UNION 
   
   
   
           -- HOT WORK PERMITS
        select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,s.SuppServiceRequestID ResourceAllocationID,
             S.VCN, SB.SubCatName as MovementName ,
                    'SUPPLIMANT' as ServiceName,M.GroupCode, M.MaterialCode,
                   upper(M.MaterialDescription) MaterialDescription ,
                    RPH.SAPAccNo AccountNo, 
            S.FromDate as StartTime,S.ToDate as Endtime,M.IsCalculated, M.Chargedas,M.UOM,
            '' as MovementType,  M.ServiceType as ServiceType,
             '' as ServiceReferenceType,M.ChargedFor as OperationType, '' as TaskStatus,
                  RPD.PostedOn RecentlyPostedDate, getdate() PostingDateTime,
                M.Chargedas AS 'DueHours',
                '1'  AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
             from dbo.SuppServiceRequest S 
        inner join SuppHotWorkInspection H on S.SuppServiceRequestID = h.SuppServiceRequestID
        inner join SubCategory SB on SB.SubCatCode = S.ServiceType
        inner join MaterialCodeMaster M ON M.ServiceType = S.ServiceType
        inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
        Left join RevenuePostingDtl RPD on RPD.vcn = S.VCN  
                                       and rpd.GroupCode = M.GroupCode
                                       and rpd.MaterialCode = M.MaterialCode   
                                       and RPD.ReferenceID = s.SuppServiceRequestID
        LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
          --                             and RPD.VCN = S.VCN
        where S.ServiceType = 'HWST' and 
        M.ChargedFor = 'HWRK'    and   
        s.VCN =  @VCN 
       and MP.PortCode =  @portcode
       
                       
                       
   ------
  UNION
    -- DRY DOCK ELECTRICITY CONNECTION FEES
  select 
          isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , 
          MS.SuppMiscServiceID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName,
          upper(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
          RPH.SAPAccNo AS AccountNo,
             MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated,M.Chargedas,M.UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
                M.Chargedas 'DueHours', M.Chargedas  AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'ELCT' AND MS.ServiceTypeCode = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode 
                                                    and RPD.ReferenceID =  MS.SuppMiscServiceID
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE  SD.VCN  = @VCN   -- 'VCNDB2014226' 'VCNDB2014289'
                       and MP.PortCode = @portcode
                    
  UNION 
     -- DRY DOCK ELECTRICITY DAY HAIR
   select   isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , MS.SuppMiscServiceID  ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName, upper(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
          RPH.SAPAccNo AS AccountNo, MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated,
            M.Chargedas ,M.UOM  ,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
             ceiling(DATEDIFF(mi, MS.FromDateTime ,  MS.ToDateTime )/1440.0) AS'DueHours', 
               ceiling(DATEDIFF(mi, MS.FromDateTime ,  MS.ToDateTime )/1440.0) AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'ELPD' AND MS.ServiceTypeCode = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode   
                                                    and RPD.ReferenceID =  MS.SuppMiscServiceID
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE  SD.VCN  = @VCN   -- 'VCNDB2014226' 'VCNDB2014289'
                       and MP.PortCode = @portcode
                    
                    
  UNION 
        -- DRY DOCK ELECTRICITY USAGE CHARGES
   select   isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,MS.SuppMiscServiceID  ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName, upper(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
          RPH.SAPAccNo AS AccountNo, MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated, M.Chargedas as Chargedas,M.UOM  as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
               MS.Quantity AS'DueHours', 
              MS.Quantity AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'ELMT' AND MS.ServiceTypeCode = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode  
                                                    and RPD.ReferenceID =  MS.SuppMiscServiceID
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE   SD.VCN  = @VCN   -- 'VCNDB2014226' 'VCNDB2014289'
                       and MP.PortCode = @portcode
   UNION
   -- DRY DOCK FRESH WATER  
   select   isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , MS.SuppMiscServiceID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName, upper(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
          RPH.SAPAccNo AS AccountNo, MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated,M.Chargedas as Chargedas,M.UOM  as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
               MS.Quantity AS'DueHours', 
              MS.Quantity AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'DOWT' AND MS.ServiceTypeCode = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode    
                                                     and RPD.ReferenceID =  MS.SuppMiscServiceID
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE   SD.VCN  = @VCN   -- 'VCNDB2014226' 'VCNDB2014289'
                       and MP.PortCode = @portcode
                       
  UNION 
       -- CHARGED FOR WHARF CRANE USAGE AT DRY DOCK/SHIP REPAIR
      select   isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , MS.SuppMiscServiceID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName, upper(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
          RPH.SAPAccNo AS AccountNo, MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated,M.Chargedas,M.UOM  ,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
               ceiling(DATEDIFF(mi, MS.FromDateTime ,  MS.ToDateTime  )/60.0) AS'DueHours', 
              ceiling(DATEDIFF(mi, MS.FromDateTime ,  MS.ToDateTime  )/60.0) AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'WHAR' AND MS.ServiceTypeCode  = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode    
                                                     and RPD.ReferenceID =  MS.SuppMiscServiceID
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE    SD.VCN  = @VCN   -- 'VCNDB2014226' 'VCNDB2014289'
                       and MP.PortCode = @portcode
                       
    union 

  -- CHARGED FOR COMPRESSED AIR
      select   isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , MS.SuppMiscServiceID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName, upper(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,upper(M.MaterialDescription) MaterialDescription ,
          RPH.SAPAccNo AS AccountNo, MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated,M.Chargedas ,M.UOM ,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate, getdate() PostingDateTime,
               ceiling(DATEDIFF(mi, MS.FromDateTime ,  MS.ToDateTime  )/60.0) AS'DueHours', 
              ceiling(DATEDIFF(mi, MS.FromDateTime ,  MS.ToDateTime  )/60.0) AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'CPAR' AND MS.ServiceTypeCode  = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode    
                                                     and RPD.ReferenceID =  MS.SuppMiscServiceID
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE    SD.VCN  = @VCN   -- 'VCNDB2014226' 'VCNDB2014289'
                       and MP.PortCode = @portcode
                       
  
    
             
UNION 
       -- NO EXTRA TUGS USED FOR DISPLAY       
        select '1' ISPOSTED , 1 ResourceAllocationID,
          VCN ,  'DISPLAYONLY' as MovementName, 
          convert(varchar, concat('No of Extra Tugs Used: ',  extratugs))   as ServiceName,
          '11' GroupCode, '11' MaterialCode, upper(SubCatName) MaterialDescription,
          '11' AS AccountNo, AllocationDate as StartTime, AllocationDate as Endtime,
            'N' as IsCalculated, '1' as Chargedas, 'Units' as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              getdate()  RecentlyPostedDate , AllocationDate PostingDateTime,
               '0' AS'DueHours', 
              '0' AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName 
         from (   
          select ( count(1) -( select top 1  TotalTugs from ResourceAllocationConfigRule cr 
                          where EffectedFrom <= RA.AllocationDate and portcode = @portcode
                          order by EffectedFrom desc )) as extratugs
                          ,MT.SubCatName,
                          RA.ServiceReferenceID , SR.VCN , RA.AllocationDate
          from ResourceAllocation RA 
          inner join ServiceRequest SR on  SR.ServiceRequestID =  RA.ServiceReferenceID  
          INNER JOIN SubCategory MT ON SR.MovementType = MT.SubCatCode 
          where RA.ServiceReferenceType = 'VTSR' and RA.OperationType   = 'TGWR' and  SR.VCN  = @VCN 
          group by MT.SubCatName,RA.ServiceReferenceID  , SR.VCN ,  RA.AllocationDate
          )t where extratugs > 0
          
   UNION 
       -- MAIN ENGINE DISPLAY
        select '1' ISPOSTED , 1 ResourceAllocationID,
          VCN ,  'DISPLAYONLY' as MovementName, 
          'No Main Engine'   as ServiceName,
          '11' GroupCode, '11' MaterialCode, upper(SubCatName) MaterialDescription,
          '11' AS AccountNo, MovementDateTime as StartTime, MovementDateTime as Endtime,
            'N' as IsCalculated, '1' as Chargedas, 'Units' as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              getdate()  RecentlyPostedDate , MovementDateTime PostingDateTime,
               '0' AS'DueHours', 
              '0' AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName 
         from (   
            select SR.VCN,MT.SubCatName, SR.MovementDateTime 
            from ServiceRequest SR
            inner join SubCategory MT on SR.MovementType = MT.SubCatCode
            where SR.NoMainEngine = 'Y'  AND SR.VCN = @VCN 
          )t 
       
   UNION 
      -- WAITING BERTH MASTER DISPLAY
   select '1' ISPOSTED , 1 ResourceAllocationID,
          VCN ,  'DISPLAYONLY' as MovementName, 
          Reason   as ServiceName,
          '11' GroupCode, '11' MaterialCode, upper(srvname) MaterialDescription,
          '11' AS AccountNo, AllocationDate as StartTime, AllocationDate as Endtime,
            'N' as IsCalculated, '1' as Chargedas, 'Units' as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              getdate()  RecentlyPostedDate , AllocationDate PostingDateTime,
               '0' AS'DueHours', 
              '0' AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName 
         from (   
         
               select sr.VCN,concat(sb.SubCatName ,' - ' ,OT.SubCatName) srvname,
               RA.AllocationDate,
               --concat('Delay From: ', BT.WaitingStartTime ,' To: ' , BT.WaitingEndTime, '   ', BT.DelayReason) AS Reason
                concat('Delay From: ', dbo.udf_FormatDateTime(BT.WaitingStartTime,'YYYY-MM-DD'),
                     ' ',dbo.udf_FormatDateTime(BT.WaitingStartTime,'HH:MM 24'),' To: ' ,
                     dbo.udf_FormatDateTime(BT.WaitingEndTime,'YYYY-MM-DD'),
                     ' ',dbo.udf_FormatDateTime(BT.WaitingEndTime,'HH:MM 24')
                     , '   ',
                     BT.DelayReason) AS Reason
               --BT.WaitingStartTime, BT.WaitingEndTime, BT.DelayReason
      from ResourceAllocation RA
      inner join ServiceRequest SR on SR.ServiceRequestID = RA.ServiceReferenceID
      inner join ShiftingBerthingTaskExecution BT on BT.ResourceAllocationID = RA.ResourceAllocationID
      INNER JOIN SubCategory sb ON sb.SubCatCode = SR.MovementType
      INNER JOIN SubCategory OT ON OT.SubCatCode = RA.OperationType
      where RA.ServiceReferenceType = 'VTSR' AND SR.VCN =  @VCN 
      AND BT.WaitingStartTime IS NOT NULL AND   BT.WaitingEndTime IS NOT NULL
      and  (ra.taskstatus='COMP' or RA.taskstatus ='VERF' )
      )t 

      UNION 
        -- WAITING PILOT DISPLAY
        
      select '1' ISPOSTED , 1 ResourceAllocationID,
                VCN ,  'DISPLAYONLY' as MovementName, 
                Reason   as ServiceName,
                '11' GroupCode, '11' MaterialCode, upper(srvname) MaterialDescription,
                '11' AS AccountNo, AllocationDate as StartTime, AllocationDate as Endtime,
                  'N' as IsCalculated, '1' as Chargedas, 'Units' as UOM,
                    '' as MovementType,'' as ServiceType,
                    '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                    getdate()  RecentlyPostedDate , AllocationDate PostingDateTime,
                     '0' AS'DueHours', 
                    '0' AS 'TotalHours', '0' CloseMterReding, 
                       '0' as startmtrreding,
                        '' MeterSerialNo, '' BerthName 
               from (   
               
                     select sr.VCN,concat(sb.SubCatName ,' - ' ,OT.SubCatName) srvname,
                     RA.AllocationDate,
                    -- concat('Delay From: ', BT.WaitingStartTime ,' To: ' , BT.WaitingEndTime, '   ', BT.DelayReason) AS Reason
                     concat('Delay From: ', dbo.udf_FormatDateTime(BT.WaitingStartTime,'YYYY-MM-DD'),
                     ' ',dbo.udf_FormatDateTime(BT.WaitingStartTime,'HH:MM 24'),' To: ' ,
                     dbo.udf_FormatDateTime(BT.WaitingEndTime,'YYYY-MM-DD'),
                     ' ',dbo.udf_FormatDateTime(BT.WaitingEndTime,'HH:MM 24')
                     , '   ',
                     BT.DelayReason) AS Reason
                     --BT.WaitingStartTime, BT.WaitingEndTime, BT.DelayReason
            from ResourceAllocation RA
            inner join ServiceRequest SR on SR.ServiceRequestID = RA.ServiceReferenceID
            inner join PilotageServiceRecording BT on BT.ResourceAllocationID = RA.ResourceAllocationID
            INNER JOIN SubCategory sb ON sb.SubCatCode = SR.MovementType
               INNER JOIN SubCategory OT ON OT.SubCatCode = RA.OperationType
            where RA.ServiceReferenceType = 'VTSR' AND SR.VCN =  @VCN 
            AND BT.WaitingStartTime IS NOT NULL AND  BT.WaitingEndTime IS NOT NULL
            and  (ra.taskstatus='COMP' or RA.taskstatus ='VERF' )
      )t 

      union 
 
        -- WAITING TUG DISPLAY
      select '1' ISPOSTED , 1 ResourceAllocationID,
                VCN ,  'DISPLAYONLY' as MovementName, 
                Reason   as ServiceName,
                '11' GroupCode, '11' MaterialCode, upper(srvname) MaterialDescription,
                '11' AS AccountNo, AllocationDate as StartTime, AllocationDate as Endtime,
                  'N' as IsCalculated, '1' as Chargedas, 'Units' as UOM,
                    '' as MovementType,'' as ServiceType,
                    '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                    getdate()  RecentlyPostedDate , AllocationDate PostingDateTime,
                     '0' AS'DueHours', 
                    '0' AS 'TotalHours', '0' CloseMterReding, 
                       '0' as startmtrreding,
                        '' MeterSerialNo, '' BerthName 
               from (   
               
                     select sr.VCN,concat(sb.SubCatName ,' - ' ,OT.SubCatName) srvname,
                     RA.AllocationDate,
                     -- concat('Delay From: ', BT.WaitingStartTime ,' To: ' , BT.WaitingEndTime, '   ', BT.DelayReason) AS Reason
                       concat('Delay From: ', dbo.udf_FormatDateTime(BT.WaitingStartTime,'YYYY-MM-DD'),
                     ' ',dbo.udf_FormatDateTime(BT.WaitingStartTime,'HH:MM 24'),' To: ' ,
                     dbo.udf_FormatDateTime(BT.WaitingEndTime,'YYYY-MM-DD'),
                     ' ',dbo.udf_FormatDateTime(BT.WaitingEndTime,'HH:MM 24')
                     , '   ',
                     BT.DelayReason) AS Reason
                     --BT.WaitingStartTime, BT.WaitingEndTime, BT.DelayReason
            from ResourceAllocation RA
            inner join ServiceRequest SR on SR.ServiceRequestID = RA.ServiceReferenceID
            inner join OtherServiceRecording BT on BT.ResourceAllocationID = RA.ResourceAllocationID
            INNER JOIN SubCategory sb ON sb.SubCatCode = SR.MovementType
            INNER JOIN SubCategory OT ON OT.SubCatCode = RA.OperationType
            where RA.ServiceReferenceType = 'VTSR' AND SR.VCN =  @VCN 
            AND BT.WaitingStartTime IS NOT NULL AND  BT.WaitingEndTime IS NOT NULL
            and  (ra.taskstatus='COMP' or RA.taskstatus ='VERF' )
      )t 


    UNION 
          -- PASSENGER BAGGAGE
        select 
         isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,
        SR.ServiceRequestid ResourceAllocationID,
        AN.VCN, SB.SubCatName as MovementName,upper(M.MaterialDescription) as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, upper(M.MaterialDescription) MaterialDescription ,
        CASE
        WHEN (RPD.RevenuePostingDtlID IS NULL)
           THEN ''
        WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
           THEN RP.SAPAccNo
        END AS AccountNo,
         SR.MovementDateTime as StartTime,SR.MovementDateTime as Endtime,'N' as IsCalculated,
         convert(varchar,ISNULL(SR.PassengersEmbarking,0) + ISNULL(SR.PassengersDisembarking,0)) as Chargedas
         ,M.UOM as UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RPD.PostedOn RecentlyPostedDate, getdate() PostingDateTime,
         convert(varchar,ISNULL(SR.PassengersEmbarking,0) + ISNULL(SR.PassengersDisembarking,0))  AS 'DueHours',
         '1'  AS 'TotalHours'
         , '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
        from ArrivalNotification an 
        inner join ServiceRequest SR on an.vcn = sr.vcn
        inner join SubCategory SB on SB.SubCatCode = SR.MovementType
        INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
        INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='BAGG'
        left join RevenuePostingDtl RPD on RPD.vcn = AN.VCN  
                                            and rpd.ReferenceID =  SR.ServiceRequestid
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where   SR.MovementType = 'SGMV' 
            AND (ISNULL(SR.PassengersEmbarking,0) > 0 OR ISNULL(SR.PassengersDisembarking,0) >0)
            AND AN.VCN  = @VCN
            
     UNION 
          -- PASSENGER LEVY
        select 
         isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,
        SR.ServiceRequestid ResourceAllocationID,
        AN.VCN, SB.SubCatName as MovementName,upper(M.MaterialDescription) as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, upper(M.MaterialDescription) MaterialDescription ,
        CASE
        WHEN (RPD.RevenuePostingDtlID IS NULL)
           THEN ''
        WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
           THEN RP.SAPAccNo
        END AS AccountNo,
         SR.MovementDateTime as StartTime,SR.MovementDateTime as Endtime,'N' as IsCalculated,
         convert(varchar,ISNULL(SR.PassengersEmbarking,0) + ISNULL(SR.PassengersDisembarking,0)) as Chargedas
         ,M.UOM as UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RPD.PostedOn RecentlyPostedDate, getdate() PostingDateTime,
          convert(varchar,ISNULL(SR.PassengersEmbarking,0) + ISNULL(SR.PassengersDisembarking,0))  AS 'DueHours',  '1'  AS 'TotalHours'
         , '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
        from ArrivalNotification an 
        inner join ServiceRequest SR on an.vcn = sr.vcn
        inner join SubCategory SB on SB.SubCatCode = SR.MovementType
        INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
        INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='LEVY'
        left join RevenuePostingDtl RPD on RPD.vcn = AN.VCN  
                                            and rpd.ReferenceID =  SR.ServiceRequestid
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where   SR.MovementType = 'SGMV' 
            AND (ISNULL(SR.PassengersEmbarking,0) > 0 OR ISNULL(SR.PassengersDisembarking,0) >0)
            AND AN.VCN  = @VCN


	UNION 

	  
           select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED , VC.VesselCallMovementID ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                   --SB1.SubCatName as ServiceName,
                   upper(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,upper(M.MaterialDescription) MaterialDescription  ,
                     CASE
                            WHEN (RPD.RevenuePostingDtlID IS NULL)
                               THEN ''
                            WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
                               THEN RP.SAPAccNo
                            END AS AccountNo,
                             VC.ATB  StartTime, VC.ATUB  Endtime,
                              M.IsCalculated, 
                               m.Chargedas ,M.UOM  ,
                            VC.MovementType  ,M.ServiceType   ,
                            'VTSR' ServiceReferenceType,''  OperationType,'' TaskStatus,
                            PostedOn RecentlyPostedDate, getdate() PostingDateTime,
                             m.Chargedas AS 'DueHours', 
                             1 AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
              from VesselCallMovement VC 
                  INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN 
                  inner join MaterialCodeMaster M on m.PortCode = VC.FromPositionPortCode and
                                           M.QuayCode = VC.FromPositionQuayCode  and
                                           M.BerthCode= VC.FromPositionBerthCode  and
										    M.MovementType = VC.MovementType and
                                            M.ChargedFor='WBCA' 
                  INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
                  AND MP.PortCode = @portcode
                   inner join SubCategory S on S.SubCatCode = VC.MovementType
                 
                   left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  VC.VesselCallMovementID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
                  left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
                           
                  WHERE 
                 VC.VCN  =   @VCN
                  AND VC.MovementType ='ARMV'


UNION 


           select isnull(RPD.RevenuePostingDtlID,'0') ISPOSTED ,
		    (select VesselCallMovementID from VesselCallMovement VC  
														where VC.VCN  = @VCN
													AND   VC.MovementType ='SGMV' ) AS ResourceAllocationID,

                   VC.VCN,S.SubCatName AS MovementName ,
                   --SB1.SubCatName as ServiceName,
                   upper(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,upper(M.MaterialDescription) MaterialDescription  ,
                     CASE
                            WHEN (RPD.RevenuePostingDtlID IS NULL)
                               THEN ''
                            WHEN (RPD.RevenuePostingDtlID IS NOT NULL)
                               THEN RP.SAPAccNo
                            END AS AccountNo,
                             VC.ATB  StartTime, VC.ATUB  Endtime,
                              M.IsCalculated, 
                                m.Chargedas,M.UOM  ,
                           'SGMV' AS MovementType  ,M.ServiceType   ,
                            'VTSR' ServiceReferenceType,''  OperationType,'' TaskStatus,
                            PostedOn RecentlyPostedDate, getdate() PostingDateTime,
                            m.Chargedas AS 'DueHours', 
                             m.Chargedas  AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
              from VesselCallMovement VC 
			          INNER JOIN (  
									SELECT  TOP(1) VesselCallMovementID FROM VesselCallMovement nvc WHERE 
									NVC.VCN  =  @VCN AND
									VesselCallMovementID <  
												 (select VesselCallMovementID from VesselCallMovement VC  
														where VC.VCN  =  @VCN
													AND   VC.MovementType ='SGMV' 
												  ) 
									ORDER BY 1 DESC 
			  
								) AS NWVCN  ON NWVCN.VesselCallMovementID  = VC.VesselCallMovementID 

                  INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN 
                  inner join MaterialCodeMaster M on m.PortCode = VC.FromPositionPortCode and
                                           M.QuayCode = VC.FromPositionQuayCode  and
                                           M.BerthCode= VC.FromPositionBerthCode  and
										    M.MovementType = VC.MovementType and
                                            M.ChargedFor='WBCA' 
                  INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
                  AND MP.PortCode = @portcode
                   inner join SubCategory S on S.SubCatCode = 'SGMV' 
                 
                   left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  VC.VesselCallMovementID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
                  left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
                           
                  WHERE 
                 VC.VCN  =   @VCN
                 



						

               
   
End
GO
ALTER PROCEDURE [dbo].[usp_RevenueDtls_View]
@portcode nvarchar(2), @revenuePostingid int
WITH 
EXECUTE AS CALLER
AS
BEGIN
         -- FOR PORT DUES
      
       
        select '1' ISPOSTED , RevenuePostingDtlID ResourceAllocationID,V.VCN,
        'PORTDUESVIEW' as MovementName,'PORT DUES' as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, UPPER(M.MaterialDescription) MaterialDescription ,
              hdr.SAPAccNo AS AccountNo,
              hdr.CreatedDate  as StartTime,V.BreakWaterOut as Endtime,'N' as IsCalculated,M.Chargedas,M.UOM  ,
               '' as MovementType,'' as ServiceType,
               '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
               dtl.PostingFrom RecentlyPostedDate,
                dtl.PostedOn PostingDateTime,
                
                    cast( REPLACE(dtl.Units ,',','.') as numeric(10,3)) AS 'TotalHours',
                  cast( REPLACE(dtl.Units ,',','.') as numeric(10,3)) AS 'DueHours'
               ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
        from 
         VesselCall V 
         INNER JOIN RevenuePosting hdr on hdr.vcn = V.VCN 
        inner join RevenuePostingDtl dtl on dtl.RevenuePostingID = hdr.RevenuePostingID 
                       --   and dtl.GroupCode ='900002' and dtl.MaterialCode = '132'  
       INNER JOIN MaterialCodeMaster M ON M.MaterialCode   = dtl.MaterialCode and M.GroupCode = dtl.GroupCode
                                        and   M.ChargedFor ='PODU'
        INNER JOIN MaterialCodePort MP on M.MaterialCodeMasterid = MP.MaterialCodeMasterid 
                                          and  MP.PortCode = @portcode
        where   hdr.RevenuePostingID =  @revenuePostingid
       
	   
	    UNION           
         -- for BERTH DUES

		 select '1' ISPOSTED , RevenuePostingDtlID ResourceAllocationID,V.VCN,
        'PORTDUESVIEW' as MovementName,'BERTH DUES' as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, UPPER(M.MaterialDescription) MaterialDescription ,
              hdr.SAPAccNo AS AccountNo,
              hdr.CreatedDate  as StartTime,V.BreakWaterOut as Endtime,'N' as IsCalculated,M.Chargedas,M.UOM  ,
               '' as MovementType,'' as ServiceType,
               '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
               dtl.PostingFrom RecentlyPostedDate,
                dtl.PostedOn PostingDateTime,
                
                    cast( REPLACE(dtl.Units ,',','.') as numeric(10,3)) AS 'TotalHours',
                  cast( REPLACE(dtl.Units ,',','.') as numeric(10,3)) AS 'DueHours'
               ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
        from 
         VesselCall V 
         INNER JOIN RevenuePosting hdr on hdr.vcn = V.VCN 
        inner join RevenuePostingDtl dtl on dtl.RevenuePostingID = hdr.RevenuePostingID 
                       --   and dtl.GroupCode ='900002' and dtl.MaterialCode = '132'  
       INNER JOIN MaterialCodeMaster M ON M.MaterialCode   = dtl.MaterialCode and M.GroupCode = dtl.GroupCode
                                        and   M.ChargedFor ='BRTH'
        INNER JOIN MaterialCodePort MP on M.MaterialCodeMasterid = MP.MaterialCodeMasterid 
                                          and  MP.PortCode = @portcode
        where   hdr.RevenuePostingID =  @revenuePostingid
		  UNION   
    ------------ NEW 22 MAY 2015
    
    --  REFUSE REMOVAL
                select '1' ISPOSTED  , RevenuePostingDtlID ResourceAllocationID,
                 VC.VCN,'PORTDUESVIEW' MovementName , 'REFUSE REMOVAL' ServiceName,
                   M.GroupCode, M.MaterialCode  , UPPER(M.MaterialDescription) MaterialDescription ,
                   RP.SAPAccNo AS AccountNo,
				   RP.CreatedDate  as StartTime,VC.BreakWaterOut as Endtime,'N' as IsCalculated,M.Chargedas,M.UOM  ,
               '' as MovementType,'' as ServiceType,
               '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
               RPD.PostingFrom RecentlyPostedDate,
                RPD.PostedOn PostingDateTime,
                
                    cast( REPLACE(RPD.Units ,',','.') as numeric(10,3)) AS 'TotalHours',
                  cast( REPLACE(RPD.Units ,',','.') as numeric(10,3)) AS 'DueHours'
               ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
                          -- VC.BreakWaterIn StartTime, VC.BreakWaterOut Endtime,
                          --  M.IsCalculated, RPD.Units Chargedas,M.UOM  ,
                          --'' MovementType  , '' ServiceType   ,
                          --'' ServiceReferenceType, '' OperationType, '' TaskStatus,
                          -- RP.PostedDate RecentlyPostedDate, 
                          --   rpd.PostedOn PostingDateTime,
                          -- cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours',  
                          -- cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS 'TotalHours'
                          --, '0' CloseMterReding, 
                          --       '0' as startmtrreding,
                          --   ''  MeterSerialNo, '' BerthName
                   
                from VesselCall VC
                inner join ArrivalNotification AN ON AN.VCN = VC.VCN
                INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
                INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid 
                                                   AND M.ChargedFor='REFU'
                INNER join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                              and rpd.ReferenceID =  VC.VesselCallid
                                                              and rpd.GroupCode =M.GroupCode
                                                              and rpd.MaterialCode = M.MaterialCode 
                INNER join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID    
                WHERE    RP.RevenuePostingID = @revenuePostingid

/*
        select '1' ISPOSTED ,V.VesselCallID ResourceAllocationID,
        V.VCN, 'Arrival' as MovementName,UPPER(M.MaterialDescription) as ServiceName,
        M.GroupCode GroupCode,  M.MaterialCode MaterialCode, UPPER(M.MaterialDescription) MaterialDescription ,
       RP.SAPAccNo AS AccountNo,
          V.ATB as StartTime,V.ATUB as Endtime,'N' as IsCalculated,RPD.Units as Chargedas,RPD.UOM as UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RP.PostedDate RecentlyPostedDate,   rpd.PostedOn PostingDateTime, 
           cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours', 
           cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
           ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
        from VesselCall V 
          inner join ArrivalNotification an on an.VCN = V.VCN
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='BRTH'
        INNER join RevenuePostingDtl RPD on RPD.vcn = V.VCN  
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       INNER join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where  RP.RevenuePostingID = @revenuePostingid  -- 'VCNDB2014190' 'VCNDB2014289' 
        */
        union
           -- FOR VTS CHARGES ONLY FOR DURBON PORT
        select '1' ISPOSTED ,V.VesselCallID ResourceAllocationID,
        V.VCN, 'Arrival' as MovementName,'VTS CHARGES' as ServiceName,
        M.GroupCode GroupCode,  M.MaterialCode MaterialCode, UPPER(M.MaterialDescription) MaterialDescription ,
       RP.SAPAccNo AS AccountNo,
          V.ATA as StartTime,V.ATD as Endtime,'N' as IsCalculated,RPD.Units as Chargedas,RPD.UOM as UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RP.PostedDate RecentlyPostedDate, 
            RPD.PostedOn PostingDateTime,
           cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours', 
           cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
           ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
        from VesselCall V 
          inner join ArrivalNotification an on an.VCN = V.VCN
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='VTCH'
        INNER join RevenuePostingDtl RPD on RPD.vcn = V.VCN  
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       INNER join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where  RP.RevenuePostingID = @revenuePostingid  -- 'VCNDB2014190' 'VCNDB2014289'
  
                
UNION
      /*  -- As per the Discussion with Vasu and Vinoda commented on 06-oct-2015 
      -- Fire Protection Only
      
             select RPD.RevenuePostingDtlID ISPOSTED , VC.VesselCallMovementID ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                  UPPER(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,UPPER(M.MaterialDescription) MaterialDescription ,
                     RP.SAPAccNo AccountNo,
                             VC.ATB  StartTime, VC.ATUB  Endtime,
                              M.IsCalculated, 
                               RPD.Units  Chargedas,M.UOM  ,
                            VC.MovementType  ,M.ServiceType   ,
                            'VTSR' ServiceReferenceType,''  OperationType,'' TaskStatus,
                             RP.PostedDate  RecentlyPostedDate, 
                              rpd.PostedOn PostingDateTime,
                           cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours',
                           cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
              from VesselCallMovement VC 
               INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN AND AN.IsIMDGANFinal = 'Y'
               INNER JOIN MaterialCodePort MP on AN.PortCode = MP.PortCode
               inner join MaterialCodeMaster M on  MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
                                            and M.ChargedFor='FIPR' 
               inner join SubCategory S on S.SubCatCode = VC.MovementType
               inner join SubCategory SB1 on SB1.SubCatCode = M.ServiceType
               inner join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  VC.VesselCallMovementID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
               inner join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
               WHERE  RP.RevenuePostingID = @revenuePostingid
      
   
        
UNION 
         */
           -- Fire Protection plus Security
             
                               
        select RPD.RevenuePostingDtlID ISPOSTED , VC.VesselCallMovementID ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                   UPPER(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  , UPPER(M.MaterialDescription) MaterialDescription ,
                     RP.SAPAccNo AS AccountNo,
                             VC.ATB  StartTime, VC.ATUB  Endtime,
                              M.IsCalculated, 
                                RPD.Units Chargedas,M.UOM  ,
                            VC.MovementType  ,M.ServiceType   ,
                            'VTSR' ServiceReferenceType,''  OperationType,'' TaskStatus,
                            RP.PostedDate RecentlyPostedDate,
                             rpd.PostedOn PostingDateTime,
                             cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours',  
                             cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
              from VesselCallMovement VC 
                  INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN AND AN.IsIMDGANFinal = 'Y'
                  inner join MaterialCodeMaster M on m.PortCode = VC.FromPositionPortCode and
                                            M.QuayCode = VC.FromPositionQuayCode  and
                                            M.BerthCode= VC.FromPositionBerthCode  and
                                            M.ChargedFor='FPPS' 
                  INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
                  AND MP.PortCode ='DB'
                   inner join SubCategory S on S.SubCatCode = VC.MovementType
                   inner join SubCategory SB1 on SB1.SubCatCode = M.ServiceType
                   inner join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  VC.VesselCallMovementID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
                  inner join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
                           
                  WHERE  RP.RevenuePostingID = @revenuePostingid
      
      UNION
      
      -- CREW TRANS PORTATION
                  select RPD.RevenuePostingDtlID ISPOSTED , RA.ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                    UPPER(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,UPPER(M.MaterialDescription) MaterialDescription ,
                     RP.SAPAccNo AS AccountNo,
                             VC.ATB StartTime, VC.ATUB Endtime,
                              M.IsCalculated,  RPD.Units Chargedas,M.UOM  ,
                            VC.MovementType  ,M.ServiceType   ,
                            RA.ServiceReferenceType,RA.OperationType,RA.TaskStatus,
                             RP.PostedDate RecentlyPostedDate, 
                              rpd.PostedOn PostingDateTime,
                             cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours',  
                             cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
                     
                  from ShiftingBerthingTaskExecution SB
                  inner join ResourceAllocation RA on RA.ResourceAllocationID = SB.ResourceAllocationID
                  Inner join VesselCallMovement VC on VC.ServiceRequestID = RA.ServiceReferenceID 
                  inner join MaterialCodeMaster M on m.PortCode = SB.FromBerthPortCode  and
                                            M.QuayCode = SB.FromBerthQuayCode  and
                                            M.BerthCode= SB.FromBerthCode and
                                            M.ChargedFor='ILND' 
                  INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
                                            AND MP.PortCode =@portcode
                   inner join SubCategory S on S.SubCatCode = VC.MovementType
                   inner join SubCategory SB1 on SB1.SubCatCode = M.ServiceType
                   INNER join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  RA.ResourceAllocationID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
                  INNER join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
                           
                  WHERE  RP.RevenuePostingID = @revenuePostingid
      UNION
    
    
      -- Running of Lines
                  select RPD.RevenuePostingDtlID ISPOSTED , RA.ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                    UPPER(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,UPPER(M.MaterialDescription) MaterialDescription ,
                     RP.SAPAccNo AS AccountNo,
                     VC.ATB StartTime, VC.ATUB Endtime,
                     M.IsCalculated,  RPD.Units Chargedas ,M.UOM  ,
                     VC.MovementType  ,M.ServiceType   ,
                     RA.ServiceReferenceType,RA.OperationType,RA.TaskStatus,
                      RP.PostedDate RecentlyPostedDate, 
                       rpd.PostedOn PostingDateTime,
                       cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours', 
                       cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
                     
                  from ShiftingBerthingTaskExecution SB
                  inner join ResourceAllocation RA on RA.ResourceAllocationID = SB.ResourceAllocationID
                  Inner join VesselCallMovement VC on VC.ServiceRequestID = RA.ServiceReferenceID 
                  inner join MaterialCodeMaster M on m.PortCode = SB.FromBerthPortCode and
                                            M.QuayCode = SB.FromBerthQuayCode  and
                                            M.BerthCode= SB.FromBerthCode and
                                            M.ChargedFor='RNOF' 
                  INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
                                      AND MP.PortCode =@portcode
                   inner join SubCategory S on S.SubCatCode = VC.MovementType
                   inner join SubCategory SB1 on SB1.SubCatCode = M.ServiceType
                   inner join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                    and rpd.ReferenceID =  RA.ResourceAllocationID
                                                    and rpd.GroupCode =M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode 
                  inner join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
                  WHERE  RP.RevenuePostingID = @revenuePostingid
                 
    
        UNION
    
              -- FOR SAMSA LEVY CHARGES
        select '1' ISPOSTED ,V.VesselCallID ResourceAllocationID,
        V.VCN, 
        'Arrival' as MovementName,UPPER(M.MaterialDescription) as ServiceName,
        M.GroupCode GroupCode,  M.MaterialCode MaterialCode, UPPER(M.MaterialDescription) MaterialDescription ,
       RP.SAPAccNo AS AccountNo,
          V.ATA as StartTime,V.ATD as Endtime,'N' as IsCalculated,RPD.Units as Chargedas,RPD.UOM as UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RP.PostedDate RecentlyPostedDate,  
           rpd.PostedOn PostingDateTime,
           cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS 'DueHours',  
          cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'TotalHours'
           ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
        from VesselCall V 
          inner join ArrivalNotification an on an.VCN = V.VCN
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='SMSA'
        INNER join RevenuePostingDtl RPD on RPD.vcn = V.VCN  
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       INNER join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where  RP.RevenuePostingID = @revenuePostingid
            
            
            
            
     UNION        
              -- FOR LIGHT CHARGES
        select '1' ISPOSTED ,V.VesselCallID ResourceAllocationID,
        V.VCN, 
        'Arrival' as MovementName,UPPER(M.MaterialDescription) as ServiceName,
        M.GroupCode GroupCode,  M.MaterialCode MaterialCode, UPPER(M.MaterialDescription) MaterialDescription ,
       RP.SAPAccNo AS AccountNo,
          V.ATA as StartTime,V.ATD as Endtime,'N' as IsCalculated,RPD.Units as Chargedas,RPD.UOM as UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RPD.PostedOn RecentlyPostedDate,  rpd.PostedOn PostingDateTime, 
          cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'DueHours',  
          cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'TotalHours'
           ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
        from VesselCall V 
          inner join ArrivalNotification an on an.VCN = V.VCN
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='LIDC'--'SMSA'
        INNER join RevenuePostingDtl RPD on RPD.vcn = V.VCN  
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       INNER join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where  RP.RevenuePostingID = @revenuePostingid
            
        union 
      -- IS SPECIAL NATURE
         
        select RPD.RevenuePostingDtlID ISPOSTED ,
        VesselCallID ResourceAllocationID, V.VCN, 'Arrival' as MovementName,
        upper(M.MaterialDescription) as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, upper(M.MaterialDescription) MaterialDescription  ,
         RP.SAPAccNo AS AccountNo,
          v.ATA as StartTime,v.ATD as Endtime,'N' as IsCalculated,RPD.Units Chargedas,RPD.UOM UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RPD.PostedOn RecentlyPostedDate,   rpd.PostedOn PostingDateTime,
          cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'DueHours',  
          cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS 'TotalHours'
         , '0' CloseMterReding, '0' as startmtrreding,'' MeterSerialNo, '' BerthName
        from VesselCall V 
        inner join ArrivalNotification an on an.VCN = V.VCN 
          INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
       INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='SPCL'
       inner join RevenuePostingDtl RPD on RPD.vcn = V.VCN  
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       inner join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
             where  RP.RevenuePostingID = @revenuePostingid
            
        union 
        
         -- FOR VTS SERVICES ARRV,SHIFT,WARP,SAIL 
          SELECT RPD.RevenuePostingDtlID ISPOSTED , T.ResourceAllocationID,
          RP.VCN,SB.SubCatName AS MovementName ,UPPER(SB1.SubCatName) as ServiceName,
          M.GroupCode, M.MaterialCode  ,UPPER(M.MaterialDescription) MaterialDescription ,
          RP.SAPAccNo AS AccountNo,
          T.StartTime,T.Endtime,
            M.IsCalculated,  RPD.Units  AS Chargedas ,M.UOM  ,
          M.MovementType  ,M.ServiceType   ,
         '' as ServiceReferenceType, M.ServiceType as OperationType ,'' as TaskStatus,
             RP.PostedDate RecentlyPostedDate,  rpd.PostedOn PostingDateTime,
             cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS 'DueHours',
             cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS 'TotalHours'
              ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 TB.BerthName  MeterSerialNo, FB.BerthName BerthName
                              
           from  RevenuePosting RP           
           inner join RevenuePostingDtl RPD on RP.RevenuePostingID = RPD.RevenuePostingID 
           inner join dbo.ResourceAllocation T on  rpd.ReferenceID =  T.ResourceAllocationID
           INNER JOIN MaterialCodeMaster M ON rpd.MomentType = M.MovementType 
                                            and rpd.ServiceType = M.ServiceType
                                            and rpd.GroupCode =M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode 
          INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          inner join SubCategory SB on SB.SubCatCode = M.MovementType
          inner join SubCategory SB1 on SB1.SubCatCode = M.ServiceType
          LEFT JOIN ShiftingBerthingTaskExecution sbr ON  T.ResourceAllocationID = sbr.ResourceAllocationID
          left join Berth FB ON FB.BerthCode = sbr.FromBerthCode and FB.QuayCode = sbr.FromBerthQuayCode 
                                  AND FB.PortCode =  sbr.FromBerthPortCode 
          left join Berth TB ON TB.BerthCode = sbr.ToBerthCode and TB.QuayCode = sbr.ToBerthQuayCode 
                    AND TB.PortCode =  sbr.ToBerthPortCode 
          WHERE RP.RevenuePostingID = @revenuePostingid
          --order by M.MovementType  , SB1.SubCatName
        
        
     
     

      UNION
         -- DRY DOCK BOOKING FEES
                select 
                RPD.RevenuePostingDtlID ISPOSTED , SuppDryDockID ResourceAllocationID,
                SD.VCN ,  'DRYDOCKSERVICES' as MovementName,UPPER(M.MaterialDescription) as ServiceName,
                M.GroupCode,  M.MaterialCode, UPPER(M.MaterialDescription) MaterialDescription,
                RPH.SAPAccNo AS AccountNo,
                   SD.FromDate  as StartTime,SD.ToDate as Endtime,
                   M.IsCalculated as IsCalculated,RPD.Units as Chargedas,M.UOM  as UOM,
                    '' as MovementType,'' as ServiceType,
                    '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                    RPD.PostedOn  RecentlyPostedDate,  rpd.PostedOn PostingDateTime,
                    cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours', 
                    cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS 'TotalHours'
                     ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '1' MeterSerialNo, '' BerthName
                from SuppDryDock SD
                inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                           AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                           and  M.ChargedFor = 'DKBK'
                inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                inner join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                            and rpd.GroupCode = M.GroupCode
                                                          and rpd.MaterialCode = M.MaterialCode    
                inner JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                                and RPD.VCN = SD.VCN
                     WHERE RPH.RevenuePostingID = @revenuePostingid
       union 
             -- PREPARATION COST OF DRY DOCK
           select 
              RPD.RevenuePostingDtlID ISPOSTED , SuppDryDockID ResourceAllocationID,
              SD.VCN ,  'DRYDOCKSERVICES' as MovementName,UPPER(M.MaterialDescription) as ServiceName,
              M.GroupCode,  M.MaterialCode, UPPER(M.MaterialDescription) MaterialDescription,
              RPH.SAPAccNo AS AccountNo,
                 SD.FromDate  as StartTime,SD.ToDate as Endtime,
                 M.IsCalculated as IsCalculated,M.Chargedas as Chargedas,M.UOM  as UOM,
                  '' as MovementType,'' as ServiceType,
                  '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                  RPD.PostedOn  RecentlyPostedDate,  rpd.PostedOn PostingDateTime,
                      cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours',
                      cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS 'TotalHours'
                      , '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '2' MeterSerialNo, '' BerthName
              from SuppDryDock SD
              inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                         AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                         and  M.ChargedFor = 'PREP'
              inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
              Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                          and rpd.GroupCode = M.GroupCode
                                                        and rpd.MaterialCode = M.MaterialCode    
              LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                              and RPD.VCN = SD.VCN
                    WHERE RPH.RevenuePostingID = @revenuePostingid
       UNION 
    -- DOCKING FEES
       select 
          RPD.RevenuePostingDtlID ISPOSTED , SuppDryDockID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKSERVICES' as MovementName, UPPER(M.MaterialDescription)  as ServiceName,
          M.GroupCode,  M.MaterialCode,UPPER(M.MaterialDescription) MaterialDescription,
          RPH.SAPAccNo AS AccountNo,
             SD.EnteredDockDateTime  as StartTime,SD.LeftDockDateTime as Endtime,
             M.IsCalculated as IsCalculated,RPD.Units as Chargedas,M.UOM  as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate,  rpd.PostedOn PostingDateTime, 
             cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'DueHours',  
             cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'TotalHours'
              ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '3' MeterSerialNo, '' BerthName
          from SuppDryDock SD
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'DOCK'
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          inner join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode    
          inner JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
         WHERE RPH.RevenuePostingID = @revenuePostingid
         
       union 
     -- DRY DOCK 24 HOURS
                          select 
                 RPD.RevenuePostingDtlID  ISPOSTED , SuppDryDockID ResourceAllocationID,
                SD.VCN ,  'DRYDOCKSERVICES' as MovementName,'FIRST 24 HR PERIOD' as ServiceName,
                M.GroupCode,  M.MaterialCode,UPPER(M.MaterialDescription) MaterialDescription,
                RPH.SAPAccNo AS AccountNo,
                   SD.EnteredDockDateTime  as StartTime,SD.LeftDockDateTime as Endtime,
                   M.IsCalculated as IsCalculated, RPD.Units  AS Chargedas,M.UOM ,
                    '' as MovementType,'' as ServiceType,
                    '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                    RPD.PostedOn  RecentlyPostedDate, rpd.PostedOn PostingDateTime,
                     cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'DueHours', 
                     cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'TotalHours'
                      ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '4' MeterSerialNo, '' BerthName
                from SuppDryDock SD
                inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                           AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                           and M.ChargedFor = 'DR24'
                inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                inner join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                            and rpd.GroupCode = M.GroupCode
                                                            and rpd.MaterialCode = M.MaterialCode    
                inner JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                                and RPD.VCN = SD.VCN
                      WHERE RPH.RevenuePostingID = @revenuePostingid
                                 
     
      
      
       UNION 
     -- DRY DOCK 12 Hours      
          select  '1' ISPOSTED,* from (
                    select ResourceAllocationID,VCN ,   MovementName, ServiceName,
                    GroupCode,MaterialCode,MaterialDescription,AccountNo,
                     StartTime,Endtime,IsCalculated, Chargedas, UOM, MovementType,ServiceType,
                     ServiceReferenceType,OperationType, TaskStatus,RecentlyPostedDate,
                      PostingDateTime,
                     
                       --cast(Chargedas as numeric(10,0)) AS 'DueHours',
                    --ceiling(DATEDIFF(mi,StartTime ,isnull(Endtime, RecentlyPostedDate)  )/720.0) AS 'TotalHours'
                    cast( REPLACE(Chargedas ,',','.') as numeric(10,0)) AS 'DueHours',
                    cast (ceiling(DATEDIFF(mi,StartTime ,isnull(Endtime, RecentlyPostedDate)  )/720.0) as numeric(10,0)) AS 'TotalHours'
                    
                     ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
                    from (
                    select 
                     SuppDryDockID ResourceAllocationID,
                    SD.VCN ,  'DRYDOCK12HRSSERVICES' as MovementName, UPPER(M.MaterialDescription) as ServiceName,
                    M.GroupCode,  M.MaterialCode,UPPER(M.MaterialDescription) MaterialDescription,
                    RPH.SAPAccNo AS AccountNo,
                       SD.EnteredDockDateTime+1  as StartTime,SD.LeftDockDateTime as Endtime,
                       M.IsCalculated as IsCalculated,RPD.Units as Chargedas,M.UOM  as UOM,
                        '' as MovementType,'' as ServiceType,
                        '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
                        RPD.PostedOn  RecentlyPostedDate,  rpd.PostedOn PostingDateTime
                    from SuppDryDock SD
                    inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                               AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                               and M.ChargedFor = 'DR12'
                               and SD.FromDate +1  <= getdate()
                    inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
                    INNER join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                                and rpd.GroupCode = M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode    
                    INNER JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                                    and RPD.VCN = SD.VCN
                     WHERE RPH.RevenuePostingID = @revenuePostingid
      
      
          ) n)k

    union 
        -- UNDOCKING FEES
      select 
           RPD.RevenuePostingDtlID ISPOSTED , SuppDryDockID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKSERVICES' as MovementName,UPPER(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,UPPER(M.MaterialDescription) MaterialDescription,
          RPH.SAPAccNo AS AccountNo,
             SD.EnteredDockDateTime  as StartTime,SD.LeftDockDateTime as Endtime,
             M.IsCalculated as IsCalculated,RPD.Units as Chargedas,M.UOM  as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate,  rpd.PostedOn PostingDateTime,
               cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'DueHours',  
               cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'TotalHours'
               ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '5' MeterSerialNo, '' BerthName
          from SuppDryDock SD
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'UNDK'
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          inner join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode    
          inner JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE RPH.RevenuePostingID = @revenuePostingid
      
       union 
     -- SUPPLIMANTORY SERVICE FOR WATER AND FLOOTING CRANES      
       select RPD.RevenuePostingDtlID  ISPOSTED ,a.ResourceAllocationID,
             sup.VCN, b.ServiceTypeName as MovementName ,
                    'SUPPLIMANT' as ServiceName,M.GroupCode, M.MaterialCode,
                    UPPER(M.MaterialDescription) as MaterialDescription,
                    RPH.SAPAccNo AccountNo, 
            oth.StartTime as StartTime,oth.EndTime as Endtime,M.IsCalculated, RPD.Units as Chargedas ,M.UOM,
            '' as MovementType,a.OperationType as ServiceType,
             '' as ServiceReferenceType,a.OperationType as OperationType, '' as TaskStatus,
                  RPD.PostedOn RecentlyPostedDate,   rpd.PostedOn PostingDateTime, 
                   cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS 'DueHours',
                 cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'TotalHours',
                 isnull(oth.ClosingMeterReading,0) as CloseMterReding, 
                 isnull(oth.OpeningMeterReading,0) as startmtrreding,
                 isnull(oth.MeterSerialNo, ' ') as MeterSerialNo,
                 BT.BerthName
            from dbo.ResourceAllocation a 
            inner join ServiceType b on b.ServiceTypeCode = a.OperationType 
            inner join dbo.SuppServiceRequest sup on  sup.SuppServiceRequestID = a.ServiceReferenceID
            inner join OtherServiceRecording oth on oth.ResourceAllocationID = a.ResourceAllocationID
            inner join MaterialCodeMaster M ON M.ServiceType = a.OperationType
            inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid AND MP.PortCode = @portcode
            left join dbo.Berth BT ON BT.PortCode = sup.PortCode AND BT.QuayCode = sup.QuayCode  AND BT.BerthCode = sup.BerthCode
         INNER join RevenuePostingDtl RPD on RPD.vcn = sup.VCN  
                                           and rpd.GroupCode = M.GroupCode
                                           and rpd.MaterialCode = M.MaterialCode 
                                            and RPD.ReferenceID = a.ResourceAllocationID
            INNER JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                           and RPD.VCN = sup.VCN
            WHERE RPH.RevenuePostingID = @revenuePostingid
      
        UNION 
        
            --  FLOATING CRANES - MOBILISATION & DEMO.
        select RPD.RevenuePostingDtlID ISPOSTED ,a.ResourceAllocationID,
             sup.VCN, b.ServiceTypeName as MovementName ,
                    'SUPPLIMANT' as ServiceName,M.GroupCode, M.MaterialCode,UPPER(M.MaterialDescription) as MaterialDescription,
                    RPH.SAPAccNo AccountNo, 
            oth.StartTime as StartTime,oth.EndTime as Endtime,M.IsCalculated,RPD.Units as Chargedas,M.UOM,
            '' as MovementType,a.OperationType as ServiceType,
             '' as ServiceReferenceType,a.OperationType as OperationType, '' as TaskStatus,
                  RPD.PostedOn RecentlyPostedDate,   rpd.PostedOn PostingDateTime,
                cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours',
                cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                 ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
        from dbo.ResourceAllocation a 
        inner join ServiceType b on b.ServiceTypeCode = a.OperationType 
        inner join dbo.SuppServiceRequest sup on  sup.SuppServiceRequestID = a.ServiceReferenceID
        inner join OtherServiceRecording oth on oth.ResourceAllocationID = a.ResourceAllocationID
        inner join MaterialCodeMaster M ON M.ChargedFor = a.OperationType
        inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
        INNER join RevenuePostingDtl RPD on RPD.vcn = sup.VCN  
                                       and rpd.GroupCode = M.GroupCode
                                       and rpd.MaterialCode = M.MaterialCode   
                                        and RPD.ReferenceID = a.ResourceAllocationID
        INNER JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                       and RPD.VCN = sup.VCN
        where a.ServiceReferenceType = 'SUPP'
            AND RPH.RevenuePostingID = @revenuePostingid
   
   
    UNION 
        
           -- HOT WORK PERMITS
            
        select RPD.RevenuePostingDtlID ISPOSTED ,s.SuppServiceRequestID as ResourceAllocationID,
             S.VCN, SB.SubCatName as MovementName ,
                    'SUPPLIMANT' as ServiceName,M.GroupCode, M.MaterialCode,
                    UPPER(M.MaterialDescription) as MaterialDescription,
                    RPH.SAPAccNo AccountNo, 
            S.FromDate as StartTime,S.ToDate as Endtime,M.IsCalculated, RPD.Units as Chargedas,M.UOM,
            '' as MovementType,  M.ServiceType as ServiceType,
             '' as ServiceReferenceType,M.ChargedFor as OperationType, '' as TaskStatus,
                  RPD.PostedOn RecentlyPostedDate,   rpd.PostedOn PostingDateTime,
                  
                 cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours',
                 cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                 ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
                 
             from dbo.SuppServiceRequest S 
        inner join SuppHotWorkInspection H on S.SuppServiceRequestID = h.SuppServiceRequestID
        inner join SubCategory SB on SB.SubCatCode = S.ServiceType
        inner join MaterialCodeMaster M ON M.ServiceType = S.ServiceType
        inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
        inner join RevenuePostingDtl RPD on RPD.vcn = S.VCN  
                                       and rpd.GroupCode = M.GroupCode
                                       and rpd.MaterialCode = M.MaterialCode   
                                       and RPD.ReferenceID = s.SuppServiceRequestID
        inner JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                       and RPD.VCN = S.VCN
           where S.ServiceType = 'HWST' 
        and MP.PortCode = 'CT' and
        M.ChargedFor = 'HWRK'    AND RPH.RevenuePostingID = @revenuePostingid
        and MP.PortCode = @portcode
            
      UNION
    -- DRY DOCK ELECTRICITY CONNECTION FEES
  select 
          RPD.RevenuePostingDtlID ISPOSTED , 
          MS.SuppMiscServiceID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName,
          UPPER(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,UPPER(M.MaterialDescription) MaterialDescription,
          RPH.SAPAccNo AS AccountNo,
             MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated,RPD.Units as Chargedas,M.UOM  as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate,   rpd.PostedOn PostingDateTime,
                cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS 'DueHours',
                 cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                 ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'ELCT' AND MS.ServiceTypeCode = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          inner join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode 
                                                    and RPD.ReferenceID =  MS.SuppMiscServiceID
          inner JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE  RPH.RevenuePostingID = @revenuePostingid
                    
                    
                    
                    
  UNION 
     -- DRY DOCK ELECTRICITY DAY HAIR
   select   RPD.RevenuePostingDtlID ISPOSTED , MS.SuppMiscServiceID  ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName, UPPER(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,UPPER(M.MaterialDescription) MaterialDescription,
          RPH.SAPAccNo AS AccountNo, MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated, RPD.Units as Chargedas
               ,M.UOM  as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate,   rpd.PostedOn PostingDateTime,
                cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS'DueHours', 
                 cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                  ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'ELPD' AND MS.ServiceTypeCode = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode   
                                                    and RPD.ReferenceID =  MS.SuppMiscServiceID
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      where  RPH.RevenuePostingID = @revenuePostingid
                    
  UNION 
        -- DRY DOCK ELECTRICITY USAGE CHARGES
   select RPD.RevenuePostingDtlID ISPOSTED ,MS.SuppMiscServiceID  ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName, UPPER(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode, UPPER(M.MaterialDescription) MaterialDescription,
          RPH.SAPAccNo AS AccountNo, MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated, RPD.Units as Chargedas,M.UOM  as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate,    rpd.PostedOn PostingDateTime,
                cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS'DueHours', 
                 cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                 ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'ELMT' AND MS.ServiceTypeCode = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode  
                                                    and RPD.ReferenceID =  MS.SuppMiscServiceID
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE   RPH.RevenuePostingID = @revenuePostingid
   UNION
   -- DRY DOCK FRESH WATER  
   select   RPD.RevenuePostingDtlID ISPOSTED , MS.SuppMiscServiceID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName, UPPER(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode, UPPER(M.MaterialDescription) MaterialDescription,
          RPH.SAPAccNo AS AccountNo, MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated,RPD.Units as Chargedas,M.UOM  as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate,    rpd.PostedOn PostingDateTime,
                 cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS'DueHours', 
                cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                ,'0' CloseMterReding, 
                 '0' as startmtrreding,
                 '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'DOWT' AND MS.ServiceTypeID = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          Left join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode    
                                                     and RPD.ReferenceID =  MS.SuppMiscServiceID
          LEFT JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE   RPH.RevenuePostingID = @revenuePostingid
              
       UNION 
       -- CHARGED FOR WHARF CRANE USAGE AT DRY DOCK/SHIP REPAIR
      select  RPD.RevenuePostingDtlID ISPOSTED , MS.SuppMiscServiceID ResourceAllocationID,
          SD.VCN ,  'DRYDOCKMISCSERVICE' as MovementName, UPPER(M.MaterialDescription) as ServiceName,
          M.GroupCode,  M.MaterialCode,UPPER(M.MaterialDescription) MaterialDescription,
          RPH.SAPAccNo AS AccountNo, MS.FromDateTime  as StartTime,MS.ToDateTime as Endtime,
             M.IsCalculated as IsCalculated,M.Chargedas as Chargedas,M.UOM  as UOM,
              '' as MovementType,'' as ServiceType,
              '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
              RPD.PostedOn  RecentlyPostedDate,   rpd.PostedOn PostingDateTime,
                cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS'DueHours', 
                cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours', '0' CloseMterReding, 
                 '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
           from SuppDryDock SD
          INNER JOIN SuppMiscService MS ON SD.SuppDryDockid = MS.SuppDryDockID
          inner join MaterialCodeMaster M ON SD.DockPortCode = M.PortCode 
                     AND SD.DockQuayCode = M.QuayCode  AND SD.DockBerthCode = M.BerthCode 
                     and  M.ChargedFor = 'WHAR' AND MS.ServiceTypeCode  = M.ServiceType
          inner join MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid
          INNER join RevenuePostingDtl RPD on RPD.vcn = SD.VCN  
                                                      and rpd.GroupCode = M.GroupCode
                                                    and rpd.MaterialCode = M.MaterialCode    
                                                     and RPD.ReferenceID =  MS.SuppMiscServiceID
          INNER JOIN RevenuePosting RPH on RPD.RevenuePostingID = RPH.RevenuePostingID
                                          and RPD.VCN = SD.VCN
      WHERE      RPH.RevenuePostingID = @revenuePostingid
     
      
        UNION 
       
          -- PASSENGER BAGGAGE
        select 
         RPD.RevenuePostingDtlID ISPOSTED ,
        SR.ServiceRequestid ResourceAllocationID,
        AN.VCN, SB.SubCatName as MovementName,UPPER(M.MaterialDescription) as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, UPPER(M.MaterialDescription) MaterialDescription ,
       RP.SAPAccNo AS AccountNo,
         SR.MovementDateTime as StartTime,SR.MovementDateTime as Endtime,'N' as IsCalculated,
        RPD.Units as Chargedas
         ,RPD.UOM as UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RP.PostedDate RecentlyPostedDate,  rpd.PostedOn PostingDateTime,
          cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))   AS 'DueHours', 
           cast( REPLACE(RPD.Units ,',','.') as numeric(10,0))  AS 'TotalHours'
         , '0' CloseMterReding, '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
        from ArrivalNotification an 
        inner join ServiceRequest SR on an.vcn = sr.vcn
        inner join SubCategory SB on SB.SubCatCode = SR.MovementType
        INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
        INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='BAGG'
        INNER join RevenuePostingDtl RPD on RPD.vcn = AN.VCN  
                                            and rpd.ReferenceID =  SR.ServiceRequestid
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       INNER join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where  RP.RevenuePostingID = @revenuePostingid  
     UNION 
          -- PASSENGER LEVY
        select 
         RPD.RevenuePostingDtlID ISPOSTED ,
        SR.ServiceRequestid ResourceAllocationID,
        AN.VCN, SB.SubCatName as MovementName, UPPER(M.MaterialDescription) as ServiceName,
         M.GroupCode GroupCode,M.MaterialCode MaterialCode, UPPER(M.MaterialDescription) MaterialDescription ,
       RP.SAPAccNo AS AccountNo,
         SR.MovementDateTime as StartTime,SR.MovementDateTime as Endtime,'N' as IsCalculated,
        RPD.Units as Chargedas
         ,RPD.UOM as UOM,
          '' as MovementType,'' as ServiceType,
          '' as ServiceReferenceType,'' as OperationType, '' as TaskStatus,
          RP.PostedDate RecentlyPostedDate,  rpd.PostedOn PostingDateTime,
          cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours',  
          cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
         , '0' CloseMterReding, '0' as startmtrreding,
                  '' MeterSerialNo, '' BerthName
        from ArrivalNotification an 
        inner join ServiceRequest SR on an.vcn = sr.vcn
        inner join SubCategory SB on SB.SubCatCode = SR.MovementType
        INNER JOIN MaterialCodePort MP on an.PortCode = MP.PortCode
        INNER JOIN MaterialCodeMaster M ON M.MaterialCodeMasterid = MP.MaterialCodeMasterid
                                        and   M.ChargedFor ='LEVY'
        INNER join RevenuePostingDtl RPD on RPD.vcn = AN.VCN  
                                            and rpd.ReferenceID =  SR.ServiceRequestid
                                            and rpd.GroupCode = M.GroupCode
                                            and rpd.MaterialCode = M.MaterialCode
       INNER join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID           
            where  RP.RevenuePostingID = @revenuePostingid  
	UNION
			--added by divya
			 select RPD.RevenuePostingDtlID ISPOSTED , VC.VesselCallMovementID as ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                    UPPER(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,UPPER(M.MaterialDescription) MaterialDescription ,
                     RP.SAPAccNo AS AccountNo,
                     VC.ATB StartTime, VC.ATUB Endtime,
                     M.IsCalculated,  RPD.Units Chargedas ,M.UOM  ,
                     VC.MovementType  ,M.ServiceType   ,                     
					 'VTSR' as ServiceReferenceType,'' as OperationType,'' as TaskStatus,
                      RP.PostedDate RecentlyPostedDate, 
                       rpd.PostedOn PostingDateTime,
                       cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours', 
                       cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
                       from VesselCallMovement VC 
					   --inner join ResourceAllocation RA on VC.ServiceRequestID =RA.ServiceReferenceID 
                  INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN 				  
                  inner join MaterialCodeMaster M on m.PortCode = VC.FromPositionPortCode and
                                           M.QuayCode = VC.FromPositionQuayCode  and
                                           M.BerthCode= VC.FromPositionBerthCode  and
										    M.MovementType = VC.MovementType and
                                            M.ChargedFor='WBCA' 
                  INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
                  AND MP.PortCode = @portcode
                   inner join SubCategory S on S.SubCatCode = VC.MovementType
                 
                   left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  VC.VesselCallMovementID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
                  left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID
				  WHERE  RP.RevenuePostingID = @revenuePostingid
				  AND VC.MovementType ='ARMV'

UNION
select  RPD.RevenuePostingDtlID ISPOSTED , (select VesselCallMovementID from VesselCallMovement VC  
														where VC.VCN  = (select vcn from RevenuePosting where RevenuePostingID=@revenuePostingid)
													AND   VC.MovementType ='SGMV' ) AS ResourceAllocationID,
                   VC.VCN,S.SubCatName AS MovementName ,
                    UPPER(M.MaterialDescription) AS ServiceName,
                     M.GroupCode, M.MaterialCode  ,UPPER(M.MaterialDescription) MaterialDescription ,
                     RP.SAPAccNo AS AccountNo,
                     VC.ATB StartTime, VC.ATUB Endtime,
                     M.IsCalculated,  RPD.Units Chargedas ,M.UOM  ,
                     VC.MovementType  ,M.ServiceType   ,                     
					 'VTSR' as ServiceReferenceType,'' as OperationType,'' as TaskStatus,
                      RP.PostedDate RecentlyPostedDate, 
                       rpd.PostedOn PostingDateTime,
                       cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'DueHours', 
                       cast( REPLACE(RPD.Units ,',','.') as numeric(10,0)) AS 'TotalHours'
                            , '0' CloseMterReding, 
                                   '0' as startmtrreding,
                               ''  MeterSerialNo, '' BerthName
from VesselCallMovement VC 
			          INNER JOIN (  
									SELECT  TOP(1) VesselCallMovementID FROM VesselCallMovement nvc WHERE 
									NVC.VCN  =  (select vcn from RevenuePosting where RevenuePostingID=@revenuePostingid) AND
									VesselCallMovementID <  
												 (select VesselCallMovementID from VesselCallMovement VC  
														where VC.VCN  =  (select vcn from RevenuePosting where RevenuePostingID=@revenuePostingid)
													AND   VC.MovementType ='SGMV' 
												  ) 
									ORDER BY 1 DESC 
			  
								) AS NWVCN  ON NWVCN.VesselCallMovementID  = VC.VesselCallMovementID 

                  INNER JOIN ArrivalNotification AN ON AN.VCN = VC.VCN 
                  inner join MaterialCodeMaster M on m.PortCode = VC.FromPositionPortCode and
                                           M.QuayCode = VC.FromPositionQuayCode  and
                                           M.BerthCode= VC.FromPositionBerthCode  and
										    M.MovementType = VC.MovementType and
                                            M.ChargedFor='WBCA' 
                  INNER JOIN MaterialCodePort MP on MP.MaterialCodeMasterid = M.MaterialCodeMasterid 
                  AND MP.PortCode = @portcode
                   inner join SubCategory S on S.SubCatCode = 'SGMV' 
                 
                   left join RevenuePostingDtl RPD on RPD.vcn = VC.VCN  
                                                                and rpd.ReferenceID =  VC.VesselCallMovementID
                                                                and rpd.GroupCode =M.GroupCode
                                                                and rpd.MaterialCode = M.MaterialCode 
                  left join RevenuePosting RP ON RP.RevenuePostingID = RPD.RevenuePostingID      
                           
                  WHERE 
                 VC.VCN  =   (select vcn from RevenuePosting where RevenuePostingID=@revenuePostingid)

End
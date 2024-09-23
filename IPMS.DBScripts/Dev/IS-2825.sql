IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetArrivalGridDetails]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_GetArrivalGridDetails]
GO
CREATE PROCEDURE [dbo].[usp_GetArrivalGridDetails]
   @A_PortCode VARCHAR (4),
   @A_UserType VARCHAR (4),
   @A_UserTypeid int,
   @A_Userid int,
   @A_frmdt VARCHAR (25),
   @A_todt VARCHAR (25),
   @A_vcn  VARCHAR (12), 
   @A_veselid  VARCHAR (25),
   @A_imdg  VARCHAR (3), 
   @A_isps  VARCHAR (3),
   @A_imdgClear  VARCHAR (6), 
   @A_ispsClear  VARCHAR (6),
   @A_phoClear   VARCHAR (6)
AS
BEGIN

DECLARE @ArvEntId int 
 DECLARE @ArvPHOId int 
 DECLARE @ArvISPS int 
 DECLARE @ArvIMDG int 
 DECLARE @ArvDHM int 
  
  SET @ArvEntId = (select EntityID from Entity where EntityCode = 'ARVLNOT');
  SET @ArvPHOId = (select EntityID from Entity where EntityCode = 'PHAN');
  SET @ArvISPS = (select EntityID from Entity where EntityCode = 'ISPSAN');
  SET @ArvIMDG = (select EntityID from Entity where EntityCode = 'IMDGAN');
  SET @ArvDHM = (select EntityID from Entity where EntityCode = 'DHMAN');
  

  if @A_UserType = 'AGNT'
   begin
               select  VCN,  VesselName, VesselType, ReasonforvisitName,
          ETA, ETD, NominationDate
          ,wfStatus, ReasonForVisit,                          
                 case when (
              (IsArrivaStatus = 'NEW' or IsPHANStatus  = 'NEW' or 
                isnull(IsISPSANStatus,'NEW')  = 'NEW' or isnull(IsIMDGANStatus, 'NEW') = 'NEW' ) 
                and Srvcount = 0  and  RecordStatus = 'A' and (ISNULL(RecentAgentID, 0) = @A_UserTypeid)
               ) 
              then 'true' else 'false' end as isEditVisible,
             
              case when (
              IsArrivaStatus = 'NEW' and
              IsPHANStatus  = 'NEW' and 
              isnull(IsISPSANStatus,'NEW')  = 'NEW' and 
              isnull(IsIMDGANStatus, 'NEW') = 'NEW'
               ) 
              then 'true' else 'false' end as isViewVisible,
              
                IsANFinal,IsPHANFinal ,IsIMDGANFinal ,IsISPSANFinal, AnyDangerousGoodsonBoard,
                IsSamsaArrested,IsArrivaStatus,IsPHANStatus
                , IsIMDGANStatus ,
                IsISPSANStatus, RecordStatus
                 ,ArrvwfRemarks,PHOwfRemarks,ISPSwfRemarks,IMDGwfRemarks, 
                    case 
                                   when   (ATD IS NOT NULL) then
                                    'Sailed'
                                   when (ATB IS NOT NULL) then
                                    'Berthed'
                                   when (ATA IS NOT NULL ) then
                                      'Arrived'
                                   when ( IsArrivaStatus ='WFCA' or IsPHANStatus='WFCA' 
                                          or (IsISPSANStatus IS not NULL and IsISPSANStatus ='WFCA') 
                                          or (IsIMDGANStatus IS not NULL and IsIMDGANStatus ='WFCA')
                                          or (IsTIDALStatus IS not NULL and IsTIDALStatus ='WFCA')
                                          or RecordStatus = 'I' ) then
                                      'Cancelled'
                                   when ( IsArrivaStatus ='WFRE' or IsPHANStatus='WFRE' 
                                          or (IsISPSANStatus IS not NULL and IsISPSANStatus ='WFRE') 
                                          or (IsIMDGANStatus IS not NULL and IsIMDGANStatus ='WFRE')
                                          or (IsTIDALStatus IS not NULL and IsTIDALStatus ='WFRE')
                                          or RecordStatus = 'I' ) then
                                      'Rejected'
                                   else 
                                       'Not Arrived'
                                   end  ArvStatus
                 , IsPrimary,CancelRemarks
                FROM (
                
                                   select an.VCN VCN,  vc.VesselName, sb.SubCatName VesselType, ISNULL(cal.RecentAgentID, an.AgentID) RecentAgentID,
                              dbo.udf_GetArrivalReasonForVisit(an.VCN) ReasonforvisitName,
                              an.ETA, an.ETD, an.NominationDate
                              ,wsb.SubCatName wfStatus, An.ReasonForVisit,                        
                              An.CreatedDate,
                               an.IsANFinal,an.IsPHANFinal ,an.IsIMDGANFinal ,an.IsISPSANFinal, AnyDangerousGoodsonBoard,
                                case when (sa.VesselArrested = 'Y' and sa.VesselReleased = 'Y'  ) 
                               then 'true' else 'false' end as IsSamsaArrested
                               ,An.RecordStatus,
                                dbo.udf_GetWorkflowPreviousRemarksOnEntityRefer(@ArvEntId, An.VCN) as ArrvwfRemarks,
                                dbo.udf_GetWorkflowPreviousRemarksOnEntityRefer(@ArvPHOId, An.VCN) as PHOwfRemarks,
                                dbo.udf_GetWorkflowPreviousRemarksOnEntityRefer(@ArvISPS, An.VCN)  as ISPSwfRemarks,
                                dbo.udf_GetWorkflowPreviousRemarksOnEntityRefer(@ArvIMDG, An.VCN) as IMDGwfRemarks,
                               (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvEntId and ReferenceID =  An.VCN) IsArrivaStatus,
                               (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvPHOId and ReferenceID =  An.VCN) IsPHANStatus,
                               (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvISPS and ReferenceID =  An.VCN)  IsISPSANStatus,
                               (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvIMDG and ReferenceID =  An.VCN) IsIMDGANStatus,
                              (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvDHM and ReferenceID =  An.VCN) IsTIDALStatus,
                               
                                   cal.ATD ,cal.ATB ,cal.ATA ,                                
                                ( select count(1) from ServiceRequest SRV where SRV.VCN = an.VCN) as Srvcount,
                                ARA.IsPrimary, An.CancelRemarks
                              from ArrivalNotification An 
                              inner join ArrivalAgent ARA ON ARA.VCN = An.VCN                              
                              inner join Vessel vc on An.VesselID = vc.VesselID
                              inner join SubCategory sb on sb.SubCatCode = vc.VesselType
                              inner join WorkflowInstance WI on WI.WorkflowInstanceId = An.WorkflowInstanceId
                              inner join SubCategory wsb on wsb.SubCatCode = WI.WorkflowTaskCode
                              left join VesselCall cal on cal.VCN = An.VCN
                              left join VesselArrestImmobilizationSAMSA sa on sa.VCN = AN.VCN
                           where isnull(An.Isdraft, 'N') = 'N' and An.PortCode = @A_PortCode                                                   
                           and  dbo.udf_FormatDateTime(an.ETA,'yyyy-mm-dd') between   @A_frmdt and @A_todt 
                           and  an.VCN = CASE @A_vcn when 'NA' then an.VCN else @A_vcn end
                           and  An.VesselID = CASE @A_veselid when '0' then An.VesselID else @A_veselid end
                           and  An.AnyDangerousGoodsonBoard = CASE @A_imdg when 'All' then An.AnyDangerousGoodsonBoard else @A_imdg end
                           and  An.AppliedForISPS = CASE @A_isps when 'All' then An.AppliedForISPS else @A_isps end
                           and An.IsIMDGANFinal = CASE @A_imdgClear when 'false' then An.IsIMDGANFinal else 'Y' end
                           and An.IsISPSANFinal = CASE @A_ispsClear when 'false' then An.IsISPSANFinal else 'Y' end
                           and An.IsPHANFinal = CASE @A_phoClear when 'false' then An.IsPHANFinal else 'Y' end                        
                           and (An.AgentID = @A_UserTypeid OR @A_UserTypeid IN (select v.ProposedAgent from VesselAgentChange  v
            join dbo.WorkflowInstance w ON w.WorkflowInstanceId = v.WorkflowInstanceId
            where v.VCN = an.VCN and w.WorkflowTaskCode = 'WFSA')) 
                        
              
            ) T order by  CreatedDate desc
  end
  else
  begin
  
     select  VCN,  VesselName, VesselType, ReasonforvisitName,     
          ETA, ETD, NominationDate
          ,wfStatus, ReasonForVisit,
                case when (
              (IsArrivaStatus = 'NEW' or 
              IsPHANStatus  = 'NEW' or 
              isnull(IsISPSANStatus,'NEW')  = 'NEW' or 
              isnull(IsIMDGANStatus, 'NEW') = 'NEW' ) and Srvcount = 0  and  RecordStatus = 'A'
               ) 
              then 'true' else 'false' end as isEditVisible,
                case when (
              IsArrivaStatus = 'NEW' and 
              IsPHANStatus  = 'NEW' and 
              isnull(IsISPSANStatus,'NEW')  = 'NEW' and 
              isnull(IsIMDGANStatus, 'NEW') = 'NEW'
               ) 
              then 'true' else 'false' end as isViewVisible,
            
                IsANFinal,IsPHANFinal ,IsIMDGANFinal ,IsISPSANFinal, AnyDangerousGoodsonBoard,
                IsSamsaArrested,IsArrivaStatus,IsPHANStatus
                , IsIMDGANStatus ,
                IsISPSANStatus, RecordStatus
                 ,ArrvwfRemarks,PHOwfRemarks,ISPSwfRemarks,IMDGwfRemarks, 
                    case 
                                   when   (ATD IS NOT NULL) then
                                    'Sailed'
                                   when (ATB IS NOT NULL) then
                                    'Berthed'
                                   when (ATA IS NOT NULL ) then
                                      'Arrived'
                                   when ( IsArrivaStatus ='WFCA' or IsPHANStatus='WFCA' 
                                          or (IsISPSANStatus IS not NULL and IsISPSANStatus ='WFCA') 
                                          or (IsIMDGANStatus IS not NULL and IsIMDGANStatus ='WFCA')
                                          or (IsTIDALStatus IS not NULL and IsTIDALStatus ='WFCA')
                                          or RecordStatus = 'I' ) then
                                      'Cancelled'
                                    when ( IsArrivaStatus ='WFRE' or IsPHANStatus='WFRE' 
                                          or (IsISPSANStatus IS not NULL and IsISPSANStatus ='WFRE') 
                                          or (IsIMDGANStatus IS not NULL and IsIMDGANStatus ='WFRE')
                                          or (IsTIDALStatus IS not NULL and IsTIDALStatus ='WFRE')
                                          or RecordStatus = 'I' ) then
                                      'Rejected'
                                   else 
                                       'Not Arrived'
                                   end  ArvStatus
                 , IsPrimary, CancelRemarks
                FROM (
        
                       select an.VCN VCN,  vc.VesselName, sb.SubCatName VesselType, 
                        dbo.udf_GetArrivalReasonForVisit(an.VCN) ReasonforvisitName,
                       an.ETA, an.ETD, an.NominationDate
                        ,wsb.SubCatName wfStatus, An.ReasonForVisit,                        
                        An.CreatedDate,
                         an.IsANFinal,an.IsPHANFinal ,an.IsIMDGANFinal ,an.IsISPSANFinal, AnyDangerousGoodsonBoard,
                          case when (sa.VesselArrested = 'Y' and sa.VesselReleased = 'Y'  ) 
                         then 'true' else 'false' end as IsSamsaArrested
                          ,An.RecordStatus
                           ,dbo.udf_GetWorkflowPreviousRemarksOnEntityRefer(@ArvEntId, An.VCN) as ArrvwfRemarks,
                          dbo.udf_GetWorkflowPreviousRemarksOnEntityRefer(@ArvPHOId, An.VCN) as PHOwfRemarks,
                          dbo.udf_GetWorkflowPreviousRemarksOnEntityRefer(@ArvISPS, An.VCN) as ISPSwfRemarks,
                          dbo.udf_GetWorkflowPreviousRemarksOnEntityRefer(@ArvIMDG, An.VCN) as IMDGwfRemarks,
                         (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvEntId and ReferenceID =  An.VCN) IsArrivaStatus,
                         (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvPHOId and ReferenceID =  An.VCN) IsPHANStatus,
                         (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvISPS and ReferenceID =  An.VCN)  IsISPSANStatus,
                         (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvIMDG and ReferenceID =  An.VCN) IsIMDGANStatus,
                         (select WorkflowTaskCode FROM WorkflowInstance WHERE EntityID = @ArvDHM and ReferenceID =  An.VCN) IsTIDALStatus,
                           
                              cal.ATD ,cal.ATB ,cal.ATA ,                         
                            ( select count(1)  from ServiceRequest SRV where SRV.VCN = an.VCN) as Srvcount,
                            'Y' IsPrimary, CancelRemarks
                        from ArrivalNotification An 
                        inner join Vessel vc on An.VesselID = vc.VesselID
                        inner join SubCategory sb on sb.SubCatCode = vc.VesselType
                        inner join WorkflowInstance WI on WI.WorkflowInstanceId = An.WorkflowInstanceId
                        inner join SubCategory wsb on wsb.SubCatCode = WI.WorkflowTaskCode
                        left join VesselCall cal on cal.VCN = An.VCN
                        left join VesselArrestImmobilizationSAMSA sa on sa.VCN = AN.VCN
                      
                        where isnull(An.Isdraft, 'N') = 'N' and An.PortCode = @A_PortCode
                     and  dbo.udf_FormatDateTime(an.ETA,'yyyy-mm-dd') between   @A_frmdt and @A_todt 
                     and  an.VCN = CASE @A_vcn when 'NA' then an.VCN else @A_vcn end
                     and  An.VesselID = CASE @A_veselid when '0' then An.VesselID else @A_veselid end
                     and  An.AnyDangerousGoodsonBoard = CASE @A_imdg when 'All' then An.AnyDangerousGoodsonBoard else @A_imdg end
                     and  An.AppliedForISPS = CASE @A_isps when 'All' then An.AppliedForISPS else @A_isps end
                     and An.IsIMDGANFinal = CASE @A_imdgClear when 'false' then An.IsIMDGANFinal else 'Y' end
                     and An.IsISPSANFinal = CASE @A_ispsClear when 'false' then An.IsISPSANFinal else 'Y' end
                     and An.IsPHANFinal = CASE @A_phoClear when 'false' then An.IsPHANFinal else 'Y' end
                      
               )t  order by  CreatedDate desc         
             

  end
          
END
GO



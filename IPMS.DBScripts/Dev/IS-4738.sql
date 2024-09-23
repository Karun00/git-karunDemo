alter PROCEDURE [dbo].[usp_VesselAgentChange_GetVCNActiveDetails]
 @PortCode Varchar(10),     
 @CurrentAgentID INT
 
AS
BEGIN
       SET NOCOUNT ON;         


              
                    select x.VCN,x.VesselName,x.VesselType,x.VesselTypeName,x.ReasonForVisit,x.ReasonForVisitName,Recordstatus From
              (
                    select vc.VCN,v.VesselName,v.VesselType,s.SubCatName as VesselTypeName,
				    --s2.SubCatName as ReasonForVisitName,
						dbo.udf_GetArrivalReasonForVisit ( vc.VCN) as ReasonForVisitName,
						a.ReasonForVisit,a.RecordStatus   From VesselCall vc
                     Inner join ArrivalNotification a on vc.VCN = a.VCN  and a.PortCode=@PortCode and vc.RecentAgentID=@CurrentAgentID
                     INNER JOIN Vessel v on v.VesselID =a.VesselID  
                     INNER JOIN SubCategory s on v.VesselType = s.SubCatCode  
                      INNER JOIN SubCategory s2 on a.ReasonForVisit = s2.SubCatCode
                     Where Vc.VCN not in(                                  
                                   select vag1.VCN from VesselAgentChange vag1 where vag1.CurrentAgentID = @CurrentAgentID
                                  UNION select vag2.VCN from VesselAgentChange vag2 where vag2.ProposedAgent = @CurrentAgentID
                     )
               UNION 
                            select vc.VCN,v.VesselName,v.VesselType,s.SubCatName as VesselTypeName,
						--s2.SubCatName as ReasonForVisitName,
							dbo.udf_GetArrivalReasonForVisit ( vc.VCN) as ReasonForVisitName,
							a.ReasonForVisit,A.RecordStatus  From VesselCall vc 
                           Inner join ArrivalNotification a on vc.VCN = a.VCN  and a.PortCode=@PortCode
                           INNER JOIN Vessel v on v.VesselID =a.VesselID 
                           INNER JOIN SubCategory s on v.VesselType = s.SubCatCode  
                             INNER JOIN SubCategory s2 on a.ReasonForVisit = s2.SubCatCode
                           Where Vc.VCN in(
                                  select VCN from VesselAgentChange vag  left join workflowinstance wf on vag.WorkflowInstanceId = wf.WorkflowInstanceID 
                                  where (WorkflowTaskCode = 'REJ' OR WorkflowTaskCode = 'WFRE') and vag.VesselAgentChangeID = (select max(v1.VesselAgentChangeID) from VesselAgentChange v1 where v1.VCN = vag.VCN) and vag.CurrentAgentID = 3
                           	)  AND VC.ATD IS NULL
              ) x where RecordStatus='A'
END


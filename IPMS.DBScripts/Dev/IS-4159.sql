


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetServiceRecordinDetails_New]') AND type in (N'P'))
DROP PROCEDURE [dbo].[usp_GetServiceRecordinDetails_New]
GO
CREATE PROCEDURE [dbo].[usp_GetServiceRecordinDetails_New]
@portcode nvarchar(2),
@VCN nvarchar(12),
@VesselName nvarchar(20),
@ResourceName  nvarchar(20)
WITH EXEC AS CALLER
AS
begin
        if( RTRIM(LTRIM(UPPER(@VCN))) = 'NA' and RTRIM(LTRIM(UPPER(@ResourceName))) = 'NA' and RTRIM(LTRIM(UPPER(@VesselName))) = 'NA')
        BEGIN
              select ResourceAllocationID,
              ServiceReferenceType , ServiceReferenceTypeName ,
              VCN, VesselName,VesselLength, MovementType, MovementTypeName, MovementDateTime,
              ServiceReferenceID ,
              OperationType , OperationTypeName ,ResourceID
              ,ResourceType ,
              StartTime ,
              EndTime, FirstName 
              ,TaskStatus , TaskStatusName ,RecordStatus, ModifiedDate,BerthKey
              from (

              select a.ResourceAllocationID,
              a.ServiceReferenceType , c.SubCatName ServiceReferenceTypeName ,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
              (select VCN from dbo.SuppServiceRequest where SuppServiceRequestID = a.ServiceReferenceID and PortCode = @portcode) 
              ELSE 
              (select sr.VCN from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = @portcode)
              END VCN,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
              (select V.VesselName from dbo.SuppServiceRequest SSR
              inner join ArrivalNotification AN on an.VCN = SSR.VCN
              inner join Vessel V on V.VesselID = AN.VesselID
               where SuppServiceRequestID = a.ServiceReferenceID and AN.PortCode = @portcode) 
              ELSE 
              (select V.VesselName from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              inner join Vessel V on v.VesselID = an.VesselID
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = @portcode)
              END VesselName,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
              (select V.LengthOverallInM from dbo.SuppServiceRequest SSR
              inner join ArrivalNotification AN on an.VCN = SSR.VCN
              inner join Vessel V on V.VesselID = AN.VesselID
               where SuppServiceRequestID = a.ServiceReferenceID and AN.PortCode = @portcode) 
              ELSE 
              (select V.LengthOverallInM from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              inner join Vessel V on v.VesselID = an.VesselID
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = @portcode)
              END VesselLength,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
              (select Servicetype from dbo.SuppServiceRequest where SuppServiceRequestID = a.ServiceReferenceID and PortCode = @portcode) 
              ELSE 
              (select MovementType from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = @portcode)
              END MovementType,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
              (select sc.SubCatName from dbo.SuppServiceRequest ssr
              join SubCategory sc on sc.SubCatCode = ssr.ServiceType where ssr.SuppServiceRequestID = a.ServiceReferenceID and ssr.PortCode = @portcode) 
              ELSE 
              (select sc.SubCatName from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              inner join SubCategory sc on sc.SubCatCode = sr.MovementType
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = @portcode)
              END MovementTypeName,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
                null
              ELSE 
              (select MovementDateTime from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = 'DB')
              END MovementDateTime,
              a.ServiceReferenceID ,
              a.OperationType , b.ServiceTypeName OperationTypeName ,
              a.ResourceID
              ,a.ResourceType ,
              a.StartTime as StartTime ,
              a.EndTime as EndTime,CONCAT(u.FirstName, ' ', u.LastName) AS FirstName 
              ,a.TaskStatus , e.SubCatName TaskStatusName ,a.RecordStatus, a.ModifiedDate,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
                  ( select CONCAT(b.PortCode,'.',b.QuayCode,'.',b.BerthCode) AS BerthKey from SuppServiceRequest s
                       left JOIN Berth b on s.PortCode = b.PortCode AND s.QuayCode = b.QuayCode AND s.BerthCode = b.BerthCode
                       where s.SuppServiceRequestID = a.ServiceReferenceID  )
              ELSE 
               ' '
              END BerthKey
              from dbo.ResourceAllocation a 
              inner join dbo.ServiceType b ON b.ServiceTypeCode = a.OperationType
              inner join dbo.SubCategory c ON c.SubCatCode = a.ServiceReferenceType 
              inner join dbo.SubCategory e ON e.SubCatCode = a.TaskStatus 
              Left join dbo.Users u ON a.ResourceID = u.UserID
              where
              AllocationDate > getdate()-15 and
			   (
              a.TaskStatus = 'ACCP' or a.TaskStatus = 'COMP' or a.TaskStatus = 'VERF' 
              or a.TaskStatus='STRD' or a.TaskStatus = 'CFRI' )


              ) t where t.VCN is not null
              order by ModifiedDate desc
        END
        else
        BEGIN
        
        if(@VCN  = 'NA')
          select @VCN = '';
          
        if(@VesselName  = 'NA')
          select @VesselName = '';
          
        if(@ResourceName  = 'NA')
          select @ResourceName = '';
         

        select ResourceAllocationID,
              ServiceReferenceType , ServiceReferenceTypeName ,
              VCN, VesselName,VesselLength, MovementType, MovementTypeName, MovementDateTime, 
              ServiceReferenceID ,
              OperationType , OperationTypeName ,ResourceID
              ,ResourceType ,
              StartTime ,
              EndTime, FirstName 
              ,TaskStatus , TaskStatusName ,RecordStatus, ModifiedDate,BerthKey
              from (

              select a.ResourceAllocationID,
              a.ServiceReferenceType , c.SubCatName ServiceReferenceTypeName ,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
              (select VCN from dbo.SuppServiceRequest where SuppServiceRequestID = a.ServiceReferenceID and PortCode = @portcode) 
              ELSE 
              (select sr.VCN from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = @portcode)
              END VCN,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
              (select V.VesselName from dbo.SuppServiceRequest SSR
              inner join ArrivalNotification AN on an.VCN = SSR.VCN
              inner join Vessel V on V.VesselID = AN.VesselID
               where SuppServiceRequestID = a.ServiceReferenceID and AN.PortCode = @portcode) 
              ELSE 
              (select V.VesselName from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              inner join Vessel V on v.VesselID = an.VesselID
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = @portcode)
              END VesselName,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
              (select V.LengthOverallInM from dbo.SuppServiceRequest SSR
              inner join ArrivalNotification AN on an.VCN = SSR.VCN
              inner join Vessel V on V.VesselID = AN.VesselID
               where SuppServiceRequestID = a.ServiceReferenceID and AN.PortCode = @portcode) 
              ELSE 
              (select V.LengthOverallInM from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              inner join Vessel V on v.VesselID = an.VesselID
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = @portcode)
              END VesselLength,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
              (select Servicetype from dbo.SuppServiceRequest where SuppServiceRequestID = a.ServiceReferenceID and PortCode = @portcode) 
              ELSE 
              (select MovementType from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = @portcode)
              END MovementType,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
              (select sc.SubCatName from dbo.SuppServiceRequest ssr
              join SubCategory sc on sc.SubCatCode = ssr.ServiceType where ssr.SuppServiceRequestID = a.ServiceReferenceID and ssr.PortCode = @portcode) 
              ELSE 
              (select sc.SubCatName from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              inner join SubCategory sc on sc.SubCatCode = sr.MovementType
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = @portcode)
              END MovementTypeName,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
                null
              ELSE 
              (select MovementDateTime from dbo.ServiceRequest sr
              inner join ArrivalNotification An on An.VCN = sr.VCN 
              where sr.ServiceRequestID = a.ServiceReferenceID
              and An.PortCode = 'DB')
              END MovementDateTime,
              a.ServiceReferenceID ,
              a.OperationType , b.ServiceTypeName OperationTypeName ,
              a.ResourceID
              ,a.ResourceType ,
              a.StartTime as StartTime ,
              a.EndTime as EndTime,CONCAT(u.FirstName, ' ', u.LastName) AS FirstName 
              ,a.TaskStatus , e.SubCatName TaskStatusName ,a.RecordStatus, a.ModifiedDate,
              CASE a.ServiceReferenceType
              WHEN 'SUPP' THEN 
                  ( select CONCAT(b.PortCode,'.',b.QuayCode,'.',b.BerthCode) AS BerthKey from SuppServiceRequest s
                       left JOIN Berth b on s.PortCode = b.PortCode AND s.QuayCode = b.QuayCode AND s.BerthCode = b.BerthCode
                       where s.SuppServiceRequestID = a.ServiceReferenceID  )
              ELSE 
               ' '
              END BerthKey
              from dbo.ResourceAllocation a 
              inner join dbo.ServiceType b ON b.ServiceTypeCode = a.OperationType
              inner join dbo.SubCategory c ON c.SubCatCode = a.ServiceReferenceType 
              inner join dbo.SubCategory e ON e.SubCatCode = a.TaskStatus 
              Left join dbo.Users u ON a.ResourceID = u.UserID
              where
               (
              a.TaskStatus = 'ACCP' or a.TaskStatus = 'COMP' or a.TaskStatus = 'VERF' 
              or a.TaskStatus='STRD' or a.TaskStatus = 'CFRI' )

              ) t where t.VCN is not null
              and t.VCN  like '%'+ upper(coalesce (@VCN, t.VCN))+ '%' 
              and t.VesselName  like '%'+ upper(coalesce (@VesselName, t.VesselName))+ '%' 
              and t.FirstName  like '%'+ upper(coalesce (@ResourceName, t.FirstName))+ '%' 			  
              order by ModifiedDate desc
              
        END
end
GO





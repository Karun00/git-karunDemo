IF EXISTS
      (SELECT *
         FROM sys.objects
        WHERE     object_id =
                     OBJECT_ID (N'[dbo].[usp_GetServiceRecordinDetails_VCN]')
              AND type IN (N'P'))
   DROP PROCEDURE [dbo].[usp_GetServiceRecordinDetails_VCN]
GO

CREATE PROCEDURE [dbo].[usp_GetServiceRecordinDetails_VCN]
   @portcode NVARCHAR (2), @VCN NVARCHAR (12)
   WITH
   EXECUTE AS CALLER
AS
   BEGIN
      SELECT ResourceAllocationID,
             ServiceReferenceType,
             ServiceReferenceTypeName,
             VCN,
             VesselName,
             VesselLength,
             MovementType,
             MovementTypeName,
             ServiceReferenceID,
             OperationType,
             OperationTypeName,
             ResourceID,
             ResourceType,
             StartTime,
             EndTime,
             FirstName,
             TaskStatus,
             TaskStatusName,
             RecordStatus,
             ModifiedDate,
             BerthKey        
        FROM (SELECT a.ResourceAllocationID,
                     a.ServiceReferenceType,
                     c.SubCatName ServiceReferenceTypeName,
                     CASE a.ServiceReferenceType
                        WHEN 'SUPP'
                        THEN
                           (SELECT VCN
                              FROM dbo.SuppServiceRequest
                             WHERE     SuppServiceRequestID =
                                          a.ServiceReferenceID
                                   AND PortCode = @portcode)
                        ELSE
                           (SELECT sr.VCN
                              FROM dbo.ServiceRequest sr
                                   INNER JOIN ArrivalNotification An
                                      ON An.VCN = sr.VCN
                             WHERE     sr.ServiceRequestID =
                                          a.ServiceReferenceID
                                   AND An.PortCode = @portcode)
                     END
                        VCN,
                     CASE a.ServiceReferenceType
                        WHEN 'SUPP'
                        THEN
                           (SELECT V.VesselName
                              FROM dbo.SuppServiceRequest SSR
                                   INNER JOIN ArrivalNotification AN
                                      ON an.VCN = SSR.VCN
                                   INNER JOIN Vessel V
                                      ON V.VesselID = AN.VesselID
                             WHERE     SuppServiceRequestID =
                                          a.ServiceReferenceID
                                   AND AN.PortCode = @portcode)
                        ELSE
                           (SELECT V.VesselName
                              FROM dbo.ServiceRequest sr
                                   INNER JOIN ArrivalNotification An
                                      ON An.VCN = sr.VCN
                                   INNER JOIN Vessel V
                                      ON v.VesselID = an.VesselID
                             WHERE     sr.ServiceRequestID =
                                          a.ServiceReferenceID
                                   AND An.PortCode = @portcode)
                     END
                        VesselName,
                     CASE a.ServiceReferenceType
                        WHEN 'SUPP'
                        THEN
                           (SELECT V.LengthOverallInM
                              FROM dbo.SuppServiceRequest SSR
                                   INNER JOIN ArrivalNotification AN
                                      ON an.VCN = SSR.VCN
                                   INNER JOIN Vessel V
                                      ON V.VesselID = AN.VesselID
                             WHERE     SuppServiceRequestID =
                                          a.ServiceReferenceID
                                   AND AN.PortCode = @portcode)
                        ELSE
                           (SELECT V.LengthOverallInM
                              FROM dbo.ServiceRequest sr
                                   INNER JOIN ArrivalNotification An
                                      ON An.VCN = sr.VCN
                                   INNER JOIN Vessel V
                                      ON v.VesselID = an.VesselID
                             WHERE     sr.ServiceRequestID =
                                          a.ServiceReferenceID
                                   AND An.PortCode = @portcode)
                     END
                        VesselLength,
                     CASE a.ServiceReferenceType
                        WHEN 'SUPP'
                        THEN
                           (SELECT Servicetype
                              FROM dbo.SuppServiceRequest
                             WHERE     SuppServiceRequestID =
                                          a.ServiceReferenceID
                                   AND PortCode = @portcode)
                        ELSE
                           (SELECT MovementType
                              FROM dbo.ServiceRequest sr
                                   INNER JOIN ArrivalNotification An
                                      ON An.VCN = sr.VCN
                             WHERE     sr.ServiceRequestID =
                                          a.ServiceReferenceID
                                   AND An.PortCode = @portcode)
                     END
                        MovementType,
                     CASE a.ServiceReferenceType
                        WHEN 'SUPP'
                        THEN
                           (SELECT sc.SubCatName
                              FROM dbo.SuppServiceRequest ssr
                                   JOIN SubCategory sc
                                      ON sc.SubCatCode = ssr.ServiceType
                             WHERE     ssr.SuppServiceRequestID =
                                          a.ServiceReferenceID
                                   AND ssr.PortCode = @portcode)
                        ELSE
                           (SELECT sc.SubCatName
                              FROM dbo.ServiceRequest sr
                                   INNER JOIN ArrivalNotification An
                                      ON An.VCN = sr.VCN
                                   INNER JOIN SubCategory sc
                                      ON sc.SubCatCode = sr.MovementType
                             WHERE     sr.ServiceRequestID =
                                          a.ServiceReferenceID
                                   AND An.PortCode = @portcode)
                     END
                        MovementTypeName,
                     a.ServiceReferenceID,
                     a.OperationType,
                     b.ServiceTypeName OperationTypeName,
                     a.ResourceID,
                     a.ResourceType,
                     a.StartTime AS StartTime,
                     a.EndTime AS EndTime,
                     CONCAT (u.FirstName, '', u.LastName) AS FirstName,
                     a.TaskStatus,
                     e.SubCatName TaskStatusName,
                     a.RecordStatus,
                     a.ModifiedDate,
                     CASE a.ServiceReferenceType
                        WHEN 'SUPP'
                        THEN
                           (SELECT CONCAT (b.PortCode,
                                           '.',
                                           b.QuayCode,
                                           '.',
                                           b.BerthCode)
                                      AS BerthKey
                              FROM SuppServiceRequest s
                                   LEFT JOIN Berth b
                                      ON     s.PortCode = b.PortCode
                                         AND s.QuayCode = b.QuayCode
                                         AND s.BerthCode = b.BerthCode
                             WHERE s.SuppServiceRequestID =
                                      a.ServiceReferenceID)
                        ELSE
                           ' '
                     END
                        BerthKey
                FROM dbo.ResourceAllocation a
                     INNER JOIN dbo.ServiceType b
                        ON b.ServiceTypeCode = a.OperationType
                     INNER JOIN dbo.SubCategory c
                        ON c.SubCatCode = a.ServiceReferenceType
                     INNER JOIN dbo.SubCategory e
                        ON e.SubCatCode = a.TaskStatus
                     LEFT JOIN dbo.Users u ON a.ResourceID = u.UserID
               WHERE    a.TaskStatus = 'ACCP'
                     OR a.TaskStatus = 'COMP'
                     OR a.TaskStatus = 'VERF'
                     OR a.TaskStatus = 'STRD'
                     OR a.TaskStatus = 'CFRI') t
       WHERE t.VCN = @VCN
      ORDER BY  ServiceReferenceID ASC, MovementTypeName ASC,StartTime asc, ModifiedDate ASC 
   END
   
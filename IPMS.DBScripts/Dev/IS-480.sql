ALTER PROCEDURE [dbo].[usp_Service_Request_Auto_Email_Rule1]
AS
   BEGIN
      SET  NOCOUNT ON
      SET  XACT_ABORT ON

      DECLARE @duration               INT
      DECLARE @Index                  INT
      DECLARE @eventscheduletrackid   INT
      DECLARE @eventscheduletaskid    INT
      DECLARE @reference              NVARCHAR (12)
      DECLARE @WorkflowInstanceId     INT
      DECLARE @portcode               NVARCHAR (2)
      DECLARE @usertypeid	            INT
      DECLARE @usertype		            nvarchar(4)

      DECLARE @EventScheduleTrack TABLE
      (
         EventScheduleTrackID   INT IDENTITY (1, 1),
         EventScheduleTaskID    INT,
         NotificationId         INT,
         Reference              NVARCHAR (12),
         WorkflowInstanceId     INT,
         WorkflowProcessId      INT
      )

      DECLARE @pendingServiceRequests TABLE
      (
         PendingServiceRequestID   INT IDENTITY (1, 1),
         ServiceRequestId          INT,
         VCN                       NVARCHAR (12),
         ToPositionPortCode        NVARCHAR (2),
         ToPositionQuayCode        NVARCHAR (4),
         ToPositionBerthCode       NVARCHAR (4),
         ToPositionBollardCode     NVARCHAR (4),
         MovementType              NVARCHAR (4),
         MovementDateTime          DATETIME,
         CurrentDatetime           DATETIME,
         WorkflowTaskCode          NVARCHAR (4),
         WorkflowInstanceId        INT,
         PortCode                  NVARCHAR (2),
         duration                  INT,
	       CreatedBy		             INT
      )
      DECLARE @RecordCnt   INT
      SET @duration = 250
      SET @Index = 1

      SET @eventscheduletaskid = 0
      SELECT @eventscheduletaskid = EST.EventScheduleTaskID
        FROM EventScheduleTask EST
       WHERE upper (EST.EventScheduleTaskName) = 'USP_SERVICE_REQUEST_AUTO_EMAIL_RULE1'


      SELECT @duration =
                  CONVERT (INT, substring (EventScheduleTime, 1, 2)) * 60
                + CONVERT (INT, substring (EventScheduleTime, 4, 2))
        FROM EventSchedule
       WHERE EventScheduleID =
                (SELECT EventScheduleID
                   FROM EventScheduleTask
                  WHERE EventScheduleTaskID = @eventscheduletaskid)

      --print 'Duration ' + convert(varchar,@duration)
      
      --Fethching Service Request details which are approve but not Confirmed and inserting into @pendingServiceRequests
      INSERT INTO @pendingServiceRequests (ServiceRequestId,
                                           VCN,
                                           ToPositionPortCode,
                                           ToPositionQuayCode,
                                           ToPositionBerthCode,
                                           ToPositionBollardCode,
                                           MovementType,
                                           MovementDateTime,
                                           CurrentDatetime,
                                           WorkflowTaskCode,
                                           WorkflowInstanceId,
                                           PortCode,duration,CreatedBy)
         SELECT SR.ServiceRequestID,
                SR.VCN,
                VC.ToPositionPortCode,
                VC.ToPositionQuayCode,
                VC.ToPositionBerthCode,
                VC.FromPositionBollardCode,
                SR.MovementType,
                SR.MovementDateTime,
                getdate () AS CurrentDatetime,
                WI.WorkflowTaskCode,
                WI.WorkflowInstanceId,
                WI.PortCode,
                --datediff (hour, getdate (), SR.MovementDateTime) duration_hours,
                datediff (minute, getdate (), SR.MovementDateTime) AS duration_minutes,
		            SR.CreatedBy
           FROM ServiceRequest SR
                INNER JOIN WorkflowInstance WI
                   ON WI.WorkflowInstanceId = SR.WorkflowInstanceID
                INNER JOIN VesselCall VC ON VC.VCN = SR.VCN
                INNER JOIN ArrivalNotification an ON an.VCN = SR.VCN   
                INNER JOIN (
				
				
				 select VCN from (
                select count(AR.VCN) reasoncount, AR.VCN from ArrivalReason AR 
                inner join VesselCall VC on VC.VCN = AR.VCN  
                where VC.ATD is null  and AR.vcn in (
                select VCN from ArrivalReason where  Reason = 'BUNK') 
                group by AR.VCN 
               )a where reasoncount > 1
			   
			    union 
			    select  AR.VCN from ArrivalReason AR 
                inner join VesselCall VC on VC.VCN = AR.VCN  
                where VC.ATD is null  and AR.vcn in (
                select VCN from ArrivalReason where  Reason != 'BUNK') 
                group by AR.VCN
			   
			   
			   )BA on BA.VCN = SR.VCN                
          WHERE     WI.WorkflowTaskCode = (SELECT PC.ConfigValue FROM PortGeneralConfig PC WHERE PC.PortCode = WI.Portcode AND PC.ConfigName ='ApproveCode')
                AND datediff (minute, getdate (), SR.MovementDateTime) <= @duration
                AND SR.ServiceRequestID NOT IN (SELECT CONVERT (
                                                          INT,
                                                          EST.Reference)
                                                  FROM EventScheduleTrack EST
                                                 WHERE EST.EventScheduleTaskID =
                                                          @eventscheduletaskid)

      SELECT @RecordCnt = COUNT (ServiceRequestID)
      FROM @pendingServiceRequests

      --  print '@pendingServiceRequests record count : ' + convert(varchar,@Index)
      
      WHILE (@Index <= @RecordCnt)
      BEGIN
         BEGIN TRY
            BEGIN TRANSACTION

            SELECT @WorkflowInstanceId = PR.WorkflowInstanceId,@portcode = PR.PortCode,@reference = PR.ServiceRequestID,@usertypeid = PR.CreatedBy, @usertype = coalesce((select UserType from Users Where UserId = PR.CreatedBy) ,'EMP')
            FROM @pendingServiceRequests PR
            WHERE PendingServiceRequestID = @Index
            
            --    print 'Reference : ' + @reference

            INSERT INTO [Notification] (NotificationTemplateCode,
                                        [DateTime],
                                        Reference,
                                        EmailStatus,
                                        SMSStatus,
                                        SystemNotificationStatus,
                                        UserTypeId,
                                        UserType,
                                        PortCode,
                                        RecordStatus,
                                        CreatedBy,
                                        CreatedDate,
                                        ModifiedBy,
                                        ModifiedDate)
            VALUES (N'SRC1',
                    getdate (),
                    @reference,
                    'O',
                    'O',
                    'O',
                    @usertypeid,
                    @usertype,
                    @portcode,
                    N'A',
                    1,
                    getdate (),
                    NULL,
                    getdate ()
                    )

            SELECT @eventscheduletrackid = SCOPE_IDENTITY ();

            --    print @eventscheduletrackid

            INSERT INTO @EventScheduleTrack (EventScheduleTaskID,
                                             NotificationId,
                                             Reference,
                                             WorkflowInstanceId)
            VALUES (@eventscheduletaskid,
                    @eventscheduletrackid,
                    @reference,
                    @WorkflowInstanceId)

            --Final Insertion Tables
            INSERT INTO EventScheduleTrack (EventScheduleTaskID,
                                            NotificationId,
                                            Reference,
                                            WorkflowInstanceId)
            VALUES (@eventscheduletaskid,
                    @eventscheduletrackid,
                    @reference,
                    @WorkflowInstanceId)

            SELECT @Index = @Index + 1
            COMMIT TRANSACTION
         END TRY
         BEGIN CATCH
            IF @@TRANCOUNT > 0
               ROLLBACK TRANSACTION
         END CATCH
      END
   END
GO



ALTER PROCEDURE [dbo].[usp_Service_Request_Auto_Email_Rule2]
AS
   BEGIN
      SET  NOCOUNT ON
      SET  XACT_ABORT ON

      DECLARE @duration               INT
      DECLARE @Index                  INT
      DECLARE @eventscheduletrackid   INT
      DECLARE @eventscheduletaskid    INT
      DECLARE @reference              NVARCHAR (12)
      DECLARE @WorkflowInstanceId     INT
      DECLARE @portcode               NVARCHAR (2)
      DECLARE @usertypeid	            INT
      DECLARE @usertype		            nvarchar(4)
      DECLARE @EventScheduleTrack TABLE
      (
         EventScheduleTrackID   INT IDENTITY (1, 1),
         EventScheduleTaskID    INT,
         NotificationId         INT,
         Reference              NVARCHAR (12),
         WorkflowInstanceId     INT,
         WorkflowProcessId      INT
      )

      DECLARE @pendingServiceRequests TABLE
      (
         PendingServiceRequestID   INT IDENTITY (1, 1),
         ServiceRequestId          INT,
         VCN                       NVARCHAR (12),
         ToPositionPortCode        NVARCHAR (2),
         ToPositionQuayCode        NVARCHAR (4),
         ToPositionBerthCode       NVARCHAR (4),
         ToPositionBollardCode     NVARCHAR (4),
         MovementType              NVARCHAR (4),
         MovementDateTime          DATETIME,
         CurrentDatetime           DATETIME,
         WorkflowTaskCode          NVARCHAR (4),
         WorkflowInstanceId        INT,
         PortCode                  NVARCHAR (2),
         duration                  INT,
	 CreatedBy		   INT
      )
      DECLARE @RecordCnt   INT
      SET @duration = 250
      SET @Index = 1

      SET @eventscheduletaskid = 0
      SELECT @eventscheduletaskid = EST.EventScheduleTaskID
        FROM EventScheduleTask EST
       WHERE upper (EST.EventScheduleTaskName) =
                'USP_SERVICE_REQUEST_AUTO_EMAIL_RULE2'

      SELECT @duration =
                  CONVERT (INT, substring (EventScheduleTime, 1, 2)) * 60
                + CONVERT (INT, substring (EventScheduleTime, 4, 2))
        FROM EventSchedule
       WHERE EventScheduleID =
                (SELECT EventScheduleID
                   FROM EventScheduleTask
                  WHERE EventScheduleTaskID = @eventscheduletaskid)

      --print 'Duration ' + convert(varchar,@duration)
      --Fethching Service Request details which are approve but not Confirmed and inserting into @pendingServiceRequests
      INSERT INTO @pendingServiceRequests (ServiceRequestId,
                                           VCN,
                                           ToPositionPortCode,
                                           ToPositionQuayCode,
                                           ToPositionBerthCode,
                                           ToPositionBollardCode,
                                           MovementType,
                                           MovementDateTime,
                                           CurrentDatetime,
                                           WorkflowTaskCode,
                                           WorkflowInstanceId,
                                           PortCode,
                                           duration,CreatedBy)
         SELECT SR.ServiceRequestID,
                SR.VCN,
                VC.ToPositionPortCode,
                VC.ToPositionQuayCode,
                VC.ToPositionBerthCode,
                VC.FromPositionBollardCode,
                SR.MovementType,
                SR.MovementDateTime,
                getdate () AS CurrentDatetime,
                WI.WorkflowTaskCode,
                WI.WorkflowInstanceId,
                WI.PortCode,
                --datediff (hour, getdate (), SR.MovementDateTime) duration_hours,
                datediff (minute, getdate (), SR.MovementDateTime) AS duration_minutes,
               SR.CreatedBy
           FROM ServiceRequest SR
                INNER JOIN WorkflowInstance WI
                   ON WI.WorkflowInstanceId = SR.WorkflowInstanceID
                INNER JOIN VesselCall VC ON VC.VCN = SR.VCN
                INNER JOIN ArrivalNotification an ON an.VCN = SR.VCN   
                INNER JOIN ( 
                
                select VCN from (
                select count(AR.VCN) reasoncount, AR.VCN from ArrivalReason AR 
                inner join VesselCall VC on VC.VCN = AR.VCN  
                where VC.ATD is null  and AR.vcn in (
                select VCN from ArrivalReason where  Reason = 'BUNK') 
                group by AR.VCN 
               )a where reasoncount > 1
            union 
			        select  AR.VCN from ArrivalReason AR 
                inner join VesselCall VC on VC.VCN = AR.VCN  
                where VC.ATD is null  and AR.vcn in (
                select VCN from ArrivalReason where  Reason != 'BUNK') 
                group by AR.VCN              
               
               )BA on BA.VCN = SR.VCN
          WHERE     WI.WorkflowTaskCode = (SELECT PC.ConfigValue FROM PortGeneralConfig PC WHERE PC.PortCode = WI.Portcode AND PC.ConfigName ='ApproveCode')
                AND datediff (minute, getdate (), SR.MovementDateTime) <=
                       @duration
                AND SR.ServiceRequestID NOT IN (SELECT CONVERT (
                                                          INT,
                                                          EST.Reference)
                                                  FROM EventScheduleTrack EST
                                                 WHERE EST.EventScheduleTaskID =
                                                          @eventscheduletaskid);

      SELECT @RecordCnt = COUNT (ServiceRequestID)
      FROM @pendingServiceRequests

      --  print '@pendingServiceRequests record count : ' + convert(varchar,@Index)
      WHILE (@Index <= @RecordCnt)
      BEGIN
         BEGIN TRY
            BEGIN TRANSACTION

            SELECT @WorkflowInstanceId = PR.WorkflowInstanceId,@portcode = PR.PortCode,@reference = PR.ServiceRequestID,@usertypeid = PR.CreatedBy, @usertype = coalesce((select UserType from Users Where UserId = PR.CreatedBy) ,'EMP')
            FROM @pendingServiceRequests PR
            WHERE PendingServiceRequestID = @Index
            
            --    print 'Reference : ' + @reference

            INSERT INTO [Notification] (NotificationTemplateCode,
                                        [DateTime],
                                        Reference,
                                        EmailStatus,
                                        SMSStatus,
                                        SystemNotificationStatus,
                                        UserTypeId,
                                        UserType,
                                        PortCode,
                                        RecordStatus,
                                        CreatedBy,
                                        CreatedDate,
                                        ModifiedBy,
                                        ModifiedDate)
            VALUES (N'SRC2',
                    getdate (),
                    @reference,
                    'O',
                    'O',
                    'O',
                    @usertypeid,
                    @usertype,
                    @portcode,
                    N'A',
                    1,
                    getdate (),
                    NULL,
                    getdate ()
                    )

            SELECT @eventscheduletrackid = SCOPE_IDENTITY ();

            --    print @eventscheduletrackid

            INSERT INTO @EventScheduleTrack (EventScheduleTaskID,
                                             NotificationId,
                                             Reference,
                                             WorkflowInstanceId)
            VALUES (@eventscheduletaskid,
                    @eventscheduletrackid,
                    @reference,
                    @WorkflowInstanceId)

            --Final Insertion Tables
            INSERT INTO EventScheduleTrack (EventScheduleTaskID,
                                            NotificationId,
                                            Reference,
                                            WorkflowInstanceId)
            VALUES (@eventscheduletaskid,
                    @eventscheduletrackid,
                    @reference,
                    @WorkflowInstanceId)

            SELECT @Index = @Index + 1
            COMMIT TRANSACTION
         END TRY
         BEGIN CATCH
            IF @@TRANCOUNT > 0
               ROLLBACK TRANSACTION
         END CATCH
      END
   END
GO



ALTER PROCEDURE [dbo].[usp_Service_Request_Auto_Email_Rule3]
AS
   BEGIN
      SET  NOCOUNT ON
      SET  XACT_ABORT ON
      DECLARE @duration   INT
      DECLARE @Index   INT
      DECLARE @eventscheduletrackid   INT
      DECLARE @eventscheduletaskid   INT
      DECLARE @reference   NVARCHAR (12)
      DECLARE @WorkflowInstanceId   INT
      DECLARE @portcode   NVARCHAR (2)
      DECLARE @MovementType NVARCHAR (4)
      DECLARE @BPWorkflowInstanceId   INT
      DECLARE @EventScheduleTrack TABLE
                                  (
                                     EventScheduleTrackID   INT
                                                               IDENTITY (1, 1),
                                     EventScheduleTaskID    INT,
                                     NotificationId         INT,
                                     Reference              NVARCHAR (12),
                                     WorkflowInstanceId     INT,
                                     WorkflowProcessId      INT
                                  )

      DECLARE @usertypeid	            INT
      DECLARE @usertype		            nvarchar(4)
      DECLARE @pendingServiceRequests TABLE
                                      (
                                         PendingServiceRequestID   INT
                                                                      IDENTITY (1, 1),
                                         ServiceRequestId          INT,
                                         VCN                       NVARCHAR (12),
                                         ToPositionPortCode        NVARCHAR (2),
                                         ToPositionQuayCode        NVARCHAR (4),
                                         ToPositionBerthCode       NVARCHAR (4),
                                         ToPositionBollardCode     NVARCHAR (4),
                                         MovementType              NVARCHAR (4),
                                         MovementDateTime          DATETIME,
                                         CurrentDatetime           DATETIME,
                                         WorkflowTaskCode          NVARCHAR (4),
                                         WorkflowInstanceId        INT,
                                         PortCode                  NVARCHAR (2),
                                         BPWorkflowInstanceId      INT,
                                         duration                  INT,
					 CreatedBy		   INT
                                      )
      DECLARE @RecordCnt   INT
      SET @duration = 250
      SET @Index = 1

      SET @eventscheduletaskid = 0
      SELECT @eventscheduletaskid = EST.EventScheduleTaskID
        FROM EventScheduleTask EST
       WHERE upper (EST.EventScheduleTaskName) =
                'USP_SERVICE_REQUEST_AUTO_EMAIL_RULE3'

      SELECT @duration =
                  CONVERT (INT, substring (EventScheduleTime, 1, 2)) * 60
                + CONVERT (INT, substring (EventScheduleTime, 4, 2))
        FROM EventSchedule
       WHERE EventScheduleID =
                (SELECT EventScheduleID
                   FROM EventScheduleTask
                  WHERE EventScheduleTaskID = @eventscheduletaskid)

      --print 'Duration ' + convert(varchar,@duration)
      --Fethching Service Request details which are approve but not Confirmed and inserting into @pendingServiceRequests
      INSERT INTO @pendingServiceRequests (ServiceRequestId,
                                           VCN,
                                           ToPositionPortCode,
                                           ToPositionQuayCode,
                                           ToPositionBerthCode,
                                           ToPositionBollardCode,
                                           MovementType,
                                           MovementDateTime,
                                           CurrentDatetime,
                                           WorkflowTaskCode,
                                           WorkflowInstanceId,
                                           PortCode,
                                           BPWorkflowInstanceId,
                                           duration,CreatedBy)
         SELECT SR.ServiceRequestID,
                SR.VCN,
                VC.ToPositionPortCode,
                VC.ToPositionQuayCode,
                VC.ToPositionBerthCode,
                VC.FromPositionBollardCode,
                SR.MovementType,
                SR.MovementDateTime,
                getdate () AS CurrentDatetime,
                WI.WorkflowTaskCode,
                WI.WorkflowInstanceId,
                WI.PortCode,
                SR.BPWorkflowInstanceId,
                --datediff (hour, getdate (), SR.MovementDateTime) duration_hours,
                datediff (minute, getdate (), SR.MovementDateTime) AS duration_minutes,
		SR.CreatedBy
           FROM ServiceRequest SR
                INNER JOIN WorkflowInstance WI
                   ON WI.WorkflowInstanceId = SR.WorkflowInstanceID
                INNER JOIN VesselCall VC ON VC.VCN = SR.VCN
                INNER JOIN ArrivalNotification an ON an.VCN = SR.VCN   
                INNER JOIN (
                select VCN from (
                select count(AR.VCN) reasoncount, AR.VCN from ArrivalReason AR 
                inner join VesselCall VC on VC.VCN = AR.VCN  
                where VC.ATD is null  and AR.vcn in (
                select VCN from ArrivalReason where  Reason = 'BUNK') 
                 group by AR.VCN 
               )a where reasoncount > 1
        union 
			    select  AR.VCN from ArrivalReason AR 
                inner join VesselCall VC on VC.VCN = AR.VCN  
                where VC.ATD is null  and AR.vcn in (
                select VCN from ArrivalReason where  Reason != 'BUNK') 
                group by AR.VCN               
               )BA on BA.VCN = SR.VCN
          WHERE     WI.WorkflowTaskCode = (SELECT PC.ConfigValue FROM PortGeneralConfig PC WHERE PC.PortCode = WI.Portcode AND PC.ConfigName ='ApproveCode')
                AND datediff (minute, getdate (), SR.MovementDateTime) <=
                       @duration
                AND SR.ServiceRequestID NOT IN (SELECT CONVERT (
                                                          INT,
                                                          EST.Reference)
                                                  FROM EventScheduleTrack EST
                                                 WHERE EST.EventScheduleTaskID =
                                                          @eventscheduletaskid);
                                                          
     --Fethching Service Request Shifting details inserting into @pendingServiceRequests                                                      
      INSERT INTO @pendingServiceRequests (ServiceRequestId,
                                           VCN,
                                           ToPositionPortCode,
                                           ToPositionQuayCode,
                                           ToPositionBerthCode,
                                           ToPositionBollardCode,
                                           MovementType,
                                           MovementDateTime,
                                           CurrentDatetime,
                                           WorkflowTaskCode,
                                           WorkflowInstanceId,
                                           PortCode,
                                           BPWorkflowInstanceId,
                                           duration,CreatedBy)
         SELECT SR.ServiceRequestID,
                SR.VCN,
                VC.ToPositionPortCode,
                VC.ToPositionQuayCode,
                VC.ToPositionBerthCode,
                VC.FromPositionBollardCode,
                SR.MovementType,
                SR.MovementDateTime,
                getdate () AS CurrentDatetime,
                WI.WorkflowTaskCode,
                WI.WorkflowInstanceId,
                WI.PortCode,
                SR.BPWorkflowInstanceId,
                --datediff (hour, getdate (), SR.MovementDateTime) duration_hours,
                datediff (minute, getdate (), SR.MovementDateTime) AS duration_minutes,
		SR.CreatedBy
           FROM ServiceRequest SR
               INNER JOIN WorkflowInstance WI
                   ON WI.WorkflowInstanceId = SR.BPWorkflowInstanceId
                   INNER JOIN VesselCall VC ON VC.VCN = SR.VCN
                   INNER JOIN ArrivalNotification an ON an.VCN = SR.VCN   
                   INNER JOIN ( select VCN from (
                   select count(AR.VCN) reasoncount, AR.VCN from ArrivalReason AR 
                   inner join VesselCall VC on VC.VCN = AR.VCN  
                   where VC.ATD is null  and AR.vcn in (
                   select VCN from ArrivalReason where  Reason = 'BUNK') 
                   group by AR.VCN 
                 )a where reasoncount > 1
               union 
			    select  AR.VCN from ArrivalReason AR 
                inner join VesselCall VC on VC.VCN = AR.VCN  
                where VC.ATD is null  and AR.vcn in (
                select VCN from ArrivalReason where  Reason != 'BUNK') 
                group by AR.VCN                 
                 )BA on BA.VCN = SR.VCN
           WHERE SR.MovementType = 'SHMV' AND SR.BPWorkflowInstanceId is not null AND SR.WorkflowInstanceID is null 
       AND SR.RecordStatus='A' AND datediff (minute, getdate (), SR.MovementDateTime) <=
                       @duration
                AND SR.ServiceRequestID NOT IN (SELECT CONVERT (
                                                          INT,
                                                          EST.Reference)
                                                  FROM EventScheduleTrack EST
                                                 WHERE EST.EventScheduleTaskID =
                                                          @eventscheduletaskid);                                                          
                                                          

      SELECT @RecordCnt = COUNT (ServiceRequestID)
      FROM @pendingServiceRequests


      --  print '@pendingServiceRequests record count : ' + convert(varchar,@Index)
      WHILE (@Index <= @RecordCnt)
      BEGIN
         BEGIN TRY
            BEGIN TRANSACTION

            SELECT @WorkflowInstanceId = PR.WorkflowInstanceId,
                   @BPWorkflowInstanceId = PR.BPWorkflowInstanceId,
                   @portcode = PR.PortCode,
                   @reference = PR.ServiceRequestID,
                   @MovementType = PR.MovementType,
                   @usertypeid = PR.CreatedBy, @usertype = coalesce((select UserType from Users Where UserId = PR.CreatedBy) ,'EMP')
            FROM @pendingServiceRequests PR
            WHERE PendingServiceRequestID = @Index

            --    print 'Reference : ' + @reference

            INSERT INTO [Notification] (NotificationTemplateCode,
                                        [DateTime],
                                        Reference,
                                        EmailStatus,
                                        SMSStatus,
                                        SystemNotificationStatus,
                                        UserTypeId,
                                        UserType,
                                        PortCode,
                                        RecordStatus,
                                        CreatedBy,
                                        CreatedDate,
                                        ModifiedBy,
                                        ModifiedDate)
            VALUES (N'SRVC',
                    getdate (),
                    @reference,
                    'O',
                    'O',
                    'O',
                    @usertypeid,
                    @usertype,
                    @portcode,
                    N'A',
                    1,
                    getdate (),
                    NULL,
                    getdate ())

            SELECT @eventscheduletrackid = SCOPE_IDENTITY ();

            --    print @eventscheduletrackid

            DECLARE @WorkflowProcessId   INT
            DECLARE @RoleId   INT
            DECLARE @FromTaskCode   NVARCHAR (4)
            DECLARE @ToTaskCode   NVARCHAR (4)
            DECLARE @ReferenceData   NVARCHAR (MAX)
            DECLARE @CreatedBy   INT
            DECLARE @CreatedDate   DATETIME


            SELECT @RoleId = WP.RoleId,
                   @FromTaskCode = WP.ToTaskCode,
                   @ReferenceData = WP.ReferenceData,
                   @CreatedBy = WP.CreatedBy
              FROM WorkflowProcess WP INNER JOIN WorkflowInstance WI on WP.WorkflowProcessId = WI.WorkflowProcessId
             WHERE WI.WorkflowInstanceId = @WorkflowInstanceId

            SELECT @ToTaskCode =
                      (SELECT PC.ConfigValue FROM PortGeneralConfig PC WHERE PC.PortCode = @portcode AND PC.ConfigName ='CancelCode')

            --print @RoleId; print @FromTaskCode; print @ReferenceData; print @CreatedBy; print @CreatedDate; print @ToTaskCode;print @RecordStatus

            SELECT @CreatedDate = (SELECT getdate ())

            --Raising Cancellation Service Request
            EXEC usp_WorkflowProcess_dml @WorkflowProcessId OUTPUT,
                                         @WorkflowInstanceId,
                                         @RoleId,
                                         @FromTaskCode,
                                         @ToTaskCode,
                                         @ReferenceData,
                                         'Auto Service Request Cancel',
                                         'A',
                                         @CreatedBy,
                                         @CreatedDate,
                                         @CreatedBy,
                                         @CreatedDate


            UPDATE WorkflowInstance
               SET WorkflowTaskCode = @ToTaskCode,
                   ModifiedDate = @CreatedDate,
                   ModifiedBy=(select userid from Users where UPPER(UserName)='ADMIN')
             WHERE WorkflowInstanceId = @WorkflowInstanceId
            --For Shiftng cancellaion-----.
           
            IF @MovementType = 'SHMV'
            BEGIN
            SELECT @RoleId = WP.RoleId,
                   @FromTaskCode = WP.ToTaskCode,
                   @ReferenceData = WP.ReferenceData,
                   @CreatedBy = WP.CreatedBy
              FROM WorkflowProcess WP INNER JOIN WorkflowInstance WI on WP.WorkflowProcessId = WI.WorkflowProcessId
             WHERE WI.WorkflowInstanceId = @BPWorkflowInstanceId

            SELECT @CreatedDate = (SELECT getdate ())

            --Raising Cancellation Service Request
            EXEC usp_WorkflowProcess_dml @WorkflowProcessId OUTPUT,
                                         @BPWorkflowInstanceId,
                                         @RoleId,
                                         @FromTaskCode,
                                         @ToTaskCode,
                                         @ReferenceData,
                                         'Auto Service Request Cancel',
                                         'A',
                                         @CreatedBy,
                                         @CreatedDate,
                                         @CreatedBy,
                                         @CreatedDate


            UPDATE WorkflowInstance
               SET WorkflowTaskCode = @ToTaskCode,
                   ModifiedDate = @CreatedDate,
		   ModifiedBy=(select userid from Users where UPPER(UserName)='ADMIN')
             WHERE WorkflowInstanceId = @BPWorkflowInstanceId
            END
            -----------------------------
                        
            update ServiceRequest set RecordStatus='I', ModifiedBy=(select userid from Users where UPPER(UserName)='ADMIN'),ModifiedDate=getdate() where ServiceRequestID = CAST(@reference AS int);
            
            update VesselCallMovement set RecordStatus='I', ModifiedBy=(select userid from Users where UPPER(UserName)='ADMIN'),ModifiedDate=getdate(),
            MovementStatus='MPEN',FromPositionQuayCode=null,FromPositionBerthCode=null,FromPositionBollardCode=null,
            ToPositionQuayCode=null,ToPositionBerthCode=null,ToPositionBollardCode=null,ToPositionPortCode=null            
            where ServiceRequestID = CAST(@reference AS int);

            INSERT INTO @EventScheduleTrack (EventScheduleTaskID,
                                             NotificationId,
                                             Reference,
                                             WorkflowInstanceId,
                                             WorkflowProcessId)
            VALUES (@eventscheduletaskid,
                    @eventscheduletrackid,
                    @reference,
                    @WorkflowInstanceId,
                    @WorkflowProcessId)

            --Final Insertion Tables
            INSERT INTO EventScheduleTrack (EventScheduleTaskID,
                                            NotificationId,
                                            Reference,
                                            WorkflowInstanceId,
                                            WorkflowProcessId)
            VALUES (@eventscheduletaskid,
                    @eventscheduletrackid,
                      @reference,
                    @WorkflowInstanceId,
                    @WorkflowProcessId)


            SELECT @Index = @Index + 1
            COMMIT TRANSACTION
         END TRY
         BEGIN CATCH
            IF @@TRANCOUNT > 0
               ROLLBACK TRANSACTION
         END CATCH
      END
   END
GO

Update NotificationTemplate set 
EmailTemplate = '<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Confirm below Service Request details.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Service Request Details</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Movement Type</td><td width="62%">%MovementName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Movement Date & Time</td><td width="62%">%MovementDateTime%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Requested Date & Time</td><td width="62%">%SubmittedDateTime%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
where NotificationTemplateCode = 'SRC1'

GO

Update NotificationTemplate set 
EmailTemplate = '<p style="margin-bottom: 12pt;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Greetings &nbsp;[UserName],</span></p><p style="margin-bottom: 12pt;"></p><br/><meta charset="utf-8"/><title>Integrated Port Management System</title><table align="center" border="0" cellpadding="0" cellspacing="0" width="550"><tbody><tr><td width="328" height="71" valign="bottom" bgcolor="#dddddd"><table width="548" border="0" align="center" cellpadding="0" cellspacing="0"><tbody><tr><td valign="bottom"><table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td height="39" colspan="2" bgcolor="#FFFFFF">&nbsp;</td></tr><tr style="background-color: #1d1d1d;"><td height="30"  style="font-family: Verdana;font-size: 14px;font-family: Open Sans, sans-serif;color: #fff; padding-left:2px;">Integrated Port Management System</td><td  style="font-family: Verdana;font-size: 12px;color: #f03225;text-align: right;font-weight: bold;"></td></tr></table></td><td width="17" height="69" valign="bottom" bgcolor="#dddddd"><img height="69" src="https://ipms.transnet.net/Content/Images/email-logo.jpg" width="114"/></td></tr></tbody></table></td></tr><tr><td style="font-family: Verdana;font-size: 12px;background-color: #aacdb3;padding: 25px;"><span style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Confirm below Service Request details.</span><br/><br/><table border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 12px;background-color: #c9ead1;border: 2px solid #88a790;padding: 6px 4px;color: #3d5343;" width="100%"><tbody><tr><td><span style="font-size: 18px;color: #3d5343;padding-bottom: 5px;display: block;">Service Request Details</span><table border="0" cellpadding="5" cellspacing="1" style="font-family: Verdana;font-size: 12px;" width="100%"><tbody><tr style="background-color: #aacdb3;"><td width="38%">VCN</td><td width="62%">%VCN%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Vessel Name</td><td width="62%">%VesselName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Movement Type</td><td width="62%">%MovementName%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Movement Date & Time</td><td width="62%">%MovementDateTime%</td></tr><tr style="background-color: #aacdb3;"><td width="38%">Requested Date & Time</td><td width="62%">%SubmittedDateTime%</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td><p><img height="15" src="https://ipms.transnet.net/Content/Images/bottom-bar.jpg" width="550"/></p></td></tr></tbody></table><br/><table width="550" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 11px;"><tr><td height="30" colspan="2"style="font-size: 9.0pt;font-family: Verdana,sans-serif;">Kind Regards</td></tr><tr><td width="140" valign="top"><img src="https://ipms.transnet.net/Content/Images/transet-logo-email-sign.png" width="128" height="119" /></td><td width="410"><p><strong>IPMS ADMIN </strong><br /></p><table width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family: Verdana;font-size: 10px;"><tr><td width="23"><img width="23" height="16" src="https://ipms.transnet.net/Content/Images/email-temp-phone-icon.jpg"/></td><td height="25">(+27 86) 010 9330</td></tr><tr><td>&nbsp;</td><td height="25"><a href="http://www.transnet.net">www.transnet.net</a></td></tr></table><p>&nbsp;</p></td></tr></table>'
where NotificationTemplateCode = 'SRC2'

GO
INSERT INTO WorkflowTask(EntityID, WorkflowTaskCode, Step, NextStep, ValidityPeriod, HasNotification, APIUrl, RecordStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, PortCode, HasRemarks) 
SELECT (select EntityId from Entity where EntityCode='SERVSHFT') EntityId, 'WFCA' as WorkflowTaskCode, 30 Step,9999 NextStep,0 ValidityPeriod, 'Y' HasNotification, '' APIUrl,'A' as RecordStatus,1 as CreatedBy,getdate() as CreatedDate,1 as ModifiedBy, getdate() as ModifiedDate , PortCode, 'N' HasRemarks from Port order by portcode


INSERT INTO dbo.WorkflowTaskRole ( EntityID ,RoleID ,Step ,PortCode ,RecordStatus ,CreatedBy ,CreatedDate ,ModifiedBy ,ModifiedDate) SELECT (select EntityId from Entity where EntityCode='SERVSHFT') EntityId, RoleId, 30 StepId, PortCode, 'A' RecordStatus, 1 CreatedBy, getdate() CreatedDate, 1 ModifiedBy, getdate() ModifiedDate from Port, ((select roleid from Role where Rolecode in (select value from udf_SplitString('AGNT',',')))) A Order By PortCode

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

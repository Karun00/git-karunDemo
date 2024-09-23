using Core.Repository;
using Core.Repository.Providers.EntityFramework;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace SAPNotifications.Engine
{
    public class SAPNotificationEngine
    {


        private ISAPIntegrationRepository _sapintegrationRepository;
        private ILog log;
        private int saplastNotificationId;
        private bool isInProcess;
        public static int processcount = 0;
        private CompanyVO nextStepCompany;
        public SAPNotificationEngine()
        {
            try
            {
                XmlConfigurator.Configure();
                log = LogManager.GetLogger(typeof(SAPNotificationEngine));
                saplastNotificationId = 0;
                isInProcess = false;
            }
            catch (Exception ex)
            {
                log.Error("Exception = " + ex.Message + Convert.ToChar(13) + "Location :" + ex.StackTrace);
            }
        }

        public bool IsInProcess()
        {
            return isInProcess;
        }

        public void Start()
        {
            try
            {
                isInProcess = true;
                List<SAPPosting> pendingNotifications = GetSAPPendingNotifications();
                if (pendingNotifications != null)
                {
                    if (pendingNotifications.Count > 0)
                    {
                        saplastNotificationId = pendingNotifications.OrderBy(t => t.SAPPostingID).Last().SAPPostingID;
                        Stopwatch wfstopwatch = Stopwatch.StartNew();

                        log.Info("Request 1 : Vessel Create Started...");
                        Post_Of_Vessel_Detail_ID100(null);
                        log.Info("Request 1 : Vessel Create Completed...");

                        log.Info("Request 2 : Vessel Arrival Create Started...");
                        Post_Of_Arrival_Details_ID100(null);
                        log.Info("Request 2 : Vessel Arrival Create Completed...");

                        log.Info("Request 3 : Vessel Arrival Update Started...");
                        Update_Of_Arrival_Details_ID100(null);
                        log.Info("Request 3 : Vessel Arrival Update Completed...");

                        log.Info("Request 4 : Marine Order Create Started...");
                        Post_Of_Marine_Order_Id100(null);
                        log.Info("Request 4 : Marine Order Create Completed...");

                        log.Info("Request 5 : Marine Order Update Started...");
                        Update_Of_Marine_Order_Id100(null);
                        log.Info("Request 5 : Marine Order Update Completed...");

                        log.Info("Request 6 : SAP Invoice Fetching Started...");
                        Post_Of_Invoice_Detail_ID100(null);
                        log.Info("Request 6 : SAP Invoice Fetching Completed...");

                        wfstopwatch.Stop();
                        log.Debug("Time taken for Batch Ends with:" + saplastNotificationId.ToString(CultureInfo.InvariantCulture) + " (No of records " + pendingNotifications.Count.ToString(CultureInfo.InvariantCulture) + " )  : " + wfstopwatch.Elapsed.ToString());

                    }
                    else
                    {
                        log.Info("No Pending SAP Notifications for process...");
                    }
                }
                else
                    log.Info("No Pending SAP Notifications for process...");

            }
            catch (Exception ex)
            {

                log_error("SAPNotificationEngine Start() method Error : ", ex);
            }
            finally
            {
                UpdateNotificationBatch(saplastNotificationId);
                isInProcess = false;
            }
        }

        private void ProcessNotifications(List<SAPPosting> pendingNotifications)
        {
            foreach (SAPPosting pendingNotification in pendingNotifications)
            {
                Stopwatch wfstopwatch;
                wfstopwatch = Stopwatch.StartNew();
                log.Info(" Process Started For : " + pendingNotification.NotificationTemplateCode + " " + pendingNotification.SAPPostingID);
                try
                {

                    //SAPNotifier notifier = SAPNotifier.GetNotifier(pendingNotification);
                    //using (Notifier notifier = Notifier.GetNotifier(pendingNotification))
                    //{

                    try
                    {
                        log.Error("ProcessNotifications is Pending");
                    }
                    catch (Exception ex)
                    {
                        //pendingNotification.EmailStatus = "E";
                        //pendingNotification.SMSStatus = "E";
                        //pendingNotification.SystemNotificationStatus = "E";
                        log_error("ProcessNotifications FAILED : Notification Id " + pendingNotification.SAPPostingID + ", TemplateCode = " + pendingNotification.NotificationTemplateCode, ex);
                    }
                    finally
                    {
                        //UpdateNotificationsStatus(pendingNotification);
                    }
                }
                catch (Exception ex)
                {
                    //pendingNotification.EmailStatus = "E";
                    //pendingNotification.SMSStatus = "E";
                    //pendingNotification.SystemNotificationStatus = "E";
                    //UpdateNotificationsStatus(pendingNotification);
                    //log_error("ProcessNotifications FAILED : Notification Id " + pendingNotification.SAPNotificationId + ", TemplateCode = " + pendingNotification.NotificationTemplateCode, ex);
                }
                wfstopwatch.Stop();
                log.Info("Time taken for processing of  : " + pendingNotification.SAPPostingID + " Completed in " + wfstopwatch.Elapsed.ToString());
            }
        }


        private void log_error(string pretext, Exception ex)
        {
            string msg = pretext + " " + ex.Message;
            if (ex.InnerException != null)
            {
                if (!string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    msg = msg + " Inner Exception:" + ex.InnerException.Message;
                }
            }
            log.Error(msg);

        }


        /// <summary>
        /// To EmailStatus,SMSStatus & SystemNotificationStatus in Notification tables against NotificationId
        /// </summary>
        /// <param name="openNotification"></param>
        private void UpdateNotificationsStatus(Notification openNotification)
        {
            using (IUnitOfWork uow = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    uow.ExecuteSqlCommand("update dbo.Notification set EmailStatus = @p0, SMSStatus = @p1, SystemNotificationStatus = @p2 where NotificationId = @p3", openNotification.EmailStatus, openNotification.SMSStatus, openNotification.SystemNotificationStatus, openNotification.NotificationId);
                }
                catch (Exception ex)
                {
                    log_error("UpdateNotificationsStatus method ERROR: ", ex);
                }
            }
        }

        /// <summary>
        /// To Update Last Processed Notification ID in PortConfiguration table BATCH wise
        /// </summary>
        /// <param name="openNotification"></param>
        private void UpdateNotificationBatch(int lastNotificationId)
        {
            if (lastNotificationId > 0)
            {
                using (IUnitOfWork uow = new UnitOfWork(new TnpaContext()))
                {
                    try
                    {
                        uow.ExecuteSqlCommand("update dbo.PortConfiguration set SAPLastNotificationId=@p0", lastNotificationId);
                    }
                    catch (Exception ex)
                    {
                        log_error("UpdateNotificationBatch method ERROR: ", ex);
                    }
                }
            }
            else
                log.Error("SAPLastNotificationId is 0 to update Batch");
        }

        /// <summary>
        /// To Update Posting Status of specific SAPPostingID
        /// </summary>
        /// <param name="openNotification"></param>
        private void UpdateSAPNotificationStatus(int sappostingid,string status)
        {
            if (sappostingid > 0)
            {
                using (IUnitOfWork uow = new UnitOfWork(new TnpaContext()))
                {
                    try
                    {
                        uow.ExecuteSqlCommand("update dbo.SAPPosting set PostingStatus=@p0 where SAPPostingID=@p1", status, sappostingid);
                    }
                    catch (Exception ex)
                    {
                        log_error("UpdateSAPNotificationStatus method ERROR: ", ex);
                    }
                }
            }
            else
                log.Error("Unable to update PostingStatus as Posted for SAPPostingID " + sappostingid);
        }

        #region IDisposable Methods

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion



        /// <summary>
        /// Use Case ID: SYIN-101.102
        /// Use Case Name : Posting of Arrival Details/ ID100
        /// Step 1: This method is used to call Use Case : SYIN-101.102 , Posting of Arrival Details/ ID100
        /// </summary>
        public void Post_Of_Arrival_Details_ID100(SAPArrivalVO objSAPArrival)
        {
            IEntityRepository _entity;
            try
            {
                IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext());
                _entity = new EntityRepository(_unitOfWork);
                _sapintegrationRepository = new SAPIntegrationRepository(_unitOfWork);

                nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = "EMP";
                nextStepCompany.UserTypeId = 0;

                if (objSAPArrival == null)
                {
                    List<SAPArrivalVO> _PendingArrivalNotificationlist = _sapintegrationRepository.GetAllArrivalDataforSAP();

                    if (_PendingArrivalNotificationlist.Count > 0)
                    {
                        log.Info("Total Pending SAP Arrival Notification count " + _PendingArrivalNotificationlist.Count);

                        foreach (SAPArrivalVO _PendingArrivalNotificationdata in _PendingArrivalNotificationlist)
                        {
                            #region SAP Integration

                            Stopwatch wfstopwatch = Stopwatch.StartNew();
                            log.Info("Post_Of_Arrival_Details_ID100 for VCN " + _PendingArrivalNotificationdata.VCN);

                           // _PendingArrivalNotificationdata.CODE = "06"; //This value is User based selection from Front End screen.
                            _PendingArrivalNotificationdata.ZZBERTH = _PendingArrivalNotificationdata.ZZBERTH;

                            string Result = Posting_Vessel_Arrival_Biztalk(MapDataToArrivalObject(_PendingArrivalNotificationdata));

                            if (Result.ToUpper() == "CREATED") //This result is only for Acknowledgment, which indicates Successfull data received by Biztalk Service
                            {
                                log.Info("SAP Posting completed for VCN : " + _PendingArrivalNotificationdata.VCN);
                                UpdateSAPNotificationStatus(_PendingArrivalNotificationdata.SAPPostingID, SAPPostingStatus.Posted);
                                //  Publish(_entity.GetEntitiesNotification(EntityCodes.Arrival_Notification).EntityID, _PendingArrivalNotificationdata.SAPArrival.VCN, _PendingArrivalNotificationdata.CreatedBy, nextStepCompany, _PendingArrivalNotificationdata.PortCode, "INTG", _PendingArrivalNotificationdata.SAPArrival);
                            }
                            else
                            {
                                log.Error("Response from BIZTALK/SAP failed for VCN :" + objSAPArrival.VCN + " " + Result);
                            }


                            wfstopwatch.Stop();
                            log.Info("Time taken for processing of  : " + _PendingArrivalNotificationdata.VCN + " Completed in " + wfstopwatch.Elapsed.ToString());
                            #endregion
                        }
                    }
                }
                else
                {
                    string Result = Posting_Vessel_Arrival_Biztalk(MapDataToArrivalObject(objSAPArrival));

                    if (Result.ToUpper() == "CREATED") //This result is only for Acknowledgment, which indicates Successfull data received by Biztalk Service
                    {
                        log.Info("SAP Posting completed for VCN : " + objSAPArrival.VCN);
                        ///Publish(_entity.GetEntitiesNotification(EntityCodes.Arrival_Notification).EntityID, objSAPArrival.SAPArrival.VCN, objSAPArrival.CreatedBy, nextStepCompany, objSAPArrival.PortCode, "INTG", objSAPArrival.SAPArrival);
                    }
                    else
                    {
                        log.Error("Response from BIZTALK/SAP failed for VCN :" + objSAPArrival.VCN + " " + Result);
                    }
                }
            }
            catch (Exception ex)
            {
                log_error("Request 2 : Vessel Arrival Create - Post_Of_Arrival_Details_ID100 - ERROR : ", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private SAPArrivalVO MapDataToArrivalObject(SAPArrivalVO objArrival)
        {
            SAPArrivalVO _objArrival = new SAPArrivalVO();

            _objArrival.AED = objArrival.AED;
            _objArrival.AET = objArrival.AET;
            _objArrival.DED = objArrival.DED;
            _objArrival.DET = objArrival.DET;
            _objArrival.ARRNO = objArrival.ARRNO;
            _objArrival.VCN = objArrival.VCN;
            _objArrival.CODE = objArrival.CODE;
            _objArrival.EDA = objArrival.EDA; //format should be YYYY-MM-DD
            _objArrival.EDD = objArrival.EDD; //format should be YYYY-MM-DD
            _objArrival.KUNNR = objArrival.KUNNR;
            _objArrival.PORTCALL = objArrival.PORTCALL;
            _objArrival.PORTORIGIN = objArrival.PORTORIGIN;
            _objArrival.VKORG = objArrival.VKORG;
            _objArrival.VOYIN = objArrival.VOYIN;
            _objArrival.VOYOUT = objArrival.VOYOUT;
            _objArrival.ZZBERTH = objArrival.ZZBERTH;
            _objArrival.EDD = objArrival.EDD;
            _objArrival.SAPPostingID = objArrival.SAPPostingID;
            _objArrival.MESSAGETYPE = objArrival.MESSAGETYPE;

            return _objArrival;
        }

        /// <summary>
        /// Step 2: This Method is used to Call Biztalk WCF service by sending information as required in the form of SAPArrivalVO object
        /// </summary>
        /// <param name="objArrival"></param>
        private string Posting_Vessel_Arrival_Biztalk(SAPArrivalVO objArrival)
        {
            string res = string.Empty;
            try
            {

                ChannelFactory<VesselArrivalServiceReference.BiztalkArrivalService> cf = new ChannelFactory<VesselArrivalServiceReference.BiztalkArrivalService>("WSHttpBinding_ITwoWayAsync");
                VesselArrivalServiceReference.BiztalkArrivalOperationRequest req = new VesselArrivalServiceReference.BiztalkArrivalOperationRequest(new VesselArrivalServiceReference.SAPArrival());

                log.Info("BIZTALK/SAP connection established successfully");

                VesselArrivalServiceReference.BiztalkArrivalService ch = cf.CreateChannel();
                req.SAPArrival.AED = objArrival.AED;
                req.SAPArrival.AET = objArrival.AET;
                req.SAPArrival.DED = objArrival.DED;
                req.SAPArrival.DET = objArrival.DET;
                req.SAPArrival.ARRNO = objArrival.ARRNO;
                req.SAPArrival.CODE = objArrival.CODE;
                req.SAPArrival.KUNNR = objArrival.KUNNR;
                req.SAPArrival.PORTCALL = objArrival.PORTCALL;
                req.SAPArrival.PORTORIGIN = objArrival.PORTORIGIN;
                req.SAPArrival.VCN = objArrival.VCN;
                req.SAPArrival.VKORG = objArrival.VKORG;
                req.SAPArrival.VOYIN = objArrival.VOYIN;
                req.SAPArrival.VOYOUT = objArrival.VOYOUT;
                req.SAPArrival.ZZBERTH = objArrival.ZZBERTH;
                req.SAPArrival.EDA = objArrival.EDA;
                req.SAPArrival.EDD = objArrival.EDD;
                req.SAPArrival.SAPPostingID = objArrival.SAPPostingID;
                req.SAPArrival.MESSAGETYPE = objArrival.MESSAGETYPE;

                VesselArrivalServiceReference.BiztalkArrivalOperationResponse resp = ch.BiztalkArrivalOperation(req);
                res = resp.Status.Message;

            }
            catch (Exception ex)
            {
                log_error("Posting_Vessel_Arrival_Biztalk method - ERROR : ", ex);
            }
            return res; //Response acknowledgment from Biztalk Service

        }


        /// <summary>
        /// /////////For Updation of Vessel Arrival
        /// </summary>
        /// <param name="objSAPArrival"></param>
        public void Update_Of_Arrival_Details_ID100(SAPArrivalVO objSAPArrival)
        {
            IEntityRepository _entity;
            try
            {
                log.Info("Update_Of_Arrival_Details_ID100 Method started..");
                IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext());
                _entity = new EntityRepository(_unitOfWork);
                _sapintegrationRepository = new SAPIntegrationRepository(_unitOfWork);

                nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = "EMP";
                nextStepCompany.UserTypeId = 0;

                if (objSAPArrival == null)
                {
                    List<SAPArrivalVO> _PendingUpdationArrivalNotificationlist = _sapintegrationRepository.GetAllUpdateArrivalDataforSAP();

                    if (_PendingUpdationArrivalNotificationlist.Count > 0)
                    {
                        log.Info("Total Pending SAP Updating Arrival Notification count " + _PendingUpdationArrivalNotificationlist.Count);

                        foreach (SAPArrivalVO _PendingUpdateArrivalNotificationdata in _PendingUpdationArrivalNotificationlist)
                        {
                            #region SAP Integration

                            Stopwatch wfstopwatch = Stopwatch.StartNew();
                            log.Info("Updation_Of_Arrival_Details_ID100 for VCN " + _PendingUpdateArrivalNotificationdata.VCN);

                            string Result = Posting_Vessel_Arrival_Biztalk(MapDataToArrivalObject(_PendingUpdateArrivalNotificationdata));

                            if (Result.ToUpper() == "CREATED") //This result is only for Acknowledgment, which indicates Successfull data received by Biztalk Service
                            {
                                UpdateSAPNotificationStatus(_PendingUpdateArrivalNotificationdata.SAPPostingID, SAPPostingStatus.Posted);
                                log.Info("SAP Posting completed for VCN For Updation: " + _PendingUpdateArrivalNotificationdata.VCN);
                                //  Publish(_entity.GetEntitiesNotification(EntityCodes.Arrival_Notification).EntityID, _PendingArrivalNotificationdata.SAPArrival.VCN, _PendingArrivalNotificationdata.CreatedBy, nextStepCompany, _PendingArrivalNotificationdata.PortCode, "INTG", _PendingArrivalNotificationdata.SAPArrival);
                            }
                            else
                            {
                                log.Error("Response from BIZTALK/SAP failed for VCN :" + objSAPArrival.VCN + " " + Result);
                            }


                            wfstopwatch.Stop();
                            log.Info("Time taken for processing of  : " + _PendingUpdateArrivalNotificationdata.VCN + " Completed in " + wfstopwatch.Elapsed.ToString());
                            #endregion
                        }
                    }
                }
                else
                {
                    log.Info("Before Posting_Vessel_Arrival_Biztalk Method invoded.."); 
                    string Result = Posting_Vessel_Arrival_Biztalk(MapDataToArrivalObject(objSAPArrival));
                    log.Info("After Posting_Vessel_Arrival_Biztalk Method invoded.."); 

                    if (Result.ToUpper() == "AMENDED") //This result is only for Acknowledgment, which indicates Successfull data received by Biztalk Service
                    {
                        log.Info("SAP Posting completed for VCN : " + objSAPArrival.VCN);
                        ///Publish(_entity.GetEntitiesNotification(EntityCodes.Arrival_Notification).EntityID, objSAPArrival.SAPArrival.VCN, objSAPArrival.CreatedBy, nextStepCompany, objSAPArrival.PortCode, "INTG", objSAPArrival.SAPArrival);
                    }
                    else
                    {
                        log.Error("Response from BIZTALK/SAP failed for VCN :" + objSAPArrival.VCN + " " + Result);
                    }
                }
            }
            catch (Exception ex)
            {
                log_error("Request 3 : Vessel Arrival Update - Update_Of_Arrival_Details_ID100 - ERROR : ", ex);
            }
        }


        ////////////////Marine Order SAP Integration /////////////////////////////////

        public void Post_Of_Marine_Order_Id100(SAPMarineOrderVO objSapMarine)
        {
            IEntityRepository _entity;
            //try
            //{
                IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext());
              //  _entity = new EntityRepository(_unitOfWork);
                _sapintegrationRepository = new SAPIntegrationRepository(_unitOfWork);

                nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = "EMP";
                nextStepCompany.UserTypeId = 0;

                if (objSapMarine == null)
                {
                    List<SAPMarineOrderVO> _PendingMarineOrderslist = _sapintegrationRepository.GetAllMarineDataforSAP();

                    if (_PendingMarineOrderslist.Count > 0)
                    {
                        log.Info("Total Pending SAP Marine Orders Notification count " + _PendingMarineOrderslist.Count);

                        foreach (SAPMarineOrderVO _PendingMarineNotificationdata in _PendingMarineOrderslist)
                        {
                            #region SAP Integration

                            Stopwatch wfstopwatch = Stopwatch.StartNew();
                            log.Info("Post_Of_Marine_Order_ID100 for RevenuePostinID " + _PendingMarineNotificationdata.REVENUEPOSTINGID);

                            string Result = Posting_Marine_Order_Biztalk(MapDataToMarineOrderObject(_PendingMarineNotificationdata));

                            if (Result.ToUpper() == "CREATED") //This result is only for Acknowledgment, which indicates Successfull data received by Biztalk Service
                            {
                                UpdateSAPNotificationStatus(_PendingMarineNotificationdata.SAPPOSTINGID, SAPPostingStatus.Posted);
                                log.Info("SAP Posting completed for RevenuePostinID : " + _PendingMarineNotificationdata.REVENUEPOSTINGID);
                                //  Publish(_entity.GetEntitiesNotification(EntityCodes.Arrival_Notification).EntityID, _PendingArrivalNotificationdata.SAPArrival.VCN, _PendingArrivalNotificationdata.CreatedBy, nextStepCompany, _PendingArrivalNotificationdata.PortCode, "INTG", _PendingArrivalNotificationdata.SAPArrival);
                            }
                            else
                            {
                                log.Error("Response from BIZTALK/SAP failed for Marine Order :" + objSapMarine.REVENUEPOSTINGID + " " + Result);
                            }


                            wfstopwatch.Stop();
                            log.Info("Time taken for processing of Post_Of_Marine_Order_ID100 : " + _PendingMarineNotificationdata.REVENUEPOSTINGID + " Completed in " + wfstopwatch.Elapsed.ToString());
                            #endregion
                        }
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    log_error("Request 4 : Marine Order Create - Post_Of_Marine_Order_ID100 - ERROR : ", ex);
            //}
        }

        ///// <summary>
        ///// 
        ///// </summary>
        private static SAPMarineOrderVO MapDataToMarineOrderObject(SAPMarineOrderVO objMarine)
        {
            SAPMarineOrderVO _objMarineOrder = new SAPMarineOrderVO();
            _objMarineOrder.ZZTIMEFROM = objMarine.ZZTIMEFROM;
            _objMarineOrder.ZZTIMETO = objMarine.ZZTIMETO;
            _objMarineOrder.ZZDATEFROM = objMarine.ZZDATEFROM;
            _objMarineOrder.ZZDATETO = objMarine.ZZDATETO;
            _objMarineOrder.ORDERTYPE = objMarine.ORDERTYPE;
            _objMarineOrder.SALESORGANIZATION = objMarine.SALESORGANIZATION;
            _objMarineOrder.DISTRIBUTIONCHANNEL = objMarine.DISTRIBUTIONCHANNEL;
            _objMarineOrder.DIVISION = objMarine.DIVISION;
            _objMarineOrder.SOLDTOPARTY = objMarine.SOLDTOPARTY;
            _objMarineOrder.SHIPTOPARTY = objMarine.SHIPTOPARTY;
            _objMarineOrder.ZZDOCKTIME = objMarine.ZZDOCKTIME;
            _objMarineOrder.ITEMNO = objMarine.ITEMNO;
            _objMarineOrder.SALESDOCUMENT = objMarine.SALESDOCUMENT;
            _objMarineOrder.MESSAGETYPE = objMarine.MESSAGETYPE;
            _objMarineOrder.SAPPOSTINGID = objMarine.SAPPOSTINGID;
            _objMarineOrder.PONO = objMarine.PONO;
            _objMarineOrder.RETURN = objMarine.RETURN;
            _objMarineOrder.ORDER = objMarine.ORDER;
            _objMarineOrder.ORDERITEMSIN = objMarine.ORDERITEMSIN;
            _objMarineOrder.ORDERHEADERINX = objMarine.ORDERHEADERINX;
            _objMarineOrder.ORDERITEMSINX = objMarine.ORDERITEMSINX;
            _objMarineOrder.OrderScheduleLines = objMarine.OrderScheduleLines;
            _objMarineOrder.OrderScheduleLineSX = objMarine.OrderScheduleLineSX;
            

            return _objMarineOrder;
        }

        ///// <summary>
        ///// Step 2: This Method is used to Call Biztalk WCF service by sending information as required in the form of SAPMarineOrderVO object
        ///// </summary>
        ///// <param name="objMarine"></param>
        private string Posting_Marine_Order_Biztalk(SAPMarineOrderVO objMarine)
        {
            string res = string.Empty;
            //try
            //{

                ChannelFactory<MarineOrderServiceReference.BiztalkMarineService> cf = new ChannelFactory<MarineOrderServiceReference.BiztalkMarineService>("WSHttpBinding_ITwoWayAsync");
                MarineOrderServiceReference.BiztalkMarineOperationRequest req = new MarineOrderServiceReference.BiztalkMarineOperationRequest(new MarineOrderServiceReference.MARINEORDER());

                log.Info("Request 4 / 5 : Marine Order Create / Update- BIZTALK/SAP connection established successfully for ");
                MarineOrderServiceReference.BiztalkMarineService ch = cf.CreateChannel();
                req.MARINEORDER.ZZTIMEFROM = objMarine.ZZTIMEFROM;
                req.MARINEORDER.ZZTIMETO = objMarine.ZZTIMETO;
                req.MARINEORDER.ZZDATEFROM = objMarine.ZZDATEFROM;
                req.MARINEORDER.ZZDATETO = objMarine.ZZDATETO;
                req.MARINEORDER.ORDERTYPE = objMarine.ORDERTYPE;
                req.MARINEORDER.SALESORGANIZATION = objMarine.SALESORGANIZATION;
                req.MARINEORDER.DISTRIBUTIONCHANNEL = objMarine.DISTRIBUTIONCHANNEL;
                req.MARINEORDER.DIVISION = objMarine.DIVISION;
                req.MARINEORDER.SOLDTOPARTY = objMarine.SOLDTOPARTY;
                req.MARINEORDER.SHIPTOPARTY = objMarine.SHIPTOPARTY;
                req.MARINEORDER.ITEMNO = objMarine.ITEMNO;
                req.MARINEORDER.PONO = objMarine.PONO;
                req.MARINEORDER.ZZDOCKTIME = objMarine.ZZDOCKTIME;
                req.MARINEORDER.SALESDOCUMENT = objMarine.SALESDOCUMENT;
                req.MARINEORDER.MESSAGETYPE = objMarine.MESSAGETYPE;
                req.MARINEORDER.SAPPOSTINGID = objMarine.SAPPOSTINGID;
                req.MARINEORDER.ORDER = objMarine.ORDER;

                List<MarineOrderServiceReference.MARINEORDERItem> milist1 = new List<MarineOrderServiceReference.MARINEORDERItem>();
                if (objMarine.ORDERHEADERINX != null)
                {
                    foreach (var lst2 in objMarine.ORDERHEADERINX)
                    {
                        MarineOrderServiceReference.MARINEORDERItem mi1 = new MarineOrderServiceReference.MARINEORDERItem();
                        mi1.UPDATEFLAG = lst2.UPDATEFLAG;
                        milist1.Add(mi1);

                    }
                }
                req.MARINEORDER.ORDERHEADERINX = milist1.ToArray();

                List<MarineOrderServiceReference.MARINEORDERItem1> milist2 = new List<MarineOrderServiceReference.MARINEORDERItem1>();
                if (objMarine.ORDERITEMSIN != null)
                {
                    foreach (var lst2 in objMarine.ORDERITEMSIN)
                    {
                        MarineOrderServiceReference.MARINEORDERItem1 mi2 = new MarineOrderServiceReference.MARINEORDERItem1();
                        mi2.ITMNUMBER = lst2.ITMNUMBER;
                        mi2.MATERIAL = lst2.MATERIAL;
                        milist2.Add(mi2);

                    }
                }
                req.MARINEORDER.ORDERITEMIN = milist2.ToArray();




                List<MarineOrderServiceReference.MARINEORDERItem2> milist3 = new List<MarineOrderServiceReference.MARINEORDERItem2>();
                if (objMarine.ORDERITEMSINX != null)
                {
                    foreach (var lst3 in objMarine.ORDERITEMSINX)
                    {
                        MarineOrderServiceReference.MARINEORDERItem2 mi3 = new MarineOrderServiceReference.MARINEORDERItem2();
                        mi3.ITMNUMBER = lst3.ITMNUMBER;
                        mi3.MATERIAL = lst3.MATERIAL;
                        mi3.UPDATEFLAG = lst3.UPDATEFLAG;
                        milist3.Add(mi3);

                    }
                }
                req.MARINEORDER.ORDERITEMSINX = milist3.ToArray();

                List<MarineOrderServiceReference.MARINEORDERItem3> milist6 = new List<MarineOrderServiceReference.MARINEORDERItem3>();
                if (objMarine.RETURN != null)
                {
                    foreach (var lst3 in objMarine.RETURN)
                    {
                        MarineOrderServiceReference.MARINEORDERItem3 mi3 = new MarineOrderServiceReference.MARINEORDERItem3();
                         mi3.TYPE = lst3.TYPE;
                         mi3.ID = lst3.ID;
                         mi3.NUMBER = lst3.NUMBER;
                         mi3.MESSAGE = lst3.MESSAGE;
                         mi3.LOGNO = lst3.LOGNO;
                         mi3.LOGMSGNO = lst3.LOGMSGNO;
                         mi3.MESSAGEV1 = lst3.MESSAGEV1;
                         mi3.MESSAGEV2 = lst3.MESSAGEV2;
                         mi3.MESSAGEV3 = lst3.MESSAGEV3;
                         mi3.MESSAGEV4 = lst3.MESSAGEV4;
                         mi3.PARAMETER = lst3.PARAMETER;
                         mi3.ROW = lst3.ROW;
                         mi3.FIELD = lst3.FIELD;
                         mi3.SYSTEM = lst3.SYSTEM;
                         milist6.Add(mi3);

                    }
                }
                req.MARINEORDER.RETURN = milist6.ToArray();

                List<MarineOrderServiceReference.MARINEORDERItem4> milist4 = new List<MarineOrderServiceReference.MARINEORDERItem4>();
                if (objMarine.OrderScheduleLines != null)
                {
                    foreach (var lst4 in objMarine.OrderScheduleLines)
                    {
                        MarineOrderServiceReference.MARINEORDERItem4 mi4 = new MarineOrderServiceReference.MARINEORDERItem4();
                        mi4.ITMNUMBER = lst4.ITMNUMBER;
                        mi4.REQQTY = lst4.REQQTY;
                        mi4.SCHEDLINE = lst4.SCHEDLINE;
                        milist4.Add(mi4);

                    }
                }
                req.MARINEORDER.SCHEDULELINES = milist4.ToArray();

                List<MarineOrderServiceReference.MARINEORDERItem5> milist5 = new List<MarineOrderServiceReference.MARINEORDERItem5>();
                if (objMarine.OrderScheduleLineSX != null)
                {
                    foreach (var lst5 in objMarine.OrderScheduleLineSX)
                    {
                        MarineOrderServiceReference.MARINEORDERItem5 mi5 = new MarineOrderServiceReference.MARINEORDERItem5();
                        mi5.ITMNUMBER = lst5.ITMNUMBER;
                        //mi5.UPDATEFLAG = lst5.UPDATEFLAG;
                        mi5.REQQTY = lst5.REQQTY;
                        milist5.Add(mi5);

                    }
                }
                req.MARINEORDER.SCHEDULELINESX = milist5.ToArray();


                MarineOrderServiceReference.BiztalkMarineOperationResponse resp = ch.BiztalkMarineOperation(req);
                res = resp.Status.Message;

            //}
            //catch (Exception ex)
            //{
            //    log_error("Posting_Marine_Order_Biztalk method Error : ", ex);
            //}
            return res; //Response acknowledgment from Biztalk Service

        }


        public void Update_Of_Marine_Order_Id100(SAPMarineOrderVO objSapMarine)
        {
            IEntityRepository _entity;
            //try
            //{
                IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext());
                //_entity = new EntityRepository(_unitOfWork);
                _sapintegrationRepository = new SAPIntegrationRepository(_unitOfWork);

                nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = "EMP";
                nextStepCompany.UserTypeId = 0;

                if (objSapMarine == null)
                {
                    List<SAPMarineOrderVO> _PendingMarineOrderslist = _sapintegrationRepository.GetAllMarineUpdateDataforSAP();

                    if (_PendingMarineOrderslist.Count > 0)
                    {
                        log.Info("Total Pending SAP Marine Orders Notification count " + _PendingMarineOrderslist.Count);

                        foreach (SAPMarineOrderVO _PendingMarineNotificationdata in _PendingMarineOrderslist)
                        {
                            #region SAP Integration

                            Stopwatch wfstopwatch = Stopwatch.StartNew();
                            log.Info("Update_Of_Marine_Order_ID100 for RevenuePostinID " + _PendingMarineNotificationdata.REVENUEPOSTINGID);

                            string Result = Posting_Marine_Order_Biztalk(MapDataToMarineOrderObject(_PendingMarineNotificationdata));

                            if (Result.ToUpper() == "CREATED") //This result is only for Acknowledgment, which indicates Successfull data received by Biztalk Service
                            {
                                UpdateSAPNotificationStatus(_PendingMarineNotificationdata.SAPPOSTINGID, SAPPostingStatus.Posted);
                                log.Info("SAP Posting completed for RevenuePostinID : " + _PendingMarineNotificationdata.REVENUEPOSTINGID);
                                //  Publish(_entity.GetEntitiesNotification(EntityCodes.Arrival_Notification).EntityID, _PendingArrivalNotificationdata.SAPArrival.VCN, _PendingArrivalNotificationdata.CreatedBy, nextStepCompany, _PendingArrivalNotificationdata.PortCode, "INTG", _PendingArrivalNotificationdata.SAPArrival);
                            }
                            else
                            {
                                log.Error("Response from BIZTALK/SAP failed for Marine Order :" + objSapMarine.REVENUEPOSTINGID + " " + Result);
                            }


                            wfstopwatch.Stop();
                            log.Info("Time taken for processing of Update_Of_Marine_Order_ID100 : " + _PendingMarineNotificationdata.REVENUEPOSTINGID + " Completed in " + wfstopwatch.Elapsed.ToString());
                            #endregion
                        }
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    log_error("Request 5 : Marine Order Update - Update_Of_Marine_Order_ID100 method ERROR : ", ex);
            //}
        }


        ////////////////Vessel Create SAP Integration /////////////////////////////////

        public void Post_Of_Vessel_Detail_ID100(SAPVesselCreateVO objSAPVessel)
        {
            IEntityRepository _entity;
            try
            {
                IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext());
               // _entity = new EntityRepository(_unitOfWork);
                _sapintegrationRepository = new SAPIntegrationRepository(_unitOfWork);

                nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = "EMP";
                nextStepCompany.UserTypeId = 0;

                if (objSAPVessel == null)
                {
                    //log.Info("Request 1 : Method GetAllVesselDataforSAP Started..."); 
                    List<SAPVesselCreateVO> _PendingVesselDetailslist = _sapintegrationRepository.GetAllVesselDataforSAP();
                    //log.Info("Request 1 : Method GetAllVesselDataforSAP Completed..."); 

                    if (_PendingVesselDetailslist.Count > 0)
                    {
                        log.Info("Total Pending Vessel Notification count " + _PendingVesselDetailslist.Count);

                        foreach (SAPVesselCreateVO _PendingVesselDetaildata in _PendingVesselDetailslist)
                        {
                            #region SAP Integration

                            Stopwatch wfstopwatch = Stopwatch.StartNew();
                            log.Info("Post_Of_Vessel_Details for VesselID " + _PendingVesselDetaildata.VesselID);

                            string Result = Posting_Vessel_Detail_Biztalk(MapDataToVesselDetailObject(_PendingVesselDetaildata));

                            //log.Info("Request 1 : Vessel Arrival Create - Posting_Vessel_Detail_Biztalk(MapDataToVesselDetailObject(_PendingVesselDetaildata) - FAILED");

                            if (Result == "CREATED")
                            {
                                UpdateSAPNotificationStatus(_PendingVesselDetaildata.SAPPOSTINGID, SAPPostingStatus.Posted);
                                log.Info("SAP Posting completed for VesselID : " + _PendingVesselDetaildata.VesselID);
                            }
                            else
                            {
                                log.Error("Response from BIZTALK/SAP failed for Vessel Create :" + objSAPVessel.VesselID + " " + Result);
                            }


                            wfstopwatch.Stop();
                            log.Info("Time taken for processing of Post_Of_Vessel_Detail_ID100 : " + _PendingVesselDetaildata.VesselID + " Completed in " + wfstopwatch.Elapsed.ToString());
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log_error("Request 1 : Vessel Arrival Create - Post_Of_Vessel_Detail_ID100 - ERROR : ", ex);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        private SAPVesselCreateVO MapDataToVesselDetailObject(SAPVesselCreateVO objVessel)
        {
            SAPVesselCreateVO _objVessel = new SAPVesselCreateVO();

            _objVessel.ICALLSIGN = objVessel.ICALLSIGN;
            _objVessel.ICITY = objVessel.ICITY;
            _objVessel.ICOUNTRY = objVessel.ICOUNTRY;
            _objVessel.IDATE = objVessel.IDATE;
            _objVessel.IIMO = objVessel.IIMO;
            _objVessel.ILENGTH = objVessel.ILENGTH;
            _objVessel.IPOSTAL = objVessel.IPOSTAL;
            _objVessel.ITONNAGE = objVessel.ITONNAGE;
            _objVessel.IVESIND = objVessel.IVESIND;
            _objVessel.IVESNAME = objVessel.IVESNAME;
            _objVessel.IVESTYPE = objVessel.IVESTYPE;
            //_objVessel.ENUMBER = objVessel.ENUMBER;//Not Passing in XML data
            //_objVessel.SUBRC = objVessel.SUBRC;//Not Passing in XML data
            _objVessel.MESSAGETYPE = objVessel.MESSAGETYPE;
            _objVessel.VesselID = objVessel.VesselID;
            _objVessel.MESSTAB = objVessel.MESSTAB;
            _objVessel.SAPPOSTINGID = objVessel.SAPPOSTINGID;
            _objVessel.VKORG = objVessel.VKORG;

            return _objVessel;
        }

        ///// <summary>
        ///// Step 2: This Method is used to Call Biztalk WCF service by sending information as required in the form of SAPVesselCreateVO object
        ///// </summary>
        ///// <param name="objVessel"></param>
        private string Posting_Vessel_Detail_Biztalk(SAPVesselCreateVO objVessel)
        {
            string res = string.Empty;
            try
            {

                ChannelFactory<VesselCreateServiceReference.BiztalkVesselCreateService> cf = new ChannelFactory<VesselCreateServiceReference.BiztalkVesselCreateService>("WSHttpBinding_ITwoWayAsync");
                VesselCreateServiceReference.BiztalkVesselCreateOperationRequest req = new VesselCreateServiceReference.BiztalkVesselCreateOperationRequest(new VesselCreateServiceReference.VesselCreate());


                log.Info("BIZTALK/SAP connection established successfully for Posting_Vessel_Detail_Biztalk");
                VesselCreateServiceReference.BiztalkVesselCreateService ch = cf.CreateChannel();

                req.VesselCreate.ICALLSIGN = objVessel.ICALLSIGN;
                req.VesselCreate.ICITY = objVessel.ICITY;
                req.VesselCreate.ICOUNTRY = objVessel.ICOUNTRY;
                req.VesselCreate.IDATE = objVessel.IDATE;
                req.VesselCreate.IIMO = objVessel.IIMO;
                req.VesselCreate.ILENGTH = objVessel.ILENGTH;
                req.VesselCreate.IPOSTAL = objVessel.IPOSTAL;
                req.VesselCreate.ITONNAGE = objVessel.ITONNAGE;
                req.VesselCreate.IVESIND = objVessel.IVESIND;
                req.VesselCreate.IVESNAME = objVessel.IVESNAME;
                req.VesselCreate.IVESTYPE = objVessel.IVESTYPE;
                //req.VesselCreate.ENUMBER = objVessel.ENUMBER; //Not Passing in XML data
                //req.VesselCreate.SUBRC = objVessel.SUBRC; //Not Passing in XML data
                req.VesselCreate.MESSAGETYPE = objVessel.MESSAGETYPE;
                req.VesselCreate.VESSELID = objVessel.VesselID;
                req.VesselCreate.SAPPOSTINGID = objVessel.SAPPOSTINGID;
                req.VesselCreate.VKORG = objVessel.VKORG;

                //log.Info("Request 1 : Vessel Arrival Create - Posting_Vessel_Detail_Biztalk - Values assigned to Service from object");

                List<VesselCreateServiceReference.VesselCreateItem> milist = new List<VesselCreateServiceReference.VesselCreateItem>();
                if (objVessel.MESSTAB != null)
                {
                    foreach (var lst in objVessel.MESSTAB)
                    {
                        VesselCreateServiceReference.VesselCreateItem mi = new VesselCreateServiceReference.VesselCreateItem();
                        mi.TCODE = lst.TCODE;
                        mi.DYNAME = lst.DYNAME;
                        mi.DYNUMB = lst.DYNUMB;
                        mi.MSGTYP = lst.MSGTYP;
                        mi.MSGSPRA = lst.MSGSPRA;
                        mi.MSGID = lst.MSGID;
                        mi.MSGNR = lst.MSGNR;
                        mi.MSGV2 = lst.MSGV2;
                        mi.MSGV3 = lst.MSGV3;
                        mi.MSGV4 = lst.MSGV4;
                        mi.ENV = lst.ENV;
                        mi.FLDNAME = lst.FLDNAME;
                        mi.MSGV1 = lst.MSGV1;
                        milist.Add(mi);
                    }
                }
                req.VesselCreate.MESSTAB = milist.ToArray();
                //log.Info("Request 1 : Vessel Arrival Create - Posting_Vessel_Detail_Biztalk service triggered...");
                VesselCreateServiceReference.BiztalkVesselCreateOperationResponse resp = ch.BiztalkVesselCreateOperation(req);
                res = resp.Status.Message;

            }
            catch (Exception ex)
            {
                log_error("Posting_Vessel_Details_Biztalk method Error : ", ex);
            }
            log.Info("Request 1 : Vessel Arrival Create - Posting_Vessel_Detail_Biztalk Response Message is received with " + res);
            return res; //Response acknowledgment from Biztalk Service

        }


        ////////////////Invoice Create SAP Integration /////////////////////////////////

        public void Post_Of_Invoice_Detail_ID100(SAPInvoiceVO objSAPInvoice)
        {
            IEntityRepository _entity;
            try
            {
                IUnitOfWork _unitOfWork = new UnitOfWork(new TnpaContext());
                _entity = new EntityRepository(_unitOfWork);
                _sapintegrationRepository = new SAPIntegrationRepository(_unitOfWork);

                nextStepCompany = new CompanyVO();
                nextStepCompany.UserType = "EMP";
                nextStepCompany.UserTypeId = 0;

                log.Info("Use Case ID: User Case Name: Posting of Invoice Deatils/ ID100 Started...");

                if (objSAPInvoice == null)
                {
                    List<SAPInvoiceVO> _PendingInvoiceDetailslist = _sapintegrationRepository.GetAllInvoiceDataforSAP();

                    log.Info("Total Pending SAP Marine Orders Notification count " + _PendingInvoiceDetailslist.Count);

                    foreach (SAPInvoiceVO _PendingInvoiceDetaildata in _PendingInvoiceDetailslist)
                    {
                        #region SAP Integration

                        Stopwatch wfstopwatch = Stopwatch.StartNew();
                        log.Info("Post_Of_Invoice_Details for VBELN " + _PendingInvoiceDetaildata.VBELN);

                        string Result = Posting_Invoice_Detail_Biztalk(MapDataToInvoiceDetailObject(_PendingInvoiceDetaildata));

                        if (Result == "Created")
                        {
                            UpdateSAPNotificationStatus(_PendingInvoiceDetaildata.SAPPOSTINGID, SAPPostingStatus.Posted);
                            log.Info("SAP Posting completed for Invoice : " + _PendingInvoiceDetaildata.VBELN);
                        }
                        else
                        {
                            log.Error("Response from BIZTALK/SAP failed for Invoice :" + objSAPInvoice.VBELN + " " + Result);
                        }


                        wfstopwatch.Stop();
                        log.Info("Time taken for processing of Post_Of_Invoice_Detail_ID100 : " + _PendingInvoiceDetaildata.VBELN + " Completed in " + wfstopwatch.Elapsed.ToString());
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                log_error("Use Case ID:  User Case Name: Posting of Invoice Details/ ID100 ERROR : ", ex);
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        private SAPInvoiceVO MapDataToInvoiceDetailObject(SAPInvoiceVO objInvoice)
        {
            SAPInvoiceVO _objInvoice = new SAPInvoiceVO();

            _objInvoice.VBELN = objInvoice.VBELN;
            _objInvoice.EINVOICE = objInvoice.EINVOICE;
            _objInvoice.MESSAGETYPE = objInvoice.MESSAGETYPE;
            _objInvoice.SAPPOSTINGID = objInvoice.SAPPOSTINGID;

            return _objInvoice;
        }

        ///// <summary>
        ///// Step 2: This Method is used to Call Biztalk WCF service by sending information as required in the form of SAPVesselCreateVO object
        ///// </summary>
        ///// <param name="objVessel"></param>
        private string Posting_Invoice_Detail_Biztalk(SAPInvoiceVO objInvoice)
        {
            string res = string.Empty;
            try
            {

                ChannelFactory<InvoiceServiceReference.BiztalkInvoiceService> cf = new ChannelFactory<InvoiceServiceReference.BiztalkInvoiceService>("WSHttpBinding_ITwoWayAsync");
                InvoiceServiceReference.BiztalkInvoiceOperationRequest req = new InvoiceServiceReference.BiztalkInvoiceOperationRequest(new InvoiceServiceReference.Invoice());


                log.Info("BIZTALK/SAP connection established successfully");
                InvoiceServiceReference.BiztalkInvoiceService ch = cf.CreateChannel();

                req.Invoice.VBELN = objInvoice.VBELN;
                req.Invoice.MESSAGETYPE = objInvoice.MESSAGETYPE;
                req.Invoice.SAPPOSTINGID = objInvoice.SAPPOSTINGID;

                List<InvoiceServiceReference.InvoiceItem> inlist = new List<InvoiceServiceReference.InvoiceItem>();
                if (objInvoice.EINVOICE != null)
                {
                    foreach (var inv in objInvoice.EINVOICE)
                    {
                        InvoiceServiceReference.InvoiceItem mi = new InvoiceServiceReference.InvoiceItem();
                        mi.ORDERNUMBER = inv.ORDERNUMBER;
                        inlist.Add(mi);
                    }
                }
                req.Invoice.EINVOICE = inlist.ToArray();
                InvoiceServiceReference.BiztalkInvoiceOperationResponse resp = ch.BiztalkInvoiceOperation(req);
                res = resp.Status.Message;

            }
            catch (Exception ex)
            {
                log_error("Posting_Invoice_Details_Biztalk method Error : ", ex);
            }
            return res; //Response acknowledgment from Biztalk Service

        }
        private List<SAPPosting> GetSAPPendingNotifications()
        {

            List<SAPPosting> pendingSAPNotifications = null;

            using (IUnitOfWork uow = new UnitOfWork(new TnpaContext()))
            {
                try
                {
                    ISAPIntegrationRepository _sapnotificationRepository = new SAPIntegrationRepository(uow);
                    saplastNotificationId = uow.SqlQuery<int>("Select COALESCE(SAPLastNotificationId,0) from PortConfiguration").FirstOrDefault();
                    pendingSAPNotifications = _sapnotificationRepository.GetSAPPendingNotifications(saplastNotificationId);
                }
                catch (Exception ex)
                {
                    log_error("SAPNotifications.Engine.SAPNotificationEngine --> GetSAPPendingNotifications", ex);
                }
            }
            return pendingSAPNotifications;
        }

    }
}


using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Globalization;


namespace IPMS.Repository
{
    public class SAPIntegrationRepository : ISAPIntegrationRepository
    {
        protected IUnitOfWork _unitOfWork;
        private readonly ILog log;

        public SAPIntegrationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(SAPIntegrationRepository));
        }

        #region SAP Integration Methods
        public ArrivalNotificationVO GetArrivalDataforSAPByVCN(string VCN)
        {
            var arrivalDetails = _unitOfWork.SqlQuery<ArrivalNotificationVO>("select DISTINCT VCN,PortCode,AgentID,VesselID,VoyageIn,VoyageOut,VKORG,dbo.udf_FormatDateTime(ETA,'YYYY-MM-DD') as ETA,dbo.udf_FormatDateTime(ETD,'YYYY-MM-DD') as ETD,SAPAccNo,NextPortOfCall, LastPortOfCall,CreatedBy,CreatedDate from dbo.udf_GetArrivalNotification() where IsANFinal='Y' and IsPHANFinal='Y' and (IsISPSANFinal='Y' OR IsISPSANFinal='NA') AND (IsIMDGANFinal = 'Y' OR IsIMDGANFinal='NA') and SAPARRNO is null and VCN=@p0", VCN).FirstOrDefault<ArrivalNotificationVO>();
            arrivalDetails.SAPArrival = _unitOfWork.SqlQuery<SAPArrivalVO>("select DISTINCT VCN,PortCode,AgentID,VesselID,VoyageIn as VOYIN,VoyageOut as VOYOUT,VKORG,cast(dbo.udf_FormatDateTime(ETA,'YYYY-MM-DD') as date) as EDA,cast(dbo.udf_FormatDateTime(ETD,'YYYY-MM-DD') as date) as EDD,SAPAccNo as KUNNR,NextPortOfCall as PORT_CALL, LastPortOfCall as PORT_ORIGIN,CreatedBy,CreatedDate from dbo.udf_GetArrivalNotification() where IsANFinal='Y' and IsPHANFinal='Y' and (IsISPSANFinal='Y' OR IsISPSANFinal='NA') AND (IsIMDGANFinal = 'Y' OR IsIMDGANFinal='NA') and SAPARRNO is null and VCN=@p0", VCN).FirstOrDefault<SAPArrivalVO>();
            return arrivalDetails;

        }
        public List<SAPArrivalVO> GetAllArrivalDataforSAP()
        {
            List<SAPArrivalVO> _saparrivalvo = new List<SAPArrivalVO>();
            //try
            //{
                List<SAPPosting> arrivalDetails = _unitOfWork.SqlQuery<SAPPosting>("select * from SAPPosting where MessageType = '" + SAPMessageTypes.ArrivalCreate + "' and PostingStatus = '" + SAPPostingStatus.New + "'").ToList<SAPPosting>();
                foreach (var andata in arrivalDetails)
                {
                    string str = andata.TransmitData;
                    TextReader tr = new StringReader(str);
                    XDocument xDoc = XDocument.Load(tr);

                    var data = from item in xDoc.Descendants("ArrivalCreate")
                               select new SAPArrivalVO
                               {
                                   AED = (string)item.Element("AED"),
                                   AET = (string)item.Element("AET"),
                                   DED = (string)item.Element("DED"),
                                   DET = (string)item.Element("DET"),
                                   ARRNO = (string)item.Element("ARRNO"),
                                   CODE = (string)item.Element("CODE"),
                                   EDA = Convert.ToDateTime(item.Element("EDA").Value, CultureInfo.InvariantCulture),
                                   KUNNR = (string)item.Element("KUNNR"),
                                   PORTCALL = (string)item.Element("PORTCALL"),
                                   PORTORIGIN = (string)item.Element("PORTORIGIN"),
                                   VKORG = (string)item.Element("VKORG"),
                                   VOYIN = (string)item.Element("VOYIN"),
                                   VOYOUT = (string)item.Element("VOYOUT"),
                                   ZZBERTH = (string)item.Element("ZZBERTH"),
                                   VCN = andata.ReferenceNo,
                                   MESSAGETYPE = andata.MessageType,
                                   SAPPostingID = andata.SAPPostingID
                               };

                    _saparrivalvo.Add(data.FirstOrDefault());

                }

            //}
            //catch (Exception ex)
            //{
            //    log_error("Request 2 : Vessel Arrival Create - GetAllArrivalDataforSAP - ERROR : ", ex);
            //}
            return _saparrivalvo;

        }

        public List<SAPArrivalVO> GetAllUpdateArrivalDataforSAP()
        {
            log.Info("GetAllUpdateArrivalDataforSAP Method started..");
            
            List<SAPPosting> arrivalDetails = _unitOfWork.SqlQuery<SAPPosting>("select * from SAPPosting where MessageType = '" + SAPMessageTypes.ArrivalUpdate + "' and PostingStatus = '" + SAPPostingStatus.New + "'").ToList<SAPPosting>();

            List<SAPArrivalVO> _saparrivalvo = new List<SAPArrivalVO>();

            foreach (var andata in arrivalDetails)
            {
                string str = andata.TransmitData;
                TextReader tr = new StringReader(str);
                XDocument xDoc = XDocument.Load(tr);

                var data = from item in xDoc.Descendants("ArrivalUpdate")
                           select new SAPArrivalVO
                           {
                               AED = (string)item.Element("AED"),
                               AET = (string)item.Element("AET"),
                               DED = (string)item.Element("DED"),
                               DET = (string)item.Element("DET"),
                               ARRNO = (string)item.Element("ARRNO"),
                               CODE = (string)item.Element("CODE"),
                               EDA = Convert.ToDateTime(item.Element("EDA").Value, CultureInfo.InvariantCulture),
                               EDD = Convert.ToDateTime(item.Element("EDD").Value, CultureInfo.InvariantCulture),
                               KUNNR = (string)item.Element("KUNNR"),
                               PORTCALL = (string)item.Element("PORTCALL"),
                               PORTORIGIN = (string)item.Element("PORTORIGIN"),
                               VKORG = (string)item.Element("VKORG"),
                               VOYIN = (string)item.Element("VOYIN"),
                               VOYOUT = (string)item.Element("VOYOUT"),
                               ZZBERTH = (string)item.Element("ZZBERTH"),
                               VCN = andata.ReferenceNo,
                               MESSAGETYPE = andata.MessageType,
                               SAPPostingID = andata.SAPPostingID
                           };
                _saparrivalvo.Add(data.FirstOrDefault());

            }
            log.Info("GetAllUpdateArrivalDataforSAP Method completed..");

            return _saparrivalvo;
        }

        /////marine order //////////

        public List<SAPMarineOrderVO> GetAllMarineDataforSAP()
        {
            List<SAPPosting> marineDetails = _unitOfWork.SqlQuery<SAPPosting>("select * from SAPPosting where MessageType = '" + SAPMessageTypes.MarineCreate + "' and PostingStatus = '" + SAPPostingStatus.New + "'").ToList<SAPPosting>();

            List<SAPMarineOrderVO> _sapmarinevo = new List<SAPMarineOrderVO>();

            foreach (var mdata in marineDetails)
            {
                string str = mdata.TransmitData;
                TextReader tr = new StringReader(str);
                XDocument xDoc = XDocument.Load(tr);

                var data = from item in xDoc.Descendants("MarineOrderCreate")
                           select new SAPMarineOrderVO
                           {
                              
                               ZZTIMEFROM = (string)item.Element("ZZTIMEFROM"), 
                               ZZTIMETO = (string)item.Element("ZZTIMETO"),
                               ZZDATEFROM = (string)item.Element("ZZDATEFROM"),
                               ZZDATETO = (string)item.Element("ZZDATETO"),
                               ORDERTYPE = (string)item.Element("ORDERTYPE"),
                               SALESORGANIZATION = (string)item.Element("SALESORGANIZATION"),
                               DISTRIBUTIONCHANNEL = (string)item.Element("DISTRIBUTIONCHANNEL"),
                               DIVISION = (string)item.Element("DIVISION"),
                               ITEMNO = (string)item.Element("ITEMNO"),
                               SOLDTOPARTY = (string)item.Element("SOLDTOPARTY"),
                               SHIPTOPARTY = (string)item.Element("SHIPTOPARTY"),
                               ZZDOCKTIME = (string)item.Element("ZZDOCKTIME"),
                               SALESDOCUMENT = (string)item.Element("SALESDOCUMENT"),
                               PONO = Convert.ToInt32(item.Element("PONO").Value, CultureInfo.InvariantCulture),
                               MESSAGETYPE = mdata.MessageType,
                               SAPPOSTINGID = mdata.SAPPostingID,
                               ORDERITEMSIN = (from ord in xDoc.Descendants("MarineOrderCreate").Elements("ORDERITEMIN").Elements("Item")
                                               select new SAPMarineOrderItemsInVO
                                               {
                                                   ITMNUMBER = (string)ord.Element("ITMNUMBER"),
                                                   MATERIAL = (string)ord.Element("MATERIAL")
                                               }).ToList(),
                               OrderScheduleLines = (from ord in xDoc.Descendants("MarineOrderCreate").Elements("SCHEDULELINES").Elements("Item")
                                                     select new SAPMarineOrderScheduleLinesVO
                                                     {
                                                         ITMNUMBER = (string)ord.Element("ITMNUMBER"),
                                                         REQQTY = (string)ord.Element("REQQTY")
                                                     }).ToList(),
                               OrderScheduleLineSX = (from ord in xDoc.Descendants("MarineOrderCreate").Elements("SCHEDULELINESX").Elements("Item")//YES
                                                      select new SAPMarineOrderScheduleLineSXVO
                                                      {
                                                          ITMNUMBER = (string)ord.Element("ITMNUMBER"),//YES
                                                          REQQTY = (string)ord.Element("REQQTY")//YES
                                                      }).ToList(),
                               RETURN = (from ord in xDoc.Descendants("MarineOrderCreate").Elements("RETURN").Elements("Item")//YES
                                         select new SAPMarineOrderReturnVO
                                         {

                                             TYPE = (string)ord.Element("TYPE"),
                                             ID = (string)ord.Element("ID"),
                                             NUMBER = (string)ord.Element("NUMBER"),
                                             MESSAGE = (string)ord.Element("MESSAGE"),
                                             LOGNO = (string)ord.Element("LOGNO"),
                                             LOGMSGNO = (string)ord.Element("LOGMSGNO"),
                                             MESSAGEV1 = (string)ord.Element("MESSAGEV1"),
                                             MESSAGEV2 = (string)ord.Element("MESSAGEV2"),
                                             MESSAGEV3 = (string)ord.Element("MESSAGEV3"),
                                             MESSAGEV4 = (string)ord.Element("MESSAGEV4"),
                                             PARAMETER = (string)ord.Element("PARAMETER"),
                                             ROW = (string)ord.Element("ROW"),
                                             FIELD = (string)ord.Element("FIELD"),
                                             SYSTEM = (string)ord.Element("SYSTEM")

                                         }).ToList()

                           };

                _sapmarinevo.Add(data.FirstOrDefault());

            }

            return _sapmarinevo;
        }

        public List<SAPMarineOrderVO> GetAllMarineUpdateDataforSAP()
        {
            List<SAPPosting> marineDetails = _unitOfWork.SqlQuery<SAPPosting>("select * from SAPPosting where MessageType = '" + SAPMessageTypes.MarineUpdate + "' and PostingStatus = '" + SAPPostingStatus.New + "'").ToList<SAPPosting>();

            List<SAPMarineOrderVO> _sapmarinevo = new List<SAPMarineOrderVO>();

            foreach (var mdata in marineDetails)
            {
                string str = mdata.TransmitData;
                TextReader tr = new StringReader(str);
                XDocument xDoc = XDocument.Load(tr);

                var data = from item in xDoc.Descendants("MarineOrderUpdate")
                           select new SAPMarineOrderVO
                           {


                               ZZTIMEFROM = (string)item.Element("ZZTIMEFROM"),  //Convert.ToDateTime(item.Element("ZZTIMEFROM")),
                               ZZTIMETO = (string)item.Element("ZZTIMETO"),  // Convert.ToDateTime(item.Element("ZZTIMETO")),
                               ZZDATEFROM = (string)item.Element("ZZDATEFROM"),
                               ZZDATETO = (string)item.Element("ZZDATETO"),
                               ORDER = (string)item.Element("ORDER"),
                               SALESDOCUMENT = (string)item.Element("ORDER"),
                              // REVENUEPOSTINGID = Convert.ToInt32(mdata.ReferenceNo),
                               MESSAGETYPE = mdata.MessageType,
                               SAPPOSTINGID = mdata.SAPPostingID,
                               ORDERITEMSIN = (from ord in xDoc.Descendants("MarineOrderUpdate").Elements("ORDERITEMSIN").Elements("Item")
                                               select new SAPMarineOrderItemsInVO
                                               {
                                                   ITMNUMBER = (string)ord.Element("ITMNUMBER"),
                                                   MATERIAL = (string)ord.Element("MATERIAL")
                                               }).ToList(),
                               ORDERHEADERINX = (from ord in xDoc.Descendants("MarineOrderUpdate").Elements("ORDERHEADERINX").Elements("Item")
                                               select new SAPMarineOrderOrderHeaderInXVO
                                               {
                                                   UPDATEFLAG = (string)ord.Element("UPDATEFLAG"),                                                   
                                               }).ToList(),
                               ORDERITEMSINX = (from ord in xDoc.Descendants("MarineOrderUpdate").Elements("ORDERITEMSINX").Elements("Item")
                                               select new SAPMarineOrderOrderItemsInXVO
                                               {
                                                   ITMNUMBER = (string)ord.Element("ITMNUMBER"),
                                                   MATERIAL = (string)ord.Element("MATERIAL"),
                                                   UPDATEFLAG = (string)ord.Element("UPDATEFLAG")
                                               }).ToList(),
                               OrderScheduleLines = (from ord in xDoc.Descendants("MarineOrderUpdate").Elements("SCHEDULELINES").Elements("Item")
                                               select new SAPMarineOrderScheduleLinesVO
                                               {
                                                   ITMNUMBER = (string)ord.Element("ITMNUMBER"),
                                                   REQQTY = (string)ord.Element("REQQTY")
                                               }).ToList(),
                               OrderScheduleLineSX = (from ord in xDoc.Descendants("MarineOrderUpdate").Elements("SCHEDULELINESX").Elements("Item")
                                               select new SAPMarineOrderScheduleLineSXVO
                                               {
                                                   ITMNUMBER = (string)ord.Element("ITMNUMBER"),
                                                   REQQTY = (string)ord.Element("REQQTY")
                                                   //UPDATEFLAG = (string)ord.Element("UPDATEFLAG")
                                               }).ToList(),
  

                           };

                _sapmarinevo.Add(data.FirstOrDefault());

            }

            return _sapmarinevo;
        }

        ///// Vessel Create //////////

        public List<SAPVesselCreateVO> GetAllVesselDataforSAP()
        {
            List<SAPVesselCreateVO> _sapvesselvo = new List<SAPVesselCreateVO>();
            //log.Info("Request 1 : Vessel Arrival Create - Post_Of_Vessel_Detail_ID100 - GetAllVesselDataforSAP - Started");
            //try
            //{
            List<SAPPosting> vesselDetails = _unitOfWork.SqlQuery<SAPPosting>("select * from SAPPosting where MessageType = '" + SAPMessageTypes.VesselCreate + "' and PostingStatus = '" + SAPPostingStatus.New + "'").ToList<SAPPosting>();
                //log.Info("Request 1 : Vessel Arrival Create - Post_Of_Vessel_Detail_ID100 - GetAllVesselDataforSAP - Vessels data Fetching process completed...");
                foreach (var vdata in vesselDetails)
                {
                    string str = vdata.TransmitData;
                    TextReader tr = new StringReader(str);
                    XDocument xDoc = XDocument.Load(tr);

                    var data = from item in xDoc.Descendants("VESSELDETAILS")
                               select new SAPVesselCreateVO
                               {
                                   ICALLSIGN = (string)item.Element("ICALLSIGN"),
                                   ICITY = (string)item.Element("ICITY"),
                                   ICOUNTRY = (string)item.Element("ICOUNTRY"),
                                   IDATE = Convert.ToDateTime(item.Element("IDATE").Value, CultureInfo.InvariantCulture),
                                   IIMO = (string)item.Element("IIMO"),
                                   ILENGTH = (string)item.Element("ILENGTH"),
                                   IPOSTAL = (string)item.Element("IPOSTAL"),
                                   ITONNAGE = (string)item.Element("ITONNAGE"),
                                   IVESIND = (string)item.Element("IVESIND"),
                                   IVESNAME = (string)item.Element("IVESNAME"),
                                   IVESTYPE = (string)item.Element("IVESTYPE"),
                                   //ENUMBER = (string)item.Element("ENUMBER"), //Not Passing in XML data
                                   //SUBRC = Convert.ToInt32(item.Element("SUBRC").Value), //Not Passing in XML data
                                   MESSAGETYPE = vdata.MessageType,
                                   SAPPOSTINGID = vdata.SAPPostingID,
                                   VesselID = Convert.ToInt32(vdata.ReferenceNo, CultureInfo.InvariantCulture),
                                   VKORG = (string)item.Element("VKORG"),
                                   MESSTAB = (from ord in xDoc.Descendants("VESSELDETAILS").Elements("MESSTAB").Elements("Item")
                                              select new SAPVesselCreateMESSTABVO
                                              {

                                                  TCODE = (string)ord.Element("TCODE"),
                                                  DYNAME = (string)ord.Element("DYNAME"),
                                                  DYNUMB = (string)ord.Element("DYNUMB"),
                                                  MSGTYP = (string)ord.Element("MSGTYP"),
                                                  MSGSPRA = (string)ord.Element("MSGSPRA"),
                                                  MSGID = (string)ord.Element("MSGID"),
                                                  MSGNR = (string)ord.Element("MSGNR"),
                                                  MSGV2 = (string)ord.Element("MSGV2"),
                                                  MSGV3 = (string)ord.Element("MSGV3"),
                                                  MSGV4 = (string)ord.Element("MSGV4"),
                                                  ENV = (string)ord.Element("ENV"),
                                                  FLDNAME = (string)ord.Element("FLDNAME"),
                                                  MSGV1 = (string)ord.Element("MSGV1")

                                              }).ToList()
                               };

                    _sapvesselvo.Add(data.FirstOrDefault());

                }
                //log.Info("Request 1 : Vessel Arrival Create - Post_Of_Vessel_Detail_ID100 - GetAllVesselDataforSAP - Completed");
            //}
            //catch (Exception ex)
            //{
            //    log_error("Request 1 : Vessel Arrival Create - GetAllVesselDataforSAP - ERROR : ", ex);
            //}

            return _sapvesselvo;
        }


        ///// Invoice Create //////////

        public List<SAPInvoiceVO> GetAllInvoiceDataforSAP()
        {
            List<SAPPosting> invoiceDetails = _unitOfWork.SqlQuery<SAPPosting>("select * from SAPPosting where MessageType = '" + SAPMessageTypes.InvoiceCreate + "' and PostingStatus = '" + SAPPostingStatus.New + "'").ToList<SAPPosting>();

            List<SAPInvoiceVO> _sapinvoicevo = new List<SAPInvoiceVO>();

            foreach (var idata in invoiceDetails)
            {
                string str = idata.TransmitData;
                TextReader tr = new StringReader(str);
                XDocument xDoc = XDocument.Load(tr);

                var data = from item in xDoc.Descendants("INVOICE")
                           select new SAPInvoiceVO
                           {
                               VBELN = (string)item.Element("VBELN"),
                               MESSAGETYPE = idata.MessageType,
                               SAPPOSTINGID = idata.SAPPostingID,
                               EINVOICE = (from ord in xDoc.Descendants("INVOICE").Elements("EINVOICE").Elements("Item")
                                           select new SAPInvoiceItemVO
                                           {
                                               ORDERNUMBER = (string)ord.Element("ORDERNUMBER")
                                           }).ToList()
                           };

                _sapinvoicevo.Add(data.FirstOrDefault());

            }

            return _sapinvoicevo;
        }


        public List<SAPPosting> GetSAPPendingNotifications(int laterThanId)
        {
            Stopwatch wfstopwatch;
            wfstopwatch = Stopwatch.StartNew();
            //log.Info("Query Fetching Started");
            var pendingNotifications = (from n in _unitOfWork.Repository<SAPPosting>().Query().Select()
                                        where n.PostingStatus == SAPPostingStatus.New
                                        select n).OrderBy(info => info.SAPPostingID).ToList();
            wfstopwatch.Stop();
            //log.Info("Query Fetching Completed in  " + wfstopwatch.Elapsed.ToString());
            return pendingNotifications;
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

        #endregion

    }
}

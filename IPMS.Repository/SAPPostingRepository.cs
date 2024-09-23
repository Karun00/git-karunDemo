using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using IPMS.Domain.DTOS;
using System.Globalization;

namespace IPMS.Repository
{
    public class SAPPostingRepository : ISAPPostingRepository
    {
        private IUnitOfWork _unitOfWork;       

        public SAPPostingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();           
        }

        /// <summary>
        /// To Get SAP Posting VCN
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>

        public List<SAPPostingVO> GetSAPPostingVCN(string searchValue, string portCode)
        {
            var portcode = new SqlParameter("@p_PortCode", portCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);

            var vcns = _unitOfWork.SqlQuery<SAPPostingVO>("dbo.usp_GetSAPPostingVCN @p_SearchText, @p_PortCode", Searchvalue, portcode).ToList();


            return vcns;
        }


        /// <summary>
        /// To Get SAP Posting Details by VCN
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<SAPPostingVO> GetSAPPostingDetailsByVCN(string VCN)
        {
            var vcn = new SqlParameter("@A_vcn", VCN);
            var vcns = _unitOfWork.SqlQuery<SAPPostingVO>("dbo.usp_GetSapPostingItemGrid @A_vcn", vcn).ToList();
            return vcns;
        }


        /// <summary>
        /// To Get SAP Posting Add Details by VCN
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>        
        public string GetSAPPostingDetailsAddDetails(string VCN, string portCode, string MsgType, string ReceavedARRNO, string MarineAccNo, int MarinePostingId, string PostingStatus, string isRevenueUpd,string RevAgentAccNo)
        {
            var portcode = new SqlParameter("@p_PortCode", portCode);
            var vcn = new SqlParameter("@A_vcn", VCN);
            var msgType = new SqlParameter("@MSGTYPE", MsgType);
            var RevAccNo = new SqlParameter("@RevenueAgentAccNo", RevAgentAccNo);

            var SAPArvData = _unitOfWork.SqlQuery<SAPArrivalVO>("dbo.usp_GetToPostSapData @A_vcn, @MSGTYPE,@RevenueAgentAccNo", vcn, msgType, RevAccNo).ToList();
            string AddArv = "";
            if (MsgType == "CRAR")
            {


                AddArv = PostToSAP.AddArrival;
                AddArv = AddArv

                            .Replace("#AED#", SAPArvData[0].AED != null ? SAPArvData[0].AED.ToString() : "")
                            .Replace("#AET#", SAPArvData[0].AET != null ? SAPArvData[0].AET.ToString() : "")
                            .Replace("#DED#", SAPArvData[0].DED != null ? SAPArvData[0].DED.ToString() : "")
                            .Replace("#DET#", SAPArvData[0].DET != null ? SAPArvData[0].DET.ToString() : "")
                            .Replace("#EDA#", SAPArvData[0].STREDA != null ? SAPArvData[0].STREDA.ToString() : "")
                            .Replace("#KUNNR#", SAPArvData[0].KUNNR != null ? SAPArvData[0].KUNNR.ToString() : "")
                            .Replace("#PORTCALL#", SAPArvData[0].PORTCALL != null ? SAPArvData[0].PORTCALL.ToString() : "")
                            .Replace("#VKORG#", SAPArvData[0].VKORG != null ? SAPArvData[0].VKORG.ToString() : "")
                            .Replace("#VOYIN#", SAPArvData[0].VOYIN != null ? SAPArvData[0].VOYIN.ToString() : "")
                            .Replace("#PORTORIGIN#", SAPArvData[0].PORTORIGIN != null ? SAPArvData[0].PORTORIGIN.ToString() : "")
                            .Replace("#VOYOUT#", SAPArvData[0].VOYOUT != null ? SAPArvData[0].VOYOUT.ToString() : "")
                            .Replace("#ZZBERTH#", SAPArvData[0].ZZBERTH != null ? SAPArvData[0].ZZBERTH.ToString() : "");                            
            }
            else if (MsgType == "UPAR")
            {

                AddArv = PostToSAP.UpdateArrival;
                AddArv = AddArv.Replace("#ARRNO#", ReceavedARRNO)
                            .Replace("#AED#", SAPArvData[0].AED != null ? SAPArvData[0].AED.ToString() : "")
                            .Replace("#AET#", SAPArvData[0].AET != null ? SAPArvData[0].AET.ToString() : "")
                            .Replace("#DED#", SAPArvData[0].DED != null ? SAPArvData[0].DED.ToString() : "")
                            .Replace("#DET#", SAPArvData[0].DET != null ? SAPArvData[0].DET.ToString() : "")
                            .Replace("#EDA#", SAPArvData[0].STREDA != null ? SAPArvData[0].STREDA.ToString() : "")
                            .Replace("#EDD#", SAPArvData[0].STREDD != null ? SAPArvData[0].STREDD.ToString() : "")
                            .Replace("#KUNNR#", SAPArvData[0].KUNNR != null ? SAPArvData[0].KUNNR.ToString() : "")
                            .Replace("#PORTCALL#", SAPArvData[0].PORTCALL != null ? SAPArvData[0].PORTCALL.ToString() : "")
                            .Replace("#VKORG#", SAPArvData[0].VKORG != null ? SAPArvData[0].VKORG.ToString() : "")
                            .Replace("#VOYIN#", SAPArvData[0].VOYIN != null ? SAPArvData[0].VOYIN.ToString() : "")
                            .Replace("#PORTORIGIN#", SAPArvData[0].PORTORIGIN != null ? SAPArvData[0].PORTORIGIN.ToString() : "")
                            .Replace("#VOYOUT#", SAPArvData[0].VOYOUT != null ? SAPArvData[0].VOYOUT.ToString() : "")
                            .Replace("#ZZBERTH#", SAPArvData[0].ZZBERTH != null ? SAPArvData[0].ZZBERTH.ToString() : "");
            }
            else if ((MsgType == "CRMO" || MsgType == "UPMO") && PostingStatus == "SAPINV")
            {                

                var SAPdata = _unitOfWork.SqlQuery<SAPPostingVO>("SELECT SAPReferenceNo FROM SAPPOSTING WHERE MarinePostingId = @p0 and RevenueAgentAccNo =@p1 ", MarinePostingId,RevAgentAccNo).FirstOrDefault();

                AddArv = PostToSAP.SapInvoice;
                AddArv = AddArv.Replace("#ORDERNUMBER#", SAPdata.SAPReferenceNo != null ? SAPdata.SAPReferenceNo : "")
                    .Replace("#VBELN#", SAPdata.SAPReferenceNo != null ? SAPdata.SAPReferenceNo : "");
            }
            else if (MsgType == "CRMO")
            {
                int RevenuehdrID = 0;
                if (isRevenueUpd == "Y")
                {
                    AddArv = PostToSAP.MarineCreateHdr;
                }
                else
                {
                    AddArv = PostToSAP.MarineCreateHdr;
                }

                AddArv = AddArv.Replace("#SALESORGANIZATION#", SAPArvData[0].SALESORGANIZATION != null ? SAPArvData[0].SALESORGANIZATION.ToString() : "")
                            .Replace("#SOLDTOPARTY#", SAPArvData[0].SOLDTOPARTY != null ? SAPArvData[0].SOLDTOPARTY.ToString() : "")
                            .Replace("#SHIPTOPARTY#", SAPArvData[0].SHIPTOPARTY != null ? SAPArvData[0].SHIPTOPARTY.ToString() : "")
                            .Replace("#PONO#", ReceavedARRNO);
               
                var SMarineAcNo = new SqlParameter("@AccountNo", MarineAccNo);
                var svcn = new SqlParameter("@A_vcn", VCN);
                var SP_marinePostingId = new SqlParameter("@SP_MarinePostingId", MarinePostingId);
                var RevAgentAccountNo = new SqlParameter("@RevenueAgentAccNo", RevAgentAccNo);

                var SAPDataList = _unitOfWork.SqlQuery<SAPMarineDetailsVO>("dbo.usp_GetSAPMarinPostDtls @A_vcn, @AccountNo,@SP_MarinePostingId,@RevenueAgentAccNo", svcn, SMarineAcNo, SP_marinePostingId, RevAgentAccountNo).ToList();

                bool Isportdues = true;

                string AllMrineitemDetails = "";
                string AllSCHEDULELINESdtls = "";
                foreach (SAPMarineDetailsVO i in SAPDataList)
                {
                    RevenuehdrID = i.mrrevid;
                    string AddMrineitemDetail = PostToSAP.MarineCreatedtl;
                    AllMrineitemDetails = AllMrineitemDetails + "" + AddMrineitemDetail
                                           .Replace("#MATERIAL#", i.MaterialCode.ToString())
                                          .Replace("#ITMNUMBER#", i.ITMNUMBER.ToString());


                    string addSCHEDULELINESdtls = PostToSAP.SCHEDULELINESdtl;
                    if (i.MaterialCode.ToString() != "000000000000000132")
                    {
                        var units = Convert.ToString(i.Units, CultureInfo.InvariantCulture);
                        AllSCHEDULELINESdtls = AllSCHEDULELINESdtls + "" + addSCHEDULELINESdtls
                                                  .Replace("#ITMNUMBER#", i.ITMNUMBER.ToString())
                                                  .Replace("#REQQTY#", units);
                    }



                    if (i.MaterialCode.ToString() == "000000000000000132")
                    {
                        Isportdues = false;
                        AddArv = AddArv.Replace("#ZZTIMETO#", i.ZZTIMETO != null ? i.ZZTIMETO.ToString() : "")
                                       .Replace("#ZZTIMEFROM#", i.ZZTIMEFROM != null ? i.ZZTIMEFROM.ToString() : "")
                                       .Replace("#ZZDATETO#", i.ZZDATETO != null ? i.ZZDATETO.ToString() : "")
                                       .Replace("#ZZDATEFROM#", i.ZZDATEFROM != null ? i.ZZDATEFROM.ToString() : "");
                    }
                }

                if (Isportdues)
                {
                    AddArv = AddArv.Replace("#ZZTIMETO#", "00:00:00")
                                   .Replace("#ZZTIMEFROM#", "00:00:00")
                                   .Replace("#ZZDATETO#", "0000-00-00")
                                   .Replace("#ZZDATEFROM#", "0000-00-00");
                }

                AddArv = AddArv.Replace("#@@@@@@@@@@@#", AllMrineitemDetails);
                AddArv = AddArv.Replace("###@@@@@###", AllSCHEDULELINESdtls);

                AddArv = AddArv + "" + PostToSAP.MarineCreateHdrClose;
                AddArv = AddArv + "$" + Convert.ToString(RevenuehdrID, CultureInfo.InvariantCulture);
            }
            else if (MsgType == "UPMO")
            {

                var SAPdata = _unitOfWork.SqlQuery<SAPPostingVO>("SELECT SAPReferenceNo FROM SAPPosting where MessageType  = 'CRMO' and ReferenceNo = @p0 and RevenueAgentAccNo = @p1", VCN, RevAgentAccNo).FirstOrDefault();

                AddArv = PostToSAP.MarineUpdateHdrXML;
                AddArv = AddArv.Replace("#ORDER#", SAPdata.SAPReferenceNo != null ? SAPdata.SAPReferenceNo : "");
                int RevenuehdrID = 0;

                var SMarineAcNo = new SqlParameter("@AccountNo", MarineAccNo);
                var svcn = new SqlParameter("@A_vcn", VCN);
                var SP_marinePostingId = new SqlParameter("@SP_MarinePostingId", MarinePostingId);
                var RevAgentAccountNo1 = new SqlParameter("@RevenueAgentAccNo", RevAgentAccNo);
                var SAPDataList = _unitOfWork.SqlQuery<SAPMarineDetailsVO>("dbo.usp_GetSAPMarinPostDtls @A_vcn, @AccountNo,@SP_MarinePostingId,@RevenueAgentAccNo", svcn, SMarineAcNo, SP_marinePostingId, RevAgentAccountNo1).ToList();

                bool Isportdues = true;

                string ALLORDERITEMSINSTR = "";
                string ALLORDERITEMSINXSTR = "";

                string AllSCHEDULELINESSTR = "";
                string AllSCHEDULELINESXSTR = "";
                
                foreach (SAPMarineDetailsVO i in SAPDataList)
                {
                    RevenuehdrID = i.mrrevid;
                    string AddORDERITEMSINSTR = PostToSAP.ORDERITEMSINSTR; 
                    string AddORDERITEMSINXSTR = PostToSAP.ORDERITEMSINXSTR; 
                    string AddSCHEDULELINESSTR = PostToSAP.SCHEDULELINESSTR; 
                    string AddSCHEDULELINESXSTR = PostToSAP.SCHEDULELINESXSTR; 
                    

                    

                 var units = Convert.ToString(i.Units, CultureInfo.InvariantCulture);
                        ALLORDERITEMSINXSTR = ALLORDERITEMSINXSTR + "" + AddORDERITEMSINXSTR
                                                .Replace("#ITMNUMBER#", i.ITMNUMBER.ToString());

                        ALLORDERITEMSINSTR = ALLORDERITEMSINSTR + "" + AddORDERITEMSINSTR
                                              .Replace("#MATERIAL#", i.MaterialCode.ToString())
                                             .Replace("#ITMNUMBER#", i.ITMNUMBER.ToString());
                          AllSCHEDULELINESSTR = AllSCHEDULELINESSTR+AddSCHEDULELINESSTR
                                                .Replace("#ITMNUMBER#", i.ITMNUMBER.ToString())
                                                 .Replace("#REQQTY#", units);
                   
                    AllSCHEDULELINESXSTR = AllSCHEDULELINESXSTR + AddSCHEDULELINESXSTR
                                                .Replace("#ITMNUMBER#", i.ITMNUMBER.ToString());

                    if (i.MaterialCode.ToString() == "000000000000000132")
                    {
                        Isportdues = false;
                        AddArv = AddArv.Replace("#ZZTIMETO#", i.ZZTIMETO != null ? i.ZZTIMETO.ToString() : "")
                                       .Replace("#ZZTIMEFROM#", i.ZZTIMEFROM != null ? i.ZZTIMEFROM.ToString() : "")
                                       .Replace("#ZZDATETO#", i.ZZDATETO != null ? i.ZZDATETO.ToString() : "")
                                       .Replace("#ZZDATEFROM#", i.ZZDATEFROM != null ? i.ZZDATEFROM.ToString() : "");
                    }
                }

                if (Isportdues)
                {
                    AddArv = AddArv.Replace("#ZZTIMETO#", "00:00:00")
                                   .Replace("#ZZTIMEFROM#", "00:00:00")
                                   .Replace("#ZZDATETO#", "0000-00-00")
                                   .Replace("#ZZDATEFROM#", "0000-00-00");
                }

                AddArv = AddArv.Replace("@@@@@", ALLORDERITEMSINSTR);
                AddArv = AddArv.Replace("@#@@@@", ALLORDERITEMSINXSTR);
                AddArv = AddArv.Replace("@@#@@@", AllSCHEDULELINESSTR);
                AddArv = AddArv.Replace("@@@#@@", AllSCHEDULELINESXSTR);
                AddArv = AddArv + "$" + Convert.ToString(RevenuehdrID, CultureInfo.InvariantCulture);
            }

            return AddArv;
        }

        /// <summary>
        /// To Get SAP Posting Account Details by VCN
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public List<SAPPostingVO> GetSAPPostingAccountDetails(string VCN)
        {

            var account = (from ar in _unitOfWork.Repository<ArrivalNotification>().Query().Select()
                           join ac in _unitOfWork.Repository<AgentAccount>().Query().Select() on ar.AgentID equals ac.AgentID
                           where ar.RecordStatus == "A" && ar.VCN == VCN
                           select new SAPPostingVO
                          {
                              AgentAccountID = ac.AgentAccountID,
                              AccountNo = ac.AccountNo

                          });
            return account.ToList();

        }

        /// <summary>
        /// To Get SAP Posting Details 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public SAPPostingVO GetSAPPostingDetails(int SAPPostingID)
        {

            var sapposting = (from sap in _unitOfWork.Repository<SAPPosting>().Queryable()
                              orderby sap.SAPPostingID
                              where sap.SAPPostingID == SAPPostingID
                              select new SAPPostingVO
                              {
                                  VCN = sap.ReferenceNo,
                                  Reason = sap.Reason,                                  
                                  MessageType = sap.MessageType,
                                  PostingStatus = sap.PostingStatus,
                                  TransmitData = sap.TransmitData,
                                  RevenueAgentAccNo=sap.RevenueAgentAccNo,
                                  RevinueAccountNo=sap.RevinueAccountNo,
                                  MarinePostingId=sap.MarinePostingId,
                                  SAPPostingID=sap.SAPPostingID
                              }).FirstOrDefault<SAPPostingVO>();
            return sapposting;

        }


        /// <summary>
        /// To Get SAP Posting Invoice Response Details 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public SAPInvoiceItem GetSAPInvoiceResponseDetails(string MarineOrderNo)
        {

            var sapposting = (from sap in _unitOfWork.Repository<SAPInvoiceItem>().Query().Select()                              
                              where sap.MarineOrder == MarineOrderNo
                              select new SAPInvoiceItem
                              {
                                  InvoiceID = sap.InvoiceID,
                                  MarineOrder = sap.MarineOrder,
                                  BillingDocNo = sap.BillingDocNo,
                                  OrderNo = sap.OrderNo,
                                  ItemNo = sap.ItemNo,
                                  MaterialNo = sap.MaterialNo,
                                  Service = sap.Service,
                                  UOM = sap.UOM,
                                  Qunatity = sap.Qunatity,
                                  TarifF = sap.TarifF,
                                  TarifF2 = sap.TarifF2,
                                  Amount = sap.Amount,
                                  VAT = sap.VAT,
                                  NetAmount = sap.NetAmount,
                                  SalesOrgNo = sap.SalesOrgNo,
                                  AgentName = sap.AgentName,
                                  Address = sap.Address,
                                  TelephoneNo = sap.TelephoneNo,
                                  FaxNo = sap.FaxNo,
                                  Account = sap.Account,
                                  VesselID = sap.VesselID,
                                  VesselName = sap.VesselName,
                                  VesselTonnage = sap.VesselTonnage,
                                  VesselCapacity = sap.VesselCapacity,
                                  VesselLength = sap.VesselLength,
                                  ArrivalID = sap.ArrivalID,
                                  ArrivalDate = sap.ArrivalDate,
                                  Arrivaltime = sap.Arrivaltime,
                                  DepartureDate = sap.DepartureDate,
                                  DepartureTime = sap.DepartureTime,
                                  VoyageIn = sap.VoyageIn,
                                  VoyageOut = sap.VoyageOut,
                                  ESubscription = sap.ESubscription,
                                  NetValue = sap.NetValue
                              }).FirstOrDefault<SAPInvoiceItem>();
            return sapposting;

        }


        public string AutoArrivalUpdateForETAChange(string VCN, string portCode, string SAPVslNo)
        {
            var portcode = new SqlParameter("@p_PortCode", portCode);
            var vcn = new SqlParameter("@A_vcn", VCN);
            var msgType = new SqlParameter("@MSGTYPE", SAPMessageTypes.ArrivalUpdate);
            var RevAcNo = new SqlParameter("@RevenueAgentAccNo", "0");

            var SAPArvData = _unitOfWork.SqlQuery<SAPArrivalVO>("dbo.usp_GetToPostSapData @A_vcn, @MSGTYPE,@RevenueAgentAccNo", vcn, msgType, RevAcNo).ToList();
            string AddArv = "";
                AddArv = PostToSAP.UpdateArrival;
                AddArv = AddArv.Replace("#ARRNO#", SAPVslNo)
                            .Replace("#AED#", SAPArvData[0].AED != null ? SAPArvData[0].AED.ToString() : "")
                            .Replace("#AET#", SAPArvData[0].AET != null ? SAPArvData[0].AET.ToString() : "")
                            .Replace("#DED#", SAPArvData[0].DED != null ? SAPArvData[0].DED.ToString() : "")
                            .Replace("#DET#", SAPArvData[0].DET != null ? SAPArvData[0].DET.ToString() : "")
                            .Replace("#EDA#", SAPArvData[0].STREDA != null ? SAPArvData[0].STREDA.ToString() : "")
                            .Replace("#EDD#", SAPArvData[0].STREDD != null ? SAPArvData[0].STREDD.ToString() : "")
                            .Replace("#KUNNR#", SAPArvData[0].KUNNR != null ? SAPArvData[0].KUNNR.ToString() : "")
                            .Replace("#PORTCALL#", SAPArvData[0].PORTCALL != null ? SAPArvData[0].PORTCALL.ToString() : "")
                            .Replace("#VKORG#", SAPArvData[0].VKORG != null ? SAPArvData[0].VKORG.ToString() : "")
                            .Replace("#VOYIN#", SAPArvData[0].VOYIN != null ? SAPArvData[0].VOYIN.ToString() : "")
                            .Replace("#PORTORIGIN#", SAPArvData[0].PORTORIGIN != null ? SAPArvData[0].PORTORIGIN.ToString() : "")
                            .Replace("#VOYOUT#", SAPArvData[0].VOYOUT != null ? SAPArvData[0].VOYOUT.ToString() : "")
                            .Replace("#ZZBERTH#", SAPArvData[0].ZZBERTH != null ? SAPArvData[0].ZZBERTH.ToString() : "");  // Mahesh
            return AddArv;
        }
        public VesselCallMovement GetVesselCallMovementAtVCN(string VCN, string portcode)
        {

            var vcallmovement = (from vcm in _unitOfWork.Repository<VesselCallMovement>().Query().Select()
                                 where vcm.VCN == VCN && vcm.FromPositionPortCode == portcode
                                 select vcm);
            return vcallmovement.FirstOrDefault();

        }
        public SAPPosting GetDetailsByVCN(string VCN)
        {

            var sapposting = (from sap in _unitOfWork.Repository<SAPPosting>().Query().Select()
                                       where sap.ReferenceNo == VCN && sap.MessageType == SAPMessageTypes.ArrivalCreate && sap.PostingStatus == SAPPostingStatus.Completed
                                       select sap).FirstOrDefault<SAPPosting>();
            return sapposting;

        }     


        public List<SAPArrivalVO> GetAutoSAPVesselDetails()
        {          

            var vcnDetails = _unitOfWork.SqlQuery<SAPArrivalVO>("dbo.usp_GetAutoSAPVesselDetails").ToList();


            return vcnDetails;
        }


        public List<SAPArrivalVO> GetSAPVesselPostingDetails(SAPArrivalVO arrivalvo)
        {
            List<SAPArrivalVO> sapList = new List<SAPArrivalVO>();

            var vcallmovement = _unitOfWork.SqlQuery<VesselSAPPostingVO>("SELECT SAPAccountNo from VesselSAPAccount WHERE VesselID = @p0 AND VesselID NOT IN (select an.VesselID from ArrivalNotification an join SAPPosting sp on sp.ReferenceNo = an.VCN where an.RecordStatus = 'A' and an.VCN = @p1)", arrivalvo.VesselID, arrivalvo.VCN).ToList();          
           
            string AddArv = "";
            if (arrivalvo != null)
                {
                    AddArv = PostToSAP.AddArrival;
                    AddArv = AddArv.Replace("#AED#", arrivalvo.AED != null ? arrivalvo.AED.ToString() : "")
                                .Replace("#AET#", arrivalvo.AET != null ? arrivalvo.AET.ToString() : "")
                                .Replace("#DED#", arrivalvo.DED != null ? arrivalvo.DED.ToString() : "")
                                .Replace("#DET#", arrivalvo.DET != null ? arrivalvo.DET.ToString() : "")
                                .Replace("#EDA#", arrivalvo.STREDA != null ? arrivalvo.STREDA.ToString() : "")
                                .Replace("#KUNNR#", arrivalvo.KUNNR != null ? arrivalvo.KUNNR.ToString() : "0")
                                .Replace("#PORTCALL#", arrivalvo.PORTCALL != null ? arrivalvo.PORTCALL.ToString() : "")
                                .Replace("#VKORG#", arrivalvo.VKORG != null ? arrivalvo.VKORG.ToString() : "")
                                .Replace("#VOYIN#", arrivalvo.VOYIN != null ? arrivalvo.VOYIN.ToString() : "")
                                .Replace("#PORTORIGIN#", arrivalvo.PORTORIGIN != null ? arrivalvo.PORTORIGIN.ToString() : "")
                                .Replace("#VOYOUT#", arrivalvo.VOYOUT != null ? arrivalvo.VOYOUT.ToString() : "")
                                .Replace("#CODE#", arrivalvo.CODE != null ? arrivalvo.CODE.ToString() : "")
                                .Replace("#ZZBERTH#", arrivalvo.ZZBERTH != null ? arrivalvo.ZZBERTH.ToString() : "");


                    SAPPosting sapdata = new SAPPosting();


                    if (vcallmovement.Count > 0)
                    {
                        sapdata.PostingStatus = SAPPostingStatus.New;
                    }
                    else
                    {
                        sapdata.PostingStatus = SAPPostingStatus.Error;
                        sapdata.Remarks = "Error - SAP Vessel No. is not available";
                    }

                    sapdata.MessageType = SAPMessageTypes.ArrivalCreate;
                    sapdata.ReferenceNo = arrivalvo.VCN != null ? arrivalvo.VCN.ToString() : "";                    
                    sapdata.TransmitData = AddArv;
                    sapdata.PortCode = arrivalvo.PORTCODE != null ? arrivalvo.PORTCODE.ToString() : "";
                    sapdata.EmailStatus = "O";
                    sapdata.SMSStatus = "O";
                    sapdata.SystemNotificationStatus = "O";
                    sapdata.RecordStatus = RecordStatus.Active;
                    sapdata.CreatedBy = arrivalvo.USERID != null ? arrivalvo.USERID : 1;
                    sapdata.CreatedDate = DateTime.Now;
                    sapdata.ModifiedBy = arrivalvo.USERID != null ? arrivalvo.USERID : 1;
                    sapdata.ModifiedDate = DateTime.Now;
                    sapdata.Reason = arrivalvo.CODE != null ? arrivalvo.CODE.ToString() : "";
                    sapdata.RevinueAccountNo = arrivalvo.KUNNR != null ? arrivalvo.KUNNR.ToString() : ""; 


                    sapdata.ObjectState = ObjectState.Added;
                    _unitOfWork.Repository<SAPPosting>().Insert(sapdata);

                    _unitOfWork.SaveChanges();
                   
            }

            return sapList;
            
        }
    }
}














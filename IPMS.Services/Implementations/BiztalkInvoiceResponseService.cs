using Core.Repository;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using IPMS.Core.Repository.Exceptions;
using System;
using log4net;
using log4net.Config;
using IPMS.Domain;
using System.Collections.Generic;
using IPMS.Domain.Models;
using Core.Repository.Providers.EntityFramework;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BiztalkInvoiceResponseService : ServiceBase, IBiztalkInvoiceResponseService
    {
        private ILog log;
        public BiztalkInvoiceResponseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BiztalkInvoiceResponseService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(BiztalkInvoiceResponseService));
            _unitOfWork = new UnitOfWork(new TnpaContext());
            log.Info("BiztalkInvoiceResponseService class Instantiated....");
        }

        public SAPInvoiceVO BiztalkInvoiceResponse(SAPInvoiceVO objInvoice)
        {
            return EncloseTransactionAndHandleException(() =>
             {

                 log.Info("BiztalkResponse method called....");
                 var Remarks = "";
                 if (objInvoice != null)
                 {
                     if (!string.IsNullOrEmpty(objInvoice.ERRMSG))
                     {
                         log.Error(objInvoice.ERRMSG);
                        _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, "Error : "+objInvoice.ERRMSG, objInvoice.SAPPOSTINGID);
                     }
                     else
                     {
                     if (!string.IsNullOrEmpty(objInvoice.BILLINGDOC))
                     {
                         if (objInvoice.MESSAGETYPE == SAPMessageTypes.InvoiceCreate)
                         {

                             log.Debug("SAP Invoice response" + objInvoice.VBELN);
                             Remarks="Invoice Posted";
                             var brt = _unitOfWork.ExecuteSqlCommand("update SAPPosting set SAPReferenceNo=@p0,PostingStatus=@p1,Remarks=@p2 where SAPPostingID = @p3", objInvoice.BILLINGDOC, SAPPostingStatus.Completed, Remarks, objInvoice.SAPPOSTINGID);
                           

                             foreach (var item in objInvoice.EINVOICE)
                             {

                                 SAPInvoiceItem objItem = new SAPInvoiceItem();
                                 objItem.MarineOrder = objInvoice.VBELN;
                                 objItem.BillingDocNo = objInvoice.BILLINGDOC;
                                 objItem.NetValue = objInvoice.NETVALUE;
                                 objItem.ESubscription = objInvoice.ESUBRC;
                                 objItem.OrderNo = item.ORDERNUMBER;
                                 objItem.ItemNo = item.ITEMNUMBER;
                                 objItem.MaterialNo = item.MATNR;
                                 objItem.Service = item.SERVICE;
                                 objItem.UOM = item.UOM;
                                 objItem.Qunatity = item.QUANTITY;
                                 objItem.TarifF = item.TARIFF;
                                 objItem.TarifF2 = item.TARIFF2;
                                 objItem.Amount = item.AMOUNT;
                                 objItem.VAT = item.VAT;
                                 objItem.NetAmount = item.NETAMNT;
                                 objItem.SalesOrgNo = item.KUNNR;
                                 objItem.AgentName = item.AGENTNAME;
                                 objItem.Address = item.ADDRESS;
                                 objItem.TelephoneNo = item.CONTACTT;
                                 objItem.FaxNo = item.CONTACTF;
                                 objItem.Account = item.ACCOUNT;
                                 objItem.VesselID = item.VESSELID;
                                 objItem.VesselName = item.VESSELNAME;
                                 objItem.VesselTonnage = item.VESSELTON;
                                 objItem.VesselCapacity = item.VESSELCAP;
                                 objItem.VesselLength = item.VESSELLEN;
                                 objItem.ArrivalID = item.ARRIVALID;
                                 objItem.ArrivalDate = item.ARRIVALDATE.ToString();
                                 objItem.Arrivaltime = item.ARRIVALTIME.ToString();
                                 objItem.DepartureDate = item.DEPARTUREDATE.ToString();
                                 objItem.DepartureTime = item.DEPARTURETIME.ToString();   	
                                 objItem.VoyageIn = item.VOYAGERI;
                                 objItem.VoyageOut = item.VOYAGERO;

                                 var sapinvproc = new SAPInvoiceItem.SAPInvoiceReponsetoSAPInvoiceItem_proc(objItem);
                                 var result = _unitOfWork.ExecuteStoredProcedure<ParameterDirectionStoredProcedureReturn>(sapinvproc);

                             }

                             log.Debug("SAP Response Received with SAPReferenceNo..." + objInvoice.BILLINGDOC);

                         }
                     }
                     else
                     {
                         Remarks = "Invoice can't be update with out BILLINGDOC";
                         _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, Remarks, objInvoice.SAPPOSTINGID);
                         log.Error("BusinessExceptions :  BILLINGDOC is null");
                      //   throw new BusinessExceptions("Invoice can't be update with out BILLINGDOC");
                     }
                 }
                 }
                 else
                 {
                     log.Error("SAPInvoiceVO object is null");
                     _unitOfWork.ExecuteSqlCommand("update SAPPosting set PostingStatus=@p0,Remarks=@p1 where SAPPostingID = @p2", SAPPostingStatus.Error, Remarks, objInvoice.SAPPOSTINGID);
                  //   throw new BusinessExceptions("Response Object can't be null");

                 }
                 return objInvoice;
             });
        }
    }
}

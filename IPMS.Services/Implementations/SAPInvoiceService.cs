using Core.Repository;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using IPMS.Core.Repository.Exceptions;
using System;
using System.Collections.Generic;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SAPInvoiceService : ServiceBase, ISAPInvoiceService
    {
       
        private ILog log;

        public SAPInvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        public SAPInvoiceService()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(SAPInvoiceService));
            log.Info("SAPInvoiceService() Class Instantiated....");
            _unitOfWork = new UnitOfWork(new TnpaContext());
           
        }

        public SAPInvoiceVO ZIPMS_INVOICE(SAPInvoiceVO objInvoice)
        {
            return EncloseTransactionAndHandleException(() =>
             {
                 SAPInvoiceVO objRes = new SAPInvoiceVO();
                 if (objInvoice != null)
                 {
                     log.Info("ZIPMS_INVOICE method Instantiated....");
                     objRes.ESUBRC = 12;
                     objRes.BILLINGDOC = "MHD12";
                     objRes.NETVALUE = "1000";
                     objRes.VBELN = "MO100";

                     List<SAPInvoiceItemVO> invlist = new List<SAPInvoiceItemVO>();
                     SAPInvoiceItemVO inv=new SAPInvoiceItemVO();
                            inv.ORDERNUMBER = "OR123";
                            inv.ITEMNUMBER = "IT100";      
                            inv.MATNR = "Mt100"; 
                            inv.SERVICE = "SER10";
                            inv.UOM = "UOM100";
                            inv.QUANTITY = "10500";
                            inv.TARIFF = "TAR1";
                            inv.TARIFF2 = "TARF2";         
                            inv.AMOUNT = "RS900";         
                            inv.VAT = "VAT90";         
                            inv.NETAMNT = "NET100k";         
                            inv.KUNNR = "OR100";
                            inv.AGENTNAME = "MAAGENT";
                            inv.ADDRESS = "MAAGENT,INDIA";
                            inv.CONTACTT = "89897898";
                            inv.CONTACTF = "90909090";         
                            inv.ACCOUNT = "12345678";
                            inv.VESSELID = "90";
                            inv.VESSELNAME = "MAVESSEL";
                            inv.VESSELTON  = "VTON100";        
                            inv.VESSELCAP = "VC200";
                            inv.VESSELLEN = "MAVE45";
                            inv.ARRIVALID = "100";
                            inv.ARRIVALDATE = DateTime.Now.ToString();
                            inv.ARRIVALTIME = DateTime.Now.ToString();
                            inv.DEPARTUREDATE = DateTime.Now.ToString(); 
                            inv.DEPARTURETIME = DateTime.Now.ToString();
                            inv.VOYAGERI  = "VIN";
                            inv.VOYAGERO = "VOUT";

                            invlist.Add(inv);
                            objRes.EINVOICE = invlist;

                     log.Info("ZIPMS_INVOICE method completed....ENUMBER : " + objRes.BILLINGDOC);
                 }
                 else
                 {
                     log.Error("SAPInvoiceVO is null");
                     throw new BusinessExceptions("SAPInvoiceVO Object can't be null");
                 }
                 return objRes;

             });
        }
    }
}

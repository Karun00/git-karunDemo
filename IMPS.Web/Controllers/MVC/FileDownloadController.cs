using IPMS.Domain.Models;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

using IPMS.Domain.ValueObjects;
using ItextSharpDoc = iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;


namespace IPMS.Web.Controllers
{
    public class FileDownloadController : IpmsBaseController
    {
        [AllowAnonymous]
        public FileResult Download(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Download(id);
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }
            return File(document.Data, contentType, document.FileName);
        }
        #region FileExport methods

        public FileResult ExportPDF(ReportBuilderVO rbitem)
        {
            ReportBuilderVO rvo = new ReportBuilderVO();
            if (rbitem != null)
            {
                string p_displaycolumns = rbitem.DisplayColumns;
                string p_reportresult = rbitem.Result;
            }

            #region PDF

                // step 1: creation of a document-object
                var document = new iTextSharp.text.Document(ItextSharpDoc.PageSize.A4, 10, 10, 10, 10);

                //step 2: we create a memory stream that listens to the document
                var output = new MemoryStream();
                PdfWriter.GetInstance(document, output);

                //step 3: we open the document
                document.Open();

                //step 4: we add content to the document
                string[] reportColumns =
                    ("VCN,VoyageIn,VoyageOut,ETA,ETD,ReasonForVisit,IsTerminalOperator,AppliedDate,PilotExemption,VesselName")
                        .Split(','); //p_displaycolumns.Split(','); 
                var numOfColumns = reportColumns.Length;
                var dataTable = new PdfPTable(numOfColumns);

                dataTable.DefaultCell.Padding = 3;

                dataTable.DefaultCell.BorderWidth = 2;
                dataTable.DefaultCell.HorizontalAlignment = ItextSharpDoc.Element.ALIGN_CENTER;


                string result = string.Empty;
                result =
                    (@"[
                {\""VCN\"":\""VCNDB2014001\"",\""VoyageIn\"":\""11\"",\""VoyageOut\"":\""22\"",\""ETA\"":\""2014-08-15T00:00:00\"",\""ETD\"":\""2014-08-21T00:00:00\"",\""ReasonForVisit\"":\""BUNK\"",\""IsTerminalOperator\"":\""I\"",\""PilotExemption\"":\""I\"",\""VesselName\"":\""OLIVA\""},
                {\""VCN\"":\""VCNDB2014002\"",\""VoyageIn\"":\""33\"",\""VoyageOut\"":\""44\"",\""ETA\"":\""2014-08-21T00:00:00\"",\""ETD\"":\""2014-08-22T00:00:00\"",\""ReasonForVisit\"":\""BUNK\"",\""IsTerminalOperator\"":\""I\"",\""PilotExemption\"":\""I\"",\""VesselName\"":\""OLIVA\""},
                {\""VCN\"":\""VCNDB2014003\"",\""VoyageIn\"":\""11\"",\""VoyageOut\"":\""2\"",\""ETA\"":\""2014-08-21T00:00:00\"",\""ETD\"":\""2014-08-21T00:00:00\"",\""ReasonForVisit\"":\""BUNK\"",\""IsTerminalOperator\"":\""I\"",\""PilotExemption\"":\""I\"",\""VesselName\"":\""NEW LUCKY VII\""},
                {\""VCN\"":\""VCNDB2014004\"",\""VoyageIn\"":\""11\"",\""VoyageOut\"":\""22\"",\""ETA\"":\""2014-08-21T00:00:00\"",\""ETD\"":\""2014-08-23T00:00:00\"",\""ReasonForVisit\"":\""BUNK\"",\""IsTerminalOperator\"":\""I\"",\""PilotExemption\"":\""I\"",\""VesselName\"":\""NEW LUCKY VII\""},
                {\""VCN\"":\""VCNDB2014005\"",\""VoyageIn\"":\""22\"",\""VoyageOut\"":\""11\"",\""ETA\"":\""2014-08-21T00:00:00\"",\""ETD\"":\""2014-08-22T00:00:00\"",\""ReasonForVisit\"":\""BUNK\"",\""IsTerminalOperator\"":\""I\"",\""PilotExemption\"":\""I\"",\""VesselName\"":\""TRA LY 18 ALCI\""}]")
                        .Replace(Convert.ToChar(34), Convert.ToChar(32)).Replace("\\ ", Convert.ToChar(34).ToString());

                dynamic obj = DynamicJson.Parse(result);

                //dynamic obj = DynamicJson.Parse(p_reportresult);


                // Adding headers
                dataTable.HeaderRows = 1;
                dataTable.DefaultCell.BorderWidth = 1;

                foreach (string _reportColumns in reportColumns)
                {
                    // Adding headers
                    dataTable.AddCell(_reportColumns);
                }

                object value;
                foreach (System.Dynamic.ExpandoObject eo in obj)
                {
                    IDictionary<string, object> expando = eo;
                    foreach (string _reportColumns in reportColumns)
                    {
                        expando.TryGetValue(_reportColumns, out value);
                        if (value == null)
                            value = "";
                        dataTable.AddCell(value.ToString());
                    }
                }

                // Add table to the document
                document.Add(dataTable);

                //This is important don't forget to close the document
                document.Close();

                #region Under Testing - Don't Remove

                //StringReader sr = new StringReader("Sample Text");

                ////iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document();
                //HTMLWorker htmlparser = new HTMLWorker(document);
                //PdfWriter PDFWriter = PdfWriter.GetInstance(document, Response.OutputStream);
                //PDFWriter.ViewerPreferences = PdfWriter.PageModeUseOutlines;
                //document.Open();
                //htmlparser.Parse(sr);
                //document.Close();
                //Response.Write(document);
                //Response.Flush();
                //Response.End();

                #endregion

                // send the memory stream as File
                return File(output.ToArray(), "application/pdf", "DynamicReport.pdf");

                #endregion
            }


        #endregion


        //anusha 20/03/2024 start

        [AllowAnonymous]
        public ActionResult DownloadPortInfo(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "PORTI";
            string EntityCode = IPMS.Domain.EntityCodes.PortInformation;
            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.DownloadExternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }


        //anusha 04/04/2024



        [Authorize]
        public ActionResult DownloadFile_Docking(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "DOCKPLAN";

            string EntityCode = IPMS.Domain.EntityCodes.Docking_Plan;
            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }





        [Authorize]
        public ActionResult AgentDocuments(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "AGENT";
            string EntityCode = IPMS.Domain.EntityCodes.Agent;
            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }




        [Authorize]
        public ActionResult IncidentReportingDocuments(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            string EntityCode = IPMS.Domain.EntityCodes.IncidentReporting; //"INR";

            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }




        [Authorize]
        public ActionResult Pilotexemptiondocuments(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "PILTEXEMP";
            string EntityCode = IPMS.Domain.EntityCodes.PilotExemption;

            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }




        [Authorize]
        public ActionResult InternalEmployeePermitdocuments(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "EMPPERMIT";
            string EntityCode = IPMS.Domain.EntityCodes.InternalEmployeePermit;

            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }





        [Authorize]
        public ActionResult DryDockApplicationdocuments(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "DDAP";
            string EntityCode = IPMS.Domain.EntityCodes.Supp_DryDock;

            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }




        [Authorize]
        public ActionResult VesselAgentchangeDocuments(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "VACHREQ";
            string EntityCode = IPMS.Domain.EntityCodes.VACHREQ;
            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }




        [Authorize]
        public ActionResult VesselArrestImmobilizationSAMSAStopDocuments(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "VAIS";

            string EntityCode = IPMS.Domain.EntityCodes.VesselArrests;

            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }




        [Authorize]
        public ActionResult ArrivalNotificationDocument(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "ARVLNOT";
            string EntityCode = IPMS.Domain.EntityCodes.Arrival_Notification;

            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }






        [Authorize]
        public ActionResult DryDockExtensionDocument(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "DDEX";
            string EntityCode = IPMS.Domain.EntityCodes.Supp_DryDockExtension;
            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }



        [Authorize]
        public ActionResult DredgingpriorityDocumentsDownload(int id)
        {
            DocumentVO document = null;
            string contentType = null;
            //string EntityCode = "DREDGPRTY";
            string EntityCode = IPMS.Domain.EntityCodes.Dredging_Priority;

            using (IFileService _fileservice = new FileClient())
            {
                document = _fileservice.Downloadinternal(id, EntityCode);
            }

            if (document == null || document.Data == null)
            {
                return Content("Not Authorized  User");
            }

            if (string.IsNullOrEmpty(document.FileType))
            {
                contentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }
            else
            {
                contentType = document.FileType;
            }

            return File(document.Data, contentType, document.FileName);
        }
















    }
}
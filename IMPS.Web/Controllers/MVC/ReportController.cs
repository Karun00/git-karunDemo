using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.ValueObjects;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ItextSharpDoc = iTextSharp.text;


namespace IPMS.Web.Controllers.MVC
{
    public class ReportController : IpmsBaseController
    {
        internal static readonly InMemoryCache _cache = new InMemoryCache();
        object _cacheObject;
        string _key = string.Empty;
        string _portcode = string.Empty;
        string _fromDate = string.Empty;
        string _toDate = string.Empty;

        [Route("SetReportParameters")]
        public void Set_Report_Parameters(string fromDate, string toDate, int userid)
        {
            if (!string.IsNullOrEmpty(fromDate))
                ViewBag.fromDate = Convert.ToDateTime(fromDate, CultureInfo.InvariantCulture);
            else
                ViewBag.fromDate = DateTime.Now.AddDays(-6);

            _fromDate = ViewBag.fromDate.ToString("yyyy-MM-dd");
            ViewBag.FD = _fromDate;
            if (!string.IsNullOrEmpty(toDate))
                ViewBag.toDate = Convert.ToDateTime(toDate, CultureInfo.InvariantCulture);
            else
                ViewBag.toDate = DateTime.Now.AddDays(-6);

            _toDate = Convert.ToString(ViewBag.toDate); ;
            ViewBag.FD = _toDate;



            if (userid > 0)
                ViewBag.UserId = userid;
            else
                ViewBag.UserId = 1; //TO

            #region Port Code
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies["Port"];
            if (authCookie != null)
            {
                _portcode = authCookie.Value;
                StringBuilder inSb = new StringBuilder(_portcode);
                StringBuilder outSb = new StringBuilder(_portcode.Length);
                char c;
                for (int i = 0; i < _portcode.Length; i++)
                {
                    c = inSb[i];
                    c = (char)(c ^ 6); /// remember to use the same XORkey value you used in javascript
                    outSb.Append(c);
                }
                _portcode = outSb.ToString();

            }
            if (!string.IsNullOrEmpty(_portcode))
            {
                //TODO : Need to maintain Portcode instead in PortName in Report Parameter(s) to avoid hardcode
                ViewBag.PortName = _portcode;

            }
            else
                ViewBag.PortName = (_portcode == "CT" ? "Cape Town" : (_portcode == "DB" ? "Durban" : (_portcode == "EL" ? "East London" : (_portcode == "MB" ? "Mossel Bay" : (_portcode == "RB" ? "Richards Bay" : (_portcode == "SB" ? "Saldanha Bay" : (_portcode == "PE" ? "Port Elizabeth" : (_portcode == "NG" ? "Ngqura" : ""))))))));
            #endregion

            ViewBag.UserName = User.Identity.Name;
        }

        [Route("Report/ArrivalNotification")]
        public ActionResult ArrivalNotification()
        {
            //DateTime.Now.ToString("yyyy-MM-dd")
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/Eta")]
        public ActionResult Eta()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/CraftMasterReport")]
        public ActionResult CraftMasterReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/CargoTypeReport")]
        public ActionResult CargoTypeReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/CargoTypeDashboard")]
        public ActionResult CargoTypeDashboard()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/AnchorageCurrent")]
        public ActionResult AnchorageReport()
        {

            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/NumberOfMovementsPerVesselType")]
        public ActionResult ToatalnoofMovementsReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/Incidentreport")]
        public ActionResult Incidentreport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/Visitreportsummary")]
        public ActionResult Visitreportsummary()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/Safrepreport")]
        public ActionResult Safrepreport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/Weatherreport")]
        public ActionResult Weatherreport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/Completemovementreport")]
        public ActionResult Completemovementreport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }


        [Route("Report/CommodityHandle")]
        public ActionResult CommodityHandle()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/ServiceDeliveryRep")]
        public ActionResult ServiceDeliveryRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View("ServiceDelivery");
        }

        [Route("Report/MarineServiceDelayReport")]
        public ActionResult MarineServiceDelayReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View("MarineServiceDelayReport");
        }
        [Route("Report/MobileMovementsrep")]
        public ActionResult VesselMovements()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/MobileOperatrionalDelay")]
        public ActionResult OperationalDelays()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/MobileIncidentGraphRep")]
        public ActionResult IncidentsChart()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/MobilCargoVolumesReport")]
        public ActionResult CargoVolumes()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/MobilGrossRegisteredTonRep")]
        public ActionResult GrossRegisteredTonnage()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/MobilVesselArrivalRep")]
        public ActionResult VesselArrivals()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/BerthOccupancy")]
        public ActionResult BerthOccupancy()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/CommidityHandledRep")]
        public ActionResult CommidityHandledRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/ISPSRep")]
        public ActionResult Ispsrep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/DashISPSRep")]
        public ActionResult DashIspsrep(string fromDate, string toDate)
        {
            Set_Report_Parameters(fromDate, toDate, 0);
            return View();
        }

        [Route("Report/DashCommodityHandled")]
        public ActionResult DashCommodityHandled(string fromDate, string toDate)
        {
            Set_Report_Parameters(fromDate, toDate, 0);
            return View();
        }

        [Route("Report/DashVesselMovements")]
        public ActionResult DashVesselMovements(string fromDate, string toDate)
        {
            Set_Report_Parameters(fromDate, toDate, 0);
            return View();
        }

        [Route("Report/DashResourceUtilization")]
        public ActionResult DashResourceUtilization(string fromDate, string toDate)
        {
            Set_Report_Parameters(fromDate, toDate, 0);
            return View();
        }

        [Route("Report/DashBerthOccupancy")]
        public ActionResult DashBerthOccupancy(string fromDate, string toDate)
        {
            Set_Report_Parameters(fromDate, toDate, 0);
            return View();
        }

        [Route("Report/DashBerthUtilization")]
        public ActionResult DashBerthUtilization(string fromDate, string toDate)
        {
            Set_Report_Parameters(fromDate, toDate, 0);
            return View();
        }

        [Route("Report/DashDelayStastics")]
        public ActionResult DashDelayStastics(string fromDate, string toDate)
        {
            Set_Report_Parameters(fromDate, toDate, 0);
            return View();
        }

        [Route("Report/IMDGRep")]
        public ActionResult Imdgrep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/PortHealthClerance")]
        public ActionResult PortHealthClerance()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/AnchorageWaitingTimeRep")]
        public ActionResult AnchorageWaitingTimeRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/BerthUtilizationRep")]
        public ActionResult BerthUtilizationRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/ShipTurnaroundtime")]
        public ActionResult ShipTurnaroundtime()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/BerthOccupancyRep")]
        public ActionResult BerthOccupancyRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/ListofRegusersRep")]
        public ActionResult ListofRegusersRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }


        [Route("Report/SuppServicesRep")]
        public ActionResult SuppServicesRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/DelaysRep")]
        public ActionResult DelaysRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/ListLicenTerminalOper")]
        public ActionResult ListLicenTerminalOper()
        {
            Set_Report_Parameters(null, null, 0);
            return View("ListofLicensedTerminalOper");
        }

        [Route("Report/ListofBunkeringLicense")]
        public ActionResult ListofBunkeringLicense()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/ListofDivingLicenses")]
        public ActionResult ListofDivingLicenses()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/ListofRegAgentsRep")]
        public ActionResult ListofRegAgentsRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/ListofWasteDisposalLicense")]
        public ActionResult ListofWasteDisposalLicense()
        {
            return View();
        }
        [Route("Report/RailTurnAroundTime")]
        public ActionResult RailTurnAroundTime()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/TruckTurnaroundTime")]
        public ActionResult TruckTurnaroundTime()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/TerminalOperPerfRep")]
        public ActionResult TerminalOperPerfRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/TptDelaysRep")]
        public ActionResult TptDelaysRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/StatTerminalOperPerfRep")]
        public ActionResult StatTerminalOperPerfRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/StatVolumeActBudgRep")]
        public ActionResult StatVolumeActBudgRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/FuelUsageRep")]
        public ActionResult FuelUsageRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/DwellRep")]
        public ActionResult DwellRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MgmtNoofArrivals")]
        public ActionResult MgmtNoofArrivals()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MgmtTotNoofMoves")]
        public ActionResult MgmtTotNoofMoves()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MgmtTotGT")]
        public ActionResult MgmtTotGT()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MgmtMonthAvgAnchrTime")]
        public ActionResult MgmtMonthAvgAnchrTime()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MgmtAnchReasonNoofVessel")]
        public ActionResult MgmtAnchReasonNoOfVessel()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MgmtAnchReasonTimeinHrs")]
        public ActionResult MgmtAnchReasonTimeinHrs()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MgmtPilotDelays")]
        public ActionResult MgmtPilotDelays()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MgmtTugtDelays")]
        public ActionResult MgmtTugtDelays()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MgmtBerthDelays")]
        public ActionResult MgmtBerthDelays()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MgmtTugAvailable")]
        public ActionResult MgmtTugAvailable()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }


        [Route("Report/MgmtTugUtilization")]
        public ActionResult MgmtTugUtilization()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/TugAvailableUtilization")]
        public ActionResult TugAvailableUtilization()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }


        [Route("Report/SapInvoicereport")]
        public ActionResult SapInvoicereport()
        {
            Set_Report_Parameters(null, null, 0);
            return View("SapInvoiceReport");
        }


        [Route("Report/NewUsersReport")]
        public ActionResult NewUsersReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View("NewUsersReport");
        }

        [Route("Report/AuditTrailUserReport")]
        public ActionResult AuditTrailUserReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View("AuditTrailUsersReport");
        }

        [Route("Report/LastLogDateRep")]
        public ActionResult LastLogDateRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View("LastLogonDaterep");
        }

        [Route("Report/RolesPerUserRep")]
        public ActionResult RolesPerUserRep()
        {
            Set_Report_Parameters(null, null, 0);
            return View("RolesPerUserreport");
        }

        [Route("Report/NumberOfMovementsPerCraft")]
        public ActionResult MonthMovmReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/MOPSDetailReport")]
        public ActionResult MOPSDelayReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/AuditTrailMisReport")]
        public ActionResult AuditTrailMisReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View("AuditTrailMisReport");
        }

        [Route("Report/WegoDetailedReport")]
        public ActionResult WegoDetailedReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/SAPPostingReport")]
        public ActionResult SAPPostingReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/WasteDeclarationReport")]
        public ActionResult WasteDeclarationReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View("WasteDeclarationReport");
        }

        [Route("Report/SlotsOveriddenReport")]
        public ActionResult SlotsOveriddenReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/MOPSSummaryReport")]
        public ActionResult MOPSSummaryReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }
        [Route("Report/IMDGSummaryReport")]
        public ActionResult IMDGSummaryReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }


        #region Report Builder Methods
        [Route("ReportBuilders")]
        public ActionResult ManageReportBuilder()
        {
            return View();
        }

        /// <summary>
        /// To Save the Object into Cache
        /// </summary>
        /// <param name="rbvo"></param>
        /// <returns></returns>
        /// 
        [Route("SetData")]
        public ActionResult SetData(ReportBuilderVO rbvo)
        {
            #region maintaining object in Cache
            _key = "ReportBuilderVO";
            _cache.InvalidateItem(_key);
            _cache.PutItem(_key, rbvo, new string[0], TimeSpan.FromSeconds(1000), DateTimeOffset.MaxValue);
            #endregion
            return View();
        }
        /// <summary>
        /// Exporting to PDF file - Data extracting from Cache object
        /// </summary>
        /// <returns></returns>
        public FileStreamResult ExportPdf()
        {
            var output = new MemoryStream();
            ReportBuilderVO rbVO = new ReportBuilderVO();
            PdfWriter PDFWriter = null;
            dynamic obj = null;
            ItextSharpDoc.Document document = null;

            //Getting Object from Cache
            _cache.GetItem("ReportBuilderVO", out _cacheObject);
            rbVO = (ReportBuilderVO)_cacheObject;

            if (rbVO != null)
            {
                string p_displaycolumns = rbVO.DisplayColumns;
                string p_reportresult = rbVO.Result;

                string[] reportColumns = p_displaycolumns.Split(',');

                string[] reportTypes = rbVO.ColumnTypes.Split(',');

                #region Specifying PageSize & Orientation to the PDF document

                ItextSharpDoc.Rectangle pSize = new ItextSharpDoc.Rectangle(0, 0);

                //Specifying PageSize Based on Report Column Headings
                if (Convert.ToInt32(reportColumns.Length) <= 9)
                    pSize = ItextSharpDoc.PageSize.A4;
                else if (Convert.ToInt32(reportColumns.Length) <= 12)
                    pSize = ItextSharpDoc.PageSize.A3;
                else if (Convert.ToInt32(reportColumns.Length) <= 17)
                    pSize = ItextSharpDoc.PageSize.A2;
                else if (Convert.ToInt32(reportColumns.Length) <= 22)
                    pSize = ItextSharpDoc.PageSize.A1;
                else pSize = ItextSharpDoc.PageSize.A0;

                document = new ItextSharpDoc.Document();
                document.SetPageSize(pSize.Rotate());
                #endregion

                //step 2: we create a memory stream that listens to the document
                PDFWriter = PdfWriter.GetInstance(document, output);

                //step 3: we open the document
                document.Open();

                Paragraph p = new Paragraph(rbVO.ReportHeader);
                p.SpacingAfter = 3;
                p.Alignment = 1;
                p.Add("");
                document.Add(p);

                var numOfColumns = reportColumns.Length;
                var dataTable = new PdfPTable(numOfColumns);

                dataTable.DefaultCell.Padding = 3;

                dataTable.DefaultCell.BorderWidth = 2;

                //Parsing Query Resultant Json data to object
                obj = DynamicJson.Parse(p_reportresult);

                // Adding headers
                dataTable.HeaderRows = 1;
                dataTable.DefaultCell.BorderWidth = 1;

                #region Adding Report Column Headings
                foreach (string _reportColumns in reportColumns)
                {
                    //Creating Bold Font with White ForeColor
                    var boldFont = ItextSharpDoc.FontFactory.GetFont(ItextSharpDoc.FontFactory.HELVETICA_BOLD, 12, ItextSharpDoc.BaseColor.WHITE);

                    //Creating Phrase
                    var phrase = new ItextSharpDoc.Phrase();

                    //Adding Text to Phrase with Bold Font and ForeColor White
                    phrase.Add(new ItextSharpDoc.Chunk(_reportColumns, boldFont));

                    //Setting Text Background color
                    ItextSharpDoc.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(phrase);
                    cell.BackgroundColor = new ItextSharpDoc.BaseColor(0, 0, 0);

                    cell.HorizontalAlignment = ItextSharpDoc.Element.ALIGN_CENTER;

                    //Adding Headers to the PDF Table
                    dataTable.AddCell(cell);

                }
                #endregion

                #region Adding Table Body Content
                object value;
                foreach (System.Dynamic.ExpandoObject eo in obj)
                {
                    IDictionary<string, object> expando = eo;

                    //foreach (string _reportColumns in reportColumns)

                    for (int j = 0; j < reportColumns.Length; j++)
                    {
                        expando.TryGetValue(reportColumns[j], out value);
                        if (value == null)
                            value = "";

                        int strAlign = 0;

                        for (int i = j; i <= reportTypes.Length; i++)
                        {
                            if (reportTypes[i] == "int" || reportTypes[i] == "numeric" || reportTypes[i] == "tinyint"
                                || reportTypes[i] == "bigint" || reportTypes[i] == "float" || reportTypes[i] == "decimal" || reportTypes[i] == "smallint")
                            {
                                strAlign = ItextSharpDoc.Element.ALIGN_RIGHT;

                                break;
                            }
                            else if (reportTypes[i] == "datetime" && (value != null && value != ""))
                            {
                                strAlign = ItextSharpDoc.Element.ALIGN_LEFT;
                                DateTime date = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
                                value = date.ToString(rbVO.DateFormat, CultureInfo.InvariantCulture);
                                break;
                            }
                            else
                            {
                                strAlign = ItextSharpDoc.Element.ALIGN_LEFT;

                                break;
                            }
                        }

                        dataTable.DefaultCell.HorizontalAlignment = strAlign;
                        dataTable.AddCell(value.ToString());
                    }
                }
                #endregion

                // Add table to the document
                document.Add(dataTable);

                //This is important don't forget to close the document
                document.Close();
            }
            else
            {
                document = new ItextSharpDoc.Document();
                StringReader sr = new StringReader("<b>No Records found!</b>");
                HTMLWorker htmlparser = new HTMLWorker(document);
                PDFWriter = PdfWriter.GetInstance(document, output);
                PDFWriter = PdfWriter.GetInstance(document, Response.OutputStream);
                PDFWriter.ViewerPreferences = PdfWriter.PageModeUseOutlines;
                document.Open();
                htmlparser.Parse(sr);
                document.Close();
            }

            Response.ContentType = ExportTypes.PDF;

            //Response.AddHeader("Content-Disposition", "inline;DynamicReport.pdf");
            if (!string.IsNullOrWhiteSpace(rbVO.ReportHeader))
            {
                Response.AddHeader("Content-Disposition", "attachment;filename=" + rbVO.ReportHeader + ".pdf");
            }
            else
            {
                Response.AddHeader("Content-Disposition", "attachment;filename=ReportBuilders.pdf");
            }

            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(output.GetBuffer(), 0, output.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();
            return new FileStreamResult(Response.OutputStream, ExportTypes.PDF);
        }

        /// <summary>
        /// Exporting to Excel file - Data extracting from Cache object
        /// </summary>
        /// <returns></returns>
        public FileStreamResult ExportExcel()
        {
            StringWriter sw = new StringWriter(CultureInfo.InvariantCulture);
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            ReportBuilderVO rbVO = new ReportBuilderVO();

            //Getting Object from Cache
            _cache.GetItem("ReportBuilderVO", out _cacheObject);
            rbVO = (ReportBuilderVO)_cacheObject;

            if (rbVO != null)
            {
                string[] reportColumns = rbVO.DisplayColumns.Split(',');

                Response.Clear();
                Response.Buffer = true;
                if (!string.IsNullOrWhiteSpace(rbVO.ReportHeader))
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + rbVO.ReportHeader + ".xls");
                }
                else
                {
                    Response.AddHeader("content-disposition", "attachment;filename=ReportBuilders.xls");
                }
                Response.Charset = "";
                Response.ContentType = ExportTypes.Excel;

                Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">\n");
                hw.Write(GenerateHtml(rbVO));
            }
            else
            {
                sw.Write("No Records found!");
            }
            hw.Write("<b>Disclaimer</b>:This is Adhoc Report Designed by Anonymous");
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return new FileStreamResult(Response.OutputStream, ExportTypes.Excel);
        }

        #region Excel export Common Methods
        /// <summary>
        /// Converting Result into HTML format
        /// </summary>
        /// <param name="rbVO"></param>
        /// <returns></returns>
        private string GenerateHtml(ReportBuilderVO rbVO)
        {
            //Generating Dynamic Html
            StringBuilder sbHtml = new StringBuilder();
            StringBuilder sbBuilder = new StringBuilder();
            string[] reportColumns = rbVO.DisplayColumns.Split(',');
            string[] reportTypes = rbVO.ColumnTypes.Split(',');

            sbHtml.Append("<table cellpadding=\"3\" border=\"1\" cellspacing=\"0\" width=\"100%\" style=\"border-bottom:1px solid #9B9B9B; font-size:13px;\" class=\"table_rbuilder\"><thead>");

            #region Report Column Headings
            sbHtml.Append("<tr><th align=\"center\" colspan=\"" + reportColumns.Length + "\"  style=\"background-color:#e4e4e4; font-size:11px;\" class=\"table_rbuilder\"><h4>" + rbVO.ReportHeader + "</h4></th></tr><tr>");
            foreach (string _reportColumns in reportColumns)
            {
                sbHtml.Append("<th align=\"center\"  style=\"background-color:#e4e4e4; font-size:11px;\" class=\"table_rbuilder\">" + _reportColumns + "</th>");
            }

            sbHtml.Append("</tr></thead><tbody>");
            #endregion

            #region Adding Table Body Content

            dynamic obj = DynamicJson.Parse(rbVO.Result);

            object value;

            if (string.IsNullOrEmpty(rbVO.Result.ToString()))
            {
                sbHtml.Append("<tr><td align=\"middle\" colspan=\"" + reportColumns.Length + "\" style=\"font-size:12px;font:bold\" >No Records</td></tr>");
            }

            foreach (System.Dynamic.ExpandoObject eo in obj)
            {
                IDictionary<string, object> expando = eo;
                sbHtml.Append("<tr>");

                for (int j = 0; j < reportColumns.Length; j++)
                {
                    expando.TryGetValue(reportColumns[j], out value);
                    if (value == null)
                        value = "";
                    string strAlign = string.Empty;

                    for (int i = j; i <= reportTypes.Length; i++)
                    {
                        if (reportTypes[i] == "int" || reportTypes[i] == "numeric" || reportTypes[i] == "tinyint" || reportTypes[i] == "bigint" || reportTypes[i] == "float" || reportTypes[i] == "decimal" || reportTypes[i] == "smallint")
                        {
                            strAlign = "right";

                            break;
                        }
                        else if (reportTypes[i] == "datetime" && (value != null && value != ""))
                        {
                            strAlign = "right";

                            DateTime date = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
                            value = date.ToString(rbVO.DateFormat, CultureInfo.InvariantCulture);
                            break;
                        }
                        else
                        {
                            strAlign = "left";

                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
                        sbHtml.Append("<td align=\"" + strAlign + "\" style=\"font-size:11px;\" >" + value + "</td>");
                    }
                    else
                        sbHtml.Append("<td align=\"" + strAlign + "\" style=\"font-size:11px;\" >" + " " + "</td>");
                }
                sbHtml.Append("</tr>");

            }
            sbHtml.Append("</tbody></table>");

            #endregion

            return sbHtml.ToString();
        }
        #endregion

        #endregion




        #region Add by Dilshad  
        [Route("Report/NewBerthOccupancy")]
        public ActionResult SetNewBerthOccupancy()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/NewSAPDetailReport")]
        public ActionResult NewSAPDetailReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }

        [Route("Report/NewSAPPostingDetailReport")]
        public ActionResult NewSAPPostingDetailReport()
        {
            Set_Report_Parameters(null, null, 0);
            return View();
        }


        [Route("Report/PwdChangeReqLog")]
        public ActionResult PwdChangeReqLog()
        {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }
        #endregion
    }
}
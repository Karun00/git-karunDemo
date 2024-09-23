using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.ValueObjects;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using System.Web.UI;
using ItextSharpDoc = iTextSharp.text;
using System.Globalization;

namespace IPMS.Web.Controllers.MVC
{
    public class BerthPlanningController : IpmsBaseController
    {
        internal static readonly InMemoryCache _cache = new InMemoryCache();
        object _cacheObject;
        string _key = string.Empty;
        //
        // GET: /BerthPlanning/
        public ActionResult Index()
        {
            return View();
        }

        //For Viewing autoberthplanning User Interface
        [Route("BerthPlanning")]
        public ActionResult AutoBerthPlanning()
        {
            //return View();
            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("AutoBerthPlanning", privilege);
            }
            else
            {
                return View("NotFound");
            }
        }
        [Route("BerthPreScheduling")]
        public ActionResult BerthPreScheduling()
        {
            return View();
        }

        public ActionResult BerthPlanningJqueryMaster()
        {
            return View();
        }



        [Route("BerthPlanningGIS")]
        public ActionResult BerthPlanningGIS()
        {
            return View();
        }
        
        [Route("MBerthPlanningGIS")]
        public ActionResult MBerthPlanningGIS()
        {
            
            return View();
        }
        [Route("DeskBerthPlanningGIS")]
        public ActionResult DeskBerthPlanningGIS()
        {

            return View();
        }


        [Route("BerthPlanningTable")]
        public ActionResult BerthPlanningTable()
        {
            return View();
        }




        /// <summary>
        /// To Save the Object into Cache
        /// </summary>
        /// <param name="vcmtableVO"></param>
        /// <returns></returns>
        /// 
        [Route("SetDataBerth")]
        public ActionResult SetData(VCMTableData vcmtableVO)
        {
            #region maintaining object in Cache
            _key = "VCMTableData";
            _cache.InvalidateItem(_key);
            _cache.PutItem(_key, vcmtableVO, new string[0], TimeSpan.FromSeconds(1000), DateTimeOffset.MaxValue);
            #endregion
            return View();
        }

        /// <summary>
        /// Exporting to PDF file - Data extracting from Cache object
        /// </summary>
        /// <returns></returns>
        public FileStreamResult ExportPDF()
        {
            var output = new MemoryStream();
            VCMTableData vcmtableVO = new VCMTableData();
            PdfWriter PDFWriter = null;
            dynamic obj = null;
            ItextSharpDoc.Document document = null;


            //Getting Object from Cache
            _cache.GetItem("VCMTableData", out _cacheObject);
            vcmtableVO = (VCMTableData)_cacheObject;

            if (vcmtableVO.Result != null)
            {
                string p_displaycolumns = "VCN,VesselName,Berth,Berth,FromBollard,ToBollard,MooringBowBollard,MooringStemBollard,ETB,ETUB,ATB,ATUB,LengthOverallInM,CargoTypeName,ReasonForVisitName";
                string p_reportresult = vcmtableVO.Result;

                string[] reportColumns = p_displaycolumns.Split(',');

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

                var numOfColumns = reportColumns.Length;
                var dataTable = new PdfPTable(numOfColumns);

                dataTable.DefaultCell.Padding = 3;

                dataTable.DefaultCell.BorderWidth = 2;
                dataTable.DefaultCell.HorizontalAlignment = ItextSharpDoc.Element.ALIGN_CENTER;


                //Parsing Query Resultant Json data to object
                obj = DynamicJson.Parse(p_reportresult);


                // Adding headers
                dataTable.HeaderRows = 1;
                dataTable.DefaultCell.BorderWidth = 1;

                #region Adding Column Headings
                string p_displaycolumns1 = "VCN,Vessel,From Berth,To Berth,From Bollard,To Bollard,From Mooring Bollard,To Mooring Bollard,ETB,ETUB,ATB,ATUB,LOA,Cargo Type,Reason For Visit";
                string[] reportColumns1 = p_displaycolumns1.Split(',');
                foreach (string _reportColumns in reportColumns1)
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
                    int cnt = 0;
                    IDictionary<string, object> expando = eo;
                    foreach (string _reportColumns in reportColumns)
                    {
                        expando.TryGetValue(_reportColumns, out value);

                        if (value == null)
                        {
                            value = "";
                        }
                        else
                        {
                            // ETB ETUB  ATB ATUB are dates
                            // Modified By Srini on 28 / April / 2015 - Bug# 7836 - CUAT - 37 - Berth Planning - Berth Planning (Table view) - The PDF download option on the table view for Berth Planning gives a 'Runtime Error' (see below): Server Error in '/' Application. Runtime Error Description: An application error occurred on the
                            if ((reportColumns1[cnt].ToString() == "ETB") || (reportColumns1[cnt].ToString() == "ETUB") || (reportColumns1[cnt].ToString() == "ATB") || (reportColumns1[cnt].ToString() == "ATUB"))
                            {


                                if (CheckValueMatchesToDateTime(value.ToString()))
                                {
                                    if (!string.IsNullOrWhiteSpace(value.ToString()))
                                    {
                                        value = Convert.ToDateTime(value, CultureInfo.InvariantCulture).ToString(GlobalConstants.DateTimeFormatWith24Hour, CultureInfo.InvariantCulture);
                                    }
                                }
                            }
                        }

                        dataTable.AddCell(value.ToString());
                        cnt = cnt + 1;
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
            Response.AddHeader("Content-Disposition", "attachment;filename=BerthPlanningTable.pdf");
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

            VCMTableData vcmtableVO = new VCMTableData();

            //Getting Object from Cache
            _cache.GetItem("VCMTableData", out _cacheObject);
            vcmtableVO = (VCMTableData)_cacheObject;

            if (vcmtableVO.Result != null)
            {
                // string[] reportColumns = rbVO.DisplayColumns.Split(',');
                string p_displaycolumns = "VCN,VesselName,Berth,Berth,FromBollard,ToBollard,MooringBowBollard,MooringStemBollard,ETB,ETUB,ATB,ATUB,LengthOverallInM,CargoTypeName,ReasonForVisitName";


                //string[] reportColumns = p_displaycolumns.Split(',');

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=BerthPlanningTable.xls");
                Response.Charset = "";
                Response.ContentType = ExportTypes.Excel;

                Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">\n");
                hw.Write(GenerateHtml(vcmtableVO));
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
        /// <param name="vcmtableVO"></param>
        /// <returns></returns>
        private string GenerateHtml(VCMTableData vcmtableVO)
        {
            //Generating Dynamic Html
            StringBuilder sbHtml = new StringBuilder();
            //StringBuilder sbBuilder = new StringBuilder();
            //  string[] reportColumns = rbVO.DisplayColumns.Split(',');

            string p_displaycolumns = "VCN,VesselName,Berth,Berth,FromBollard,ToBollard,MooringBowBollard,MooringStemBollard,ETB,ETUB,ATB,ATUB,LengthOverallInM,CargoTypeName,ReasonForVisitName";

            string p_columnHeadings = "VCN,Vessel,From Berth,To Berth,From Bollard,To Bollard,From Mooring Bollard,To Mooring Bollard,ETB,ETUB,ATB,ATUB,LOA,Cargo Type,Reason For Visit";


            string[] reportColumns = p_displaycolumns.Split(',');
            string[] reportHeadings = p_columnHeadings.Split(',');

            sbHtml.Append("<table cellpadding=\"3\" border=\"1\" cellspacing=\"0\" width=\"100%\" style=\"border-bottom:1px solid #9B9B9B; font-size:13px;\" class=\"table_rbuilder\"><thead>");


            #region Report Column Headings
            sbHtml.Append("<tr><th align=\"center\" colspan=\"" + reportHeadings.Length + "\"  style=\"background-color:#e4e4e4; font-size:11px;\" class=\"table_rbuilder\">Report Heading</th></tr><tr>");
            foreach (string _reportColumns in reportHeadings)
            {
                sbHtml.Append("<th align=\"center\"  style=\"background-color:#e4e4e4; font-size:11px;\" class=\"table_rbuilder\">" + _reportColumns + "</th>");
            }

            sbHtml.Append("</tr></thead><tbody>");
            #endregion

            #region Adding Table Body Content

            dynamic obj = DynamicJson.Parse(vcmtableVO.Result);

            object value;

            if (string.IsNullOrEmpty(vcmtableVO.Result.ToString()))
            {
                sbHtml.Append("<tr><td align=\"middle\" colspan=\"" + reportColumns.Length + "\" style=\"font-size:12px;font:bold\" >No Records</td></tr>");
            }

            foreach (System.Dynamic.ExpandoObject eo in obj)
            {
                IDictionary<string, object> expando = eo;
                sbHtml.Append("<tr>");
                foreach (string _reportColumns in reportColumns)
                {
                    expando.TryGetValue(_reportColumns, out value);
                    if (value == null)
                    {
                        value = "";
                    }

                    string strAlign = string.Empty;
                    //if (col.DataType == System.Type.GetType("System.Decimal") || col.DataType == System.Type.GetType("System.Int32") || col.DataType == System.Type.GetType("System.Double"))
                    //    strAlign = "right";
                    //else
                    strAlign = "left";
                    //if (col.ColumnName.Trim() != "exampleno".ToString().ToUpper())
                    if (!string.IsNullOrEmpty(value.ToString()))
                    {
                        //if (_reportColumns.ToUpper().Contains("DATE"))
                        if (CheckValueMatchesToDateTime(value.ToString()))
                        {
                            string strdate = Convert.ToDateTime(value.ToString(), CultureInfo.InvariantCulture).ToString(GlobalConstants.DateTimeFormatWith24Hour, CultureInfo.InvariantCulture);
                            sbHtml.Append("<td align=\"" + strAlign + "\" style=\"font-size:11px;\" >" + strdate + "</td>");
                        }
                        else
                        {
                            sbHtml.Append("<td align=\"" + strAlign + "\" style=\"font-size:11px;\" >" + value + "</td>");
                        }
                    }
                    else
                    {
                        sbHtml.Append("<td align=\"" + strAlign + "\" style=\"font-size:11px;\" >" + "-" + "</td>");
                    }
                }
                sbHtml.Append("</tr>");

            }
            sbHtml.Append("</tbody></table>");

            #endregion

            return sbHtml.ToString();
        }
        #endregion

        private bool CheckValueMatchesToDateTime(string date)
        {
            Boolean hasDate = false;
            DateTime dateTime = new DateTime();

            //Use the Parse() method
            try
            {
                dateTime = DateTime.Parse(date, CultureInfo.InvariantCulture);
                hasDate = true;
            }
            catch
            {
                hasDate = false;
            }
            return hasDate;
        }
    }
}

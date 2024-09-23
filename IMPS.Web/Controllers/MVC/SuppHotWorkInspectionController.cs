using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPMS.Domain;

namespace IPMS.Web.Controllers.MVC
{
    public class SuppHotWorkInspectionController : IpmsBaseController
    {
        //
        // GET: /SuppHotWorkInspection/
        [Route("SuppHotWorkInspections")]
        public ActionResult SuppHotWorkInspection()
        {
            //return View("SuppHotWorkInspection",privilege);

            if (!string.IsNullOrEmpty(privilege.Privileges))
            {
                return View("SuppHotWorkInspection", privilege);

            }
            else
            {
                return View("NotFound");
            }

        }

        //////Under R & D for Print PDF
        /// <summary>
        /// adds / inserts the Supp Hot Work Inspection details
        /// </summary>
        /// <param name="SuppHotWorkInspectiondata"></param>
        /// <returns></returns>
        //[Authorize]
        //[Route("api/HTMLToPDF")]
        //[HttpPost]
       // [HttpPost]
        //public System.Web.Mvc.FileStreamResult HTMLToPDF(FormCollection form,string exporttype)
        //{
        //    var objForm = Request.Form;
        //    var output = new System.IO.MemoryStream();
        //    string p_displaycolumns = "";
        //    string p_reportresult = "";

        //    string[] reportColumns = p_displaycolumns.Split(',');

        //    // #region PDF
        //    ItextSharpDoc.Rectangle pSize = new ItextSharpDoc.Rectangle(0, 0);

        //    //Specifying PageSize Based on Report Column Headings
        //    if (Convert.ToInt32(reportColumns.Length) <= 9)
        //        pSize = ItextSharpDoc.PageSize.A4;
        //    else if (Convert.ToInt32(reportColumns.Length) <= 12)
        //        pSize = ItextSharpDoc.PageSize.A3;
        //    else if (Convert.ToInt32(reportColumns.Length) <= 17)
        //        pSize = ItextSharpDoc.PageSize.A2;
        //    else if (Convert.ToInt32(reportColumns.Length) <= 22)
        //        pSize = ItextSharpDoc.PageSize.A1;
        //    else pSize = ItextSharpDoc.PageSize.A0;

        //    ItextSharpDoc.Document document = new ItextSharpDoc.Document();
        //    document.SetPageSize(pSize.Rotate());


        //    //step 2: we create a memory stream that listens to the document
        //    iTextSharp.text.pdf.PdfWriter PDFWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(document, output);

        //    //step 3: we open the document
        //    document.Open();

        //    var numOfColumns = reportColumns.Length;
        //    var dataTable = new iTextSharp.text.pdf.PdfPTable(1);

        //    dataTable.DefaultCell.Padding = 3;

        //    dataTable.DefaultCell.BorderWidth = 2;
        //    dataTable.DefaultCell.HorizontalAlignment = ItextSharpDoc.Element.ALIGN_CENTER;



        //    ItextSharpDoc.Document docempty = new ItextSharpDoc.Document();
        //    System.IO.StringReader sr = new System.IO.StringReader("<b>No Records found!</b>");
        //    iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(docempty);
        //    iTextSharp.text.pdf.PdfWriter PDFWriter1 = iTextSharp.text.pdf.PdfWriter.GetInstance(docempty, output);
        //    PDFWriter1 = iTextSharp.text.pdf.PdfWriter.GetInstance(docempty, Response.OutputStream);
        //    PDFWriter1.ViewerPreferences = iTextSharp.text.pdf.PdfWriter.PageModeUseOutlines;
        //    docempty.Open();
        //    htmlparser.Parse(sr);
        //    docempty.Close();
        //    Response.ContentType = ExportTypes.PDF;
        //    //Response.AddHeader("Content-Disposition", "inline;DynamicReport.pdf");
        //    Response.AddHeader("Content-Disposition", "attachment;filename=DynamicReport.pdf");
        //    Response.Buffer = true;
        //    Response.Clear();
        //    Response.OutputStream.Write(output.GetBuffer(), 0, output.GetBuffer().Length);
        //    Response.OutputStream.Flush();
        //    Response.End();
        //    return new System.Web.Mvc.FileStreamResult(Response.OutputStream, ExportTypes.PDF);
        //}
	}
}
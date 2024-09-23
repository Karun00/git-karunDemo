using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IPMS.Web.Content.Reports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //rptViewer.ServerReport.ReportServerCredentials = new ReportCredentials("myuser", "mypass");
            ShowReport();
        }
        private void ShowReport()
        {
            //try
            //{
                rptViewer.ProcessingMode = ProcessingMode.Remote;  // ProcessingMode will be Either Remote or Local
                string urlReportServer = "http://nitsez14/Reports";
                rptViewer.ServerReport.ReportServerUrl = new Uri(urlReportServer); //Set the ReportServer Url
                rptViewer.ServerReport.ReportPath = "/Tutorial/Sales Orders.rdlc"; //Passing the Report Path                
                rptViewer.ServerReport.Refresh();
                rptViewer.AsyncRendering = false;
                rptViewer.SizeToReportContent = true;
                ////Creating an ArrayList for combine the Parameters which will be passed into SSRS Report
                //ArrayList reportParam = new ArrayList();
                //reportParam = ReportDefaultPatam();

                //ReportParameter[] param = new ReportParameter[reportParam.Count];
                //for (int k = 0; k < reportParam.Count; k++)
                //{
                //    param[k] = (ReportParameter)reportParam[k];
                //}
               //  pass crendentitilas
              //  rptViewer.ServerReport.ReportServerCredentials = 
                 // new ReportServerCredentials("uName", "PassWORD", "doMain");

               // pass parmeters to report
                //rptViewer.ServerReport.SetParameters(param); //Set Report Parameters
               
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private ArrayList ReportDefaultPatam()
        {
            ArrayList arrLstDefaultParam = new ArrayList();
            arrLstDefaultParam.Add(CreateReportParameter("ReportTitle", "Title of Report"));
            arrLstDefaultParam.Add(CreateReportParameter("ReportSubTitle", "Sub Title of Report"));
            return arrLstDefaultParam;
        }
        private ReportParameter CreateReportParameter(string paramName, string pramValue)
        {
            ReportParameter aParam = new ReportParameter(paramName, pramValue);
            return aParam;
        }
    }
}
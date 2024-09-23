using IPMS.Core.Repository.Exceptions;
using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Clients;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.Api;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace IPMS.Web.Controllers.API
{
    public class FileController : ApiControllerBase
    {
        IAccountService _loginservice;
        IFileService _fileservice;
        ITptDocumentUploadService _TptDocumentUploadservice; 
        ILog log = log4net.LogManager.GetLogger(typeof(FileController));
       

        public FileController()
        {
            _loginservice = new AccountClient();
            _fileservice = new FileClient();
            _TptDocumentUploadservice = new TptDocumentUploadClient();
        }
        [HttpPost]
        public Task<HttpResponseMessage> Upload(HttpRequestMessage request)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Uploads");
            var provider = new MultipartFormDataStreamProvider(root);
            if (log.IsDebugEnabled)
            {
                log.Debug("The Path is :  " + root);
            }
            // Read the form data.
            return Request.Content.ReadAsMultipartAsync(provider).ContinueWith(t =>
            {
                HttpResponseMessage response = null;
                var files = new List<string>();
                byte[] byteArray = null;
                DocumentVO document = null;
                //  int doucmentId = 0;
                string documentType = null;
                string fileType = null;
                string documentName = null;//Document Name is not provided in front end, so we are using filename as document name
                foreach (MultipartFileData file in provider.FileData)
                {
                    if (string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                    {
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
                    }

                    fileType = file.Headers.ContentType.ToString();
                    string fileName = file.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    if (documentName == null)
                    {
                        documentName = fileName.Substring(0, 4);
                    }
                    files.Add(fileName);
                    File.Copy(file.LocalFileName, Path.Combine(root, fileName), true);
                    byteArray = StreamFile(Path.Combine(root, fileName));
                    document = _fileservice.Upload(byteArray, fileName, fileType, documentName, documentType);
                    File.Delete(Path.Combine(root, file.LocalFileName));
                    File.Delete(Path.Combine(root, fileName));
                }
                response = request.CreateResponse(HttpStatusCode.OK, document);
                return response;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private byte[] StreamFile(string filename)
        {

            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] byteArrayData = new byte[fs.Length];
            fs.Read(byteArrayData, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return byteArrayData; 
        }

        //======================================
        public List<TerminalDelaysVO> TerminalDelaysDataDisplay(string path, string TemplateDoc, string PortCode, out string error)
        {
            log.Info("TerminalDelayDataDisplay method is called");  
            try
            { 
                string[][] drc1;
                string errMsg = string.Empty;
                List<TerminalDelaysVO> delayValues = new List<TerminalDelaysVO>();
                error = string.Empty;
                DataRowCollection drc = ValidateUploadDocumentSchema(path, TemplateDoc,out errMsg);
               // if (errMsg != string.Empty)
                if (!string.IsNullOrEmpty(errMsg))
                {
                    error = errMsg;
                    delayValues = null;
                    return delayValues;
                } 
                string[][] drs = new string[drc.Count][];

                log.Info("Count of DRS " + drc.Count.ToString(CultureInfo.InvariantCulture));
                for (int i = 0; i < drc.Count; i++)
                {
                    string[] row = new string[drc[i].ItemArray.Length];
                    for (int j = 0; j < drc[i].ItemArray.Length; j++)
                    {
                        row[j] = drc[i].ItemArray[j].ToString();
                    } 
                    drs[i] = row;
                }

                DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
                DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;
                string[] TemplateColumns = new string[dtTemplate.Columns.Count];
                for (int i = 0; i < dtTemplate.Columns.Count; i++)
                {
                    dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
                    TemplateColumns[i] = (dtTemplate.Rows[0][i].ToString());
                }

                log.Info("Before calling _TptDocumentUploadservice.TerminalDelayDataValid(TemplateColumns, drs)");
                //drc1 = _TptDocumentUploadservice.TerminalDelayDataValid(dtTemplatecolumns, drc); 
                drc1 = _TptDocumentUploadservice.TerminalDelayDataValid(TemplateColumns, drs);
                log.Info("After calling _TptDocumentUploadservice.TerminalDelayDataValid(TemplateColumns, drs)");
                

                
                    DataTable dt1 = ReadExcelWithStream(path);
                    log.Info("TerminalDelayData is converting to VO method is started");
                    for (int i = 1; i < dt1.Rows.Count; i++)
                    {
                        TerminalDelaysVO tdvo = new TerminalDelaysVO();
                        tdvo.PortCode = PortCode;
                        tdvo.PortName = dt1.Rows[i][0].ToString();
                        tdvo.TerminalOperator = dt1.Rows[i][1].ToString();
                        DateTime dtParam;
                        //System.Globalization.CultureInfo enGB = CultureInfo.CurrentCulture;  
                        log.Info("Before DateValue is converting to VO method");
                        try
                        {
                            dtParam = Convert.ToDateTime(dt1.Rows[i][2].ToString(), CultureInfo.InvariantCulture);
                            //dtParam = DateTime.Parse(dt1.Rows[i][2].ToString());
                            //dtParam = DateTime.ParseExact(dt1.Rows[i][2].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None);
                            // bool outcome = DateTime.TryParseExact(dt1.Rows[i][2].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtParam); 
                        }
                        catch (FormatException efx)
                        {
                            log.Info("Error occured in converting string to datetime for arrival date. Error is" + efx.Message);
                            throw;
                        }
                        tdvo.ArrivalDate = Convert.ToDateTime(dtParam.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);
                        log.Info("After DateValue is converting to VO method");
                        //tdvo.ArrivalDate = Convert.ToDateTime(dt1.Rows[i][2].ToString());
                        tdvo.IMONo = dt1.Rows[i][3].ToString();
                        tdvo.Voyage = dt1.Rows[i][4].ToString();
                        tdvo.VesselName = dt1.Rows[i][5].ToString();
                        tdvo.Terminal = dt1.Rows[i][6].ToString();
                        tdvo.CargoType = dt1.Rows[i][7].ToString();
                        tdvo.ReasonForDelay = dt1.Rows[i][8].ToString();
                        tdvo.DelayDuration = Convert.ToDecimal(dt1.Rows[i][9].ToString(), CultureInfo.InvariantCulture);
                        tdvo.UnitOfMeasure = dt1.Rows[i][10].ToString();
                        tdvo.Comments = dt1.Rows[i][11].ToString();
                        tdvo.RecordStatus = drc1[i][12].ToString();
                    tdvo.ErrorStatus = drc1[i][13].ToString();
                        delayValues.Add(tdvo);
                    }
                    return delayValues; 
            }
            catch (FormatException fx)
            {
                log.Error("Exception = ", fx);
                throw new BusinessExceptions(fx.Message);
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex); 
                throw; 
            }
        }

        public List<OutTurnVolumesVO> OutTurnVolumesDataDisplay(string path, string TemplateDoc, string PortCode, out string error)
        {
            log.Info("OutTurnVolumesDataDisplay method is called");
            try
            { 
                //=================================
                string[][] drc1;

                List<OutTurnVolumesVO> OutTurnVolumes = new List<OutTurnVolumesVO>();
                string errMsg = string.Empty;
                DataRowCollection drc = ValidateUploadDocumentSchema(path, TemplateDoc,out errMsg);
                error = string.Empty;
             //   if (errMsg != string.Empty)
                    if (!string.IsNullOrEmpty(errMsg))
                {
                    error = errMsg;
                    OutTurnVolumes = null;
                    return OutTurnVolumes;
                } 
                string[][] drs = new string[drc.Count][];
                log.Info("Count of DRS " + drc.Count.ToString(CultureInfo.InvariantCulture));
                for (int i = 0; i < drc.Count; i++)
                {
                    string[] row = new string[drc[i].ItemArray.Length];
                    for (int j = 0; j < drc[i].ItemArray.Length; j++)
                    {
                        row[j] = drc[i].ItemArray[j].ToString();
                    }
                    drs[i] = row;
                }

                DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
                DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;
                string[] TemplateColumns = new string[dtTemplate.Columns.Count];

                for (int i = 0; i < dtTemplate.Columns.Count; i++)
                {
                    dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
                    TemplateColumns[i] = (dtTemplate.Rows[0][i].ToString());
                }
                log.Info("Before calling _TptDocumentUploadservice.OutTurnsDataValid(TemplateColumns, drs)");
                drc1 = _TptDocumentUploadservice.OutTurnsDataValid(TemplateColumns, drs);
                log.Info("After calling _TptDocumentUploadservice.OutTurnsDataValid(TemplateColumns, drs)");
                //==================================  
                for (int i = 1; i < drc1.Length; i++)
                {

                    OutTurnVolumesVO otVo = new OutTurnVolumesVO();
                    otVo.PortCode = PortCode;
                    otVo.PortName = drc1[i][0].ToString();
                    otVo.TerminalOperator = drc1[i][1].ToString();
                    DateTime dtParam2;
                    System.Globalization.CultureInfo enGB1 = new System.Globalization.CultureInfo("en-GB");
                    //if (drc1[i][2].ToString() != "")
                        if (!string.IsNullOrEmpty(drc1[i][2].ToString()))
                    {
                        dtParam2 = Convert.ToDateTime(drc1[i][2].ToString(), enGB1);
                        otVo.ArrivalDate = Convert.ToDateTime(dtParam2.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);
                    }
                    //otVo.ArrivalDate = Convert.ToDateTime(drc1[i][2].ToString());
                    otVo.IMONo = drc1[i][3].ToString();
                    otVo.Voyage = drc1[i][4].ToString();
                    otVo.VesselName = drc1[i][5].ToString();
                    otVo.Terminal = drc1[i][6].ToString();
                    otVo.CargoType = drc1[i][7].ToString();
                    //if (drc1[i][8].ToString() != "")
                        if (!string.IsNullOrEmpty(drc1[i][8].ToString()))
                    {
                        otVo.OutTurnVolume = Convert.ToDecimal(drc1[i][8].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                    }
                    otVo.UnitOfMeasure = drc1[i][9].ToString();
                    DateTime dtParam, dtParam1;
                    System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                    //if (drc1[i][10].ToString() != "")
                        if (!string.IsNullOrEmpty(drc1[i][10].ToString()))
                    {
                        dtParam = Convert.ToDateTime(drc1[i][10].ToString(), enGB);
                        otVo.FirstCraneSwing = Convert.ToDateTime(dtParam.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);
                    }
                    //if (drc1[i][11].ToString() != "")
                        if (!string.IsNullOrEmpty(drc1[i][11].ToString()))
                    {
                        dtParam1 = Convert.ToDateTime(drc1[i][11].ToString(), enGB);
                        otVo.LastCraneSwing = Convert.ToDateTime(dtParam1.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);//Convert.ToDateTime(drc1[i][11].ToString());
                    }
                    if (!string.IsNullOrEmpty(drc1[i][12].ToString()))
                    //if (drc1[i][12].ToString() != "")
                    {
                        otVo.NoOfCranes = Convert.ToInt16(drc1[i][12].ToString(), CultureInfo.InvariantCulture);
                    }
                    otVo.Comments = drc1[i][13].ToString();
                    otVo.RecordStatus = drc1[i][14].ToString();
                    otVo.ErrorStatus = drc1[i][15].ToString();
                    OutTurnVolumes.Add(otVo);

                }
                return OutTurnVolumes;
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }

        public List<TerminalWeeklyDataVO> TerminalWeeklyDataDisplay(string path, string TemplateDoc, string PortCode,out string error)
        {
            log.Info("TerminalWeeklyDataDisplay method is called");
            //=================================
            //try
            //{
                string[][] drc1;
                string errMsg = string.Empty;
                List<TerminalWeeklyDataVO> TerminalWeeklyData = new List<TerminalWeeklyDataVO>();
                
                error = string.Empty;
                DataRowCollection drc = ValidateUploadDocumentSchema(path, TemplateDoc, out errMsg);
                //if (errMsg !=string.Empty)
                    if (!string.IsNullOrEmpty(errMsg))
                {
                    error = errMsg;
                    TerminalWeeklyData = null;
                    return TerminalWeeklyData;
                } 
                string[][] drs = new string[drc.Count][];
                log.Info("Count of DRS " + drc.Count.ToString(CultureInfo.InvariantCulture));

                for (int i = 0; i < drc.Count; i++)
                {
                    string[] row = new string[drc[i].ItemArray.Length];
                    for (int j = 0; j < drc[i].ItemArray.Length; j++)
                    {
                        row[j] = drc[i].ItemArray[j].ToString();
                    }
                    drs[i] = row;
                }

                DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
                DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;

                string[] TemplateColumns = new string[dtTemplate.Columns.Count];

                for (int i = 0; i < dtTemplate.Columns.Count; i++)
                {
                    // columnNames[i] = dtTemplate.Rows[0][i].ToString();
                    dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());

                    TemplateColumns[i] = (dtTemplate.Rows[0][i].ToString());
                }
                //bool IsDataValid = OutTurnsDataValid(dtTemplatecolumns, drc);
                drc1 = _TptDocumentUploadservice.TerminalDataValid(TemplateColumns, drs);
                //================================== 
              
                //return ExecuteFaultHandledOperation(() =>
                //{
                    DataTable dt1 = ReadExcelWithStream(path);
                    for (int i = 1; i < dt1.Rows.Count; i++)
                    {
                        TerminalWeeklyDataVO twdVo = new TerminalWeeklyDataVO();
                        twdVo.PortCode = PortCode;
                        twdVo.PortName = dt1.Rows[i][0].ToString();
                        twdVo.Terminal = dt1.Rows[i][11].ToString();
                        twdVo.TerminalOperator = dt1.Rows[i][1].ToString();
                        twdVo.WeekNo = Convert.ToInt32(dt1.Rows[i][2].ToString(), CultureInfo.InvariantCulture);

                        DateTime dtParam;
                        System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                        dtParam = Convert.ToDateTime(dt1.Rows[i][3].ToString(), enGB);
                        twdVo.WeekEnding = Convert.ToDateTime(dtParam.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);

                        //twdVo.WeekEnding = Convert.ToDateTime(dt1.Rows[i][3].ToString());

                        twdVo.PerformanceArea = dt1.Rows[i][4].ToString();
                        twdVo.Measure = dt1.Rows[i][5].ToString();
                        twdVo.UnitOfMeasure = dt1.Rows[i][6].ToString();
                        twdVo.CargoType = dt1.Rows[i][7].ToString();
                        twdVo.Planned = Convert.ToDecimal(dt1.Rows[i][8].ToString(), CultureInfo.InvariantCulture);
                        twdVo.Actual = Convert.ToDecimal(dt1.Rows[i][9].ToString(), CultureInfo.InvariantCulture);
                        twdVo.Comments = dt1.Rows[i][10].ToString();
                        twdVo.RecordStatus = drc1[i][12].ToString();
                        twdVo.ErrorStatus = drc1[i][13].ToString();
                        TerminalWeeklyData.Add(twdVo);
                    }
                    return TerminalWeeklyData;
                //});

            //}
            //catch (Exception ex)
            //{
            //    log.Error("Exception = ", ex);
            //    throw new FaultException(ex.Message);
               
            //}
        }

        public List<RailPlanVO> RailPlanDataDisplay(string path, string TemplateDoc, string PortCode,out string error)
        {
            log.Info("RailPlanDataDisplay method is called");
            try
            {
                //=================================
                string[][] drc1;
                string errMsg = string.Empty;

                List<RailPlanVO> RailPlanData = new List<RailPlanVO>();
                DataRowCollection drc = ValidateUploadDocumentSchema(path, TemplateDoc,out errMsg);
                error = string.Empty;
             //   if (errMsg != string.Empty)
                    if (!string.IsNullOrEmpty(errMsg))
                {
                    error = errMsg;
                    RailPlanData = null;
                    return RailPlanData;
                } 
                string[][] drs = new string[drc.Count][];
                log.Info("Count of DRS " + drc.Count.ToString(CultureInfo.InvariantCulture));
                for (int i = 0; i < drc.Count; i++)
                {
                    string[] row = new string[drc[i].ItemArray.Length];
                    for (int j = 0; j < drc[i].ItemArray.Length; j++)
                    {
                        row[j] = drc[i].ItemArray[j].ToString();
                    }
                    drs[i] = row;
                }
                DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
                DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;

                string[] TemplateColumns = new string[dtTemplate.Columns.Count];
                for (int i = 0; i < dtTemplate.Columns.Count; i++)
                {
                    dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
                    TemplateColumns[i] = (dtTemplate.Rows[0][i].ToString());

                }
                log.Info("Before calling _TptDocumentUploadservice.RailPlanValid(TemplateColumns, drs)");
                drc1 = _TptDocumentUploadservice.RailPlanValid(TemplateColumns, drs);
                log.Info("Before calling _TptDocumentUploadservice.RailPlanValid(TemplateColumns, drs)");
                //================================== 
                //return ExecuteFaultHandledOperation(() =>
                //{
                    DataTable dt1 = ReadExcelWithStream(path);
                    for (int i = 1; i < dt1.Rows.Count; i++)
                    {
                        RailPlanVO rpVo = new RailPlanVO();
                        rpVo.PortCode = PortCode;
                        rpVo.PortName = dt1.Rows[i][0].ToString();
                        rpVo.Corridor = dt1.Rows[i][1].ToString();
                        rpVo.SlNo = i;
                        DateTime dtParam;
                        System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                        dtParam = Convert.ToDateTime(dt1.Rows[i][2].ToString(), enGB);
                        rpVo.PlannedDate = Convert.ToDateTime(dtParam.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);

                        rpVo.Schedule = Convert.ToInt32(dt1.Rows[i][3].ToString(), CultureInfo.InvariantCulture);
                        rpVo.TrainNo = Convert.ToInt32(dt1.Rows[i][4].ToString(), CultureInfo.InvariantCulture);
                        rpVo.Origin = dt1.Rows[i][5].ToString();
                        rpVo.Destination = dt1.Rows[i][6].ToString();
                        rpVo.BreakType = dt1.Rows[i][7].ToString();
                        rpVo.PlannedETD = dt1.Rows[i][8].ToString();
                        rpVo.PlannedETA = dt1.Rows[i][9].ToString();
                        rpVo.Loco = dt1.Rows[i][10].ToString();
                        rpVo.LocoQty = dt1.Rows[i][11].ToString();
                        rpVo.NWBRef = dt1.Rows[i][12].ToString();
                        rpVo.PlannedTons = dt1.Rows[i][13].ToString();
                        rpVo.Load = dt1.Rows[i][14].ToString();
                        rpVo.Remark = dt1.Rows[i][15].ToString();
                        rpVo.YQ = dt1.Rows[i][16].ToString();
                        rpVo.TrainStatus = dt1.Rows[i][17].ToString();
                        rpVo.RecordStatus = drc1[i][18].ToString();
                        RailPlanData.Add(rpVo);
                    }
                    return RailPlanData;
               // });
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }

        public List<RailPlanVO> RailChangeNotificationsDataDisplay(string path, string TemplateDoc, string PortCode,out string error)
        {
            log.Info("RailChangeNotificationsDataDisplay method is called");
            try
            {

                //=================================
                string[][] drc1;
                string errMsg = string.Empty;

                List<RailPlanVO> RailChangeNotificationsData = new List<RailPlanVO>();
                error = string.Empty;
                DataRowCollection drc = ValidateUploadDocumentSchema(path, TemplateDoc,out errMsg);
               // if (errMsg != string.Empty)
                    if (!string.IsNullOrEmpty(errMsg))
                {
                    error = errMsg;
                    RailChangeNotificationsData = null;
                    return RailChangeNotificationsData;
                } 
                string[][] drs = new string[drc.Count][];
                log.Info("Count of DRS " + drc.Count.ToString(CultureInfo.InvariantCulture));
                for (int i = 0; i < drc.Count; i++)
                {
                    string[] row = new string[drc[i].ItemArray.Length];
                    for (int j = 0; j < drc[i].ItemArray.Length; j++)
                    {
                        row[j] = drc[i].ItemArray[j].ToString();
                    }
                    drs[i] = row;
                } 
                
                DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
                DataColumnCollection dtTemplatecolumns = dtTemplate.Columns; 
                string[] TemplateColumns = new string[dtTemplate.Columns.Count];

                for (int i = 0; i < dtTemplate.Columns.Count; i++)
                {
                    dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
                    TemplateColumns[i] = (dtTemplate.Rows[0][i].ToString());
                }

                log.Info("Before calling _TptDocumentUploadservice.RailChangeNotificationsValid(TemplateColumns, drs)");
                drc1 = _TptDocumentUploadservice.RailChangeNotificationsValid(TemplateColumns, drs);
                log.Info("After calling _TptDocumentUploadservice.RailChangeNotificationsValid(TemplateColumns, drs)");

                //================================== 
                //return ExecuteFaultHandledOperation(() =>
                //{
                    DataTable dt1 = ReadExcelWithStream(path);
                    for (int i = 1; i < dt1.Rows.Count; i++)
                    {
                        RailPlanVO rpVo = new RailPlanVO();
                        rpVo.PortCode = PortCode;
                        rpVo.PortName = dt1.Rows[i][0].ToString();
                        rpVo.Corridor = dt1.Rows[i][1].ToString();
                        rpVo.SlNo = i;
                        DateTime dtParam;
                        System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                        dtParam = Convert.ToDateTime(dt1.Rows[i][2].ToString(), enGB);
                        rpVo.PlannedDate = Convert.ToDateTime(dtParam.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);
                        rpVo.TrainNo = Convert.ToInt32(dt1.Rows[i][3].ToString(),CultureInfo.InvariantCulture);
                        rpVo.ReasonForChange = dt1.Rows[i][4].ToString();
                        rpVo.TrainStatus = dt1.Rows[i][5].ToString();
                        rpVo.NewETD = dt1.Rows[i][6].ToString();
                        rpVo.NewETA = dt1.Rows[i][7].ToString();
                        rpVo.RecordStatus = drc1[i][8].ToString();
                        RailChangeNotificationsData.Add(rpVo);
                    }
                    return RailChangeNotificationsData;
                //});
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }
         
        public List<RailPlanVO> ArrivalAndDepartureDataDisplay(string path, string TemplateDoc, string PortCode,out string error)
        {
            log.Info("ArrivalAndDepartureDataDisplay method is called");
            try
            {
                //=================================
                string[][] drc1;
                string errMsg=string.Empty;

                List<RailPlanVO> ArrivalAndDepartureData = new List<RailPlanVO>();
                error = string.Empty;
                DataRowCollection drc = ValidateUploadDocumentSchema(path, TemplateDoc,out errMsg);
              //  if (errMsg != string.Empty)
                    if (!string.IsNullOrEmpty(errMsg))

                {
                    error = errMsg;
                    ArrivalAndDepartureData = null;
                    return ArrivalAndDepartureData;
                } 
                string[][] drs = new string[drc.Count][];
                log.Info("Count of DRS " + drc.Count.ToString(CultureInfo.InvariantCulture));
                for (int i = 0; i < drc.Count; i++)
                {
                    string[] row = new string[drc[i].ItemArray.Length];
                    for (int j = 0; j < drc[i].ItemArray.Length; j++)
                    {
                        row[j] = drc[i].ItemArray[j].ToString();
                    }
                    drs[i] = row;
                }
                 
                DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
                DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;

                string[] TemplateColumns = new string[dtTemplate.Columns.Count];

                for (int i = 0; i < dtTemplate.Columns.Count; i++)
                {
                    dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
                    TemplateColumns[i] = (dtTemplate.Rows[0][i].ToString());
                }
                log.Info("Before calling _TptDocumentUploadservice.ArrivalAndDepartureDataValid(TemplateColumns, drs)");
                drc1 = _TptDocumentUploadservice.ArrivalAndDepartureDataValid(TemplateColumns, drs);
                log.Info("After calling _TptDocumentUploadservice.ArrivalAndDepartureDataValid(TemplateColumns, drs)");

                //================================== 
               // return ExecuteFaultHandledOperation(() =>
                //{
                    DataTable dt1 = ReadExcelWithStream(path);
                    for (int i = 1; i < dt1.Rows.Count; i++)
                    {
                        RailPlanVO rpVo = new RailPlanVO();
                        rpVo.PortCode = PortCode;
                        rpVo.PortName = dt1.Rows[i][0].ToString();
                        rpVo.Corridor = dt1.Rows[i][1].ToString();
                        rpVo.SlNo = i;
                        DateTime dtParam;
                        System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                        dtParam = Convert.ToDateTime(dt1.Rows[i][2].ToString(), enGB);
                        rpVo.PlannedDate = Convert.ToDateTime(dtParam.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);
                        rpVo.TrainNo = Convert.ToInt32(dt1.Rows[i][3].ToString(), CultureInfo.InvariantCulture);
                        // rpVo.ReasonForChange = dt1.Rows[i][4].ToString();
                        rpVo.TrainMovement = dt1.Rows[i][4].ToString();
                        if (rpVo.TrainMovement == "0")
                        {
                            rpVo.ATD = dt1.Rows[i][5].ToString();
                        }
                        else
                        {
                            rpVo.ATA = dt1.Rows[i][5].ToString();
                        }
                        // rpVo.ATA = dt1.Rows[i][6].ToString(); 
                        rpVo.RecordStatus = drc1[i][6].ToString(CultureInfo.InvariantCulture);
                        ArrivalAndDepartureData.Add(rpVo);
                    }
                    return ArrivalAndDepartureData;
                //});
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }

        private DataRowCollection ValidateUploadDocumentSchema(string path, string TemplateDoc,out string msg)
        {
            log.Info("ValidateUploadDocumentSchema method is called");
            try
            {
                string currFileExtension = string.Empty;
                DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
                DataTable dtUpload = null;
                currFileExtension = System.IO.Path.GetExtension(path);

                if (currFileExtension == ".csv")
                {
                    dtUpload = ReadExcelWithStream(path);  //Read .CSV File
                }
                bool areEqual = false;
                DataRowCollection drc;
                if (dtTemplate.Columns.Count == dtUpload.Columns.Count)
                {
                    int colCount = dtTemplate.Columns.Count;
                    DataColumnCollection dtUploadcolumns = dtUpload.Columns;
                    DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;

                    for (int i = 0; i < dtUpload.Columns.Count; i++)
                    {
                        dtUploadcolumns[i].ColumnName = dtUpload.Rows[0][i].ToString();
                        dtTemplatecolumns[i].ColumnName = dtTemplate.Rows[0][i].ToString();
                    }
                    DataColumn[] dtcolumns = new DataColumn[dtTemplatecolumns.Count];
                    dtTemplatecolumns.CopyTo(dtcolumns, 0);
                    areEqual = IsSchemaEqual(dtTemplatecolumns, dtUploadcolumns);

                    DataColumn dc = new DataColumn("Record Status");
                    dtUpload.Columns.Add("Record Status");

                    //DataColumn dc1 = new DataColumn("ErrorStatus");
                    dtUpload.Columns.Add("ErrorStatus");
                }
                drc = dtUpload.Rows;
                msg = "";
                if (areEqual == !true)
                {
                    log.Info("Schema mismatch with the uploaded document.");
                    msg = "Schema mismatch with the uploaded document.";
                }
                else
                {
                    log.Info("ValidateUploadDocumentSchema method process completed");
                }
                return drc;
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }

        }
        bool AreAllColumnsEmpty(DataRow dr)
        {
            if (dr == null)
            {
                return true;
            }
            else
            {
                foreach (var value in dr.ItemArray)
                {
                    if (value != null)
                    {
                        if (!string.IsNullOrEmpty(value.ToString())) //if (value.ToString() != "")

                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        private DataTable ReadExcelWithStream(string path)
        {
            DataTable dt = new DataTable();
            bool isDtHasColumn = false;   //Mark if DataTable Generates Column

            log.Info("Read Stream : " + path);
            StreamReader reader = new StreamReader(path, System.Text.Encoding.Default);

            try
            {
                while (!reader.EndOfStream)
                {
                    string message = reader.ReadLine();
                    string[] splitResult = message.Split(new char[] { ',' }, StringSplitOptions.None);  //Read One Row and Separate by Comma, Save to Array
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < splitResult.Length; i++)
                    {
                        if (!isDtHasColumn) //If not Generate Column
                        {
                            dt.Columns.Add("column" + i, typeof(string));
                        }
                        row[i] = splitResult[i];
                    }
                    dt.Rows.Add(row);  //Add Row
                    isDtHasColumn = true;  //Mark the Existed Column after Read the First Row, Not Generate Column after Reading Later Rows
                }
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
            return dt;
        }

        public static bool IsSchemaEqual(DataColumnCollection a, DataColumnCollection b)
        {  
            try
            {
                if (a != null && b != null)
                {
                    if (a.Count != b.Count)
                        return false;
                    for (int i = 0; i < a.Count; i++)
                    {
                        //if (a[i].ColumnName != b[i].ColumnName && a[i].DataType != b[i].DataType)
                        if (a[i].ColumnName.Trim().ToLower() != b[i].ColumnName.Trim().ToLower())
                        {
                            //if(a[i].DataType != b[i].DataType)
                            return false;
                        }
                    }
                }
                return true;
                
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }

        }
        public static string ParseValue(string str)
        {
            bool boolValue;
            Int32 intValue;
            Int64 bigintValue;
            decimal doubleValue;
            DateTime dateValue;

            // Place checks higher in if-else statement to give higher priority to type.

            if (bool.TryParse(str, out boolValue))
                return "System_Boolean";
            else if (Int32.TryParse(str, out intValue))
                return "System_Int32";
            else if (Int64.TryParse(str, out bigintValue))
                return "System_Int64";
            else if (decimal.TryParse(str, out doubleValue))
                return "System_Decimal";
            else if (DateTime.TryParse(str, out dateValue))
                return "System_DateTime";
            else return "System_String"; 
        } 

        /// <summary>
        /// Single File Upload as well as Multiple File Upload for TPT Documents
        /// Author : Naren M
        /// Dated  : 5th Mar 2015
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<HttpResponseMessage> TptDocFileUpload(HttpRequestMessage request, string documentType, string PortCode)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Uploads");
            var provider = new MultipartFormDataStreamProvider(root);
            if (log.IsDebugEnabled)
            {
                log.Debug("The Path is :  " + root);
            }
            try
            {
                // Read the form data.
                System.Collections.ArrayList arrLst = new System.Collections.ArrayList();
                List<TerminalDelaysVO> tdvo = new List<TerminalDelaysVO>();
                List<OutTurnVolumesVO> otvo = new List<OutTurnVolumesVO>();
                List<TerminalWeeklyDataVO> twdvo = new List<TerminalWeeklyDataVO>();
                return Request.Content.ReadAsMultipartAsync(provider).ContinueWith(t =>
                {
                    HttpResponseMessage response = null;
                    var files = new List<string>();
                    byte[] byteArray = null;
                    DocumentVO document = null;

                    //  int doucmentId = 0;
                    string fileType = null;
                    string documentName = null;//Document Name is not provided in front end, so we are using filename as document name
                    string fileName = null;

                    foreach (MultipartFileData file in provider.FileData)
                    {
                        byteArray = null;
                        _fileservice = new FileClient();

                        if (string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                        {
                            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
                        }

                        fileType = file.Headers.ContentType.ToString();
                        fileName = file.Headers.ContentDisposition.FileName;

                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }
                        if (documentName == null)
                        {
                            documentName = fileName.Substring(0, 4);
                        }
                        files.Add(fileName);

                        File.Copy(file.LocalFileName, Path.Combine(root, fileName), true);

                        //===Code for template validation 
                        string root1 = HttpContext.Current.Server.MapPath("~/TPT_Templates"); 
                        string TemplateDoc = "";
                        string errorMessage = string.Empty;
                        if (documentType == "TLDL")
                        {
                            TemplateDoc = Path.Combine(root1, "TNPA_TerminalDelaysDailyUploadTemplate.csv");
                            tdvo = TerminalDelaysDataDisplay(Path.Combine(root, fileName), TemplateDoc, PortCode, out errorMessage);
                            List<TerminalDelaysVO> tdvo1 = new List<TerminalDelaysVO>();
                           // if (errorMessage != string.Empty)
                                if (!string.IsNullOrEmpty(errorMessage))
                            {
                                response = request.CreateResponse<string>(HttpStatusCode.OK, errorMessage);
                            }
                            else
                            {
                                response = request.CreateResponse<List<TerminalDelaysVO>>(HttpStatusCode.OK, tdvo);
                            }
                            //response = request.CreateResponse<List<TerminalDelaysVO>>(HttpStatusCode.OK, tdvo);
                        }
                        else if (documentType == "OTVL")
                        {
                            TemplateDoc = Path.Combine(root1, "TNPA_OutturnVolumesDailyUploadTemplate.csv");
                            otvo = OutTurnVolumesDataDisplay(Path.Combine(root, fileName), TemplateDoc, PortCode,out errorMessage);
                            List<OutTurnVolumesVO> Otvol1 = new List<OutTurnVolumesVO>();
                            //if (errorMessage != string.Empty)
                            if (!string.IsNullOrEmpty(errorMessage))
                            {
                                response = request.CreateResponse<string>(HttpStatusCode.OK, errorMessage);
                            }
                            else
                            {
                                response = request.CreateResponse<List<OutTurnVolumesVO>>(HttpStatusCode.OK, otvo);
                            }
                           // response = request.CreateResponse<List<OutTurnVolumesVO>>(HttpStatusCode.OK, otvo);
                        }
                        else if (documentType == "TLDT")
                        {
                            TemplateDoc = Path.Combine(root1, "TNPA_TerminalDataWeeklyUploadTemplate.csv");

                            twdvo = TerminalWeeklyDataDisplay(Path.Combine(root, fileName), TemplateDoc, PortCode, out errorMessage);
                            List<TerminalWeeklyDataVO> td1 = new List<TerminalWeeklyDataVO>();
                            //if (errorMessage != string.Empty)
                                if (!string.IsNullOrEmpty(errorMessage))
                            {
                                response = request.CreateResponse<string>(HttpStatusCode.OK, errorMessage);
                            }
                            else
                            {
                                response = request.CreateResponse<List<TerminalWeeklyDataVO>>(HttpStatusCode.OK, twdvo);
                            }
                           // response = request.CreateResponse<List<TerminalWeeklyDataVO>>(HttpStatusCode.OK, twdvo);
                        } 
                        byteArray = MultipleFileUploadStreamFile(Path.Combine(root, fileName));
                        document = _fileservice.Upload(byteArray, fileName, fileType, documentName, documentType);
                        arrLst.Add(document);

                        if (documentType != "TLDT" || documentType != "TLDL" || documentType != "OTVL")
                        {
                            File.Delete(Path.Combine(root, file.LocalFileName));
                            //File.Delete(Path.Combine(root, fileName));
                        }
                    }
                    return response;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                //throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Single File Upload as well as Multiple File Upload for TFR Documents
        /// Author : Naren M
        /// Dated  : 14th April 2015
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<HttpResponseMessage> TfrDocFileUpload(HttpRequestMessage request, string documentType, string PortCode)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Uploads");
            var provider = new MultipartFormDataStreamProvider(root);
            if (log.IsDebugEnabled)
            {
                log.Debug("The Path is :  " + root);
            }
            try
            {
                // Read the form data.
                System.Collections.ArrayList arrLst = new System.Collections.ArrayList();
                List<RailPlanVO> tdvo = new List<RailPlanVO>(); 
                return Request.Content.ReadAsMultipartAsync(provider).ContinueWith(t =>
                {
                    HttpResponseMessage response = null;
                    var files = new List<string>();
                    byte[] byteArray = null;
                    DocumentVO document = null;

                    //  int doucmentId = 0;
                    string fileType = null;
                    string documentName = null;//Document Name is not provided in front end, so we are using filename as document name
                    string fileName = null;

                    foreach (MultipartFileData file in provider.FileData)
                    {
                        byteArray = null;
                        _fileservice = new FileClient();

                        if (string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                        {
                            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
                        }

                        fileType = file.Headers.ContentType.ToString();
                        fileName = file.Headers.ContentDisposition.FileName;

                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }
                        if (documentName == null)
                        {
                            documentName = fileName.Substring(0, 4);
                        }
                        files.Add(fileName);

                        File.Copy(file.LocalFileName, Path.Combine(root, fileName), true);

                        //===Code for template validation 
                        string root1 = HttpContext.Current.Server.MapPath("~/TPT_Templates"); 
                        string TemplateDoc = "";
                        string errorMessage = string.Empty;
                        if (documentType == "RLOP")
                        {
                            TemplateDoc = Path.Combine(root1, "TNPA_RailPlanUploadTemplate.csv");
                            tdvo = RailPlanDataDisplay(Path.Combine(root, fileName), TemplateDoc, PortCode,out errorMessage);
                            List<RailPlanVO> tdvo1 = new List<RailPlanVO>();
                            //if (errorMessage != string.Empty)
                                if (!string.IsNullOrEmpty(errorMessage))
                            {
                                response = request.CreateResponse<string>(HttpStatusCode.OK, errorMessage);
                            }
                            else
                            {
                                response = request.CreateResponse<List<RailPlanVO>>(HttpStatusCode.OK, tdvo);
                            }
                            //response = request.CreateResponse<List<RailPlanVO>>(HttpStatusCode.OK, tdvo);
                        }
                        else if (documentType == "RLCN")
                        {
                            TemplateDoc = Path.Combine(root1, "TNPA_RailChangeNotificationUploadTemplate.csv");
                            tdvo = RailChangeNotificationsDataDisplay(Path.Combine(root, fileName), TemplateDoc, PortCode, out errorMessage);
                            List<RailPlanVO> Otvol1 = new List<RailPlanVO>();
                            //if (errorMessage != string.Empty)
                            if (!string.IsNullOrEmpty(errorMessage))

                            {
                                response = request.CreateResponse<string>(HttpStatusCode.OK, errorMessage);
                            }
                            else
                            {
                                response = request.CreateResponse<List<RailPlanVO>>(HttpStatusCode.OK, tdvo);
                            }
                            //response = request.CreateResponse<List<RailPlanVO>>(HttpStatusCode.OK, tdvo);
                        }
                        else if (documentType == "DAAN")
                        {
                            TemplateDoc = Path.Combine(root1, "TNPA_ArrivalAndDepartureTimeUploadTemplate.csv");
                            tdvo = ArrivalAndDepartureDataDisplay(Path.Combine(root, fileName), TemplateDoc, PortCode, out errorMessage);
                            List<RailPlanVO> td1 = new List<RailPlanVO>();
                            //if (errorMessage != string.Empty)
                                if (!string.IsNullOrEmpty(errorMessage))
                            {
                                response = request.CreateResponse<string>(HttpStatusCode.OK, errorMessage);
                            }
                            else
                            {
                                response = request.CreateResponse<List<RailPlanVO>>(HttpStatusCode.OK, tdvo);
                            }
                            //response = request.CreateResponse<List<RailPlanVO>>(HttpStatusCode.OK, tdvo);
                        }
                        //=== 
                        byteArray = MultipleFileUploadStreamFile(Path.Combine(root, fileName));
                        document = _fileservice.Upload(byteArray, fileName, fileType, documentName, documentType);
                        arrLst.Add(document);

                        if (documentType != "RLOP" || documentType != "RLCN" || documentType != "DAAN")
                        {
                            File.Delete(Path.Combine(root, file.LocalFileName));
                            //File.Delete(Path.Combine(root, fileName));
                        }
                    }
                    return response;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
            }
        }


        /// <summary>
        /// Single File Upload as well as Multiple File Upload
        /// Author : Omprakash Kotha 
        /// Dated  : 3rd Sep 2014
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<HttpResponseMessage> MultipleFileUpload(HttpRequestMessage request, string documentType)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Uploads");
            var provider = new MultipartFormDataStreamProvider(root);
            var result = false;
            if (log.IsDebugEnabled)
            {
                log.Debug("The Path is :  " + root);
            }
            // Read the form data.
            System.Collections.ArrayList arrLst = new System.Collections.ArrayList();
            return Request.Content.ReadAsMultipartAsync(provider).ContinueWith(t =>
            {
                HttpResponseMessage response = null;
                var files = new List<string>();
                byte[] byteArray = null;
                DocumentVO document = null;

                //  int doucmentId = 0;
                string fileType = null;
                string documentName = null;//Document Name is not provided in front end, so we are using filename as document name
                string fileName = null;
                //log.Debug("Log Ramana 1");
                foreach (MultipartFileData file in provider.FileData)
                {
                    byteArray = null;
                    _fileservice = new FileClient();
                   // log.Debug("Log Ramana 2");

                    if (string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                    {
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
                    }
                   // log.Debug("Log Ramana 3");
                    fileType = file.Headers.ContentType.ToString();
                    fileName = file.Headers.ContentDisposition.FileName;

                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    if (documentName == null)
                    {
                        documentName = fileName.Substring(0, 4);
                    }
                    files.Add(fileName);
                   // log.Debug("Log Ramana 4");

                    string SecurityScanFile = WebConfigurationManager.AppSettings["SecurityScanner"];
                    string filePath = "@" + root +"\\"+fileName;
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = SecurityScanFile;
                    startInfo.Arguments = "-Scan -ScanType 3 -File " + filePath;
                    process.StartInfo = startInfo;

                    //log.Debug("Log Ramana 5");
                    if (!string.IsNullOrEmpty(SecurityScanFile) && File.Exists(SecurityScanFile))
                        result = process.Start();
                    else
                        result = true;
                    log.Debug("Log Ramana 6");
                    if (result == true)
                    {
                        log.Debug("Log Ramana 7");
                        File.Copy(file.LocalFileName, Path.Combine(root, fileName), true);
                        //log.Debug("Log Ramana File Copy 8" + Path.Combine(root, fileName));
                        byteArray = MultipleFileUploadStreamFile(Path.Combine(root, fileName));
                        try
                        {
                            //log.Debug("Log Ramana File Copy 8.1" + Path.Combine(root, fileName));

                        log.Debug("Upload service function start");
                        log.Debug("anonymousUserId:  " + Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]));
                        log.Debug("doc Name: " + documentName);
                        log.Debug("doc type: " + documentType);
                        document = _fileservice.Upload(byteArray, fileName, fileType, documentName, documentType);
                        log.Debug("Upload service function end");
                        }
                        catch (Exception ex)
                        {
                           // log.Debug("Log Ramana File Copy 8.2" + Path.Combine(root, fileName));
                     
                           log.Debug("Exception Message in File Controller:" + ex.Message);
                            log.Debug(ex.InnerException.Message);
                            log.Debug(ex.StackTrace);



                        }
                      
                        arrLst.Add(document);
                        //log.Debug("Log Ramana File Copy 9");
                    }
                    else {
                        //log.Debug("Log Ramana File Copy 10");
                        throw new BusinessExceptions("File upload unsuccessful, Attempted file is infected with virus.");
                    }
                    //log.Debug("Log Ramana File Copy 11");
                    File.Delete(Path.Combine(root, file.LocalFileName));
                    File.Delete(Path.Combine(root, fileName));
                }
                //log.Debug("Log Ramana File Copy 12");
                response = request.CreateResponse(HttpStatusCode.OK, arrLst);

                return response;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        /// <summary>
        /// Single File Upload as well as Multiple File Upload
        /// Author : Omprakash Kotha 
        /// Dated  : 3rd Sep 2014
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private byte[] MultipleFileUploadStreamFile(string filename)
        {

            byte[] buff = null;
            FileStream fs = new FileStream(filename,
                                           FileMode.Open,
                                           FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(filename).Length;
            buff = br.ReadBytes((int)numBytes);
            fs.Flush();
            fs.Close();
            br.Close();
            return buff;
        }

        /// <summary>
        /// To get the file size config value
        /// Author : Amala Gunda
        /// Dated  : 28th Nov 2014
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage GetFileSizeConfigValue(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                string configValue = _fileservice.GetFileSizeConfigValue();
                response = request.CreateResponse<string>(HttpStatusCode.OK, configValue);
                return response;
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fileservice.Dispose();
                _TptDocumentUploadservice.Dispose();
                _loginservice.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

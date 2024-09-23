using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web.Hosting;
using log4net;
using System.Runtime.Serialization;
using System.Globalization;
using System.Text.RegularExpressions;
using IPMS.Core.Repository.Exceptions;
using System.Xml;
using IPMS.Services.TrainSummary;
//using IPMS.Services.TfrWebService;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class TptDocumentUploadService : ServiceBase, ITptDocumentUploadService
    {
       // private IBudgetedValuesRepository _BudgetedValuesRepository;
        private TptDocumentUploadRepository _TptDocUploadRepository;
        private ITerminalOperatorRepository _terminalOperatorRepository;
        private ISubCategoryRepository _SubCategoryRepository;
        private IBerthRepository _BerthRepository;

        private readonly ILog log; 
        public TptDocumentUploadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         //   _BudgetedValuesRepository = new BudgetedValuesRepository(_unitOfWork);
            _TptDocUploadRepository = new TptDocumentUploadRepository(_unitOfWork);
            _terminalOperatorRepository = new TerminalOperatorRepository(_unitOfWork);
            _SubCategoryRepository = new SubCategoryRepository(_unitOfWork);
            _BerthRepository = new BerthRepository(_unitOfWork);

            _UserId = GetUserIdByLoginname(_LoginName);
            log = LogManager.GetLogger(typeof(TptDocumentUploadService));
        }

        public TptDocumentUploadService()
        {  
            _unitOfWork = new UnitOfWork(new TnpaContext()); 
            _TptDocUploadRepository = new TptDocumentUploadRepository(_unitOfWork);
            _terminalOperatorRepository = new TerminalOperatorRepository(_unitOfWork);
            _SubCategoryRepository = new SubCategoryRepository(_unitOfWork);
            _BerthRepository = new BerthRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            log = LogManager.GetLogger(typeof(TptDocumentUploadService));
        }

        public List<TerminalDelaysVO> TerminalDelaysDataDisplay(string path, string TemplateDoc)
        {
            log.Info("TerminalDelayDataDisplay method is called");

            //=================================
            try
            {
                DataRowCollection drc1;
                DataRowCollection drc = ValidateUploadDocumentSchema(path, TemplateDoc);

                DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
                DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;
                for (int i = 0; i < dtTemplate.Columns.Count; i++)
                {
                    dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
                }
               // drc1 = TerminalDelayDataValid(dtTemplatecolumns, drc);
                //================================== 

                List<TerminalDelaysVO> delayValues = new List<TerminalDelaysVO>();
                return ExecuteFaultHandledOperation(() =>
                {
                    DataTable dt1 = ReadExcelWithStream(path);
                    log.Info("TerminalDelayData is converting to VO method is started");
                    for (int i = 1; i < dt1.Rows.Count; i++)
                    {
                        TerminalDelaysVO tdvo = new TerminalDelaysVO();
                        tdvo.PortCode = _PortCode;
                        tdvo.PortName = dt1.Rows[i][0].ToString();
                        tdvo.TerminalOperator = dt1.Rows[i][1].ToString();
                        DateTime dtParam;
                        //System.Globalization.CultureInfo enGB = CultureInfo.CurrentCulture;  
                        log.Info("Before DateValue is converting to VO method");
                        try
                        {
                            //dtParam = DateTime.Parse(dt1.Rows[i][2].ToString());
                            dtParam = DateTime.ParseExact(dt1.Rows[i][2].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None);
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
                        //tdvo.RecordStatus = drc1[i][12].ToString();
                        delayValues.Add(tdvo);
                    }
                    return delayValues;
                });
            }
            catch (FormatException fx)
            {
                log.Error("Exception = ", fx);
                throw new BusinessExceptions(fx.Message);
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                //throw new FaultException(ex.Message);
                throw;// new Exception(ex.Message);
            }
        }

        private DataRowCollection ValidateUploadDocumentSchema(string path, string TemplateDoc)
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
            }
             drc = dtUpload.Rows;

                if (areEqual == !true)
            {
                log.Info("Schema mismatch with the uploaded document."); 
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

        public List<OutTurnVolumesVO> OutTurnVolumesDataDisplay(string path, string TemplateDoc)
        {
            log.Info("OutTurnVolumesDataDisplay method is called");
            try
            { 
           
            //=================================
           // DataRowCollection drc1;
            DataRowCollection drc1 = ValidateUploadDocumentSchema(path, TemplateDoc); 
            List<OutTurnVolumesVO> OutTurnVolumes = new List<OutTurnVolumesVO>();
            DataTable dtTemplate = ReadExcelWithStream(TemplateDoc); 
            DataColumnCollection dtTemplatecolumns = dtTemplate.Columns; 
            for (int i = 0; i < dtTemplate.Columns.Count; i++)
            { 
                    dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
            } 
           // drc1 = OutTurnsDataValid(dtTemplatecolumns, drc);
            //==================================  
            for (int i = 1; i < drc1.Count; i++)
            {
               
                OutTurnVolumesVO otVo = new OutTurnVolumesVO();
                otVo.PortCode = _PortCode;
                otVo.PortName = drc1[i][0].ToString();
                otVo.TerminalOperator = drc1[i][1].ToString();
                DateTime dtParam2;
                System.Globalization.CultureInfo enGB1 = new System.Globalization.CultureInfo("en-GB");
                if (!string.IsNullOrEmpty(drc1[i][2].ToString()))
               // if (drc1[i][2].ToString() != "")
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

                if (!string.IsNullOrEmpty(drc1[i][8].ToString()))
                  //  if (drc1[i][8].ToString() != "")
                {
                    otVo.OutTurnVolume = Convert.ToDecimal(drc1[i][8].ToString(), CultureInfo.InvariantCulture);
                }                
                otVo.UnitOfMeasure = drc1[i][9].ToString();
                DateTime dtParam, dtParam1;
                System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
              //  if (drc1[i][10].ToString() != "")
                    if (!string.IsNullOrEmpty(drc1[i][10].ToString()))
                {
                    dtParam = Convert.ToDateTime(drc1[i][10].ToString(), enGB);
                    otVo.FirstCraneSwing = Convert.ToDateTime(dtParam.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);
                }
                    if (!string.IsNullOrEmpty(drc1[i][11].ToString()))
               // if (drc1[i][11].ToString() != "")
                {
                    dtParam1 = Convert.ToDateTime(drc1[i][11].ToString(), enGB);
                    otVo.LastCraneSwing = Convert.ToDateTime(dtParam1.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);//Convert.ToDateTime(drc1[i][11].ToString());
                }
                    if (!string.IsNullOrEmpty(drc1[i][12].ToString()))
               // if (drc1[i][12].ToString() != "")
                {
                    otVo.NoOfCranes = Convert.ToInt16(drc1[i][12].ToString(), CultureInfo.InvariantCulture);
                }
                otVo.Comments = drc1[i][13].ToString();
                otVo.RecordStatus = drc1[i][14].ToString();
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
                    if (!string.IsNullOrEmpty(value.ToString())) 
                    {
                        if (value != null) //if (value.ToString() != "")
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public List<TerminalWeeklyDataVO> TerminalWeeklyDataDisplay(string path, string TemplateDoc)
        {
            log.Info("TerminalWeeklyDataDisplay method is called");
            //=================================
            try
            {

            //DataRowCollection drc1;
            DataRowCollection drc1 = ValidateUploadDocumentSchema(path, TemplateDoc);
            //List<OutTurnVolumesVO> OutTurnVolumes = new List<OutTurnVolumesVO>();
            DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
            DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;
            for (int i = 0; i < dtTemplate.Columns.Count; i++)
            {
                // columnNames[i] = dtTemplate.Rows[0][i].ToString();
                dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
            }
            //bool IsDataValid = OutTurnsDataValid(dtTemplatecolumns, drc);
            //drc1 = TerminalDataValid(dtTemplatecolumns, drc);
            //================================== 
            List<TerminalWeeklyDataVO> TerminalWeeklyData = new List<TerminalWeeklyDataVO>();
            return ExecuteFaultHandledOperation(() =>
            {
                DataTable dt1 = ReadExcelWithStream(path);
                for (int i = 1; i < dt1.Rows.Count; i++)
                {
                    TerminalWeeklyDataVO twdVo = new TerminalWeeklyDataVO();
                    twdVo.PortCode = _PortCode;
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
                    TerminalWeeklyData.Add(twdVo);
                }
                return TerminalWeeklyData;
            });

            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }

        ///<summary>
        ///Method to Read CSV Format
        ///</summary>
        ///<param name="path">Read File Full Path</param>
        ///<returns></returns>

        private DataTable ReadExcelWithStream(string path)
        {
            DataTable dt = new DataTable();
            bool isDtHasColumn = false;   //Mark if DataTable Generates Column
            StreamReader reader = new StreamReader(path, System.Text.Encoding.Default);
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
            reader.Close();
           // reader.Dispose();
            return dt;
        } 
       
        public static bool IsSchemaEqual(DataColumnCollection a, DataColumnCollection b)
        {
              
            //=================================
            try
            {

                if (a != null && b !=null)
                {
                    if (a.Count != b.Count)
                        return false;
                    for (int i = 0; i < a.Count; i++)
                    {
                        if (a[i].ColumnName != b[i].ColumnName && a[i].DataType != b[i].DataType)
                            return false;
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
        public string[][] TerminalDelayDataValid(string[] a, string[][] b)
        {
            log.Info("TerminalDelayDataValid method is called");
            try
            {
                return ExecuteFaultHandledOperation(() =>
                {
                    string UnitOfMeasure = "Hours";
                    SubCategoryRepository scr = new SubCategoryRepository(_unitOfWork);

                    List<SubCategoryCodeNameVO> DelayReasons = scr.GetReasonsForDelay();
                    string[] ReasonforDelay = DelayReasons.Select(DR => DR.SubCatName).ToArray();

                    List<TerminalOperatorVO> TerminalOperators = GetTerminalOperatorList();
                    string[] PortOperators = TerminalOperators.Select(TO => TO.RegisteredName).ToArray();

                    List<SubCategoryCodeNameVO> CargoTypes = GetCargoTypesList();
                    string[] Cargotypes = CargoTypes.Select(C => C.SubCatName).ToArray();

                    //List<SubCategory> UnitOfMeasures = GetUOMTypesList();
                    //string[] UnitOfMeasure = UnitOfMeasures.Select(UOM => UOM.SubCatName).ToArray();

                    List<BerthVO> Berths = _BerthRepository.GetBerthsDetails(_PortCode);
                    string[] BerthNames = Berths.Select(B => B.BerthName).ToArray();


                    List<VesselVO> lstVessels = GetVesselDtl();
                    string[] IMONumbers = lstVessels.Select(B => B.IMONo).ToArray();

                    string[] VesselNames = lstVessels.Select(B => B.VesselName).ToArray();


                    string ErrMsg = string.Empty;
                    //for (int colIndex = 0; colIndex < a.Length; colIndex++)
                    //{
                    for (int rowIndex = 1; rowIndex < b.Length; rowIndex++)
                    {
                        for (int colIndex = 0; colIndex < a.Length; colIndex++)
                        {

                            //if (a[colIndex].ToString().Trim().ToLower() == "terminal operator")
                            //{
                            //    var terminalsop = Array.FindAll(PortOperators, s => s.Trim().ToLower().Contains(b[rowIndex][colIndex].Trim().ToLower().ToString()));

                            //    //if (!PortOperators.ToString().ToLower().Trim().Contains(b[rowIndex][colIndex].ToString())) //Naren
                            //    if (terminalsop.Length == 0)
                            //    {
                            //        b[rowIndex][a.Length] = "E";
                            //        ErrMsg = ErrMsg + "- Terminals OP ";
                            //    }
                            //}
                            if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "terminal")
                            {
                                var terminals = Array.FindAll(BerthNames, s => s.Trim().ToUpperInvariant().Contains(b[rowIndex][colIndex].Trim().ToUpperInvariant().ToString()));

                                //if (!PortOperators.ToString().ToLower().Trim().Contains(b[rowIndex][colIndex].ToString())) //Naren
                                if (terminals.Length == 0)
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + "- Terminals ";
                                }

                            }

                            if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "arrival date")
                            {
                                if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_DateTime")
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + "- Arrival Date";
                                }
                                //service call to check the dates                       
                            }

                            if (a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "imo no.")
                            {
                                if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int32")
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - IMO No";
                                }
                                else
                                {
                                    var imonums = Array.FindAll(IMONumbers, s => s.Trim().ToUpperInvariant().Equals(b[rowIndex][colIndex].Trim().ToUpperInvariant().ToString()));
                                    if (imonums.Length == 0)
                                    {
                                        b[rowIndex][a.Length] = "E";
                                        ErrMsg = ErrMsg + " - IMO Not Exist";

                                    }
                                    else
                                    {
                                        //check for arrival dates against IMO No. 
                                        List<VesselCallVO> vaDetails = new List<VesselCallVO>();
                                        vaDetails = GetArrivalDetails(b[rowIndex][colIndex].ToString());
                                        if (vaDetails != null && vaDetails.Count == 1)
                                        {
                                            if (!(DateTime.Parse(b[rowIndex][2].ToString(), CultureInfo.InvariantCulture).Date >= DateTime.Parse(vaDetails[0].ATB.ToString(), CultureInfo.InvariantCulture).Date))
                                            {
                                                ErrMsg = ErrMsg + " - Data Mismatch on Arrival Date";
                                            }

                                            //vaDetails[0].ATB
                                        }
                                    }
                                }
                            }

                            if (a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "vessel name")
                            {
                                if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_String")
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Vessel Name";
                                }
                                else
                                {
                                    var vesselnames = Array.FindAll(VesselNames, s => s.Trim().ToUpperInvariant().Equals(b[rowIndex][colIndex].Trim().ToUpperInvariant().ToString()));
                                    if (vesselnames.Length == 0)
                                    {
                                        b[rowIndex][a.Length] = "E";
                                        ErrMsg = ErrMsg + " - Vessel Name Not Exist";

                                    }
                                }
                            }
                            //if (a[colIndex].ToString() == "Voyage")
                            //{
                            //    if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int64")
                            //    {
                            //        b[rowIndex][a.Length] = "E";
                            //        ErrMsg = ErrMsg + " - Voyage";
                            //    }
                            //}
                            if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "cargo type")
                            {
                                if (!Cargotypes.Contains(b[rowIndex][colIndex].ToString()))
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Cargo Type";
                                }
                                else
                                {
                                    var cargotypes = Array.FindAll(Cargotypes, s => s.Trim().ToUpperInvariant().Equals(b[rowIndex][colIndex].Trim().ToUpperInvariant().ToString()));
                                    if (cargotypes.Length == 0)
                                    {
                                        b[rowIndex][a.Length] = "E";
                                        ErrMsg = ErrMsg + " - Cargotypes Not Exist";

                                    }
                                }
                            }
                            if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "unit of measure")
                            {
                                if (b[rowIndex][colIndex].ToString() != UnitOfMeasure)
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Unit of Measure";
                                }

                            }
                            if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "reason for delay")
                            {
                                var reasonfordelay = Array.FindAll(ReasonforDelay, s => s.Trim().ToUpperInvariant().Contains(b[rowIndex][colIndex].Trim().ToUpperInvariant().ToString()));
                                if (reasonfordelay.Length == 0)
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Reason for Delay";
                                }

                            }

                            if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "delay duration")
                            {
                                if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int32")
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Reason for Delay";
                                }
                            }

                            //if (a[colIndex].ToString() == "Comments")
                            //{
                            //    if (b[rowIndex][colIndex].ToString().Length > 100)
                            //    {
                            //        b[rowIndex][a.Length] = "E";
                            //        ErrMsg = ErrMsg + " - Comments exceeding the Limit";
                            //    }
                            //}


                        }
                        b[rowIndex][a.Length + 1] = ErrMsg;
                        ErrMsg = "";
                    }
                    log.Info("TerminalDelayDataValid method execution is completed");
                    return b;
                });

            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        } 

        public string[][] TerminalDataValid(string[] a, string[][] b)
        { 
            log.Info("TerminalDataValid method is called");
            try
            { 
            return ExecuteFaultHandledOperation(() =>
            {
             string[] PerformanceArea = { "Vessels", "Productivity" };
             string[] MeasureValues = { "Vessels Handled", "Gross Crane Hours (GCH)", "ShipWorking Hours (SWH)", "Truck Turnaround Time", "Rail Turnaround Time", "Cargo Dwell Time" };
             
             List<TerminalOperatorVO> TerminalOperators = GetTerminalOperatorList();
             string[] PortOperators = TerminalOperators.Select(TO => TO.RegisteredName).ToArray();

             List<SubCategoryCodeNameVO> CargoTypes = GetCargoTypesList();
             string[] Cargotypes = CargoTypes.Select(C => C.SubCatName).ToArray();

             List<SubCategory> UnitOfMeasures = GetUOMTypesList();
             string[] UnitOfMeasure = UnitOfMeasures.Select(UOM => UOM.SubCatName).ToArray();

             List<BerthVO> Berths = _BerthRepository.GetBerthsDetails(_PortCode);
             string[] BerthNames = Berths.Select(B => B.BerthName.ToLower(CultureInfo.InvariantCulture)).ToArray();  

             string ErrMsg = "";

            int weekNo=0;
            string CargoType=string.Empty,PerformArea=string.Empty,Measure=string.Empty,UOMeasure=string.Empty;
                 
                 for (int rowIndex = 1; rowIndex < b.Length; rowIndex++)
                 {
                 //ErrMsg = "Data error in Column:";
                  for (int colIndex = 0; colIndex < a.Length; colIndex++)
                  {
                      if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "week no")
                     {
                         
                         if (!string.IsNullOrEmpty(b[rowIndex][colIndex].ToString()))  ///b[rowIndex][colIndex].ToString() != string.Empty && b[rowIndex][colIndex].ToString() != null)
                         {
                         if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int32")
                         {
                             b[rowIndex][a.Length] = "E";
                                 ErrMsg = ErrMsg + "- Week No";
                         }
                             else
	                        {
                                weekNo = Convert.ToInt32(b[rowIndex][colIndex].ToString(), CultureInfo.InvariantCulture);
                     }
                         }
                     }
                      if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "week ending")
                     {
                         if (!string.IsNullOrEmpty(b[rowIndex][colIndex].ToString())) 
                         //if (b[rowIndex][colIndex].ToString() != string.Empty && b[rowIndex][colIndex].ToString() != null)
                         {
                         if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_DateTime")
                         {
                             //b[rowIndex][a.Length] = "E";
                         }
                     }
                     }
                      if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "performance area")
                     {
                         if (!string.IsNullOrEmpty(b[rowIndex][colIndex].ToString())) 
                        // if (b[rowIndex][colIndex].ToString() != null && b[rowIndex][colIndex].ToString() != string.Empty)
                         {
                             if (!PerformanceArea.Contains(b[rowIndex][colIndex].ToString()))
                             {
                                 b[rowIndex][a.Length] = "E";
                                 ErrMsg = ErrMsg + "- Performance Area";
                             }
                             else
	                            {
                                 PerformArea=b[rowIndex][colIndex].ToString();
                         }
                     }
                     }

                      if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "measure")
                     {
                         if (!MeasureValues.Contains(b[rowIndex][colIndex].ToString()))
                         {
                             b[rowIndex][a.Length] = "E";
                             ErrMsg = ErrMsg + "- Measure";
                         }
                         else
	                        {
                             Measure=b[rowIndex][colIndex].ToString();
                     }
                     }
                      if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "cargo type")
                     {
                         if (!string.IsNullOrEmpty(b[rowIndex][colIndex].ToString())) 

                        // if (b[rowIndex][colIndex].ToString() != string.Empty && b[rowIndex][colIndex].ToString() != null)
                         {
                         if (!Cargotypes.Contains(b[rowIndex][colIndex].ToString()))
                         {
                             b[rowIndex][a.Length] = "E";
                                 ErrMsg = ErrMsg + "- Cargo Type";
                         }
                             else
	                            {
                                  CargoType=b[rowIndex][colIndex].ToString();
                                }
                         }
                     }
                      if (a[colIndex].ToString().Trim().Trim().ToLower(CultureInfo.InvariantCulture) == "unit of measure")
                     {
                         if (!string.IsNullOrEmpty(b[rowIndex][colIndex].ToString())) 
                         ///if (b[rowIndex][colIndex].ToString() != string.Empty && b[rowIndex][colIndex].ToString() != null)
                     {
                         if (!UnitOfMeasure.Contains(b[rowIndex][colIndex].ToString()))
                             {
                             b[rowIndex][a.Length] = "E";
                                 ErrMsg = ErrMsg + "- Unit of Measure";
                             }
                             else
	                         {
                                 UOMeasure=b[rowIndex][colIndex].ToString();
	                         }
                         }
                     }

                      if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "planned" || a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "actual")
                     {
                         //if (b[rowIndex][colIndex].ToString() != string.Empty && b[rowIndex][colIndex].ToString() != null)
                         if (!string.IsNullOrEmpty(b[rowIndex][colIndex].ToString())) 
                         {
                         if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int32")
                             {
                             b[rowIndex][a.Length] = "E";
                                 ErrMsg = ErrMsg + "- "+a[colIndex].ToString();
                             }
                     }
                     }
                     //if (a[colIndex].ToString() == "Comments")
                     //{
                     //    if (b[rowIndex][colIndex].ToString() != string.Empty && b[rowIndex][colIndex].ToString() != null)
                     //    {
                     //        if (b[rowIndex][colIndex].ToString().Length > 100)
                     //        {
                     //            b[rowIndex][a.Length] = "E";
                     //            ErrMsg = ErrMsg + "- Comments length exceeding the limit";
                     //        }

                     //    }
                     //}
                     //check for INput terminal name. 
                      if (a[colIndex].ToString().Trim().ToLower(CultureInfo.InvariantCulture) == "input terminal name")
                     {
                       //  if (b[rowIndex][colIndex].ToString() != string.Empty && b[rowIndex][colIndex].ToString() != null)
                         if (!string.IsNullOrEmpty(b[rowIndex][colIndex].ToString())) 
                         {
                             if (!BerthNames.Contains(b[rowIndex][colIndex].ToString().ToLower(CultureInfo.InvariantCulture)))
                             {                            
                                 b[rowIndex][a.Length] = "E";
                                 ErrMsg = ErrMsg + "- " + a[colIndex].ToString();
                             }
                         }
                     }
                          
                     }
                  int recCount = CheckDuplicateTerminalDataRecord(weekNo, PerformArea, Measure, UOMeasure, CargoType);
                  if (recCount != null && recCount >= 0)
                  {
                      b[rowIndex][a.Length] = "E";
                      ErrMsg = ErrMsg + "Similar Record exists in database";
                 }
                 b[rowIndex][a.Length+1] = ErrMsg;
                 ErrMsg ="";
             }
             return b;
         });

            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }
        public string[][] OutTurnsDataValid(string[] a, string[][] b)
        {
            log.Info("OutTurnsDataValid method is called");
            try
            {
                return ExecuteFaultHandledOperation(() =>
                {
                    List<TerminalOperatorVO> TerminalOperators = GetTerminalOperatorList();
                    string[] PortOperators = TerminalOperators.Select(TO => TO.RegisteredName).ToArray();

                    List<SubCategoryCodeNameVO> CargoTypes = GetCargoTypesList();
                    string[] Cargotypes = CargoTypes.Select(C => C.SubCatName).ToArray();

                    List<SubCategory> UnitOfMeasures = GetUOMTypesList();
                    string[] UnitOfMeasure = UnitOfMeasures.Select(UOM => UOM.SubCatName).ToArray();

                    List<BerthVO> Berths = _BerthRepository.GetBerthsDetails(_PortCode);
                    string[] BerthNames = Berths.Select(B => B.BerthName).ToArray();


                    List<VesselVO> lstVessels = GetVesselDtl();
                    string[] IMONumbers = lstVessels.Select(B => B.IMONo).ToArray();

                    string[] VesselNames = lstVessels.Select(B => B.VesselName).ToArray();
                    string ErrMsg = string.Empty;

                    //Type dtype = a[i].DataType;
                    for (int rowIndex = 1; rowIndex < b.Length; rowIndex++)
                    {
                        for (int colIndex = 0; colIndex < a.Length; colIndex++)
                        {
                            if (a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "terminal")
                            {
                                var terminals = Array.FindAll(BerthNames, s => s.Trim().ToUpperInvariant().Contains(b[rowIndex][colIndex].Trim().ToUpperInvariant().ToString()));
                                if (terminals.Length == 0)
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Terminal";
                                }

                            }
                            if (a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "arrival date")
                            {
                                if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_DateTime")
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Arrival Date";
                                }
                                //service call to check the dates                       
                            }

                            if (a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "imo no")
                            {
                                if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int32")
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - IMO No";
                                }
                                else
                                {
                                    var imonums = Array.FindAll(IMONumbers, s => s.Trim().ToUpperInvariant().Equals(b[rowIndex][colIndex].Trim().ToUpperInvariant().ToString()));
                                    if (imonums.Length == 0)
                                    {
                                        b[rowIndex][a.Length] = "E";
                                        ErrMsg = ErrMsg + " - IMO Not Exist";

                                    }
                                }
                            }

                            if (a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "vessel name")
                            {
                                if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_String")
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Vessel Name";
                                }
                                else
                                {
                                    var vesselnames = Array.FindAll(VesselNames, s => s.Trim().ToUpperInvariant().Equals(b[rowIndex][colIndex].Trim().ToUpperInvariant().ToString()));
                                    if (vesselnames.Length == 0)
                                    {
                                        b[rowIndex][a.Length] = "E";
                                        ErrMsg = ErrMsg + " - Vessel Name Not Exist";

                                    }
                                    else
                                    {
                                        //check for arrival dates against IMO No. 
                                        List<VesselCallVO> vaDetails = new List<VesselCallVO>();
                                        vaDetails = GetArrivalDetails(b[rowIndex][colIndex].ToString());
                                        if (vaDetails != null && vaDetails.Count == 1)
                                        {
                                            if (vaDetails[0].ATB != DateTime.Parse(b[rowIndex][2].ToString(), CultureInfo.InvariantCulture))
                                            {
                                                b[rowIndex][a.Length] = "E";
                                                ErrMsg = ErrMsg + " - Data Mismatch on Arrival Date";
                                            }
                                        }
                                    }
                                }
                            }

                            //if (a[colIndex].ToString() == "Voyage")
                            //{
                            //    if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int64")
                            //    {
                            //        b[rowIndex][a.Length] = "E";
                            //    }
                            //}
                            if (a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "cargo type")
                            {
                                if (!Cargotypes.Contains(b[rowIndex][colIndex].ToString()))
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Cargo Type";
                                }
                                else
                                {
                                    var cargotypes = Array.FindAll(Cargotypes, s => s.Trim().ToUpperInvariant().Equals(b[rowIndex][colIndex].Trim().ToUpperInvariant().ToString()));
                                    if (cargotypes.Length == 0)
                                    {
                                        b[rowIndex][a.Length] = "E";
                                        ErrMsg = ErrMsg + " - Cargotypes Not Exist";

                                    }
                                }
                            }

                            if (a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "unit of measure")
                            {
                                if (!UnitOfMeasure.Contains(b[rowIndex][colIndex].ToString()))
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Unit of Measure";
                                }
                            }

                            if (a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "out turn volume" || a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "no.of cranes")
                            {
                                if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int32")
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - Out turn Volume - no.of cranes";

                                }
                            }
                            if (a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "first crane swing" || a[colIndex].Trim().ToLower(CultureInfo.InvariantCulture).ToString() == "last crane swing")
                            {
                                if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_DateTime")
                                {
                                    b[rowIndex][a.Length] = "E";
                                    ErrMsg = ErrMsg + " - first crane swing - last crane swing";
                                }
                            }

                            //if (a[colIndex].ToString() == "Comments")
                            //{
                            //    if (b[rowIndex][colIndex].ToString().Length > 100)
                            //    {
                            //        b[rowIndex][a.Length] = "E";
                            //    }
                            //}

                        }

                        b[rowIndex][a.Length + 1] = ErrMsg;
                        ErrMsg = "";

                    }

                    return b;
                });

            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }


        public string[][] RailPlanValid(string[] a, string[][] b)
        {
            log.Info("RailPlanValid method is called");
            try
            { 
            return ExecuteFaultHandledOperation(() =>
            {
                    string[] ScheduleTypes = { "1", "2", "3", "4", "5", "" };
                    string[] BreakTypes = { "V", "A" };
                string[] YQValues = { "Y", "N" };
                    string[] TrainStatus = { "S", "A" };
                //string[] MeasureValues = { "Vessels Handled", "Gross Crane Hours (GCH)", "ShipWorking Hours (SWH)", "Truck Turnaround Time", "Rail Turnaround Time", "Cargo Dwell Time" };
                //string[] UnitOfMeasure = { "Crane moves/hour", "Units/hour", "Tons", "Kilolitres/hour", "Tons/hour", "Minutes", "Hours", "Days" };
                
                //schedule,origin,destination,break Type,ETD,ETA,Load,YQ,Train Status
                 

                List<SubCategoryCodeNameVO> CargoTypes = GetCargoTypesList();
                string[] Cargotypes = CargoTypes.Select(C => C.SubCatName).ToArray();  

                for (int colIndex = 0; colIndex < a.Length; colIndex++)
                {
                    for (int rowIndex = 1; rowIndex < b.Length; rowIndex++)
                    {
                        if (a[colIndex].ToString() == "Schedule")
                        {
                            if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int32" && !ScheduleTypes.Contains(b[rowIndex][colIndex].ToString()))
                            {
                                b[rowIndex][a.Length] = "E";
                            }
                        }
                        if (a[colIndex].ToString() == "Origin" || a[colIndex].ToString() == "Destination")
                        {
                            if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_DateTime")
                            {
                              //  b[rowIndex][a.Length] = "E";
                            }
                        }
                        if (a[colIndex].ToString() == "Break Type")
                        {
                            if (!BreakTypes.Contains(b[rowIndex][colIndex].ToString()))
                            {
                                b[rowIndex][a.Length] = "E";
                            }
                        }

                        if (a[colIndex].ToString() == "ETD" || a[colIndex].ToString() == "ETA")
                        {
                            string pattern = "\\d{1,2}:\\d{2}";
                            if (!Regex.IsMatch(b[rowIndex][colIndex].ToString(), pattern, RegexOptions.CultureInvariant))  
                            {
                                b[rowIndex][a.Length] = "E";
                            }
                        }
                        if (a[colIndex].ToString() == "Load")
                        {
                            if (!Cargotypes.Contains(b[rowIndex][colIndex].ToString()))
                            {
                                b[rowIndex][a.Length] = "E";
                            }
                        }
                        if (a[colIndex].ToString() == "Planned Tons")
                        {
                                //if (b[rowIndex][colIndex].ToString() != string.Empty && b[rowIndex][colIndex].ToString() != null)
                                    if (!string.IsNullOrEmpty(b[rowIndex][colIndex].ToString()))
                            {
                                if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int32")
                                    b[rowIndex][a.Length] = "E";
                            } 
                        }

                        if (a[colIndex].ToString() == "YQ")
                        {
                            if (!YQValues.Contains(b[rowIndex][colIndex].ToString()))
                                b[rowIndex][a.Length] = "E";
                        }
                        if (a[colIndex].ToString() == "Train Status")
                        {
                               // if (b[rowIndex][colIndex].ToString() != string.Empty && b[rowIndex][colIndex].ToString() != null)
                            if (!string.IsNullOrEmpty(b[rowIndex][colIndex].ToString()))
                            {
                                if (!TrainStatus.Contains(b[rowIndex][colIndex].ToString()))
                                b[rowIndex][a.Length] = "E";
                            } 
                        } 
                    }
                } 
                return b;
            });
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }
        public string[][] RailChangeNotificationsValid(string[] a, string[][] b)
        {
            log.Info("RailChangeNotificationsValid method is called");
            try
            { 
            return ExecuteFaultHandledOperation(() =>
            {
                    string[] ReasonForChange = { "0", "1", "2" };
                    string[] TrainStatus = { "S", "A" };
               
                //ReasonForChange,Train Status  

                for (int colIndex = 0; colIndex < a.Length; colIndex++)
                {
                    for (int rowIndex = 1; rowIndex < b.Length; rowIndex++)
                    {
                        if (a[colIndex].ToString() == "Reason For Change")
                        {
                            if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int32" && !ReasonForChange.Contains(b[rowIndex][colIndex].ToString()))
                            {
                                b[rowIndex][a.Length] = "E";
                            }
                        }
                        if (a[colIndex].ToString() == "Train Status")
                        {
                                //if (b[rowIndex][colIndex].ToString() != string.Empty && b[rowIndex][colIndex].ToString() != null)
                                    if (!string.IsNullOrEmpty(b[rowIndex][colIndex].ToString()))
                            {
                                if (!TrainStatus.Contains(b[rowIndex][colIndex].ToString()))
                                b[rowIndex][a.Length] = "E";
                            }
                            
                        } 
                    }
                } 
                return b;
            });
             }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }
        public string[][] ArrivalAndDepartureDataValid(string[] a, string[][] b)
        {
            log.Info("ArrivalAndDepartureDataValid method is called");
            try
            { 
            return ExecuteFaultHandledOperation(() =>
            {
                    string[] ReasonForChange = { "0", "1", "2" };
                    string[] TrainMovement = { "0", "1" };

                for (int colIndex = 0; colIndex < a.Length; colIndex++)
                {
                    for (int rowIndex = 1; rowIndex < b.Length; rowIndex++)
                    {
                        if (a[colIndex].ToString() == "Reason For Change")
                        {
                            if (ParseValue(b[rowIndex][colIndex].ToString()) != "System_Int32" && !ReasonForChange.Contains(b[rowIndex][colIndex].ToString()))
                            {
                                b[rowIndex][a.Length] = "E";
                            }
                        }
                        if (a[colIndex].ToString() == "Train Movement")
                        {
                            if (!TrainMovement.Contains(b[rowIndex][colIndex].ToString()))
                                b[rowIndex][a.Length] = "E";
                        } 
                    }
                } 
                return b;
            });
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }
        //======================== 
        public List<TerminalDelaysVO> InsertTerminalDelays(List<TerminalDelaysVO> TerminalDelaysData)
        {
            return EncloseTransactionAndHandleException(() =>
            { 
                return _TptDocUploadRepository.InsertTerminalDelays(TerminalDelaysData, _UserId);
            });
        }

        public List<OutTurnVolumesVO> InsertOutTurnVolumes(List<OutTurnVolumesVO> OutTurnVolumesData)
        {

            return EncloseTransactionAndHandleException(() =>
            { 
                return _TptDocUploadRepository.InsertOutTurnVolumes(OutTurnVolumesData, _UserId);
            });

        }

        public List<TerminalWeeklyDataVO> InsertTerminalData(List<TerminalWeeklyDataVO> TerminalWeeklyData)
        {
            return EncloseTransactionAndHandleException(() =>
            {

                return _TptDocUploadRepository.InsertTerminalData(TerminalWeeklyData, _UserId);
            });
        }

        public List<RailPlanVO> InsertRailPlan(List<RailPlanVO> RailPlanData)
        {
            return EncloseTransactionAndHandleException(() =>
            {

                return _TptDocUploadRepository.InsertRailPlan(RailPlanData, _UserId);
            });
        }

        public List<RailPlanVO> UpdateRailChangeNotifications(List<RailPlanVO> RailPlanData)
        {
            return EncloseTransactionAndHandleException(() =>
            {

                return _TptDocUploadRepository.UpdateRailChangeNotifications(RailPlanData, _UserId);
            });
        }

        public List<RailPlanVO> UpdateArrivalAndDepartureTimes(List<RailPlanVO> RailPlanData)
        {
            return EncloseTransactionAndHandleException(() =>
            {

                return _TptDocUploadRepository.UpdateArrivalAndDepartureTimes(RailPlanData, _UserId);
            });
        }

        //public List<TrainMonitoringVO> GetTrainMonitoringDetails(string plannedDate, string Corridor, string movementStatus)
        public List<TrainMonitoringVO> GetTrainMonitoringDetails(string FromDate, string ToDate)
        {
            log.Info("GetTrainMonitoringDetails method is called");
            try
            { 
            return ExecuteFaultHandledOperation(() =>
            {
                 DateTime dtParam;
                    System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                    //dtParam = Convert.ToDateTime(plannedDate, enGB);
                    List<TrainMonitoringVO> trainMonitoringData = new List<TrainMonitoringVO>();
                   // trainMonitoringData = _TptDocUploadRepository.GetTrainMonitoringDetails(plannedDate, Corridor, movementStatus);
                    trainMonitoringData = GetTrainDetailsFromTfrService(FromDate.ToString(), ToDate.ToString());
                    int i = 1;
                    foreach (var rail in trainMonitoringData)
                    {
                        rail.SlNo = i++;
                        //if (rail.ATA > rail.PlannedETA)
                        if (Convert.ToDateTime(rail.ATA, CultureInfo.InvariantCulture) > Convert.ToDateTime(rail.PlannedETA, CultureInfo.InvariantCulture))
                        {
                            rail.RecordStatus = "D";    
                        }
                        else if (rail.TrainStatus == "A" && rail.TrainMovement == 0)
                        {
                            rail.RecordStatus = "Dp";
                        }
                        else if ((rail.TrainStatus != "S" || rail.TrainStatus != "") && rail.TrainMovement == 1)
                        {
                            rail.RecordStatus = "Ar";
                        }
                    } 
                    return trainMonitoringData;
            });
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }

        //=============================================================
       



        /// <summary>
        /// Web service extraction method
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="Todate"></param>
        /// <returns></returns>

        public List<TrainMonitoringVO> GetTrainDetailsFromTfrService(string FromDate, string Todate)
        {
            List<TrainMonitoringVO> TrainMonitoringList = new List<TrainMonitoringVO>();
            #region TrainSummary
            TRAIN_SUMMARY myService = new TRAIN_SUMMARY();
            TrainSummary.getTrainSummary_wsd obj_trainSummary = new TrainSummary.getTrainSummary_wsd();
            TrainSummary.TRAIN_SUMMARY[] trainSummary = new TrainSummary.TRAIN_SUMMARY[] { };
            trainSummary = obj_trainSummary.getTrainSummary(Convert.ToDateTime(FromDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(Todate).ToString("dd/MM/yyyy"));

            //TrainSummary.getTrainSummary_wsd myService = new TrainSummary.getTrainSummary_wsd();
            //TrainSummary.TRAIN_SUMMARY[] trainSummary = new TrainSummary.TRAIN_SUMMARY[] { }; 

            //trainSummary = myService.getTrainSummary(FromDate, Todate);

            if (trainSummary.Count() > 0)
            {
                for (int i = 0; i < trainSummary.Count(); i++)
                {
                    TrainMonitoringVO TrainDetailVO = new TrainMonitoringVO();
                    TrainDetailVO.TrainNo = Convert.ToInt32(trainSummary[i].TRAIN_NUMBER.ToString(), CultureInfo.InvariantCulture);
                    TrainDetailVO.Origin = trainSummary[i].ORIGIN_DEPART_FROM_PLACE;
                    TrainDetailVO.Destination = trainSummary[i].DEPART_TO_PLACE;
                    TrainDetailVO.PlannedETD = trainSummary[i].SCHEDULED_DEPART_DATE_TIME;
                    if (!string.IsNullOrEmpty(trainSummary[i].SCHEDULED_ARRIVAL_DATE_TIME) && !string.IsNullOrEmpty(trainSummary[i].SCHEDULED_ARRIVAL_DATE_TIME))
                    //   if (trainSummary[i].SCHEDULED_ARRIVAL_DATE_TIME != null && trainSummary[i].SCHEDULED_ARRIVAL_DATE_TIME != string.Empty)
                    {
                        TrainDetailVO.PlannedETA = trainSummary[i].SCHEDULED_ARRIVAL_DATE_TIME;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].LOCO_QUANTITY) && !string.IsNullOrEmpty(trainSummary[i].LOCO_QUANTITY))
                    // if (trainSummary[i].LOCO_QUANTITY != null && trainSummary[i].LOCO_QUANTITY != string.Empty)
                    {
                        TrainDetailVO.LocoQty = Convert.ToInt32(trainSummary[i].LOCO_QUANTITY, CultureInfo.InvariantCulture);
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].PLANNED_TONNAGE) && !string.IsNullOrEmpty(trainSummary[i].PLANNED_TONNAGE))
                    //  if (trainSummary[i].PLANNED_TONNAGE != null && trainSummary[i].PLANNED_TONNAGE != string.Empty)
                    {
                        TrainDetailVO.PlannedTons = Convert.ToInt32(trainSummary[i].PLANNED_TONNAGE, CultureInfo.InvariantCulture);
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].LOAD) && !string.IsNullOrEmpty(trainSummary[i].LOAD))
                    //  if (trainSummary[i].LOAD != null && trainSummary[i].LOAD != string.Empty)
                    {
                        TrainDetailVO.Load = trainSummary[i].LOAD;
                    }

                    TrainDetailVO.TrainStatus = trainSummary[i].STATUS;
                    if (!string.IsNullOrEmpty(trainSummary[i].ACTUAL_DEPART_DATE_TIME) && !string.IsNullOrEmpty(trainSummary[i].ACTUAL_DEPART_DATE_TIME))
                    //if (trainSummary[i].ACTUAL_DEPART_DATE_TIME != null && trainSummary[i].ACTUAL_DEPART_DATE_TIME != string.Empty)
                    {
                        TrainDetailVO.ATD = trainSummary[i].ACTUAL_DEPART_DATE_TIME;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].ACTUAL_ARRIVE_DATE_TIME) && !string.IsNullOrEmpty(trainSummary[i].ACTUAL_ARRIVE_DATE_TIME))
                    //if (trainSummary[i].ACTUAL_ARRIVE_DATE_TIME != null && trainSummary[i].ACTUAL_ARRIVE_DATE_TIME != string.Empty)
                    {
                        TrainDetailVO.ATA = trainSummary[i].ACTUAL_ARRIVE_DATE_TIME;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].NWB_REF_NUMBER) && !string.IsNullOrEmpty(trainSummary[i].NWB_REF_NUMBER))
                    //  if (trainSummary[i].NWB_REF_NUMBER != null && trainSummary[i].NWB_REF_NUMBER != string.Empty)
                    {
                        TrainDetailVO.NWBRef = Convert.ToInt32(trainSummary[i].NWB_REF_NUMBER, CultureInfo.InvariantCulture);
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].REMARKS) && !string.IsNullOrEmpty(trainSummary[i].REMARKS))
                    //if (trainSummary[i].REMARKS != null && trainSummary[i].REMARKS != string.Empty)
                    {
                        TrainDetailVO.Remark = trainSummary[i].REMARKS;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].LOCO_TYPE) && !string.IsNullOrEmpty(trainSummary[i].LOCO_TYPE))
                    //if (trainSummary[i].LOCO_TYPE != null && trainSummary[i].LOCO_TYPE != string.Empty)
                    {
                        TrainDetailVO.Loco = trainSummary[i].LOCO_TYPE;
                    }
                    //if (trainSummary[i].SCHEDULE_TYPE != null && trainSummary[i].SCHEDULE_TYPE != string.Empty)
                    if (!string.IsNullOrEmpty(trainSummary[i].SCHEDULE_TYPE) && !string.IsNullOrEmpty(trainSummary[i].SCHEDULE_TYPE))
                    {
                        TrainDetailVO.Schedule = Convert.ToInt32(trainSummary[i].SCHEDULE_TYPE, CultureInfo.InstalledUICulture);
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].BUSINESS_UNIT) && !string.IsNullOrEmpty(trainSummary[i].BUSINESS_UNIT))
                    {
                        TrainDetailVO.BUSINESS_UNIT = trainSummary[i].BUSINESS_UNIT;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].CORRIDOR_NAME) && !string.IsNullOrEmpty(trainSummary[i].CORRIDOR_NAME))
                    {
                        TrainDetailVO.CORRIDOR_NAME = trainSummary[i].CORRIDOR_NAME;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].SECTION_DESC) && !string.IsNullOrEmpty(trainSummary[i].SECTION_DESC))
                    {
                        TrainDetailVO.SECTION_DESC = trainSummary[i].SECTION_DESC;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].PLAN_TYPE) && !string.IsNullOrEmpty(trainSummary[i].PLAN_TYPE))
                    {
                        TrainDetailVO.PLAN_TYPE = trainSummary[i].PLAN_TYPE;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].TRAIN_ROUTE) && !string.IsNullOrEmpty(trainSummary[i].TRAIN_ROUTE))
                    {
                        TrainDetailVO.TRAIN_ROUTE = trainSummary[i].TRAIN_ROUTE;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].CATEGORY_DESC) && !string.IsNullOrEmpty(trainSummary[i].CATEGORY_DESC))
                    {
                        TrainDetailVO.CATEGORY_DESC = trainSummary[i].CATEGORY_DESC;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].TRAIN_TYPE) && !string.IsNullOrEmpty(trainSummary[i].TRAIN_TYPE))
                    {
                        TrainDetailVO.TRAIN_TYPE = trainSummary[i].TRAIN_TYPE;
                    }

                    if (!string.IsNullOrEmpty(trainSummary[i].OUTBOUND_INBOUND_INDICATOR) && !string.IsNullOrEmpty(trainSummary[i].OUTBOUND_INBOUND_INDICATOR))
                    {
                        TrainDetailVO.OUTBOUND_INBOUND_INDICATOR = trainSummary[i].OUTBOUND_INBOUND_INDICATOR;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].TOTAL_WAGONS) && !string.IsNullOrEmpty(trainSummary[i].TOTAL_WAGONS))
                    {
                        TrainDetailVO.TOTAL_WAGONS = trainSummary[i].TOTAL_WAGONS;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].ACTUAL_TRAIN_MASS) && !string.IsNullOrEmpty(trainSummary[i].ACTUAL_TRAIN_MASS))
                    {
                        TrainDetailVO.ACTUAL_TRAIN_MASS = trainSummary[i].ACTUAL_TRAIN_MASS;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].DELAY_REASON) && !string.IsNullOrEmpty(trainSummary[i].DELAY_REASON))
                    {
                        TrainDetailVO.DELAY_REASON = trainSummary[i].DELAY_REASON;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].ETA) && !string.IsNullOrEmpty(trainSummary[i].ETA))
                    {
                        TrainDetailVO.ETA = trainSummary[i].ETA;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].CANCELLATION_REF_NUMBER) && !string.IsNullOrEmpty(trainSummary[i].CANCELLATION_REF_NUMBER))
                    {
                        TrainDetailVO.CANCELLATION_REF_NUMBER = trainSummary[i].CANCELLATION_REF_NUMBER;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].CANCELLATION_REASON) && !string.IsNullOrEmpty(trainSummary[i].CANCELLATION_REASON))
                    {
                        TrainDetailVO.CANCELLATION_REASON = trainSummary[i].CANCELLATION_REASON;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].STAGED_REF_NUMBER) && !string.IsNullOrEmpty(trainSummary[i].STAGED_REF_NUMBER))
                    {
                        TrainDetailVO.STAGED_REF_NUMBER = trainSummary[i].STAGED_REF_NUMBER;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].STAGED_REASON) && !string.IsNullOrEmpty(trainSummary[i].STAGED_REASON))
                    {
                        TrainDetailVO.STAGED_REASON = trainSummary[i].STAGED_REASON;
                    }
                    if (!string.IsNullOrEmpty(trainSummary[i].UPDATE_DATETIME) && !string.IsNullOrEmpty(trainSummary[i].UPDATE_DATETIME))
                    {
                        TrainDetailVO.UPDATE_DATETIME = trainSummary[i].UPDATE_DATETIME;
                    }
                    TrainMonitoringList.Add(TrainDetailVO);
                }
            }
            #endregion
            return TrainMonitoringList;
        }

        public List<WagonDetailsVO> GetWagonDetailsFromTfrService(int TrainNo, string Origin,DateTime OriginDate)
        {
           List<WagonDetailsVO> WagonDetailsList = new List<WagonDetailsVO>();

           #region Wagon Details
           string toDate;
           TfrService.getWagonDetails_wsd WagonDetailsService = new TfrService.getWagonDetails_wsd();
           TfrService.WAGON_DETAILS[] WagonDetails = new TfrService.WAGON_DETAILS[] { };
           Console.WriteLine("Enter To Date ");
           toDate = Console.ReadLine();
           string Odate = Convert.ToDateTime(OriginDate).ToString("DD/MM/yyyy");
           WagonDetails = WagonDetailsService.getWagonDetails(TrainNo.ToString(CultureInfo.InvariantCulture), Origin, Odate);
          // WagonDetails = WagonDetailsService.getWagonDetails("005950", "FYN", Odate);

           if (WagonDetails.Count() > 0)
           {
               for (int i = 0; i < WagonDetails.Count(); i++)
               {
                   WagonDetailsVO TrainDetailVO = new WagonDetailsVO();
                   if (!string.IsNullOrEmpty(WagonDetails[i].TRAIN_NO) && !string.IsNullOrEmpty(WagonDetails[i].TRAIN_NO))
                   //if (WagonDetails[i].TRAIN_NO != null && WagonDetails[i].TRAIN_NO != string.Empty)
                   {
                       TrainDetailVO.TrainNo = Convert.ToInt32(WagonDetails[i].TRAIN_NO, CultureInfo.InvariantCulture);
                   }
               //    if (WagonDetails[i].WAGON_NUMBER != null && WagonDetails[i].WAGON_NUMBER != string.Empty)
                       if (!string.IsNullOrEmpty(WagonDetails[i].WAGON_NUMBER) && !string.IsNullOrEmpty(WagonDetails[i].WAGON_NUMBER))
                   {
                       TrainDetailVO.WagonNumber = Convert.ToInt32(WagonDetails[i].WAGON_NUMBER, CultureInfo.InvariantCulture);
                   }
                   TrainDetailVO.WagonType = WagonDetails[i].WAGON_TYPE;
                   TrainDetailVO.Commodity = WagonDetails[i].COMMODITY;
                  // if (WagonDetails[i].TONNAGE != null && WagonDetails[i].TONNAGE != string.Empty)
                       if (!string.IsNullOrEmpty(WagonDetails[i].TONNAGE) && !string.IsNullOrEmpty(WagonDetails[i].TONNAGE))
                   {
                       TrainDetailVO.Tonnage = Convert.ToInt32(WagonDetails[i].TONNAGE, CultureInfo.InvariantCulture);
                   }
                   //if (WagonDetails[i].ORIGIN_DATE != null && WagonDetails[i].ORIGIN_DATE != string.Empty)
                       if (!string.IsNullOrEmpty(WagonDetails[i].ORIGIN_DATE) && !string.IsNullOrEmpty(WagonDetails[i].ORIGIN_DATE))
                   {
                       TrainDetailVO.OriginDate = Convert.ToDateTime(WagonDetails[i].ORIGIN_DATE, CultureInfo.InvariantCulture);
                   }
                   TrainDetailVO.TrainOrigin = WagonDetails[i].TRAIN_ORIGIN;
                   //if (WagonDetails[i].UPDATE_DATETIME != null && WagonDetails[i].UPDATE_DATETIME != string.Empty)
                       if (!string.IsNullOrEmpty(WagonDetails[i].UPDATE_DATETIME) && !string.IsNullOrEmpty(WagonDetails[i].UPDATE_DATETIME))
                   {
                       TrainDetailVO.UpatedateTime = Convert.ToDateTime(WagonDetails[i].UPDATE_DATETIME, CultureInfo.InvariantCulture);
                   }
                   WagonDetailsList.Add(TrainDetailVO);
               } 
           }
           #endregion 

            return WagonDetailsList;
        }

        //=============================================================

        public List<RailPlanVO> RailPlanDataDisplay(string path, string TemplateDoc)
        {
            log.Info("RailPlanDataDisplay method is called");
            try
            { 
            //=================================
           // DataRowCollection drc1;
            DataRowCollection drc1 = ValidateUploadDocumentSchema(path, TemplateDoc);
            //List<OutTurnVolumesVO> OutTurnVolumes = new List<OutTurnVolumesVO>();
            DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
            DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;
            for (int i = 0; i < dtTemplate.Columns.Count; i++)
            { 
                dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
            } 
            //drc1 = RailPlanValid(dtTemplatecolumns, drc);
            //================================== 
            List<RailPlanVO> RailPlanData = new List<RailPlanVO>();
            return ExecuteFaultHandledOperation(() =>
            {
                DataTable dt1 = ReadExcelWithStream(path);
                for (int i = 1; i < dt1.Rows.Count; i++)
                {
                    RailPlanVO rpVo = new RailPlanVO();
                    rpVo.PortCode = _PortCode;
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
            });
             }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }

        public List<RailPlanVO> RailChangeNotificationsDataDisplay(string path, string TemplateDoc)
        {
            log.Info("RailChangeNotificationsDataDisplay method is called");
            try
            { 

            //=================================
            //DataRowCollection drc1;
            DataRowCollection drc1 = ValidateUploadDocumentSchema(path, TemplateDoc);
            //List<OutTurnVolumesVO> OutTurnVolumes = new List<OutTurnVolumesVO>();
            DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
            DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;
            for (int i = 0; i < dtTemplate.Columns.Count; i++)
            {
                dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
            }
            //drc1 = RailChangeNotificationsValid(dtTemplatecolumns, drc);
            //================================== 
            List<RailPlanVO> RailChangeNotificationsData = new List<RailPlanVO>();
            return ExecuteFaultHandledOperation(() =>
            {
                DataTable dt1 = ReadExcelWithStream(path);
                for (int i = 1; i < dt1.Rows.Count; i++)
                {
                    RailPlanVO rpVo = new RailPlanVO();
                    rpVo.PortCode = _PortCode;
                    rpVo.PortName = dt1.Rows[i][0].ToString();
                    rpVo.Corridor = dt1.Rows[i][1].ToString();
                    rpVo.SlNo = i;
                    DateTime dtParam;
                    System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
                    dtParam = Convert.ToDateTime(dt1.Rows[i][2].ToString(), enGB);
                    rpVo.PlannedDate = Convert.ToDateTime(dtParam.GetDateTimeFormats('G', CultureInfo.InvariantCulture)[0], CultureInfo.InvariantCulture);
                    rpVo.TrainNo = Convert.ToInt32(dt1.Rows[i][3].ToString(), CultureInfo.InvariantCulture);
                    rpVo.ReasonForChange = dt1.Rows[i][4].ToString();
                    rpVo.TrainStatus = dt1.Rows[i][5].ToString();
                    rpVo.NewETD = dt1.Rows[i][6].ToString();
                    rpVo.NewETA = dt1.Rows[i][7].ToString(); 
                    rpVo.RecordStatus = drc1[i][8].ToString();
                    RailChangeNotificationsData.Add(rpVo);
                }
                return RailChangeNotificationsData;
            });
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }

        public List<RailPlanVO> ArrivalAndDepartureDataDisplay(string path, string TemplateDoc)
        {
            log.Info("ArrivalAndDepartureDataDisplay method is called");
            try
            { 
            //=================================
            //DataRowCollection drc1;
            DataRowCollection drc1 = ValidateUploadDocumentSchema(path, TemplateDoc);
            //List<OutTurnVolumesVO> OutTurnVolumes = new List<OutTurnVolumesVO>();
            DataTable dtTemplate = ReadExcelWithStream(TemplateDoc);
            DataColumnCollection dtTemplatecolumns = dtTemplate.Columns;
            for (int i = 0; i < dtTemplate.Columns.Count; i++)
            {
                dtTemplatecolumns[i].ColumnName = (dtTemplate.Rows[0][i].ToString());
            }
           // drc1 = ArrivalAndDepartureDataValid(dtTemplatecolumns, drc);
            //================================== 
            List<RailPlanVO> ArrivalAndDepartureData = new List<RailPlanVO>();
            return ExecuteFaultHandledOperation(() =>
            {
                DataTable dt1 = ReadExcelWithStream(path);
                for (int i = 1; i < dt1.Rows.Count; i++)
                {
                    RailPlanVO rpVo = new RailPlanVO();
                    rpVo.PortCode = _PortCode;
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
                    rpVo.RecordStatus = drc1[i][6].ToString();
                    ArrivalAndDepartureData.Add(rpVo);
                }
                return ArrivalAndDepartureData;
            });
             }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }

        #region GetTerminalOperatorList
        /// <summary>
        /// To get Terminal Operator List
        /// </summary>
        /// <returns></returns>
        public List<TerminalOperatorVO> GetTerminalOperatorList()
        { 
            return _terminalOperatorRepository.GetTerminalOperatorList(_PortCode); 
        }
        #endregion

        #region GetCargoTypesList
        /// <summary>
        /// To get Terminal Operator List
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryCodeNameVO> GetCargoTypesList()
        { 
            return _SubCategoryRepository.GetCargoTypes(); 
        }
        #endregion

        #region GetUOMTypesList
        /// <summary>
        /// To get Terminal Operator List
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetUOMTypesList()
        {  
            return _SubCategoryRepository.GetTptUOMTypes(); 
        }
        #endregion

        //select TP.Portcode,Tor.TerminalOperatorID,RegisteredName from TerminalOperator TOr,TerminalOperatorPort TP where TP.PortCode='CT' 
 
        /// <summary>
        /// To get Arrival date details with reference to IMO No.
        /// </summary>
        /// <returns></returns>
        public List<VesselCallVO> GetArrivalDetails(string IMONo)
        {
            log.Debug("GetArrivalDetails method is called");
            try
            { 
            return ExecuteFaultHandledOperation(() =>
            {
                _unitOfWork = new UnitOfWork(new TnpaContext());

                var vesselDetails = (from t in _unitOfWork.Repository<VesselCall>().Query()
                                   .Include(t => t.ArrivalNotification.Vessel)
                                    .Select()
                                     select t
                                    ).Where(t => t.ArrivalNotification.Vessel.IMONo == IMONo).ToList();
                
                return vesselDetails.MapToDto(); 
                //Select v.VesselID,VesselName,IMONo,an.VCN,ATB,ATUB  from VesselCall vc 
                //inner join ArrivalNotification an on vc.VCN = an.VCN
                //inner join Vessel v on v.VesselID = an.VesselID
                //where v.IMONo='9592836' 
            });
            }
            catch (Exception ex)
            {
                log.Error("Exception = ", ex);
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// GET IMO Number from vessel
        /// </summary>
        /// <returns></returns>
        public List<VesselVO> GetVesselDtl()
        {
            var IMONumbers = (from p in _unitOfWork.Repository<Vessel>().Query().Select().Where(t => t.RecordStatus == "A")
                              select new VesselVO
                              {
                                  IMONo = p.IMONo,
                                  VesselName = p.VesselName

                              });
            return IMONumbers.ToList();
        }

        public int CheckDuplicateTerminalDataRecord(int weekNo,string PerformArea,string measure,string UoM,string cargoType)
        {  
                return _TptDocUploadRepository.CheckDuplicateTerminalDataRecord(weekNo, PerformArea,measure,UoM,cargoType);  
        } 

    }
}

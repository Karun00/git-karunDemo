using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using IPMS.Repository;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using IPMS.Services.WorkFlow;
using IPMS.Domain;
using IPMS.Services.LloydsSOAPForVesselRegistration;
using System.IO;
using System.Xml.Linq;
using System.Globalization;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                ConcurrencyMode = ConcurrencyMode.Multiple)]

    public class VesselRegistrationService : ServiceBase, IVesselRegistrationService
    {        
        private IVesselRegistrationRepository _vesselRegRepository;
        private IAccountRepository _accountRepository;
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;
        private ISubCategoryRepository _subCategoryRepository;
        
        public VesselRegistrationService()
        {            
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _vesselRegRepository = new VesselRegistrationRepository(_unitOfWork);
            _accountRepository = new AccountRepository(_unitOfWork);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _subCategoryRepository = new SubCategoryRepository(_unitOfWork);         
        }

        public VesselRegistrationService(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _subCategoryRepository = new SubCategoryRepository(_unitOfWork);

        }

        /// <summary>
        /// To get vessel registration reference data
        /// </summary>
        /// <returns></returns>
        public VesselRegistrationReferenceVO GetVesselRegistrationReferenceData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                VesselRegistrationReferenceVO vo = new VesselRegistrationReferenceVO();

                vo.GetClassificationSocity = _subCategoryRepository.ClassificationSocity().MapToDto();
                vo.GetVesselTypes = _subCategoryRepository.VesselTypes().MapToDto();                
                vo.GetPOR = _vesselRegRepository.GetPortRegistry();
                vo.GetVesselNationality = _subCategoryRepository.VesselNationality().MapToDto();
                vo.GetEngineTypes = _subCategoryRepository.EngineType().MapToDto();
                vo.GetPropulsionTypes = _subCategoryRepository.PropulsionType().MapToDto();
                vo.GetCertificateNames = _subCategoryRepository.VesselCertificateNames().MapToDto();
                vo.GetSafeWorkingLoad = _subCategoryRepository.VesselSafeWorkingLoad().MapToDto();

                return vo;
            });
        }
        /// <summary>
        /// To get vessel registration details
        /// </summary>
        /// <returns></returns>
        public List<VesselVO> GetVesselRegistrationData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _vesselRegRepository.GetVesselRegistrationData(_PortCode, _UserId);
            });
        }

        public List<VesselVO> GetSearchVesselData(string imoNo, string vesselName, string portOfRegistry, string vesselNationality, string vesselType, string callSign)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _vesselRegRepository.GetSearchVesselData(imoNo, vesselName, portOfRegistry, vesselNationality, vesselType, callSign, _PortCode, _UserId);
            });
        }       


        /// <summary>
        /// To get vessel registration data from pending tasks
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        public List<VesselVO> GetzVesselRegistrationData(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _vesselRegRepository.GetzVesselRegistrationData(vcn).MapToDTO();
            });
        }



        /// <summary>
        /// To add vessel registration data
        /// </summary>
        /// <param name="vesselData"></param>
        /// <returns></returns>
        public VesselVO AddVesselRegistrationDetails(VesselVO vesselData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                Vessel ve = null;
                ve = vesselData.MapToEntity();
                if (vesselData.VesselID > 0)
                    ve.ObjectState = ObjectState.Modified;
                else
                    ve.ObjectState = ObjectState.Added;

                int userid = _accountRepository.GetUserId(_LoginName);
                ve.CreatedBy = userid;
                ve.CreatedDate = DateTime.Now;
                ve.ModifiedBy = userid;
                ve.ModifiedDate = DateTime.Now;
                ve.SubmissionDate = ve.CreatedDate;               


                List<VesselEngine> vesselenginelist = ve.VesselEngines.ToList();
                if (vesselenginelist.Count() > 0)
                {
                    foreach (var vesseleng in vesselenginelist)
                    {
                        vesseleng.VesselID = ve.VesselID;
                        vesseleng.CreatedBy = userid;
                        vesseleng.CreatedDate = ve.CreatedDate;
                        vesseleng.ModifiedBy = userid;
                        vesseleng.ModifiedDate = DateTime.Now;
                        vesseleng.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<VesselEngine>().InsertRange(vesselenginelist);
                }

                List<VesselCertificateDetail> vesselCertificateList = ve.VesselCertificateDetails.ToList();
                if (vesselCertificateList.Count() > 0)
                {
                    foreach (var vesselcert in vesselCertificateList)
                    {
                        vesselcert.VesselID = ve.VesselID;
                        vesselcert.CreatedBy = userid;
                        vesselcert.CreatedDate = ve.CreatedDate;
                        vesselcert.ModifiedBy = userid;
                        vesselcert.ModifiedDate = DateTime.Now;

                    }
                    _unitOfWork.Repository<VesselCertificateDetail>().InsertRange(vesselCertificateList);
                }

                List<VesselHatchHold> vesselHatchHoldList = ve.VesselHatchHolds.ToList();
                if (vesselHatchHoldList.Count() > 0)
                {
                    foreach (var vesselhatch in vesselHatchHoldList)
                    {
                        vesselhatch.VesselID = ve.VesselID;

                        vesselhatch.CreatedBy = userid;
                        vesselhatch.CreatedDate = ve.CreatedDate;
                        vesselhatch.ModifiedBy = userid;
                        vesselhatch.ModifiedDate = DateTime.Now;
                        vesselhatch.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<VesselHatchHold>().InsertRange(vesselHatchHoldList);
                }

                List<VesselGrab> VesselGrabList = ve.VesselGrabs.ToList();
                if (VesselGrabList.Count() > 0)
                {
                    foreach (var vesselGrab in VesselGrabList)
                    {
                        vesselGrab.VesselID = ve.VesselID;
                        vesselGrab.CreatedBy = userid;

                        vesselGrab.CreatedDate = ve.CreatedDate;
                        vesselGrab.ModifiedBy = userid;
                        vesselGrab.ModifiedDate = DateTime.Now;
                        vesselGrab.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<VesselGrab>().InsertRange(VesselGrabList);
                }


                List<VesselGear> VesselGearList = ve.VesselGears.ToList();
                if (VesselGearList.Count() > 0)
                {
                    foreach (var vesselGear in VesselGearList)
                    {
                        vesselGear.VesselID = ve.VesselID;
                        vesselGear.CreatedBy = userid;

                        vesselGear.CreatedDate = ve.CreatedDate;
                        vesselGear.ModifiedBy = userid;
                        vesselGear.ModifiedDate = DateTime.Now;
                        vesselGear.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<VesselGear>().InsertRange(VesselGearList);
                }
                _unitOfWork.Repository<Vessel>().Insert(ve);
                #region Vessel Registration Workflow Integration
                string remarks = "Vessel Registration";              
                VesselRegistrationWorkFlow vesselRegistrationWorkFlow = new VesselRegistrationWorkFlow(_unitOfWork, ve, remarks);
                WorkFlowEngine<VesselRegistrationWorkFlow> wf = new WorkFlowEngine<VesselRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(vesselRegistrationWorkFlow, _portGeneralConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.WorkflowInitialStatus));

                #endregion               

                return vesselData;

            });
        }

        /// <summary>
        /// To modify registration data
        /// </summary>
        /// <param name="vesselData"></param>
        /// <returns></returns>
        public VesselVO ModifyVesselRegistrationDetails(VesselVO vesselData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                Vessel ve = null;
                ve = vesselData.MapToEntity();

                ve.ObjectState = ObjectState.Modified;


                int userid = _accountRepository.GetUserId(_LoginName);
                ve.CreatedBy = userid;
                ve.CreatedDate = DateTime.Now;
                ve.ModifiedBy = userid;
                ve.ModifiedDate = DateTime.Now;
                ve.SubmissionDate = ve.ModifiedDate;               

                List<VesselEngine> vesselenginelist = ve.VesselEngines.ToList();

                var delvesseleng = _unitOfWork.ExecuteSqlCommand(" delete dbo.VesselEngine where VesselID = @p0", vesselData.VesselID);

                if (vesselenginelist.Count() > 0)
                {
                    foreach (var vesseleng in vesselenginelist)
                    {

                        vesseleng.VesselID = ve.VesselID;
                        vesseleng.CreatedBy = userid;
                        vesseleng.CreatedDate = ve.CreatedDate;
                        vesseleng.ModifiedBy = userid;
                        vesseleng.ModifiedDate = DateTime.Now;
                        vesseleng.RecordStatus = "A";


                    }
                    _unitOfWork.Repository<VesselEngine>().InsertRange(vesselenginelist);
                }

                List<VesselCertificateDetail> vesselCertificateList = ve.VesselCertificateDetails.ToList();

                var delvesselcert = _unitOfWork.ExecuteSqlCommand(" delete dbo.VesselCertificateDetails where VesselID = @p0", vesselData.VesselID);

                if (vesselCertificateList.Count() > 0)
                {
                    foreach (var vesselcert in vesselCertificateList)
                    {
                        if (!string.IsNullOrEmpty(vesselcert.CertificateName))
                        {
                            vesselcert.VesselID = ve.VesselID;
                            vesselcert.CreatedBy = userid;
                            vesselcert.CreatedDate = ve.CreatedDate;
                            vesselcert.ModifiedBy = userid;
                            vesselcert.ModifiedDate = DateTime.Now;
                        }
                    }
                    _unitOfWork.Repository<VesselCertificateDetail>().InsertRange(vesselCertificateList);
                }

                List<VesselHatchHold> vesselHatchHoldList = ve.VesselHatchHolds.ToList();

                var delvesselhatch = _unitOfWork.ExecuteSqlCommand(" delete dbo.VesselHatchHold where VesselID = @p0", vesselData.VesselID);

                if (vesselHatchHoldList.Count() > 0)
                {
                    foreach (var vesselhatch in vesselHatchHoldList)
                    {
                        vesselhatch.VesselID = ve.VesselID;

                        vesselhatch.CreatedBy = userid;
                        vesselhatch.CreatedDate = ve.CreatedDate;
                        vesselhatch.ModifiedBy = userid;
                        vesselhatch.ModifiedDate = DateTime.Now;
                        vesselhatch.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<VesselHatchHold>().InsertRange(vesselHatchHoldList);
                }

                List<VesselGrab> VesselGrabList = ve.VesselGrabs.ToList();

                var delvesselgrab = _unitOfWork.ExecuteSqlCommand(" delete dbo.VesselGrab where VesselID = @p0", vesselData.VesselID);

                if (VesselGrabList.Count() > 0)
                {
                    foreach (var vesselGrab in VesselGrabList)
                    {
                        vesselGrab.VesselID = ve.VesselID;
                        vesselGrab.CreatedBy = userid;

                        vesselGrab.CreatedDate = ve.CreatedDate;
                        vesselGrab.ModifiedBy = userid;
                        vesselGrab.ModifiedDate = DateTime.Now;
                        vesselGrab.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<VesselGrab>().InsertRange(VesselGrabList);
                }


                List<VesselGear> VesselGearList = ve.VesselGears.ToList();

                var delvesselgear = _unitOfWork.ExecuteSqlCommand(" delete dbo.VesselGear where VesselID = @p0", vesselData.VesselID);

                if (VesselGearList.Count() > 0)
                {
                    foreach (var vesselGear in VesselGearList)
                    {
                        vesselGear.VesselID = ve.VesselID;
                        vesselGear.CreatedBy = userid;

                        vesselGear.CreatedDate = ve.CreatedDate;
                        vesselGear.ModifiedBy = userid;
                        vesselGear.ModifiedDate = DateTime.Now;
                        vesselGear.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<VesselGear>().InsertRange(VesselGearList);
                }

                _unitOfWork.Repository<Vessel>().Update(ve);

                if (ve.IsFinal == "N")
                {
                    #region Vessel Registration Workflow Integration
                    string remarks = "Update Vessel Registration";

                    VesselRegistrationWorkFlow vesselRegistrationWorkFlow = new VesselRegistrationWorkFlow(_unitOfWork, ve, remarks);
                    WorkFlowEngine<VesselRegistrationWorkFlow> wf = new WorkFlowEngine<VesselRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                    wf.Process(vesselRegistrationWorkFlow, _portGeneralConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.WorkflowInitialStatus));

                    #endregion
                }
                else
                {
                    _unitOfWork.SaveChanges();
                }
                             

                return vesselData;

            });
        }

        /// <summary>
        /// To verify registration of vessel 
        /// </summary>
        /// <param name="imoNum"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void VerifyVesselRegistration(string imoNum, string remarks, string taskCode)
        { 
            EncloseTransactionAndHandleException(() =>
            {
                var andata = (from t in _unitOfWork.Repository<Vessel>().Query().Select()                              
                              where t.IMONo == imoNum
                              select t).FirstOrDefault<Vessel>();
                
                andata.ModifiedBy = _UserId;
                andata.ModifiedDate = DateTime.Now;
                VesselRegistrationWorkFlow vesselRegistrationWorkFlow = new VesselRegistrationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<VesselRegistrationWorkFlow> wf = new WorkFlowEngine<VesselRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(vesselRegistrationWorkFlow, taskCode);

            });
        }
        /// <summary>
        /// To approve registration of vessel 
        /// </summary>
        /// <param name="imoNum"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void ApproveVesselRegistration(string imoNum, string remarks, string taskCode)
        {          
            EncloseTransactionAndHandleException(() =>
            {
                var andata = (from t in _unitOfWork.Repository<Vessel>().Query().Select()                              
                              where t.IMONo == imoNum
                              select t).FirstOrDefault<Vessel>();

                andata.ModifiedBy = _UserId;
                andata.ModifiedDate = DateTime.Now;
                andata.IsFinal = "Y";
                if (string.IsNullOrEmpty(remarks))
                { remarks = "Vessel Registration"; }

                VesselRegistrationWorkFlow vesselRegistrationWorkFlow = new VesselRegistrationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<VesselRegistrationWorkFlow> wf = new WorkFlowEngine<VesselRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(vesselRegistrationWorkFlow, taskCode);

            });
        }


        /// <summary>
        /// To reject registration of vessel 
        /// </summary>
        /// <param name="imoNum"></param>
        /// <param name="remarks"></param>
        /// <param name="taskCode"></param>
        public void RejectVesselRegistration(string imoNum, string remarks, string taskCode)
        {
            EncloseTransactionAndHandleException(() =>
            {
                var andata = (from t in _unitOfWork.Repository<Vessel>().Query().Select()                              
                              where t.IMONo == imoNum
                              select t).FirstOrDefault<Vessel>();

                andata.ModifiedBy = _UserId;
                andata.ModifiedDate = DateTime.Now;
                VesselRegistrationWorkFlow vesselRegistrationWorkFlow = new VesselRegistrationWorkFlow(_unitOfWork, andata, remarks);
                WorkFlowEngine<VesselRegistrationWorkFlow> wf = new WorkFlowEngine<VesselRegistrationWorkFlow>(_unitOfWork, _PortCode, _UserId);
                wf.Process(vesselRegistrationWorkFlow, taskCode);

            });
        }

        public int CheckIMOExists(string imo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var andata = (from t in _unitOfWork.Repository<Vessel>().Query().Select()
                              where t.IMONo == imo
                              select t);
                return andata.Count();
            });
        }

        public VesselVO GetVesselDetailsFromService(string imo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                VesselVO vslVO = new VesselVO();

                SidDBRequestService obj1 = new SidDBRequestService();
                string str = obj1.request_data("transnet", "NJUC4P0I3KT88", "LLIDS82_VESSEL_REQUEST", "" + imo + "", "IMO", "?", "?", "?", "?", "?", "?", "?", "?");
                TextReader tr = new StringReader(str);
                //TextReader tr = new StringReader("<Vessel><VesselID>8697158</VesselID><IMO>9527037</IMO><VslName>MaerskLondrina</VslName><VslType>fullycellularcontainership</VslType><Status>Live</Status><MMSI>477334300</MMSI><Gross>89505</Gross><LastUpdate>2015-02-23T12:18:19</LastUpdate><Characteristics><YOB>2012</YOB><Flag>HKG</Flag><CallSign>VRLB7</CallSign><PortReg><TownID>80</TownID><Name>HongKong</Name><Country>HongKong</Country></PortReg><Net>45087</Net><DWT>99778</DWT><GenType>U</GenType><SubType>CC</SubType><Class1>AB</Class1><HullDetails><HullID>8194903</HullID><Dimensions><BreadthMoulded>45.2</BreadthMoulded><Depth>24.2</Depth><Draft>12.5</Draft><Freeboard>10700</Freeboard><LBP>286.8</LBP><LOA>299.9</LOA><LRG>286.8</LRG><FormulaDWT>138486</FormulaDWT></Dimensions><Superstructure><BulbousBow>Y</BulbousBow><Material>S</Material><Decks>1</Decks><FuelConsumption>23kts(L)</FuelConsumption><PropulsionType>MO</PropulsionType><Speed>23</Speed><ReeferPlugs>1700</ReeferPlugs></Superstructure><Machinery><Propellers><PropellerRecord><PropID>6715303</PropID><No>1</No><Type>FP</Type></PropellerRecord></Propellers></Machinery></HullDetails><Engines><EngineRecord><EngineID>8106092</EngineID><Designation><Designer><CompanyID>170119</CompanyID><CompanyName>UnknownEngineDesigner</CompanyName><HeadOffice><OfficeRecord><OfficeID>133981</OfficeID><TownCity>Unknown</TownCity><Country>Unknown</Country><LastUpdate>2005-01-11T00:00:00</LastUpdate></OfficeRecord></HeadOffice><LastUpdate>2005-04-29T00:00:00</LastUpdate></Designer></Designation><Start>2008-07-01</Start><DOB>2008-01-01</DOB><DOBQ>A</DOBQ><EngineNo>1</EngineNo><RPM>78</RPM><LastUpdate>2013-09-25T03:22:27</LastUpdate></EngineRecord></Engines></Characteristics><History><YardNo>4226</YardNo><Builder><CompanyID>82136</CompanyID><CompanyName>DaewooShipbuilding&amp;MarineEngineeringCompanyLimited</CompanyName><HeadOffice><OfficeRecord><OfficeID>41875</OfficeID><Line1>OkpoShipyard</Line1><Line2>1Ajoo-dong</Line2><TownCity>GoejeIs.</TownCity><Country>SouthKorea</Country><Telephone>82556802114</Telephone><Fax>82556802125</Fax><Email>tjhwang@dsme.co.kr</Email><Website>http://www.dsme.co.kr</Website><LastUpdate>2014-12-12T14:52:36</LastUpdate></OfficeRecord></HeadOffice><LastUpdate>2013-01-16T09:40:20</LastUpdate></Builder> <OrderDate>2008-07-01</OrderDate> <OrderQ>B</OrderQ> <OrderValue>153</OrderValue> <SchedConsEnd>2012-10-01</SchedConsEnd> <SchedConsEndQ>M</SchedConsEndQ> <OriSchedConsEnd>2012-04-01</OriSchedConsEnd> <OriSchedConsEndQ>M</OriSchedConsEndQ> <ConsStart>2012-04-24</ConsStart> <Launch>2012-06-30</Launch> <ConsEnd>2012-10-09</ConsEnd> <ConsEndQ>B</ConsEndQ> <NameHistory> <NameRecord> <NameID>8375286</NameID> <Name>YardNo.4226Daewoo</Name> <Start>2008-07-01</Start> <StartQ>B</StartQ> <End>2012-01-01</End> <EndQ>B</EndQ> <LastUpdate>2014-06-20T15:56:03</LastUpdate> </NameRecord> <NameRecord> <NameID>10744265</NameID> <Name>MaerskLondrina</Name> <Current>Y</Current> <Start>2012-01-02</Start> <StartQ>B</StartQ> <LastUpdate>2012-05-30T09:25:02</LastUpdate> </NameRecord> </NameHistory> <FlagHistory> <FlagRecord> <FlagID>8301379</FlagID> <Flag>DIS</Flag> <Start>2008-07-01</Start> <StartQ>B</StartQ> <End>2012-08-22</End> <EndQ>B</EndQ> <LastUpdate>2012-08-24T08:41:18</LastUpdate> </FlagRecord> <FlagRecord> <FlagID>10919492</FlagID> <Flag>HKG</Flag> <Current>Y</Current> <Start>2012-08-23</Start> <StartQ>B</StartQ> <PortReg> <TownID>80</TownID> <Name>HongKong</Name> <Country>HongKong</Country> </PortReg> <MMSI>477334300</MMSI> <CallSign>VRLB7</CallSign> <LastUpdate>2012-08-24T08:41:18</LastUpdate> </FlagRecord> </FlagHistory> </History> <Ownership> <Current> <CurrentRecord> <RelationshipType>BO</RelationshipType> <Company> <CompanyID>10498</CompanyID> <CompanyName>A.P.Moller-MaerskGroup</CompanyName> <AllegianceCountry>Denmark</AllegianceCountry> <HeadOffice> <OfficeRecord> <OfficeID>4519</OfficeID> <Line1>50Esplanaden</Line1> <TownCity>Copenhagen</TownCity> <PostCode1>DK-1263</PostCode1> <PostCode2>K</PostCode2> <Country>Denmark</Country> <Telephone>4533633363</Telephone> <Fax>4533634108</Fax> <Email>cphinfo@maersk.com</Email> <Website>www.maersk.com</Website> <LastUpdate>2015-01-28T09:33:09</LastUpdate> </OfficeRecord> </HeadOffice> <LastUpdate>2014-02-04T11:13:08</LastUpdate> </Company> <Start>2012-10-09</Start> <StartQ>B</StartQ> </CurrentRecord> <CurrentRecord> <RelationshipType>CO</RelationshipType> <Company> <CompanyID>127123</CompanyID> <CompanyName>MaerskLineA/S</CompanyName> <AllegianceCountry>Denmark</AllegianceCountry> <HeadOffice> <OfficeRecord> <OfficeID>74968</OfficeID> <Line1>Esplanaden50</Line1> <TownCity>Copenhagen</TownCity> <PostCode1>DK-1263</PostCode1> <Country>Denmark</Country> <Telephone>4533633363</Telephone> <Fax>4533634108</Fax> <Email>rene.moller@maerskline.com</Email> <Website>www.maerskline.com</Website> <LastUpdate>2015-02-06T10:10:47</LastUpdate> </OfficeRecord> </HeadOffice> <LastUpdate>2015-02-06T10:10:35</LastUpdate> </Company> <Start>2012-10-09</Start> <StartQ>B</StartQ> </CurrentRecord> <CurrentRecord> <RelationshipType>RO</RelationshipType> <Company> <CompanyID>238986</CompanyID> <CompanyName>MaerskShippingHongKongLimited</CompanyName> <AllegianceCountry>Denmark</AllegianceCountry> <HeadOffice> <OfficeRecord> <OfficeID>227954</OfficeID> <Line1>19Floor,OneKowloon</Line1> <Line2>1WangYuenStreet</Line2> <Line3>KowloonBay</Line3> <TownCity>HongKong</TownCity> <Country>HongKong</Country> <Telephone>85237652006</Telephone> <Fax>85229720656</Fax> <Email>hkgshipgen@maersk.com</Email> <Website>www.maerskship-hk.com</Website> <LastUpdate>2014-07-08T14:26:24</LastUpdate> </OfficeRecord> </HeadOffice> <LastUpdate>2014-10-16T16:22:00</LastUpdate> </Company> <Start>2012-10-09</Start> <StartQ>B</StartQ> </CurrentRecord> <CurrentRecord> <RelationshipType>TM</RelationshipType> <Company> <CompanyID>9531</CompanyID> <CompanyName>A.P.MollerSingaporePrivateLimited</CompanyName> <HeadOffice> <OfficeRecord> <OfficeID>170334</OfficeID> <Line1>3HarbourFrontPlace13-01</Line1> <Line2>HarbourFrontTower2</Line2> <TownCity>Singapore</TownCity> <PostCode1>099254</PostCode1> <Country>Singapore</Country> <Telephone>6563238323</Telephone> <Fax>6562237191</Fax> <Email>asishipprj@maersk.com</Email> <Website>www.apmsingapore.com.sg</Website> <LastUpdate>2014-11-03T11:23:08</LastUpdate> </OfficeRecord> </HeadOffice> <LastUpdate>2010-11-30T09:46:20</LastUpdate> </Company> <Start>2013-06-20</Start> <StartQ>B</StartQ> </CurrentRecord> <CurrentRecord> <RelationshipType>LO</RelationshipType> <Company> <CompanyID>127123</CompanyID> <CompanyName>MaerskLineA/S</CompanyName> <AllegianceCountry>Denmark</AllegianceCountry> <HeadOffice> <OfficeRecord> <OfficeID>74968</OfficeID> <Line1>Esplanaden50</Line1> <TownCity>Copenhagen</TownCity> <PostCode1>DK-1263</PostCode1> <Country>Denmark</Country> <Telephone>4533633363</Telephone> <Fax>4533634108</Fax> <Email>rene.moller@maerskline.com</Email> <Website>www.maerskline.com</Website> <LastUpdate>2015-02-06T10:10:47</LastUpdate> </OfficeRecord> </HeadOffice> <LastUpdate>2015-02-06T10:10:35</LastUpdate> </Company> <Start>2011-04-16</Start> <StartQ>A</StartQ> </CurrentRecord> </Current> </Ownership> </Vessel>");

                //TextReader tr = new StreamReader("<Vessel><VesselID>8697158</VesselID><IMO>9527037</IMO><VslName>Maersk Londrina</VslName><VslType>fully cellular containership</VslType><Status>Live</Status><MMSI>477334300</MMSI><Gross>89505</Gross><LastUpdate>2015-02-23T12:18:19</LastUpdate><Characteristics><YOB>2012</YOB><Flag>HKG</Flag><CallSign>VRLB7</CallSign><PortReg> <TownID>80</TownID> <Name>Hong Kong</Name> <Country>Hong Kong</Country></PortReg><Net>45087</Net><DWT>99778</DWT><GenType>U</GenType><SubType>CC</SubType><Class1>AB</Class1><HullDetails> <HullID>8194903</HullID> <Dimensions><BreadthMoulded>45.2</BreadthMoulded><Depth>24.2</Depth><Draft>12.5</Draft><Freeboard>10700</Freeboard><LBP>286.8</LBP><LOA>299.9</LOA><LRG>286.8</LRG><FormulaDWT>138486</FormulaDWT> </Dimensions> <Superstructure><BulbousBow>Y</BulbousBow><Material>S</Material><Decks>1</Decks><FuelConsumption>23kts (L)</FuelConsumption><PropulsionType>MO</PropulsionType><Speed>23</Speed><ReeferPlugs>1700</ReeferPlugs> </Superstructure> <Machinery><Propellers><PropellerRecord> <PropID>6715303</PropID> <No>1</No> <Type>FP</Type></PropellerRecord></Propellers> </Machinery></HullDetails><Engines> <EngineRecord><EngineID>8106092</EngineID><Designation><Designer> <CompanyID>170119</CompanyID> <CompanyName>Unknown Engine Designer</CompanyName> <HeadOffice><OfficeRecord><OfficeID>133981</OfficeID><TownCity>Unknown</TownCity><Country>Unknown</Country><LastUpdate>2005-01-11T00:00:00</LastUpdate></OfficeRecord> </HeadOffice> <LastUpdate>2005-04-29T00:00:00</LastUpdate></Designer></Designation><Start>2008-07-01</Start><DOB>2008-01-01</DOB><DOBQ>A</DOBQ><EngineNo>1</EngineNo><RPM>78</RPM><LastUpdate>2013-09-25T03:22:27</LastUpdate> </EngineRecord></Engines></Characteristics></Vessel>");

                CultureInfo culture = new CultureInfo("en-US");

                XDocument xDoc = XDocument.Load(tr);
                if (xDoc.Element("Vessel") != null)
                {
                    if (xDoc.Element("Vessel").Element("VslName") != null)
                    {
                        vslVO.VesselName = xDoc.Element("Vessel").Element("VslName").Value;
                    }
                    if (xDoc.Element("Vessel").Element("VslType") != null)
                    {
                        //DDL
                        //string VslType = xDoc.Element("Vessel").Element("VslType").Value;

                        string vesselType = xDoc.Element("Vessel").Element("VslType").Value;
                        vslVO.VesselTypeName = vesselType;
                        vslVO.VesselType = _unitOfWork.SqlQuery<string>("Select SubCatCode from [dbo].SubCategory  where SubCatName = @p0 and supcatcode='VETY' and RecordStatus='A'", vesselType.Trim()).FirstOrDefault<string>();


                    }
                    if (xDoc.Element("Vessel").Element("MMSI") != null)
                    {
                        vslVO.MMSINumber = long.Parse(xDoc.Element("Vessel").Element("MMSI").Value, CultureInfo.InvariantCulture);
                    }
                    if (xDoc.Element("Vessel").Element("IMO") != null)
                    {
                        vslVO.IMONo = xDoc.Element("Vessel").Element("IMO").Value;
                    }

                    if (xDoc.Element("Vessel").Element("Gross") != null)
                    {
                        vslVO.GrossRegisteredTonnageInMT = Convert.ToDecimal(xDoc.Element("Vessel").Element("Gross").Value, culture);
                    }

                    


                    if (xDoc.Element("Vessel").Element("Characteristics") != null)
                    {

                        if (xDoc.Element("Vessel").Element("Characteristics").Element("HullDetails").Element("Dimensions").Element("LOA") != null)
                        {
                            vslVO.LengthOverallInM = Convert.ToDecimal(xDoc.Element("Vessel").Element("Characteristics").Element("HullDetails").Element("Dimensions").Element("LOA").Value, CultureInfo.InvariantCulture);
                        }

                        if (xDoc.Element("Vessel").Element("Characteristics").Element("HullDetails").Element("Dimensions").Element("BreadthExtreme") != null)
                        {
                            vslVO.BeamInM = Convert.ToDecimal(xDoc.Element("Vessel").Element("Characteristics").Element("HullDetails").Element("Dimensions").Element("BreadthExtreme").Value, culture);
                        }

                        if (xDoc.Element("Vessel").Element("Characteristics").Element("Net") != null)
                        {
                            vslVO.NetRegisteredTonnageInMT = Convert.ToDecimal(xDoc.Element("Vessel").Element("Characteristics").Element("Net").Value, culture);
                        }

                        if (xDoc.Element("Vessel").Element("Characteristics").Element("DWT") != null)
                        {
                            vslVO.DeadWeightTonnageInMT = Convert.ToDecimal(xDoc.Element("Vessel").Element("Characteristics").Element("DWT").Value, culture);
                        }

                        if (xDoc.Element("Vessel").Element("Characteristics").Element("YOB") != null)
                        {
                            vslVO.VesselBuildYear = long.Parse(xDoc.Element("Vessel").Element("Characteristics").Element("YOB").Value, CultureInfo.InvariantCulture);
                        }
                        if (xDoc.Element("Vessel").Element("Characteristics").Element("Flag") != null)
                        {
                            //DDL
                            vslVO.VesselNationality = xDoc.Element("Vessel").Element("Characteristics").Element("Flag").Value;
                        }
                        if (xDoc.Element("Vessel").Element("Characteristics").Element("CallSign") != null)
                        {

                            vslVO.CallSign = xDoc.Element("Vessel").Element("Characteristics").Element("CallSign").Value;
                        }
                        if (xDoc.Element("Vessel").Element("Characteristics").Element("Net") != null)
                        {
                            vslVO.NetRegisteredTonnageInMT = decimal.Parse(xDoc.Element("Vessel").Element("Characteristics").Element("Net").Value, CultureInfo.InvariantCulture);
                        }
                        if (xDoc.Element("Vessel").Element("Characteristics").Element("DWT") != null)
                        {
                            vslVO.DeadWeightTonnageInMT = decimal.Parse(xDoc.Element("Vessel").Element("Characteristics").Element("DWT").Value, CultureInfo.InvariantCulture);
                        }
                        if (xDoc.Element("Vessel").Element("Characteristics").Element("PortReg") != null)
                        {
                            if (xDoc.Element("Vessel").Element("Characteristics").Element("PortReg").Element("Name") != null)
                            {
                                string strPortName = xDoc.Element("Vessel").Element("Characteristics").Element("PortReg").Element("Name").Value;
                                PortRegistry detailOfPortRegistry = _unitOfWork.Repository<PortRegistry>().Queryable().Where(pr => pr.PortName.ToLower() == strPortName.ToLower()).FirstOrDefault();
                                if (detailOfPortRegistry != null)
                                {
                                    vslVO.PortCode = detailOfPortRegistry.PortCode;
                                    vslVO.PortName = detailOfPortRegistry.PortName;
                                    vslVO.PortOfRegistry = detailOfPortRegistry.PortCode;
                                }
                            }

                            //vslVO.DeadWeightTonnageInMT = decimal.Parse(xDoc.Element("Vessel").Element("Characteristics").Element("DWT").Value);
                        }

                    }

                    // CallSign
                }
                return vslVO;
            });
        }

    }
}

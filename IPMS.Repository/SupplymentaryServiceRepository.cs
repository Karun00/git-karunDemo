using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using IPMS.Domain.DTOS;
using System;
using System.Data.Entity;
using IPMS.Domain;
using System.Data.SqlClient;
using System.Globalization;
namespace IPMS.Repository
{
    public class SupplymentaryServiceRepository : ISupplymentaryServiceRepository
    {
        private IUnitOfWork _unitOfWork;

        public SupplymentaryServiceRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Author  : Sandeep Appana 
        /// Date    : 21st August 2014
        /// Purpose : To Get ServiceType details
        /// SRTY - Super Category Code
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> GetServiceType()
        {
            var servicetype = (from st in _unitOfWork.Repository<SubCategory>().Query().Select()
                               where st.SupCatCode == "SRTY"
                               select new SubCategoryVO
                               {
                                   SubCatCode = st.SubCatCode,
                                   SubCatName = st.SubCatName
                               });

            return servicetype.ToList();
        }

        /// <summary>
        /// Author  : Srini  
        /// Date    : 15th Sep 2014
        /// Purpose : To Get Supplymentary Service Request details by SuppServiceRequestID
        /// </summary>
        /// <returns></returns>
        public SuppServiceRequestVO GetSupplymentaryServiceRequest(string portcode, string SuppServiceRequestId)
        {
            int id=Convert.ToInt32(SuppServiceRequestId);
            SuppServiceRequest servicerequestlist = new SuppServiceRequest();        

            servicerequestlist = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Query()
                                      //.Include(sr => sr.WorkflowInstance)
                                       .Include(sr => sr.WorkflowInstance.SubCategory)
                                     .Include(sr => sr.SuppFloatingCranes)
                                      .Include(sr => sr.SuppHotColdWorkPermits)
                                     .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments))
                                     .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments.Select(s => s.Document.SubCategory1)))
                                     .Include(sr => sr.SuppHotColdWorkPermits.Select(l => l.Location))                                   
                                     .Include(sr => sr.SubCategory)
                                     .Include(sr => sr.Berth)

                                     .Select()

                                  where //sr.ArrivalNotification.PortCode == portcode &&

                                     sr.SuppServiceRequestID.ToString(CultureInfo.InvariantCulture) == SuppServiceRequestId

                                  orderby sr.ModifiedDate descending

                                  select sr).FirstOrDefault();


            string vcn = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Query().Select().Where(sr => sr.SuppServiceRequestID == id && sr.PortCode == portcode) select sr.VCN).FirstOrDefault().ToString();
            var _portcode = new SqlParameter("@p_PortCode", portcode);
            var _vcn = new SqlParameter("@p_VCN", vcn);

            var arrdetails = _unitOfWork.SqlQuery<ArrvNotForSuppServiceViewVO>("usp_GetSuppServiceRequestsArrivalDetailsById @p_PortCode,@p_VCN",
                _portcode, _vcn).FirstOrDefault();

            ArrivalNotification arr = new ArrivalNotification();
            servicerequestlist.ArrivalNotification = arr;
            servicerequestlist.ArrivalNotification.Vessel = new Vessel();
            VesselCallMovement vmo = new VesselCallMovement();

            servicerequestlist.VCN = arrdetails.VCN;                 
            servicerequestlist.ArrivalNotification.VCN = arrdetails.VCN;           
            servicerequestlist.ArrivalNotification.Vessel.VesselName= arrdetails.VesselName;
            servicerequestlist.ArrivalNotification.Vessel.LengthOverallInM = arrdetails.LengthOverallInM;        
            servicerequestlist.ArrivalNotification.ArrDraft = arrdetails.ArrDraft;
            servicerequestlist.ArrivalNotification.Vessel.DeadWeightTonnageInMT = arrdetails.DeadWeightTonnageInMT;
            servicerequestlist.ArrivalNotification.Tidal = arrdetails.Tidal;          
            servicerequestlist.ArrivalNotification.Vessel.BeamInM = arrdetails.BeamInM;
            servicerequestlist.ArrivalNotification.Vessel.CallSign = arrdetails.CallSign;
            servicerequestlist.ArrivalNotification.Vessel.VesselNationality = arrdetails.VesselNationality;
            servicerequestlist.ArrivalNotification.LastPortOfCall = arrdetails.LastPortOfCall;
            servicerequestlist.ArrivalNotification.DaylightRestriction = arrdetails.DaylightRestriction;
            servicerequestlist.ArrivalNotification.VoyageIn = arrdetails.VoyageIn;
            servicerequestlist.ArrivalNotification.VoyageOut = arrdetails.VoyageOut;
            servicerequestlist.ArrivalNotification.Vessel.IMONo = arrdetails.IMONo;
            servicerequestlist.ArrivalNotification.Vessel.GrossRegisteredTonnageInMT = arrdetails.GrossRegisteredTonnageInMT;
            servicerequestlist.ArrivalNotification.AnyDangerousGoodsonBoard = arrdetails.AnyDangerousGoodsonBoard;
            servicerequestlist.ArrivalNotification.Vessel.VesselType = arrdetails.CargoType;
            servicerequestlist.ArrivalNotification.NextPortOfCall = arrdetails.NextPortOfCall;            
            
            servicerequestlist.ArrivalNotification.ArrivalReasons = (from sr in _unitOfWork.Repository<ArrivalReason>().Queryable().Where(sr => sr.VCN == vcn)
                                                                     .Include(sr=>sr.SubCategory)
                                                                     select sr).ToList();           
            servicerequestlist.ArrivalNotification.VesselETAChanges = (from sr in _unitOfWork.Repository<VesselETAChange>().Query().Select().Where(sr => sr.VCN == vcn).OrderByDescending(sr=>sr.CreatedDate) select sr).ToList();

            if (servicerequestlist.ServiceType == "FCST")
            {
                servicerequestlist.ArrivalNotification.ArrivalCommodities = (from sr in _unitOfWork.Repository<ArrivalCommodity>().Queryable().Where(sr => sr.VCN == vcn && sr.PortCode == portcode)
                                                                            .Include(sr => sr.SubCategory)
                                                                            .Include(sr => sr.Berth)
                                                                            .Include(sr => sr.SubCategory1)
                                                                            .Include(sr => sr.SubCategory2)
                                                                             select sr).ToList();
            }
            

            
            return servicerequestlist.MapToDto();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="portcode"></param>
        /// <param name="SuppServiceRequestID"></param>
        /// <returns></returns>
        public List<SuppServiceRequestVO> GetSupplymentaryServiceRequestListVcn(string portcode, string VCN)
        {
            var servicerequestlist = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Queryable().Where(sr => sr.VCN == VCN)
                                      .Include(sr => sr.WorkflowInstance)
                                      .Include(sr => sr.WorkflowInstance.SubCategory)
                                      .Include(sr => sr.SuppFloatingCranes)
                                      .Include(sr => sr.SuppHotColdWorkPermits)
                                      .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments))
                                      .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments.Select(s => s.Document.SubCategory1)))
                                      .Include(sr => sr.SuppHotColdWorkPermits.Select(l => l.Location))
                                      .Include(sr => sr.ArrivalNotification)
                                      .Include(sr => sr.ArrivalNotification.Vessel)
                                      .Include(sr => sr.ArrivalNotification.ArrivalCommodities)
                                      .Include(sr => sr.SubCategory)
                                      .Include(sr => sr.Berth)

                                      // .Select()

                                      where sr.ArrivalNotification.PortCode == portcode
                                      //   && sr.VCN == VCN

                                      select sr).OrderByDescending(x => x.ModifiedDate).ToList();


            return servicerequestlist.MapToDto();
        }
        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 22st August 2014
        /// Purpose : To Get Supplymentary Service Request details
        /// </summary>
        /// <returns></returns>
        public List<SuppServiceRequestVO> GetSupplymentaryServiceRequestList(string portcode, int agentUserId, int toUserId, int empId, string frmdate, string todate, string vcnSearch, string vesselName)
        {
            var servicerequestlist = new List<SuppServiceRequest>();
            DateTime dtfromDate = new DateTime();
            dtfromDate = Convert.ToDateTime(frmdate);
            DateTime dtToDate = new DateTime();
            dtToDate = Convert.ToDateTime(todate);
            dtToDate = dtToDate.AddDays(1);
            if (agentUserId != 0 || toUserId != 0)
            {
                if (vcnSearch != "All")
                {
                    servicerequestlist = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Queryable().Where(sr => sr.ArrivalNotification.PortCode == portcode
                                          && sr.AgentId == agentUserId
                                    && sr.ArrivalNotification.VCN.Equals(vcnSearch)
                                         )
                                         .Include(sr => sr.WorkflowInstance)
                                         .Include(sr => sr.WorkflowInstance.SubCategory)
                                         .Include(sr => sr.SuppFloatingCranes)
                                         .Include(sr => sr.SuppHotColdWorkPermits)
                                         .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments))
                                         .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments.Select(s => s.Document.SubCategory1)))
                                         .Include(sr => sr.SuppHotColdWorkPermits.Select(l => l.Location))
                                         .Include(sr => sr.ArrivalNotification)
                                         .Include(sr => sr.ArrivalNotification.VesselETAChanges)
                                         .Include(sr => sr.ArrivalNotification.Vessel)
                                         .Include(sr => sr.ArrivalNotification.VesselCalls)
                                         .Include(sr => sr.ArrivalNotification.VesselCalls.Select(bo => bo.Bollard1))
                                         .Include(sr => sr.ArrivalNotification.VesselCalls.Select(bo => bo.Bollard1.Berth))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities)
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(b => b.Berth))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc => sc.SubCategory))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc1 => sc1.SubCategory1))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc2 => sc2.SubCategory2))
                                         .Include(sr => sr.ArrivalNotification.ArrivalReasons.Select(b => b.SubCategory))
                                         .Include(sr => sr.SubCategory)
                                         .Include(sr => sr.Berth)
                                          // .Include(sr => sr.ArrivalNotification.ArrivalAgents.Where(x => x.AgentID == agentUserId))

                                          //.Select()

                                          //join aa in _unitOfWork.Repository<ArrivalAgent>().Query().Select() on sr.VCN equals aa.VCN into t
                                          //from rt in t.DefaultIfEmpty()

                                          //where sr.ArrivalNotification.PortCode == portcode && sr.RecordStatus == "A"
                                          //&& (sr.ArrivalNotification.CreatedBy == AgentUserID || sr.ArrivalNotification.TerminalOperatorID == ToUserID) //sr.CreatedBy == AgentUserID 
                                          // && (sr.ArrivalNotification.TerminalOperatorID == ToUserID) //sr.CreatedBy == AgentUserID 
                                          select sr).OrderByDescending(x => x.ModifiedDate).ToList();
                }
                else if (vesselName != "All")
                {
                    servicerequestlist = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Queryable().Where(sr => sr.ArrivalNotification.PortCode == portcode
                                          && sr.AgentId == agentUserId

                                         && sr.ArrivalNotification.Vessel.VesselName.Equals(vesselName)
                                         )
                                         .Include(sr => sr.WorkflowInstance)
                                         .Include(sr => sr.WorkflowInstance.SubCategory)
                                         .Include(sr => sr.SuppFloatingCranes)
                                         .Include(sr => sr.SuppHotColdWorkPermits)
                                         .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments))
                                         .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments.Select(s => s.Document.SubCategory1)))
                                         .Include(sr => sr.SuppHotColdWorkPermits.Select(l => l.Location))
                                         .Include(sr => sr.ArrivalNotification)
                                         .Include(sr => sr.ArrivalNotification.VesselETAChanges)
                                         .Include(sr => sr.ArrivalNotification.Vessel)
                                         .Include(sr => sr.ArrivalNotification.VesselCalls)
                                         .Include(sr => sr.ArrivalNotification.VesselCalls.Select(bo => bo.Bollard1))
                                         .Include(sr => sr.ArrivalNotification.VesselCalls.Select(bo => bo.Bollard1.Berth))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities)
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(b => b.Berth))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc => sc.SubCategory))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc1 => sc1.SubCategory1))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc2 => sc2.SubCategory2))
                                         .Include(sr => sr.ArrivalNotification.ArrivalReasons.Select(b => b.SubCategory))
                                         .Include(sr => sr.SubCategory)
                                         .Include(sr => sr.Berth)
                                          // .Include(sr => sr.ArrivalNotification.ArrivalAgents.Where(x => x.AgentID == agentUserId))

                                          //.Select()

                                          //join aa in _unitOfWork.Repository<ArrivalAgent>().Query().Select() on sr.VCN equals aa.VCN into t
                                          //from rt in t.DefaultIfEmpty()

                                          //where sr.ArrivalNotification.PortCode == portcode && sr.RecordStatus == "A"
                                          //&& (sr.ArrivalNotification.CreatedBy == AgentUserID || sr.ArrivalNotification.TerminalOperatorID == ToUserID) //sr.CreatedBy == AgentUserID 
                                          // && (sr.ArrivalNotification.TerminalOperatorID == ToUserID) //sr.CreatedBy == AgentUserID 
                                          select sr).OrderByDescending(x => x.ModifiedDate).ToList();
                }
                else
                {
                    servicerequestlist = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Queryable().Where(sr => sr.ArrivalNotification.PortCode == portcode
                                           && sr.AgentId == agentUserId
                                           && sr.FromDate >= dtfromDate
                                          && sr.FromDate <= dtToDate
                                          && sr.ArrivalNotification.Vessel.VesselName.Equals(vesselName)
                                          )
                                          .Include(sr => sr.WorkflowInstance)
                                          .Include(sr => sr.WorkflowInstance.SubCategory)
                                          .Include(sr => sr.SuppFloatingCranes)
                                          .Include(sr => sr.SuppHotColdWorkPermits)
                                          .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments))
                                          .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments.Select(s => s.Document.SubCategory1)))
                                          .Include(sr => sr.SuppHotColdWorkPermits.Select(l => l.Location))
                                          .Include(sr => sr.ArrivalNotification)
                                          .Include(sr => sr.ArrivalNotification.VesselETAChanges)
                                          .Include(sr => sr.ArrivalNotification.Vessel)
                                          .Include(sr => sr.ArrivalNotification.VesselCalls)
                                          .Include(sr => sr.ArrivalNotification.VesselCalls.Select(bo => bo.Bollard1))
                                          .Include(sr => sr.ArrivalNotification.VesselCalls.Select(bo => bo.Bollard1.Berth))
                                          .Include(sr => sr.ArrivalNotification.ArrivalCommodities)
                                          .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(b => b.Berth))
                                          .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc => sc.SubCategory))
                                          .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc1 => sc1.SubCategory1))
                                          .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc2 => sc2.SubCategory2))
                                          .Include(sr => sr.ArrivalNotification.ArrivalReasons.Select(b => b.SubCategory))
                                          .Include(sr => sr.SubCategory)
                                          .Include(sr => sr.Berth)
                                          // .Include(sr => sr.ArrivalNotification.ArrivalAgents.Where(x => x.AgentID == agentUserId))

                                          //.Select()

                                          //join aa in _unitOfWork.Repository<ArrivalAgent>().Query().Select() on sr.VCN equals aa.VCN into t
                                          //from rt in t.DefaultIfEmpty()

                                          //where sr.ArrivalNotification.PortCode == portcode && sr.RecordStatus == "A"
                                          //&& (sr.ArrivalNotification.CreatedBy == AgentUserID || sr.ArrivalNotification.TerminalOperatorID == ToUserID) //sr.CreatedBy == AgentUserID 
                                          // && (sr.ArrivalNotification.TerminalOperatorID == ToUserID) //sr.CreatedBy == AgentUserID 
                                          select sr).OrderByDescending(x => x.ModifiedDate).ToList();

                }


            }
            else
            {
                servicerequestlist = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Queryable()

                                          .Where(sr => sr.ArrivalNotification.PortCode == portcode
                                              && sr.FromDate >= dtfromDate
                                          && sr.FromDate <= dtToDate)
                                         .Include(sr => sr.WorkflowInstance)
                                         .Include(sr => sr.WorkflowInstance.SubCategory)
                                         .Include(sr => sr.SuppFloatingCranes)
                                         .Include(sr => sr.SuppHotColdWorkPermits)
                                          //.Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments.Select(s=>s.SubCategory)))
                                         .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments))
                                         .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments.Select(s => s.Document.SubCategory1)))
                                         .Include(sr => sr.SuppHotColdWorkPermits.Select(l => l.Location))
                                         .Include(sr => sr.ArrivalNotification)
                                         .Include(sr => sr.ArrivalNotification.VesselETAChanges)
                                         .Include(sr => sr.ArrivalNotification.Vessel)
                                         .Include(sr => sr.ArrivalNotification.VesselCalls)
                                         .Include(sr => sr.ArrivalNotification.VesselCalls.Select(bo => bo.Bollard1))
                                         .Include(sr => sr.ArrivalNotification.VesselCalls.Select(bo => bo.Bollard1.Berth))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities)
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(b => b.Berth))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc => sc.SubCategory))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc1 => sc1.SubCategory1))
                                         .Include(sr => sr.ArrivalNotification.ArrivalCommodities.Select(sc2 => sc2.SubCategory2))
                                         .Include(sr => sr.ArrivalNotification.ArrivalReasons.Select(b => b.SubCategory))
                                         .Include(sr => sr.SubCategory)
                                         .Include(sr => sr.Berth)

                                      // .Select()

                                      // where sr.ArrivalNotification.PortCode == portcode && sr.RecordStatus == "A"
                                      //  && sr.CreatedBy == userid

                                      select sr).OrderByDescending(x => x.ModifiedDate).ToList();
            }


            var arrivalargent = (from aa in _unitOfWork.Repository<ArrivalAgent>().Queryable().Where(aa => aa.AgentID == agentUserId && aa.IsPrimary == "N")
                                 //where aa.AgentID == agentUserId && aa.IsPrimary == "N"
                                 select new
                                 {
                                     VCN = aa.VCN,
                                     AgentID = aa.AgentID
                                 });
            //collection.Select(c => {c.PropertyToSet = value; return c;}).ToList();
            //lstusers.Where(a => !lstresc.Any(a1 => a1.ResourceID == a.ID)).ToList();

            var vesselCall = (from vc in _unitOfWork.Repository<VesselCall>().Queryable().Where(vc => vc.RecentAgentID == agentUserId)
                              // where vc.RecentAgentID == agentUserId
                              select new
                              {
                                  VCN = vc.VCN,
                                  AgentID = vc.RecentAgentID
                              });


            // Updated By Srinivas - on 25th Jan 2016 - view provision for employee also all
            if (agentUserId != 0 || toUserId != 0)
            {
                servicerequestlist = servicerequestlist.Where(a => arrivalargent.Any(a1 => a1.VCN == a.VCN) || vesselCall.Any(a2 => a2.VCN == a.VCN)).ToList();


                foreach (SuppServiceRequest objSuppRequest in servicerequestlist)
                {
                    if (objSuppRequest.AgentId == agentUserId)
                    {
                        objSuppRequest.IsPrimaryAgent = "Y";
                    }
                    else
                    {
                        objSuppRequest.IsPrimaryAgent = "N";
                    }
                }
            }
            else
            {

                foreach (SuppServiceRequest objSuppRequest in servicerequestlist)
                {
                    objSuppRequest.IsPrimaryAgent = "Y";
                }
            }
            if (frmdate != "All")
            {
                //servicerequestlist = servicerequestlist.FindAll(sr => sr.FromDate != null && sr.FromDate >= Convert.ToDateTime(frmdate)).ToList();
                //servicerequestlist = servicerequestlist.Where(sr => (DateTime.Parse(sr.FromDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) >= DateTime.Parse(frmdate, CultureInfo.InvariantCulture)) && (DateTime.Parse(sr.FromDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) <= DateTime.Parse(todate, CultureInfo.InvariantCulture))).ToList();
                //servicerequestlist = servicerequestlist.Where(sr => (DateTime.Parse(sr.FromDate.ToString("yyyy-MM-dd")) >= DateTime.Parse(frmdate)) && (DateTime.Parse(sr.FromDate.ToString("yyyy-MM-dd")) <= DateTime.Parse(todate))).ToList();
                if (frmdate != string.Empty && todate != string.Empty)
                {
                    servicerequestlist = servicerequestlist.FindAll(t => t.FromDate != null && (Convert.ToDateTime(t.FromDate, CultureInfo.InvariantCulture).Date >= Convert.ToDateTime(frmdate, CultureInfo.InvariantCulture).Date && Convert.ToDateTime(t.FromDate, CultureInfo.InvariantCulture).Date < Convert.ToDateTime(todate, CultureInfo.InvariantCulture).AddDays(1).Date));
                }
            }
            if (vcnSearch != "All")
                servicerequestlist = servicerequestlist.FindAll(t => t.VCN.ToUpperInvariant().Contains(vcnSearch.ToUpperInvariant()));

            if (vesselName != "All")
                servicerequestlist = servicerequestlist.FindAll(t => t.ArrivalNotification.Vessel.VesselName.ToUpperInvariant().Contains(vesselName.ToUpperInvariant()));

            return servicerequestlist.MapToDto();
        }
        //Srinivas 

        public List<SuppServiceRequestVO> GetSupplementaryGridDetails(string portcode, int agentUserId, int toUserId, int empId, 
            string frmdate, string todate, string vcnSearch, string vesselName)
        {
            var _portcode = new SqlParameter("@PortCode", portcode);
            var _agentuserid = new SqlParameter("@Userid", toUserId);
            var _frmdate = new SqlParameter("@Formdate", frmdate);
            var _todate = new SqlParameter("@Todate", todate);
            var _vcnSearch = new SqlParameter("@VCN", vcnSearch);
            var _vesselName = new SqlParameter("@VesselName", vesselName);
            //DateTime dtfromDate = new DateTime();
            //dtfromDate = Convert.ToDateTime(frmdate);
            //DateTime dtToDate = new DateTime();
            //dtToDate = Convert.ToDateTime(todate);
            //dtToDate = dtToDate.AddDays(1);

            var suppdetails = _unitOfWork.SqlQuery<SuppServiceRequestVO>("GetSupplemenataryServiceRequestGridDetails @Formdate,@Todate,@VCN,@VesselName,@Userid,@PortCode",
                _frmdate,_todate,_vcnSearch,_vesselName,_agentuserid ,_portcode).ToList();

            return suppdetails;
        }



        /// <summary>
        /// Author  : Srini  
        /// Date    : 1st Sep 2014
        /// Purpose : To Get Supplymentary Service Request based on Water Service Type details
        /// </summary>
        /// <returns></returns>
        public List<SuppServiceRequestVO> AllSuppHotWorkInspectionDetails(string portcode)
        {
            var servicerequestlist = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Queryable()
                                          .Include(sr => sr.WorkflowInstance)
                                          .Include(sr => sr.WorkflowInstance.SubCategory)
                                          .Include(sr => sr.SuppHotWorkInspections)
                                          //.Include(sr => sr.SuppFloatingCranes)
                                          .Include(sr => sr.SuppHotColdWorkPermits)
                                          .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments))
                                       .Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments.Select(s => s.Document.SubCategory1)))
                                      .Include(sr => sr.SuppHotColdWorkPermits.Select(l => l.Location))
                                      .Include(sr => sr.ArrivalNotification)
                                      .Include(sr => sr.ArrivalNotification.VesselETAChanges)
                                      .Include(sr => sr.ArrivalNotification.Vessel)
                                      .Include(sr => sr.ArrivalNotification.ArrivalCommodities)
                                      .Include(sr => sr.SubCategory)
                                      .Include(sr => sr.Berth)
                                      //.Select()

                                      where sr.ArrivalNotification.PortCode == portcode && sr.ServiceType == "HWST"
                                      && sr.WorkflowInstance.WorkflowTaskCode == WFStatus.Approved
                                      orderby sr.ModifiedDate descending

                                      select sr).ToList();
            return servicerequestlist.MapToDto();
        }


        /// <summary>
        /// Author  : Srini  
        /// Date    : 1st Sep 2014
        /// Purpose : To Get Supplymentary Service Request based on Water Service Type details
        /// </summary>
        /// <returns></returns>
        public List<SuppServiceRequestVO> AllSuppDockUnDockTimeDetails(string portcode)
        {

            //                          .Select()

            var servicerequestlist = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Query().Tracking(true)
                                            .Include(sr => sr.WorkflowInstance)
                                            .Include(sr => sr.WorkflowInstance.SubCategory)
                                          //.Include(sr => sr.SuppDryDocks)
                                          //.Include(sr => sr.SuppDockUnDockTimes)
                                            .Include(sr => sr.ArrivalNotification)
                                            .Include(sr => sr.ArrivalNotification.VesselETAChanges)
                                            .Include(sr => sr.ArrivalNotification.Vessel)
                                            .Include(sr => sr.ArrivalNotification.ArrivalCommodities)
                                            .Include(sr => sr.SubCategory)
                                            .Include(sr => sr.Berth)
                                            .Select()
                                      // Dry dock service type- DDST
                                      where sr.PortCode == portcode && sr.ServiceType == "DDST"
                                      //&& sr.WorkflowInstance.SubCategory.SubCatName == "Approved"
                                      orderby sr.ModifiedDate descending

                                      select sr).ToList();


            return servicerequestlist.MapToDto();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="suppservicerequestid"></param>
        /// <returns></returns>
        public SuppServiceRequestVO GetSuppServiceRequestByID(int suppservicerequestid)
        {
            var suppservicerequest = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Queryable().Where(sr => sr.SuppServiceRequestID == suppservicerequestid)
                                      .Include(sr => sr.ArrivalNotification)
                                      .Include(sr => sr.ArrivalNotification.Vessel)
                                      .Include(sr => sr.SubCategory)
                                      .Include(sr => sr.Berth)

                                      select new SuppServiceRequestVO
                                      {
                                          SuppServiceRequestID = sr.SuppServiceRequestID,
                                          ServiceTypeName = sr.SubCategory.SubCatName,
                                          PortCode = sr.PortCode,
                                          VCN = sr.ArrivalNotification.VCN,
                                          AgentId = (sr.AgentId == null ? 0 : sr.AgentId),
                                          VesselName = sr.ArrivalNotification.Vessel.VesselName,
                                          BerthName = sr.Berth.BerthName,
                                          FromDate = sr.FromDate.ToString(),
                                          ToDate = sr.ToDate.ToString(),
                                          CreatedDate = sr.CreatedDate,
                                          CreatedBy = sr.CreatedBy,
                                          ModifiedBy = sr.ModifiedBy,
                                          SubmittedDateTime = sr.CreatedDate,
                                          Remarks = sr.Remarks,
                                      }).FirstOrDefault();



            suppservicerequest.FromDate = suppservicerequest.FromDate != "" ? Convert.ToDateTime(suppservicerequest.FromDate, CultureInfo.InvariantCulture).ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture) : null;
            suppservicerequest.ToDate = suppservicerequest.ToDate != "" ? Convert.ToDateTime(suppservicerequest.ToDate, CultureInfo.InvariantCulture).ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture) : null;

            return suppservicerequest;
        }

        /// <summary>
        /// Author : Sandeep Appana
        /// Date   : 15th Sep 2014
        /// Purpose: To get all approved water service details
        /// </summary>
        /// <param name="vcn"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<SuppServiceRequestVO> GetApprovedWaterService(string vcn, string date)
        {
            var SuppWaterService = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Query().Tracking(true)
                                      .Include(sr => sr.WorkflowInstance)
                                        //.Include(sr => sr.WorkflowInstance)
                                      .Include(sr => sr.ArrivalNotification)
                                      .Include(sr => sr.ArrivalNotification.Vessel)
                                      .Include(sr => sr.Berth)
                                      .Include(sr => sr.SubCategory)
                                      .Select()

                                    where sr.WorkflowInstance.WorkflowTaskCode == "WFSA" && sr.ServiceType == "WTST" && sr.FromDate.ToShortDateString() == date

                                    select new SuppServiceRequestVO
                                    {
                                        SuppServiceRequestID = sr.SuppServiceRequestID,
                                        //VCN = sr.VCN,
                                        //VesselName = sr.ArrivalNotification.Vessel.VesselName,
                                        //VesselType = sr.ArrivalNotification.Vessel.VesselType,
                                        RequestNo = sr.VCN + "-" + sr.ArrivalNotification.Vessel.VesselName + "-" + sr.CreatedDate.ToShortDateString(),
                                        VesselDetails = "VCN : " + sr.VCN + "\nVessel Name : " + sr.ArrivalNotification.Vessel.VesselName + "\nService Required at Berth : " + sr.Berth.BerthName + "\nDate of Service : " + sr.CreatedDate + "\nQuantity : " + sr.Quantity


                                    }).ToList();

            return SuppWaterService;
        }

        public List<IMDGInformationVO> GetIMDGForSupplymentaryServiceRequest(string vcn)
        {
            var lstIMDGInformation = (from imdg in _unitOfWork.Repository<IMDGInformation>().Queryable().Where(imdg => imdg.VCN == vcn)
                                     .Include(sr => sr.SubCategory1)
                                      //.Select()
                                      //where imdg.VCN == vcn
                                      select new IMDGInformationVO
                                      {
                                          ClassCode = imdg.SubCategory1.SubCatName,
                                          UNNo = imdg.UNNo

                                      }).ToList();

            return lstIMDGInformation;
        }
        public VesselCallMovementVO GetEtbEtubFromVcn(string vcn)
        {
            //var lstETB_ETUBFromVCM = (from vc in _unitOfWork.Repository<VesselCall>().Query().Tracking(true)
            //                          .Include(vc => vc.ArrivalNotification)
            //                          .Include(vc => vc.ArrivalNotification.Vessel)
            //                         .Select()
            //                          join por1 in _unitOfWork.Repository<PortRegistry>().Query().Tracking(true).Select()
            //                            on vc.ArrivalNotification.LastPortOfCall equals por1.PortCode
            //                          join por2 in _unitOfWork.Repository<PortRegistry>().Query().Tracking(true).Select()
            //                          on vc.ArrivalNotification.NextPortOfCall equals por2.PortCode
            //                          join ve in _unitOfWork.Repository<Vessel>().Query().Tracking(true).Select()
            //                          on vc.ArrivalNotification.VesselID equals ve.VesselID
            //                          join vesselTyp in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
            //                            on ve.VesselType equals vesselTyp.SubCatCode
            //                          join vesselNationality in _unitOfWork.Repository<SubCategory>().Query().Tracking(true).Select()
            //                          on ve.VesselNationality equals vesselNationality.SubCatCode

            //                          where vc.VCN == vcn
            //                          select new VesselCallMovementVO
            //                          {
            //                              ETB = vc.ETB,
            //                              ETUB = vc.ETUB,
            //                              ETA = vc.ETA,
            //                              ETD = vc.ETD,
            //                              CargoType = vesselTyp.SubCatName,
            //                              VesselNationality = vesselNationality.SubCatName,
            //                              LastPortOfCall = por1.PortName,
            //                              NextPortOfCall = por2.PortName,
            //                              ModifiedDate = vc.ModifiedDate

            //                          }).ToList();

            var lstETB_ETUBFromVCM = (from vc in _unitOfWork.Repository<VesselCall>().Queryable().Where(vc => vc.VCN == vcn)
                                      //commenented by divya n getting below details from SP to increase performance
                                      //.Include(vc => vc.ArrivalNotification.LastPort)
                                      //.Include(vc => vc.ArrivalNotification.NextPort)
                                      //.Include(vc => vc.ArrivalNotification.Vessel)
                                      //.Include(vc => vc.ArrivalNotification.Vessel.SubCategory3)
                                      //.Include(vc => vc.ArrivalNotification.Vessel.SubCategory2)
                                      //.Select()

                                      //where vc.VCN == vcn
                                      select new VesselCallMovementVO
                                      {
                                          ETB = vc.ETB,
                                          ETUB = vc.ETUB,
                                          ETA = vc.ETA,
                                          ETD = vc.ETD,
                                          //commenented by divya n getting below details from SP to increase performance
                                          //CargoType = vc.ArrivalNotification.Vessel.SubCategory3.SubCatName,
                                          //VesselNationality = vc.ArrivalNotification.Vessel.SubCategory2.SubCatName,
                                          //LastPortOfCall = vc.ArrivalNotification.LastPort.PortName,
                                          //NextPortOfCall = vc.ArrivalNotification.NextPort.PortName,
                                          //ModifiedDate = vc.ModifiedDate

                                      }).FirstOrDefault();

            //VesselCallMovementVO objVesselCallMovementVO = lstETB_ETUBFromVCM.OrderByDescending(x => x.ModifiedDate).FirstOrDefault();
            //return objVesselCallMovementVO;
            return lstETB_ETUBFromVCM;
        }

        public UserMasterVO GetUserTypesForUser(int userId, string portCode)
        {
            var userdetails = (from user in _unitOfWork.Repository<User>().Queryable()
                               join userpt in _unitOfWork.Repository<UserPort>().Queryable()
                               on user.UserID equals userpt.UserID
                               where userpt.UserID == userId && userpt.PortCode == portCode
                               select new UserMasterVO
                               {
                                   UserID = user.UserID,
                                   UserType = user.UserType,
                                   UserTypeID = user.UserTypeID
                               }).FirstOrDefault<UserMasterVO>();

            return userdetails;

        }

        public List<ServiceRequestVCNDetails> GetVCNDetailsForSuppServiceRequest(string portCode, int agentUserId, string searchValue)
        {
            var _portcode = new SqlParameter("@p_PortCode", portCode);
            var _agentuserid = new SqlParameter("@p_AgentUserID", agentUserId);
            var _searchvalue = new SqlParameter("@p_searchValue", searchValue);

            var vcndtls = _unitOfWork.SqlQuery<ServiceRequestVCNDetails>("dbo.usp_GetVCNDetailsForSuppServiceRequest  @p_PortCode,@p_AgentUserID,@p_searchValue", _portcode, _agentuserid, _searchvalue).ToList();

            return vcndtls;
        }
    }
}

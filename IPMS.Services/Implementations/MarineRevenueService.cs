using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using IPMS.Services.Business;
using IPMS.Services.WorkFlow;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.ServiceModel;


namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
               ConcurrencyMode = ConcurrencyMode.Multiple)]

    public class MarineRevenueService : ServiceBase, IMarineRevenueService
    {

        private IMarineRevenueRepository _marineRevenueRepository;

        public MarineRevenueService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _UserId = GetUserIdByLoginname(_LoginName);
            _marineRevenueRepository = new MarineRevenueRepository(_unitOfWork);

        }

        public MarineRevenueService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());

            _UserId = GetUserIdByLoginname(_LoginName);
            _marineRevenueRepository = new MarineRevenueRepository(_unitOfWork);
        }

        public List<RevenuePostingVO> GetMarineRevenueList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _marineRevenueRepository.GetMarineRevenueList(_PortCode);
            });
        }

        public List<RevenuePostingVO> GetMarineRevenueDetails(string vcnSearch, string vesselName,string frmdate,string todate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _marineRevenueRepository.GetMarineRevenueDetails(_PortCode,vcnSearch, vesselName, frmdate, todate);
            });
        }

        public List<RevenuePostingVO> GetVcnDetails(string searchValue, string searchColumn, string param)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _marineRevenueRepository.GetVcnDetails(searchValue, searchColumn, param, _PortCode);
            });
        }

        public RevenuePostingSectionsVO GetRevenueSectionDetails(string vcn)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _marineRevenueRepository.GetRevenueSectionDetails(vcn, _PortCode);
            });
        }

        public List<AgentVO> GetVcnAgents(string searchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _marineRevenueRepository.GetVcnAgents(searchValue, _PortCode);
            });
        }

        public List<AgentAccountVO> GetAgentAccountDetails(int agentId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _marineRevenueRepository.GetAgentAccountDetails(agentId, _PortCode);
            });
        }

        public int AddMarineRevenueDetails(RevenuePostingSectionsVO revenuePostingDetails)
        {
            return ExecuteFaultHandledOperation(() =>
            {


                List<MarineRevenuePostingVO> PortDuesDetails = revenuePostingDetails.PortDuesDetails;
                List<MarineRevenuePostingVO> BerthDuesDetails = revenuePostingDetails.BerthDuesDetails;
                List<MarineRevenuePostingVO> RefuseRemovalDetails = revenuePostingDetails.RefuseRemovalDetails;

//                revenuepostingvo.BerthDuesDetails = revenuePostings.FindAll(t => t.MovementName == "BERTH DUES");

                List<MarineRevenuePostingVO> ArrivalDetails = revenuePostingDetails.ArrivalDetails;
                List<MarineRevenuePostingVO> ShiftingDetails = revenuePostingDetails.ShiftingDetails;
                List<MarineRevenuePostingVO> WarpingDetails = revenuePostingDetails.WarpingDetails;
                List<MarineRevenuePostingVO> SailingDetails = revenuePostingDetails.SailingDetails;
                List<MarineRevenuePostingVO> DryDockDetails = revenuePostingDetails.DryDockDetails;
                List<MarineRevenuePostingVO> DryDock12HrsDetails = revenuePostingDetails.DryDock12HrsDetails;
                List<MarineRevenuePostingVO> supplimantoryDetails = revenuePostingDetails.SupplimantoryDetails;
                List<MarineRevenuePostingVO> drydockMislaniousDetails = revenuePostingDetails.DrydockMislaniousDetails;


                RevenuePosting _revenuePosting = new RevenuePosting();
                _revenuePosting.vcn = revenuePostingDetails.VCN;
                _revenuePosting.PortCode = _PortCode;
                _revenuePosting.PostedDate = DateTime.Now;
                _revenuePosting.SAPAccNo = revenuePostingDetails.AccountNo;
                _revenuePosting.AgentID = revenuePostingDetails.AgentID;
                _revenuePosting.PostingStatus = "A";
                _revenuePosting.CreatedBy = _UserId;
                _revenuePosting.CreatedDate = DateTime.Now;
                _revenuePosting.ModifiedBy = _UserId;
                _revenuePosting.ModifiedDate = DateTime.Now;
                _revenuePosting.RevenuePostingDtls = null;
                _unitOfWork.Repository<RevenuePosting>().Insert(_revenuePosting);
                List<RevenuePostingDtl> PostPortDuesList = new List<RevenuePostingDtl>();

                var vcn = new SqlParameter("@VCN", revenuePostingDetails.VCN);
                var postsapaccno = new SqlParameter("@PostSapAccno", revenuePostingDetails.AccountNo);

                int revenuedtlcount = _unitOfWork.SqlQuery<int>("dbo.usp_MarineRevenueDtlsCount @VCN,@PostSapAccno", vcn, postsapaccno).FirstOrDefault();



                if (PortDuesDetails.Count > 0)
                {

                    // RevenuePostingDtlSrno
                    foreach (var dtl in PortDuesDetails)
                    {
                        if (dtl.ischecked)
                        {

                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = Convert.ToString(dtl.DueHours, CultureInfo.InvariantCulture);                   
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.MomentType = dtl.MovementType;
                            PostPortDues.ServiceType = dtl.ServiceType;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            PostPortDues.PostingFrom = dtl.RecentlyPostedDate;
                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);


                        }
                    }
                }

                if (BerthDuesDetails.Count > 0)
                {

                    // RevenuePostingDtlSrno
                    foreach (var dtl in BerthDuesDetails)
                    {
                        if (dtl.ischecked)
                        {

                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = Convert.ToString(dtl.DueHours, CultureInfo.InvariantCulture);
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.MomentType = dtl.MovementType;
                            PostPortDues.ServiceType = dtl.ServiceType;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            PostPortDues.PostingFrom = dtl.RecentlyPostedDate;
                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);


                        }
                    }
                }
                if (RefuseRemovalDetails.Count > 0)
                {

                    // RevenuePostingDtlSrno
                    foreach (var dtl in RefuseRemovalDetails)
                    {
                        if (dtl.ischecked)
                        {

                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = Convert.ToString(dtl.DueHours, CultureInfo.InvariantCulture);
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.MomentType = dtl.MovementType;
                            PostPortDues.ServiceType = dtl.ServiceType;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            PostPortDues.PostingFrom = dtl.RecentlyPostedDate;
                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);


                        }
                    }
                }


                if (ArrivalDetails.Count > 0)
                {
                    foreach (var dtl in ArrivalDetails)
                    {
                        if (dtl.ischecked)
                        {
                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = dtl.DueHours.ToString();
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.MomentType = dtl.MovementType;
                            PostPortDues.ServiceType = dtl.ServiceType;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);
                        }
                    }
                }

                if (ShiftingDetails.Count > 0)
                {
                    foreach (var dtl in ShiftingDetails)
                    {
                        if (dtl.ischecked)
                        {
                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = dtl.DueHours.ToString();
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.MomentType = dtl.MovementType;
                            PostPortDues.ServiceType = dtl.ServiceType;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);
                        }
                    }
                }

                if (WarpingDetails.Count > 0)
                {
                    foreach (var dtl in WarpingDetails)
                    {
                        if (dtl.ischecked)
                        {
                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = dtl.DueHours.ToString();
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.MomentType = dtl.MovementType;
                            PostPortDues.ServiceType = dtl.ServiceType;
                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);
                        }
                    }
                }



                if (SailingDetails.Count > 0)
                {
                    foreach (var dtl in SailingDetails)
                    {
                        if (dtl.ischecked)
                        {
                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = dtl.DueHours.ToString();
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.MomentType = dtl.MovementType;
                            PostPortDues.ServiceType = dtl.ServiceType;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);
                        }
                    }
                }


                if (DryDockDetails.Count > 0)
                {
                    foreach (var dtl in DryDockDetails)
                    {
                        if (dtl.ischecked)
                        {
                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = dtl.DueHours.ToString();
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.MomentType = dtl.MovementType;
                            PostPortDues.ServiceType = dtl.ServiceType;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            //if (dtl.PostingDateTime == null)
                            //{     PostPortDues.PostedOn = DateTime.Now;//}

                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);
                        }
                    }
                }
                if (DryDock12HrsDetails.Count > 0)
                {
                    foreach (var dtl in DryDock12HrsDetails)
                    {
                        if (dtl.ischecked)
                        {
                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = dtl.DueHours.ToString();
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.MomentType = dtl.MovementType;
                            PostPortDues.ServiceType = dtl.ServiceType;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);
                        }
                    }
                }


                if (supplimantoryDetails.Count > 0)
                {
                    foreach (var dtl in supplimantoryDetails)
                    {
                        if (dtl.ischecked)
                        {
                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = dtl.DueHours.ToString();
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.ServiceType = dtl.OperationType;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);
                        }
                    }
                }

                if (drydockMislaniousDetails.Count > 0)
                {
                    foreach (var dtl in drydockMislaniousDetails)
                    {
                        if (dtl.ischecked)
                        {
                            RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                            PostPortDues.VCN = _revenuePosting.vcn;
                            PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                            PostPortDues.GroupCode = dtl.GroupCode;
                            PostPortDues.MaterialCode = dtl.MaterialCode;
                            PostPortDues.Units = dtl.DueHours.ToString();
                            PostPortDues.UOM = dtl.UOM;
                            PostPortDues.ServiceType = dtl.OperationType;
                            PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                            PostPortDues.PostedOn = dtl.PostingDateTime;
                            revenuedtlcount = revenuedtlcount + 10;
                            PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                            PostPortDuesList.Add(PostPortDues);
                        }
                    }
                }

                _unitOfWork.Repository<RevenuePostingDtl>().InsertRange(PostPortDuesList);

                _unitOfWork.SaveChanges();

                return _revenuePosting.RevenuePostingID;
            });
        }

        private int AddPortDuesDetails(List<MarineRevenuePostingVO> PortDuesDetails, RevenuePosting _revenuePosting, List<RevenuePostingDtl> PostPortDuesList, SqlParameter vcn, SqlParameter postsapaccno)
        {
            int revenuedtlcount = _unitOfWork.SqlQuery<int>("dbo.usp_MarineRevenueDtlsCount @VCN,@PostSapAccno", vcn, postsapaccno).FirstOrDefault();

            if (PortDuesDetails.Count > 0)
            {

                // RevenuePostingDtlSrno
                foreach (var dtl in PortDuesDetails)
                {
                    if (dtl.ischecked)
                    {

                        RevenuePostingDtl PostPortDues = new RevenuePostingDtl();
                        PostPortDues.VCN = _revenuePosting.vcn;
                        PostPortDues.RevenuePostingID = _revenuePosting.RevenuePostingID;
                        PostPortDues.GroupCode = dtl.GroupCode;
                        PostPortDues.MaterialCode = dtl.MaterialCode;
                        PostPortDues.Units = dtl.DueHours.ToString();
                        PostPortDues.UOM = dtl.UOM;
                        PostPortDues.MomentType = dtl.MovementType;
                        PostPortDues.ServiceType = dtl.ServiceType;
                        PostPortDues.ReferenceID = dtl.ResourceAllocationID;
                        PostPortDues.PostedOn = dtl.PostingDateTime;
                        revenuedtlcount = revenuedtlcount + 10;
                        PostPortDues.RevenuePostingDtlSrno = revenuedtlcount;
                        PostPortDuesList.Add(PostPortDues);


                    }
                }
            }
            return revenuedtlcount;
        }

        public RevenuePostingSectionsVO GetRevenueSectionDetailsView(int revenuePostingId, int agentId, int accountId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _marineRevenueRepository.GetRevenueSectionDetailsView(revenuePostingId, agentId, accountId, _PortCode);
            });
        }

        /// <summary>
        ///  /// Srini
        /// Adv search for VCN auto complete
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public List<RevenuePostingVO> RevenuePostingVcnDetailsforAutocomplete(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _marineRevenueRepository.RevenuePostingVcnDetailsforAutocomplete(searchvalue, _PortCode);
            });
        }
        /// <summary>
        ///  Vessel / IMO No. Autocomplete for Search
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        public List<VesselVO> RevenuePostingVesselDetailsforAutocomplete(string searchvalue)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _marineRevenueRepository.RevenuePostingVesselDetailsforAutocomplete(_PortCode, searchvalue);
            });
        }
    }
}

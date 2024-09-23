using System;
using System.Collections.Generic;
using System.Linq;
using Core.Repository;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using IPMS.Repository;
using IPMS.Services.WorkFlow;
using System.Data.SqlClient;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RosterService : ServiceBase, IRosterService
    {
        private IRosterRepository _RosterRepository;
        private IAccountRepository _accountRepository;
        private IResourceAttendanceRepository _resourceattendenceRepository;

        public RosterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _accountRepository = new AccountRepository(_unitOfWork);
            _RosterRepository = new RosterRepository(_unitOfWork);
            _resourceattendenceRepository = new ResourceAttendanceRepository(_unitOfWork);
        }

        public RosterService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _accountRepository = new AccountRepository(_unitOfWork);
            _RosterRepository = new RosterRepository(_unitOfWork);
            _resourceattendenceRepository = new ResourceAttendanceRepository(_unitOfWork);
        }

        public RosterVO AddRoster(RosterVO rosterdata)
        {
            throw new NotImplementedException();
        }

        public RosterVO ModifyRoster(RosterVO rosterdata)
        {
            throw new NotImplementedException();
        }

        public RosterReferenceVO GetRosterReferencesData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                RosterReferenceVO VO = new RosterReferenceVO();
                string portCode = _PortCode;
                VO.Designations = _resourceattendenceRepository.GetDesignations().MapToDto();
                VO.Shifts = _RosterRepository.GetshiftList(portCode).MapToDTO(); // sandeep commented, it is retrieving all shifts including ShiftOff also on 10-12-2014
                //VO.Shifts = _RosterRepository.GetshiftList(portCode).FindAll(s => s.IsShiftOff == "N").MapToDTO();  // sandeep added for retrieving only shifts which are not ShitOFF on 10-12-2014
                VO.Months = _RosterRepository.GetMonthDetails();
                VO.Years = _RosterRepository.GetYearDetails();
                return VO;

            });
        }

        public List<RosterVO> GetRosterlist()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _RosterRepository.GetRosterList().MapToListDTO();
            });
        }

        public List<RosterVO> GetRosterDetails(RosterVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var PortCode = new SqlParameter("@portcode", _PortCode);
                var designation = new SqlParameter("@designation", data.Designation);
                var year = new SqlParameter("@yearno", data.Year);
                var month = new SqlParameter("@monthno", data.Month);
                var rosterDetails = _unitOfWork.SqlQuery<RosterVO>("dbo.usp_GetRosterInformation @monthno,@yearno, @designation, @portcode", month, year, designation, PortCode).ToList();
                if (rosterDetails.Count > 0)
                {
                    for (int i = 0; i < rosterDetails.Count; i++)
                    {
                        rosterDetails[i].Year = data.Year;
                    }
                }
                return rosterDetails;
            });
        }

        public int SaveRosterDetails(RosterGroupVO data)
        {
            return ExecuteFaultHandledOperation(() =>
            {

                int userid = _accountRepository.GetUserId(_LoginName);

                if (data.RosterAloocationLists.Count > 0)
                {
                    foreach (var rosteralocation in data.RosterAloocationLists)
                    {

                        var P_monthid = new SqlParameter("@monthid", data.month);
                        var P_Yearid = new SqlParameter("@Yearid", data.Year);
                        var P_Weekid = new SqlParameter("@Weekid", rosteralocation.WeekNo);
                        var P_MonShiftid = new SqlParameter("@MonShiftid", rosteralocation.Monday == null ? "" : rosteralocation.Monday);
                        var P_TueShiftid = new SqlParameter("@TueShiftid", rosteralocation.Tuesday == null ? "" : rosteralocation.Tuesday);
                        var P_WedShiftid = new SqlParameter("@WedShiftid", rosteralocation.Wednesday == null ? "" : rosteralocation.Wednesday);
                        var P_ThuShiftid = new SqlParameter("@ThuShiftid", rosteralocation.Thursday == null ? "" : rosteralocation.Thursday);
                        var P_FriShiftid = new SqlParameter("@FriShiftid", rosteralocation.Friday == null ? "" : rosteralocation.Friday);
                        var P_SatShiftid = new SqlParameter("@SatShiftid", rosteralocation.Saturday == null ? "" : rosteralocation.Saturday);
                        var P_SunShiftid = new SqlParameter("@SunShiftid", rosteralocation.Sunday == null ? "" : rosteralocation.Sunday);
                        var P_Groupid = new SqlParameter("@Groupid", rosteralocation.ResourceGroupID);
                        var P_Userid = new SqlParameter("@Userid", userid);

                        _unitOfWork.SqlQuery<UserConversationVO>("dbo.usp_RosterAlocation @monthid, @Yearid, @Weekid, @MonShiftid, @TueShiftid, @WedShiftid, @ThuShiftid, @FriShiftid, @SatShiftid, @SunShiftid, @Groupid, @Userid", P_monthid, P_Yearid, P_Weekid, P_MonShiftid, P_TueShiftid, P_WedShiftid, P_ThuShiftid, P_FriShiftid, P_SatShiftid, P_SunShiftid, P_Groupid, P_Userid).ToList();
                    }
                }

                return userid;
            });
        }
    }
}

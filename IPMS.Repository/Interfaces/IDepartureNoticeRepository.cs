using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IDepartureNoticeRepository
    {
        UserMasterVO GetTerminalOperatorForUser(int userId, string portCode);

        List<DepartureNoticeVO> GetPendingArrivalNotifications(int? agentId, string portCode, string departureId, int? userId, string vcn, string vesselName, string submissionDateFrom, string submissionDateTo);

        DepartureNoticeVO GetDepartureNoticeById(string departureId);

        DepartureNoticeVO GetDepartureNoticeDetailsForWorkFlow(int? agentId, string portCode, string departureNoticeId, int? userId);

        DepartureNoticeVO GetDepartureNoticeServiceRequest(string vcn);

        /// <summary>
        /// Srini - 
        /// Adv Search for VCN Auto complete
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        List<RevenuePostingVO> DepartureNoticeVcnDetailsforAutocomplete(int? agentId, string portCode, int? userId, string searchValue);
        /// <summary>
        /// Srini
        /// Adv search for Vessel auto complete
        /// </summary>
        /// <param name="PortCode"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<VesselVO> DepartureNoticeVesselDetailsforAutocomplete(int? agentId, string portCode, int? userId,  string searchValue);
    }
}

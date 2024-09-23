using System;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IArrivalNotificationRepository
    {
        ArrivalNotificationDetails GetArrivalNotificationById(string value);
        ArrivalNotification GetArrivalNotificationByVcn(string vcn);
        List<ArrivalNotification> GetArrivalNotificationByPortCode(string portCode, string userType, int userTypeId, int userId);
        List<ArrivalNotification> GetArrivalNotificationSearch(string portCode, string userType, int userTypeId, int userId, string etaFrom, string etaTo);
        List<ArrivalNotification> GetArrivalNotificationsByAgentId(int agentId);
        List<ArrivalNotificationVO> GetArrivalNotificationsByPortCodeAgentId(string portCode, int agentId);
        List<ArrivalNotificationDraftVO> GetArrivalNotificationsDrafts(string portCode, int agentId);
        List<TerminalOperatorVO> GetBirthingTo(string portCode);
        List<ArrivalNotificationGridVO> GetArrivalNotificationByPortCodeGrid(string fromDate, string toDate, string vcn, string vesselId, string imdg, string isps, string portCode, string userType, int userTypeId, int userId, string imdgClear, string ispsClear, string phoClear);

        List<RevenuePostingVO> GetArrivalVcnDetailsForAutocomplete(string searchValue, string portCode, string userType, int userId);
        List<ArrvWorkflowStatusVo> GetNotificationStatus(string vcn);

        bool IsIspsClearanceRole(int userId);
        /// <summary>
        /// Author   : Sandeep Appana
        /// Date     : 27-8-2014
        /// Purpose  : To Get Arrival Commodity record(s) based on vcn
        /// </summary>
        /// <param name="vcn"></param>
        /// <returns></returns>
        List<ArrivalCommodityVo> GetArrivalCommoditiesByVcn(string vcn);

        string GetArrivalReasonForVisit(string pvcn);

        WorkflowInstance GetTidalWorkflowStatusByVcn(string entityCode, string referenceId);

        string ArrivalNotificationVoyageValidation(int vesselid, string voyagein, string voyageout,string portcode);        

        List<MarpolGroupVO> Marpol();
    }
}

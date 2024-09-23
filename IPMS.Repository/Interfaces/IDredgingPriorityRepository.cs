using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
   public interface IDredgingPriorityRepository
    {
       List<DredgingPriorityVO> GetDredgingPriorityDetails(int financialYearId, string portCode);

       List<DredgingPriorityVolumeVO> GetDredgingPriorityVolumes(int financialYearId, string portCode);
       List<DredgingPriorityVO> GetBerthTypes(string portCode);
       List<DredgingPriorityVO> GetLocationTypes(string portCode);
       DredgingPriority GetDredgingPriorityDetailsById(string dredging);
       List<DredgingPriorityVO> GetDredgingPriorityPendingView(int dredgingPriorityId);
       DredgingOperation GetDredgingPriorityApproveId(string dredgingPriorityAreaId);
       List<FinancialYearVO> GetFinancialYear();
       List<FinancialYearVO> GetMonths(int financialYearId);
       /// <summary>
       /// Author  : Sandeep Appana
       /// Date    : 30th Dec 2014
       /// Purpose : To get List of Berth Occupation details
       /// </summary>
       /// <returns></returns>
       List<DredgingOperationVO> GetBerthOccupationList();

       List<DredgingOperationVO> GetBerthOccupationById(int id);
        /// <summary>
        /// Author  : Sandeep Appana
        /// Date    : 1st Jan 2015
        /// Purpose : To get List of Dredging Volume details
        /// </summary>
        /// <returns></returns>
       List<DredgingOperationVO> GetDredgingVolumeList();
       List<DredgingOperationVO> GetDredgingPriorityAreaDetails(int dredgingPriorityId);

       List<DredgingOperationVO> GetDredgingPriorityAreaDetailsPending(int dredgingPriorityId);

       List<DredgingOperationVO> GetDredgingVolumeById(int id);

       List<DredgingPriorityDocumentVO> GetDocument(int dredgingPriorityId);
    }
}

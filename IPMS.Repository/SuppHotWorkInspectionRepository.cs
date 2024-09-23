using Core.Repository;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using System.Data.SqlClient;
using IPMS.Domain.DTOS;

namespace IPMS.Repository
{
    public class SuppHotWorkInspectionRepository : ISuppHotWorkInspectionRepository
    {
        private IUnitOfWork _unitOfWork;

        public SuppHotWorkInspectionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //public List<SuppHotWorkInspectionVO> AllSuppHotWorkInspectionDetails()
        //{



        //    var AllSuppHotWorkInspectionDetails = (from hw in _unitOfWork.Repository<SuppHotWorkInspection>().Query()
        //                                           .Include(hw => hw.SuppServiceRequest)
        //                                           .Include(hw => hw.SuppServiceRequest.ArrivalNotification)
        //                                           .Include(hw => hw.SuppServiceRequest.ArrivalNotification.Vessel)
        //                                           .Include(hw => hw.SuppServiceRequest.ArrivalNotification.SubCategory)
        //                                           .Select()
        //                                           select hw).ToList();



        //    return AllSuppHotWorkInspectionDetails.MapToDTO();


        //}

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 22st August 2014
        /// Purpose : To Get Supplementary Service Request details
        /// </summary>
        /// <returns></returns>
        public List<SuppHotWorkInspectionVO> AllSuppHotWorkInspectionDetails()
        {
            var servicerequestlist1 = (from sr in _unitOfWork.Repository<SuppServiceRequest>().Query()
                                      //.Include(sr => sr.WorkflowInstance)
                                      //.Include(sr => sr.WorkflowInstance.SubCategory)
                                      .Include(sr => sr.SuppHotWorkInspections)
                                           //.Include(sr => sr.SuppFloatingCranes)
                                           //.Include(sr => sr.SuppHotColdWorkPermits)
                                           //.Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments))
                                           //.Include(sr => sr.SuppHotColdWorkPermits.Select(d => d.SuppHotColdWorkPermitDocuments.Select(s => s.Document)))
                                      .Include(sr => sr.ArrivalNotification)
                                      .Include(sr => sr.ArrivalNotification.Vessel)
                                      .Include(sr => sr.ArrivalNotification.ArrivalCommodities)
                                      .Include(sr => sr.SubCategory)

                                      .Select()

                                       where sr.ServiceType == "HWST"
                                       orderby sr.ModifiedDate descending

                                       select sr).ToList();
            List<SuppHotWorkInspectionVO> servicerequestlist = new List<SuppHotWorkInspectionVO>();

            return servicerequestlist;
        }
    }
}

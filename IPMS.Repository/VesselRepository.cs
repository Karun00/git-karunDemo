using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace IPMS.Repository
{
    public class VesselRepository : IPMS.Repository.IVesselRepository
    {
        private IUnitOfWork _unitOfWork;

        public VesselRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<Vessel> GetVesselDetails()
        {
            var VesselDetails = _unitOfWork.Repository<Vessel>().Queryable().OrderBy(x => x.VesselName);
            return VesselDetails.ToList();
        }

        public List<Vessel> VesselDeetailsAutoComplete(string vslname)
        {
            var VesselDetails = (from v in _unitOfWork.Repository<Vessel>().Query().Select()
                                 where v.RecordStatus == "A" && v.VesselName.StartsWith(vslname)
                                 select v);

            return VesselDetails.ToList();
        }


        public List<VesselVO> VesselDeetailsAutoCompleteforpilot(string searchValue)
        {

            //   var portcode = new SqlParameter("@p_PortCode", PortCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);

            var _VesselInfo = _unitOfWork.SqlQuery<VesselVO>("dbo.usp_GetPilotVessel @p_SearchText", Searchvalue).ToList();

            return _VesselInfo;

            //var VesselDetails = (from v in _unitOfWork.Repository<Vessel>().Query().Select()
            //                     //where v.RecordStatus == "A" && v.VesselName.StartsWith(vslname)
            //                     where v.RecordStatus == "A" && v.IsFinal == "Y" && v.VesselName.ToLower().Contains(searchValue) 
            //                     select v);

            //return VesselDetails.ToList();

        }


        public List<VesselVO> GetVesselDetailsWitDryDoc(string PortCode, string searchValue, string SerchColumn)
        {

             var portcode = new SqlParameter("@p_PortCode", PortCode);
            var Searchvalue = new SqlParameter("@p_SearchText", searchValue);
            var serchColumn = new SqlParameter("@p_SrchOn", SerchColumn);

            var _VesselInfo = _unitOfWork.SqlQuery<VesselVO>("dbo.usp_GetVesselSearch @p_SearchText, @p_PortCode, @p_SrchOn", Searchvalue, portcode, serchColumn).ToList();


            //_VesselInfo = (from t in _unitOfWork.Repository<Vessel>().Query()
            //            .Include(t => t.DockingPlans).Include(t =>t.SubCategory3).Include(t =>t.SubCategory2).Select()
            //               orderby t.VesselName
            //               where t.RecordStatus == "A" && t.IsFinal == "Y" && (t.VesselName.ToLower().Contains(searchValue) || t.IMONo.ToLower().Contains(searchValue) || t.CallSign.ToLower().Contains(searchValue))
            //             select t).ToList<Vessel>();         

           
            return _VesselInfo;



        }


    }
}

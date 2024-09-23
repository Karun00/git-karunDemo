using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace IPMS.Repository
{
    public class VesselCallRepository:IVesselCallRepository
    {
         private IUnitOfWork _unitOfWork;

         public VesselCallRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public VesselCall VesselCallDetails(string VCN)
        {
            var vcalldtls = (from an in _unitOfWork.Repository<VesselCall>().Queryable().Where(an => an.VCN == VCN)
                             //where an.VCN == VCN
                             select an).FirstOrDefault<VesselCall>();
            return vcalldtls;
           
        }

        public VesselCallAnchorageVO GetVesselCallDetailsById(string vesselcallID)
        {
            var vesselid = Convert.ToInt32(vesselcallID, CultureInfo.InvariantCulture);
            var data = (from vc in _unitOfWork.Repository<VesselCall>().Queryable().Where(vc => vc.VesselCallID == vesselid)
                        join an in _unitOfWork.Repository<ArrivalNotification>().Queryable() on vc.VCN equals an.VCN
                        join vs in _unitOfWork.Repository<Vessel>().Queryable() on an.VesselID equals vs.VesselID                       
                        select new VesselCallAnchorageVO
                        {                            
                            VCN = vc.VCN,
                            VesselName = vs.VesselName,
                            PortLimitIn = vc.PortLimitIn,
                            BreakWaterIn = vc.BreakWaterIn,
                            BreakWaterOut = vc.BreakWaterOut,
                            PortLimitOut = vc.PortLimitOut,
                            PortCode = an.PortCode,
                            CreatedBy = an.CreatedBy,                           

                        }).FirstOrDefault<VesselCallAnchorageVO>();
    
            return data;
        }


        public VesselCallAnchorageVO GetVesselCallAnchorageDetailsById(string vesselcallAnchorageID)
        {
            var vesselcallid = Convert.ToInt32(vesselcallAnchorageID, CultureInfo.InvariantCulture);
            var data = (from vc in _unitOfWork.Repository<VesselCallAnchorage>().Queryable().Where(vc => vc.VesselCallAnchorageID == vesselcallid)
                        join an in _unitOfWork.Repository<ArrivalNotification>().Queryable() on vc.VCN equals an.VCN
                        join vs in _unitOfWork.Repository<Vessel>().Queryable() on an.VesselID equals vs.VesselID
                        join sb in _unitOfWork.Repository<SubCategory>().Queryable() on vc.Reason equals sb.SubCatCode into sublInfo
                        from subcat in sublInfo.DefaultIfEmpty()
                        //join sb in _unitOfWork.Repository<SubCategory>().Queryable() on vc.Reason equals sb.SubCatCode
                        select new VesselCallAnchorageVO
                        {
                            VCN = vc.VCN,
                            VesselName = vs.VesselName,
                            AnchorDropDateTime = vc.AnchorDropTime,
                            AnchorAweighDateTime = vc.AnchorAweighTime,
                            //AnchorDropTime = vc.AnchorDropTime != null ? vc.AnchorDropTime.ToString() : string.Empty,
                            //AnchorAweighTime = vc.AnchorAweighTime.HasValue ? vc.AnchorAweighTime.ToString() : string.Empty,
                            BearingDistanceFromBreakWater = vc.BearingDistanceFromBreakWater,
                            Reason = subcat.SubCatName,
                            PortCode = an.PortCode,
                            CreatedBy = an.CreatedBy,
                        }).FirstOrDefault<VesselCallAnchorageVO>();


            //data.AnchorDropTime = data.AnchorDropTime != "" ? Convert.ToDateTime(data.AnchorDropTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;
            //data.AnchorAweighTime = data.AnchorAweighTime != "" ? Convert.ToDateTime(data.AnchorAweighTime, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) : null;          

            return data;
        }     
       
    }
}

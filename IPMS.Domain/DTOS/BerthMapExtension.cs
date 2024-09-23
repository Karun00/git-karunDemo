using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class BerthMapExtension
    {


        public static List<BerthVO> MapToListDtoMobile(this IEnumerable<Berth> berthList)
        {
            List<BerthVO> berthvoList = new List<BerthVO>();
            if (berthList != null)
                foreach (var data in berthList)
                {
                    berthvoList.Add(data.MapToDtoMobile());

                }
            return berthvoList;
        }

        public static List<BerthVO> MapToListDto(this IEnumerable<Berth> berthList)
        {
            List<BerthVO> berthvoList = new List<BerthVO>();
            if (berthList != null)
                foreach (var data in berthList)
                {
                    berthvoList.Add(data.MapToDto());

                }
            return berthvoList;
        }

        public static List<Berth> MapToListEntity(this IEnumerable<BerthVO> berthVoList)
        {
            List<Berth> berthList = new List<Berth>();
            if (berthVoList != null)
                foreach (var data in berthVoList)
                {
                    berthList.Add(data.MapToEntity());
                }
            return berthList;
        }

        public static BerthVO MapToDto(this Berth data)
        {
            BerthVO berthvo = new BerthVO();
            if (data != null)
            {
                berthvo.PortCode = data.PortCode;
                berthvo.PortName = null;
                berthvo.QuayCode = data.QuayCode;
                berthvo.QuayName = null;
                berthvo.BerthCode = data.BerthCode.ToUpper();
                berthvo.BerthName = data.BerthName;
                berthvo.ShortName = data.ShortName;
                berthvo.BerthType = data.BerthType;
                berthvo.BerthTypeName = null;
                berthvo.CargoType = null;
                berthvo.FromMeter = data.FromMeter;
                berthvo.ToMeter = data.ToMeter;
                berthvo.Lengthm = data.Lengthm;
                berthvo.Draftm = data.Draftm;
                berthvo.TidalDraft = data.TidalDraft;
                berthvo.RecordStatus = data.RecordStatus;
                berthvo.CreatedBy = data.CreatedBy;
                berthvo.CreatedDate = data.CreatedDate;
                berthvo.ModifiedBy = data.ModifiedBy;
                berthvo.ModifiedDate = data.ModifiedDate;
                berthvo.BerthKey = data.PortCode + "." + data.QuayCode + "." + data.BerthCode;

                if (data.Bollards != null)
                {
                    berthvo.Bollards = new List<BollardVO>();

                    var bollards = data.Bollards.OrderBy(l => l.FromMeter);


                    foreach (var bollard in bollards)
                    {
                        var bollardvo = bollard.MapToDTO();
                        berthvo.Bollards.Add(bollardvo);
                    }
                }
            }
            return berthvo;
        }

        public static BerthVO MapToDtoMobile(this Berth data)
        {
            BerthVO berthvo = new BerthVO();
            if (data != null)
            {
                berthvo.PortCode = data.PortCode;
                berthvo.PortName = null;
                berthvo.QuayCode = data.QuayCode;
                berthvo.QuayName = null;
                berthvo.BerthCode = data.BerthCode;
                berthvo.BerthName = data.BerthName;
                berthvo.ShortName = data.ShortName;
                berthvo.BerthType = data.BerthType;
                berthvo.BerthTypeName = null;
                berthvo.CargoType = null;
                berthvo.FromMeter = data.FromMeter;
                berthvo.ToMeter = data.ToMeter;
                berthvo.Lengthm = data.Lengthm;
                berthvo.Draftm = data.Draftm;
                berthvo.RecordStatus = data.RecordStatus;
                berthvo.CreatedBy = data.CreatedBy;
                berthvo.CreatedDate = data.CreatedDate;
                berthvo.ModifiedBy = data.ModifiedBy;
                berthvo.ModifiedDate = data.ModifiedDate;
                berthvo.BerthKey = data.PortCode + "." + data.QuayCode + "." + data.BerthCode;
                berthvo.TidalDraft = data.TidalDraft;
                if (data.BerthCargoes != null)
                {
                    foreach (var berthCargo in data.BerthCargoes)
                    {
                        if (berthCargo.SubCategory != null)
                        {
                            if (!string.IsNullOrEmpty(berthCargo.SubCategory.SubCatName))
                            {
                                if (string.IsNullOrWhiteSpace(berthvo.CargoDetails))
                                {
                                    berthvo.CargoDetails = berthCargo.SubCategory.SubCatName;
                                }
                                else
                                {
                                    berthvo.CargoDetails = berthvo.CargoDetails + ", " +
                                                           berthCargo.SubCategory.SubCatName;
                                }
                            }
                        }
                    }
                }

                if (data.Bollards != null && data.Bollards.Count > 0)
                {
                    berthvo.FirstBolardName = data.Bollards.OrderBy(T => T.FromMeter).FirstOrDefault().BollardName;
                    berthvo.LastBollardName = data.Bollards.OrderBy(T => T.FromMeter).LastOrDefault().BollardName;
                }

            }
            return berthvo;
        }

        public static Berth MapToEntity(this BerthVO berthVo)
        {
            Berth berth = new Berth();
            if (berthVo != null)
            {
                berth.PortCode = berthVo.PortCode;
                berth.QuayCode = berthVo.QuayCode;
                berth.BerthCode = berthVo.BerthCode.ToUpper();
                berth.BerthName = berthVo.BerthName;
                berth.ShortName = berthVo.ShortName;
                berth.BerthType = berthVo.BerthType;
                berth.FromMeter = berthVo.FromMeter;
                berth.ToMeter = berthVo.ToMeter;
                berth.Lengthm = berthVo.Lengthm;
                berth.Draftm = berthVo.Draftm;
                berth.TidalDraft = berthVo.TidalDraft;
                berth.RecordStatus = berthVo.RecordStatus;

                berth.CreatedBy = berthVo.CreatedBy;
                berth.CreatedDate = berthVo.CreatedDate;
                berth.ModifiedBy = berthVo.ModifiedBy;
                berth.ModifiedDate = berthVo.ModifiedDate;

                if (berthVo.CargoType != null)
                {
                    List<BerthCargo> berthcagoes = new List<BerthCargo>();
                    foreach (var cargotype in berthVo.CargoType)
                    {
                        BerthCargo obj = new BerthCargo();
                        obj.BerthCargoID = 0;
                        obj.PortCode = berthVo.PortCode;
                        obj.QuayCode = berthVo.QuayCode;
                        obj.BerthCode = berthVo.BerthCode.ToUpper();
                        obj.CargoTypeCode = cargotype;
                        obj.RecordStatus = berthVo.RecordStatus;
                        obj.CreatedBy = berthVo.CreatedBy;
                        obj.CreatedDate = berthVo.CreatedDate;
                        obj.ModifiedBy = berthVo.ModifiedBy;
                        obj.ModifiedDate = berthVo.ModifiedDate;

                        berthcagoes.Add(obj);
                    }
                    berth.BerthCargoes = berthcagoes;
                }

                if (berthVo.VesselType != null)
                {
                    List<BerthVesselType> berthvessels = new List<BerthVesselType>();
                    foreach (var vesseltype in berthVo.VesselType)
                    {
                        BerthVesselType obj = new BerthVesselType();
                        obj.BerthVesselTypeID = 0;
                        obj.PortCode = berthVo.PortCode;
                        obj.QuayCode = berthVo.QuayCode;
                        obj.BerthCode = berthVo.BerthCode.ToUpper();
                        obj.VesselTypeCode = vesseltype;
                        obj.RecordStatus = berthVo.RecordStatus;
                        obj.CreatedBy = berthVo.CreatedBy;
                        obj.CreatedDate = berthVo.CreatedDate;
                        obj.ModifiedBy = berthVo.ModifiedBy;
                        obj.ModifiedDate = berthVo.ModifiedDate;

                        berthvessels.Add(obj);
                    }
                    berth.BerthVesselTypes = berthvessels;
                }

                if (berthVo.ReasonForVisitType != null)
                {
                    List<BerthReasonForVisit> berthreasons = new List<BerthReasonForVisit>();
                    foreach (var reasontype in berthVo.ReasonForVisitType)
                    {
                        BerthReasonForVisit obj = new BerthReasonForVisit();
                        obj.BerthReasonForVisitID = 0;
                        obj.PortCode = berthVo.PortCode;
                        obj.QuayCode = berthVo.QuayCode;
                        obj.BerthCode = berthVo.BerthCode.ToUpper();
                        obj.ReasonForVisitCode = reasontype;
                        obj.RecordStatus = berthVo.RecordStatus;
                        obj.CreatedBy = berthVo.CreatedBy;
                        obj.CreatedDate = berthVo.CreatedDate;
                        obj.ModifiedBy = berthVo.ModifiedBy;
                        obj.ModifiedDate = berthVo.ModifiedDate;

                        berthreasons.Add(obj);
                    }
                    berth.BerthReasonForVisits = berthreasons;
                }
            }
            return berth;
        }


        public static List<BerthVO> MapToDtoforArrivalBerths(this IEnumerable<Berth> berthList)
        {
            List<BerthVO> berthvoList = new List<BerthVO>();
            if (berthList != null)
                foreach (var data in berthList)
                {
                    berthvoList.Add(data.MapToDtoforArrivalBerth());

                }
            return berthvoList;
        }


        public static BerthVO MapToDtoforArrivalBerth(this Berth data)
        {
            BerthVO berthvo = new BerthVO();
            if (data != null)
            {
                berthvo.BerthName = data.BerthName;
                berthvo.BerthKey = data.PortCode + "." + data.QuayCode + "." + data.BerthCode;
                berthvo.Draftm = data.Draftm;
                if (data.BerthCargoes != null)
                {
                    foreach (var berthCargo in data.BerthCargoes)
                    {
                        if (string.IsNullOrWhiteSpace(berthvo.CargoDetails))
                        {
                            berthvo.CargoDetails = berthCargo.CargoTypeCode;
                        }
                        else
                        {
                            berthvo.CargoDetails = berthvo.CargoDetails + "," + berthCargo.CargoTypeCode;
                        }
                    }
                }
            }
            return berthvo;
        }



    }
}

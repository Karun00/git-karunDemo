using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class Section625EMapExtension
    {
        public static List<Section625EVO> MapToDTO(this IEnumerable<Section625E> entities)
        {
            List<Section625EVO> Vos = new List<Section625EVO>();
            foreach (var item in entities)
            {
                Vos.Add(item.MapToDTO());
            }
            return Vos;
        }
        public static List<Section625E> MapToEntity(this IEnumerable<Section625EVO> Vos)
        {
            List<Section625E> entities = new List<Section625E>();
            foreach (var item in Vos)
            {
                entities.Add(item.MapToEntity());
            }

            return entities;
        }

        public static Section625EVO MapToDTO(this Section625E data)
        {
            Section625EVO Vo = new Section625EVO();
            Vo.Section625EID = data.Section625EID;
            Vo.Section625ABCDID = data.Section625ABCDID;
            Vo.Hour24Report625ID = data.Hour24Report625ID;
            Vo.IncidentDateTime = data.IncidentDateTime;
            if (data.TimeReported != null)
            {
                Vo.TimeReported = Convert.ToDateTime(data.TimeReported, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture);
            }
            Vo.OwnerNameofStolenItem = data.OwnerNameofStolenItem;
            Vo.OwnerAddress = data.OwnerAddress;
            Vo.TelephoneNo = data.TelephoneNo;
            Vo.MobileNo = data.MobileNo;
            Vo.EmailID = data.EmailID;
            Vo.IDWhenandWhereStolenDateTime = data.IDWhenandWhereStolenDateTime;
            Vo.IDWhenandWhereStolenLocation = data.IDWhenandWhereStolenLocation;
            Vo.IDWhenWasDiscoveredDateTime = data.IDWhenWasDiscoveredDateTime;
            Vo.IDWhenWasDiscoveredLocation = data.IDWhenWasDiscoveredLocation;
            Vo.TheftOccur = data.TheftOccur;
            Vo.StolenFromBuilding = data.StolenFromBuilding;
            Vo.ISPSBreach = data.ISPSBreach;
            Vo.ProtectTheft = data.ProtectTheft;
            Vo.Circumstances = data.Circumstances;
            Vo.TheftAvoided = data.TheftAvoided;
            Vo.PoliceAdviced = data.PoliceAdviced;
            Vo.SAPSOBNumber = data.SAPSOBNumber;
            Vo.PoliceStationReportedTo = data.PoliceStationReportedTo;
            Vo.RecordStatus = data.RecordStatus;
            Vo.CreatedBy = data.CreatedBy;
            Vo.CreatedDate = data.CreatedDate;
            Vo.ModifiedBy = data.ModifiedBy;
            Vo.ModifiedDate = data.ModifiedDate; 

            return Vo;

        }
        public static Section625E MapToEntity(this Section625EVO VO)
        {
            Section625E data = new Section625E();
            data.Section625EID = VO.Section625EID;
            data.Section625ABCDID = VO.Section625ABCDID;
            data.Hour24Report625ID = VO.Hour24Report625ID;
            data.IncidentDateTime = VO.IncidentDateTime;
            if (VO.TimeReported != "")
            {
                data.TimeReported = DateTime.Parse(Convert.ToDateTime(VO.TimeReported, CultureInfo.InvariantCulture).ToString("HH:mm", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            }
            data.OwnerNameofStolenItem = VO.OwnerNameofStolenItem;
            data.OwnerAddress = VO.OwnerAddress;
            data.TelephoneNo = VO.TelephoneNo;
            data.MobileNo = VO.MobileNo;
            data.EmailID = VO.EmailID;
            data.IDWhenandWhereStolenDateTime = VO.IDWhenandWhereStolenDateTime;
            data.IDWhenandWhereStolenLocation = VO.IDWhenandWhereStolenLocation;
            data.IDWhenWasDiscoveredDateTime = VO.IDWhenWasDiscoveredDateTime;
            data.IDWhenWasDiscoveredLocation = VO.IDWhenWasDiscoveredLocation;
            data.TheftOccur = VO.TheftOccur;
            data.StolenFromBuilding = VO.StolenFromBuilding;
            data.ISPSBreach = VO.ISPSBreach;
            data.ProtectTheft = VO.ProtectTheft;
            data.Circumstances = VO.Circumstances;
            data.TheftAvoided = VO.TheftAvoided;
            data.PoliceAdviced = VO.PoliceAdviced;
            data.SAPSOBNumber = VO.SAPSOBNumber;
            data.PoliceStationReportedTo = VO.PoliceStationReportedTo;
            data.RecordStatus = VO.RecordStatus;
            data.CreatedBy = VO.CreatedBy;
            data.CreatedDate = VO.CreatedDate;
            data.ModifiedBy = VO.ModifiedBy;
            data.ModifiedDate = VO.ModifiedDate;
            return data;

        }

    }
}

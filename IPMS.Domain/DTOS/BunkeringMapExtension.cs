using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Domain.DTOS
{
    public static class BunkeringMapExtension
    {


        public static BunkeringVO MapToDTO(this Bunkering data)
        {
            BunkeringVO BunkeringVO = new BunkeringVO();
            if (data != null)
            {
                BunkeringVO.LicenseRequestID = data.LicenseRequestID;
                BunkeringVO.BunkeringID = data.BunkeringID;
                BunkeringVO.ProvideBunkeringPorts = data.ProvideBunkeringPorts;
                BunkeringVO.YearsProvidingBunkering = data.YearsProvidingBunkering;
                BunkeringVO.GenlHealthSafetyCertificate = data.GenlHealthSafetyCertificate;
                BunkeringVO.EmployeesSelfInflating = data.EmployeesSelfInflating;
                BunkeringVO.CreatedBy = data.CreatedBy;
                BunkeringVO.RecordStatus = data.RecordStatus;
                BunkeringVO.CreatedDate = data.CreatedDate;
                BunkeringVO.ModifiedBy = data.ModifiedBy;
                BunkeringVO.ModifiedDate = data.ModifiedDate;
            }
            return BunkeringVO;
        }
        public static Bunkering MapToEntity(this BunkeringVO Bunkeringvo)
        {
            Bunkering Bunkering = new Bunkering();
            if (Bunkeringvo != null)
            {
                Bunkering.LicenseRequestID = Bunkeringvo.LicenseRequestID;
                Bunkering.BunkeringID = Bunkeringvo.BunkeringID;
                Bunkering.ProvideBunkeringPorts = Bunkeringvo.ProvideBunkeringPorts;
                Bunkering.YearsProvidingBunkering = Bunkeringvo.YearsProvidingBunkering;
                Bunkering.QualifyPublicLiabilityInsu = Bunkeringvo.QualifyPublicLiabilityInsu;


                Bunkering.GenlHealthSafetyCertificate = Bunkeringvo.GenlHealthSafetyCertificate;
                Bunkering.EmployeesSelfInflating = Bunkeringvo.EmployeesSelfInflating;
                Bunkering.CreatedBy = Bunkeringvo.CreatedBy;
                Bunkering.RecordStatus = Bunkeringvo.RecordStatus;
                Bunkering.CreatedDate = Bunkeringvo.CreatedDate;
                Bunkering.ModifiedBy = Bunkeringvo.ModifiedBy;
                Bunkering.ModifiedDate = Bunkeringvo.ModifiedDate;
            }
            return Bunkering;
        }


        public static BunkeringVO MapToDTOObj(this IEnumerable<Bunkering> bunkerings)
        {
            var bunkeringVoList = new BunkeringVO();
       //     BunkeringVO BunkeringVO = new BunkeringVO();
            if (bunkerings != null)
            {
                foreach (var item in bunkerings)
                {

                    bunkeringVoList.LicenseRequestID = item.LicenseRequestID;
                    bunkeringVoList.BunkeringID = item.BunkeringID;
                    bunkeringVoList.ProvideBunkeringPorts = item.ProvideBunkeringPorts;
                    bunkeringVoList.YearsProvidingBunkering = item.YearsProvidingBunkering;
                    bunkeringVoList.QualifyPublicLiabilityInsu = item.QualifyPublicLiabilityInsu;
                    bunkeringVoList.GenlHealthSafetyCertificate = item.GenlHealthSafetyCertificate;
                    bunkeringVoList.EmployeesSelfInflating = item.EmployeesSelfInflating;
                    bunkeringVoList.CreatedBy = item.CreatedBy;
                    bunkeringVoList.RecordStatus = item.RecordStatus;
                    bunkeringVoList.CreatedDate = item.CreatedDate;
                    bunkeringVoList.ModifiedBy = item.ModifiedBy;
                    bunkeringVoList.ModifiedDate = item.ModifiedDate;

                }
            }
            return bunkeringVoList;
        }

        public static List<Bunkering> MapToEntity(this IEnumerable<BunkeringVO> bunkeringList)
        {
            var bunkerings = new List<Bunkering>();
            if (bunkeringList != null)
            {
                foreach (var item in bunkeringList)
                {
                    bunkerings.Add(item.MapToEntity());
                }
            }
            return bunkerings;

        }
    }
}

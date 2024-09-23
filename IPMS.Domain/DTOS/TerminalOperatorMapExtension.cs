using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace IPMS.Domain.DTOS
{
    public static class TerminalOperatorMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="terminalOperators"></param>
        /// <returns></returns>
        public static List<TerminalOperatorVO> MapToDto(this List<TerminalOperator> terminalOperators)
        {
            List<TerminalOperatorVO> terminalOperatorVos = new List<TerminalOperatorVO>();
            if (terminalOperators != null)
            {
                foreach (var terminalOperator in terminalOperators)
                {
                    terminalOperatorVos.Add(terminalOperator.MapToDto());
                }
            }
            return terminalOperatorVos;
        }

        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TerminalOperatorVO MapToDto(this TerminalOperator data)
        {
            TerminalOperatorVO terminaloperatorVO = new TerminalOperatorVO();
            if (data != null)
            {
                terminaloperatorVO.TerminalOperatorID = data.TerminalOperatorID;
                terminaloperatorVO.RegisteredName = data.RegisteredName;
                terminaloperatorVO.RegistrationDate = data.RegistrationDate.ToString();
                terminaloperatorVO.TradingName = data.TradingName;
                terminaloperatorVO.RegistrationNumber = data.RegistrationNumber;
                terminaloperatorVO.ValidityDate = data.ValidityDate.ToString();
                terminaloperatorVO.PremiseLocation = data.PremiseLocation;
                terminaloperatorVO.BusinessAddressID = data.BusinessAddressID;
                terminaloperatorVO.PostalAddressID = data.PostalAddressID;
                terminaloperatorVO.TelephoneNo1 = data.TelephoneNo1;
                terminaloperatorVO.FaxNo = data.FaxNo;
                terminaloperatorVO.RecordStatus = data.RecordStatus;
                terminaloperatorVO.CreatedBy = data.CreatedBy;
                terminaloperatorVO.CreatedDate = data.CreatedDate;
                terminaloperatorVO.ModifiedBy = data.ModifiedBy;
                terminaloperatorVO.ModifiedDate = data.ModifiedDate;
                terminaloperatorVO.BusinessAddress = data.BusinessAddress.MapToDTO();
                terminaloperatorVO.PostalAddress = data.PostalAddress.MapToDTO();
                terminaloperatorVO.OperationalBerths = data.TerminalOperatorBerths.MapToBerthKeysArray();
                terminaloperatorVO.OperationalCargotypes = data.TerminalOperatorCargoHandlings.MapToCargokeysArray();
                terminaloperatorVO.TerminalOperatorPortsArr = data.TerminalOperatorPorts.MapToPortArrayT();
                terminaloperatorVO.IsSameBusinessAdd = CheckUncheck(terminaloperatorVO.BusinessAddress,
                    terminaloperatorVO.PostalAddress);

                List<string> TempArray = new List<string>();
                if (data.LicensedFor == "BOTH")
                {
                    terminaloperatorVO.LicenLicensedForString = "Berths/Cargo Handling";
                    TempArray.Add("TOB");
                    TempArray.Add("TOCH");
                }
                else
                {
                    if (data.LicensedFor == "TOB")
                    {
                        terminaloperatorVO.LicenLicensedForString = "Berths";
                    }
                    else
                    {
                        terminaloperatorVO.LicenLicensedForString = "Cargo Handling";
                    }

                    TempArray.Add(data.LicensedFor);
                }
                terminaloperatorVO.TerminalOperatorServiceTypesArray = TempArray;
            }
            return terminaloperatorVO;
        }

        /// <summary>
        /// Data Transfer from DTO Entity
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static TerminalOperator MapToEntity(this TerminalOperatorVO vo)
        {
            TerminalOperator terminaloperator = new TerminalOperator();
            if (vo != null)
            {
                terminaloperator.TerminalOperatorID = vo.TerminalOperatorID;
                terminaloperator.RegisteredName = vo.RegisteredName;
                terminaloperator.RegistrationDate = DateTime.Parse(vo.RegistrationDate, CultureInfo.InvariantCulture);
                terminaloperator.TradingName = vo.TradingName;
                terminaloperator.RegistrationNumber = vo.RegistrationNumber;
                terminaloperator.ValidityDate = DateTime.Parse(vo.ValidityDate, CultureInfo.InvariantCulture);
                terminaloperator.PremiseLocation = vo.PremiseLocation;
                terminaloperator.BusinessAddressID = vo.BusinessAddressID;
                terminaloperator.PostalAddressID = vo.PostalAddressID;
                terminaloperator.BusinessAddress = vo.BusinessAddress.MapToEntity();
                terminaloperator.PostalAddress = vo.PostalAddress.MapToEntity();
                terminaloperator.TelephoneNo1 = vo.TelephoneNo1;
                terminaloperator.FaxNo = vo.FaxNo;
                terminaloperator.RecordStatus = vo.RecordStatus;
                terminaloperator.CreatedBy = vo.CreatedBy;
                terminaloperator.CreatedDate = vo.CreatedDate;
                terminaloperator.ModifiedBy = vo.ModifiedBy;
                terminaloperator.ModifiedDate = vo.ModifiedDate;
                terminaloperator.TerminalOperatorCargoHandlings =
                    vo.OperationalCargotypes.MapToEntityTerminalOperatorCargoes(vo.TerminalOperatorID);
                terminaloperator.TerminalOperatorBerths =
                    vo.OperationalBerths.MapToEntityTerminalOperatorBerths(vo.TerminalOperatorID);
                terminaloperator.TerminalOperatorPorts =
                    vo.TerminalOperatorPortsArr.MapToEntityPortT(vo.TerminalOperatorID);
            }
            return terminaloperator;
        }

        public static bool CheckUncheck(AddressVO ResidentialAddress, AddressVO PostalAddress)
        {
            int check = 4;
            bool AddressCheckbox = false;
            if (ResidentialAddress != null)
            {
                
                if ((ResidentialAddress.NumberStreet.ToString()).Trim() ==
                    (PostalAddress.NumberStreet.ToString()).Trim())
                {
                    check = check - 1;
                }
                if ((ResidentialAddress.Suburb.ToString()).Trim() == (PostalAddress.Suburb.ToString()).Trim())
                {
                    check = check - 1;
                }
                if ((ResidentialAddress.TownCity.ToString()).Trim() == (PostalAddress.TownCity.ToString()).Trim())
                {
                    check = check - 1;
                }
                if ((ResidentialAddress.PostalCode.ToString()).Trim() == (PostalAddress.PostalCode.ToString()).Trim())
                {
                    check = check - 1;
                }
                if (check <= 0)
                {
                    AddressCheckbox = true;
                }
                else
                {
                    AddressCheckbox = false;
                }
            }
            return AddressCheckbox;
        }
    }
}

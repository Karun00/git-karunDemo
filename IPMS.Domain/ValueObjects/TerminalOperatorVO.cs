using System;
using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for TerminalOperator
    /// </summary>
    public class TerminalOperatorVO
    {
        public int TerminalOperatorID { get; set; }
        public string RegisteredName { get; set; }
        public string TradingName { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string ValidityDate { get; set; }

        public string PremiseLocation { get; set; }
        public int BusinessAddressID { get; set; }
        public Nullable<int> PostalAddressID { get; set; }
        public AddressVO BusinessAddress { get; set; }
        public AddressVO PostalAddress { get; set; }
        public List<string> OperationalBerths { get; set; }
        public List<string> OperationalCargotypes { get; set; }

        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string TelephoneNo1 { get; set; }
        public string FaxNo { get; set; }
        public List<string> TerminalOperatorPortsArr { get; set; }
        public List<string> TerminalOperatorServiceTypesArray { get; set; }

        public string LicenLicensedForString { get; set; }
        public TerminalOperatorPortVO TerminalOperatorPorts { get; set; }

        public bool IsSameBusinessAdd { get; set; }
    }



}

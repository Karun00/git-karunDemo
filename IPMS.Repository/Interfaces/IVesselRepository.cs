using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
namespace IPMS.Repository
{
    public interface IVesselRepository
    {
        List<Vessel> GetVesselDetails();
        List<Vessel> VesselDeetailsAutoComplete(string vslname);
        List<VesselVO> GetVesselDetailsWitDryDoc(string PortName, string searchValue, string SerchColumn);
        List<VesselVO> VesselDeetailsAutoCompleteforpilot(string searchValue);
    }
}

using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IVesselCallRepository
    {
        VesselCall VesselCallDetails(string vcn);

        VesselCallAnchorageVO GetVesselCallAnchorageDetailsById(string vesselcallAnchorageID);

        VesselCallAnchorageVO GetVesselCallDetailsById(string vesselcallID);
    }
}

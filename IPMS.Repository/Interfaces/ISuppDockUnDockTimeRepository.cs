using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface ISuppDockUnDockTimeRepository
    {
        List<SuppDryDockVO> AllSuppDockUnDockTimeDetails(string portCode);

        SuppDryDock GetSuppDockUndockDetailsByID(string suppdrydockid);
    }
}

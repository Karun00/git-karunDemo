using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;

namespace IPMS.Repository
{
    public interface ISuppHotWorkInspectionRepository
    {
        List<SuppHotWorkInspectionVO> AllSuppHotWorkInspectionDetails();
    }
}

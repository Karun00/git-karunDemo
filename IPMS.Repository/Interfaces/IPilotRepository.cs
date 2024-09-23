using System;
using IPMS.Domain.Models;
using System.Collections.Generic;
using IPMS.Domain.ValueObjects;
namespace IPMS.Repository
{
   public interface IPilotRepository
    {
       List<PioltVO> GetListofPilots(string portCode);
       List<PioltVO> GetApprovedPilotsList(string portCode);
    }
}

using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repository
{
    public interface IUserPreferenceRepository
    {
        List<UserPreferenceVO> GetUserPreferenceDetails(int UserID);
        List<UserPreferenceVO> GetUserPreferenceDetailsByUser(int UserID);
        UserPreference GetUserPreferences(int UserID);
        
    }
}

using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace IPMS.Repository
{
    public class UserPreferenceRepository : IUserPreferenceRepository
    {
        private IUnitOfWork _unitOfWork;

        public UserPreferenceRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<UserPreferenceVO> GetUserPreferenceDetails(int UserID)
        {
            var userid1 = new SqlParameter("@userid", UserID);
            var param1 = new SqlParameter("@param1", 1);

            var UserPref1 = _unitOfWork.SqlQuery<UserPreferenceVO>("Select * from  dbo.udf_GetUserPreferencesDetails(@userid,@param1)", userid1, param1).ToList();

            return UserPref1;
        }

        public List<UserPreferenceVO> GetUserPreferenceDetailsByUser(int UserID)
        {
            var userid2 = new SqlParameter("@userid", UserID);
            var param2 = new SqlParameter("@param1", 2);

            var UserRole1 = _unitOfWork.SqlQuery<UserPreferenceVO>("Select * from  dbo.udf_GetUserPreferencesDetails(@userid,@param1)", userid2, param2).ToList();

            return UserRole1;
        }

        public UserPreference GetUserPreferences(int UserID)
        {
             var result = (from up in _unitOfWork.Repository<UserPreference>().Query().Select()
                           where up.UserID == UserID
                           select up);

             return result.FirstOrDefault<UserPreference>(); ;
           
        }

    }
}

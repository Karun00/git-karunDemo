using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Globalization;

namespace IPMS.Domain.DTOS
{
    public static class UserMapExtention
    {
        public static UserMasterVO MapToDTO(this User data)
        {
            UserMasterVO userMasterVo = new UserMasterVO();
            userMasterVo.UserID = data.UserID;
            userMasterVo.UserName = data.UserName;
            userMasterVo.UserType = data.UserType;
            userMasterVo.UserTypeID = data.UserTypeID;
            userMasterVo.ContactNo = data.ContactNo;
            userMasterVo.EmailID = data.EmailID;
            userMasterVo.FirstName = data.FirstName;
            userMasterVo.LastName = data.LastName;
            userMasterVo.CreatedBy = data.CreatedBy;
            userMasterVo.CreatedDate = data.CreatedDate;
            userMasterVo.RecordStatus = data.RecordStatus;
            userMasterVo.ModifiedBy = data.ModifiedBy;
            userMasterVo.ModifiedDate = data.ModifiedDate;
            userMasterVo.AnonymousUserYn = data.AnonymousUserYn;
            userMasterVo.WorkflowInstanceId = data.WorkflowInstanceId;
            userMasterVo.ReasonForAccess = data.ReasonForAccess;
            userMasterVo.PwdExpirtyDate = data.PwdExpirtyDate;
            DateTime? dt2 = data.ValidFromDate;
            userMasterVo.ValidFromDate = dt2.HasValue ? dt2.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty;

            DateTime? dt3 = data.ValidToDate;
            userMasterVo.ValidToDate = dt3.HasValue ? dt3.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : string.Empty;


            //userMasterVo.ValidToDate = Convert.ToString(data.ValidToDate, CultureInfo.InvariantCulture);
            return userMasterVo;
        }
        public static User MapToEntity(this UserMasterVO vo)
        {
            User user = new User();
            user.UserID = vo.UserID;
            user.UserName = vo.UserName;
            user.UserType = vo.UserType;
            user.UserTypeID = vo.UserTypeID;
            user.ContactNo = vo.ContactNo;
            user.EmailID = vo.EmailID;
            user.FirstName = vo.FirstName;
            user.LastName = vo.LastName;
            user.CreatedBy = vo.CreatedBy;
            user.CreatedDate = vo.CreatedDate;
            user.ModifiedBy = vo.ModifiedBy;
            user.ModifiedDate = vo.ModifiedDate;
            user.RecordStatus = vo.RecordStatus;
            user.Roles = vo.Roles.MapToEntity();
            user.AnonymousUserYn = vo.AnonymousUserYn;
            user.WorkflowInstanceId = vo.WorkflowInstanceId;
            user.ReasonForAccess = vo.ReasonForAccess;
            user.ValidFromDate = Convert.ToDateTime(vo.ValidFromDate, CultureInfo.InvariantCulture);
            user.ValidToDate = Convert.ToDateTime(vo.ValidToDate, CultureInfo.InvariantCulture);
            //user.r
            return user;
        }
        public static List<UserMasterVO> MapToListDTO(this IEnumerable<User> userList)
        {
            List<UserMasterVO> userMasterVOList = new List<UserMasterVO>();
            foreach (var user in userList)
            {
                UserMasterVO userMasterVO = new UserMasterVO();
                userMasterVO.UserID = user.UserID;
                userMasterVO.UserName = user.UserName;
                userMasterVO.UserType = user.UserType;
                userMasterVO.UserTypeID = user.UserTypeID;
                userMasterVO.FirstName = user.FirstName;
                userMasterVO.LastName = user.LastName;
                userMasterVO.EmailID = user.EmailID;
                userMasterVO.ContactNo = user.ContactNo;
                userMasterVO.CreatedBy = user.CreatedBy;
                userMasterVO.CreatedDate = user.CreatedDate;
                userMasterVO.ModifiedBy = user.ModifiedBy;
                userMasterVO.ModifiedDate = user.ModifiedDate;
                userMasterVO.Roles = user.Roles.MapToDto();

                userMasterVOList.Add(userMasterVO);
            }
            return userMasterVOList;
        }
        public static List<User> MapToEntity(this IEnumerable<UserMasterVO> userMasterVOList)
        {
            List<User> userList = new List<User>();
            foreach (var userVO in userMasterVOList)
            {
                User user = new User();
                user.UserID = userVO.UserID;
                user.UserName = userVO.UserName;
                user.UserType = userVO.UserType;
                user.UserTypeID = userVO.UserTypeID;
                user.FirstName = userVO.FirstName;
                user.LastName = userVO.LastName;
                user.EmailID = userVO.EmailID;
                user.ContactNo = userVO.ContactNo;
                user.CreatedBy = userVO.CreatedBy;
                user.CreatedDate = userVO.CreatedDate;
                user.ModifiedBy = userVO.ModifiedBy;
                user.ModifiedDate = userVO.ModifiedDate;
                user.Roles = userVO.Roles.MapToEntity();
                userList.Add(user);
            }
            return userList;
        }
    }
}

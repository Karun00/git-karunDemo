using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class AuthorizedContactPersonMapExtension
    {
        public static AuthorizedContactPersonVO MapToDTO(this AuthorizedContactPerson data)
        {
            AuthorizedContactPersonVO authContactPersonVo = new AuthorizedContactPersonVO();
            if (data != null)
            {
                authContactPersonVo.AuthorizedContactPersonID = data.AuthorizedContactPersonID;
                authContactPersonVo.AuthorizedContactPersonType = data.AuthorizedContactPersonType;
                authContactPersonVo.FirstName = data.FirstName;
                authContactPersonVo.SurName = data.SurName;
                authContactPersonVo.IdentityNo = data.IdentityNo;
                authContactPersonVo.Designation = data.Designation;
                authContactPersonVo.CellularNo = data.CellularNo;
                authContactPersonVo.EmailID = data.EmailID;
                authContactPersonVo.RecordStatus = data.RecordStatus;
                authContactPersonVo.CreatedBy = data.CreatedBy;
                authContactPersonVo.CreatedDate = data.CreatedDate;
                authContactPersonVo.ModifiedBy = data.ModifiedBy;
                authContactPersonVo.ModifiedDate = data.ModifiedDate;
            }
            return authContactPersonVo;
        }
        public static AuthorizedContactPerson MapToEntity(this AuthorizedContactPersonVO vo)
        {
            AuthorizedContactPerson authContactPerson = new AuthorizedContactPerson();
            if (vo != null)
            {
                authContactPerson.AuthorizedContactPersonID = vo.AuthorizedContactPersonID;
                authContactPerson.AuthorizedContactPersonType = vo.AuthorizedContactPersonType;
                authContactPerson.FirstName = vo.FirstName;
                authContactPerson.SurName = vo.SurName;
                authContactPerson.IdentityNo = vo.IdentityNo;
                authContactPerson.Designation = vo.Designation;
                authContactPerson.CellularNo = vo.CellularNo;
                authContactPerson.EmailID = vo.EmailID;
                authContactPerson.RecordStatus = vo.RecordStatus;
                authContactPerson.CreatedBy = vo.CreatedBy;
                authContactPerson.CreatedDate = vo.CreatedDate;
                authContactPerson.ModifiedBy = vo.ModifiedBy;
                authContactPerson.ModifiedDate = vo.ModifiedDate;
            }
            return authContactPerson;
        }
    }
}

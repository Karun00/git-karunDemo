using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.Domain.DTOS
{
    public static class EmployeeMapExtension
    {

        public static Employee MapToEntity(this EmployeeMasterDetails vo)
        {
            Employee empVo = new Employee();
            if (vo != null)
            {
                empVo.EmployeeID = vo.EmployeeID;
                empVo.PortCode = vo.PortCode;
                empVo.SAPNumber = vo.SAPNumber;
                empVo.FirstName = vo.FirstName;
                empVo.LastName = vo.LastName;
                empVo.Initials = vo.Initials;
                empVo.BirthDate = vo.BirthDate;
                empVo.Age = vo.Age;
                empVo.JoiningDate = vo.JoiningDate;
                empVo.YearsofService = vo.YearsofService;
                empVo.OfficialMobileNo = vo.OfficialMobileNo;
                empVo.PersonalMobileNo = vo.PersonalMobileNo;
                empVo.EmailID = vo.EmailID;
                empVo.Gender = vo.Gender;
                empVo.IDNo = vo.IDNo;
                empVo.RecordStatus = vo.RecordStatus;
                empVo.CreatedBy = vo.CreatedBy;
                empVo.Department = vo.Department;
                empVo.Designation = vo.Designation;
                empVo.BusinessUnit = vo.BusinessUnit;
                empVo.CostCenter = vo.CostCenter;
                empVo.PayrollArea = vo.PayrollArea;
                empVo.PSGroup = vo.PSGroup;
                empVo.PersonalSubArea = vo.PersonalSubArea;
                empVo.OrganizationalUnit = vo.OrganizationalUnit;
            }
            return empVo;
        }

        public static EmployeeMasterDetails MapToDTO(this Employee data)
        {
            EmployeeMasterDetails empVo = new EmployeeMasterDetails();
            if (data != null)
            {
                empVo.EmployeeID = data.EmployeeID;
                empVo.PortCode = data.PortCode;
                empVo.SAPNumber = data.SAPNumber;
                empVo.FirstName = data.FirstName;
                empVo.LastName = data.LastName;
                empVo.Initials = data.Initials;
                empVo.BirthDate = data.BirthDate;
                empVo.Age = data.Age;
                empVo.JoiningDate = data.JoiningDate;
                empVo.YearsofService = data.YearsofService;
                empVo.OfficialMobileNo = data.OfficialMobileNo;
                empVo.PersonalMobileNo = data.PersonalMobileNo;
                empVo.EmailID = data.EmailID;
                empVo.Gender = data.Gender;
                empVo.IDNo = data.IDNo;
                empVo.RecordStatus = data.RecordStatus;
                empVo.CreatedBy = data.CreatedBy;
                empVo.Department = data.Department;
                empVo.Designation = data.Designation;
                empVo.BusinessUnit = data.BusinessUnit;
                empVo.CostCenter = data.CostCenter;
                empVo.PayrollArea = data.PayrollArea;
                empVo.PSGroup = data.PSGroup;
                empVo.PersonalSubArea = data.PersonalSubArea;
                empVo.OrganizationalUnit = data.OrganizationalUnit;
            }
            return empVo;
        }


        public static List<Employee> MapToEntity(this List<EmployeeMasterDetails> vos)
        {
            List<Employee> employee = new List<Employee>();
            if (vos != null)
            {
                foreach (var emp in vos)
                {
                    employee.Add(emp.MapToEntity());
                }
            }
            return employee;
        }
        public static List<EmployeeMasterDetails> MapToDTO(this List<Employee> data)
        {
            List<EmployeeMasterDetails> employee = new List<EmployeeMasterDetails>();
            if (data != null)
            {
                foreach (var emp in data)
                {
                    employee.Add(emp.MapToDTO());
                }
            }
            return employee;
        }

        public static List<Employee> ResourceGroupMapToEntity(this List<EmployeeMasterDetails> vos)
        {
            List<Employee> employee = new List<Employee>();
            if (vos != null)
            {
                foreach (var emp in vos)
                {
                    employee.Add(emp.ResourceGroupMapToEntity());
                }
            }
            return employee;
        }

        public static Employee ResourceGroupMapToEntity(this EmployeeMasterDetails vo)
        {
            Employee empVo = new Employee();
            if (vo != null)
            {
                empVo.EmployeeID = vo.EmployeeID;
                empVo.FirstName = String.Concat(vo.FirstName, ' ', vo.LastName);
            }
            return empVo;
        }
    }
}

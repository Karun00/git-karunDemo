using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System.Collections.Generic;

namespace IPMS.Repository
{
    public interface IEmployeeRepository
    {
        List<EmployeeMasterDetails> GetEmployeesDetails(string designation, string searchText, string portCode);
        List<SubCategory> GetDepartments();
        List<SubCategory> GetDesignations();
        List<SubCategory> GetBusinessUnits();
        List<SubCategory> GetCostCenters();
        List<SubCategory> GetPayrollAreas();
        List<SubCategory> GetPSGroups();
        List<SubCategory> GetPersonalSubAreas();
        List<SubCategory> GetOrganizationalUnits();
        Employee AddEmployee(Employee employeeData, int userId, string portCode);
        Employee ModifyEmployee(Employee employeeData, int userId, string portCode);
        Employee DeleteEmployeeById(Employee employeeData);
        Port GetPortCode(Port portData);
        bool ValidateSapNumber(string value);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 11th March 2015
        /// Purpose : To Find EmployeeID in ResourceGroup 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        bool FindEmployeeIdInResourceGroup(int employeeId);
    }
}

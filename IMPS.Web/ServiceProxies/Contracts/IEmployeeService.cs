using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.Web.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IEmployeeService : IDisposable
    {
        [OperationContract]
        List<EmployeeMasterDetails> GetEmployeesDetails(string designation, string searchText);

        [OperationContract]
        List<SubCategory> GetDepartments();

        [OperationContract]
        List<SubCategory> GetDesignations();

        [OperationContract]
        List<SubCategory> GetBusinessUnits();

        [OperationContract]
        List<SubCategory> GetCostCenters();

        [OperationContract]
        List<SubCategory> GetPayrollAreas();

        [OperationContract]
        List<SubCategory> GetPSGroups();

        [OperationContract]
        List<SubCategory> GetPersonalSubAreas();

        [OperationContract]
        List<SubCategory> GetOrganizationalUnits();


        [OperationContract]
        Employee AddEmployee(Employee employeeData);

        [OperationContract]
        Employee ModifyEmployee(Employee employeeData);

        [OperationContract]
        Employee DeleteEmployeeById(Employee employeeData);

        [OperationContract]
        bool ValidateSapNumber(string value);

        //[OperationContract]
        //Task<List<EmployeeMasterDetails>> GetEmployeesDetailsAsync(string Desg, string searchText);

        //[OperationContract]
        //Task<List<SubCategory>> GetDepartmentsAsync();

        //[OperationContract]
        //Task<List<SubCategory>> GetDesignationsAsync();

        //[OperationContract]
        //Task<List<SubCategory>> GetBusinessUnitsAsync();

        //[OperationContract]
        //Task<List<SubCategory>> GetCostCentersAsync();

        //[OperationContract]
        //Task<List<SubCategory>> GetPayrollAreasAsync();

        //[OperationContract]
        //Task<List<SubCategory>> GetPSGroupsAsync();

        //[OperationContract]
        //Task<List<SubCategory>> GetPersonalSubAreasAsync();

        //[OperationContract]
        //Task<List<SubCategory>> GetOrganizationalUnitsAsync();

        //[OperationContract]
        //Task<Employee> AddEmployeeAsync(Employee employeedata);

        //[OperationContract]
        //Task<Employee> ModifyEmployeeAsync(Employee employeedata);

        //[OperationContract]
        //Task<Employee> DelEmployeeByIDAsync(Employee employeedata);

        //[OperationContract]
        //bool ValidateSAPNumberAsync(string value);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 11th March 2015
        /// Purpose : To Find EmployeeID in ResourceGroup 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [OperationContract]
        bool FindEmployeeIdInResourceGroup(int employeeId);
    }
}
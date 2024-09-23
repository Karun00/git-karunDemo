using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using IPMS.Web.ServiceProxies.Contracts;

namespace IPMS.Web.ServiceProxies.Clients
{
    public class EmployeeClient : UserClientBase<IEmployeeService>, IEmployeeService
    {
        public List<EmployeeMasterDetails> GetEmployeesDetails(string designation, string searchText)
        {
            return WrapOperationWithException(() => Channel.GetEmployeesDetails(designation, searchText));
        }

        public List<SubCategory> GetDepartments()
        {
            return WrapOperationWithException(() => Channel.GetDepartments());
        }

        public List<SubCategory> GetDesignations()
        {
            return WrapOperationWithException(() => Channel.GetDesignations());
        }

        public List<SubCategory> GetBusinessUnits()
        {
            return WrapOperationWithException(() => Channel.GetBusinessUnits());
        }

        public List<SubCategory> GetCostCenters()
        {
            return WrapOperationWithException(() => Channel.GetCostCenters());
        }

        public List<SubCategory> GetPayrollAreas()
        {
            return WrapOperationWithException(() => Channel.GetPayrollAreas());
        }

        public List<SubCategory> GetPSGroups()
        {
            return WrapOperationWithException(() => Channel.GetPSGroups());
        }

        public List<SubCategory> GetPersonalSubAreas()
        {
            return WrapOperationWithException(() => Channel.GetPersonalSubAreas());
        }

        public List<SubCategory> GetOrganizationalUnits()
        {
            return WrapOperationWithException(() => Channel.GetOrganizationalUnits());
        }

        public Employee AddEmployee(Employee employeeData)
        {
            return WrapOperationWithException(() => Channel.AddEmployee(employeeData));
        }

        public Employee ModifyEmployee(Employee employeeData)
        {
            return WrapOperationWithException(() => Channel.ModifyEmployee(employeeData));
        }

        public Employee DeleteEmployeeById(Employee employeeData)
        {
            return WrapOperationWithException(() => Channel.DeleteEmployeeById(employeeData));
        }

        public bool ValidateSapNumber(string value)
        {
            return WrapOperationWithException(() => Channel.ValidateSapNumber(value));
        }

        //public Task<List<EmployeeMasterDetails>> GetEmployeesDetailsAsync(string Desg, string searchText)
        //{
        //    return WrapOperationWithException(() => Channel.GetEmployeesDetailsAsync(Desg, searchText));
        //}

        //public Task<List<SubCategory>> GetDepartmentsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetDepartmentsAsync());
        //}

        //public Task<List<SubCategory>> GetDesignationsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetDesignationsAsync());
        //}

        //public Task<List<SubCategory>> GetBusinessUnitsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetBusinessUnitsAsync());
        //}

        //public Task<List<SubCategory>> GetCostCentersAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetCostCentersAsync());
        //}

        //public Task<List<SubCategory>> GetPayrollAreasAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetPayrollAreasAsync());
        //}

        //public Task<List<SubCategory>> GetPSGroupsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetPSGroupsAsync());
        //}

        //public Task<List<SubCategory>> GetPersonalSubAreasAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetPersonalSubAreasAsync());
        //}

        //public Task<List<SubCategory>> GetOrganizationalUnitsAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetOrganizationalUnitsAsync());
        //}

        //public Task<Employee> AddEmployeeAsync(Employee employeedata)
        //{
        //    return WrapOperationWithException(() => Channel.AddEmployeeAsync(employeedata));
        //}

        //public Task<Employee> ModifyEmployeeAsync(Employee employeedata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyEmployeeAsync(employeedata));
        //}

        //public Task<Employee> DelEmployeeByIDAsync(Employee employeedata)
        //{
        //    return WrapOperationWithException(() => Channel.DelEmployeeByIDAsync(employeedata));
        //}

        //public bool ValidateSAPNumberAsync(string value)
        //{
        //    return WrapOperationWithException(() => Channel.ValidateSAPNumberAsync(value));
        //}

        public bool FindEmployeeIdInResourceGroup(int employeeId)
        {
            return WrapOperationWithException(() => Channel.FindEmployeeIdInResourceGroup(employeeId));
        }
    }
}
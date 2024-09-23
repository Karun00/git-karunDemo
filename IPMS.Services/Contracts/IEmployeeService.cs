using System;
using System.Collections.Generic;
using System.ServiceModel;
using IPMS.Domain.Models;

namespace IPMS.Services
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<EmployeeMasterDetails> GetEmployeesDetails(string designation, string searchText);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetDepartments();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetDesignations();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetBusinessUnits();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetCostCenters();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetPayrollAreas();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetPSGroups();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetPersonalSubAreas();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<SubCategory> GetOrganizationalUnits();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Employee AddEmployee(Employee employeeData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Employee ModifyEmployee(Employee employeeData);

        [OperationContract]
        Employee DeleteEmployeeById(Employee employeeData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool ValidateSapNumber(string value);

        /// <summary>
        /// Author  : Sandeep Appana  
        /// Date    : 11th March 2015
        /// Purpose : To Find EmployeeID in ResourceGroup 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool FindEmployeeIdInResourceGroup(int employeeId);
    }
}


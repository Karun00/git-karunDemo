using System.Collections.Generic;
using System.Linq;
using Core.Repository;
using IPMS.Domain.Models;
using IPMS.Data.Context;
using System.ServiceModel;
using IPMS.Repository;
using System;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class EmployeeService : ServiceBase, IEmployeeService
    {
       // private IAccountRepository _accountRepository;
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          //  _accountRepository = new AccountRepository(_unitOfWork);
            _employeeRepository = new EmployeeRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        public EmployeeService()
        {
            // TODO: Complete member initialization
            _unitOfWork = new UnitOfWork(new TnpaContext());
           // _accountRepository = new AccountRepository(_unitOfWork);
            _employeeRepository = new EmployeeRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
        }

        #region GetEmployeesDetails
        /// <summary>
        /// This method is used for fetches the Department details
        /// </summary>
        /// <returns></returns>
        public List<EmployeeMasterDetails> GetEmployeesDetails(string designation, string searchText)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.GetEmployeesDetails(designation, searchText, _PortCode);
            });
        }
        #endregion

        #region GetDepartments
        /// <summary>
        /// This method is used for fetches the department details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDepartments()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.GetDepartments();
            });
        }
        #endregion

        #region GetDesignations
        /// <summary>
        /// This method is used for fetches the designation details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDesignations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.GetDesignations();
            });
        }
        #endregion

        #region GetBusinessUnits
        /// <summary>
        /// This method is used for fetches the business unit details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetBusinessUnits()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.GetBusinessUnits();
            });
        }
        #endregion

        #region GetCostCenters
        /// <summary>
        /// This method is used for fetches the cost centers details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetCostCenters()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.GetCostCenters();
            });
        }
        #endregion

        #region GetPayrollAreas
        /// <summary>
        /// This method is used for fetches the payrollarea details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetPayrollAreas()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.GetPayrollAreas();
            });
        }
        #endregion

        #region GetPSGroups
        /// <summary>
        /// This method is used for fetches the ps group details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetPSGroups()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.GetPSGroups();
            });
        }
        #endregion

        #region GetPersonalSubAreas
        /// <summary>
        /// This method is used for fetches the personal sub areas details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetPersonalSubAreas()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.GetPersonalSubAreas();
            });
        }
        #endregion

        #region GetOrganizationalUnits
        /// <summary>
        /// This method is used for fetches the organizational unit details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetOrganizationalUnits()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.GetOrganizationalUnits();
            });
        }
        #endregion

        #region FindEmployeeIDInResourceGroup
        public bool FindEmployeeIdInResourceGroup(int employeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.FindEmployeeIdInResourceGroup(employeeId);
            });
        }
        #endregion

        #region AddEmployee
        /// <summary>
        /// This method is used for insert the data
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns></returns>
        public Employee AddEmployee(Employee employeeData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _employeeRepository.AddEmployee(employeeData, _UserId, _PortCode);
            });
        }
        #endregion

        #region ModifyEmployee
        /// <summary>
        /// This method is used for update the data
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns></returns>
        public Employee ModifyEmployee(Employee employeeData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _employeeRepository.ModifyEmployee(employeeData, _UserId, _PortCode);
            });
        }
        #endregion

        #region DelEmployeeByID
        /// <summary>
        /// This method is used for delete the data
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns></returns>
        public Employee DeleteEmployeeById(Employee employeeData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                return _employeeRepository.DeleteEmployeeById(employeeData);
            });
        }
        #endregion

        #region GetPortCode
        /// <summary>
        /// This method is used for fetche the port data
        /// </summary>
        /// <param name="portData"></param>
        /// <returns></returns>
        public Port GetPortCode(Port portData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.GetPortCode(portData);
            });
        }
        #endregion

        #region ValidateSAPNumber
        /// <summary>
        /// This method is used for verify the SAP Number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ValidateSapNumber(string value)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _employeeRepository.ValidateSapNumber(value);
            });
        }
        #endregion
    }
}

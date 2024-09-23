using Core.Repository;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using IPMS.Domain;
namespace IPMS.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private IUnitOfWork _unitOfWork;
        // private readonly ILog log;

        public EmployeeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            // log = 
            LogManager.GetLogger(typeof(EmployeeRepository));
        }

        #region GetEmployeesDetails
        /// <summary>
        /// This method is used for fetches the Department details
        /// </summary>
        /// <returns></returns>
        public List<EmployeeMasterDetails> GetEmployeesDetails(string designation, string searchText, string portCode)
        {
            var employeeslist = (from e in _unitOfWork.Repository<Employee>().Queryable()
                                 join de in _unitOfWork.Repository<SubCategory>().Queryable()
                                 on e.Department equals de.SubCatCode
                                 join des in _unitOfWork.Repository<SubCategory>().Queryable()
                                  on e.Designation equals des.SubCatCode
                                 join bu in _unitOfWork.Repository<Port>().Queryable()
                                on e.PortCode equals bu.PortCode
                                 join cc in _unitOfWork.Repository<SubCategory>().Queryable()
                                  on e.CostCenter equals cc.SubCatCode
                                 join pa in _unitOfWork.Repository<SubCategory>().Queryable()
                                on e.PayrollArea equals pa.SubCatCode
                                 join ps in _unitOfWork.Repository<SubCategory>().Queryable()
                                on e.PSGroup equals ps.SubCatCode
                                 join psa in _unitOfWork.Repository<SubCategory>().Queryable()
                                 on e.PersonalSubArea equals psa.SubCatCode
                                 join ou in _unitOfWork.Repository<SubCategory>().Queryable()
                                 on e.OrganizationalUnit equals ou.SubCatCode
                                 join gen in _unitOfWork.Repository<SubCategory>().Queryable()
                                on e.Gender equals gen.SubCatCode
                                 //where e.PortCode == portCode Commented by sandeep on 14-09-2015
                                 select new EmployeeMasterDetails
                                 {
                                     EmployeeID = e.EmployeeID,
                                     SAPNumber = e.SAPNumber,
                                     FirstName = e.FirstName,
                                     LastName = e.LastName,
                                     Initials = e.Initials,
                                     Name = e.FirstName + " " + e.LastName + " " + e.Initials,
                                     BirthDate = e.BirthDate,
                                     Age = e.Age,
                                     JoiningDate = e.JoiningDate,
                                     YearsofService = e.YearsofService,
                                     OfficialMobileNo = e.OfficialMobileNo,
                                     PersonalMobileNo = e.PersonalMobileNo,
                                     EmailID = e.EmailID,
                                     IDNo = e.IDNo,
                                     GenderCode = gen.SubCatCode,
                                     Gender = gen.SubCatName,
                                     DepartmentCode = e.Department,
                                     DepartmentName = de.SubCatName,
                                     DesignationCode = e.Designation,
                                     DesignationName = des.SubCatName,
                                     BusinessUnitCode = e.PortCode, // e.BusinessUnit,
                                     BusinessUnitName = bu.PortName, //bu.SubCatName,
                                     CostCenterCode = e.CostCenter,
                                     CostCenterName = cc.SubCatName,
                                     PayrollAreaCode = e.PayrollArea,
                                     PayrollAreaName = pa.SubCatName,
                                     PSGroupCode = e.PSGroup,
                                     PSGroupName = ps.SubCatName,
                                     PersonalSubAreaCode = e.PersonalSubArea,
                                     PersonalSubAreaName = psa.SubCatName,
                                     OrganizationalUnitCode = e.OrganizationalUnit,
                                     OrganizationalUnitName = ou.SubCatName,
                                     RecordStatus = e.RecordStatus,
                                     PortCode = e.PortCode,
                                     DeadWeightTonnage = e.DeadWeightTonnage
                                 }).OrderByDescending(x => x.EmployeeID);

            var Result = GetEmployeeMasterDetailses(designation, searchText, employeeslist);

            return Result;
        }

        private static List<EmployeeMasterDetails> GetEmployeeMasterDetailses(string Designation, string searchText, IEnumerable<EmployeeMasterDetails> employeeslist)
        {
            List<EmployeeMasterDetails> Result = null;

            if (Designation != null && searchText != null)
            {
                Result =
                    employeeslist.ToList<EmployeeMasterDetails>()
                        .Where(
                            v =>
                                v.DesignationCode == Designation &&
                                ((searchText != null && v.Name.ToUpperInvariant().Contains(searchText.ToUpperInvariant())) ||
                                 (searchText != null && v.SAPNumber.ToUpperInvariant().Contains(searchText.ToUpperInvariant()))))
                        .ToList();
            }
            else if (Designation != null || searchText != null)
            {
                Result =
                    employeeslist.ToList<EmployeeMasterDetails>()
                        .Where(
                            v =>
                                v.DesignationCode == Designation ||
                                ((searchText != null && v.Name.ToUpperInvariant().Contains(searchText.ToUpperInvariant())) ||
                                 (searchText != null && v.SAPNumber.ToUpperInvariant().Contains(searchText.ToUpperInvariant()))))
                        .ToList();
            }
            else
            {
                Result = employeeslist.ToList();
            }
            return Result;
        }

        #endregion

        #region GetDepartments
        /// <summary>
        /// This method is used for fetches the department details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDepartments()
        {
            var DepartmentType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.SupCatCode == SuperCategoryConstants.DEPARTMENT_TYPE).OrderBy(x => x.SubCatName).ToList();
            return DepartmentType;
        }
        #endregion

        #region GetDesignations
        /// <summary>
        /// This method is used for fetches the designation details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDesignations()
        {
            var DesignationType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.SupCatCode == SuperCategoryConstants.RESGRP_DESIGANTIONCODE).OrderBy(x => x.SubCatName);
            return DesignationType.ToList();
        }
        #endregion

        #region GetBusinessUnits
        /// <summary>
        /// This method is used for fetches the business unit details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetBusinessUnits()
        {
            var BusinessUnitType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.SupCatCode == "BU").OrderBy(x => x.SubCatName);
            return BusinessUnitType.ToList();
        }
        #endregion

        #region GetCostCenters
        /// <summary>
        /// This method is used for fetches the cost centers details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetCostCenters()
        {
            var CostCenterType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.SupCatCode == "CC").OrderBy(x => x.SubCatName);
            return CostCenterType.ToList();
        }
        #endregion

        #region GetPayrollAreas
        /// <summary>
        /// This method is used for fetches the payrollarea details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetPayrollAreas()
        {
            var PayrollAreaType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.SupCatCode == "PA").OrderBy(x => x.SubCatName);
            return PayrollAreaType.ToList();
        }
        #endregion

        #region GetPSGroups
        /// <summary>
        /// This method is used for fetches the ps group details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetPSGroups()
        {
            var PSGroupType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.SupCatCode == "PSG").OrderBy(x => x.SubCatName);
            return PSGroupType.ToList();
        }
        #endregion

        #region GetPersonalSubAreas
        /// <summary>
        /// This method is used for fetches the personal sub areas details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetPersonalSubAreas()
        {
            var PersonalSubAreaType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.SupCatCode == "PSA").OrderBy(x => x.SubCatName);
            return PersonalSubAreaType.ToList();
        }
        #endregion

        #region GetOrganizationalUnits
        /// <summary>
        /// This method is used for fetches the organizational unit details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetOrganizationalUnits()
        {
            var OrganizationalUnitType = _unitOfWork.Repository<SubCategory>().Queryable().Where(x => x.SupCatCode == "OU").OrderBy(x => x.SubCatName);

            return OrganizationalUnitType.ToList();
        }
        #endregion

        #region FindEmployeeIDInResourceGroup
        public bool FindEmployeeIdInResourceGroup(int employeeId)
        {
            var employee = _unitOfWork.Repository<ResourceEmployeeGroup>().Queryable().Where(x => x.EmployeeID == employeeId).SingleOrDefault();
            if (employee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region AddEmployee

        /// <summary>
        /// This method is used for insert the data
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns></returns>
        public Employee AddEmployee(Employee employeeData, int userId, string portCode)
        {
            if (employeeData != null)
            {

                employeeData.GrossWeightTonnage = employeeData.DeadWeightTonnage;
                employeeData.BusinessUnit = null; // Added by sandeep on 14-09-2015 
                employeeData.CreatedDate = DateTime.Now;
                employeeData.CreatedBy = userId;
                employeeData.ModifiedDate = DateTime.Now;
                employeeData.ModifiedBy = userId;
                //employeeData.PortCode = portCode; // Commented by sandeep on 14-09-2015
                employeeData.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<Employee>().Insert(employeeData);
                _unitOfWork.SaveChanges();
            }

            return employeeData;
        }
        #endregion

        #region ModifyEmployee
        /// <summary>
        /// This method is used for update the data
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns></returns>
        public Employee ModifyEmployee(Employee employeeData, int userId, string portCode)
        {
            if (employeeData != null)
            {
                employeeData.GrossWeightTonnage = employeeData.DeadWeightTonnage;
                employeeData.BusinessUnit = null; // Added by sandeep on 14-09-2015
                employeeData.ModifiedDate = DateTime.Now;
                employeeData.ModifiedBy = userId;
                employeeData.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Employee>().Update(employeeData);
                _unitOfWork.SaveChanges();
            }
            return employeeData;
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
            if (employeeData != null)
            {
            }
            var employeeObj = _unitOfWork.Repository<Employee>().Find(employeeData.EmployeeID);
            employeeObj.RecordStatus = "I";
            employeeObj.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<Employee>().Update(employeeObj);
            _unitOfWork.SaveChanges();

            return employeeObj;

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
            var port = _unitOfWork.Repository<Port>().Queryable().FirstOrDefault<Port>();
            return port;
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
            var employeeObj = _unitOfWork.Repository<Employee>().Queryable().Where(x => x.SAPNumber == value);
            if (employeeObj.Count() > 0)
                return false;
            return true;
        }
        #endregion
    }
}


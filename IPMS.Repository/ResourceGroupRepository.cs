using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPMS.Domain;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using Core.Repository;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;

namespace IPMS.Repository
{
    public class ResourceGroupRepository : IResourceGroupRepository
    {
        private IUnitOfWork _unitOfWork;

        public ResourceGroupRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region CharsToTitleCase
        IEnumerable<char> CharsToTitleCase(string s)
        {
            bool newWord = true;
            foreach (char c in s)
            {
                if (newWord) { yield return Char.ToUpper(c); newWord = false; }
                else yield return Char.ToLower(c);
                if (c == ' ') newWord = true;
            }
        }
        #endregion

        #region GetDesignations
        /// <summary>
        /// To get desigantion details
        /// </summary>
        /// <returns></returns>
        public List<SubCategory> GetDesignations()
        {
            var DesignationType = (from ad in _unitOfWork.Repository<SubCategory>().Query().Select()
                                   where ad.SupCatCode == SuperCategoryConstants.RESGRP_DESIGANTIONCODE && ad.RecordStatus == "A"
                                   orderby ad.SubCatName ascending
                                   select new SubCategory
                                   {
                                       SubCatName = ad.SubCatName,
                                       SubCatCode = ad.SubCatCode
                                   });

            return DesignationType.ToList();
        }
        #endregion

        #region GetResourceGroups
        /// <summary>
        /// To get resource group details
        /// </summary>
        /// <returns></returns>
        public List<ResourceGroupVO> GetResourceGroups(string portCode)
        {
            var resourcegroups = (from t in _unitOfWork.Repository<ResourceGroup>().Queryable().Include(t => t.SubCategory)
                                  where t.PortCode == portCode
                                  select new ResourceGroupVO
                                  {
                                      ResourceGroupCode = t.ResourceGroupCode,
                                      ResourceGroupID = t.ResourceGroupID,
                                      RecordStatus = t.RecordStatus,
                                      ResourceGroupName = t.ResourceGroupName,
                                      Designation = t.SubCategory.SubCatName,
                                      Position = t.Position,
                                      DesignationCode = t.SubCategory.SubCatCode,
                                      CreatedBy = t.CreatedBy,
                                      CreatedDate = t.CreatedDate
                                  }).OrderByDescending(x=>x.ResourceGroupID).ToList<ResourceGroupVO>();

            foreach (var item in resourcegroups)
            {
                List<ResourceEmployeeGroup> resourcegrpList = (from t in _unitOfWork.Repository<ResourceEmployeeGroup>().Queryable()
                                                               where t.ResourceGroupID == item.ResourceGroupID
                                                               select t).ToList<ResourceEmployeeGroup>();

                List<ResourceEmployeeGroupVO> resgrplist = resourcegrpList.MapToDTO();

                item.ResourceEmployeeGroups = resgrplist;
            }

            return resourcegroups;
        }
        #endregion

        #region GetEmployees
        /// <summary>
        /// To get employee by desigantion code
        /// </summary>
        /// <param name="designationCode"></param>
        /// <returns></returns>
        public List<Employee> GetEmployees(string resourceGroupCode, string designationCode, string mode, string portCode)
        {
            var Grpcode = new SqlParameter("@ResourceGroupCode", resourceGroupCode == null ? "" : resourceGroupCode);
            var designation = new SqlParameter("@Designation", designationCode);
            var viewmode = new SqlParameter("@Mode", mode);
            var _portCode = new SqlParameter("@PortCode", portCode);

            var pt = _unitOfWork.SqlQuery<Employee>("dbo.usp_Resourcegroup_Employees @ResourceGroupCode, @Designation, @Mode, @PortCode", Grpcode, designation, viewmode, _portCode).ToList().MapToDTO();

            return pt.ResourceGroupMapToEntity();
        }
        #endregion

        #region AddResourceGroupDetails
        /// <summary>
        ///To add resource group details 
        /// </summary>
        /// <param name="resourcegrp"></param>
        /// <returns></returns>
        public ResourceGroupVO AddResourceGroupDetails(ResourceGroupVO resourcegrp, int userId, string portCode)
        {
            if (resourcegrp != null)
            {
                if (!string.IsNullOrWhiteSpace(resourcegrp.ResourceGroupName))
                {
                    resourcegrp.ResourceGroupName = new string(CharsToTitleCase(resourcegrp.ResourceGroupName).ToArray());
                }

                ResourceGroup resourcegroup = null;

                resourcegroup = resourcegrp.MapToEntity();
                resourcegroup.ObjectState = ObjectState.Added;
                resourcegroup.CreatedBy = userId;
                resourcegroup.CreatedDate = DateTime.Now;
                resourcegroup.ModifiedBy = userId;
                resourcegroup.ModifiedDate = DateTime.Now;
                resourcegroup.PortCode = portCode;

                List<ResourceEmployeeGroup> resourceGroupEmpList = resourcegroup.ResourceEmployeeGroups.ToList();

                if (resourceGroupEmpList.Count > 0)
                {
                    foreach (var rsgemp in resourceGroupEmpList)
                    {
                        rsgemp.ResourceGroupID = resourcegroup.ResourceGroupID;
                        rsgemp.RecordStatus = "A";
                        rsgemp.CreatedBy = userId;
                        rsgemp.CreatedDate = DateTime.Now;
                        rsgemp.ModifiedBy = userId;
                        rsgemp.ModifiedDate = DateTime.Now;
                    }
                    _unitOfWork.Repository<ResourceEmployeeGroup>().InsertRange(resourceGroupEmpList);
                }

                _unitOfWork.Repository<ResourceGroup>().Insert(resourcegroup);
                _unitOfWork.SaveChanges();
            }

            return resourcegrp;
        }
        #endregion

        #region ModifyResourceGroups
        /// <summary>
        ///  To modify resource group details
        /// </summary>
        /// <param name="rsgroup"></param>
        /// <returns></returns>
        public ResourceGroupVO ModifyResourceGroups(ResourceGroupVO rsgroup, int userId, string portCode)
        {
            if (rsgroup != null)
            {
                if (!string.IsNullOrWhiteSpace(rsgroup.ResourceGroupName))
                {
                    rsgroup.ResourceGroupName = new string(CharsToTitleCase(rsgroup.ResourceGroupName).ToArray());
                }

                ResourceGroup resourcegroup = null;

                resourcegroup = rsgroup.MapToEntity();

                resourcegroup.ObjectState = ObjectState.Modified;
                resourcegroup.ModifiedBy = userId;
                resourcegroup.ModifiedDate = DateTime.Now;
                resourcegroup.PortCode = portCode;

                List<ResourceEmployeeGroup> rsgempList = resourcegroup.ResourceEmployeeGroups.ToList();

                var originalemployees = _unitOfWork.SqlQuery<int>("select EmployeeID from dbo.ResourceEmployeeGroup where ResourceGroupID = @p0", rsgroup.ResourceGroupID).ToList();

                _unitOfWork.ExecuteSqlCommand("delete dbo.ResourceEmployeeGroup where ResourceGroupID = @p0", rsgroup.ResourceGroupID);

                if (rsgempList.Count() > 0)
                {
                    foreach (var rsemp in rsgempList)
                    {
                        rsemp.ResourceGroupID = rsgroup.ResourceGroupID;
                        rsemp.CreatedBy = userId;
                        rsemp.CreatedDate = DateTime.Now;
                        rsemp.ModifiedBy = userId;
                        rsemp.ModifiedDate = DateTime.Now;
                        rsemp.RecordStatus = "A";
                    }
                    _unitOfWork.Repository<ResourceEmployeeGroup>().InsertRange(rsgempList);
                }

                _unitOfWork.Repository<ResourceGroup>().Update(resourcegroup);

                _unitOfWork.SaveChanges();

                var changedvalues = rsgempList.Select(i => i.EmployeeID).ToList();

                var distinctValues = originalemployees.Except(changedvalues).ToList();

                if (distinctValues.Count > 0)
                {
                    foreach (var emp in distinctValues)
                    {
                        var employee = (from rad in _unitOfWork.Repository<ResourceAttendanceDtl>().Query().Tracking(true).Select()
                                        where rad.EmployeeID == emp && rad.CreatedDate.Date == DateTime.Now.Date
                                        select rad).SingleOrDefault();

                        if (employee != null)
                        {
                            _unitOfWork.ExecuteSqlCommand("delete dbo.ResourceAttendanceDtl where ResourceAttendanceDtlID = @p0", employee.ResourceAttendanceDtlID);
                        }
                    }
                }
            }

            return rsgroup;
        }
        #endregion
    }
}

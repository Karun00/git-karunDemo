using System;
using System.Collections.Generic;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using log4net;
using log4net.Config;

namespace IPMS.Repository
{
    public class PortInformationRepository : IPortInformationRepository
    {
        private IUnitOfWork _unitOfWork;

        // private readonly ILog log;

        public PortInformationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
            // log = LogManager.GetLogger(typeof(DepartureNoticeRepository));
        }

        #region AddPortContent
        /// <summary>
        /// Add PortContent
        /// </summary>
        /// <param name="portContentData"></param>
        /// <param name="userId"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public PortContentVO AddPortContent(PortContentVO portContentData, int userId, string portCode)
        {
            if (portContentData != null)
            {
                PortContent objcnt = new PortContent();
                objcnt = PortContentMapExtension.MapToEntity(portContentData);
                objcnt.CreatedBy = userId;
                objcnt.ModifiedBy = userId;
                objcnt.CreatedDate = DateTime.Now;
                objcnt.ModifiedDate = DateTime.Now;
                objcnt.PortCode = portCode;
                objcnt.RecordStatus = (objcnt.ParentPortContentID == null ? "A" : objcnt.RecordStatus);

                objcnt.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<PortContent>().Insert(objcnt);
                _unitOfWork.SaveChanges();

                if (portContentData.PortContentRole.Count() > 0)
                {
                    foreach (var item in portContentData.PortContentRole)
                    {
                        PortContentRole objcontentrole = new PortContentRole();
                        objcontentrole.PortContentID = objcnt.PortContentID;
                        objcontentrole.RecordStatus = "A";
                        objcontentrole.UserType = item.UserType;
                        objcontentrole.RoleID = item.RoleID;
                        objcontentrole.CreatedBy = userId;
                        objcontentrole.CreatedDate = DateTime.Now;
                        objcontentrole.ModifiedBy = userId;
                        objcontentrole.ModifiedDate = DateTime.Now;
                        _unitOfWork.Repository<PortContentRole>().Insert(objcontentrole);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            return portContentData;
        }
        #endregion

        #region ModifyPortContent
        /// <summary>
        /// Modify PortContent
        /// </summary>
        /// <param name="portContentData"></param>
        /// <param name="userId"></param>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public PortContentVO ModifyPortContent(PortContentVO portContentData, int userId, string portCode)
        {
            List<PortContentRole> lstuserPortcontentRole = _unitOfWork.Repository<PortContentRole>().Queryable().Where(e => e.PortContentID == portContentData.PortContentID).ToList();

            foreach (PortContentRole contentRole in lstuserPortcontentRole)
            {
                _unitOfWork.Repository<PortContentRole>().Delete(contentRole);
                _unitOfWork.SaveChanges();
            }

            PortContent objcnt = new PortContent();
            objcnt = PortContentMapExtension.MapToEntity(portContentData);
            objcnt.CreatedBy = userId;
            objcnt.ModifiedBy = userId;
            objcnt.CreatedDate = DateTime.Now;
            objcnt.ModifiedDate = DateTime.Now;
            objcnt.RecordStatus = (objcnt.ParentPortContentID == null ? "A" : objcnt.RecordStatus);
            objcnt.ContentType = "Y";
            objcnt.PortCode = portCode;
            objcnt.ObjectState = ObjectState.Added;
            _unitOfWork.Repository<PortContent>().Update(objcnt);
            _unitOfWork.SaveChanges();

            PortContentRole objdatarole = new PortContentRole();
            if (portContentData.PortContentRole.Count() > 0)
            {
                foreach (var item1 in portContentData.PortContentRole)
                {
                    objdatarole.PortContentID = objcnt.PortContentID;
                    objdatarole.RecordStatus = "A";
                    objdatarole.UserType = item1.UserType;
                    objdatarole.RoleID = item1.RoleID;
                    objdatarole.CreatedBy = userId;
                    objdatarole.CreatedDate = DateTime.Now;
                    objdatarole.ModifiedBy = userId;
                    objdatarole.ModifiedDate = DateTime.Now;
                    _unitOfWork.Repository<PortContentRole>().Insert(objdatarole);
                    _unitOfWork.SaveChanges();
                }
            }

            return portContentData;
        }
        #endregion

        #region GetRolesforEmployee
        /// <summary>
        /// Get RolesforEmployee
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRolesForEmployee()
        {
            var Roles = (from ad in _unitOfWork.Repository<Role>().Queryable()
                         where ad.RoleName != "Agent" && ad.RoleName != "Terminal Operator" && ad.RoleName != "Admin"
                         select ad);
            return Roles.ToList();
        }
        #endregion

        #region GetPortContentRoles
        /// <summary>
        /// Get PortContentRoles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PortContentRoleVO> GetPortContentRoles(int id)
        {
            List<PortContentRoleVO> portcontentroles = new List<PortContentRoleVO>();
            //var portcontentroles = (from objrole in _unitOfWork.Repository<PortContentRole>().Query().Select()
            //                        where objrole.PortContentID == id
            //                        select new PortContentRoleVO
            //                        {
            //                            RoleID = objrole.RoleID,
            //                            PortContentID = objrole.PortContentID,
            //                            UserType = objrole.UserType,

            //                        }).ToList();
            //return portcontentroles;
            List<PortContent> lstuserPortcontentRole = _unitOfWork.Repository<PortContent>().Queryable().Where(e => e.PortContentID == id).ToList();

            foreach (var item in lstuserPortcontentRole)
            {
                if (item.PortContentID == id &&item.LinkVisibility=="U"&&item.LinkType=="D")
                {
                    portcontentroles = (from p in _unitOfWork.Repository<PortContent>().Queryable()
                                        join pr in _unitOfWork.Repository<PortContentRole>().Query().Select()
                                        on p.PortContentID equals pr.PortContentID
                                        join d in _unitOfWork.Repository<Document>().Query().Select()
                                        on p.DocumentID equals d.DocumentID
                                        where pr.PortContentID == id
                                        select new PortContentRoleVO
                                        {
                                            RoleID = pr.RoleID,
                                            PortContentID = pr.PortContentID,
                                            UserType = pr.UserType,
                                            DocumentID = d.DocumentID,
                                            DocumentName = d.DocumentPath
                                        }).ToList();

                }

                else if (item.PortContentID == id && item.LinkVisibility == "G" && item.LinkType == "D")
                {

                    portcontentroles = (from p in _unitOfWork.Repository<PortContent>().Queryable()
                                        join d in _unitOfWork.Repository<Document>().Query().Select()
                                        on p.DocumentID equals d.DocumentID
                                        where p.PortContentID == id
                                        select new PortContentRoleVO
                                        {
                                            PortContentID = p.PortContentID,
                                            DocumentID = d.DocumentID,
                                            DocumentName = d.DocumentPath
                                        }).ToList();


                }


            }
            return portcontentroles;







        }

        #endregion

        #region GetDocumentDetails
        /// <summary>
        /// Get Document Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Document GetDocumentDetails(int id)
        {
            var result1 = _unitOfWork.Repository<Document>().Queryable().Where(e => e.DocumentID == id).FirstOrDefault();
            return result1;
        }
        #endregion

        #region GetPortContenetforTreeview
        /// <summary>
        /// Get Port Contenet for Treeview
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public IEnumerable<PortContent> GetPortContentForTreeView(int userId, string loginName)
        {
            //if (_UserId != 0 || _LoginName != "")
            //{

            List<PortContent> portcontent = new List<PortContent>();
            List<PortContentVO> portcontentvo = null;
            List<PortContentRole> portcontentrole = new List<PortContentRole>();

            var content = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                           where m.ParentPortContentID == null
                           select m).ToList();

            portcontentvo = content.MapToDTO();
            content = portcontentvo.MapToEntity();

            List<PortContent> submodules1 = new List<PortContent>();
            List<PortContentRole> roledet = new List<PortContentRole>();

            if (userId > 0)
            {
                var lstroles = (from ur in _unitOfWork.Repository<UserRole>().Queryable()
                                join r in _unitOfWork.Repository<Role>().Queryable() on ur.RoleID equals r.RoleID
                                where ur.UserID == userId
                                select new
                                {
                                    ur.RoleID,
                                    r.RoleCode
                                }).ToList();
                List<int> roles = new List<int>();
                foreach (var role in lstroles)
                {
                    roles.Add(role.RoleID);
                }

                int[] inRoles = roles.ToArray();

                var adminRoleExist = lstroles.FindAll(r => r.RoleCode == Roles.Admin||r.RoleCode==Roles.Annuser);
                if (adminRoleExist.Count == 0)
                {
                    foreach (var item1 in content)
                    {

                        var portCount = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                                         join cr in _unitOfWork.Repository<PortContentRole>().Queryable() on m.PortContentID equals cr.PortContentID
                                         where inRoles.Contains(cr.RoleID) && m.ParentPortContentID == item1.PortContentID
                                         select m).OrderBy(x => x.ContentName).Count();

                        if (portCount > 0)
                        {
                            submodules1 = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                                           join cr in _unitOfWork.Repository<PortContentRole>().Queryable() on m.PortContentID equals cr.PortContentID
                                           where inRoles.Contains(cr.RoleID) && m.ParentPortContentID == item1.PortContentID
                                           select m).OrderBy(x => x.ContentName).Distinct().ToList();
                        }
                        else
                        {
                            submodules1 = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                                           where m.PortContentID == item1.PortContentID
                                           select m).OrderBy(x => x.ContentName).Distinct().ToList();
                        }

                        portcontentvo = submodules1.MapToDTO();
                        submodules1 = portcontentvo.MapToEntity();

                        List<PortContent> submodulesm = new List<PortContent>();

                        if (submodules1.Count() > 0)
                        {
                            foreach (var item2 in submodules1)
                            {
                                var parentPortCount = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                                                       //join cr in _unitOfWork.Repository<PortContentRole>().Query().Select() on m.PortContentID equals cr.PortContentID
                                                       //where inRoles.Contains(cr.RoleID) && m.PortContentID == item2.ParentPortContentID
                                                       where m.PortContentID == item2.ParentPortContentID
                                                       select m).OrderBy(x => x.ContentName).Count();

                                if (parentPortCount > 0)
                                {
                                    var ContentData = new PortContent();
                                    ContentData.PortContentID = item2.PortContentID;
                                    ContentData.ParentPortContentID = item2.ParentPortContentID;
                                    ContentData.ContentName = item2.ContentName;
                                    ContentData.ContentType = item2.ContentType;
                                    ContentData.LinkVisibility = item2.LinkVisibility;
                                    ContentData.LinkType = item2.LinkType;
                                    ContentData.LinkContent = item2.LinkContent;
                                    ContentData.DocumentID = item2.DocumentID;
                                    ContentData.PortCode = item1.PortCode;
                                    ContentData.CreatedBy = item2.CreatedBy;
                                    ContentData.CreatedDate = item2.CreatedDate;
                                    ContentData.ModifiedBy = item2.ModifiedBy;
                                    ContentData.ModifiedDate = item2.ModifiedDate;
                                    ContentData.RecordStatus = item2.RecordStatus;

                                    submodulesm.Add(ContentData);
                                }
                                else
                                {
                                    var ContentData = new PortContent();
                                    ContentData.ContentName = "No Data";
                                    ContentData.ContentType = null;
                                    ContentData.LinkVisibility = null;
                                    ContentData.LinkType = null;
                                    ContentData.LinkContent = null;
                                    submodulesm.Add(ContentData);
                                }
                            }

                            portcontent.Add(new PortContent()
                            {
                                PortContentID = item1.PortContentID,
                                ParentPortContentID = item1.ParentPortContentID,
                                ContentName = item1.ContentName,
                                PortContent1 = submodulesm,
                                RecordStatus = item1.RecordStatus
                            });
                        }
                    }
                }
                else
                {
                    foreach (var item1 in content)
                    {
                        var portCount = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                                         where m.ParentPortContentID == item1.PortContentID && m.RecordStatus == "A"
                                         select m).OrderBy(m => m.ContentName).Count();

                        if (portCount > 0)
                        {
                            submodules1 = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                                           where m.ParentPortContentID == item1.PortContentID
                                           select m).OrderBy(m => m.ContentName).Distinct().ToList();
                        }
                        else
                        {
                            submodules1 = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                                           where m.PortContentID == item1.PortContentID
                                           select m).OrderBy(m => m.ContentName).Distinct().ToList();
                        }



                        portcontentvo = submodules1.MapToDTO();
                        submodules1 = portcontentvo.MapToEntity();

                        List<PortContent> submodulesm = new List<PortContent>();
                        if (submodules1.Count() > 0)
                        {
                            foreach (var item2 in submodules1)
                            {
                                var portCount1 = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                                                  where m.PortContentID == item2.ParentPortContentID && m.RecordStatus == "A"
                                                  select m).OrderBy(m => m.ContentName).Count();

                                if (portCount1 > 0)
                                {
                                    var ContentData = new PortContent();
                                    ContentData.PortContentID = item2.PortContentID;
                                    ContentData.ParentPortContentID = item2.ParentPortContentID;
                                    ContentData.ContentName = item2.ContentName;
                                    ContentData.ContentType = item2.ContentType;
                                    ContentData.LinkVisibility = item2.LinkVisibility;
                                    ContentData.LinkType = item2.LinkType;
                                    ContentData.LinkContent = item2.LinkContent;
                                    ContentData.DocumentID = item2.DocumentID;
                                    ContentData.PortCode = item1.PortCode;
                                    ContentData.CreatedBy = item2.CreatedBy;
                                    ContentData.CreatedDate = item2.CreatedDate;
                                    ContentData.ModifiedBy = item2.ModifiedBy;
                                    ContentData.ModifiedDate = item2.ModifiedDate;
                                    ContentData.RecordStatus = item2.RecordStatus;

                                    submodulesm.Add(ContentData);
                                }
                                else
                                {
                                    var ContentData = new PortContent();
                                    ContentData.ContentName = "No Data";
                                    ContentData.ContentType = null;
                                    ContentData.LinkVisibility = null;
                                    ContentData.LinkType = null;
                                    ContentData.LinkContent = null;
                                    submodulesm.Add(ContentData);
                                }
                            }

                            portcontent.Add(new PortContent()
                            {
                                PortContentID = item1.PortContentID,
                                ParentPortContentID = item1.ParentPortContentID,
                                ContentName = item1.ContentName,
                                PortContent1 = submodulesm,
                                RecordStatus = item1.RecordStatus
                            });
                        }
                    }
                }
            }
            else
            {
                foreach (var item1 in content)
                {
                    submodules1 = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                                   where m.ParentPortContentID == item1.PortContentID && m.RecordStatus == "A" && m.LinkVisibility == "G"
                                   select m).OrderBy(m => m.ContentName).Distinct().ToList();

                    portcontentvo = submodules1.MapToDTO();
                    submodules1 = portcontentvo.MapToEntity();

                    List<PortContent> submodulesm = new List<PortContent>();
                    if (submodules1.Count() > 0)
                    {
                        foreach (var item2 in submodules1)
                        {
                            var portCount = (from m in _unitOfWork.Repository<PortContent>().Queryable()
                                             where m.PortContentID == item2.ParentPortContentID && m.RecordStatus == "A"
                                             select m).OrderBy(m => m.ContentName).Count();

                            if (portCount > 0)
                            {
                                var ContentData = new PortContent();
                                ContentData.PortContentID = item2.PortContentID;
                                ContentData.ParentPortContentID = item2.ParentPortContentID;
                                ContentData.ContentName = item2.ContentName;
                                ContentData.ContentType = item2.ContentType;
                                ContentData.LinkVisibility = item2.LinkVisibility;
                                ContentData.LinkType = item2.LinkType;
                                ContentData.LinkContent = item2.LinkContent;
                                ContentData.DocumentID = item2.DocumentID;
                                ContentData.PortCode = item1.PortCode;
                                ContentData.CreatedBy = item2.CreatedBy;
                                ContentData.CreatedDate = item2.CreatedDate;
                                ContentData.ModifiedBy = item2.ModifiedBy;
                                ContentData.ModifiedDate = item2.ModifiedDate;
                                ContentData.RecordStatus = item2.RecordStatus;

                                submodulesm.Add(ContentData);
                            }
                            else
                            {
                                var ContentData = new PortContent();
                                ContentData.ContentName = "No Data";
                                ContentData.ContentType = null;
                                ContentData.LinkVisibility = null;
                                ContentData.LinkType = null;
                                ContentData.LinkContent = null;
                                submodulesm.Add(ContentData);
                            }
                        }

                        portcontent.Add(new PortContent()
                        {
                            PortContentID = item1.PortContentID,
                            ParentPortContentID = item1.ParentPortContentID,
                            ContentName = item1.ContentName,
                            PortContent1 = submodulesm,
                            RecordStatus = item1.RecordStatus
                        });
                    }
                }
            }
            //}

            return portcontent;
        }
        #endregion


    }
}



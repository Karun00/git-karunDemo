using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMatrix.WebData;

namespace IPMS.Data
{
    class TnpaDataSeeder
    {
        TnpaContext _ctx;
        private readonly IUnitOfWork _unitOfWork;

        public TnpaDataSeeder(TnpaContext ctx, IUnitOfWork unitOfWork)
        {
            _ctx = ctx;
            _unitOfWork = unitOfWork;
        }

        public void Seed()
        {

            #region DataSeeder for Super Category

            if (_ctx.SuperCategories.Count() == 0)
            {
                for (int k = 0; k < super_category.Length; k++)
                {
                    var _super_categories = SplitValue(super_category[k]);
                    SuperCategory _super_category = new SuperCategory()
                    {
                        SupCatCode = String.Format("{0}", _super_categories[0], CultureInfo.InvariantCulture),
                        SupCatName = String.Format("{0}", _super_categories[1], CultureInfo.InvariantCulture),
                        RecordStatus = String.Format("{0}", _super_categories[2], CultureInfo.InvariantCulture),
                        CreatedBy = Convert.ToInt32(String.Format("{0}", _super_categories[3], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                        CreatedDate = DateTime.UtcNow.AddDays(-new Random().Next(10, 30)),
                        ObjectState = ObjectState.Added
                    };

                    _unitOfWork.Repository<SuperCategory>().Insert(_super_category);
                    _unitOfWork.SaveChanges();
                }
            }
            #endregion

            #region DataSeeder for Sub Category
            //SubCatCode,SupCatCode,SubCatName,Status,CreatedDate,CreatedBy,ModifiedBy,ModifiedDate
            if (_ctx.SubCategories.Count() == 0)
            {
                for (int k = 0; k < sub_category.Length; k++)
                {
                    var _sub_categories = SplitValue(sub_category[k]);
                    SubCategory _sub_category = new SubCategory()
                    {
                        SubCatCode = String.Format("{0}", _sub_categories[0], CultureInfo.InvariantCulture),
                        SupCatCode = String.Format("{0}", _sub_categories[1], CultureInfo.InvariantCulture),
                        SubCatName = String.Format("{0}", _sub_categories[2], CultureInfo.InvariantCulture),
                        RecordStatus = String.Format("{0}", _sub_categories[3], CultureInfo.InvariantCulture),
                        CreatedBy = Convert.ToInt32(String.Format("{0}", _sub_categories[4], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                        CreatedDate = DateTime.UtcNow.AddDays(-new Random().Next(10, 30)),
                        ObjectState = ObjectState.Added
                    };

                    _unitOfWork.Repository<SubCategory>().Insert(_sub_category);
                    _unitOfWork.SaveChanges();
                }
            }
            #endregion


            #region DataSeeder for Ports

            if (_ctx.Ports.Count() == 0)
            {
                if (_ctx.Users.Count() > 0)
                {

                    for (int k = 0; k < port.Length; k++)
                    {
                        var _ports = SplitValue(port[k]);
                        Port _port = new Port()
                        {
                            //PortID,PortCode,PortName,InternationalCharacter,GeographicLocation,ContactNo,Email,Fax,Website,Description,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate

                            PortCode = String.Format("{0}", _ports[0], CultureInfo.InvariantCulture),
                            PortName = String.Format("{0}", _ports[1], CultureInfo.InvariantCulture),
                            InternationalCharacter = String.Format("{0}", _ports[2], CultureInfo.InvariantCulture),
                            GeographicLocation = String.Format("{0}", _ports[3], CultureInfo.InvariantCulture),
                            ContactNo = String.Format("{0}", _ports[4], CultureInfo.InvariantCulture),
                            Email = String.Format("{0}", _ports[5], CultureInfo.InvariantCulture),
                            Fax = String.Format("{0}", _ports[6], CultureInfo.InvariantCulture),
                            Website = String.Format("{0}", _ports[7], CultureInfo.InvariantCulture),
                            Description = String.Format("{0}", _ports[8], CultureInfo.InvariantCulture),
                            RecordStatus = String.Format("{0}", _ports[9], CultureInfo.InvariantCulture),
                            CreatedBy = Convert.ToInt32(String.Format("{0}", _ports[10], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            CreatedDate = DateTime.UtcNow.AddDays(-new Random().Next(10, 30)),
                            ObjectState = ObjectState.Added
                        };

                        _unitOfWork.Repository<Port>().Insert(_port);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            #endregion


            //#region DataSeeder for Quay
            //if (_ctx.QuaySet.Count() == 0)
            //{

            //    for (int k = 0; k < quays.Length; k++)
            //    {
            //        var _quays = SplitValue(quays[k]);
            //        Quay _quay = new Quay()
            //        {
            //            //QuayID,QuayCode,ShortName,QuayName,QuayLength,Description,PortID,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate
            //            QuayCode = String.Format("{0}", _quays[0]),
            //            QuayName = String.Format("{0}", _quays[1]),
            //            ShortName = String.Format("{0}", _quays[2]),
            //            QuayLength =Convert.ToInt32( String.Format("{0}", _quays[3])),
            //            Description = String.Format("{0}", _quays[4]),
            //            PortID = Convert.ToInt32(String.Format("{0}", _quays[4])),
            //            Status = String.Format("{0}", _quays[9]),                                           
            //            CreatedBy = Convert.ToInt32(String.Format("{0}", _quays[10])),
            //            CreatedDate = DateTime.UtcNow.AddDays(-new Random().Next(10, 30)),
            //            ObjectState = ObjectState.Added
            //        };

            //        _unitOfWork.Repository<Quay>().Insert(_quay);
            //        _unitOfWork.SaveChanges();
            //    }
            //}
            //#endregion



            //#region DataSeeder for Login Credentials
            //if (_ctx.UserSet.Count() == 0)

            //#endregion


            //#region DataSeeder for Login Credentials
            //if (_ctx.UserSet.Count() == 0)
            //{
            //    for (int k = 0; k < logindetails.Length; k++)
            //    {
            //        var _logindetails = SplitValue(logindetails[k]);

            //        long _portid = Convert.ToInt32(String.Format("{0}", _logindetails[3]));

            //        //LoginDetails _logindetail_find = (from a in _unitOfWork.Repository<LoginDetails>().Query().Get()
            //        //                                  where a.UserName == _logindetails[0] && a.PortID == _portid
            //        //                                  select a).FirstOrDefault<LoginDetails>();

            //        //if (_logindetail_find == null)
            //        //{
            //        User _logindetail = new User()
            //        {
            //            UserName = String.Format("{0}", _logindetails[0]),
            //            Password  = String.Format("{0}", _logindetails[1]),
            //            ApplicantID = Convert.ToInt32(String.Format("{0}", _logindetails[2])),//Convert.ToInt32(String.Format("{0}", _logindetails[2])),
            //            PortID = Convert.ToInt32(String.Format("{0}", _logindetails[3])),
            //            Status = String.Format("{0}", _logindetails[4]),
            //            CreatedBy = Convert.ToInt32(String.Format("{0}", _logindetails[5])),
            //            CreatedDate = DateTime.UtcNow.AddDays(-new Random().Next(10, 30)),
            //            ObjectState = ObjectState.Added
            //        };

            //        _unitOfWork.Repository<User>().Insert(_logindetail);
            //        _unitOfWork.SaveChanges();
            //        //}
            //    }
            //}
            //#endregion




            //#region DataSeeder for Agent Registration
            //if (_ctx.ApplicantSet.Count() == 0)
            //{
            //    try
            //    {
            //        for (int i = 0; i < applicants.Length; i++)
            //        {
            //            var app = SplitValue(applicants[i]);

            //            Applicant _applicant = new Applicant()
            //            {
            //                ApplicantName = String.Format("{0}", app[0]),
            //                ApplicantTradName = String.Format("{0}", app[1]),
            //                RegnNo = String.Format("{0}", app[2]),
            //                VatNo = String.Format("{0}", app[3]),
            //                IncTaxNo = String.Format("{0}", app[4]),
            //                SkilDevLevyNo = String.Format("{0}", app[5]),
            //                SarsTaxCleaCert = String.Format("{0}", app[6]),
            //                SAASOA = String.Format("{0}", app[7]),
            //                BBBEEQualify = String.Format("{0}", app[8]),
            //                BBBEEStatus = String.Format("{0}", app[9]),
            //                BBBEEStatVeri = String.Format("{0}", app[10]),
            //                Status = String.Format("{0}", app[11]),
            //                CreatedBy = Convert.ToInt32(String.Format("{0}", app[12])),
            //                CreatedDate = DateTime.UtcNow.AddDays(-new Random().Next(10, 30)),

            //                ObjectState = ObjectState.Added
            //            };

            //            ApplicantAddress _aplicants_address = new ApplicantAddress()
            //            {
            //                Applicant = _applicant,
            //                CreatedBy = 1,
            //                CreatedDate = System.DateTime.Today,
            //                Status = "A",
            //                NumStreet = "Street :" + RandomString(5),
            //                Suburb = "Suburb :" + RandomString(5),
            //                PostalCode = new Random().Next(1, 999999),
            //                TownCity = "City : " + RandomString(5),
            //                AddrType = 1, //Hardcoded from Sub Category
            //                ObjectState = ObjectState.Added
            //            };
            //            _applicant.Applicants_Address.Add(_aplicants_address);



            //            var _portid = new Random().Next(1, 8); //Hardcoded PortIds from 1 to 8

            //            Port _port = (from a in _unitOfWork.Repository<Port>().Query().Get()
            //                          where a.PortID == _portid
            //                          select a).FirstOrDefault<Port>();
            //            ApplicantPortWorkflow _appl_port_workflow = new ApplicantPortWorkflow()
            //            {
            //                Applicant = _applicant,
            //                WFStatus = "N",
            //                PortID = _port.PortID,
            //                ObjectState = ObjectState.Added
            //            };
            //            _applicant.Appl_Port_Workflow.Add(_appl_port_workflow);

            //            ApplicantAuthorizedContactPerson _appl_auth_cont_pers_det = new ApplicantAuthorizedContactPerson()
            //            {
            //                Applicant = _applicant,
            //                FirstName = _applicant.ApplicantName,
            //                LastName = _applicant.ApplicantTradName,
            //                IdentityType = 1,
            //                IdentityNo = RandomString(5),
            //                Designation = "Test",
            //                CellularNo = new Random().Next(),
            //                Status = "A",
            //                CreatedBy = 1,
            //                CreatedDate = System.DateTime.Today,
            //                ObjectState = ObjectState.Added,
            //                EmailAddress = _applicant.ApplicantTradName + "@gmail.com"
            //            };
            //            _applicant.Appl_Auth_Cont_Pers_Det.Add(_appl_auth_cont_pers_det);

            //            //Two Mandatory Attachments as per SRS
            //            //Document 1
            //            long _supcatid = Convert.ToInt32((from x in _unitOfWork.Repository<SuperCategory>().Query().Get().AsEnumerable<SuperCategory>() where x.SupCatCode == "DOCTYPE" select x).FirstOrDefault<SuperCategory>().SupCatID);

            //            SubCategory _sub_category1 = (from a in _unitOfWork.Repository<SubCategory>().Query().Get()
            //                                          where a.SupCatID == _supcatid
            //                                         && a.SubCatCode == "DOC1"
            //                                          select a).FirstOrDefault<SubCategory>();
            //            ApplicantAttachment _appl_attachments1 = new ApplicantAttachment()
            //            {
            //                Applicant = _applicant,
            //                DocuType = _sub_category1.SubCatID,
            //                DocuPath = "Sample" + _applicant.ApplicantID.ToString(),
            //                Status = "A",
            //                ObjectState = ObjectState.Added,
            //                CreatedBy = 1,
            //                CreatedDate = DateTime.UtcNow.AddDays(-new Random().Next(10, 30))
            //            };
            //            _applicant.Appl_Attachments.Add(_appl_attachments1);

            //            ////Document 2 ---NEED TO CHECK 
            //            //Sub_Category _sub_category2 = (from a in _unitOfWork.Repository<Sub_Category>().Query().Get()
            //            //                               where a.SupCatID == _supcatid
            //            //                              && a.SubCatCode == "DOC2"
            //            //                              select a).FirstOrDefault<Sub_Category>();
            //            //Appl_Attachments _appl_attachments2 = new Appl_Attachments()
            //            //{
            //            //    Applicant = _applicant,
            //            //    DocuType = _sub_category2.SubCatID,
            //            //    DocuPath = "Sample" + _applicant.ApplicantID.ToString(),
            //            //    Status = "A",
            //            //    ObjectState = ObjectState.Added,
            //            //    CreatedBy = 1,
            //            //    CreatedDate = DateTime.UtcNow.AddDays(-new Random().Next(10, 30))
            //            //};
            //            //_applicant.Appl_Attachments.Add(_appl_attachments2);

            //            //

            //            _unitOfWork.Repository<Applicant>().Insert(_applicant);
            //            _unitOfWork.SaveChanges();
            //        }

            //        //_ctx.SaveChanges();
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Trace.TraceInformation("Error: {0}", ex.ToString());
            //        throw ex;
            //    }
            //}
            ////#endregion

            //#region DataSeeder for AuditTrailConfig
            //if (_ctx.AuditTrailConfig.Count() == 0)
            //{
            //    for (int k = 0; k < audittrailconfig.Length; k++)
            //    {
            //        var _audittrailconfigs = SplitValue(audittrailconfig[k]);
            //        AuditTrailConfiguration _audittrailconfig = new AuditTrailConfiguration()
            //        {
            //            ControlerName = String.Format("{0}", _audittrailconfigs[0]),
            //            ActionName = String.Format("{0}", _audittrailconfigs[1]),
            //            UserFriendlyDescription = String.Format("{0}", _audittrailconfigs[2]),
            //            IsAuditTrailRequired = String.Format("{0}", _audittrailconfigs[3]),
            //            ObjectState = ObjectState.Added
            //        };

            //        _unitOfWork.Repository<AuditTrailConfiguration>().Insert(_audittrailconfig);
            //        _unitOfWork.SaveChanges();
            //    }
            //}
            //#endregion


            #region DataSeeder for Module
            if (_ctx.Modules.Count() == 0)
            {
                if (_ctx.Users.Count() > 0)
                {

                    //Parent ModuleID, ModuleName , Order No,Status , CreatedBy , CreatedData
                    //  "NULL,Administration,1,Y,A,1," + DateTime.Now.ToString() +",NULL,NULL",           
                    //  "1,Administration,1,A,1,"+ DateTime.Now.ToString() +",NULL,NULL", 

                    //
                    for (int k = 0; k < module.Length; k++)
                    {
                        var _modules = SplitValue(module[k]);
                        Module _module = new Module();


                        if (_modules[0] != "NULL")
                        {
                            _module.ParentModuleID = Convert.ToInt32(String.Format("{0}", _modules[0], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                        }
                        _module.ModuleName = String.Format("{0}", _modules[1], CultureInfo.InvariantCulture);
                        _module.OrderNo = Convert.ToInt32(String.Format("{0}", _modules[2], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                        _module.RecordStatus = String.Format("{0}", _modules[3], CultureInfo.InvariantCulture);
                        _module.CreatedBy = Convert.ToInt32(String.Format("{0}", _modules[4], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                        _module.CreatedDate = Convert.ToDateTime(String.Format("{0}", _modules[5], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                        _module.ObjectState = ObjectState.Added;






                        _unitOfWork.Repository<Module>().Insert(_module);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            #endregion




            // Module ID , Entity Name,Page Url , Order No , Record Status,Created By, Created Date, Modified By , Modified Date
            //"5,User Groups,UserGroups,1,A,1,"+ DateTime.Now.ToString() +",NULL,NULL",
            #region DataSeeder for Entity
            if (_ctx.Entities.Count() == 0)
            {
                if (_ctx.Users.Count() > 0)
                {


                    for (int k = 0; k < entities.Length; k++)
                    {
                        var _entities = SplitValue(entities[k]);
                        Entity _entity = new Entity()
                        {
                            ModuleID = Convert.ToInt32(String.Format("{0}", _entities[0], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            EntityName = String.Format("{0}", _entities[1], CultureInfo.InvariantCulture),
                            PageUrl = String.Format("{0}", _entities[2], CultureInfo.InvariantCulture),
                            OrderNo = Convert.ToInt32(String.Format("{0}", _entities[3], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            RecordStatus = String.Format("{0}", _entities[4], CultureInfo.InvariantCulture),
                            CreatedBy = Convert.ToInt32(String.Format("{0}", _entities[5], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            CreatedDate = Convert.ToDateTime(String.Format("{0}", _entities[6], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            ObjectState = ObjectState.Added
                        };

                        _unitOfWork.Repository<Entity>().Insert(_entity);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            #endregion


            //"1,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
            #region DataSeeder for EntityPrivileges
            if (_ctx.EntityPrivileges.Count() == 0)
            {
                if (_ctx.Users.Count() > 0)
                {

                    for (int k = 0; k < EntityPrivileges.Length; k++)
                    {
                        var _entitypriveleges = SplitValue(EntityPrivileges[k]);
                        EntityPrivilege _entityprivelege = new EntityPrivilege()
                        {

                            EntityID = Convert.ToInt32(String.Format("{0}", _entitypriveleges[0], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            SubCatCode = String.Format("{0}", _entitypriveleges[1], CultureInfo.InvariantCulture),
                            RecordStatus = String.Format("{0}", _entitypriveleges[2], CultureInfo.InvariantCulture),
                            CreatedBy = Convert.ToInt32(String.Format("{0}", _entitypriveleges[3], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            CreatedDate = Convert.ToDateTime(String.Format("{0}", _entitypriveleges[4], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            ObjectState = ObjectState.Added
                        };

                        _unitOfWork.Repository<EntityPrivilege>().Insert(_entityprivelege);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            #endregion




            #region DataSeeder for Roles
            if (_ctx.Roles.Count() == 0)
            {
                if (_ctx.Users.Count() > 0)
                {

                    for (int k = 0; k < RolesData.Length; k++)
                    {
                        var _usergroupheader = SplitValue(RolesData[k]);
                        Role _role = new Role()
                        {

                            RoleName = String.Format( CultureInfo.InvariantCulture,"{0}", _usergroupheader[0]),
                            RecordStatus = String.Format( CultureInfo.InvariantCulture,"{0}", _usergroupheader[1], CultureInfo.InvariantCulture),
                            CreatedBy = Convert.ToInt32(String.Format("{0}", _usergroupheader[2], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            CreatedDate = Convert.ToDateTime(String.Format("{0}", _usergroupheader[3], CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            ObjectState = ObjectState.Added
                        };

                        _unitOfWork.Repository<Role>().Insert(_role);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            #endregion



            #region DataSeeder for RolePrivileges
            if (_ctx.RolePrivileges.Count() == 0)
            {
                if (_ctx.Users.Count() > 0)
                {

                    for (int k = 0; k < RolePrivileges.Length; k++)
                    {
                        var _RolePrivileges = SplitValue(RolePrivileges[k]);
                        RolePrivilege _RolePrivilege = new RolePrivilege()
                        {
                            RoleID = Convert.ToInt32(String.Format( CultureInfo.InvariantCulture,"{0}", _RolePrivileges[0]), CultureInfo.InvariantCulture),
                            EntityID = Convert.ToInt32(String.Format(CultureInfo.InvariantCulture, "{0}", _RolePrivileges[1]), CultureInfo.InvariantCulture),
                            SubCatCode = String.Format(CultureInfo.InvariantCulture, "{0}", _RolePrivileges[2]),
                            RecordStatus = String.Format(CultureInfo.InvariantCulture, "{0}", _RolePrivileges[3]),
                            CreatedBy = Convert.ToInt32(String.Format(CultureInfo.InvariantCulture, "{0}", _RolePrivileges[4]), CultureInfo.InvariantCulture),
                            CreatedDate = Convert.ToDateTime(String.Format(CultureInfo.InvariantCulture, "{0}", _RolePrivileges[5]), CultureInfo.InvariantCulture),
                            ObjectState = ObjectState.Added
                        };

                        _unitOfWork.Repository<RolePrivilege>().Insert(_RolePrivilege);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            #endregion



            //#region DataSeeder for WorkFlow
            //if (_ctx.Workflows.Count() == 0)
            //{
            //    if (_ctx.Users.Count() > 0)
            //    {

            //        for (int k = 0; k < workflow.Length; k++)
            //        {
            //            var _workflow = SplitValue(workflow[k]);
            //            Workflow _workflows = new Workflow()
            //            {
            //                WorkflowCode = String.Format("{0}", _workflow[0]),
            //                WorkflowName = String.Format("{0}", _workflow[1]),
            //                RecordStatus = String.Format("{0}", _workflow[2]),
            //                CreatedBy = Convert.ToInt32(String.Format("{0}", _workflow[3])),
            //                CreatedDate = Convert.ToDateTime(String.Format("{0}", _workflow[4])),
            //                ModifiedBy = Convert.ToInt32(String.Format("{0}", _workflow[5])),
            //                ModifiedDate = Convert.ToDateTime(String.Format("{0}", _workflow[6])),
            //                ObjectState = ObjectState.Added
            //            };

            //            _unitOfWork.Repository<Workflow>().Insert(_workflows);
            //            _unitOfWork.SaveChanges();
            //        }
            //    }
            //}
            //#endregion


            //#region DataSeeder for WorkflowTasks
            //if (_ctx.WorkflowTasks.Count() == 0)
            //{
            //    if (_ctx.Users.Count() > 0)
            //    {

            //        for (int k = 0; k < workflowaction.Length; k++)
            //        {
            //            var _workflowaction = SplitValue(workflowaction[k]);
            //            WorkflowTask _workflowactions = new WorkflowTask()
            //            {
            //                WorkflowTaskCode = String.Format("{0}", _workflowaction[0]),
            //                WorkflowTaskName = String.Format("{0}", _workflowaction[1]),
            //                StepNumber = Convert.ToInt32(String.Format("{0}", _workflowaction[2])),
            //                Url = String.Format("{0}", _workflowaction[3]),
            //                Method = String.Format("{0}", _workflowaction[4]),
            //                RecordStatus = String.Format("{0}", _workflowaction[5]),
            //                CreatedBy = Convert.ToInt32(String.Format("{0}", _workflowaction[6])),
            //                CreatedDate = Convert.ToDateTime(String.Format("{0}", _workflowaction[7])),
            //                ModifiedBy = Convert.ToInt32(String.Format("{0}", _workflowaction[8])),
            //                ModifiedDate = Convert.ToDateTime(String.Format("{0}", _workflowaction[9])),
            //                ObjectState = ObjectState.Added
            //            };

            //            _unitOfWork.Repository<WorkflowTask>().Insert(_workflowactions);
            //            _unitOfWork.SaveChanges();
            //        }
            //    }
            //}
            //#endregion


            //#region DataSeeder for WorkflowStepConfigs
            //if (_ctx.WorkflowStepConfigs.Count() == 0)
            //{
            //    if (_ctx.Users.Count() > 0)
            //    {

            //        for (int k = 0; k < workflowconfigure.Length; k++)
            //        {
            //            var _workflowconfigure = SplitValue(workflowconfigure[k]);
            //            WorkflowStepConfig _workflowconfigures = new WorkflowStepConfig()
            //            {
            //                WorkflowCode = String.Format("{0}", _workflowconfigure[0]),
            //                StepNumber = Convert.ToInt32(String.Format("{0}", _workflowconfigure[1])),                            
            //                RecordStatus = String.Format("{0}", _workflowconfigure[2]),
            //                CreatedBy = Convert.ToInt32(String.Format("{0}", _workflowconfigure[3])),
            //                CreatedDate = Convert.ToDateTime(String.Format("{0}", _workflowconfigure[4])),
            //                ModifiedBy = Convert.ToInt32(String.Format("{0}", _workflowconfigure[5])),
            //                ModifiedDate = Convert.ToDateTime(String.Format("{0}", _workflowconfigure[6])),
            //                ObjectState = ObjectState.Added
            //            };

            //            _unitOfWork.Repository<WorkflowStepConfig>().Insert(_workflowconfigures);
            //            _unitOfWork.SaveChanges();
            //        }
            //    }
            //}
            //#endregion

            //#region DataSeeder for WorkflowStepRoleConfigs
            //if (_ctx.WorkflowStepRoleConfigs.Count() == 0)
            //{
            //    if (_ctx.Users.Count() > 0)
            //    {

            //        for (int k = 0; k < workflowsteproleconfigure.Length; k++)
            //        {
            //            var _workflowconfigure = SplitValue(workflowsteproleconfigure[k]);
            //            WorkflowStepRoleConfig _workflowconfigures = new WorkflowStepRoleConfig()
            //            {
            //                WorkflowCode = String.Format("{0}", _workflowconfigure[0]),
            //                StepNumber = Convert.ToInt32(String.Format("{0}", _workflowconfigure[1])),
            //                RoleID = Convert.ToInt32(String.Format("{0}", _workflowconfigure[2])),
            //                RecordStatus = String.Format("{0}", _workflowconfigure[3]),
            //                CreatedBy = Convert.ToInt32(String.Format("{0}", _workflowconfigure[4])),
            //                CreatedDate = Convert.ToDateTime(String.Format("{0}", _workflowconfigure[5])),
            //                ModifiedBy = Convert.ToInt32(String.Format("{0}", _workflowconfigure[6])),
            //                ModifiedDate = Convert.ToDateTime(String.Format("{0}", _workflowconfigure[7])),
            //                ObjectState = ObjectState.Added
            //            };

            //            _unitOfWork.Repository<WorkflowStepRoleConfig>().Insert(_workflowconfigures);
            //            _unitOfWork.SaveChanges();
            //        }
            //    }
            //}
            //#endregion



            


            //#region DataSeeder for UserMaster Group Detail
            //if (_ctx.UserMaster_Group_DTLSet.Count() == 0)
            //{
            //    for (int k = 0; k < usermastergroupdetails.Length; k++)
            //    {
            //        var _usermastergroupdetails = SplitValue(usermastergroupdetails[k]);
            //        UserMaster_Group_DTL _usermastergroupdetail = new UserMaster_Group_DTL()
            //        {
            //            UserGroupID = Convert.ToInt32(String.Format("{0}", _usermastergroupdetails[0])),
            //            UserId = Convert.ToInt32(String.Format("{0}", _usermastergroupdetails[1])),
            //            ObjectState = ObjectState.Added
            //        };

            //        _unitOfWork.Repository<UserMaster_Group_DTL>().Insert(_usermastergroupdetail);
            //        _unitOfWork.SaveChanges();
            //    }
            //}
            //#endregion


            //#region DataSeeder for User Group Detail Set
            //if (_ctx.User_Group_DTLSet.Count() == 0)
            //{
            //    for (int k = 0; k < usergroupdetails.Length; k++)
            //    {
            //        var _usergroupdetails = SplitValue(usergroupdetails[k]);
            //        User_Group_DTL _usergroupdetail = new User_Group_DTL()
            //        {
            //            UserGroupID = Convert.ToInt32(String.Format("{0}", _usergroupdetails[0])),
            //            ModuleID = Convert.ToInt32(String.Format("{0}", _usergroupdetails[1])),                                                               
            //            SubModuleID = Convert.ToInt32(String.Format("{0}", _usergroupdetails[2])),
            //            PageID = Convert.ToInt32(String.Format("{0}", _usergroupdetails[3])),    
            //            privileges = String.Format("{0}", _usergroupdetails[4]),
            //            ObjectState = ObjectState.Added
            //        };

            //        _unitOfWork.Repository<User_Group_DTL>().Insert(_usergroupdetail);
            //        _unitOfWork.SaveChanges();
            //    }
            //}
            //#endregion





        }

        private static string[] SplitValue(string val)
        {
            return val.Split(',');
        }

        private string RandomString(int size)
        {
            Random _rng = new Random((int)DateTime.Now.Ticks);
            string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }



        // Role Data
        static string[] RolesData = { "Admin,A,1," + DateTime.Now.ToString() + ",NULL,NULL" };

        // Role Data
        // Entity ID,SubCatCode,Record Status.Created By, Created Date , Modified By,Modified Date
        static string[] EntityPrivileges =
        {"1,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "2,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "2,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "2,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "2,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "3,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "3,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "3,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "3,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "4,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "4,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "4,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "4,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "5,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "5,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "5,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "5,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "6,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "6,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "6,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "6,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "7,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "7,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "7,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "7,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "8,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "8,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "8,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "8,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "9,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "9,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "9,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "9,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "10,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "10,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "10,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "10,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL"
        };

        //RoleID,Entity ID,SubCatCode,RecordStatus,Createdby , created date,Modified By,Modified Date
        static string[] RolePrivileges =
        {
         "1,1,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,1,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,1,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,1,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,2,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,2,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,2,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,2,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,3,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,3,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,3,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,3,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,4,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,4,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,4,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,4,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,5,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,5,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,5,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,5,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,6,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,6,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,6,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,6,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,7,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,7,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,7,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,7,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,8,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,8,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,8,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,8,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,9,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,9,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,9,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,9,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,10,ADD,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,10,EDIT,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,10,VIEW,A,1," + DateTime.Now.ToString() + ",NULL,NULL",
         "1,10,DEL,A,1," + DateTime.Now.ToString() + ",NULL,NULL"
        };







        static string[] port = 
        { 
            //PortID,PortCode,PortName,InternationalCharacter,GeographicLocation,ContactNo,Email,Fax,Website,Description,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate

            "RB,Richards Bay,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",richardsbay@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DB,Durban,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",durban@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "EL,East London,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",eastlondon@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "NG,Ngqura,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",ngqura@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PE,Port Elizabeth,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",portelizabeth@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "MB,Mossel Bay,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",mosselbay@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "CT,Cape Town,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",capetown@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "SB,Saldanha Bay,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",saldanha@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL"
        };

        static string[] quays = 
        { 
            //QuayCode,PortName,InternationalCharacter,GeographicLocation,ContactNo,Email,Fax,Website,Description,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate

            "PO,Point,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",richardsbay@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DB,Durban,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",durban@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "EL,East London,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",eastlondon@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "NG,Ngqura,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",ngqura@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PE,Port Elizabeth,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",portelizabeth@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "MB,Mossel Bay,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",mosselbay@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "CT,Cape Town,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",capetown@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "SB,Saldanha Bay,NULL,NULL," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",saldanha@gmail.com," + new Random().Next().ToString(CultureInfo.InvariantCulture) + ",NULL,NULL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL"
        };



        static string[] applicants = 
        { 
            //ApplicantID,ApplicantName,ApplicantTradName,RegnNo,VatNo,IncTaxNo,SkilDevLevyNo,SarsTaxCleaCert,SAASOA,BBBEEQualify,BBBEEStatVeri,BBBEEStatus,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate

            "Taiseer,Joudeh,RegNo1,VatNo1,IncTaxNo1,SkilDevLevyNo1,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Hasan,Ahmad,RegNo2,VatNo2,IncTaxNo2,SkilDevLevyNo2,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Moatasem,Ahmad,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Salma,Tamer,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Ahmad,Radi,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Bill,Gates,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Shareef,Khaled,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null" ,
            "Aram,Naser,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Layla,Ibrahim,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Rema,Oday,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Fikri,Husein,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Zakari,Husein,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Taher,Waleed,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Tamer,Wesam,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Khaled,Hasaan,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,1,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Asaad,Ibrahim,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,2,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Tareq,Nassar,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,2,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Diana,Lutfi,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,2,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Tamara,Malek,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,2,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Arwa,Kamal,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,2,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Jana,Ahmad,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,2,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Nisreen,Tamer,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,2,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null",
            "Noura,Ahmad,RegNo3,VatNo3,IncTaxNo3,SkilDevLevyNo3,N,N,N,2,Y,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",null,null"
        };

        static string[] super_category = 
        { 
            //SupCatID,SupCatCode,SupCatName,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate

            "ADTY,Address Type,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DCTY,Document Types,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "BETY,Berth Type,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "USTY,User Type,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PRIV,Priveleges,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "WFST,WF Status,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",            
            "BU,Business Unit,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",    
            "CC,Cost Center,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",    
            "DEPT,Department,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",    
            "DESG,Designation,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",    
            "OU,Organizational Unit,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",    
            "PA,Payroll Area,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",    
            "PSA,Personal Sub Area,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",    
            "PSG,Payroll AreaPS Group,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "RSV,Reason for Visit,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DOCK,Dock Types,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "UOM,U o M,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PURP,Purpose,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DOCT,Document Category,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",

 
        };

        static string[] sub_category = 
        {
            //SubCatCode,SupCatCode,SubCatName,Status,CreatedDate,CreatedBy,ModifiedBy,ModifiedDate
          
                       
            "ADPE,ADTY,Permenant Address,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "ADTE,ADTY,Temporary Address,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DOC1,DCTY,Copy of Resolution Authorizing the Person Declared in Authorized Person Section as Annexure 1B,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DOC2,DCTY,Copy Of Company or Close Corporation Registration Certificate As annexure 1A,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DOC3,DCTY,Valid SARS tax clearance certificate as annexure 1C,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DOC4,DCTY,proof of current membership copy as annexure 1D,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DOC5,DCTY,An annexure 2A,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DOC6,DCTY,An annexure 2B,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DOC7,DCTY,As annexure 2C,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "BTCO,BETY,Container,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "BTMA,BETY,Machines,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "TO,USTY,Terminal Operator,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "EMP,USTY,Employee,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "AGNT,USTY,Agent,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "ADD,PRIV,Add,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "EDIT,PRIV,Edit,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "VIEW,PRIV,View,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DEL,PRIV,Delete,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "WFSN,WFST,New,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "WFSV,WFST,Verified,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "WFSA,WFST,Approved,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "BU1,BU,Port of Cape Town,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "BU2,BU,Coega Bay,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "CC1,CC,BERTHING,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "CC2,BUCC,HABOUR MASTER,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "CC3,CC,MARINE MANAGER,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "CC4,CC,PILOTS POOL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "CC5,CC,PORT CONTROL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "CC6,CC,PORT OPERATIONS,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "CC7,CC,TUGS POOL,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "CC8,CC,MAN CADRE : CPT,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DSG1,DESG,Assistant Dockmaster,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DSG2,DESG,Admin Officer,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DSG3,DESG,Able Seaman,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DSG4,DESG,Asst. Berthing Manager,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DEP1,DEPT,Technical,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DEP2,DEPT,QA,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "OU1,OU,Maritime Services,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "OU2,OU,Vessel Traffic Services,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PA1,PA,Cape Town,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PA2,PA,Durban,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PA3,PA,East London,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PSA1,PSA,ADVISE,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PSA2,PSA,K2,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PSG1,PSG,Harbour Master,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PSG2,PSG,Lighthouse Serv,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "LOAD,RSV,Load,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DISC,RSV,Discharge,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "TRAN,RSV,Transship,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "BUNK,RSV,Bunkers,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "LAUP,RSV,Lay-up,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "REPA,RSV,Repairs,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "STOR,RSV,Stores,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "OTHR,RSV,Others,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "STBO,DOCK,Star Board,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "POSI,DOCK,Port Side,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "MT,UOM,Mt,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "TONS,UOM,Tons,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "LD,PURP,Load,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "DC,PURP,Discharge,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "TS,PURP,Transship,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "IMDO,DOCT,IMDG Documents,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PHCD,DOCT,PHC Documents,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "ISPS,DOCT,ISPS Documents,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "PCD,DOCT,Port Clearance Documents,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "LDD,DOCT,Light Dues,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "SRD,DOCT,Ship Registry,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "ITCD,DOCT,ITC,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "SRD,DOCT,Sailing Request,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "BWD,DOCT,Ballast Water,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "WDD,DOCT,Waste Declaration,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL"
        };
        static string[] logindetails = {
                                            "admin,h4sQH0mDYomSh+92DKg8CQ==," + new Random().Next(1,23) + ","+ new Random().Next(1,8) +",A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL"
                                        };
        static string[] berth = { };
        static string[] bollard = { };
        static string[] quay = { };

        static string[] audittrailconfig = 
        { 
            "AgentRegistration,Registration,User has logged into registration page,Y",
            "AgentRegistration,ApplicantRegistration,User has logged into applicant registration page,Y",
            "AgentRegistration,AgentDetails,NULL,Y",
            "AgentRegistration,RegistrationNew,NULL,Y",
            "AgentRegistration,AgentMaster,NULL,Y",
            "Ports,ManagePorts,NULL,Y",
            "Ports,DashBoard,NULL,Y",
            "Ports,PortMaster,NULL,Y",
            "ArrivalNotification,ArrivalNotification,NULL,Y",
            "Bollard,Bollard,NULL,Y",
            "Account,PostCheckCredentials,NULL,Y",
            "Ports,GetAllPorts,NULL,Y",
            "Ports,PostPortData,NULL,Y"
        };

        static string[] module = 
           { 
           //Parent ModuleID, ModuleName , Order No,Status , CreatedBy , CreatedData
            
            "NULL,Administration,1,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "NULL,Marine Operations,2,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "NULL,Port Operations,3,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "NULL,MIS,4,A,1," + DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
            "1,Administration,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "2,Masters,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "2,Configuration,2,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "2,Vessel Traffic Services,3,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "2,Dredging Operations,4,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "3,Cargo,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "3,Fuel Management,2,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "3,Infrastructure Management,3,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "3,Incident Management,4,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "3,SHE Requirements,5,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "4,Report Builder,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "4,Marine Operations,2,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
            "4,Port Operations,3,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL"
            
          };

        //SubModuleID, PageName, PageUrl, OrderNo, ReportParentID, Reportpagecode, Actions, MenuItem, WorkflowApplicable, IsDashboard, IsListScreen, Status, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
        static string[] entities =         
        { 
            // Module ID , Entity Name,Page Url , Order No , Record Status,Created By, Created Date, Modified By , Modified Date
        "5,User Groups,UserGroups,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
        "5,Modules,Modules,3,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
        "5,Users Privileges,UsersPrivileges,4,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
        "5,General Settings,GeneralSettings,5,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL", 
        "5,Audit Trail,AuditLog/ManageAuditLog,6,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
        "6,Port,Ports/ManagePorts,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
        "6,Quay,ManageQuays/ManageQuays,2,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
        "6,Berth,Berth/BerthMaster,3,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
        "6,Bollard,Bollards/ManageBollards,4,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL",
        "6,Agent,Agent/AgentMaster,5,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",NULL,NULL"       
        };

        static string[] userrole = { "1,1" };

        //UserGroupID, ModuleID, SubModuleID, PageID, privileges        
        static string[] usergroupdetails =
        {
        "1,1,1,1,NULL",
        "1,1,1,2,NULL", 
        "1,1,1,3,NULL",
        "1,1,1,5,NULL", 
        "1,2,2,6,NULL", 
        "1,2,2,7,NULL",       
        "1,2,2,8,NULL", 
        "1,2,2,9,NULL",
        "1,2,2,10,NULL"         
        };


        static string[] workflow =
        {
            "AN,Arrival Notification,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "SR,Service Request,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "AR,Agent Registration,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +""

        };

        static string[] workflowaction =
        {            

            "CR, Create,0,Page/page,POST,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "VA,Verify Approve,1,Page/page,PUT,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "VR,Verify Reject,1,Page/Page,PUT,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "VE,Verify Expire,1,Page/Page,PUT,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "AA,Approval Approve,2,Page/Page,PUT,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "AR,Approval Reject,2,Page/Page,PUT,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "AE,Approval Expire,2,Page/Page,PUT,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "C,Confirm,3,Page/Page,PUT,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "E,End,100,NULL,NULL,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +""

        };


        static string[] workflowconfigure =
        {           

            "AN,0,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "AN,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "AN,2,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "AN,100,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "SR,0,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "SR,2,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "SR,100,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +""

        };

        static string[] workflowsteproleconfigure =
        {           

            "AN,0,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "AN,1,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "AN,2,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "AN,100,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "SR,0,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "SR,2,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +"",
            "SR,100,1,A,1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +",1,"+ DateTime.Now.ToString(CultureInfo.InvariantCulture) +""

        };

    }

}

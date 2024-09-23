using Core.Repository;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IPMS.Repository
{
    public class TerminalOperatorRepository : ITerminalOperatorRepository
    {
        private IUnitOfWork _unitOfWork;
       // private readonly ILog log;

        private SubCategoryRepository _subCategoryRepository;

        public TerminalOperatorRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            XmlConfigurator.Configure();
           // log = 
            LogManager.GetLogger(typeof(TerminalOperatorRepository));
            _subCategoryRepository = new SubCategoryRepository(_unitOfWork);
        }

        #region GetTerminalOperatorList
        /// <summary>
        /// To get Terminal Operator List
        /// </summary>
        /// <returns></returns>
        public List<TerminalOperatorVO> GetTerminalOperatorList(string portCode)
        {
            var query = (from t in _unitOfWork.Repository<TerminalOperator>().Query()
                            .Include(t => t.BusinessAddress)
                            .Include(t => t.PostalAddress)
                            .Include(t => t.TerminalOperatorBerths)
                            .Include(t => t.TerminalOperatorCargoHandlings)
                            .Include(w => w.TerminalOperatorPorts).Tracking(true).Select()
                         orderby t.TerminalOperatorID descending
                         select new
                         {
                             TerminalOperator = t,
                             BusinessAddress = t.BusinessAddress,
                             PostalAddress = t.PostalAddress,
                             TerminalOperatorBerths = t.TerminalOperatorBerths.Where(p => p.RecordStatus == "A"),
                             TerminalOperatorCargoHandling = t.TerminalOperatorCargoHandlings.Where(q => q.RecordStatus == "A"),
                             TerminalOperatorPorts = t.TerminalOperatorPorts.Where(q => q.PortCode == portCode)
                         }
                    );

            var terminalOperators = query.ToArray().Select(o => o.TerminalOperator).ToList<TerminalOperator>();

            return terminalOperators.MapToDto();
        }
        #endregion

        #region AddterminalOperator
        /// <summary>
        /// To Add Terminal Operator Data
        /// </summary>
        /// <param name="entityVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TerminalOperatorVO AddTerminalOperator(TerminalOperatorVO entityVo, int userId)
        {
            if (entityVo != null)
            {
                entityVo.CreatedBy = userId;
                entityVo.CreatedDate = DateTime.Now;
                entityVo.ModifiedBy = userId;
                entityVo.ModifiedDate = DateTime.Now;
                entityVo.RecordStatus = "A";

                TerminalOperator entity = null;
                entity = entityVo.MapToEntity();

                entity.ObjectState = ObjectState.Added;
                entity.BusinessAddress.CreatedBy = entity.CreatedBy;
                entity.BusinessAddress.CreatedDate = entity.CreatedDate;
                entity.BusinessAddress.ModifiedBy = entity.CreatedBy;
                entity.BusinessAddress.ModifiedDate = entity.CreatedDate;
                entity.BusinessAddress.RecordStatus = "A";
                _unitOfWork.Repository<Address>().Insert(entity.BusinessAddress);
                _unitOfWork.SaveChanges();

                entity.PostalAddressID = entity.BusinessAddress.AddressID;
                if (entity.PostalAddress != null)
                {
                    entity.PostalAddress.CreatedBy = entityVo.CreatedBy;
                    entity.PostalAddress.CreatedDate = entityVo.CreatedDate;
                    entity.PostalAddress.ModifiedBy = entityVo.CreatedBy;
                    entity.PostalAddress.ModifiedDate = entityVo.CreatedDate;
                    entity.PostalAddress.RecordStatus = "A";

                    _unitOfWork.Repository<Address>().Insert(entity.PostalAddress);
                    _unitOfWork.SaveChanges();
                    entity.BusinessAddressID = entity.PostalAddress.AddressID;
                }

                List<TerminalOperatorBerth> applberths = entity.TerminalOperatorBerths.ToList();
                List<TerminalOperatorCargoHandling> applcargos = entity.TerminalOperatorCargoHandlings.ToList();
                List<TerminalOperatorPort> terminalperatorPortList = entity.TerminalOperatorPorts.ToList();

                entity.PostalAddress = null;
                entity.BusinessAddress = null;
                entity.ArrivalNotifications = null;
                entity.SubCategory = null;
                entity.User = null;
                entity.User1 = null;
                entity.TerminalOperatorBerths = null;
                entity.TerminalOperatorCargoHandlings = null;
                entity.TerminalOperatorPorts = null;

                string strBirth = "";
                string strCargo = "";

                string Licencefor = "";
                foreach (string SrvType in entityVo.TerminalOperatorServiceTypesArray)
                {
                    if (SrvType == "TOB")
                    {
                        strBirth = "TOB";
                    }
                    if (SrvType == "TOCH")
                    {
                        strCargo = "TOCH";
                    }
                }
                // if (strBirth != "" && strCargo != "")
                if (!String.IsNullOrEmpty(strBirth) && !String.IsNullOrEmpty(strCargo))
                {
                    Licencefor = "BOTH";
                }
                else if (!String.IsNullOrEmpty(strBirth))
                {
                    Licencefor = strBirth;


                }
                else if (!String.IsNullOrEmpty(strCargo))
                {
                    Licencefor = strCargo;
                }

                entity.LicensedFor = Licencefor;
                entity.BusinessAddress = null;
                _unitOfWork.Repository<TerminalOperator>().Insert(entity);
                _unitOfWork.SaveChanges();

                if (terminalperatorPortList.Count > 0)
                {
                    foreach (var requestforport in terminalperatorPortList)
                    {
                        requestforport.TerminalOperatorID = entity.TerminalOperatorID;
                        requestforport.CreatedBy = entity.CreatedBy;
                        requestforport.CreatedDate = entity.CreatedDate;
                        requestforport.ModifiedBy = entity.ModifiedBy;
                        requestforport.ModifiedDate = entity.ModifiedDate;
                        requestforport.RecordStatus = "A";
                    }
                    _unitOfWork.Repository<TerminalOperatorPort>().InsertRange(terminalperatorPortList);
                }

                if (applberths.Count > 0)
                {
                    foreach (var applberth in applberths)
                    {
                        applberth.TerminalOperatorID = entity.TerminalOperatorID;
                        applberth.CreatedBy = entity.CreatedBy;
                        applberth.CreatedDate = entity.CreatedDate;
                        applberth.ModifiedBy = entity.ModifiedBy;
                        applberth.ModifiedDate = entity.ModifiedDate;
                        applberth.RecordStatus = "A";
                    }
                    _unitOfWork.Repository<TerminalOperatorBerth>().InsertRange(applberths);
                    _unitOfWork.SaveChanges();
                }

                if (applcargos.Count > 0)
                {
                    foreach (var applcargo in applcargos)
                    {
                        applcargo.TerminalOperatorID = entity.TerminalOperatorID;
                        applcargo.CreatedBy = entity.CreatedBy;
                        applcargo.ModifiedBy = entity.ModifiedBy;
                        applcargo.CreatedDate = entity.CreatedDate;
                        applcargo.ModifiedDate = entity.ModifiedDate;
                        applcargo.RecordStatus = "A";
                    }
                    _unitOfWork.Repository<TerminalOperatorCargoHandling>().InsertRange(applcargos);
                    _unitOfWork.SaveChanges();
                }

                entityVo = entity.MapToDto();
            }
            return entityVo;
        }
        #endregion

        #region ModifyTerminalOperator

        /// <summary>
        /// To Modify Terminal Operator Data
        /// </summary>
        /// <param name="entityVo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TerminalOperatorVO ModifyTerminalOperator(TerminalOperatorVO entityVo, int userId)
        {
            if (entityVo != null)
            {

                entityVo.CreatedBy = userId;
                entityVo.CreatedDate = DateTime.Now;
                entityVo.ModifiedBy = userId;
                entityVo.ModifiedDate = DateTime.Now;
                // }

                TerminalOperator entity = null;
                entity = entityVo.MapToEntity();
                entity.ObjectState = ObjectState.Modified;

                List<TerminalOperatorBerth> applberths = entity.TerminalOperatorBerths.ToList();
                List<TerminalOperatorCargoHandling> applcargos = entity.TerminalOperatorCargoHandlings.ToList();
                List<TerminalOperatorPort> terminalPorts = entity.TerminalOperatorPorts.ToList();

                entity.PostalAddress.CreatedBy = entity.CreatedBy;
                entity.PostalAddress.CreatedDate = entity.CreatedDate;

                entity.PostalAddress.ModifiedBy = entity.ModifiedBy;
                entity.PostalAddress.ModifiedDate = entity.ModifiedDate;
                entity.PostalAddress.RecordStatus = entity.RecordStatus;

                entity.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Address>().Update(entity.PostalAddress);
                entity.PostalAddressID = entity.BusinessAddress.AddressID;
                _unitOfWork.SaveChanges();

                entity.BusinessAddress.CreatedBy = entity.CreatedBy;
                entity.BusinessAddress.CreatedDate = entity.CreatedDate;
                entity.BusinessAddress.ModifiedBy = entity.ModifiedBy;
                entity.BusinessAddress.ModifiedDate = entity.ModifiedDate;
                entity.BusinessAddress.RecordStatus = entity.RecordStatus;

                entity.ObjectState = ObjectState.Modified;
                _unitOfWork.Repository<Address>().Update(entity.BusinessAddress);
                entity.BusinessAddressID = entity.PostalAddress.AddressID;
                _unitOfWork.SaveChanges();

                entity.PostalAddress = null;
                entity.BusinessAddress = null;
                entity.ArrivalNotifications = null;
                entity.SubCategory = null;
                entity.User = null;
                entity.User1 = null;
                entity.TerminalOperatorBerths = null;
                entity.TerminalOperatorCargoHandlings = null;
                entity.TerminalOperatorPorts = null;

                string strCargo;
                string Licencefor;
                var strBirth = GetValueStrBirth(entityVo, out strCargo, out Licencefor);

                //  if (strBirth != "" && strCargo != "")

                if (!String.IsNullOrEmpty(strBirth) && !String.IsNullOrEmpty(strCargo))
                {
                    Licencefor = "BOTH";
                }
                else if (!String.IsNullOrEmpty(strBirth))
                {
                    Licencefor = strBirth;
                }
                else if (String.IsNullOrEmpty(strCargo))
                {
                    Licencefor = strCargo;
                }

                entity.LicensedFor = Licencefor;
                entity.CreatedDate = entity.CreatedDate;
                entity.CreatedBy = entity.CreatedBy;
                entity.ModifiedDate = entity.ModifiedDate;
                entity.ModifiedBy = entity.ModifiedBy;

                _unitOfWork.Repository<TerminalOperator>().Update(entity);
                _unitOfWork.SaveChanges();

                // var qry = 
                _unitOfWork.ExecuteSqlCommand(" delete dbo.TerminalOperatorPort where TerminalOperatorID = @p0",
                    entityVo.TerminalOperatorID);

                if (terminalPorts.Count > 0)
                {
                    foreach (var applport in terminalPorts)
                    {
                        applport.TerminalOperatorID = entity.TerminalOperatorID;
                        applport.CreatedBy = entity.CreatedBy;
                        applport.CreatedDate = entity.CreatedDate;
                        applport.ModifiedBy = entity.ModifiedBy;
                        applport.ModifiedDate = entity.ModifiedDate;
                        applport.RecordStatus = entity.RecordStatus;
                        applport.ObjectState = ObjectState.Added;
                    }
                    _unitOfWork.Repository<TerminalOperatorPort>().InsertRange(terminalPorts);
                    _unitOfWork.SaveChanges();
                }

                //  if (strBirth != "" && strCargo != "")

                if (!String.IsNullOrEmpty(strBirth) && !String.IsNullOrEmpty(strCargo))

                {
                    Licencefor = "BOTH";
                    // var brt =
                    _unitOfWork.ExecuteSqlCommand(" delete dbo.TerminalOperatorBerth where TerminalOperatorID = @p0",
                        entityVo.TerminalOperatorID);

                    if (applberths.Count > 0)
                    {
                        foreach (var applberth in applberths)
                        {
                            applberth.TerminalOperatorID = entity.TerminalOperatorID;
                            applberth.CreatedBy = entity.CreatedBy;
                            applberth.CreatedDate = entity.CreatedDate;
                            applberth.ModifiedBy = entity.ModifiedBy;
                            applberth.ModifiedDate = entity.ModifiedDate;
                            applberth.RecordStatus = entity.RecordStatus;
                            applberth.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<TerminalOperatorBerth>().InsertRange(applberths);
                        _unitOfWork.SaveChanges();
                    }

                    // var ports = 
                    _unitOfWork.ExecuteSqlCommand(
                        " delete dbo.TerminalOperatorCargoHandling where TerminalOperatorID = @p0",
                        entityVo.TerminalOperatorID);

                    if (applcargos.Count > 0)
                    {
                        foreach (var applcargo in applcargos)
                        {
                            applcargo.TerminalOperatorID = entity.TerminalOperatorID;
                            applcargo.CreatedBy = entity.CreatedBy;
                            applcargo.ModifiedBy = entity.ModifiedBy;
                            applcargo.CreatedDate = entity.CreatedDate;
                            applcargo.ModifiedDate = entity.ModifiedDate;
                            applcargo.ObjectState = ObjectState.Added;
                            applcargo.RecordStatus = entity.RecordStatus;
                        }
                        _unitOfWork.Repository<TerminalOperatorCargoHandling>().InsertRange(applcargos);
                        _unitOfWork.SaveChanges();
                    }
                }
                else if (!String.IsNullOrEmpty(strBirth))
                {
                    Licencefor = strBirth;
                    var brt =
                        _unitOfWork.ExecuteSqlCommand(
                            " delete dbo.TerminalOperatorBerth where TerminalOperatorID = @p0",
                            entityVo.TerminalOperatorID);

                    if (applberths.Count > 0)
                    {
                        foreach (var applberth in applberths)
                        {
                            applberth.TerminalOperatorID = entity.TerminalOperatorID;
                            applberth.CreatedBy = entity.CreatedBy;
                            applberth.CreatedDate = entity.CreatedDate;
                            applberth.ModifiedBy = entity.ModifiedBy;
                            applberth.ModifiedDate = entity.ModifiedDate;
                            applberth.RecordStatus = entity.RecordStatus;
                            applberth.ObjectState = ObjectState.Added;
                        }
                        _unitOfWork.Repository<TerminalOperatorBerth>().InsertRange(applberths);
                        _unitOfWork.SaveChanges();
                    }
                }
                else if (String.IsNullOrEmpty(strCargo))
                {
                    Licencefor = strCargo;
                    var ports =
                        _unitOfWork.ExecuteSqlCommand(
                            " delete dbo.TerminalOperatorCargoHandling where TerminalOperatorID = @p0",
                            entityVo.TerminalOperatorID);

                    if (applcargos.Count > 0)
                    {
                        foreach (var applcargo in applcargos)
                        {
                            applcargo.TerminalOperatorID = entity.TerminalOperatorID;
                            applcargo.CreatedBy = entity.CreatedBy;
                            applcargo.ModifiedBy = entity.ModifiedBy;
                            applcargo.CreatedDate = entity.CreatedDate;
                            applcargo.ModifiedDate = entity.ModifiedDate;
                            applcargo.ObjectState = ObjectState.Added;
                            applcargo.RecordStatus = entity.RecordStatus;
                        }
                        _unitOfWork.Repository<TerminalOperatorCargoHandling>().InsertRange(applcargos);
                        _unitOfWork.SaveChanges();
                    }
                }

                entityVo = entity.MapToDto();
            }
            return entityVo;
        }

        private string GetValueStrBirth(TerminalOperatorVO entityVO, out string strCargo, out string Licencefor)
        {
            string strBirth = "";
            strCargo = "";
            Licencefor = "";

            foreach (string SrvType in entityVO.TerminalOperatorServiceTypesArray)
            {
                if (SrvType == "TOB")
                {
                    strBirth = "TOB";
                    List<TerminalOperatorCargoHandling> existcargos =
                        _unitOfWork.Repository<TerminalOperatorCargoHandling>().Query()
                            .Select()
                            .Where(e => e.TerminalOperatorID == entityVO.TerminalOperatorID)
                            .ToList<TerminalOperatorCargoHandling>();

                    if (existcargos.Count > 0)
                    {
                        //var brt =
                        _unitOfWork.ExecuteSqlCommand(
                            " delete dbo.TerminalOperatorCargoHandling where TerminalOperatorID = @p0",
                            entityVO.TerminalOperatorID);
                    }
                }

                if (SrvType == "TOCH")
                {
                    List<TerminalOperatorBerth> existberths = _unitOfWork.Repository<TerminalOperatorBerth>().Query()
                        .Select()
                        .Where(e => e.TerminalOperatorID == entityVO.TerminalOperatorID)
                        .ToList<TerminalOperatorBerth>();
                    if (existberths.Count > 0)
                    {
                        var brt =
                            _unitOfWork.ExecuteSqlCommand(" delete dbo.TerminalOperatorBerth where TerminalOperatorID = @p0",
                                entityVO.TerminalOperatorID);
                    }
                    strCargo = "TOCH";
                }
            }
            return strBirth;
        }

        #endregion

        #region GetCargoTypes
        /// <summary>
        /// To get CargoTypes
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryVO> GetCargoTypes()
        {
            List<SubCategory> subcategories = _subCategoryRepository.CargoTypes();
            return subcategories.MapToDto();
        }
        #endregion


        public List<TerminalOperatorVO> GetTerminalOperators(string portCode)
        {

            var terminalOperators = (from e in _unitOfWork.Repository<TerminalOperator>().Query().Select()
                             join ap in _unitOfWork.Repository<TerminalOperatorPort>().Query().Select()
                             on e.TerminalOperatorID equals ap.TerminalOperatorID
                                     where e.RecordStatus == RecordStatus.Active && ap.PortCode == portCode
                             orderby e.RegisteredName ascending
                                     select new TerminalOperatorVO
                                     {
                                         TerminalOperatorID = e.TerminalOperatorID,
                                         RegisteredName = e.RegisteredName
                                     }).ToList();           

            return terminalOperators;
        }
    }
}
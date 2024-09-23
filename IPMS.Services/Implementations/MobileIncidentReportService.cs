using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MobileIncidentReportService : ServiceBase, IMobileIncidentReportService
    {
        private IIncidentReportRepository _IncidentReportRepository;

        public MobileIncidentReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserId = GetUserIdByLoginname(_LoginName);
            _IncidentReportRepository = new IncidentReportRepository(_unitOfWork);
        }

        public MobileIncidentReportService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _UserId = GetUserIdByLoginname(_LoginName);
            _IncidentReportRepository = new IncidentReportRepository(_unitOfWork);
        }

        /// <summary>
        /// To get the all incident types
        /// </summary>
        /// <returns>SubCategoryVO--> List of Incident Types</returns>
        public List<SubCategoryVO> GetIncidentTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                SubCategoryRepository repository = new SubCategoryRepository(_unitOfWork);
                List<SubCategory> incidents = repository.IncidentTypes();
                return incidents.MapToDto();
            });
        }

        /// <summary>
        /// To add an incident.
        /// This function will add the incident data to the IncidentDocument,IncidentNature and Incident tables
        /// </summary>
        /// <param name="incidentData">Incident object</param>
        /// <returns>It will return IncidentVO</returns>
        public IncidentVO AddIncidentReport(IncidentVO incidentData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                incidentData.CreatedBy = _UserId;
                incidentData.ModifiedBy = _UserId;
                Incident incident = null;

                incident = incidentData.MapToEntity();

                incident.ObjectState = ObjectState.Added;

                List<IncidentDocument> incidentDocuments = incident.IncidentDocuments.ToList();
                List<IncidentNature> incidentNatures = incident.IncidentNatures.ToList();

                incident.IncidentDocuments = null;
                incident.IncidentNatures = null;
                incident.PortCode = _PortCode;
                _unitOfWork.Repository<Incident>().Insert(incident);
                _unitOfWork.SaveChanges();

                if (incidentDocuments.Count > 0)
                {
                    foreach (var item in incidentDocuments)
                    {
                        item.IncidentID = incident.IncidentID;
                        item.CreatedBy = incident.CreatedBy;
                        item.CreatedDate = incident.CreatedDate;
                        item.ModifiedBy = incident.ModifiedBy;
                        item.ModifiedDate = incident.ModifiedDate;
                        item.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<IncidentDocument>().InsertRange(incidentDocuments);
                    _unitOfWork.SaveChanges();
                }

                if (incidentNatures.Count > 0)
                {

                    foreach (var item in incidentNatures)
                    {
                        item.IncidentID = incident.IncidentID;
                        item.CreatedBy = incident.CreatedBy;
                        item.ModifiedBy = incident.ModifiedBy;
                        item.CreatedDate = incident.CreatedDate;
                        item.ModifiedDate = incident.ModifiedDate;
                        item.RecordStatus = "A";
                    }
                    _unitOfWork.Repository<IncidentNature>().InsertRange(incidentNatures);
                    _unitOfWork.SaveChanges();
                }

                incidentData = incident.MapToDto();
                return incidentData;
            });
        }

        public List<IncidentVO> GetIncidentReportList()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _IncidentReportRepository.GetIncidentReportList(_PortCode);
            });
        }
        public IncidentVO ModifyIncidentData(IncidentVO incidentData)
        {
            return EncloseTransactionAndHandleException(() =>
            {
                incidentData.ModifiedBy = _UserId;
                incidentData.ModifiedDate = DateTime.Now;

                Incident incident = null;
               // IncidentVO test = null;
                incident = incidentData.MapToEntity();
                incident.ObjectState = ObjectState.Modified;


                List<IncidentDocument> incidentDocuments = _unitOfWork.Repository<IncidentDocument>().Queryable().Where(l => l.IncidentID == incident.IncidentID).ToList();
                if (incidentDocuments.Count > 0)
                {
                    foreach (IncidentDocument incidentDocument in incidentDocuments)
                    {
                        _unitOfWork.Repository<IncidentDocument>().Delete(incidentDocument);
                    }
                }

                _unitOfWork.SaveChanges();

                List<IncidentDocument> incidentDocumentDetails = incident.IncidentDocuments.ToList();

                if (incidentDocumentDetails.Count > 0)
                {
                    foreach (var item in incidentDocumentDetails)
                    {
                        item.IncidentID = incident.IncidentID;
                        item.CreatedBy = incident.CreatedBy;
                        item.CreatedDate = incident.CreatedDate;
                        item.ModifiedBy = incident.ModifiedBy;
                        item.ModifiedDate = incident.ModifiedDate;
                        item.RecordStatus = "A";

                    }
                    _unitOfWork.Repository<IncidentDocument>().InsertRange(incidentDocumentDetails);
                    _unitOfWork.SaveChanges();
                }

                List<IncidentNature> incidentNatures = _unitOfWork.Repository<IncidentNature>().Queryable().Where(l => l.IncidentID == incident.IncidentID).ToList();
                if (incidentNatures.Count > 0)
                {
                    foreach (IncidentNature incidentNature in incidentNatures)
                    {
                        _unitOfWork.Repository<IncidentNature>().Delete(incidentNature);
                    }
                }
                _unitOfWork.SaveChanges();
                List<IncidentNature> incidentNatureDetails = incident.IncidentNatures.ToList();

                if (incidentNatureDetails.Count > 0)
                {

                    foreach (var item in incidentNatureDetails)
                    {
                        item.IncidentID = incident.IncidentID;
                        item.CreatedBy = incident.CreatedBy;
                        item.ModifiedBy = incident.ModifiedBy;
                        item.CreatedDate = incident.CreatedDate;
                        item.ModifiedDate = incident.ModifiedDate;
                        item.RecordStatus = "A";
                    }
                    _unitOfWork.Repository<IncidentNature>().InsertRange(incidentNatureDetails);
                    _unitOfWork.SaveChanges();
                }

                incident.IncidentDocuments = null;
                incident.IncidentNatures = null;
                _unitOfWork.Repository<Incident>().Update(incident);
                _unitOfWork.SaveChanges();
                return incident.MapToDto();
            });
        }
    }
}

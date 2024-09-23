using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.DTOS;
using IPMS.Domain;
using IPMS.Repository;
using System.Configuration;
using System.Data.SqlClient;

namespace IPMS.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class FileService : ServiceBase, IPMS.Services.IFileService
    {
        private IPortGeneralConfigsRepository _portGeneralConfigurationRepository;

        public FileService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
        }
        public FileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _portGeneralConfigurationRepository = new PortGeneralConfigsRepository(_unitOfWork);
            _UserId = GetUserIdByLoginname(_LoginName);
            _UserType = GetUserType(_LoginName);
        }

        public DocumentVO Upload(byte[] data, string filename, string fileType, string documentName, string documentType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var anonymousUserId = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);

                Document document = new Document();
                document.ObjectState = ObjectState.Added;
                document.RecordStatus = "A";
                document.DocumentName = documentType;
                document.DocumentPath = filename;
                document.DocumentType = documentType;
                document.Data = data;
                document.FileName = filename;
                document.FileType = fileType;                
                document.CreatedBy = (_UserId == 0 ? anonymousUserId : _UserId);
                document.CreatedDate = DateTime.Now;                
                document.ModifiedBy = (_UserId == 0 ? anonymousUserId : _UserId);
                document.ModifiedDate = DateTime.Now;

                _unitOfWork.Repository<Document>().Insert(document);
                _unitOfWork.SaveChanges();

                return document.MapToDTO();
            });

        }

        public DocumentVO Download(int documentId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var document = (from d in _unitOfWork.Repository<Document>().Queryable().Where(d => d.DocumentID == documentId)                                
                                select d).FirstOrDefault<Document>();
                return document.MapToDTO();
            });
        }

        public string GetFileSizeConfigValue()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                return _portGeneralConfigurationRepository.GetPortConfiguration(_PortCode, ConfigName.FileUploadMaxSize);
            });
        }


        //anusha 2024/03/20 start

        public DocumentVO DownloadExternal(int documentId, string EntityCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Document document = new Document();
                if (documentId == 0)
                {

                    throw new ArgumentException("Invalid documentId. DocumentId cannot be 0.");
                }

                if (EntityCode == IPMS.Domain.EntityCodes.PortInformation)
                {
                    if (documentId != 0 && _UserId > 1)
                    {
                        document = (from d in _unitOfWork.Repository<Document>().Queryable()
                                    join p in _unitOfWork.Repository<PortContent>().Queryable()
                                        on d.DocumentID equals p.DocumentID
                                    where d.DocumentID == documentId && (p.LinkVisibility == "U" || p.LinkVisibility == "G")
                                    select d).FirstOrDefault();
                    }
                    else if (documentId != 0 && _UserId <= 1)
                    {

                        document = (from d in _unitOfWork.Repository<Document>().Queryable()
                                    join p in _unitOfWork.Repository<PortContent>().Queryable()
                                        on d.DocumentID equals p.DocumentID
                                    where p.DocumentID == documentId && (p.LinkVisibility == "U" || p.LinkVisibility == "G")
                                    select d).FirstOrDefault();

                    }
                }

                //if (EntityCode == "LICREGLIST")
                //{
                //    if (documentId != 0 && _UserId > 1)
                //    {
                //        document = (from d in _unitOfWork.Repository<Document>().Queryable()
                //                    join p in _unitOfWork.Repository<LicenseRequestDocument>().Queryable()
                //                        on d.DocumentID equals p.DocumentID
                //                    where d.DocumentID == documentId
                //                    select d).FirstOrDefault();
                //    }
                //    else if (documentId != 0 && _UserId <= 1)
                //    {
                //        document = (from d in _unitOfWork.Repository<Document>().Queryable()
                //                   .Where(d => d.DocumentID == documentId && d.DocumentName == "LDC4")
                //                    select d).FirstOrDefault<Document>();
                //    }

                //}

                return document.MapToDTO();

            });
        }


        //anusha 04/04/2024

        public DocumentVO Downloadinternal(int documentId, string entityCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                Document document = new Document();
                if (documentId != 0 && _UserId >= 1 && _UserType == UserType.Agent)
                {
                    document = (from d in _unitOfWork.Repository<Document>().Queryable()
                             .Where(d => d.DocumentID == documentId)
                                select d).FirstOrDefault<Document>();
                }

                else if (documentId != 0 && _UserId >= 1 && _UserType == UserType.Employee)
                {
                    var _documentId = new SqlParameter("@documentId", documentId);
                    var _entityCode = new SqlParameter("@EntityCode", entityCode);
                    var _userid = new SqlParameter("@UserId", _UserId);

                    document = _unitOfWork.SqlQuery<Document>("dbo.usp_FileDownloades_All  @documentId, @EntityCode, @UserId", _documentId, _entityCode, _userid).FirstOrDefault();
                }
                return document.MapToDTO();
            });
        }













        public void Dispose()
        {
        }
    }
}

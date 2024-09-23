using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IPMS.ServiceProxies.Contracts
{
    [ServiceContract]
    public interface IFileService : IDisposable
    {

        [OperationContract]
        DocumentVO Upload(byte[] data, string filename, string fileType, string documentName, string documentType);

        //[OperationContract]
        //DocumentVO UploadAsync(byte[] data, string filename, string documentType);

        [OperationContract]
        DocumentVO Download(int documentId);

        //[OperationContract]
        //DocumentVO DownloadAsync(int documentId);

        [OperationContract]
        string GetFileSizeConfigValue();

        //[OperationContract]
        //string GetFileSizeConfigValueAsync();



        //anusha 2024/03/20 start
        [OperationContract]
        DocumentVO DownloadExternal(int documentId, string EntityCode);


        [OperationContract]
        DocumentVO Downloadinternal(int documentId, string EntityCode);








    }
}
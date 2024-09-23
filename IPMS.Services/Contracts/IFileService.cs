using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;


namespace IPMS.Services
{
    [ServiceContract]
    public interface IFileService : IDisposable
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DocumentVO Upload(byte[] data, string filename, string fileType, string documentName, string documentType);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        DocumentVO Download(int documentId);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetFileSizeConfigValue();


        //anusha 2024/03/20 start
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DocumentVO DownloadExternal(int documentId, string EntityCode);


        //anusha 2024/04/04 start
        [OperationContract]
        [FaultContract(typeof(Exception))]
        DocumentVO Downloadinternal(int documentId, string EntityCode);




    }
}

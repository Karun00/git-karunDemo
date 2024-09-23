using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using IPMS.ServiceProxies.Contracts;
using IPMS.Domain.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using IPMS.Web.ServiceProxies;
using IPMS.Domain.ValueObjects;

namespace IPMS.ServiceProxies.Clients
{
    public class FileClient : UserClientBase<IFileService>, IFileService
    {
        public DocumentVO Upload(byte[] data, string filename, string fileType, string documentName, string documentType)
        {
            return WrapOperationWithException(() => Channel.Upload(data, filename, fileType, documentName, documentType));
        }

        public DocumentVO Download(int documentId)
        {
            return WrapOperationWithException(() => Channel.Download(documentId));
        }

        //public DocumentVO UploadAsync(byte[] data, string filename, string documentType)
        //{
        //    return WrapOperationWithException(() => Channel.UploadAsync(data, filename, documentType));
        //}

        //public DocumentVO DownloadAsync(int documentId)
        //{
        //    return WrapOperationWithException(() => Channel.DownloadAsync(documentId));
        //}

        public string GetFileSizeConfigValue()
        {
            return WrapOperationWithException(() => Channel.GetFileSizeConfigValue());
        }

        //public string GetFileSizeConfigValueAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetFileSizeConfigValueAsync());
        //}


        //anusha 2024/03/20 start
        public DocumentVO DownloadExternal(int documentId, string EntityCode)
        {
            return WrapOperationWithException(() => Channel.DownloadExternal(documentId, EntityCode));
        }


        public DocumentVO Downloadinternal(int documentId, string EntityCode)
        {
            return WrapOperationWithException(() => Channel.Downloadinternal(documentId, EntityCode));
        }





    }
}
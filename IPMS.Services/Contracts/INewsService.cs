using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace IPMS.Services
{
    [ServiceContract]
    public interface INewsService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<NewsVO> GetNewsList();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        NewsVO AddNews(NewsVO newsData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        NewsVO ModifyNews(NewsVO newsData);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        List<NewsVO> GetNewsForScroll();
    }
}

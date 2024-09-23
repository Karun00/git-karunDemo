using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Contracts
{

    [ServiceContract]
    public interface INewsService : IDisposable
    {
        [OperationContract]
        List<NewsVO> GetNewsList();
        [OperationContract]
        NewsVO AddNews(NewsVO newsData);
        [OperationContract]
        NewsVO ModifyNews(NewsVO newsData);

        //[OperationContract]
        //Task<List<NewsVO>> GetNewsListAsync();
        //[OperationContract]
        //Task<NewsVO> AddNewsAsync(NewsVO newsdata);
        //[OperationContract]
        //Task<NewsVO> ModifyNewsAsync(NewsVO newsdata);


        [OperationContract]
        List<NewsVO> GetNewsForScroll();
        [OperationContract]
        Task<List<NewsVO>> GetNewsForScrollAsync();

    }
}

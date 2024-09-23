using IPMS.Domain.ValueObjects;
using IPMS.ServiceProxies.Contracts;
using IPMS.Web.ServiceProxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPMS.ServiceProxies.Clients
{
    public class NewsClient : UserClientBase<INewsService>, INewsService
    {
        public List<NewsVO> GetNewsList()
        {
            return WrapOperationWithException(() => Channel.GetNewsList());
        }

        public NewsVO AddNews(NewsVO newsData)
        {
            return WrapOperationWithException(() => Channel.AddNews(newsData));
        }

        public NewsVO ModifyNews(NewsVO newsData)
        {
            return WrapOperationWithException(() => Channel.ModifyNews(newsData));
        }

        //public Task<List<NewsVO>> GetNewsListAsync()
        //{
        //    return WrapOperationWithException(() => Channel.GetNewsListAsync());
        //}

        //public Task<NewsVO> AddNewsAsync(NewsVO newsdata)
        //{
        //    return WrapOperationWithException(() => Channel.AddNewsAsync(newsdata));
        //}

        //public Task<NewsVO> ModifyNewsAsync(NewsVO newsdata)
        //{
        //    return WrapOperationWithException(() => Channel.ModifyNewsAsync(newsdata));
        //}
       
        public List<NewsVO> GetNewsForScroll()
        {
            return WrapOperationWithException(() => Channel.GetNewsForScroll());
        }
        public Task<List<NewsVO>> GetNewsForScrollAsync()
        {
            return WrapOperationWithException(() => Channel.GetNewsForScrollAsync());
        }
    }
}
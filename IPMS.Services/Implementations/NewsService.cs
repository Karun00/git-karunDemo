using Core.Repository;
using IPMS.Data.Context;
using IPMS.Domain;
using IPMS.Domain.DTOS;
using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using IPMS.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace IPMS.Services
{
    public class NewsService : ServiceBase, INewsService
    {
        private IAccountRepository _accountRepository;
        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = new AccountRepository(_unitOfWork);
        }
        public NewsService()
        {
            _unitOfWork = new UnitOfWork(new TnpaContext());
            _accountRepository = new AccountRepository(_unitOfWork);
        }

        /// <summary>
        /// To Get News Data
        /// </summary>
        /// <returns></returns>
        public List<NewsVO> GetNewsList()
        {
            string PortCodeByLogin = string.Empty;
            return ExecuteFaultHandledOperation(() =>
            {

                //var newsdata = _unitOfWork.Repository<News>().Queryable()
                //                                .OrderByDescending(x => x.NewsID).ToList<News>();
                var newsdata = _unitOfWork.Repository<News>().Query().Include(t=>t.NewsPorts).Select()
                                                .OrderByDescending(x => x.NewsID).ToList<News>();

                return newsdata.MapToDto();

            });
            //DateTime currentDatetime = DateTime.Now;
            //string PortCodeByLogin = string.Empty;


            //return ExecuteFaultHandledOperation(() =>
            //{
            //    if (_PortCode != "")
            //    {
            //        PortCodeByLogin = _PortCode;
            //    }
            //    else
            //    {
            //        int userid = _accountRepository.GetUserId(_LoginName);
            //        PortCodeByLogin = (from e in _unitOfWork.Repository<UserPort>().Queryable()
            //                           where userid == e.UserID && e.RecordStatus == RecordStatus.Active
            //                           select e.PortCode).FirstOrDefault();
            //    }
            //    var NewsPortdata = (from a in _unitOfWork.Repository<News>().Queryable()
            //                        join np in _unitOfWork.Repository<NewsPort>().Queryable() on a.NewsID equals np.NewsID
            //                        where (np.RecordStatus == RecordStatus.Active && np.PortCode == PortCodeByLogin && a.RecordStatus == RecordStatus.Active)
            //                        select new NewsVO
            //                        {
            //                            NewsContent = a.NewsContent,
            //                            Title=a.Title,
            //                            //StartDate=a.StartDate,
            //                            //EndDate=a.EndDate,
            //                            RecordStatus=a.RecordStatus

            //                        }).ToList();



            //    return NewsPortdata;

            //});
        }

        /// <summary>
        /// To Add News Details
        /// </summary>
        /// <param name="newsData"></param>
        /// <returns></returns>
        /// 


        public NewsVO AddNews(NewsVO newsData)
        {
            //NewsPortVO newPort=new NewsPortVO();
            return ExecuteFaultHandledOperation(() =>
            {
                int userid;
                //TODO:need to check when _LoginName is empty
                if (_LoginName != "")
                    userid = _accountRepository.GetUserId(_LoginName);
                else
                    userid = Convert.ToInt32(ConfigurationManager.AppSettings["AnonymousUserId"]);
               
                News news = null;
              
                news = newsData.MapToEntity();
                List<NewsPort> _NewsPort = news.NewsPorts.ToList();
                news.NewsPorts = null;

                
                news.ObjectState = ObjectState.Added;
                news.CreatedBy = userid;
                news.CreatedDate = DateTime.Now;
                news.ModifiedBy = userid;
                news.ModifiedDate = DateTime.Now;
                news.RecordStatus = RecordStatus.Active;                         
                _unitOfWork.Repository<News>().Insert(news);
                _unitOfWork.SaveChanges();
               
                foreach (var NewsPortdata in _NewsPort)
                {
                    NewsPortdata.NewsID = news.NewsID;
                    NewsPortdata.RecordStatus = RecordStatus.Active;
                    NewsPortdata.CreatedBy = news.CreatedBy;
                    NewsPortdata.CreatedDate = DateTime.Now;
                    NewsPortdata.ModifiedBy = news.ModifiedBy;
                    NewsPortdata.ModifiedDate = DateTime.Now;
                    //_unitOfWork.Repository<NewsPort>().Insert(NewsPortdata);
                }
                _unitOfWork.Repository<NewsPort>().InsertRange(_NewsPort);
                _unitOfWork.SaveChanges();
                newsData = news.MapToDto();
                return newsData;
            });
        }

        /// <summary>
        /// To Modify News Details
        /// </summary>
        /// <param name="newsData"></param>
        /// <returns></returns>
        public NewsVO ModifyNews(NewsVO newsData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                
                int userid = _accountRepository.GetUserId(_LoginName);
                News news = null;
                news = newsData.MapToEntity();
                List<NewsPort> _NewsPort = news.NewsPorts.ToList();
                List<NewsPort> lstNewsPorts = _unitOfWork.Repository<NewsPort>().Queryable().Where(e => e.NewsID == news.NewsID).ToList();
                foreach (NewsPort NPort in lstNewsPorts)
                {
                    _unitOfWork.Repository<NewsPort>().Delete(NPort);
                    _unitOfWork.SaveChanges();
                }
                _NewsPort.ToList().ForEach(newsPort =>
                {
                    newsPort.NewsID = news.NewsID;
                    newsPort.RecordStatus = news.RecordStatus;
                    newsPort.CreatedBy = userid;
                    newsPort.CreatedDate = DateTime.Now;
                    newsPort.ModifiedBy = userid;
                    newsPort.ModifiedDate = DateTime.Now;
                    _unitOfWork.Repository<NewsPort>().Insert(newsPort);
                    _unitOfWork.SaveChanges();
                });
                news.ObjectState = ObjectState.Modified;              
                news.ModifiedBy = userid;
                news.ModifiedDate = DateTime.Now;
                news.RecordStatus = news.RecordStatus;               
                _unitOfWork.Repository<News>().Update(news);
                _unitOfWork.SaveChanges();               
                newsData = news.MapToDto();
                return newsData;
            });
        }


        /// <summary>
        /// To Get News Data between specific dates for Scrolling news : By Mahesh
        /// </summary>
        /// <returns></returns>
        public List<NewsVO> GetNewsForScroll()        
        {
            DateTime currentDatetime = DateTime.Now;
            string PortCodeByLogin=string.Empty;
           
            
            return ExecuteFaultHandledOperation(() =>
            {
                if (_PortCode != "")
                {
                    PortCodeByLogin = _PortCode;
                }
                else
                {
                    int userid = _accountRepository.GetUserId(_LoginName);
                    PortCodeByLogin = (from e in _unitOfWork.Repository<UserPort>().Queryable()
                                       where userid == e.UserID && e.RecordStatus == RecordStatus.Active
                                       select e.PortCode).FirstOrDefault();
                }              
                var NewsPortdata = (from a in _unitOfWork.Repository<News>().Queryable()
                                join np in _unitOfWork.Repository<NewsPort>().Queryable() on a.NewsID equals np.NewsID
                                    where (np.RecordStatus == RecordStatus.Active && np.PortCode == PortCodeByLogin && currentDatetime >= a.StartDate && currentDatetime <= a.EndDate && a.RecordStatus == RecordStatus.Active) 
                                select new NewsVO
                                {
                                    NewsContent = a.NewsContent

                                }).ToList();
                

                                
                return NewsPortdata;
                
            });
        }
    }
}

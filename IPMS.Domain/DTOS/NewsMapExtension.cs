using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Domain.ValueObjects;
using IPMS.Domain.Models;
using System.Globalization;


namespace IPMS.Domain.DTOS
{
    public static class NewsMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="newsList"></param>
        /// <returns></returns>
        public static List<NewsVO> MapToDto(this List<News> newsList)
        {
            List<NewsVO> newsvoList = new List<NewsVO>();
            if (newsList != null)
                foreach (var data in newsList)
                {
                    newsvoList.Add(data.MapToDto());

                }
            return newsvoList;
        }
        
        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static NewsVO MapToDto(this News data)
        {
            NewsVO newsvo = new NewsVO();
            if (data != null)
            {
                newsvo.NewsID = data.NewsID;
                newsvo.Title = data.Title;
                newsvo.NewsUrl = data.NewsUrl;
                newsvo.NewsContent = data.NewsContent;               
                newsvo.StartDate = Convert.ToDateTime(data.StartDate).ToString();               
                newsvo.EndDate = Convert.ToDateTime(data.EndDate).ToString();
                newsvo.RecordStatus = data.RecordStatus;
                newsvo.CreatedBy = data.CreatedBy;
                newsvo.CreatedDate = data.CreatedDate;
                newsvo.ModifiedBy = data.ModifiedBy;
                newsvo.ModifiedDate = data.ModifiedDate;
                newsvo.NewsPort = data.NewsPorts.ToList().MapToDto();              
            }
            return newsvo;
        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="newsVo"></param>
        /// <returns></returns>
        public static News MapToEntity(this NewsVO newsVo)
        {
            News news = new News();
            if (newsVo != null)
            {
                news.NewsID = newsVo.NewsID;
                news.Title = newsVo.Title;
                news.NewsUrl = newsVo.NewsUrl;
                news.NewsContent = newsVo.NewsContent;               
                news.StartDate = DateTime.Parse(Convert.ToDateTime(newsVo.StartDate, CultureInfo.InvariantCulture).ToString(), CultureInfo.InvariantCulture);              
                news.EndDate = DateTime.Parse(Convert.ToDateTime(newsVo.EndDate, CultureInfo.InvariantCulture).ToString(), CultureInfo.InvariantCulture);
                news.RecordStatus = newsVo.RecordStatus;
                news.CreatedBy = newsVo.CreatedBy;
                news.CreatedDate = newsVo.CreatedDate;
                news.ModifiedBy = newsVo.ModifiedBy;
                news.ModifiedDate = newsVo.ModifiedDate;
                news.NewsPorts = newsVo.NewsPort.ToList().MapToEntity();
                
            }
            return news;
        }
    }
}

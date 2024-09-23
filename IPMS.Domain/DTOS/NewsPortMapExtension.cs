using IPMS.Domain.Models;
using IPMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace IPMS.Domain.DTOS
{
   public static class NewsPortMapExtension
    {
        /// <summary>
        /// Data List Transfer from Entity to DTO
        /// </summary>
        /// <param name="newsList"></param>
        /// <returns></returns>
        /// 
      
       public static List<NewsPortVO> MapToDto(this List<NewsPort> newsPortList)
       {
           List<NewsPortVO> newsportvoList = new List<NewsPortVO>();
           if (newsPortList != null)
               foreach (var data in newsPortList)
               {
                   newsportvoList.Add(data.MapToDto());

               }
           return newsportvoList;
       }


       public static List<NewsPort> MapToEntity(this List<NewsPortVO> newsPortList)
       {
           List<NewsPort> newsportvoList = new List<NewsPort>();
           if (newsPortList != null)
               foreach (var data in newsPortList)
               {
                   newsportvoList.Add(data.MapToEntity());

               }
           return newsportvoList;
       }


        /// <summary>
        /// Data Transfer from Entity to DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static NewsPortVO MapToDto(this NewsPort data)
        {
            NewsPortVO newsportvo = new NewsPortVO();
            if (data != null)
            {
                newsportvo.NewsID = data.NewsID;
                newsportvo.NewsPortID = data.NewsPortID;
                newsportvo.RecordStatus = data.RecordStatus;
                newsportvo.CreatedBy = data.CreatedBy;
                newsportvo.CreatedDate = data.CreatedDate;
                newsportvo.ModifiedBy = data.ModifiedBy;
                newsportvo.ModifiedDate = data.ModifiedDate;
                newsportvo.PortCode = data.PortCode;
            }
            return newsportvo;
        }

        /// <summary>
        /// Data Transfer from DTO to Entity
        /// </summary>
        /// <param name="newsVo"></param>
        /// <returns></returns>
        public static NewsPort MapToEntity(this NewsPortVO newsPortVo)
        {
            NewsPort newsport = new NewsPort();
            if (newsPortVo != null)
            {
                newsport.NewsID = newsPortVo.NewsID;
                newsport.NewsPortID = newsPortVo.NewsPortID;
                newsport.RecordStatus = newsPortVo.RecordStatus;
                newsport.CreatedBy = newsPortVo.CreatedBy;
                newsport.CreatedDate = newsPortVo.CreatedDate;
                newsport.ModifiedBy = newsPortVo.ModifiedBy;
                newsport.ModifiedDate = newsPortVo.ModifiedDate;
                newsport.PortCode = newsPortVo.PortCode;
            }
            return newsport;
        }
    }
}

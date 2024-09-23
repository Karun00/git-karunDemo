using IPMS.Domain.Models;
using System;
using System.Collections.Generic;

namespace IPMS.Domain.ValueObjects
{
    /// <summary>
    /// Data Transfer Object for News
    /// </summary>
    public class NewsVO
    {
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string NewsContent { get; set; }
        public string NewsUrl { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }       
        public List<NewsPortVO> NewsPort { get; set; }
       // public virtual List<NewsPort> NewPortData { get; set; }
        
    }

}

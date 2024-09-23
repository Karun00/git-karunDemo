using Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace IPMS.Domain.Models  
{
    public partial class Hour24Report625:EntityBase
    {
        public Hour24Report625()
        {
            this.Section625ABCD = new List<Section625ABCD>();
            this.Section625B = new List<Section625B>();
            this.Section625BUnion = new List<Section625BUnion>();
            this.Section625C = new List<Section625C>();
            this.Section625CDetail = new List<Section625CDetail>();
            this.Section625CPrevent = new List<Section625CPrevent>();
            this.Section625CRecommended = new List<Section625CRecommended>();
            this.Section625D = new List<Section625D>();
            this.Section625DDetail = new List<Section625DDetail>();
            this.Section625E = new List<Section625E>();
            this.Section625EDetail = new List<Section625EDetail>();
            this.Section625G = new List<Section625G>();
            this.Section625GDetail1 = new List<Section625GDetail1>();
            this.Section625GDetail2 = new List<Section625GDetail2>();
        }

        public int Hour24Report625ID { get; set; }
        public string OperatorName { get; set; }
        public string LincseNumber { get; set; }
        public string CDName { get; set; }
        public string CDDesignation { get; set; }
        public string CDContactNumber { get; set; }
        public string CDMobileNumber { get; set; }
        public string CDEmailID { get; set; }
        public string CDAddress { get; set; }
        public string NONatureCode { get; set; }
        public Nullable<System.DateTime> IODOccuranceDateTime { get; set; }
        public string IODSpecificLocation { get; set; }
        public string IODOccuranceBriefDescription { get; set; }
        public string RecordStatus { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string PortCode { get; set; }
        public string Timeperiod { get; set; }
        [NotMapped]
        public string PortName { get; set; }
        [NotMapped]
        public string NONatureCodeType { get; set; }
        public  User User { get; set; }
        public  User User1 { get; set; }
        public  Port Port24 { get; set; }
        public  SubCategory SubCategory { get; set; }
        public  ICollection<Section625ABCD> Section625ABCD { get; set; }
        public  ICollection<Section625B> Section625B { get; set; }
        public  ICollection<Section625BUnion> Section625BUnion { get; set; }
        public  ICollection<Section625C> Section625C { get; set; }
        public  ICollection<Section625CDetail> Section625CDetail { get; set; }
        public  ICollection<Section625CPrevent> Section625CPrevent { get; set; }
        public  ICollection<Section625CRecommended> Section625CRecommended { get; set; }
        public  ICollection<Section625D> Section625D { get; set; }
        public  ICollection<Section625DDetail> Section625DDetail { get; set; }
        public  ICollection<Section625E> Section625E { get; set; }
        public  ICollection<Section625EDetail> Section625EDetail { get; set; }
        public  ICollection<Section625G> Section625G { get; set; }
        public  ICollection<Section625GDetail1> Section625GDetail1 { get; set; }
        public  ICollection<Section625GDetail2> Section625GDetail2 { get; set; }
    }
}

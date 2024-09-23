using System;
using System.Collections.Generic;
using Core.Repository;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Repository.Providers.EntityFramework;
using System.Data;

namespace IPMS.Domain.Models
{
    [DataContract]
    public partial class SAPInvoiceItem : EntityBase
    {
        [DataMember]
        public int InvoiceID { get; set; }
        [DataMember]
        public string MarineOrder { get; set; }
         [DataMember]
        public string BillingDocNo { get; set; }
         [DataMember]
        public string OrderNo { get; set; }
         [DataMember]
        public string ItemNo { get; set; }
         [DataMember]
        public string MaterialNo { get; set; }
         [DataMember]
        public string Service { get; set; }
         [DataMember]
        public string UOM { get; set; }
         [DataMember]
        public string Qunatity { get; set; }
         [DataMember]
        public string TarifF { get; set; }
         [DataMember]
        public string TarifF2 { get; set; }
         [DataMember]
        public string Amount { get; set; }
         [DataMember]
        public string VAT { get; set; }
         [DataMember]
        public string NetAmount { get; set; }
         [DataMember]
        public string SalesOrgNo { get; set; }
         [DataMember]
        public string AgentName { get; set; }
         [DataMember]
        public string Address { get; set; }
         [DataMember]
        public string TelephoneNo { get; set; }
         [DataMember]
        public string FaxNo { get; set; }
         [DataMember]
        public string Account { get; set; }
         [DataMember]
        public string VesselID { get; set; }
         [DataMember]
        public string VesselName { get; set; }
         [DataMember]
        public string VesselTonnage { get; set; }
         [DataMember]
        public string VesselCapacity { get; set; }
         [DataMember]
        public string VesselLength { get; set; }
         [DataMember]
        public string ArrivalID { get; set; }
         [DataMember]
        public string ArrivalDate { get; set; }
         [DataMember]
        public string Arrivaltime { get; set; }
         [DataMember]
        public string DepartureDate { get; set; }
         [DataMember]
        public string DepartureTime { get; set; }
         [DataMember]
        public string VoyageIn { get; set; }
         [DataMember]
         public string VoyageOut { get; set; }
         [DataMember]
        public Nullable<int> ESubscription { get; set; }
        [DataMember]
        public string NetValue { get; set; }


        [NotMapped]
        [StoredProcedure("usp_SAPInvoiceReponsetoSAPInvoiceItem_dml")]
        public class SAPInvoiceReponsetoSAPInvoiceItem_proc
        {
            private SAPInvoiceItem _objSApInvoiceItem;
            public SAPInvoiceReponsetoSAPInvoiceItem_proc(SAPInvoiceItem p_SApInvoiceItem)
            {
                _objSApInvoiceItem = p_SApInvoiceItem;
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string MarineOrder
            {
                get
                {
                    return _objSApInvoiceItem.MarineOrder;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string BillingDocNo
            {
                get
                {
                    return _objSApInvoiceItem.BillingDocNo;
                }
            }
            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string OrderNo
            {
                get
                {
                    return _objSApInvoiceItem.OrderNo;
                }
            }
            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string ItemNo
            {
                get
                {
                    return _objSApInvoiceItem.ItemNo;
                }
            }
            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string MaterialNo
            {
                get
                {
                    return _objSApInvoiceItem.MaterialNo;
                }
            }
                            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string Service
            {
                get
                {
                    return _objSApInvoiceItem.Service;
                }
            }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string UOM
            {
                get
                {
                    return _objSApInvoiceItem.UOM;
                }
            }
           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string Qunatity
            {
                get
                {
                    return _objSApInvoiceItem.Qunatity;
                }
           }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string TarifF
            {
                get
                {
                    return _objSApInvoiceItem.TarifF;
                }
            }
            
            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string TarifF2
            {
                get
                {
                    return _objSApInvoiceItem.TarifF2;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string Amount
            {
                get
                {
                    return _objSApInvoiceItem.Amount;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string VAT
            {
                get
                {
                    return _objSApInvoiceItem.VAT;
                }
            }

            [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string NetAmount
            {
                get
                {
                    return _objSApInvoiceItem.NetAmount;
                }
            }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
            public string SalesOrgNo
            {
                get
                {
                    return _objSApInvoiceItem.SalesOrgNo;
                }
            }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string AgentName
           {
               get
               {
                   return _objSApInvoiceItem.AgentName;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string Address
           {
               get
               {
                   return _objSApInvoiceItem.Address;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string TelephoneNo
           {
               get
               {
                   return _objSApInvoiceItem.TelephoneNo;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string FaxNo
           {
               get
               {
                   return _objSApInvoiceItem.FaxNo;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string Account
           {
               get
               {
                   return _objSApInvoiceItem.Account;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string VesselID
           {
               get
               {
                   return _objSApInvoiceItem.VesselID;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string VesselName
           {
               get
               {
                   return _objSApInvoiceItem.VesselName;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string VesselTonnage
           {
               get
               {
                   return _objSApInvoiceItem.VesselTonnage;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string VesselCapacity
           {
               get
               {
                   return _objSApInvoiceItem.VesselCapacity;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string VesselLength
           {
               get
               {
                   return _objSApInvoiceItem.VesselLength;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string ArrivalID
           {
               get
               {
                   return _objSApInvoiceItem.ArrivalID;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string ArrivalDate
           {
               get
               {
                   return _objSApInvoiceItem.ArrivalDate;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string Arrivaltime
           {
               get
               {
                   return _objSApInvoiceItem.Arrivaltime;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string DepartureDate
           {
               get
               {
                   return _objSApInvoiceItem.DepartureDate;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string DepartureTime
           {
               get
               {
                   return _objSApInvoiceItem.DepartureTime;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string VoyageIn
           {
               get
               {
                   return _objSApInvoiceItem.VoyageIn;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string VoyageOut
           {
               get
               {
                   return _objSApInvoiceItem.VoyageOut;
               }
           }

           [StoredProcedureParameter(SqlDbType.Int, Direction = ParameterDirection.Input)]
           public Nullable<int> ESubscription
           {
               get
               {
                   return _objSApInvoiceItem.ESubscription;
               }
           }

           [StoredProcedureParameter(SqlDbType.NVarChar, Direction = ParameterDirection.Input)]
           public string NetValue
           {
               get
               {
                   return _objSApInvoiceItem.NetValue;
               }
           }

        }

    }
}

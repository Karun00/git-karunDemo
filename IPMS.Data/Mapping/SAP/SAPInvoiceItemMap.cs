using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IPMS.Domain.Models;

namespace IPMS.Data.Mapping
{
    public class SAPInvoiceItemMap : EntityTypeConfiguration<SAPInvoiceItem>
    {
        public SAPInvoiceItemMap()
        {
            this.HasKey(t => t.InvoiceID);

            // Properties
            this.Property(t => t.MarineOrder)
                .HasMaxLength(10);

            this.Property(t => t.BillingDocNo)
                .HasMaxLength(50);

            this.Property(t => t.OrderNo)
                .HasMaxLength(10);

            this.Property(t => t.ItemNo)
                .HasMaxLength(6);

            this.Property(t => t.MaterialNo)
                .HasMaxLength(18);

            this.Property(t => t.Service)
                .HasMaxLength(40);

            this.Property(t => t.UOM)
                .HasMaxLength(4);

            this.Property(t => t.Qunatity)
                .HasMaxLength(10);

            this.Property(t => t.TarifF)
                .HasMaxLength(15);

            this.Property(t => t.TarifF2)
                .HasMaxLength(15);

            this.Property(t => t.Amount)
                .HasMaxLength(15);

            this.Property(t => t.VAT)
                .HasMaxLength(15);

            this.Property(t => t.NetAmount)
                .HasMaxLength(15);

            this.Property(t => t.SalesOrgNo)
                .HasMaxLength(10);

            this.Property(t => t.AgentName)
                .HasMaxLength(35);

            this.Property(t => t.Address)
                .HasMaxLength(50);

            this.Property(t => t.TelephoneNo)
                .HasMaxLength(10);

            this.Property(t => t.FaxNo)
                .HasMaxLength(10);

            this.Property(t => t.Account)
                .HasMaxLength(20);

            this.Property(t => t.VesselID)
                .HasMaxLength(10);

            this.Property(t => t.VesselName)
                .HasMaxLength(30);

            this.Property(t => t.VesselTonnage)
                .HasMaxLength(10);

            this.Property(t => t.VesselCapacity)
                .HasMaxLength(10);

            this.Property(t => t.VesselLength)
                .HasMaxLength(10);

            this.Property(t => t.ArrivalID)
                .HasMaxLength(20);

            this.Property(t => t.VoyageIn)
                .HasMaxLength(17);

            this.Property(t => t.VoyageOut)
                .HasMaxLength(17);

            this.Property(t => t.NetValue)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SAPInvoiceItems");
            this.Property(t => t.InvoiceID).HasColumnName("InvoiceID");
            this.Property(t => t.MarineOrder).HasColumnName("MarineOrder");
            this.Property(t => t.BillingDocNo).HasColumnName("BillingDocNo");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.ItemNo).HasColumnName("ItemNo");
            this.Property(t => t.MaterialNo).HasColumnName("MaterialNo");
            this.Property(t => t.Service).HasColumnName("Service");
            this.Property(t => t.UOM).HasColumnName("UOM");
            this.Property(t => t.Qunatity).HasColumnName("Qunatity");
            this.Property(t => t.TarifF).HasColumnName("TarifF");
            this.Property(t => t.TarifF2).HasColumnName("TarifF2");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.VAT).HasColumnName("VAT");
            this.Property(t => t.NetAmount).HasColumnName("NetAmount");
            this.Property(t => t.SalesOrgNo).HasColumnName("SalesOrgNo");
            this.Property(t => t.AgentName).HasColumnName("AgentName");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.TelephoneNo).HasColumnName("TelephoneNo");
            this.Property(t => t.FaxNo).HasColumnName("FaxNo");
            this.Property(t => t.Account).HasColumnName("Account");
            this.Property(t => t.VesselID).HasColumnName("VesselID");
            this.Property(t => t.VesselName).HasColumnName("VesselName");
            this.Property(t => t.VesselTonnage).HasColumnName("VesselTonnage");
            this.Property(t => t.VesselCapacity).HasColumnName("VesselCapacity");
            this.Property(t => t.VesselLength).HasColumnName("VesselLength");
            this.Property(t => t.ArrivalID).HasColumnName("ArrivalID");
            this.Property(t => t.ArrivalDate).HasColumnName("ArrivalDate");
            this.Property(t => t.Arrivaltime).HasColumnName("Arrivaltime");
            this.Property(t => t.DepartureDate).HasColumnName("DepartureDate");
            this.Property(t => t.DepartureTime).HasColumnName("DepartureTime");
            this.Property(t => t.VoyageIn).HasColumnName("VoyageIn");
            this.Property(t => t.VoyageOut).HasColumnName("VoyageOut");
            this.Property(t => t.ESubscription).HasColumnName("ESubscription");
            this.Property(t => t.NetValue).HasColumnName("NetValue");
        }
    }
}

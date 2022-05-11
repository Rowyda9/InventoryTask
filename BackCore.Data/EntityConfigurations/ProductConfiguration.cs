
using BackCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BackCore.DATA.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {


        public void Configure(EntityTypeBuilder<Product> ProductConfiguration)
        {

            ProductConfiguration.HasKey(x => x.Id);

            ProductConfiguration.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();


            ProductConfiguration.Property(o => o.Description)
                .HasMaxLength(1000);

            ProductConfiguration.Property(o => o.Barcode)
              .HasMaxLength(1000);
        }



    }
}

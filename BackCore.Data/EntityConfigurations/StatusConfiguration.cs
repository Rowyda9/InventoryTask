
using BackCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BackCore.DATA.EntityConfigurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {


        public void Configure(EntityTypeBuilder<Status> AdStatusConfiguration)
        {

            AdStatusConfiguration.HasKey(x => x.Id);

            AdStatusConfiguration.Property(o => o.Name)
                .HasMaxLength(100)
                .IsRequired();

            AdStatusConfiguration.Property(o => o.Description)
              .HasMaxLength(1000);

    
            

        }



    }
}

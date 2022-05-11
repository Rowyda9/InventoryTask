
using BackCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BackCore.DATA.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {


        public void Configure(EntityTypeBuilder<Category> CategoryConfiguration)
        {

            CategoryConfiguration.HasKey(x => x.Id);

            CategoryConfiguration.Property(o => o.Name)
                .HasMaxLength(500)
                .IsRequired();

        }



    }
}

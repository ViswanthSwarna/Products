using Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Constants.Enums;

namespace Products.Infrastructure.Data.Configurations
{
    public class CodeTrackerConfiguration : IEntityTypeConfiguration<CodeTracker>
    {
        public void Configure(EntityTypeBuilder<CodeTracker> builder)
        {
            builder.ToTable("CodeTrackers");

            builder.HasKey(c => c.Id);
                
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.LastCode).IsRequired();

            builder.HasData(new CodeTracker { Id = (int)CodeTrackerKey.Products, LastCode = 99999 });
        }
    }
}
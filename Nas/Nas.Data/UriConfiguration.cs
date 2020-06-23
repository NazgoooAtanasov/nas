using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nas.Data
{
    public class UriConfiguration : IEntityTypeConfiguration<Uri>
    {
        public void Configure(EntityTypeBuilder<Uri> builder)
        {
            builder.ToTable("Uris");

            builder.HasKey(x => x.Slug);

            builder.Property(x => x.Slug)
                .IsRequired();

            builder.Property(x => x.Link)
                .IsRequired();
        }
    }
}
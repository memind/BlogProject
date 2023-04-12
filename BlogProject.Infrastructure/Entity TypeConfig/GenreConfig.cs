using BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogProject.Infrastructure.Entity_TypeConfig
{
    internal class GenreConfig : BaseEntityConfig<Genre>
    {
        public override void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired(true);

            //builder.HasMany(x => x.Posts)
            //    .WithOne(x => x.Genre)
            //    .HasForeignKey(x => x.Post)

            base.Configure(builder);
        }
    }
}

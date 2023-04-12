

using BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogProject.Infrastructure.Entity_TypeConfig
{
    internal class PostConfig : BaseEntityConfig<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired(true);
            builder.Property(x => x.Content).IsRequired(true);
            builder.Property(x => x.ImagePath).IsRequired(false);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasOne(x => x.Genre)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.GenreId);

            base.Configure(builder);
        }
    }
}

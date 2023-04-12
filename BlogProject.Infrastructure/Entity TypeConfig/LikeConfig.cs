using BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogProject.Infrastructure.Entity_TypeConfig
{
    internal class LikeConfig : BaseEntityConfig<Like>
    {
        public override void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.PostId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict); 

            base.Configure(builder);
        }
    }
}

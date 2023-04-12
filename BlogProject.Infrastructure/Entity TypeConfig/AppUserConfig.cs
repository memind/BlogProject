using BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogProject.Infrastructure.Entity_TypeConfig
{
    internal class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.ImagePath).IsRequired(false);
            base.Configure(builder);
        }
    }
}

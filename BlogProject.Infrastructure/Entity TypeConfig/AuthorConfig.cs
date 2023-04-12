using BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogProject.Infrastructure.Entity_TypeConfig
{
    internal class AuthorConfig : BaseEntityConfig<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired(true);
            builder.Property(x => x.LastName).IsRequired(true);
            builder.Property(x => x.ImagePath).IsRequired(false);
            base.Configure(builder);
        }
    }
}

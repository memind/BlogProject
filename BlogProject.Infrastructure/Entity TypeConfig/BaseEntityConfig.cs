using BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Infrastructure.Entity_TypeConfig
{
    internal abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
    {
        public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.DeleteDate).IsRequired(false);            
            builder.Property(x => x.Status).IsRequired(true);
        }
    }
}

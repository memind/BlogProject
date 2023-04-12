using BlogProject.Domain.Entities;
using BlogProject.Infrastructure.Entity_TypeConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Infrastructure
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<AppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new PostConfig());
            builder.ApplyConfiguration(new LikeConfig());
            builder.ApplyConfiguration(new GenreConfig());
            builder.ApplyConfiguration(new CommentConfig());
            builder.ApplyConfiguration(new AuthorConfig());

            base.OnModelCreating(builder);
        }

    }
}

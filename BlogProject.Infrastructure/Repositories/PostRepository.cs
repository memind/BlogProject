using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;

namespace BlogProject.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}

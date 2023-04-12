using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;

namespace BlogProject.Infrastructure.Repositories
{
    public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        public LikeRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}

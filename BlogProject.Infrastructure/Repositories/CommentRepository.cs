using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;

namespace BlogProject.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}

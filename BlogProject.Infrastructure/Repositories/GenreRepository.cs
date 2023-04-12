using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;

namespace BlogProject.Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}

using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;

namespace BlogProject.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}

using BlogProject.Domain.Entities;

namespace BlogProject.Application.Services.LikeService
{
    public interface ILikeService
    {
        Task Create(Like model);
        Task Delete(int id);
    }
}

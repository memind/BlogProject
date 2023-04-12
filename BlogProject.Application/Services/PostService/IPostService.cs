using BlogProject.Application.Models.DTOs.PostDTOs;
using BlogProject.Application.Models.VMs.PostVMs;

namespace BlogProject.Application.Services.PostService
{
    public interface IPostService
    {
        Task Create(CreatePostDTO model);
        Task<List<PostVM>> GetPosts();
        Task Update(UpdatePostDTO model);
        Task Delete(int id);
        Task<UpdatePostDTO> GetById(int id);
        Task<PostDetailsVM> GetPostDetails(int id);
        Task<CreatePostDTO> CreatePost();
        Task<List<GetPostVM>> GetPostsForMembers();
    }
}

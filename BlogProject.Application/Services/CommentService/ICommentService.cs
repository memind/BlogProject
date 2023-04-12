using BlogProject.Application.Models.DTOs.CommentDTOs;
using BlogProject.Application.Models.VMs.CommentVMs;

namespace BlogProject.Application.Services.CommentService
{
    public interface ICommentService
    {
        Task Create(CreateCommentDTO model);
        Task<CreateCommentDTO> CreateComment();
        Task<List<CommentVM>> GetComments();
        Task Update(UpdateCommentDTO model);
        Task<UpdateCommentDTO> GetById(int id);
        Task Delete(int id);
    }
}

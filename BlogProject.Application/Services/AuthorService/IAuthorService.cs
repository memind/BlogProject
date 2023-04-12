using BlogProject.Application.Models.DTOs.AuthorDTOs;
using BlogProject.Application.Models.VMs.AuthorVMs;

namespace BlogProject.Application.Services.AuthorService
{
    public interface IAuthorService
    {
        // ToDo: HttpPost Create yap
        Task Create(CreateAuthorDTO model);
        Task Update(UpdateAuthorDTO model);
        Task Delete(int id);
        Task<List<AuthorVM>> GetAuthors();
        Task<UpdateAuthorDTO> GetById(int id);
        Task<AuthorDetailsVM> GetAuthorDetails(int id);
        Task<List<GetAuthorVM>> GetAuthorsForMembers();
    }
}

using BlogProject.Application.Models.DTOs.GenreDTOs;
using BlogProject.Application.Models.VMs.GenreVMs;

namespace BlogProject.Application.Services.GenreService
{
    public interface IGenreService
    {
        Task Create(GenreDTO model);
        Task<List<GenreVM>> GetGenres();
        Task Update(GenreDTO model);
        Task Delete(int id);
        Task<GenreDTO> GetById(int id);
    }
}

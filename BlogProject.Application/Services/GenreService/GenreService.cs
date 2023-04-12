using AutoMapper;
using BlogProject.Application.Models.DTOs.GenreDTOs;
using BlogProject.Application.Models.VMs.GenreVMs;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Enums;
using BlogProject.Domain.Repositories;

namespace BlogProject.Application.Services.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task Create(GenreDTO model)
        {
            Genre genre = _mapper.Map<Genre>(model);
            genre.Status = Status.Active;
            genre.CreateDate = DateTime.Now;

            await _genreRepository.Create(genre);
        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);

            genre.DeleteDate = DateTime.Now;
            genre.Status = Status.Passive;

            await _genreRepository.Delete(genre);
        }

        public async Task<GenreDTO> GetById(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);
            var model = _mapper.Map<GenreDTO>(genre);
            return model;
        }

        public async Task<List<GenreVM>> GetGenres()
        {
            return await _genreRepository.GetFilteredList(
                select: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name)
                );
        }

        public async Task Update(GenreDTO model)
        {
            var genre = _mapper.Map<Genre>(model);

            genre.UpdateDate = DateTime.Now;
            genre.Status = Status.Modified;

            await _genreRepository.Update(genre);
        }
    }
}

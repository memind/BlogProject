using AutoMapper;
using BlogProject.Application.Models.DTOs.AuthorDTOs;
using BlogProject.Application.Models.VMs.AuthorVMs;
using BlogProject.Application.Models.VMs.PostVMs;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Enums;
using BlogProject.Domain.Repositories;

namespace BlogProject.Application.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper, IPostRepository postRepository)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task Create(CreateAuthorDTO model)
        {
            Author author = _mapper.Map<Author>(model);

            if (author.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 560));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");

                author.ImagePath = $"/images/{guid}.jpg";
            }

            await _authorRepository.Create(author);
        }

        public async Task Delete(int id)
        {
            Author author = await _authorRepository.GetDefault(x => x.Id == id);
            author.DeleteDate = DateTime.Now;
            author.Status = Status.Passive;

            await _authorRepository.Delete(author);
        }

        public async Task<AuthorDetailsVM> GetAuthorDetails(int id)
        {
            var author = await _authorRepository.GetFilteredFirstOrDefault(
                select: x => new AuthorDetailsVM()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                    CreateDate = DateTime.Now
                },
                where: x => x.Id == id,
                orderBy: null
                );

            return author;
        }

        public async Task<List<AuthorVM>> GetAuthors()
        {
            var authors = await _authorRepository.GetFilteredList(
                select: x => new AuthorVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName)
                );

            return authors;
        }

        public async Task<List<GetAuthorVM>> GetAuthorsForMembers()
        {
            var authors = await _authorRepository.GetFilteredList(
                select: x => new GetAuthorVM()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreateDate,
                    Id = x.Id
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreateDate)
                );
            return authors;
        }

        public async Task<UpdateAuthorDTO> GetById(int id)
        {
            Author author = await _authorRepository.GetDefault(x => x.Id == id);
            var model = _mapper.Map<UpdateAuthorDTO>(author);

            model.Posts = await _postRepository.GetFilteredList(
                select: x => new PostVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    GenreName = x.Genre.Name,
                    ImagePath = x.ImagePath
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreateDate));

            return model;
        }

        public async Task Update(UpdateAuthorDTO model)
        {
            var author = _mapper.Map<Author>(model);

            if (author.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                author.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                author.ImagePath = model.ImagePath;
            }

            await _authorRepository.Update(author);
        }
    }
}

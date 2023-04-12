using AutoMapper;
using BlogProject.Application.Models.DTOs.PostDTOs;
using BlogProject.Application.Models.VMs.AuthorVMs;
using BlogProject.Application.Models.VMs.GenreVMs;
using BlogProject.Application.Models.VMs.PostVMs;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Enums;
using BlogProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Application.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;

        public PostService(IPostRepository postRepository, IMapper mapper, IGenreRepository genreRepository, IAuthorRepository authorRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
        }

        // HttpGet
        public async Task Create(CreatePostDTO model)
        {
            Post post = _mapper.Map<Post>(model);

            // Post'un resmi varsa veritabanina yolu yazilmali. Server uzerindeki bir klasore de resmin kendisi eklenmeli.

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 560));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");

                post.ImagePath = $"/images/{guid}.jpg";
            }

            else
                post.ImagePath = $"/images/defaultpost.jpg";

            await _postRepository.Create(post);
        }

        // Kullanicinin veritabaninda Post olusturabilmesi icin View sayfasinda modeli doldurmasini bekle. Controller'dan View'a model gonder. View'da doldurup Controller'a geri dondur.

        // HttpPost
        public async Task<CreatePostDTO> CreatePost()
        {
            CreatePostDTO model = new CreatePostDTO()
            {
                Genres = await _genreRepository.GetFilteredList(
                    select: x => new GenreVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Name)
                    ),

                Authors = await _authorRepository.GetFilteredList(
                    select: x => new AuthorVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.FirstName)
                    )
            };

            return model;
        }

        public async Task Delete(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.Id == id);
            post.DeleteDate = DateTime.Now;
            post.Status = Status.Passive;

            await _postRepository.Delete(post);
        }

        public async Task<UpdatePostDTO> GetById(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.Id == id);
            var model = _mapper.Map<UpdatePostDTO>(post);

            model.Authors = await _authorRepository.GetFilteredList(
                select: x => new AuthorVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName));

            model.Genres = await _genreRepository.GetFilteredList(
                select: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name));

            return model;
        }

        public async Task<PostDetailsVM> GetPostDetails(int id)
        {
            var post = await _postRepository.GetFilteredFirstOrDefault(
                select: x => new PostDetailsVM()
                {
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    AuthorImagePath = x.Author.ImagePath,
                    Content = x.Content,
                    CreateDate = DateTime.Now,
                    ImagePath = x.ImagePath,
                    Title = x.Title
                },
                where: x => x.Id == id,
                orderBy: null,
                include: x => x.Include(x => x.Author)
                );

            return post;
        }

        public async Task<List<PostVM>> GetPosts()
        {
            var posts = await _postRepository.GetFilteredList(
                select: x => new PostVM()
                {
                    Id = x.Id,
                    Title = x.Title,
                    GenreName = x.Genre.Name,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.Genre)
                               .Include(x => x.Author)
                );

            return posts;
        }

        public async Task<List<GetPostVM>> GetPostsForMembers()
        {
            var posts = await _postRepository.GetFilteredList(
                select: x => new GetPostVM()
                {
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    AuthorImagePath = x.Author.ImagePath,
                    Content = x.Content,
                    CreateDate = x.CreateDate,
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    Title = x.Title
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreateDate)
                );
            return posts;
        }

        public async Task Update(UpdatePostDTO model)
        {
            var post = _mapper.Map<Post>(model);

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                post.ImagePath = model.ImagePath;
            }

            await _postRepository.Update(post);
        }
    }
}

using AutoMapper;
using BlogProject.Application.Models.DTOs.CommentDTOs;
using BlogProject.Application.Models.DTOs.PostDTOs;
using BlogProject.Application.Models.VMs.CommentVMs;
using BlogProject.Application.Models.VMs.PostVMs;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Enums;
using BlogProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Application.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IPostRepository postRepository, IAppUserRepository appUserRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _postRepository = postRepository;
            _appUserRepository = appUserRepository;
        }

        public async Task Create(CreateCommentDTO model)
        {
            Comment comment = _mapper.Map<Comment>(model);
            await _commentRepository.Create(comment);
        }

        public async Task<CreateCommentDTO> CreateComment()
        {
            //CreateCommentDTO model = new CreateCommentDTO()
            //{
            //    Post = await _postRepository.GetFilteredList(
            //        select: x => new PostVM
            //        {
            //            Id = x.Id
            //        },
            //        where: x => x.Status != Status.Passive,
            //        orderBy: null,
            //        include: x => x.Include(x => x.Comments)),

            //    User = await _appUserRepository.GetFilteredList(
            //        select: x => new AppUserVM
            //        {
            //            Id = x.Id,
            //            FirstName = x.FirstName,
            //            LastName = x.LastName
            //        },
            //        where: x => x.Status != Status.Passive,
            //        orderBy: x => x.OrderBy(x => x.FirstName)
            //        )
            //};

            //return model;

            CreateCommentDTO model = new CreateCommentDTO()
            {
                //User = await _appUserRepository.Get();
                //Post = await _postRepository.GetDefault(x => x.Id),

                //User = await _appUserRepository.GetDefault(x => x.Id)
            };

            return model;
        }

        public async Task Delete(int id)
        {
            Comment comment = await _commentRepository.GetDefault(x => x.Id == id);
            comment.DeleteDate = DateTime.Now;
            comment.Status = Status.Passive;

            await _commentRepository.Delete(comment);
        }

        public async Task<UpdateCommentDTO> GetById(int id)
        {
            Comment comment = await _commentRepository.GetDefault(x => x.Id == id);
            var model = _mapper.Map<UpdateCommentDTO>(comment);
            return model;
        }

        public async Task<List<CommentVM>> GetComments()
        {
            var comments = await _commentRepository.GetFilteredList(
                select: x => new CommentVM()
                {
                    Id = x.Id,
                    Content = x.Content,
                    CommentedBy = x.User.UserName,
                    CommentedTo = x.Post.Title
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreateDate),
                include: x => x.Include(x => x.Post)
                               .Include(x => x.User)
                );

            return comments;
        }

        public async Task Update(UpdateCommentDTO model)
        {
            var comment = _mapper.Map<Comment>(model);

            if (comment.Content != null)
                comment.Content = model.Content;

            await _commentRepository.Update(comment);
        }
    }
}

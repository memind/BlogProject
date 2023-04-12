using AutoMapper;
using BlogProject.Application.Models.DTOs.AuthorDTOs;
using BlogProject.Application.Models.DTOs.CommentDTOs;
using BlogProject.Application.Models.DTOs.GenreDTOs;
using BlogProject.Application.Models.DTOs.PostDTOs;
using BlogProject.Application.Models.DTOs.UserDTOs;
using BlogProject.Application.Models.VMs.AuthorVMs;
using BlogProject.Application.Models.VMs.CommentVMs;
using BlogProject.Application.Models.VMs.GenreVMs;
using BlogProject.Application.Models.VMs.PostVMs;
using BlogProject.Domain.Entities;

namespace BlogProject.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping profilleri (Neyin neye donusturulecegi) buradan olusturulur.
            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, GetPostVM>().ReverseMap();
            CreateMap<Post, PostDetailsVM>().ReverseMap();
            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<CreatePostDTO, PostVM>().ReverseMap();
            CreateMap<UpdatePostDTO, PostVM>().ReverseMap();
            CreateMap<GetPostVM, PostVM>().ReverseMap();
            CreateMap<PostDetailsVM, PostVM>().ReverseMap();

            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
            CreateMap<Author, UpdateAuthorDTO>().ReverseMap();
            CreateMap<Author, GetAuthorVM>().ReverseMap();
            CreateMap<Author, AuthorDetailsVM>().ReverseMap();
            CreateMap<Author, AuthorVM>().ReverseMap();
            CreateMap<CreateAuthorDTO, UpdateAuthorDTO>().ReverseMap();
            CreateMap<CreateAuthorDTO, AuthorVM>().ReverseMap();
            CreateMap<UpdateAuthorDTO, AuthorVM>().ReverseMap();
            CreateMap<GetAuthorVM, AuthorVM>().ReverseMap();
            CreateMap<AuthorDetailsVM, AuthorVM>().ReverseMap();

            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();
            CreateMap<Genre, GetGenreVM>().ReverseMap();
            CreateMap<GenreVM, GetGenreVM>().ReverseMap();
            CreateMap<GenreVM, GenreDTO>().ReverseMap();
            CreateMap<GetGenreVM, GenreDTO>().ReverseMap();

            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
            CreateMap<Comment, CommentVM>().ReverseMap();
            CreateMap<UpdateCommentDTO, CommentVM>().ReverseMap();
            CreateMap<CreateCommentDTO, CommentVM>().ReverseMap();
            CreateMap<CreateCommentDTO, UpdateCommentDTO>().ReverseMap();

            CreateMap<AppUser, UpdateProfileDTO>().ReverseMap();
        }
    }
}

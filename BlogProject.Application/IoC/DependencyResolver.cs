using Autofac;
using AutoMapper;
using BlogProject.Application.AutoMapper;
using BlogProject.Application.Services.AppUserService;
using BlogProject.Application.Services.AuthorService;
using BlogProject.Application.Services.CommentService;
using BlogProject.Application.Services.GenreService;
using BlogProject.Application.Services.LikeService;
using BlogProject.Application.Services.PostService;
using BlogProject.Domain.Repositories;
using BlogProject.Infrastructure.Repositories;

namespace BlogProject.Application.IoC
{
    public class DependencyResolver : Module
    {
        // Veritabani ayaga kalkarken Package Manager Console'da secili olan proje contextin oldugu proje, startup projesi de MVC projesidir.

        protected override void Load(ContainerBuilder builder)
        {
            // Service DI
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<LikeService>().As<ILikeService>().InstancePerLifetimeScope();
            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();

            // Repository DI
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LikeRepository>().As<ILikeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();

            // AutoMapper Class DI
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();

            #region AutoMapper
            // AutoMapper'in baglanmasi (Internetten kopyala yapistir)
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }))
                .AsSelf() 
                .SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);
        }
    }
}

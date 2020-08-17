using Autofac;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Repository;
using Gallery.FileStorage;
using Gallery.FileStorage.Interfaces;
using Gallery.FileStorage.Services;

namespace Gallery.Modules
{
    public class HomeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ImageService>()
                .As<IImageService>();

            builder.RegisterType<HashService>()
                .As<IHashService>();

            builder.RegisterType<MediaStorageService>()
                .As<IMediaStorage>();

            builder.RegisterType<MediaRepository>()
                .As<IMediaRepository>();
        }
    }
}
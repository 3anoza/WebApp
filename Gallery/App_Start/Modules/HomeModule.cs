using Autofac;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;

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
        }
    }
}
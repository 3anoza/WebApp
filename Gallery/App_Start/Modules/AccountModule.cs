using Autofac;
using Gallery.BLL.Interfaces;
using Gallery.BLL.Services;
using Gallery.DAL;
using Gallery.DAL.Contexts;
using Gallery.DAL.Interfaces;
using Gallery.DAL.Models;
using Gallery.DAL.Repository;
using Gallery.Interfaces;
using Gallery.Services;

namespace Gallery.Modules
{
    public class AccountModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlContext>()
                .AsSelf();
            builder.RegisterType<DbRepository>()
                .As<IRepository>();
            builder.RegisterType<UserService>()
                .As<IUserService>();

            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>();
        }
    }
}
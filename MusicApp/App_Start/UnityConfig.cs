using MusicApp.Repository;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace MusicApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register your services here
            container.RegisterSingleton<IMusicRepository, MusicRepository>();
            container.RegisterSingleton<IUserRepository, UserRepository>();
            container.RegisterSingleton<AppDbContext>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }

}
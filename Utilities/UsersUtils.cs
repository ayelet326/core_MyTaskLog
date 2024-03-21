using User_.Interfaces;
using UserService.Services;

namespace UserUtils.Utilites;

public static class UsresUtils
{
    public static void AddUsres(this IServiceCollection services)
    {
        services.AddSingleton<IUserService, Useres>();
    }
}
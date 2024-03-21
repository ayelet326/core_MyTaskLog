using Microsoft.Extensions.DependencyInjection;
using Login.Interfaces;
using TokenService.Services;
using User_.Interfaces;
using UserService.Services;

namespace LoginUtils.Utilites;
public static class LoginUtils
{
    public static void AddLogin(this IServiceCollection services)
    {
        services.AddSingleton<ILoginService,TokenToLogin>();
        services.AddSingleton<IUserService,Useres>();    
    }
}
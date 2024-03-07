using Microsoft.AspNetCore.Authentication;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Startup;

public static class StartupExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IStudioService, StudioService>();
        builder.Services.AddScoped<IMediaService, MediaService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IClaimsTransformation, ExtendedClaimsTransformer>();
    }
}
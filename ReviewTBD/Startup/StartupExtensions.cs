using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Startup;

public static class StartupExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder) {
        builder.Services.AddScoped<IAnimeService, AnimeService>();
        //builder.Services.AddScoped<IAnimeStudioService, AnimeStudioService>();
        builder.Services.AddScoped<IGameService, GameService>();
        //builder.Services.AddScoped<IGameStudioService, GameStudioService>();
        builder.Services.AddScoped<IMovieService, MovieService>();
        //builder.Services.AddScoped<IMovieStudioService, MovieStudioService>();
        builder.Services.AddScoped<IStudioService, StudioService>();
    }
}

using Microsoft.Extensions.Logging;
using MauiApp1.Data;
using MauiApp1.Services;
using MudBlazor.Services;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using MauiApp1.Settings;

namespace MauiApp1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddMudServices();
		builder.Services.AddBlazoredLocalStorage();
        //builder.Services.Configure<ApiUrlSettings>(
        //    builder.Configuration.GetSection("BaseApi"));

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<WeatherForecastService>();
		builder.Services.AddSingleton<UserService>();
		builder.Services.AddSingleton<MQTTService>();
        builder.Services.AddTransient<TokenService>();
        builder.Services.AddSingleton<AuthService>();

        MQTTService.Start("broker.emqx.io", 1883);

        return builder.Build();
	}
}

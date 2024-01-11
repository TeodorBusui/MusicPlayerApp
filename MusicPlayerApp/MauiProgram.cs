global using Microsoft.Extensions.Logging;

global using System;
global using System.Net;
global using System.Text;
global using System.Text.Json;
global using System.Windows.Input;
global using System.Collections.ObjectModel;

global using TinyMvvm;

global using MusicPlayerApp.Models;
global using MusicPlayerApp.ViewModels;
global using MusicPlayerApp.Views;
global using MusicPlayerApp.Services;

global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Maui;

global using Microsoft.Maui.Controls;

namespace MusicPlayerApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseTinyMvvm()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Rubik-Regular.ttf", "Rubik");
				fonts.AddFont("Rubik-SemiBold.ttf", "RubikSemiBold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
		builder.Services.AddTransient<ArtistViewModel>();
		builder.Services.AddTransient<AlbumViewModel>();
		builder.Services.AddTransient<NewReleasesViewModel>();
		builder.Services.AddTransient<FavoritesViewModel>();

		builder.Services.AddTransient<LoginView>();
		builder.Services.AddTransient<HomeView>();
		builder.Services.AddTransient<ArtistView>();
		builder.Services.AddTransient<AlbumView>();
		builder.Services.AddTransient<NewReleasesView>();
		builder.Services.AddTransient<FavoritesView>();

        builder.Services.AddSingleton<ISpotifyService, SpotifyService>();
		builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();

		RegisterRoutes();

		return builder.Build();
	}

	private static void RegisterRoutes()
	{
		Routing.RegisterRoute("Artist", typeof(ArtistView));
		Routing.RegisterRoute("Album", typeof(AlbumView));
	}
}

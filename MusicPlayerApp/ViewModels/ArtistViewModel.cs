﻿namespace MusicPlayerApp.ViewModels
{
    public partial class ArtistViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;

        public ArtistViewModel(ISpotifyService spotifyService) 
        {
            this.spotifyService = spotifyService;
        }

        public override async Task OnParameterSet()
        {
            await base.OnParameterSet();

            try
            {
                IsBusy = true;

                var id = NavigationParameter.ToString();

                List<Task> tasks = new();

                var artistTask = spotifyService.GetArtist(id);
                var albumsTask = spotifyService.GetAlbums(id);

                await Task.WhenAll(artistTask, albumsTask);

                var artist = artistTask.Result;

                TopImage = artist.Images.Any() ? artist.Images.First().Url : null;
                Name = artist.Name;
                Followers = artist.Followers.Total;

                var albumResult = albumsTask.Result.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    SubTitle = string.Join(", ", x.Artists.Select(a => a.Name)),
                    ImageUrl = x.Images.Any() ? x.Images.First().Url : null,
                    TapCommand = NavigateToAlbumCommand,
                });

                Albums = new(albumResult);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }

            IsBusy = false;
        }

        [ObservableProperty]
        private string topImage, name;

        [ObservableProperty]
        private double followers;

        [ObservableProperty]
        private ObservableCollection<SearchItemViewModel> albums = new();

        [RelayCommand]
        private void NavigateToAlbum(string id)
        {
            Navigation.NavigateTo("Album", id);
        }

        [RelayCommand]
        private void AddToFavorites()
        {
            spotifyService.AddFavoriteArtist(NavigationParameter.ToString());
        }

        [RelayCommand]
        private void RemoveFromFavorites()
        {
            spotifyService.RemoveFavoriteArtist(NavigationParameter.ToString());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.ViewModels
{
    public partial class FavoritesViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;

        public FavoritesViewModel(ISpotifyService spotifyService)
        {
            this.spotifyService = spotifyService;

            InitializeAsync();
        }

        public async void InitializeAsync()
        {
            await FavoritesData();
        }

        private async Task FavoritesData()
        {
            try
            {
                IsBusy = true;

                List<Task> tasks = new();

                var artistsTask = spotifyService.GetFavoriteArtists();
                var albumsTask = spotifyService.GetFavoriteAlbums();
                var tracksTask = spotifyService.GetFavoriteTracks();

                await Task.WhenAll(artistsTask, albumsTask, tracksTask);

                var artists = artistsTask.Result.Artists.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    ImageUrl = x.Images.Any() ? x.Images.First().Url : null,
                    TapCommand = NavigateToArtistCommand,
                }).ToList();

                Artists = new ObservableCollection<SearchItemViewModel>(artists);

                var albums = albumsTask.Result.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Album.Id,
                    Title = x.Album.Name,
                    ImageUrl = x.Album.Images.Any() ? x.Album.Images.First().Url : null,
                    TapCommand = NavigateToAlbumCommand,
                });

                Albums = new ObservableCollection<SearchItemViewModel>(albums);

                var tracks = tracksTask.Result.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Track.Id,
                    Title = x.Track.Name,
                    SubTitle = string.Join(",", x.Track.Artists.Select(a => a.Name)),
                    ImageUrl = x.Track.Album.Images.Any() ? x.Track.Album.Images.First().Url : null,
                    TapCommand = NavigateToTrackCommand,
                });

                Tracks = new ObservableCollection<SearchItemViewModel>(tracks);

                HasResult = true;
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }

            IsBusy = false;
        }

        [ObservableProperty]
        private bool hasResult;

        [ObservableProperty]
        ObservableCollection<SearchItemViewModel> artists = new();
        [ObservableProperty]
        ObservableCollection<SearchItemViewModel> albums = new();
        [ObservableProperty]
        ObservableCollection<SearchItemViewModel> tracks = new();

        [RelayCommand]
        private void NavigateToArtist(string id)
        {
            Navigation.NavigateTo("Artist", id);
        }

        [RelayCommand]
        private void NavigateToAlbum(string id)
        {
            Navigation.NavigateTo("Album", id);
        }

        [RelayCommand]
        private void NavigateToTrack(string id)
        {
            Navigation.NavigateTo("Track", id);
        }
    }
}

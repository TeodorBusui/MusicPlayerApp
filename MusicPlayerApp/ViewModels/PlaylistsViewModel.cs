using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.ViewModels
{
    public partial class PlaylistsViewModel : ViewModel
    {
        public PlaylistsViewModel(ISpotifyService spotifyService)
        {
            this.spotifyService = spotifyService;

            InitializeAsync();
        }

        public async void InitializeAsync()
        {
            await PlaylistsData();
        }

        private async Task PlaylistsData()
        {

            try
            {
                IsBusy = true;

                List<Task> tasks = new();

                var playlistsTask = spotifyService.GetPlaylists();

                await playlistsTask;

                var playlistsResult = playlistsTask.Result.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    SubTitle = x.Owner.DisplayName,
                    ImageUrl = x.Images.Any() ? x.Images.First().Url : null,
                    TapCommand = NavigateToPlaylistCommand,
                });

                Playlists = new ObservableCollection<SearchItemViewModel>(playlistsResult);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }

            IsBusy = false;
        }

        [ObservableProperty]
        ObservableCollection<SearchItemViewModel> playlists = new();
        private readonly ISpotifyService spotifyService;

        [RelayCommand]
        private void NavigateToPlaylist(string id)
        {
              Navigation.NavigateTo("Playlist", id);
        }
    }
}

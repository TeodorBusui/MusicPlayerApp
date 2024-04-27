using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.ViewModels
{
    public partial class PlaylistsViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;
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

        [ObservableProperty]
        ObservableCollection<string> playlistPublicTypes = new()
        {
            "Yes",
            "No"
        };

        [ObservableProperty]
        private string playlistName;

        [ObservableProperty]
        private string playlistPublicType;

        [RelayCommand]
        private void NavigateToPlaylist(string id)
        {
              Navigation.NavigateTo("Playlist", id);
        }

        [RelayCommand]
        private async void CreatePlaylist()
        {
            var currentUserTask = spotifyService.GetCurrentUser();

            await currentUserTask;

            var currentUser = currentUserTask.Result;

            if (PlaylistPublicType == "Yes")
            {
                await spotifyService.CreatePlaylist(currentUser.Id, PlaylistName, true);
            }
            else
            {
                await spotifyService.CreatePlaylist(currentUser.Id, PlaylistName, false);
            }
        }
    }
}

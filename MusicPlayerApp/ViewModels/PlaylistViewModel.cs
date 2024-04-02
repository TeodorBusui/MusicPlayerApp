using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.ViewModels
{
    public partial class PlaylistViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;

        public PlaylistViewModel(ISpotifyService spotifyService)
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

                var playlistTask = spotifyService.GetPlaylist(id);
                var tracksTask = spotifyService.GetPlaylistTracks(id);

                await Task.WhenAll(playlistTask, tracksTask);

                var playlist = playlistTask.Result;

                TopImage = playlist.Images.Any() ? playlist.Images.First().Url : null;
                Name = playlist.Name;
                TotalTracks = tracksTask.Result.Total;

                var trackResult = tracksTask.Result.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Track.Id,
                    Title = x.Track.Name,
                    SubTitle = string.Join(",", x.Track.Artists.Select(a => a.Name)),
                    ImageUrl = x.Track.Album.Images.Any() ? x.Track.Album.Images.First().Url : null,
                    TapCommand = NavigateToTrackCommand,
                });

                Tracks = new(trackResult);
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
        private double totalTracks;

        [ObservableProperty]
        private ObservableCollection<SearchItemViewModel> tracks = new();


        [RelayCommand]
        private void NavigateToTrack(string id)
        {
            Navigation.NavigateTo("Track", id);
        }

        [RelayCommand]
        private async void StartPlaying()
        {
            var deviceTask = spotifyService.GetAvailableDevices();

            await deviceTask;

            var devices = deviceTask.Result;

            await spotifyService.TransferPlayback(devices.AvailableDevices[0].Id);

            CurrentlyPlayingTrack currentlyPlayingTrack = await spotifyService.GetCurrentlyPlayingTrack();

            await spotifyService.PlayPlaylist(NavigationParameter.ToString(), currentlyPlayingTrack);
        }

        [RelayCommand]
        private async void Pause()
        {
            await spotifyService.Pause();
        }

        [RelayCommand]
        private async void Next()
        {
            await spotifyService.Next();
        }

        [RelayCommand]
        private async void Previous()
        {
            await spotifyService.Previous();
        }
    }
}
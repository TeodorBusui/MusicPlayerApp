using System.ComponentModel;
using System.Timers;

namespace MusicPlayerApp.ViewModels
{
    public partial class TrackViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;

        public TrackViewModel(ISpotifyService spotifyService)
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

                var trackTask = spotifyService.GetTrack(id);

                await trackTask;

                var track = trackTask.Result;

                TopImage = track.Album.Images.Any() ? track.Album.Images.First().Url : null;
                Name = track.Name;

                var artists = track.Artists.Select(x => x.Name).ToList();

                Artists = new(artists);

                Duration = TimeSpan.FromMilliseconds(track.DurationMs);

                TrackDuration = Duration.TotalSeconds;

                CurrentTime = CurrentPosition.ToString(@"mm\:ss");

                timer = new System.Timers.Timer();

                GetPlaylistNames();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }

            IsBusy = false;
        }

        [ObservableProperty]
        private string currentTime;

        [ObservableProperty]
        private string topImage, name;

        [ObservableProperty]
        private TimeSpan duration, currentPosition;

        [ObservableProperty]
        private double progress, trackDuration;

        [ObservableProperty]
        private ObservableCollection<string> artists;

        [ObservableProperty]
        private ObservableCollection<string> playlistNames;

        [ObservableProperty]
        private string selectedPlaylistName;


        [RelayCommand]
        private void AddToFavorites()
        {
            spotifyService.AddFavoriteTrack(NavigationParameter.ToString());
        }

        [RelayCommand]
        private void RemoveFromFavorites()
        {
            spotifyService.RemoveFavoriteTrack(NavigationParameter.ToString());
        }


        System.Timers.Timer timer;
        [RelayCommand]
        private async void PlayTrack()
        {
            timer.Interval = 1000;
            timer.Elapsed += TimerElapsed;

            var deviceTask = spotifyService.GetAvailableDevices();

            await deviceTask;

            var devices = deviceTask.Result;

            await spotifyService.TransferPlayback(devices.AvailableDevices[0].Id);

            CurrentlyPlayingTrack currentlyPlayingTrack = await spotifyService.GetCurrentlyPlayingTrack();

            await spotifyService.PlayTrack(NavigationParameter.ToString(), currentlyPlayingTrack);

            timer.Start();
        }

        [RelayCommand]
        private async void PauseTrack()
        {
            await spotifyService.Pause();

            timer.Elapsed -= TimerElapsed;
        }

        [RelayCommand]
        private async void AddToPlaylist()
        {
            var playlistsTask = spotifyService.GetPlaylists();

            await playlistsTask;

            foreach(var playlist in playlistsTask.Result.Items) 
            { 
                if(playlist.Name == SelectedPlaylistName)
                {
                    await spotifyService.AddTrackToPlaylist(playlist.Id, NavigationParameter.ToString());
                    break;
                }
            }
        }

        [RelayCommand]
        private async void RemoveFromPlaylist()
        {
            var playlistsTask = spotifyService.GetPlaylists();

            await playlistsTask;

            foreach (var playlist in playlistsTask.Result.Items)
            {
                if (playlist.Name == SelectedPlaylistName)
                {
                    await spotifyService.RemoveTrackFromPlaylist(playlist.Id, NavigationParameter.ToString(), playlist.SnapshotId);
                    break;
                }
            }
        }

        private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Progress += 1;
            CurrentPosition = CurrentPosition.Add(TimeSpan.FromSeconds(1));
            CurrentTime = CurrentPosition.ToString(@"mm\:ss");

            if (Progress >= TrackDuration)
            {
                timer.Stop();
            }
        }

        private async void GetPlaylistNames()
        {
            var playlistNamesTask = spotifyService.GetPlaylists();

            await playlistNamesTask;

            var names = playlistNamesTask.Result.Items.Select(x => x.Name);

            PlaylistNames = new ObservableCollection<string>(names);
        }
    }
}

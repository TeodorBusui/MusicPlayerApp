using System.Xml.Linq;

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
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }

            IsBusy = false;
        }

        public string CurrentTime => CurrentPosition.ToString(@"mm\:ss");

        [ObservableProperty]
        private string topImage, name;

        [ObservableProperty]
        private TimeSpan duration, currentPosition;

        [ObservableProperty]
        private ObservableCollection<string> artists;


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
    }
}

namespace MusicPlayerApp.ViewModels
{
    public partial class AlbumViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;

        public AlbumViewModel(ISpotifyService spotifyService) 
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

                var albumTask = spotifyService.GetAlbum(id);
                var tracksTask = spotifyService.GetAlbumTracks(id);

                await Task.WhenAll(albumTask, tracksTask);

                var album = albumTask.Result;

                TopImage = album.Images.Any() ? album.Images.First().Url : null;
                Name = album.Name;
                ReleaseDate = album.ReleaseDate;

                var trackResult = tracksTask.Result.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    SubTitle = string.Join(",", x.Artists.Select(a => a.Name)),
                    ImageUrl = album.Images.Any() ? album.Images.First().Url : null,
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
        private string topImage, name, releaseDate;

        [ObservableProperty]
        private ObservableCollection<SearchItemViewModel> tracks = new();


        [RelayCommand]
        private void NavigateToTrack(string id)
        {
            Navigation.NavigateTo("Track", id);
        }

        [RelayCommand]
        private void AddToFavorites()
        {
            spotifyService.AddFavoriteAlbum(NavigationParameter.ToString());
        }

        [RelayCommand]
        private void RemoveFromFavorites()
        {
            spotifyService.RemoveFavoriteAlbum(NavigationParameter.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.ViewModels
{
    public partial class NewReleasesViewModel : ViewModel
    {
        private readonly ISpotifyService spotifyService;

        public NewReleasesViewModel(ISpotifyService spotifyService) 
        {
            this.spotifyService = spotifyService;

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            Albums = await NewReleasesData();
        }

        private async Task<ObservableCollection<SearchItemViewModel>> NewReleasesData()
        {
            ObservableCollection<SearchItemViewModel> NewAlbumsReleased = new();

            try
            {
                IsBusy = true;

                List<Task> tasks = new();

                var albumsTask = spotifyService.GetNewReleases();

                await albumsTask;

                var albumResult = albumsTask.Result.Albums.Items.Select(x => new SearchItemViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    SubTitle = string.Join(", ", x.Artists.Select(a => a.Name)),
                    ImageUrl = x.Images.Any() ? x.Images.First().Url : null,
                    TapCommand = NavigateToAlbumCommand,
                });

                NewAlbumsReleased = new(albumResult);
            }
            catch(Exception ex) 
            { 
                await HandleException(ex);
            }

            IsBusy = false;

            return NewAlbumsReleased;
        }

        [ObservableProperty]
        ObservableCollection<SearchItemViewModel> albums = new();

        [RelayCommand]
        private void NavigateToAlbum(string id)
        {
            Navigation.NavigateTo("Album", id);
        }
    }
}

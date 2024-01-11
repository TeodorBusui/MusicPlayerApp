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
            await NewReleasesData();
        }

        private async Task NewReleasesData()
        {

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

                Albums = new ObservableCollection<SearchItemViewModel>(albumResult);
            }
            catch(Exception ex) 
            { 
                await HandleException(ex);
            }

            IsBusy = false;
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

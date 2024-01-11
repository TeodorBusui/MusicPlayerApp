namespace MusicPlayerApp.Services
{
    public class SpotifyService : ISpotifyService
    {
        private string accessToken;
        private string refreshToken;

        private readonly ISecureStorageService secureStorageService;

        private HttpClient client;

        public SpotifyService(ISecureStorageService secureStorageService)
        {
            this.secureStorageService = secureStorageService;
        }

        public async Task<bool> Initialize(string authCode, bool isRefresh = false)
        {
            var bytes = Encoding.UTF8.GetBytes($"{Constants.SpotifyClientId}:{Constants.SpotifyClientSecret}");
            var authHeader = Convert.ToBase64String(bytes);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);

            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new(isRefresh ? "refresh_token" : "code", authCode),
                new("redirect_uri", Constants.RedirectUrl),
                new("grant_type", isRefresh ? "refresh_token" : "authorization_code")
            });

            var response = await client.PostAsync("https://accounts.spotify.com/api/token", content);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResult>(json);

            accessToken = result.AccessToken;

            if (!string.IsNullOrWhiteSpace(result.RefreshToken))
            {
                refreshToken = result.RefreshToken;
                await secureStorageService.Save(nameof(result.RefreshToken), result.RefreshToken);
            }

            await secureStorageService.Save(nameof(result.AccessToken), result.AccessToken);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IsSignedIn()
        {
            var hasToken = await secureStorageService.Contains(nameof(AuthResult.AccessToken));

            if (hasToken)
            {
                accessToken = await secureStorageService.Get(nameof(AuthResult.AccessToken));
                refreshToken = await secureStorageService.Get(nameof(AuthResult.RefreshToken));
            }

            return hasToken;
        }

        public Task<SearchResult> Search(string searchText, string types)
        {
            var url = $"search?q={searchText}&type={types}";

            return Get<SearchResult>(url);
        }

        public Task<Artist> GetArtist(string id)
        {
            var url = $"artists/{id}";

            return Get<Artist>(url);
        }

        public Task<SearchResult> GetFavoriteArtists()
        {
            var url = "me/following?type=artist";

            return Get<SearchResult>(url);
        }

        public Task<Album> GetAlbum(string id) 
        { 
            var url = $"albums/{id}";

            return Get<Album>(url);
        }

        public Task<Albums> GetAlbums(string artistId)
        {
            var url = $"artists/{artistId}/albums";

            return Get<Albums>(url);
        }

        public Task<FavoriteAlbumsResult> GetFavoriteAlbums()
        {
            var url = "me/albums";

            return Get<FavoriteAlbumsResult>(url);
        }

        public Task<SearchResult> GetNewReleases()
        {
            var url = "browse/new-releases";

            return Get<SearchResult>(url);
        }

        public Task<Tracks> GetAlbumTracks(string albumId)
        {
            var url = $"albums/{albumId}/tracks";

            return Get<Tracks>(url);
        }

        public Task<FavoriteTracksResult> GetFavoriteTracks()
        {
            var url = "me/tracks";

            return Get<FavoriteTracksResult>(url);
        }

        private int retryCount = 0;
        private async Task<T> Get<T>(string url)
        {
            RefreshClient();

            var response = await client.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.Unauthorized && retryCount == 0)
            {
                retryCount++;
                client = null;
                var refreshResult = await Initialize(refreshToken, true);

                if (refreshResult)
                {
                    return await Get<T>(url);
                }

                throw new UnauthorizedAccessException();
            }

            retryCount = 0;

            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<T>(content);

            return result;
        }

        private void RefreshClient()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://api.spotify.com/v1/");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
        }
    }
}
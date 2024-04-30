using Microsoft.Maui.Controls;
using System;

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

        public Task<Track> GetTrack(string trackId) 
        {
            var url = $"tracks/{trackId}";

            return Get<Track>(url);
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

        public Task<Devices> GetAvailableDevices() 
        {
            var url = "me/player/devices";

            return Get<Devices>(url);
        }

        public Task<Playlists> GetPlaylists() 
        {
            var url = "me/playlists";

            return Get<Playlists>(url);
        }

        public Task<Playlist> GetPlaylist(string playlistId)
        {
            var url = $"playlists/{playlistId}";

            return Get<Playlist>(url);
        }

        public Task<PlaylistTracks> GetPlaylistTracks(string  playlistId)
        {
            var url = $"playlists/{playlistId}/tracks";

            return Get<PlaylistTracks>(url);
        }

        public Task<CurrentlyPlayingTrack> GetCurrentlyPlayingTrack()
        {
            var url = "me/player/currently-playing";

            return Get<CurrentlyPlayingTrack>(url);
        }

        public Task<User> GetCurrentUser()
        {
            var url = "me";

            return Get<User>(url);
        }

        public Task AddFavoriteArtist(string artistId)
        {
            var url = "me/following?type=artist";

            return FollowArtist(url, artistId);
        }

        public Task RemoveFavoriteArtist(string artistId)
        {
            var url = "me/following?type=artist";

            return UnfollowArtist(url, artistId);
        }

        public Task AddFavoriteAlbum(string albumId)
        {
            var url = "me/albums";

            return FollowAlbum(url, albumId);
        }

        public Task CreatePlaylist(string userId, string playlistName, bool publicPlaylist)
        {
            var url = $"users/{userId}/playlists";

            return CreatePlaylistForAUser(url, playlistName, publicPlaylist);
        }

        public Task RemoveFavoriteAlbum(string albumId)
        {
            var url = "me/albums";

            return UnfollowAlbum(url, albumId);
        }

        public Task AddFavoriteTrack(string trackId)
        {
            var url = "me/tracks";

            return SaveTrack(url, trackId);
        }

        public Task AddTrackToPlaylist(string playlistId, string trackId)
        {
            var url = $"playlists/{playlistId}/tracks";

            return AddATrackToPlaylist(url, trackId);
        }

        public Task RemoveTrackFromPlaylist(string playlistId, string trackId, string snapshotId)
        {
            var url = $"playlists/{playlistId}/tracks";

            return RemoveATrackFromPlaylist(url, trackId, snapshotId);
        }

        public Task RemoveFavoriteTrack(string trackId)
        {
            var url = "me/tracks";

            return UnsaveTrack(url, trackId);
        }

        public Task PlayTrack(string trackId, CurrentlyPlayingTrack currentlyPlayingTrack)
        {
            var url = "me/player/play";

            return StartPlayingTrack(url, trackId, currentlyPlayingTrack);
        }

        public Task PlayPlaylist(string playlistId, CurrentlyPlayingTrack currentlyPlayingTrack)
        {
            var url = "me/player/play";

            return StartPlayingPlaylist(url, playlistId, currentlyPlayingTrack);
        }

        public Task Pause()
        {
            var url = "me/player/pause";

            return PausePlaying(url);
        }

        public Task Next()
        {
            var url = "me/player/next";

            return SkipToNext(url);
        }

        public Task Previous()
        {
            var url = "me/player/previous";

            return SkipToPrevious(url);
        }

        public Task TransferPlayback(string deviceId)
        {
            var url = "me/player";

            return ChangePlaybackDevice(url, deviceId);
        }

        public Task SetPlaylistDefaultImage(string playlistId)
        {
            var url = $"playlists/{playlistId}/images";

            return SetDefPlaylistImage(url);
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

        private async Task FollowArtist(string url, string artistId)
        {
            List<string> ids = new List<string>() { artistId };

            var request = new
            {
                ids = ids
            };

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task UnfollowArtist(string url, string artistId)
        {
            List<string> ids = new List<string>() { artistId };

            var request = new
            {
                ids = ids
            };

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task FollowAlbum(string url, string albumId)
        {
            List<string> ids = new List<string>() { albumId };

            var request = new
            {
                ids = ids
            };

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task UnfollowAlbum(string url, string albumId)
        {
            List<string> ids = new List<string>() { albumId };

            var request = new
            {
                ids = ids
            };

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task SaveTrack(string url, string trackId)
        {
            List<string> ids = new List<string>() { trackId };

            var request = new
            {
                ids = ids
            };

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task UnsaveTrack(string url , string trackId)
        {
            List<string> ids = new List<string>() { trackId };

            var request = new
            {
                ids = ids
            };

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task StartPlayingTrack(string url, string trackId, CurrentlyPlayingTrack currentlyPlayingTrack)
        {
            List<string> uris = new List<string>() { $"spotify:track:{trackId}" };

            var request = new
            {
                uris = uris,
                position_ms = 0
            };

            if(currentlyPlayingTrack != null && currentlyPlayingTrack.Track.Id == trackId)
            {
                request = new
                {
                    uris = uris,
                    position_ms = currentlyPlayingTrack.ProgressMs
                };
            }

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task StartPlayingPlaylist(string url, string playlistId, CurrentlyPlayingTrack currentlyPlayingTrack)
        {
            string context_uri = $"spotify:playlist:{playlistId}";

            PlaylistTracks playlistTracks = await GetPlaylistTracks(playlistId);
            Offset offset = new Offset();
            offset.position = 0;

            var request = new
            {
                context_uri = context_uri,
                offset = offset,
                position_ms = 0
            };

            if (currentlyPlayingTrack != null)
            {
                for(int i = 0; i<playlistTracks.Total; i++)
                {
                    if (playlistTracks.Items[i].Track.Id == currentlyPlayingTrack.Track.Id)
                    {
                        offset.position = i;

                        request = new
                        {
                            context_uri = context_uri,
                            offset = offset,
                            position_ms = currentlyPlayingTrack.ProgressMs
                        };
                        break;
                    }
                }
            }

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task PausePlaying(string url)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, url)
                {
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task SkipToNext(string url)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
                {
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task SkipToPrevious(string url)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
                {
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task ChangePlaybackDevice(string url, string deviceId)
        {
            List<string> device_ids = new List<string>() { deviceId };

            var request = new
            {
                device_ids = device_ids
            };

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task AddATrackToPlaylist(string url, string trackId)
        {
            List<string> uris = new List<string>() { $"spotify:track:{trackId}" };

            var request = new
            {
                uris = uris
            };

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task RemoveATrackFromPlaylist(string url, string trackId, string snapshotId)
        {
            List<TracksP> tracks = new List<TracksP>();

            TracksP track = new TracksP();
            track.uri = $"spotify:track:{trackId}";

            tracks.Add(track);

            var request = new
            {
                tracks = tracks,
                snapshot_id = snapshotId
            };

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task CreatePlaylistForAUser(string url, string playlistName, bool publicPlaylist)
        {
            var request = new
            {
                name = playlistName,
                @public = publicPlaylist
            };

            string json = JsonSerializer.Serialize(request);

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task SetDefPlaylistImage(string url)
        {
            using var EnStream = await FileSystem.OpenAppPackageFileAsync("playlistCover.jpeg");
            using var mstream = new MemoryStream();
            EnStream.CopyTo(mstream);
            var base64Image = Convert.ToBase64String(mstream.ToArray());

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(base64Image, Encoding.Default, "image/jpeg")
                };

                var response = await client.SendAsync(requestMessage);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
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

    class TracksP
    {
        public TracksP() { }

        public string uri { get; set;}
    }
}
﻿namespace MusicPlayerApp.Services
{
    public interface ISpotifyService
    {
        Task<bool> Initialize(string authCode, bool isRefresh = false);
        Task<bool> IsSignedIn();

        Task<SearchResult> Search(string searchText, string types);
        Task<Artist> GetArtist(string id);
        Task<SearchResult> GetFavoriteArtists();
        Task<Album> GetAlbum(string id);
        Task<Albums> GetAlbums(string artistId);
        Task<FavoriteAlbumsResult> GetFavoriteAlbums();
        Task<Track> GetTrack(string trackId);
        Task<SearchResult> GetNewReleases();
        Task<Tracks> GetAlbumTracks(string albumId);
        Task<FavoriteTracksResult> GetFavoriteTracks();
        Task<Playlists> GetPlaylists();
        Task<Playlist> GetPlaylist(string id);
        Task<PlaylistTracks> GetPlaylistTracks(string id);
        Task<CurrentlyPlayingTrack> GetCurrentlyPlayingTrack();

        Task<Devices> GetAvailableDevices();
        Task<User> GetCurrentUser();

        Task AddFavoriteArtist(string artistId);
        Task AddFavoriteAlbum(string albumId);
        Task AddFavoriteTrack(string trackId);
        Task AddTrackToPlaylist(string playlistId, string trackId);
        Task CreatePlaylist(string userId, string playlistName, bool publicPlaylist);

        Task RemoveFavoriteArtist(string artistId);
        Task RemoveFavoriteAlbum(string albumId);
        Task RemoveFavoriteTrack(string trackId);
        Task RemoveTrackFromPlaylist(string playlistId, string trackId, string snapshotId);

        Task PlayTrack(string trackId, CurrentlyPlayingTrack currentlyPlayingTrack);
        Task PlayPlaylist(string playlistId, CurrentlyPlayingTrack currentlyPlayingTrack);

        Task Pause();
        Task Next();
        Task Previous();

        Task TransferPlayback(string deviceId);
        Task SetPlaylistDefaultImage(string playlistId);
    }
}

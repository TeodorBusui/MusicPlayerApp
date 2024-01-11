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
        Task<SearchResult> GetNewReleases();
        Task<Tracks> GetAlbumTracks(string albumId);
        Task<FavoriteTracksResult> GetFavoriteTracks();
    }
}

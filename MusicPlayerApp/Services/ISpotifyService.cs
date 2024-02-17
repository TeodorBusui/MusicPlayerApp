namespace MusicPlayerApp.Services
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

        Task AddFavoriteArtist(string artistId);
        Task AddFavoriteAlbum(string albumId);
        Task AddFavoriteTrack(string trackId);

        Task RemoveFavoriteArtist(string artistId);
        Task RemoveFavoriteAlbum(string albumId);
        Task RemoveFavoriteTrack(string trackId);
    }
}

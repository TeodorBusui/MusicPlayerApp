using System.Text.Json.Serialization;

namespace MusicPlayerApp.Models
{
    public class Album
    {
        [JsonPropertyName("album_type")]
        public string AlbumType { get; set; }

        [JsonPropertyName("artists")]
        public List<Artist> Artists { get; set; }

        [JsonPropertyName("available_markets")]
        public List<string> AvailableMarkets { get; set; }

        [JsonPropertyName("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [JsonPropertyName("total_tracks")]
        public double TotalTracks { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }

    public class Albums
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("items")]
        public List<Album> Items { get; set; }

        [JsonPropertyName("limit")]
        public double Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public double Offset { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }
    }

    public class Artist
    {
        [JsonPropertyName("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("followers")]
        public Followers Followers { get; set; }
    }

    public class Artists
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("items")]
        public List<Artist> Items { get; set; }

        [JsonPropertyName("limit")]
        public double Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public double Offset { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }
    }

    public class ExternalIds
    {
        [JsonPropertyName("isrc")]
        public string Isrc { get; set; }
    }

    public class ExternalUrls
    {
        [JsonPropertyName("spotify")]
        public string Spotify { get; set; }
    }

    public class Followers
    {
        [JsonPropertyName("href")]
        public object Href { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("height")]
        public double? Height { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public double? Width { get; set; }
    }

    public class Track
    {
        [JsonPropertyName("album_type")]
        public string AlbumType { get; set; }

        [JsonPropertyName("artists")]
        public List<Artist> Artists { get; set; }

        [JsonPropertyName("available_markets")]
        public List<string> AvailableMarkets { get; set; }

        [JsonPropertyName("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; } = new();

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [JsonPropertyName("total_tracks")]
        public double TotalTracks { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("followers")]
        public Followers Followers { get; set; }

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; }

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        [JsonPropertyName("album")]
        public Album Album { get; set; }

        [JsonPropertyName("disc_number")]
        public double DiscNumber { get; set; }

        [JsonPropertyName("duration_ms")]
        public double DurationMs { get; set; }

        [JsonPropertyName("explicit")]
        public bool Explicit { get; set; }

        [JsonPropertyName("external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }

        [JsonPropertyName("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonPropertyName("track_number")]
        public double TrackNumber { get; set; }
    }

    public class CurrentlyPlayingTrack
    {
        [JsonPropertyName("device")]
        public Device Device { get; set; }

        [JsonPropertyName("repeat_state")]
        public string RepeatState { get; set; }

        [JsonPropertyName("shuffle_state")]
        public bool ShuffleState { get; set; }

        [JsonPropertyName("context")]
        public Context Context { get; set; }

        [JsonPropertyName("timestamp")]
        public double Timestamp { get; set; }

        [JsonPropertyName("progress_ms")]
        public int ProgressMs { get; set; }

        [JsonPropertyName("is_playing")]
        public bool IsPlaying { get; set; }

        [JsonPropertyName("item")]
        public Track Track { get; set; }

        [JsonPropertyName("currently_playing_type")]
        public string CurrentlyPlayingType { get; set; }

        [JsonPropertyName("actions")]
        public Actions Actions { get; set; }
    }

    public class SearchResult
    {
        [JsonPropertyName("albums")]
        public Albums Albums { get; set; }

        [JsonPropertyName("artists")]
        public Artists Artists { get; set; }

        [JsonPropertyName("tracks")]
        public Tracks Tracks { get; set; }
    }

    public class Tracks
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("items")]
        public List<Track> Items { get; set; }

        [JsonPropertyName("limit")]
        public double Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public double Offset { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }
    }

    public class FavoriteAlbum
    {
        [JsonPropertyName("added_at")]
        public string AddedAt { get; set; }

        [JsonPropertyName("album")]
        public Album Album { get; set; }
    }

    public class FavoriteTrack
    {
        [JsonPropertyName("added_at")]
        public string AddedAt { get; set; }

        [JsonPropertyName("track")]
        public Track Track { get; set; }
    }

    public class FavoriteAlbumsResult
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("items")]
        public List<FavoriteAlbum> Items { get; set; }

        [JsonPropertyName("limit")]
        public double Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public double Offset { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }
    }

    public class FavoriteTracksResult
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("items")]
        public List<FavoriteTrack> Items { get; set; }

        [JsonPropertyName("limit")]
        public double Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public double Offset { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }
    }

    public class Device
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("is_private_session")]
        public bool IsPrivateSession { get; set; }

        [JsonPropertyName("is_restricted")]
        public bool IsRestricted { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("volume_percent")]
        public double VolumePercent { get; set; }

        [JsonPropertyName("supports_volume")]
        public bool SupportsVolume { get; set; }
    }

    public class Devices
    {
        [JsonPropertyName("devices")]
        public List<Device> AvailableDevices { get; set; }
    }

    public class AddedBy
    {
        [JsonPropertyName("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("followers")]
        public Followers Followers { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }

    public class Owner
    {
        [JsonPropertyName("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("followers")]
        public Followers Followers {  get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
    }

    public class SimplifiedPlaylistObject
    {
        [JsonPropertyName("collaborative")]
        public bool Collaborative { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }

        [JsonPropertyName("public")]
        public bool Public { get; set; }

        [JsonPropertyName("snapshot_id")]
        public string SnapshotId { get; set; }

        [JsonPropertyName("tracks")]
        public Tracks Tracks { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }

    public class Playlist
    {
        [JsonPropertyName("collaborative")]
        public bool Collaborative { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("followers")]
        public Followers Followers { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }

        [JsonPropertyName("public")]
        public bool Public { get; set; }

        [JsonPropertyName("snapshot_id")]
        public string SnapshotId { get; set; }

        [JsonPropertyName("tracks")]
        public Tracks Tracks { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }

    public class Playlists
    {
        [JsonPropertyName ("href")]
        public string Href { get; set; }

        [JsonPropertyName("limit")]
        public double Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName ("offset")]
        public double Offset { get; set; }

        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }

        [JsonPropertyName("items")]
        public List<SimplifiedPlaylistObject> Items { get; set; }
    }

    public class PlaylistTrackObject
    {
        [JsonPropertyName("added_at")]
        public string AddedAt { get; set; }

        [JsonPropertyName("added_by")]
        public AddedBy AddedBy { get; set; }

        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }

        [JsonPropertyName("track")]
        public Track Track { get; set; }
    }

    public class PlaylistTracks
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("limit")]
        public double Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public double Offset { get; set; }

        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }

        [JsonPropertyName("items")]
        public List<PlaylistTrackObject> Items { get; set; }
    }

    public class Context
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }

    public class Actions
    {
        [JsonPropertyName("interrupting_playback")]
        public bool InterruptionPlayback { get; set; }

        [JsonPropertyName("pausing")]
        public bool Pausing { get; set; }

        [JsonPropertyName("resuming")]
        public bool Resuming { get; set; }

        [JsonPropertyName("seeking")]
        public bool Seeking { get; set; }

        [JsonPropertyName("skipping_next")]
        public bool SkippingNext { get; set; }

        [JsonPropertyName("skipping_prev")]
        public bool SkippingPrevious { get; set; }

        [JsonPropertyName("toggling_repeat_context")]
        public bool TogglingRepeatContext { get; set; }

        [JsonPropertyName("toggling_shuffle")]
        public bool TogglingShuffle { get; set; }

        [JsonPropertyName("toggleing_repeat_track")]
        public bool TogglingRepeatTrack { get; set; }

        [JsonPropertyName("transferring_playback")]
        public bool TransferringPlayback { get; set; }
    }

    public class User
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("explicit_content")]
        public ExplicitContent ExplicitContent { get; set; }

        [JsonPropertyName("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonPropertyName("followers")]
        public Followers Followers { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("product")]
        public string Product {  get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }

    public class Offset
    {
        public int position { get; set; }
    }

    public class ExplicitContent
    {
        [JsonPropertyName("filter_enabled")]
        public bool FilterEnabled { get; set; }

        [JsonPropertyName("filter_locked")]
        public bool FilterLocked { get; set; }
    }
}

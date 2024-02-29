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
        public int TotalTracks { get; set; }

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
        public int Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
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
        public int Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
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
        public double Height { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }
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
        public int TotalTracks { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("followers")]
        public Followers Followers { get; set; }

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; }

        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }

        [JsonPropertyName("album")]
        public Album Album { get; set; }

        [JsonPropertyName("disc_number")]
        public int DiscNumber { get; set; }

        [JsonPropertyName("duration_ms")]
        public int DurationMs { get; set; }

        [JsonPropertyName("explicit")]
        public bool Explicit { get; set; }

        [JsonPropertyName("external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }

        [JsonPropertyName("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonPropertyName("track_number")]
        public int TrackNumber { get; set; }
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
        public int Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
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
        public int Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    public class FavoriteTracksResult
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("items")]
        public List<FavoriteTrack> Items { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
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
        public int VolumePercent { get; set; }

        [JsonPropertyName("supports_volume")]
        public bool SupportsVolume { get; set; }
    }

    public class Devices
    {
        [JsonPropertyName("devices")]
        public List<Device> AvailableDevices { get; set; }
    }
}

namespace MoodPlaylistGenerator.Models
{
    public class PlaylistSong
    {
        // This is the new, required primary key
        public int Id { get; set; }

        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public int Position { get; set; } // Order of the song in the playlist

        // Navigation properties
        public Playlist Playlist { get; set; } = null!;
        public Song Song { get; set; } = null!;
    }
}
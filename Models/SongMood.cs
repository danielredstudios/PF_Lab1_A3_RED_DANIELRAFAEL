namespace MoodPlaylistGenerator.Models
{
    public class SongMood
    {
        // The conflicting 'Id' property has been removed.
        public int SongId { get; set; }
        public int MoodId { get; set; }

        // Navigation properties
        public Song Song { get; set; } = null!;
        public Mood Mood { get; set; } = null!;
    }
}
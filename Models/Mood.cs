using System.ComponentModel.DataAnnotations;

namespace MoodPlaylistGenerator.Models
{
    public class Mood
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = "#808080"; // Default to grey

        // Navigation properties
        public List<SongMood> SongMoods { get; set; } = new();
        public List<Playlist> Playlists { get; set; } = new();
    }
}
using System.ComponentModel.DataAnnotations;

namespace MoodPlaylistGenerator.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public List<Song> Songs { get; set; } = new();
        public List<Playlist> Playlists { get; set; } = new();
    }
}
using System.ComponentModel.DataAnnotations;
using MoodPlaylistGenerator.Models;

namespace MoodPlaylistGenerator.ViewModels
{
    public class PlaylistListViewModel
    {
        public List<Playlist> Playlists { get; set; } = new();
        public List<Mood> Moods { get; set; } = new();
        public int? FilterMoodId { get; set; }
    }

    public class PlaylistDetailViewModel
    {
        public Playlist Playlist { get; set; } = null!;
        public List<PlaylistSong> Songs { get; set; } = new();
        public bool CanEdit { get; set; }
    }

    public class GeneratePlaylistViewModel
    {
        [Required]
        [Display(Name = "Select a Mood")]
        public int SelectedMoodId { get; set; }

        [Range(1, 50, ErrorMessage = "Please enter a number between 1 and 50.")]
        [Display(Name = "Number of Songs")]
        public int SongCount { get; set; } = 10;

        [StringLength(100)]
        [Display(Name = "Playlist Name (Optional)")]
        public string? PlaylistName { get; set; }

        public List<Mood> AvailableMoods { get; set; } = new();
        public Dictionary<int, int> MoodSongCounts { get; set; } = new();
    }

    public class EditPlaylistNameViewModel
    {
        [Required]
        public int PlaylistId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
        public string Name { get; set; } = "";
    }
}
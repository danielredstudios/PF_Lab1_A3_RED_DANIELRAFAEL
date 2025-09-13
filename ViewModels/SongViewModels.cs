using System.ComponentModel.DataAnnotations;
using MoodPlaylistGenerator.Models;

namespace MoodPlaylistGenerator.ViewModels
{
    public class SongListViewModel
    {
        public List<Song> Songs { get; set; } = new();
        public List<Mood> Moods { get; set; } = new();
        public int? SelectedMoodId { get; set; }
        public string SearchTerm { get; set; } = "";
    }

    public class SongDetailViewModel
    {
        public Song Song { get; set; } = null!;
        public string YouTubeVideoId { get; set; } = "";
        public List<Mood> AssignedMoods { get; set; } = new();
    }

    public class CreateSongViewModel
    {
        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Artist { get; set; } = "";

        [Required]
        [Url]
        [Display(Name = "YouTube URL")]
        public string YouTubeUrl { get; set; } = "";

        [Display(Name = "Moods")]
        public List<int> SelectedMoodIds { get; set; } = new();

        public List<Mood> AvailableMoods { get; set; } = new();
    }

    public class EditSongViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Artist { get; set; } = "";

        [Required]
        [Url]
        [Display(Name = "YouTube URL")]
        public string YouTubeUrl { get; set; } = "";

        [Display(Name = "Moods")]
        public List<int> SelectedMoodIds { get; set; } = new();

        public List<Mood> AvailableMoods { get; set; } = new();
    }
}
using System.ComponentModel.DataAnnotations;

namespace MoodPlaylistGenerator.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = "";
    }

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
}
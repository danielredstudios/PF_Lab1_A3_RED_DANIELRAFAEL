using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MoodPlaylistGenerator.Models;
using MoodPlaylistGenerator.Services;
using System.Security.Claims;
using MoodPlaylistGenerator.ViewModels;

namespace MoodPlaylistGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly SongService _songService;
        private readonly PlaylistService _playlistService;

        public HomeController(SongService songService, PlaylistService playlistService)
        {
            _songService = songService;
            _playlistService = playlistService;
        }

        public IActionResult Index()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction(nameof(Dashboard));
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

            var allSongs = await _songService.GetUserSongsAsync(userId);
            var allPlaylists = await _playlistService.GetUserPlaylistsAsync(userId);
            var moods = await _songService.GetAllMoodsAsync();
            var moodSongCounts = await _playlistService.GetMoodSongCountsAsync(userId);

            var viewModel = new DashboardViewModel
            {
                TotalSongs = allSongs.Count,
                TotalPlaylists = allPlaylists.Count,
                RecentSongs = allSongs.OrderByDescending(s => s.CreatedAt).Take(5).ToList(),
                RecentPlaylists = allPlaylists.OrderByDescending(p => p.GeneratedAt).Take(5).ToList(),
                Moods = moods,
                MoodSongCounts = moodSongCounts
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
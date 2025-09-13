using Microsoft.EntityFrameworkCore;
using MoodPlaylistGenerator.Data;
using MoodPlaylistGenerator.Models;
using System.Web;

namespace MoodPlaylistGenerator.Services
{
    public class SongService
    {
        private readonly ApplicationDbContext _context;

        public SongService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Song>> GetUserSongsAsync(int userId)
        {
            return await _context.Songs
                .Include(s => s.SongMoods)
                .ThenInclude(sm => sm.Mood)
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        public async Task<Song?> GetSongByIdAsync(int songId, int userId)
        {
            return await _context.Songs
                .Include(s => s.SongMoods)
                .ThenInclude(sm => sm.Mood)
                .FirstOrDefaultAsync(s => s.Id == songId && s.UserId == userId);
        }

        // ===============================================================
        // START: Corrected CreateSongAsync Method
        // ===============================================================
        public async Task<Song> CreateSongAsync(string title, string artist, string youtubeUrl, int userId, List<int> moodIds)
        {
            // 1. Create the new Song object
            var song = new Song
            {
                Title = title,
                Artist = artist,
                YouTubeUrl = youtubeUrl,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            // 2. If moods were selected, create the SongMood objects and add them
            //    directly to the song's navigation property.
            if (moodIds != null && moodIds.Any())
            {
                foreach (var moodId in moodIds)
                {
                    song.SongMoods.Add(new SongMood { MoodId = moodId });
                }
            }

            // 3. Add the complete song object (with its moods) to the context.
            _context.Songs.Add(song);

            // 4. Save everything in a single transaction.
            await _context.SaveChangesAsync();

            return song;
        }
        // ===============================================================
        // END: Corrected CreateSongAsync Method
        // ===============================================================

        public async Task<Song?> UpdateSongAsync(int songId, int userId, string title, string artist, string youtubeUrl, List<int> moodIds)
        {
            var song = await _context.Songs
                .Include(s => s.SongMoods)
                .FirstOrDefaultAsync(s => s.Id == songId && s.UserId == userId);

            if (song == null)
                return null;

            // Update song properties
            song.Title = title;
            song.Artist = artist;
            song.YouTubeUrl = youtubeUrl;

            // Remove existing mood associations
            _context.SongMoods.RemoveRange(song.SongMoods);

            // Add new mood associations
            foreach (var moodId in moodIds)
            {
                song.SongMoods.Add(new SongMood
                {
                    SongId = song.Id,
                    MoodId = moodId
                });
            }

            await _context.SaveChangesAsync();
            return await GetSongByIdAsync(songId, userId);
        }

        public async Task<bool> DeleteSongAsync(int songId, int userId)
        {
            var song = await _context.Songs
                .FirstOrDefaultAsync(s => s.Id == songId && s.UserId == userId);

            if (song == null)
                return false;

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Mood>> GetAllMoodsAsync()
        {
            return await _context.Moods.OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<List<Song>> GetSongsByMoodAsync(int moodId, int userId)
        {
            return await _context.Songs
                .Include(s => s.SongMoods)
                .ThenInclude(sm => sm.Mood)
                .Where(s => s.UserId == userId && s.SongMoods.Any(sm => sm.MoodId == moodId))
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        public string ExtractYouTubeVideoId(string url)
        {
            var uri = new Uri(url);

            if (uri.Host.Contains("youtu.be"))
            {
                return uri.AbsolutePath.TrimStart('/');
            }

            if (uri.Host.Contains("youtube.com"))
            {
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                return query["v"] ?? "";
            }

            return "";
        }
    }
}
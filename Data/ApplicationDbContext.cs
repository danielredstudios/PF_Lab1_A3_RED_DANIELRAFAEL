using Microsoft.EntityFrameworkCore;
using MoodPlaylistGenerator.Models;

namespace MoodPlaylistGenerator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Mood> Moods { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<SongMood> SongMoods { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite primary key for SongMood
            modelBuilder.Entity<SongMood>()
                .HasKey(sm => new { sm.SongId, sm.MoodId });

            // Configure relationships
            modelBuilder.Entity<SongMood>()
                .HasOne(sm => sm.Song)
                .WithMany(s => s.SongMoods)
                .HasForeignKey(sm => sm.SongId);

            modelBuilder.Entity<SongMood>()
                .HasOne(sm => sm.Mood)
                .WithMany(m => m.SongMoods)
                .HasForeignKey(sm => sm.MoodId);

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Playlist)
                .WithMany(p => p.PlaylistSongs)
                .HasForeignKey(ps => ps.PlaylistId);

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Song)
                .WithMany(s => s.PlaylistSongs)
                .HasForeignKey(ps => ps.SongId);

            // Seed initial data for moods
            modelBuilder.Entity<Mood>().HasData(
                new Mood { Id = 1, Name = "Happy", Description = "Upbeat and joyful tunes.", Color = "#FFD700" },
                new Mood { Id = 2, Name = "Sad", Description = "Melancholic and emotional songs.", Color = "#1E90FF" },
                new Mood { Id = 3, Name = "Energetic", Description = "Fast-paced and high-energy tracks.", Color = "#FF4500" },
                new Mood { Id = 4, Name = "Calm", Description = "Relaxing and peaceful music.", Color = "#3CB371" },
                new Mood { Id = 5, Name = "Romantic", Description = "Love songs and ballads.", Color = "#FF69B4" },
                new Mood { Id = 6, Name = "Focus", Description = "Instrumental or ambient music for concentration.", Color = "#6A5ACD" },
                new Mood { Id = 7, Name = "Workout", Description = "Motivational tracks for exercise.", Color = "#DC143C" },
                new Mood { Id = 8, Name = "Party", Description = "Music to get you dancing.", Color = "#F08080" }
            );
        }
    }
}
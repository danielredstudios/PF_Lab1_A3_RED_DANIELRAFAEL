using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoodPlaylistGenerator.Migrations
{
    /// <inheritdoc />
    public partial class FinalSchemaFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    MoodId = table.Column<int>(type: "INTEGER", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_Moods_MoodId",
                        column: x => x.MoodId,
                        principalTable: "Moods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Playlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Artist = table.Column<string>(type: "TEXT", nullable: false),
                    YouTubeUrl = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistSongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlaylistId = table.Column<int>(type: "INTEGER", nullable: false),
                    SongId = table.Column<int>(type: "INTEGER", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistSongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistSongs_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongMoods",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "INTEGER", nullable: false),
                    MoodId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongMoods", x => new { x.SongId, x.MoodId });
                    table.ForeignKey(
                        name: "FK_SongMoods_Moods_MoodId",
                        column: x => x.MoodId,
                        principalTable: "Moods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongMoods_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Moods",
                columns: new[] { "Id", "Color", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "#FFD700", "Upbeat and joyful tunes.", "Happy" },
                    { 2, "#1E90FF", "Melancholic and emotional songs.", "Sad" },
                    { 3, "#FF4500", "Fast-paced and high-energy tracks.", "Energetic" },
                    { 4, "#3CB371", "Relaxing and peaceful music.", "Calm" },
                    { 5, "#FF69B4", "Love songs and ballads.", "Romantic" },
                    { 6, "#6A5ACD", "Instrumental or ambient music for concentration.", "Focus" },
                    { 7, "#DC143C", "Motivational tracks for exercise.", "Workout" },
                    { 8, "#F08080", "Music to get you dancing.", "Party" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_MoodId",
                table: "Playlists",
                column: "MoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSongs_PlaylistId",
                table: "PlaylistSongs",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSongs_SongId",
                table: "PlaylistSongs",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SongMoods_MoodId",
                table: "SongMoods",
                column: "MoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_UserId",
                table: "Songs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistSongs");

            migrationBuilder.DropTable(
                name: "SongMoods");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Moods");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

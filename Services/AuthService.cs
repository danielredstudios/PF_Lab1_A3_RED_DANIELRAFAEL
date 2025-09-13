using Microsoft.EntityFrameworkCore;
using MoodPlaylistGenerator.Data;
using MoodPlaylistGenerator.Models;

namespace MoodPlaylistGenerator.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(User?, string?)> RegisterUserAsync(string username, string password)
        {
<<<<<<< Updated upstream
            // Check if user exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email || u.Username == username);

            if (existingUser != null)
                return null;

            // Hash password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
=======
            if (await _context.Users.AnyAsync(u => u.Username == username))
            {
                return (null, "Username already exists.");
            }
>>>>>>> Stashed changes

            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (user, null);
        }

        public async Task<(User?, string?)> SignInUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return (null, "Invalid username or password.");
            }

            return (user, null);
        }
    }
}
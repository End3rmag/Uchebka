using System.Linq;
using Microsoft.EntityFrameworkCore;
using AvaloniaApplication1.Context;
using AvaloniaApplication1.Models;

namespace AvaloniaApplication1.Services
{
    public class AuthService
    {
        public User AuthenticateBySms(string phoneNumber)
        {
            using var context = new User009Context();
            return context.Users
                .Include(u => u.RoleNavigation)
                .FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        public User AuthenticateByLogin(string login, string password)
        {
            using var context = new User009Context();
            return context.Users
                .Include(u => u.RoleNavigation)
                .FirstOrDefault(u => u.Login == login && u.PasswordHash == password);
        }

        public User AuthenticateByMobileId(string phoneNumber)
        {
            using var context = new User009Context();
            return context.Users
                .Include(u => u.RoleNavigation)
                .FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }
    }
}
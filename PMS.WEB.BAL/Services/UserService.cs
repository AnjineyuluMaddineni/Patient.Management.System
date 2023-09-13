using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Web.Data;
using PMS.Web.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<AppUser> GetUser(string Id)
        {
            var result = await _context.AppUsers.FirstOrDefaultAsync();
            return result;
        }

        public async Task<Guid> GetUserId(string email, string role)
        {
            var appUser = await _userManager.FindByEmailAsync(email);

            Guid userId;
            switch (role)
            {
                case "Patient":
                    userId = await _context.Patients.Where(s => s.AppUserId == appUser.Id).Select(s => s.PId).FirstOrDefaultAsync();
                    break;
                default:
                    userId = await _context.HospitalUser.Where(s => s.AppUserId == appUser.Id).Select(s => s.EmployeeId).FirstOrDefaultAsync();
                    break;
            }
            return userId;
        }
        public async Task<string> GetAppUserId(string email)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            if(appUser!=null)
                return appUser.Id;
            return null;
        }
    }

    public interface IUserService {
        Task<AppUser> GetUser(string Id);
        Task<Guid> GetUserId(string email, string role);
        Task<string> GetAppUserId(string email);
    }
}

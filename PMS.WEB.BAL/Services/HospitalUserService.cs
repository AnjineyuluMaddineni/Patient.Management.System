using Microsoft.EntityFrameworkCore;
using PMS.Web.Data;
using PMS.Web.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public class HospitalUserService : IHospitalUserService
    {
        private readonly ApplicationDbContext _context;

        public HospitalUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HospitalUser>> GetEmployees()
        {
            var employees = await _context.HospitalUser.ToListAsync();
            return employees;
        }
    }

    public interface IHospitalUserService
    {
        Task<IEnumerable<HospitalUser>> GetEmployees();
    }
}

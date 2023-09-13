using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PMS.Web.Models;
using PMS.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalUserController : ControllerBase
    {

        private readonly IHospitalUserService _employeeService;

        private readonly IMapper _mapper;

        public HospitalUserController(IMapper mapper, IHospitalUserService employeeService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IEnumerable<HospitalUserModel>> Get()
        {
            var list = await _employeeService.GetEmployees();
            //Using automapper to map Models to/from entities.
            var employees = _mapper.Map<IEnumerable<HospitalUserModel>>(list);
            return employees;
        }
    }
}

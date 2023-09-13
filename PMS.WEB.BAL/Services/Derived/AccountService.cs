using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PMS.Web.Configuration;
using PMS.Web.Data;
using PMS.Web.Entities;
using PMS.Web.Models;
using PMS.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppUserRoles> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;
        public AccountService(UserManager<AppUser> userManager, IMapper mapper, IOptionsMonitor<JwtConfig> optionsMonitor,
            RoleManager<AppUserRoles> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _context = context;
            _jwtConfig = optionsMonitor.CurrentValue;

        }
        public async Task<List<UserModel>> GetPatientUsers()
        {
            var role = await _roleManager.FindByNameAsync(UserRolesModels.Patient);
            var userList = new List<UserModel>();
            if (role != null)
            {

                var Track = await (from o in _context.AppUsers
                                   join j in _context.UserRoles
                                   on o.Id equals j.UserId
                                   where j.RoleId == role.Id
                                   select new AppUser
                                   {
                                       Id = o.Id,
                                       FirstName = o.FirstName,
                                       LastName = o.LastName,
                                       BirthDate = o.BirthDate,
                                       RegistrationDate = o.RegistrationDate,
                                       Email = o.Email,
                                       Status = o.Status,
                                       PhoneNumber = o.PhoneNumber
                                   }).ToListAsync();
                foreach (var user in Track)
                {
                    UserModel newUser = _mapper.Map<UserModel>(user);
                    userList.Add(newUser);
                }
            }
            return userList;

        }

        public async Task<List<UserModel>> GetHospitalUsers()
        {
            var userList = new List<UserModel>();
            var role = await _roleManager.FindByNameAsync(UserRolesModels.Patient);
            if (role != null)
            {

                var Track = await (from o in _context.AppUsers
                                   join j in _context.UserRoles
                                   on o.Id equals j.UserId
                                   join r in _context.Roles
                                   on j.RoleId equals r.Id
                                   where j.RoleId != role.Id
                                   select new AppUser
                                   {
                                       Id = o.Id,
                                       FirstName = o.FirstName,
                                       LastName = o.LastName,
                                       BirthDate = o.BirthDate,
                                       RegistrationDate = o.RegistrationDate,
                                       Email = o.Email,
                                       Status = o.Status,
                                       PhoneNumber = o.PhoneNumber,
                                       Role = r.Name
                                   }).ToListAsync();
                foreach (var user in Track)
                {
                    UserModel newUser = _mapper.Map<UserModel>(user);
                    userList.Add(newUser);
                }
            }
            return userList;
        }

        public async Task<List<UserModel>> GetHospitalUsers(string appUserId)
        {
            var userList = new List<UserModel>();
            var role = await _roleManager.FindByNameAsync(UserRolesModels.Patient);
            if (role != null)
            {

                var Track = await (from o in _context.AppUsers
                                   join j in _context.UserRoles
                                   on o.Id equals j.UserId
                                   join r in _context.Roles
                                   on j.RoleId equals r.Id
                                   where j.RoleId != role.Id && o.Id != appUserId
                                   select new AppUser
                                   {
                                       Id = o.Id,
                                       FirstName = o.FirstName,
                                       LastName = o.LastName,
                                       BirthDate = o.BirthDate,
                                       RegistrationDate = o.RegistrationDate,
                                       Email = o.Email,
                                       Status = o.Status,
                                       PhoneNumber = o.PhoneNumber,
                                       Role = r.Name
                                   }).ToListAsync();
                foreach (var user in Track)
                {
                    UserModel newUser = _mapper.Map<UserModel>(user);
                    userList.Add(newUser);
                }
            }
            return userList;
        }

        public async Task<List<UserModel>> GetAllPhysicianNurseUsers(string appUserId)
        {
            var userList = new List<UserModel>();
            var role = await _roleManager.FindByNameAsync(UserRolesModels.Admin);
            var role1 = await _roleManager.FindByNameAsync(UserRolesModels.Patient);
            if (role != null && role1 != null)
            {

                var Track = await (from o in _context.AppUsers
                                   join j in _context.UserRoles
                                   on o.Id equals j.UserId
                                   join r in _context.Roles
                                   on j.RoleId equals r.Id
                                   where j.RoleId != role.Id && j.RoleId != role1.Id && o.Id != appUserId
                                   select new AppUser
                                   {
                                       Id = o.Id,
                                       FirstName = o.FirstName,
                                       LastName = o.LastName,
                                       BirthDate = o.BirthDate,
                                       RegistrationDate = o.RegistrationDate,
                                       Email = o.Email,
                                       Status = o.Status,
                                       PhoneNumber = o.PhoneNumber,
                                       Role = r.Name
                                   }).ToListAsync();
                foreach (var user in Track)
                {
                    UserModel newUser = _mapper.Map<UserModel>(user);
                    userList.Add(newUser);
                }
            }
            return userList;
        }

        public async Task<List<HospitalUserModel>> GetAllPhysician()
        {
            var physicianList = new List<HospitalUserModel>();
            var role = await _roleManager.FindByNameAsync(UserRolesModels.Physician);
            if (role != null)
            {
                var Track = await (from o in _context.HospitalUser
                                   join j in _context.AppUsers
                                   on o.AppUserId equals j.Id
                                   join u in _context.UserRoles
                                   on j.Id equals u.UserId
                                   where u.RoleId == role.Id
                                   select new HospitalUser
                                   {
                                       EmployeeId = o.EmployeeId,
                                       AppUserId = j.Id,
                                       AppUser = new AppUser
                                       {
                                           FirstName = j.FirstName,
                                           LastName = j.LastName
                                       }
                                   }).ToListAsync();

                foreach (var user in Track)
                {
                    physicianList.Add(_mapper.Map<HospitalUserModel>(user));
                }
            }
            return physicianList;
        }

        public async Task<List<PatientModel>> GetAllPatientUsers()
        {
            var physicianList = new List<PatientModel>();
            var role = await _roleManager.FindByNameAsync(UserRolesModels.Patient);
            if (role != null)
            {
                var Track = await (from o in _context.Patients
                                   join j in _context.AppUsers
                                   on o.AppUserId equals j.Id
                                   join u in _context.UserRoles
                                   on j.Id equals u.UserId
                                   where u.RoleId == role.Id
                                   select new Patient
                                   {
                                       PId = o.PId,
                                       AppUserId = j.Id,
                                       AppUser = new AppUser
                                       {
                                           FirstName = j.FirstName,
                                           LastName = j.LastName
                                       }
                                   }).ToListAsync();

                foreach (var user in Track)
                {
                    physicianList.Add(_mapper.Map<PatientModel>(user));
                }
            }
            return physicianList;
        }

        public async Task<List<AppUserRoles>> GetAllRoles()
        {

            var roles = await _roleManager.Roles.Where(s => s.Name != UserRolesModels.Patient).ToListAsync();
            return roles;
        }

        public static string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";
            string allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "@,$";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            char[] sep = {
            ','
            };
            string[] arr = allowedChars.Split(sep);
            string IDString = "";
            Random rand = new();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                string temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }
            return NewPassword;
        }

        private static void SendPasswordTOMail(UserModel user, string password)
        {
            MailMessage msg = new()
            {
                From = new MailAddress("citiusprojectteam3@gmail.com")
            };
            msg.To.Add(user.Email);
            msg.Subject = "Auto Generated Password";
            msg.Body = "Your New Password is: " + password;
            msg.IsBodyHtml = true;
            SmtpClient smtp = new();
            smtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential network = new()
            {
                UserName = "citiusprojectteam3@gmail.com",
                Password = "CT#12345"
            };
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = network;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }

        public async Task<int> EmployeeRegistration(UserModel user, string roleId)
        {
            string password = GeneratePassword();
            user.Password = password;
            AppUser newUser = new()
            {
                Id = user.Id,
                Title = user.Title,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Status = user.Status,
                RegistrationDate = user.RegistrationDate,
                Password = password
            };
            var userExists = await _userManager.FindByEmailAsync(user.Email);
            if (userExists != null)
                return 0;

            IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                //Adding employee in hospitalusers table
                var hospitalUsers = new HospitalUser
                {
                    EmployeeId = Guid.NewGuid(),
                    AppUserId = user.Id
                };
                await _context.HospitalUser.AddAsync(hospitalUsers);
                await _context.SaveChangesAsync();


                var role = await _roleManager.FindByIdAsync(roleId);
                switch (role.Name)
                {
                    case "Physician":
                        var resultOfRole1 = await _userManager.AddToRoleAsync(newUser, UserRolesModels.Physician);
                        if (resultOfRole1.Succeeded)
                        {
                            //SendPasswordTOMail(user, password);
                            return 1;
                        }
                        break;
                    case "Admin":
                        var resultOfRole2 = await _userManager.AddToRoleAsync(newUser, UserRolesModels.Admin);
                        if (resultOfRole2.Succeeded)
                        {
                            //SendPasswordTOMail(user, password);
                            return 1;
                        }
                        break;
                    case "Nurse":
                        var resultOfRole3 = await _userManager.AddToRoleAsync(newUser, UserRolesModels.Nurse);
                        if (resultOfRole3.Succeeded)
                        {
                            //SendPasswordTOMail(user, password);
                            return 1;
                        }
                        break;
                    default:
                        return 0;

                }
                return 0;

            }
            else
                return -1;
        }

        public async Task<AppUser> Login(UserLoginModel user)
        {
            AppUser appUser;
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return null;
            else
            {
                appUser = await _userManager.FindByEmailAsync(user.Email);
                if (appUser == null)
                    return null;
                else if (appUser.Status == UserStatusModels.Active)
                {
                    var res = await _userManager.CheckPasswordAsync(appUser, user.Password);
                    if (res)
                    {
                        var jwtToken = GenerateJwtToken(appUser).Result;
                        appUser.Token = jwtToken.Token;
                        return appUser;
                    }
                    else
                        return null;
                }
                else
                    return appUser;
            }
        }

        public async Task<bool> ChangePassword(string Email, string oldPassword,string newPassword)
        {
            AppUser appUser;
            if (string.IsNullOrEmpty(Email))
                return false;
            appUser = await _userManager.FindByEmailAsync(Email);
            if (appUser == null)
                return false;

            if (await _userManager.CheckPasswordAsync(appUser, oldPassword) && (newPassword != oldPassword)) {
                var res = await _userManager.RemovePasswordAsync(appUser);
                if (res.Succeeded)
                {
                    var result = await _userManager.AddPasswordAsync(appUser, newPassword);
                    if (result.Succeeded)
                    {
                        appUser.Password = newPassword;
                        appUser.Status = 1;
                        var updated = await _userManager.UpdateAsync(appUser);
                        return updated.Succeeded;
                    }
                    return false;
                }
            }
            
            
            return false;
        }

        public bool ForgotPassword(string Email)
        {
            AppUser appUser;
            if (string.IsNullOrEmpty(Email))
                return false;
            else
            {
                appUser = _userManager.FindByEmailAsync(Email).Result;
                if (appUser == null)
                    return false;
                else
                {
                    string password = GeneratePassword();

                    var res = _userManager.RemovePasswordAsync(appUser).Result;
                    if (res.Succeeded)
                    {
                        var result = _userManager.AddPasswordAsync(appUser, password).Result;
                        if (result.Succeeded)
                        {
                            appUser.Password = password;
                            appUser.Status = 1;
                            var updated = _userManager.UpdateAsync(appUser).Result;
                            if (updated.Succeeded)
                            {
                                //SendPasswordTOMail(_mapper.Map<UserModel>(appUser), password);
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
        }

        public async Task<int> ChangeStatus(string userId, string command)
        {
            var status = command switch
            {
                "InActive" => ((int)UserStatus.InActive),
                "Blocked" => ((int)UserStatus.Blocked),
                _ => ((int)UserStatus.Active),
            };
            AppUser appUser;
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(command))
                return 0;
            else
            {
                appUser = await _userManager.FindByIdAsync(userId);
                if (appUser == null)
                    return 0;
                else
                {
                    appUser.Status = status;
                    _context.AppUsers.Update(appUser);
                    var res = this._context.SaveChanges();
                    return res;
                }
            }


        }

        private async Task<AuthResult> GenerateJwtToken(AppUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var claims = await GetAllValidClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtConfig.ValidIssuer,
                Audience = _jwtConfig.ValidAudience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30), // 5-10 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                //RefreshToken = refreshToken.Token
            };
        }

        private async Task<List<Claim>> GetAllValidClaims(AppUser user)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Getting the claims that we have assigned to the user
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            // Get the user role and add it to the claims
            var UserRolesModels = await _userManager.GetRolesAsync(user);

            foreach (var userRole in UserRolesModels)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                    
                }
            }

            return claims;
        }
       
       
       
    }
}

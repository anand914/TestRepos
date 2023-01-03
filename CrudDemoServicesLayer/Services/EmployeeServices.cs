using CrudDemoDataAccessLayer.Common;
using CrudDemoDataAccessLayer.Data;
using CrudDemoDataAccessLayer.Models;
using CrudDemoDataAccessLayer.ViewModel;
using CrudDemoServicesLayer.Services.IService;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;

namespace CrudDemoServicesLayer.Services
{
    public class EmployeeServices : IEmployee
    {
        private readonly EmployeeContext _context;
        private readonly IConfiguration _configuration;
        private readonly ICommonSrvc _srvc;
        private readonly IEmail _emailSrvc;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmployeeServices(EmployeeContext context, IConfiguration configuration, ICommonSrvc srvc, IEmail emailsrvc, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _srvc = srvc;
            _emailSrvc = emailsrvc;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse> ChangePassword(ChangePasswordVM model)
        {
            try
            {
                var result = _context.Employees.Where(x => x.ID == model.Id && x.Password == model.OldPassword).FirstOrDefault();
                if (result != null)
                {
                    result.Email = model.NewEmail;
                    result.Password = model.NewPassword;
                    _context.SaveChanges();
                    return new ApiResponse(200, true, null, "password Changed Successfully", null);
                }
                return null;
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, false, new List<string> { ex.Message }, null, null);
            }
        }

        public async Task<ApiResponse> DeleteEmployee(int Id)
        {
            try
            {
                var data = _context.Employees.Find(Id);
                if (data != null)
                {
                    _context.Employees.Remove(data);
                    _context.SaveChanges();
                    return new ApiResponse(200, true, null, "Employee Deleted Succesfully", null);
                }
                return null;
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, false, new List<string> { ex.Message }, null, null);
            }
        }

        public async Task<ApiResponse> GetAllemployees()
        {
            try
            {
                List<EmployeeVM> result = (from emp in _context.Employees
                                           join dep in _context.Departments on emp.DepartmentId equals dep.ID
                                           join gen in _context.Genders on emp.GenderId equals gen.ID
                                           select new EmployeeVM
                                           {
                                               ID = emp.ID,
                                               GendeId = gen.ID,
                                               DepartmentId = dep.ID,
                                               EmpName = emp.EmpName,
                                               Address = emp.Address,
                                               Contact = emp.Contact,
                                               ProfileImagePath = emp.ProfileImagePath,
                                               DepName = dep.DepName,
                                               desc = gen.desc
                                           }).ToList();
                if (result.Count > 0)
                {
                    for (int i = 0; i < result.Count(); i++)
                    {
                        result[i].ProfileImagePath = Path.Join($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}", "images", result[i].ProfileImagePath);
                    }
                    return new ApiResponse(200, true, null, result, null);
                }
                return new ApiResponse(201, true, null, null, null);
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, false, new List<string> { ex.Message }, null, null);
            }
        }

        public async Task<ApiResponse> GetEmployeeById(int id)
        {
            try
            {
                List<EmployeeVM> result = (from emp in _context.Employees
                                           join dep in _context.Departments on emp.DepartmentId equals dep.ID
                                           join gen in _context.Genders on emp.GenderId equals gen.ID
                                           where emp.ID == id
                                           select new EmployeeVM
                                           {
                                               ID = emp.ID,
                                               GendeId = gen.ID,
                                               DepartmentId = dep.ID,
                                               EmpName = emp.EmpName,
                                               Address = emp.Address,
                                               Contact = emp.Contact,
                                               ProfileImagePath = emp.ProfileImagePath,
                                               DepName = dep.DepName,
                                               desc = gen.desc
                                           }).ToList();
                if (result != null)
                {
                    return new ApiResponse(200, true, null, result, null);
                }
                return new ApiResponse(201, true, null, null, null);
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, false, new List<string> { ex.Message }, null, null);
            };
        }

        public async Task<ApiResponse> Login(LoginVM model)
        {
            try
            {
                var data = _context.Employees.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                if (data != null)
                {
                    var tokenhandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                          new Claim(ClaimTypes.NameIdentifier,data.ID.ToString()),
                           new Claim(ClaimTypes.Name,data.EmpName),
                           new Claim(ClaimTypes.Role,"Employee") //here role should be from data base not like "Employee"
                        }),
                        NotBefore = DateTime.Now,
                        Expires = DateTime.Now.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                    };
                    var token = tokenhandler.CreateToken(tokenDescriptor);
                    var UAT = tokenhandler.WriteToken(token);
                    return new ApiResponse(200, true, null, "Login Succesfull Attempted", UAT);
                }
                return new ApiResponse(201, false, new List<string> { "Wrong User And/Or Password." }, null, null);
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, false, new List<string> { ex.Message }, null, null);
            }
        }

        public async Task<ApiResponse> SaveEmployee(AddEmployeeVM model)
        {
            string fileName = string.Empty;
            if (model.Id == 0)
            {
                Employee obj = new Employee
                {
                    EmpName = model.EmpName,
                    Address = model.Address,
                    Email = model.Email,
                    Password = model.Password,
                    Contact = model.Contact,
                    ProfileImagePath = model.ProfileImagePath,
                    GenderId = model.GendeId,
                    DepartmentId = model.DepartmentId,
                };
                if (model.files != null)
                {
                    try
                    {
                        var extension = "." + model.files.FileName.Split('.')[model.files.FileName.Split('.').Length - 1];
                        fileName = Guid.NewGuid().ToString() + extension;
                        if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\wwwroot\\images\\"))
                        {
                            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\wwwroot\\images\\");
                        }
                        using (FileStream filestream = System.IO.File.Create(Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + fileName))
                        {
                            model.files.CopyTo(filestream);
                            filestream.Flush();
                            model.ProfileImagePath = fileName;
                        }
                    }
                    catch (Exception ex)
                    {
                        return new ApiResponse(500, false, new List<string> { ex.Message }, null, null);
                    }
                }
                obj.ProfileImagePath = fileName;
                WelcomeRequest wm = new WelcomeRequest();
                wm.ToEmail = obj.Email;
                wm.UserName = obj.EmpName;
                await _emailSrvc.SendWelcomeEmailAsync(wm);
                _context.Employees.Add(obj);
                _context.SaveChanges();
                UserInfoVM vm = new UserInfoVM();
                vm.Email = obj.Email;
                vm.Password = obj.Password;
                vm.Roles = Roles.Employee;
                await _srvc.AddUserInfo(vm);
                return new ApiResponse(200, true, null, "Employee Add Succesfully", null);
            }
            return null;
        }

        //public void SendEmail(EmailModelVM model)
        //{
        //    var email = new MimeMessage();
        //    email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
        //    email.To.Add(MailboxAddress.Parse(model.To));
        //    email.Subject = model.Subject;
        //    email.Body = new TextPart(TextFormat.Html) { Text = model.Body };

        //    using var smtp = new MailKit.Net.Smtp.SmtpClient();
        //    smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
        //    smtp.Send(email);
        //    smtp.Disconnect(true);
        //}

        public async Task<ApiResponse> UpdateemployeeInfo(UpdateEmployeeVM model)
        {

            try
            {
                var extension = "." + model.files.FileName.Split('.')[model.files.FileName.Split('.').Length - 1];
                string fileName = Guid.NewGuid().ToString() + extension;
                string rootFolder = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\";
                string imgFile = "";
                var result = _context.Employees.Where(x => x.ID == model.EmpID).FirstOrDefault();
                if (result != null)
                {
                    result.EmpName = model.EmpName;
                    result.Address = model.Address;
                    result.Contact = model.Contact;
                    result.DepartmentId = model.DepartmentId;
                    result.GenderId = model.GendeId;
                    result.ID = model.EmpID;
                    if (!string.IsNullOrWhiteSpace(result.ProfileImagePath))
                    {
                        imgFile = result.ProfileImagePath;
                    }
                    if (File.Exists(Path.Combine(rootFolder, imgFile)))
                    {
                        File.Delete(Path.Combine(rootFolder, imgFile));
                    }
                    using (FileStream filestream = System.IO.File.Create(Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + fileName))
                    {
                        model.files.CopyTo(filestream);
                        filestream.Flush();
                    }
                }
                result.ProfileImagePath = fileName;
                _context.SaveChanges();
                return new ApiResponse(200, true, null, "Employee Updated Succesfully", null);

                return null;
            }
            catch (Exception ex)
            {
                return new ApiResponse(500, false, new List<string> { ex.Message }, null, null);
            }
        }


    }
}

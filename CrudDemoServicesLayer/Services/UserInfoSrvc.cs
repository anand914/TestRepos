using CrudDemoDataAccessLayer.Data;
using CrudDemoDataAccessLayer.Models;
using CrudDemoDataAccessLayer.ViewModel;
using CrudDemoServicesLayer.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoServicesLayer.Services
{
    public class UserInfoSrvc :ICommonSrvc
    {
        private readonly EmployeeContext _context;
        public UserInfoSrvc(EmployeeContext context)
        {
            _context = context; 
        }

        public async Task<ApiResponse> AddUserInfo(UserInfoVM model)
        {
            try
            {
                UserInformations obj = new UserInformations();
                obj.Email = model.Email;
                obj.Password = model.Password;
                obj.Roles = model.Roles;
                _context.UserInformations.Add(obj);
                _context.SaveChanges();
                return new ApiResponse(200, true, null, "Added info Sucessfully", null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}

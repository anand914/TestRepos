using CrudDemoDataAccessLayer.Models;
using CrudDemoDataAccessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoServicesLayer.Services.IService
{
    public interface IEmployee
    {
        Task<ApiResponse> GetAllemployees();
        Task<ApiResponse> Login(LoginVM model);
        Task<ApiResponse> GetEmployeeById(int id);
        Task<ApiResponse> SaveEmployee(AddEmployeeVM model);
        Task<ApiResponse> UpdateemployeeInfo(UpdateEmployeeVM model);
        Task<ApiResponse> DeleteEmployee(int Id);
        Task<ApiResponse> ChangePassword(ChangePasswordVM model);
        //void SendEmail(EmailModelVM model);
    }
}

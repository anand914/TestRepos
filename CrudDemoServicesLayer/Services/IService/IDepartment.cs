using CrudDemoDataAccessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoServicesLayer.Services.IService
{
    public interface IDepartment
    {
        Task<ApiResponse> GetDepartments();
        Task<ApiResponse> GetDepartmentbyId(int id);
        Task<ApiResponse> GetAllEmployeebydepId(int depid);
        Task<ApiResponse> AddOrUpdateDepartment(DepartmentVM model);


    }
}

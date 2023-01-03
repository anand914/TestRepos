using CrudDemoDataAccessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoServicesLayer.Services.IService
{
    public interface IGender
    {
        Task<ApiResponse> AddOrUpdateGender(GenderVM model);
        Task<ApiResponse> GetAllGenderInfo();
    }
}

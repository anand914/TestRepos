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
    public class GenderSrvc : IGender
    {
        private readonly EmployeeContext _context;
        public GenderSrvc(EmployeeContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse>AddOrUpdateGender(GenderVM model)
        {
            try
            {
                if(model.ID==0)
                {
                    Gender obj = new Gender();
                    obj.desc = model.desc;
                    _context.Genders.Add(obj);
                    _context.SaveChanges();
                    return new ApiResponse(200, true, null, "Add Successfully", null);
                }
                else
                {
                    var result = _context.Genders.Where(g => g.ID == model.ID).SingleOrDefault();
                    if(result!=null)
                    {
                        result.ID = model.ID;
                        result.desc = model.desc;
                        _context.SaveChanges();
                        return new ApiResponse(200, true, null, "Updated Sucessfully", null);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse> GetAllGenderInfo()
        {
            try
            {
                var result = _context.Genders.ToList();
                if(result.Count>0)
                {
                    return new ApiResponse(200, true, null, result, null);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

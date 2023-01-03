using CrudDemoDataAccessLayer.Data;
using CrudDemoDataAccessLayer.Models;
using CrudDemoDataAccessLayer.ViewModel;
using CrudDemoServicesLayer.Services.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoServicesLayer.Services
{
    public class Departmentsrvc : IDepartment
    {
        private readonly EmployeeContext _context;
        public Departmentsrvc(EmployeeContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> AddOrUpdateDepartment(DepartmentVM model)
        {
            try
            {
                if (model.Id == 0)
                {
                    Department obj = new Department();
                    obj.DepName = model.Depname;
                    _context.Departments.Add(obj);
                    _context.SaveChanges();
                    return new ApiResponse(200, true, null, "Department Add Successfully", null);
                }
                else
                {
                    var data = _context.Departments.Where(d => d.ID == model.Id).FirstOrDefault();
                    if (data != null)
                    {
                        data.DepName = model.Depname;
                        data.ID = model.Id;
                        _context.SaveChanges();
                    }
                    return new ApiResponse(200, true, null, "Department Updated Successfully", null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse> GetAllEmployeebydepId(int depid)
        {
            try
            {
                List<AllinfoByDepId> result = (from emp in _context.Employees
                                               join
                 dep in _context.Departments on emp.DepartmentId equals dep.ID
                                               join gen in _context.Genders on emp.GenderId equals gen.ID
                                               where dep.ID == depid
                                               select new AllinfoByDepId
                                               {
                                                   EmpName = emp.EmpName,
                                                   Address = emp.Address,
                                                   contact = emp.Contact,
                                                   desc = gen.desc,
                                                   DepName = dep.DepName,
                                                   EmpId = emp.ID,
                                                   Id = dep.ID,
                                                   genderId = gen.ID
                                               }).ToList();
                if (result != null)
                {
                    return new ApiResponse(200, true, null, result, null);
                }
                return new ApiResponse(201, true, null, null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse> GetDepartmentbyId(int id)
        {
            try
            {
                var result = _context.Departments.Where(d => d.ID == id).FirstOrDefault();
                if (result != null)
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

        public async Task<ApiResponse> GetDepartments()
        {
            try
            {
                var result = _context.Departments.ToList();
                if (result.Count > 0)
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

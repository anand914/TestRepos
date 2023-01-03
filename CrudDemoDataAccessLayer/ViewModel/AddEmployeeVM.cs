using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoDataAccessLayer.ViewModel
{
    public class AddEmployeeVM
    {
        public int Id { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public int GendeId { get; set; }
        public int DepartmentId { get; set; }
        public string ProfileImagePath { get; set; }
        [NotMapped]
        public IFormFile files { get; set; }

    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoDataAccessLayer.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        public int DepartmentId { get; set; }
        public int GenderId { get; set; }
        public string ProfileImagePath { get; set; }

        [NotMapped]
        public IFormFile files { get; set; }
        public virtual Department Department { get; set; }
    }
}

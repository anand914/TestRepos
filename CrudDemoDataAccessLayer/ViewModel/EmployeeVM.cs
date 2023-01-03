using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoDataAccessLayer.ViewModel
{
    public class EmployeeVM
    {
        public int ID { get; set; }
        public string EmpName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public int GendeId { get; set; }
        public int DepartmentId { get; set; }
        public string ProfileImagePath { get; set; }
        public string DepName { get; set; }
        public string desc { get; set; }
    }
}

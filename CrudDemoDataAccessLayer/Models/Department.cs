using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoDataAccessLayer.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        public string DepName { get; set; }
    }
}

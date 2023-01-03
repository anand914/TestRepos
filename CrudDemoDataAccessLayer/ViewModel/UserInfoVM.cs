using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoDataAccessLayer.ViewModel
{
    public class UserInfoVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string ProfileImagePath { get; set; }
    }
}

﻿using CrudDemoDataAccessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudDemoServicesLayer.Services.IService
{
    public interface ICommonSrvc
    {
        Task<ApiResponse> AddUserInfo(UserInfoVM model);
    }
}

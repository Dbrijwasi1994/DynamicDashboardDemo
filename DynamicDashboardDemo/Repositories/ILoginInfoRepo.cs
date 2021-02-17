using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboardDemo.Repositories
{
    public interface ILoginInfoRepo
    {
        bool IsValidUser(string username, string passsword);
    }
}

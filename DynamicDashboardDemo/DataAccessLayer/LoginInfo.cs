using DynamicDashboardDemo.Models;
using DynamicDashboardDemo.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboardDemo.DataAccessLayer
{
    public class LoginInfo : ILoginInfoRepo
    {
        private readonly DynamicDashboardDemoContext _context;
        private readonly ILogger<LoginInfo> _logger;

        public LoginInfo(DynamicDashboardDemoContext context, ILogger<LoginInfo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool IsValidUser(string username, string passsword)
        {
            try
            {
                return _context.UserDetails.Any(x => x.Username == username && x.Password == passsword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}

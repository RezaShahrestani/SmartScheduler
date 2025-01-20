using SmartScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartScheduler.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(User user);
        Task<User?> Authenticate(string username, string password);
    }
}

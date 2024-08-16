using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Exists(string email);
        Task<string> Create(Account account);
        Task<Account> FindAccount(string email);
    }
}

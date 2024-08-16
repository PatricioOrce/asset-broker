using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AccountService(AccountDbContext _context) : IAccountService
    {
        public async Task<string> Create(Account account)
        {
            try
            {
                await _context.Accounts.AddAsync(account);
                return "Cuenta creada con exito.";

            }
            catch (Exception ex)
            {
                throw new Exception($"Hubo un error registrando la cuenta: {ex.Message}");
            }
            finally
            {
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(string email)
        {
            return await _context.Accounts.AnyAsync(a => a.Email == email);
        }

        public async Task<Account> FindAccount(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
        }


    }
}

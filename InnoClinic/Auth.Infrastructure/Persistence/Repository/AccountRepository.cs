using Auth.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Repository
{
    public class AccountRepository(AuthDbContext dbContext) : IAccountRepository
    {
        public async Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default)
        {
            var newAccount = await dbContext.Accounts.AddAsync(account, cancellationToken);
            return newAccount.Entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var account = await dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == id);
            if (account != null)
            {
                dbContext.Entry(account).State = EntityState.Deleted;
            }
        }

        public async Task<Account> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await dbContext.Accounts.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Account> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await dbContext.Accounts.FirstOrDefaultAsync(x => x.UserName == name);
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            var account = await dbContext.Accounts.FirstOrDefaultAsync(x => x.Email == email);
            return account != null;
        }
    }
}

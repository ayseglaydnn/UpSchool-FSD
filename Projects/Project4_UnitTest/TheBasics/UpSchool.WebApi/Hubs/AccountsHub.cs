using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using UpSchool.Domain.Utilities;
using UpSchool.Persistence.EntityFramework.Contexts;

namespace UpSchool.WebApi.Hubs
{
    public class AccountsHub:Hub
    {
        private readonly UpStorageDbContext _dbContext;
        public AccountsHub(UpStorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> DeleteAsync(Guid accountId)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(XmlConfigurationExtensions => XmlConfigurationExtensions.Id == accountId);

            if (account == null) return false;

            _dbContext.Accounts.Remove(account);

            await _dbContext.SaveChangesAsync();

            await Clients.AllExcept(Context.ConnectionId).SendAsync(SignalRMethodHeys.Accounts.Deleted,accountId);

            return true;
        }
    }
}

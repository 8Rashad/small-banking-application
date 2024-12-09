using BankApplication.Data;
using BankApplication.Entity;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Repository.service
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<CustomerEntity> _users;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(ApplicationDbContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _users = _context.Customers;
            _logger = logger;
        }

        public async Task<CustomerEntity> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching user with ID {UserId}", id);
            var user = await _users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", id);
            }
            return user;
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all users");
            var users = await _users.ToListAsync();
            return users;
        }

        public async Task AddAsync(CustomerEntity user)
        {
            _logger.LogInformation("Adding a new user with Name {UserName}", user.FullName);
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User with Name {UserName} added successfully", user.FullName);

        }

        public async Task UpdateAsync(CustomerEntity user)
        {
            _logger.LogInformation("Updating user with ID {UserId}", user.Id);
            _users.Update(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User with ID {UserId} updated successfully", user.Id);

        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting user with ID {UserId}", id);
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _users.Remove(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User with ID {UserId} deleted successfully", id);
            }
            else
            {
                _logger.LogWarning("User with ID {UserId} not found for deletion", id);
            }
        }
    }
}


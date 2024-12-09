using BankApplication.Entity;

namespace BankApplication.Service.CustomerService
{
    public interface ICustomerService
    {
        Task<CustomerEntity> GetUserByIdAsync(int id);
        Task<IEnumerable<CustomerEntity>> GetAllUsersAsync();
        Task AddUserAsync(CustomerEntity user);
        Task UpdateUserAsync(CustomerEntity user);
        Task DeleteUserAsync(int id);
    }
}

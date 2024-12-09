using BankApplication.Entity;

namespace BankApplication.Repository.service
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity> GetByIdAsync(int id);
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task AddAsync(CustomerEntity user);
        Task UpdateAsync(CustomerEntity user);
        Task DeleteAsync(int id);
    }
}

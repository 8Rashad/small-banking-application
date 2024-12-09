using BankApplication.Entity;
using BankApplication.Repository.service;

namespace BankApplication.Service.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _userRepository;

        public CustomerService(ICustomerRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CustomerEntity> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task AddUserAsync(CustomerEntity user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(CustomerEntity user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

    }
}

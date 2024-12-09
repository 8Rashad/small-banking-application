using BankApplication.Entity;
using BankApplication.Repository.service;
using BankApplication.Service.CustomerService;
using Moq;

namespace TestProject1.Service
{
    public class UserServiceTest
    {
        private readonly Mock<ICustomerRepository> _mockUserRepository;
        private readonly CustomerService _userService;

        public UserServiceTest()
        {
            _mockUserRepository = new Mock<ICustomerRepository>();
            _userService = new CustomerService(_mockUserRepository.Object);
        } 

        [Fact]
        public async Task TestGetUserByIdAsync()
        {
            //Arrange
            var userId = 1;
            var expectedUser = new CustomerEntity {Id = userId, FullName = "Rashad Suleymanov", Email = "suleymanovv.rashad@gmail.com", 
                                         PhoneNumber = "+994503024065", Address = "Y.Aliyev 2/2a", Balance = 8500, FINCode = "6JGW1MH" };
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(expectedUser);

            //Act
            var result = await _userService.GetUserByIdAsync(userId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser, result);
            Assert.Equal(expectedUser.FullName, result.FullName);
            _mockUserRepository.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task TestGetAllUsersAsync()
        {
            // Arrange
            var numberOfUsers = 10;
            _mockUserRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(Enumerable.Range(1, numberOfUsers).Select(id => new CustomerEntity
                {
                    Id = id,
                    FullName = $"User {id}",
                    Email = $"user{id}@example.com",
                    PhoneNumber = "+994503024065", 
                    Address = $"Address {id}",
                    Balance = 8500, 
                    FINCode = $"6JGW{id}MH" 
                }).ToList());

            // Act
            var result = await _userService.GetAllUsersAsync();
            var count = result.Count();

            // Assert
            Assert.Equal(numberOfUsers, count); 
            _mockUserRepository.Verify(repo => repo.GetAllAsync(), Times.Once); 
        }


        [Fact]
        public async Task TestAddUserAsync()
        {

            //Arrange
            var newUser = new CustomerEntity
            {
                Id = 1,
                FullName = "Namiq Suleymanov",
                Email = "namiq.suleymanov@gmail.com",
                PhoneNumber = "+994506773550",
                Address = "Y.Aliyev 2/2a",
                Balance = 15000,
                FINCode = "8UTF9GF"
            };

            //Act
            await _userService.AddUserAsync(newUser);

            //Assert
            _mockUserRepository.Verify(repo => repo.AddAsync(newUser), Times.Once);
        }

        [Fact]
        public async Task TestUpdateUserAsync()
        {
            // Arrange
            var existingUser = new CustomerEntity
            {
                Id = 1,
                FullName = "Rashad Suleymanov",
                Email = "suleymanovv.rashad@gmail.com",
                PhoneNumber = "+994503024065",
                Address = "Y.Aliyev 2/2a",
                Balance = 8500,
                FINCode = "6JGW1MH"
            };

            // Act
            await _userService.UpdateUserAsync(existingUser);

            // Assert
            _mockUserRepository.Verify(repo => repo.UpdateAsync(existingUser), Times.Once);
        }

        [Fact]
        public async Task TestDeleteUserAsync()
        {
            // Arrange
            var userId = 1;

            // Act
            await _userService.DeleteUserAsync(userId);

            // Assert
            _mockUserRepository.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }
    }
}
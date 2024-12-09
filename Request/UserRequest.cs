namespace BankApplication.Request
{
    public class UserRequest
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public decimal Balance { get; set; } = 50.00m;
        public required string FINCode { get; set; }
    }
}

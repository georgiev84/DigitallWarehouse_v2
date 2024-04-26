namespace UserManagementService.Domain.Entities.Users;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
    public Guid BasketId { get; set; }
    //public Basket? Basket { get; set; }
    //public ICollection<Order> Orders { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
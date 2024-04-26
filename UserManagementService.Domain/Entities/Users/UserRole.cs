namespace UserManagementService.Domain.Entities.Users;

public class UserRole
{
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }
    public Role Role { get; set; }
    public User User { get; set; }
}
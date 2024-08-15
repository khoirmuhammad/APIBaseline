namespace APIBaseline.Models
{
	public class UserRole
	{
		public int Id { get; set; }
		public string RoleName { get; set; } = string.Empty;
		public int UserId { get; set; }
		public User User { get; set; }
	}
}

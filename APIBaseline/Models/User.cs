namespace APIBaseline.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
	}
}

namespace APIBaseline.DTOs
{
	public class UserRoleDTO
	{
		public string Name { get; set; } = string.Empty;
		public List<string> Roles { get; set; } = new List<string>();
	}
}

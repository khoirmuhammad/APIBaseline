namespace APIBaseline.Repositories
{
	public interface IUnitOfWork
	{
		IUserRepository Users { get; }
		IUserRoleRepository UserRoles { get; }
		Task<int> CompleteAsync();
		Task BeginTransactionAsync();
		Task CommitAsync();
		Task RollbackAsync();
	}
}

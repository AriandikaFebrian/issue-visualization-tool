public interface IRecentProjectRepository
{
    Task<List<RecentProjectDto>> GetRecentProjectsByNRPAsync(string nrp);
    Task AddOrUpdateRecentProjectAsync(string nrp, string projectCode);
}

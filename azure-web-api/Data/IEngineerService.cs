namespace azure_web_api.Data
{
    public interface IEngineerService
    {
        Task<Engineer> AddEngineer(Engineer engineer);
        Task<Engineer> UpdateEngineer(Engineer engineer);
        Task<string> DeleteEngineer(string? id, string? partitionKey);
        Task<List<Engineer>> GetEngineerDetails();
        Task<Engineer> GetEngineerDetailsById(string? id, string? partitionKey);
    }
}

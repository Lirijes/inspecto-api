using inspecto_API.Models;

namespace inspecto_API.Services.Interfaces
{
    public interface ICasesService
    {
        Task<List<Case>> GetAllAsync();
        Task<Case?> GetByIdAsync(Guid id);
    }
}

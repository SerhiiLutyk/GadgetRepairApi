using GadgetRepairApi.Models;

namespace GadgetRepairApi.Repositories;

public interface IGadgetRepository
{
    Task<IReadOnlyList<Gadget>> GetAllAsync();
    Task<Gadget?> GetByIdAsync(int id);
    Task<Gadget> AddAsync(Gadget gadget);
    Task<Gadget?> UpdateAsync(Gadget gadget);
    Task<bool> DeleteAsync(int id);
}

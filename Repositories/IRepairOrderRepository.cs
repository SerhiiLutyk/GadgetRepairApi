using GadgetRepairApi.Models;

namespace GadgetRepairApi.Repositories;

public interface IRepairOrderRepository
{
    Task<IReadOnlyList<RepairOrder>> GetAllAsync();
    Task<RepairOrder?> GetByIdAsync(int id);
    Task<RepairOrder> AddAsync(RepairOrder order);
    Task<RepairOrder?> UpdateAsync(RepairOrder order);
    Task<bool> DeleteAsync(int id);
}

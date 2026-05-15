using GadgetRepairApi.Data;
using GadgetRepairApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetRepairApi.Repositories;

public class RepairOrderRepository(GadgetRepairDbContext context) : IRepairOrderRepository
{
    public async Task<IReadOnlyList<RepairOrder>> GetAllAsync() =>
        await context.RepairOrders
            .Include(r => r.Gadget)
            .AsNoTracking()
            .ToListAsync();

    public async Task<RepairOrder?> GetByIdAsync(int id) =>
        await context.RepairOrders
            .Include(r => r.Gadget)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<RepairOrder> AddAsync(RepairOrder order)
    {
        context.RepairOrders.Add(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task<RepairOrder?> UpdateAsync(RepairOrder order)
    {
        var existing = await context.RepairOrders.FindAsync(order.Id);
        if (existing is null)
            return null;

        existing.GadgetId = order.GadgetId;
        existing.IssueDescription = order.IssueDescription;
        existing.Status = order.Status;
        existing.Price = order.Price;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await context.RepairOrders.FindAsync(id);
        if (order is null)
            return false;

        context.RepairOrders.Remove(order);
        await context.SaveChangesAsync();
        return true;
    }
}

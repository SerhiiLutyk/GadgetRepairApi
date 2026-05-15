using GadgetRepairApi.Data;
using GadgetRepairApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetRepairApi.Repositories;

public class GadgetRepository(GadgetRepairDbContext context) : IGadgetRepository
{
    public async Task<IReadOnlyList<Gadget>> GetAllAsync() =>
        await context.Gadgets.AsNoTracking().ToListAsync();

    public async Task<Gadget?> GetByIdAsync(int id) =>
        await context.Gadgets
            .Include(g => g.RepairOrders)
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);

    public async Task<Gadget> AddAsync(Gadget gadget)
    {
        context.Gadgets.Add(gadget);
        await context.SaveChangesAsync();
        return gadget;
    }

    public async Task<Gadget?> UpdateAsync(Gadget gadget)
    {
        var existing = await context.Gadgets.FindAsync(gadget.Id);
        if (existing is null)
            return null;

        existing.Brand = gadget.Brand;
        existing.Model = gadget.Model;
        existing.OwnerName = gadget.OwnerName;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var gadget = await context.Gadgets.FindAsync(id);
        if (gadget is null)
            return false;

        context.Gadgets.Remove(gadget);
        await context.SaveChangesAsync();
        return true;
    }
}

using GadgetRepairApi.Models;
using GadgetRepairApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GadgetRepairApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepairOrdersController(IRepairOrderRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RepairOrder>>> GetAll() =>
        Ok(await repository.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RepairOrder>> GetById(int id)
    {
        var order = await repository.GetByIdAsync(id);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<RepairOrder>> Create(RepairOrder order)
    {
        order.Id = 0;
        var created = await repository.AddAsync(order);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<RepairOrder>> Update(int id, RepairOrder order)
    {
        if (id != order.Id)
            return BadRequest("Id in URL must match Id in body.");

        var updated = await repository.UpdateAsync(order);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await repository.DeleteAsync(id) ? NoContent() : NotFound();
}

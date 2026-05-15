using GadgetRepairApi.Models;
using GadgetRepairApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GadgetRepairApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GadgetsController(IGadgetRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Gadget>>> GetAll() =>
        Ok(await repository.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Gadget>> GetById(int id)
    {
        var gadget = await repository.GetByIdAsync(id);
        return gadget is null ? NotFound() : Ok(gadget);
    }

    [HttpPost]
    public async Task<ActionResult<Gadget>> Create(Gadget gadget)
    {
        gadget.Id = 0;
        var created = await repository.AddAsync(gadget);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Gadget>> Update(int id, Gadget gadget)
    {
        if (id != gadget.Id)
            return BadRequest("Id in URL must match Id in body.");

        var updated = await repository.UpdateAsync(gadget);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await repository.DeleteAsync(id) ? NoContent() : NotFound();
}

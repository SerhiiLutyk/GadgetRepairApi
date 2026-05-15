namespace GadgetRepairApi.Models;

public class Gadget
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;

    public ICollection<RepairOrder> RepairOrders { get; set; } = [];
}

namespace GadgetRepairApi.Models;

public class RepairOrder
{
    public int Id { get; set; }
    public int GadgetId { get; set; }
    public string IssueDescription { get; set; } = string.Empty;
    public RepairOrderStatus Status { get; set; }
    public decimal Price { get; set; }

    public Gadget? Gadget { get; set; }
}

namespace CRUD_TaskInHome26.Models;

public class Order
{
    public int Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public decimal Total { get; set; }

    public DateTime CreatedAt { get; set; }
}

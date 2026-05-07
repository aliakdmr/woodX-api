namespace WoodX.API.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; } = "pending";
    public ShippingAddress ShippingAddress { get; set; } = new();
    public string PaymentMethod { get; set; } = "";
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<OrderItem> Items { get; set; } = new();

    public User? User { get; set; }
}

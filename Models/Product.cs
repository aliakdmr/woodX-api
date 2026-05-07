namespace WoodX.API.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public int Stock { get; set; }
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public bool Featured { get; set; }
    public string Image { get; set; } = "";
    public string Description { get; set; } = "";
    public List<string> Tags { get; set; } = new();
}

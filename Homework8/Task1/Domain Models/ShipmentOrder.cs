namespace Task1.Domain_Models;

public class ShipmentOrder
{
    public string Id { get; set; }
    public double Weight { get; set; }
    public bool Fragile { get; set; }
    public bool Express { get; set; }
    public string Zone { get; set; } 
}
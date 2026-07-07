namespace Task1.Domain_Models;

public class Candidate
{
    public string Id { get; set; }
    public int Experience { get; set; }
    public decimal ExpectedSalary { get; set; }
    public bool Remote { get; set; }
    public List<string> Skills { get; set; }
}
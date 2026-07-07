namespace Task1.Domain_Models;

public class LoanApplication
{
    public string Id { get; set; }
    public int CreditScore { get; set; }
    public decimal Income { get; set; }
    public bool IsEmployed { get; set; }
    public bool HasCollateral { get; set; }
    public bool HasBankruptcy { get; set; }
}
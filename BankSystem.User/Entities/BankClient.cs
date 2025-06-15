using BankSystem.Constants;

namespace BankSystem.Entities;
public class BankClient : Person
{
    public Mode Mode { get; set; }
    public string AccountNumber { get; set; }
    public string PinCode { get; set; }
    public float AccountBalance { get; set; }
    public bool IsDeleted { get; set; }
}



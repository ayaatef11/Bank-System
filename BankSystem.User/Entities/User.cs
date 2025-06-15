using BankSystem.Constants;
namespace BankSystem.Entities;
public class User
{
    public Mode Mode {  get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Permissions { get; set; }
    public bool MarkedForDelete { get; set; }
    public bool IsDeleted {  get; set; }    
}
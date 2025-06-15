using BankSystem.Constants;
using BankSystem.Entities;

namespace BankSystem.Services;

public class BankClientService
{
    private const string FilePath = "Clients.txt";
    private const string Separator = "#//#";

    public BankClient Find(string accountNumber)
    {
        var clients = LoadClientsDataFromFile();
        return clients.FirstOrDefault(c => c.AccountNumber == accountNumber);
    }

    public BankClient Find(string accountNumber, string pinCode)
    {
        var clients = LoadClientsDataFromFile();
        return clients.FirstOrDefault(c => c.AccountNumber == accountNumber && c.PinCode == pinCode);
    }


    public bool Delete(BankClient client)
    {
        var clients = LoadClientsDataFromFile();
        var clientToDelete = clients.FirstOrDefault(c => c.AccountNumber == client.AccountNumber);

        if (clientToDelete != null)
        {
            clientToDelete.IsDeleted = true;
            SaveClientsDataToFile(clients);
            return true;
        }

        return false;
    }

    public List<BankClient> GetAllClients()
    {
        return LoadClientsDataFromFile()
            .Where(c => !c.IsDeleted)
            .ToList();
    }

    public double GetTotalBalances()
    {
        return GetAllClients().Sum(c => c.AccountBalance);
    }


    private List<BankClient> LoadClientsDataFromFile()
    {
        var clients = new List<BankClient>();

        if (File.Exists(FilePath))
        {
        }

        return clients;
    }

    private void SaveClientsDataToFile(List<BankClient> clients)
    {
        var lines = clients
            .Where(c => !c.IsDeleted)
            .Select(ConvertClientObjectToLine);

        File.WriteAllLines(FilePath, lines);
    }


    private string ConvertClientObjectToLine(BankClient client)
    {
        return string.Join(Separator,
            client.FirstName,
            client.LastName,
            client.Email,
            client.Phone,
            client.AccountNumber,
            client.PinCode,
            client.AccountBalance.ToString());
    }

    public void UpdateClient(BankClient client)
    {
        var clients = LoadClientsDataFromFile();
        var existingClient = clients.FirstOrDefault(c => c.AccountNumber == client.AccountNumber);

        if (existingClient != null)
        {
            clients[clients.IndexOf(existingClient)] = client;
            SaveClientsDataToFile(clients);
        }
    }

    public void AddNewClient(BankClient client)
    {
        File.AppendAllText(FilePath, ConvertClientObjectToLine(client) + Environment.NewLine);
    }

}
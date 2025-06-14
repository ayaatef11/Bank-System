namespace BankSystem.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankSystem.Constants;
using BankSystem.Entities;
using global::BankSystem.Entities;

namespace BankSystem.Services
{
    public class BankClientService
    {
        private const string FilePath = "Clients.txt";
        private const string Separator = "#//#";

        public BankClient Find(string accountNumber)
        {
            var clients = LoadClientsDataFromFile();
            return clients.FirstOrDefault(c => c.AccountNumber == accountNumber)
                   ?? GetEmptyClientObject();
        }

        public BankClient Find(string accountNumber, string pinCode)
        {
            var clients = LoadClientsDataFromFile();
            return clients.FirstOrDefault(c => c.AccountNumber == accountNumber && c.PinCode == pinCode)
                   ?? GetEmptyClientObject();
        }

        public SaveResult Save(BankClient client)
        {
            if (client.IsEmpty)
            {
                return SaveResult.FailedEmptyObject;
            }

            switch (client.Mode)
            {
                case Mode.UpdateMode:
                    UpdateClient(client);
                    return SaveResult.Succeeded;

                case Mode.AddNewMode:
                    if (IsClientExist(client.AccountNumber))
                    {
                        return SaveResult.FailedAccountNumberExists;
                    }
                    AddNewClient(client);
                    client.Mode = Mode.UpdateMode;
                    return SaveResult.Succeeded;

                default:
                    return SaveResult.FailedEmptyObject;
            }
        }

        public bool Delete(BankClient client)
        {
            var clients = LoadClientsDataFromFile();
            var clientToDelete = clients.FirstOrDefault(c => c.AccountNumber == client.AccountNumber);

            if (clientToDelete != null)
            {
                clientToDelete.MarkedForDelete = true;
                SaveClientsDataToFile(clients);
                return true;
            }

            return false;
        }

        public List<BankClient> GetAllClients()
        {
            return LoadClientsDataFromFile()
                .Where(c => !c.MarkedForDelete)
                .ToList();
        }

        public double GetTotalBalances()
        {
            return GetAllClients().Sum(c => c.AccountBalance);
        }

        public BankClient CreateNewClient(string accountNumber)
        {
            return new BankClient(
                mode: Mode.AddNewMode,
                firstName: "",
                lastName: "",
                email: "",
                phone: "",
                accountNumber: accountNumber,
                pinCode: "",
                accountBalance: 0);
        }

        private List<BankClient> LoadClientsDataFromFile()
        {
            var clients = new List<BankClient>();

            if (File.Exists(FilePath))
            {
                foreach (var line in File.ReadAllLines(FilePath))
                {
                    clients.Add(ConvertLineToClientObject(line));
                }
            }

            return clients;
        }

        private void SaveClientsDataToFile(List<BankClient> clients)
        {
            var lines = clients
                .Where(c => !c.MarkedForDelete)
                .Select(ConvertClientObjectToLine);

            File.WriteAllLines(FilePath, lines);
        }

        private BankClient ConvertLineToClientObject(string line)
        {
            var clientData = line.Split(new[] { Separator }, StringSplitOptions.None);

            return new BankClient(
                mode: Mode.UpdateMode,
                firstName: clientData[0],
                lastName: clientData[1],
                email: clientData[2],
                phone: clientData[3],
                accountNumber: clientData[4],
                pinCode: clientData[5],
                accountBalance: double.Parse(clientData[6]));
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

        private void UpdateClient(BankClient client)
        {
            var clients = LoadClientsDataFromFile();
            var existingClient = clients.FirstOrDefault(c => c.AccountNumber == client.AccountNumber);

            if (existingClient != null)
            {
                clients[clients.IndexOf(existingClient)] = client;
                SaveClientsDataToFile(clients);
            }
        }

        private void AddNewClient(BankClient client)
        {
            File.AppendAllText(FilePath, ConvertClientObjectToLine(client) + Environment.NewLine);
        }

        private bool IsClientExist(string accountNumber)
        {
            return !Find(accountNumber).IsEmpty;
        }

        private BankClient GetEmptyClientObject()
        {
            return new BankClient(
                mode: Mode.EmptyMode,
                firstName: "",
                lastName: "",
                email: "",
                phone: "",
                accountNumber: "",
                pinCode: "",
                accountBalance: 0);
        }
    }

    public enum SaveResult
    {
        FailedEmptyObject = 0,
        Succeeded = 1,
        FailedAccountNumberExists = 2
    }

    public enum Mode
    {
        EmptyMode = 0,
        UpdateMode = 1,
        AddNewMode = 2
    }
}
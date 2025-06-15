using BankSystem.Entities;
using BankSystem.Services;
using Microsoft.AspNetCore.Mvc;
namespace BankSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BankClientController(BankClientService _bankClientService) : ControllerBase
{

    [HttpGet]
    public ActionResult<IEnumerable<BankClient>> GetAllClients()
    {
        var clients = _bankClientService.GetAllClients();
        return Ok(clients);
    }

    // GET: api/BankClients/5
    [HttpGet("{accountNumber}")]
    public ActionResult<BankClient> GetClient(string accountNumber)
    {
        var client = _bankClientService.Find(accountNumber);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    // GET: api/BankClients/validate/12345/0000
    [HttpGet("validate/{accountNumber}/{pinCode}")]
    public ActionResult<BankClient> ValidateClient(string accountNumber, string pinCode)
    {
        var client = _bankClientService.Find(accountNumber, pinCode);

        if (client == null)
        {
            return Unauthorized("Invalid account number or PIN code");
        }

        return Ok(client);
    }

    // POST: api/BankClients
    [HttpPost]
    public ActionResult<BankClient> CreateClient([FromBody] BankClient client)
    {
        if (string.IsNullOrEmpty(client.AccountNumber))
            {
            return BadRequest("Account number is required");
        }

        var existingClient = _bankClientService.Find(client.AccountNumber);
        if (existingClient != null)
        {
            return Conflict("Account number already exists");
        }

        _bankClientService.AddNewClient(client);
        return CreatedAtAction(nameof(GetClient), new { accountNumber = client.AccountNumber }, client);
    }

    // PUT: api/BankClients/5
    [HttpPut("{accountNumber}")]
    public IActionResult UpdateClient(string accountNumber, [FromBody] BankClient client)
    {
        if (accountNumber != client.AccountNumber)
        {
            return BadRequest("Account number mismatch");
        }

        var existingClient = _bankClientService.Find(accountNumber);
        if (existingClient == null)
        {
            return NotFound();
        }

        _bankClientService.UpdateClient(client);
        return NoContent();
    }

    // DELETE: api/BankClients/5
    [HttpDelete("{accountNumber}")]
    public IActionResult DeleteClient(string accountNumber)
    {
        var client = _bankClientService.Find(accountNumber);
        if (client == null)
        {
            return NotFound();
        }

        _bankClientService.Delete(client);
        return NoContent();
    }

    // GET: api/BankClients/total-balance
    [HttpGet("total-balance")]
    public ActionResult<decimal> GetTotalBalance()
    {
        var totalBalance = _bankClientService.GetTotalBalances();
        return Ok(totalBalance);
    }
}


using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerStore _store;

    public CustomersController(CustomerStore store)
    {
        _store = store;
    }

    [HttpPost]
    public IActionResult Post([FromBody] List<Customer> customers)
    {
        var errors = new List<string>();

        foreach (var customer in customers)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName) ||
                string.IsNullOrWhiteSpace(customer.LastName) ||
                customer.Id <= 0)
            {
                errors.Add($"Customer ID {customer.Id} has missing fields.");
                continue;
            }

            if (customer.Age < 18)
            {
                errors.Add($"Customer ID {customer.Id} is underage.");
                continue;
            }

            if (_store.IdExists(customer.Id))
            {
                errors.Add($"Customer ID {customer.Id} already exists.");
                continue;
            }
        }

        if (errors.Count > 0)
            return BadRequest(errors);

        _store.AddCustomers(customers);
        return Ok();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_store.GetAll());
    }
}

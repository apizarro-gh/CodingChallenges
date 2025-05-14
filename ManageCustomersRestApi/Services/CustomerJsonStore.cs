using System.Text.Json;

public class CustomerStore
{
    private readonly List<Customer> _customers = new();
    private readonly string _filePath = "customers.json";
    private readonly object _lock = new();

    public CustomerStore()
    {
        LoadFromFile();
    }

    public IEnumerable<Customer> GetAll() => _customers;

    public bool IdExists(int id) => _customers.Any(c => c.Id == id);

    public void AddCustomers(IEnumerable<Customer> customers)
    {
        lock (_lock)
        {
            foreach (var customer in customers)
            {
                InsertSorted(customer);
            }
            SaveToFile();
        }
    }

    private void InsertSorted(Customer customer)
    {
        for (int i = 0; i < _customers.Count; i++)
        {
            var current = _customers[i];

            int lastNameCmp = string.Compare(customer.LastName, current.LastName, StringComparison.OrdinalIgnoreCase);
            if (lastNameCmp < 0 ||
                (lastNameCmp == 0 && string.Compare(customer.FirstName, current.FirstName, StringComparison.OrdinalIgnoreCase) < 0))
            {
                _customers.Insert(i, customer);
                return;
            }
        }
        _customers.Add(customer); // insert at end
    }

    private void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_customers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    private void LoadFromFile()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            var loaded = JsonSerializer.Deserialize<List<Customer>>(json);
            if (loaded is not null)
                _customers.AddRange(loaded);
        }
    }
}

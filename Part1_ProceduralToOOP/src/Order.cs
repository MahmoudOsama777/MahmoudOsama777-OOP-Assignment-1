namespace Part1_ProceduralToOOP;

public class Order
{
    private const int _maxLines = 20;
    public int Id { get; }
    public Customer Customer { get;}
    public DateTime Date { get; }
    public bool IsPaid { get; private set; }

    private List<OrderLine> _lines = new List<OrderLine>();
    public IReadOnlyList<OrderLine> Lines => _lines;

    public Order(int id, Customer customer, DateTime date)
    {
        Id = id;
        Customer = customer;
        Date = date;
    }

    public void AddLine(Product product, int quantity)
    {
        if (IsPaid)
            throw new ArgumentException("ERROR: cannot change a paid order.");

        if (_lines.Count > _maxLines)
            throw new ArgumentException("ERROR: order has too many lines.");

        if (quantity <= 0)
            throw new ArgumentException("ERROR: quantity must be positive.");

        if (product.Stock < quantity)
            throw new ArgumentException($"ERROR: not enough stock for product #{product.Id}.");

        product.ReduceStock(quantity);
        _lines.Add(new OrderLine(product, quantity));
    }

    public decimal CalculateTotal()
    {
        decimal total = 0m;

        foreach (var line in _lines)
        {
            total += line.Total;
        }

        return Customer.ApplyDiscount(total);
    }

    public void MarkAsPaid()
    {

        if (_lines.Count == 0)
            throw new ArgumentException("ERROR: cannot pay an empty order.");
        

        IsPaid = true;
    }
}

namespace Part1_ProceduralToOOP;

public class Product
{
    public int Id { get;}
    public string Name { get;}

    public decimal Price { get;}

    public int Stock { get; private set; }

    public Product(int id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public bool IsEnoughStock(int quantity)
    {
        return quantity > 0 && Stock >= quantity;
    }

    public void ReduceStock(int quantity)
    {
        if (!IsEnoughStock(quantity))
            throw new ArgumentException($"ERROR: not enough stock for product #{Id}");

        Stock -= quantity;
    }
}


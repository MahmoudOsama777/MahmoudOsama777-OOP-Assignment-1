using System.Xml.Linq;

namespace Part1_ProceduralToOOP;

public class FullSystem
{
    private const int _maxCustomers = 50;
    private const int _maxProducts = 50;
    private const int _maxOrders = 100;

    private List<Customer> _customers = new List<Customer>();
    private List<Product> _products = new List<Product>();
    private List<Order> _orders = new List<Order>();

    public Customer? FindCustomerIndexById(int id)
    {
        return _customers.Find(n => n.Id == id);
    }

    public Product? FindProductIndexById(int id)
    {
        return _products.Find(n => n.Id == id);
    }

    public Order? FindOrderIndexById(int id)
    {
        return _orders.Find(n => n.Id == id);
    }

    public void AddCustomer(int id, string name, string email, string city, bool isVip)
    {
        if (_customers.Count >= _maxCustomers)
        {
            Console.WriteLine("ERROR: customer list is full.");
            return;
        }

        if (FindCustomerIndexById(id) is not null)
        {
            Console.WriteLine($"ERROR: customer id {id} already exists.");
            return;
        }

        _customers.Add(new Customer(id, name, email, city, isVip));
    }

    public void PrintCustomers()
    {
        Console.WriteLine($"\n=== CUSTOMERS ({_customers.Count}) ===");

        foreach(var  customer in _customers) 
            Console.WriteLine($"#{customer.Id}  {customer.Name}  <{customer.Email}>  {customer.City}  vip={(customer.IsVip ? "yes" : "no")}");
    }

    public void AddProduct(int id, string name, decimal price, int stock)
    {
        if (_products.Count >= _maxProducts)
        {
            Console.WriteLine("ERROR: product list is full.");
            return;
        }

        if (FindProductIndexById(id) is not null)
        {
            Console.WriteLine($"ERROR: product id {id} already exists.");
            return;
        }

        _products.Add(new Product(id, name, price, stock));
    }

    public void PrintProducts()
    {
        Console.WriteLine($"\n=== PRODUCTS ({_products.Count}) ===");

        foreach (Product product in _products)
        {
            Console.WriteLine($"#{product.Id}  {product.Name}  price={product.Price:F2}  stock={product.Stock}");
        }
    }

    public void AddOrder(int id,int customerId,DateTime date)
    {
        if (_orders.Count >= _maxOrders)
        {
            Console.WriteLine("ERROR: order list is full.");
            return;
        }

        if (FindOrderIndexById(id) is not null)
        {
            Console.WriteLine($"ERROR: order id {id} already exists.");
            return;
        }

        Customer ?customer = FindCustomerIndexById(customerId);

        if (customer is null)
        {
            Console.WriteLine($"ERROR: customer id {customerId} not found.");
            return;
        }

        _orders.Add(new Order(id, customer, date));
    }

    public void AddLineToOrder(int orderId, int productId, int quantity)
    {
        var order = FindOrderIndexById(orderId);

        if (order is null)
        {
            Console.WriteLine($"ERROR: order id {orderId} not found.");
            return;
        }

        var product = FindProductIndexById(productId);

        if (product is null)
        {
            Console.WriteLine($"ERROR: product id {productId} not found.");
            return;
        }


        order.AddLine(product, quantity);
    }

    public decimal TotalSalesPaidOnly()
    {
        decimal total = 0m;

        foreach (var order in _orders)
            if (order.IsPaid)
                total += order.CalculateTotal();
        
        return total;
    }

    public void MarkOrderPaid(int orderId)
    {
        var order = FindOrderIndexById(orderId);
        if (order is null)
        {
            Console.WriteLine($"ERROR: order id {orderId} not found.");
            return;
        }

        order.MarkAsPaid();
    }

    public void PrintOrder(int orderId)
    {
        var order = FindOrderIndexById(orderId);

        if (order is null)
        {
            Console.WriteLine($"ERROR: order id {orderId} not found.");
            return;
        }

        Console.WriteLine($"\n=== ORDER #{order.Id} ===");
        Console.WriteLine($"Date: { order.Date}");
        Console.WriteLine($"Customer: {order.Customer.Name} (#{ order.Customer.Id})");
        Console.WriteLine($"Paid: {( order.IsPaid ? "yes" : "no")}");
        Console.WriteLine("Lines:");

        foreach (var line in order.Lines) 
            Console.WriteLine($"  - {line.Product.Name}  x{line.Quantity}  @{line.Product.Price:F2}  = {line.Total:F2}");

        Console.WriteLine($"TOTAL: {order.CalculateTotal()}");
    }

    public void PrintAllOrders()
    {
        Console.WriteLine($"\n=== ALL ORDERS ({_orders.Count}) ===");

        for (int i = 0; i < _orders.Count; i++)
            PrintOrder(_orders[i].Id);
    }


    public void SeedSampleData()
    {
        AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
        AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
        AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

        AddProduct(101, "USB Cable", 50.0m, 100);
        AddProduct(102, "Wireless Mouse", 250.0m, 40);
        AddProduct(103, "Mechanical Keyboard", 1200.0m, 15);
        AddProduct(104, "Laptop Stand", 400.0m, 25);
    }

    public void RunDemoScenario()
    {
        AddOrder(1001, 1, new DateTime(2026, 9, 15));
        AddLineToOrder(1001, 101, 2);
        AddLineToOrder(1001, 102, 1);

        AddOrder(1002, 2, new DateTime(2026, 9, 15));
        AddLineToOrder(1002, 103, 1);
        AddLineToOrder(1002, 104, 1);

        AddOrder(1003, 3, new DateTime(2026, 9, 16));
        AddLineToOrder(1003, 101, 5);
    }

    public void PrintMenu()
    {
        Console.WriteLine("\n---------- MENU ----------");
        Console.WriteLine("1) Print customers");
        Console.WriteLine("2) Print products");
        Console.WriteLine("3) Print all orders");
        Console.WriteLine("4) Print one order by id");
        Console.WriteLine("5) Create order");
        Console.WriteLine("6) Add line to order");
        Console.WriteLine("7) Mark order paid");
        Console.WriteLine("8) Show paid sales total");
        Console.WriteLine("0) Exit");
        Console.WriteLine("Choice: ");
    }

    public void RunInteractiveMenu()
    {
        int choice = -1;

        while (choice != 0)
        {
            PrintMenu();
            choice = int.Parse(Console.ReadLine()!);

            switch (choice)
            {
                case 1:
                    PrintCustomers();
                    break;
                case 2:
                    PrintProducts();
                    break;
                case 3:
                    PrintAllOrders();
                    break;
                case 4:
                    Console.Write("Order id: ");
                    int orderId = int.Parse(Console.ReadLine()!);

                    PrintOrder(orderId);
                    break;
                case 5:
                    Console.Write("Order id: ");
                    orderId = int.Parse(Console.ReadLine()!);

                    Console.Write("Customer id: ");
                    int customerId = int.Parse(Console.ReadLine()!);

                    Console.Write("Date (YYYY-MM-DD): ");
                    DateTime date = DateTime.Parse(Console.ReadLine()!);

                    AddOrder(orderId, customerId, date);
                    break;
                case 6:
                    Console.Write("Order id: ");
                    orderId = int.Parse(Console.ReadLine()!);

                    Console.Write("Product id: ");
                    int productId = int.Parse(Console.ReadLine()!);

                    Console.Write("Quantity: ");
                    int quantity = int.Parse(Console.ReadLine()!);

                    AddLineToOrder(orderId, productId, quantity);
                    break;

                case 7:
                    Console.Write("Order id: ");
                    orderId = int.Parse(Console.ReadLine()!);

                    MarkOrderPaid(orderId);
                    break;
                case 8:
                    Console.WriteLine($"Paid sales total: {TotalSalesPaidOnly():N2}");
                    break;
                case 0:
                    Console.WriteLine("Bye.");
                    break;
                default:
                    Console.WriteLine("Unknown choice.");
                    break;
            }

        }

    }
}
namespace Part1_ProceduralToOOP;

public class Program
{


    static void Main(string[] args)
    {
        Console.WriteLine("Object Oriented System with classes");
        Console.WriteLine("Seed sample data, show a demo, then open the menu.");
            
        FullSystem system = new FullSystem();

        system.SeedSampleData(); 
        system.RunDemoScenario(); 

        system.PrintCustomers();
        system.PrintProducts();
        system.PrintAllOrders();

        Console.WriteLine($"\nPaid sales total after demo: {system.TotalSalesPaidOnly():F2}");

        system.RunInteractiveMenu();
    }
}

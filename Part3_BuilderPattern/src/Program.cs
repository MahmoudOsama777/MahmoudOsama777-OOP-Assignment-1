namespace Part3_BuilderPattern;

public class Program
{
    static void Main(string[] args)
    {
        var billingAddress =
            new AddressBuilder("123 Main St", "Cairo", "Egypt")
            .WithState("Cairo")
            .WithZipCode("12345")
            .Build();

        var shippingAddress =
            new AddressBuilder("456 Another St", "Giza", "Egypt")
            .WithState("Giza")
            .WithZipCode("54321")
            .Build();

        var order =
            new OrderBuilder(new DateTime(2026, 10, 7), "Credit Card", "EGP", 2500.00m, 2622.00m)
            .WithTaxAmount(322.00m)
            .WithDiscountAmount(200.00m)
            .Build();

        var invoice =
            new InvoiceComposedBuilder(1, "Mahmoud Osama", "Mahmoud@gmail.com")
            .WithPhone("01270290993")
            .WithBillingAddress(billingAddress)
            .WithShippingAddress(shippingAddress)
            .WithOrder(order)
            .Build();

    }
}

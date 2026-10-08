namespace Part3_BuilderPattern;

public class Invoice
{

    public int InvoiceId { get; }


    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string? CustomerPhone { get; }

    public string BillingStreet { get;  }
    public string BillingCity { get;  }
    public string? BillingState { get;  }
    public string? BillingZipCode { get;}
    public string BillingCountry { get; }


    public string? ShippingStreet { get;  }
    public string? ShippingCity { get;  }
    public string? ShippingState { get;  }
    public string? ShippingZipCode { get;  }
    public string? ShippingCountry { get;  }


    public DateTime OrderDate { get;  }
    public string PaymentMethod { get; }
    public string Currency { get;  }


    public decimal SubTotal { get;  }
    public decimal DiscountAmount { get;  }
    public decimal TaxAmount { get; }
    public decimal TotalAmount { get; }


    public Invoice(int invoiceId, string customerName, 
        string customerEmail, string? customerPhone, string billingStreet, 
        string billingCity, string? billingState, string? billingZipCode, string billingCountry, 
        string? shippingStreet, string? shippingCity, string? shippingState, string? shippingZipCode,
        string? shippingCountry, DateTime orderDate, string paymentMethod, string currency, decimal subTotal
        , decimal discountAmount, decimal taxAmount, decimal totalAmount)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        BillingStreet = billingStreet;
        BillingCity = billingCity;
        BillingState = billingState;
        BillingZipCode = billingZipCode;
        BillingCountry = billingCountry;
        ShippingStreet = shippingStreet;
        ShippingCity = shippingCity;
        ShippingState = shippingState;
        ShippingZipCode = shippingZipCode;
        ShippingCountry = shippingCountry;
        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
        TotalAmount = totalAmount;
    }
}

public class InvoiceComposedBuilder
{
    private readonly int _invoiceId;
    private readonly string _customerName;
    private readonly string _customerEmail;
    private string? _customerPhone;


    public Address CustomerBillingAddress { get; private set; } = null!;
    public Address CustomerShippingAddress { get; private set; } = null!;
    public Order CustomerOrder { get; private set; } = null!;

    public InvoiceComposedBuilder(int invoiceId, string customerName, string customerEmail)
    {
        _invoiceId = invoiceId;
        _customerName = customerName;
        _customerEmail = customerEmail;
    }

    public InvoiceComposedBuilder WithPhone(string customerPhone)
    {
        _customerPhone = customerPhone;
        return this;
    }

    public InvoiceComposedBuilder WithBillingAddress(Address customerBillingAddress)
    {
        CustomerBillingAddress = customerBillingAddress;
        return this;
    }
    public InvoiceComposedBuilder WithShippingAddress(Address customerShippingAddress)
    {
        CustomerShippingAddress = customerShippingAddress;
        return this;
    }

    public InvoiceComposedBuilder WithOrder(Order customerOrder)
    {
        CustomerOrder = customerOrder;
        return this;
    }

    public Invoice Build()
    {
        ArgumentNullException.ThrowIfNull(CustomerBillingAddress);
        ArgumentNullException.ThrowIfNull(CustomerShippingAddress);
        ArgumentNullException.ThrowIfNull(CustomerOrder);
     

        return new Invoice(_invoiceId, _customerName, _customerEmail, _customerPhone, CustomerBillingAddress.Street,
        CustomerBillingAddress.City, CustomerBillingAddress.State, CustomerBillingAddress.ZipCode, CustomerBillingAddress.Country,
        CustomerShippingAddress.Street, CustomerShippingAddress.City, CustomerShippingAddress.State, CustomerShippingAddress.ZipCode,
        CustomerShippingAddress.Country, CustomerOrder.OrderDate, CustomerOrder.PaymentMethod, CustomerOrder.Currency, CustomerOrder.SubTotal,
        CustomerOrder.DiscountAmount, CustomerOrder.TaxAmount, CustomerOrder.TotalAmount);
    }
}

public class InvoiceBuilder
{
    //mandatory
    private readonly int _invoiceId;
    private readonly string _customerName;
    private readonly string _customerEmail;
    private readonly string _billingStreet;
    private readonly string _billingCity;
    private readonly string _billingCountry;
    private readonly DateTime _orderDate;
    private readonly string _paymentMethod;
    private readonly string _currency;
    private readonly decimal _subTotal;
    private readonly decimal _totalAmount;





    //optional
    private string? _customerPhone;

    private string? _billingState;
    private string? _billingZipCode;


    private string? _shippingStreet;
    private string? _shippingCity;
    private string? _shippingState;
    private string? _shippingZipCode;
    private string? _shippingCountry;


    private decimal _discountAmount;
    private decimal _taxAmount;


    public InvoiceBuilder(int invoiceId, string customerName, string customerEmail,string billingStreet
        ,string billingCity,string billingCountry ,DateTime orderDate,string paymentMethod,string currency
        ,decimal subTotal,decimal totalAmount)
    {
        if (invoiceId < 0)
            throw new ArgumentException("Invoice ID cannot be negative.");

        _invoiceId = invoiceId;

        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name is required.");

        _customerName = customerName;

        if (string.IsNullOrWhiteSpace(customerEmail))
            throw new ArgumentException("Customer email is required.");

        _customerEmail = customerEmail;

        if (string.IsNullOrWhiteSpace(billingStreet))
            throw new ArgumentException("Billing Street is required.");

        _billingStreet = billingStreet;

        if (string.IsNullOrWhiteSpace(billingCity))
            throw new ArgumentException("Billing City is required.");

        _billingCity = billingCity;

        if (string.IsNullOrWhiteSpace(billingCountry))
            throw new ArgumentException("Billing Country is required.");

        _billingCountry = billingCountry;

        if (string.IsNullOrWhiteSpace(paymentMethod))
            throw new ArgumentException("Payment Method is required.");

        _paymentMethod = paymentMethod;

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.");

        _currency = currency;

        _orderDate = orderDate;

        if (subTotal < 0)
            throw new ArgumentException("Sub Total ID cannot be negative.");

        _subTotal = subTotal;

        if (totalAmount < 0)
            throw new ArgumentException("Total Amount ID cannot be negative.");

        _totalAmount = totalAmount;

    }

    public InvoiceBuilder WithPhone(string customerPhone)
    {
        _customerPhone = customerPhone;
        return this;
    }

    public InvoiceBuilder WithBillingState(string billingState)
    {
        _billingState = billingState;
        return this;
    }

    public InvoiceBuilder WithBillingZipCode(string billingZipCode)
    {
        _billingZipCode = billingZipCode;
        return this;
    }

    public InvoiceBuilder WithShippingStreet(string shippingStreet)
    {
        _shippingStreet = shippingStreet;
        return this;
    }

    public InvoiceBuilder WithShippingCity(string shippingCity)
    {
        _shippingCity = shippingCity;
        return this;
    }

    public InvoiceBuilder WithShippingState(string shippingState)
    {
        _shippingState = shippingState;
        return this;
    }

    public InvoiceBuilder WithShippingZipCode(string shippingZipCode)
    {
        _shippingZipCode = shippingZipCode;
        return this;
    }

    public InvoiceBuilder WithShippingCountry(string shippingCountry)
    {
        _shippingCountry = shippingCountry;
        return this;
    }

    public InvoiceBuilder WithDiscountAmount(decimal discountAmount)
    {
        _discountAmount = discountAmount;
        return this;
    }

    public InvoiceBuilder WithTaxAmount(decimal taxAmount)
    {
        _taxAmount = taxAmount;
        return this;
    }


    public Invoice Build()
    => new Invoice(_invoiceId, _customerName, _customerEmail, _customerPhone, _billingStreet, _billingCity, _billingState,
            _billingZipCode, _billingCountry, _shippingStreet, _shippingCity, _shippingState, _shippingZipCode, _shippingCountry,
            _orderDate, _paymentMethod, _currency, _subTotal, _discountAmount, _taxAmount, _totalAmount);

}

public class Address
{
    public string  Street { get; }
    public string  City {  get;  }
    public string  Country { get; }
                   
    public string? State { get;}
    public string? ZipCode { get; }

    public Address(string street, string city, string country, string? state,
        string? zipCode)
    {
        Street = street;
        City = city;
        Country = country;
        State = state;
        ZipCode = zipCode;

    }
}

public class AddressBuilder
{
    private readonly string _street;
    private readonly string _city;
    private readonly string _country;

    private string? _state;
    private string? _zipCode;


    public AddressBuilder( string street
        , string city, string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Billing Street is required.");

        _street = street;

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("Billing City is required.");

        _city = city;

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Billing Country is required.");

        _country = country;

    }

    public AddressBuilder WithState(string state)
    {
        _state = state;
        return this;
    }

    public AddressBuilder WithZipCode(string zipCode)
    {
        _zipCode = zipCode;
        return this;
    }


    public Address Build() => new Address(_street,_city,_country,_state,_zipCode);
                
    
}

public class Order
{
    public  DateTime OrderDate;
    public  string  PaymentMethod;
    public  string  Currency;
    public  decimal SubTotal;
    public  decimal TotalAmount;

    public decimal DiscountAmount;
    public decimal TaxAmount;

    public Order(
        DateTime orderDate, string paymentMethod, string currency, decimal subTotal,
        decimal totalAmount, decimal discountAmount, decimal taxAmount)
    {
        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        TotalAmount = totalAmount;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
    }
}

public class OrderBuilder
{
    private readonly DateTime _orderDate;
    private readonly string _paymentMethod;
    private readonly string _currency;
    private readonly decimal _subTotal;
    private readonly decimal _totalAmount;


    private string? _customerPhone;
    private decimal _discountAmount;
    private decimal _taxAmount;

    public OrderBuilder(DateTime orderDate, string paymentMethod, string currency
        ,decimal subTotal, decimal totalAmount)
    {

        if (string.IsNullOrWhiteSpace(paymentMethod))
            throw new ArgumentException("Payment Method is required.");

        _paymentMethod = paymentMethod;

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency name is required.");

        _currency = currency;

        _orderDate = orderDate;

        if (subTotal < 0)
            throw new ArgumentException("Sub Total cannot be negative.");

        _subTotal = subTotal;

        if (totalAmount < 0)
            throw new ArgumentException("Total Amount cannot be negative.");

        _totalAmount = totalAmount;

    }

    public OrderBuilder WithPhone(string customerPhone)
    {
        _customerPhone = customerPhone;
        return this;
    }

    public OrderBuilder WithDiscountAmount(decimal discountAmount)
    {
        _discountAmount = discountAmount;
        return this;
    }

    public OrderBuilder WithTaxAmount(decimal taxAmount)
    {
        _taxAmount = taxAmount;
        return this;
    }

    public Order Build() => new Order(_orderDate, _paymentMethod, _currency, 
        _subTotal, _totalAmount, _discountAmount, _taxAmount);
}
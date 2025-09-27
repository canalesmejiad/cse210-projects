using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        // Configurar cultura para mostrar decimales con punto o coma según tu sistema (opcional)
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        // ====== Orden 1: Cliente en USA ======
        var addrUSA = new Address("742 Evergreen Terrace", "Springfield", "IL", "USA");
        var custUSA = new Customer("David Canales", addrUSA);
        var order1 = new Order(custUSA);
        order1.AddProduct(new Product("Keyboard Mechanical", "KBD104", 89.99m, 1));
        order1.AddProduct(new Product("USB-C Cable 2m", "USBC200", 12.50m, 2));
        order1.AddProduct(new Product("Desk Mat Large", "DMAT300", 24.00m, 1));

        // ====== Orden 2: Cliente internacional ======
        var addrIntl = new Address("Av. Siempre Viva 123", "Lima", "Lima", "Peru");
        var custIntl = new Customer("María Pérez", addrIntl);
        var order2 = new Order(custIntl);
        order2.AddProduct(new Product("Wireless Mouse", "MOU550", 29.90m, 1));
        order2.AddProduct(new Product("Laptop Stand", "LSTD700", 39.50m, 1));

        // (Opcional) Orden 3 para mayor cobertura
        var addrCA = new Address("101 King St W", "Toronto", "ON", "Canada");
        var custCA = new Customer("Alex Johnson", addrCA);
        var order3 = new Order(custCA);
        order3.AddProduct(new Product("Noise Cancelling Headphones", "HEAD900", 119.00m, 1));
        order3.AddProduct(new Product("HDMI Cable 3m", "HDMI300", 15.75m, 2));

        // Mostrar resultados para cada orden
        PrintOrder(order1);
        PrintOrder(order2);
        PrintOrder(order3);
    }

    static void PrintOrder(Order order)
    {
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine($"TOTAL: ${order.GetTotalPrice():0.00}");
        Console.WriteLine(new string('-', 42));
    }
}
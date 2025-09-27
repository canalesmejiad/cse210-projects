using System.Collections.Generic;
using System.Text;

public class Order
{
    private readonly List<Product> _products = new();
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        if (product != null) _products.Add(product);
    }

    public decimal GetTotalPrice()
    {
        decimal subtotal = 0m;
        foreach (var p in _products)
        {
            subtotal += p.GetTotalCost();
        }

        decimal shipping = _customer.IsInUSA() ? 5m : 35m;
        return subtotal + shipping;
    }

    public string GetPackingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("PACKING LABEL ");
        foreach (var p in _products)
        {
            sb.AppendLine($"- {p.GetName()} (ID: {p.GetProductId()}) x{p.GetQuantity()}");
        }

        return sb.ToString();
    }

    public string GetShippingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("SHIPPING LABEL");
        sb.AppendLine(_customer.GetName());
        sb.Append(_customer.GetAddress().ToMultilineString());
        return sb.ToString();
    }
}
public class Product
{
    private string _name;
    private string _productId;
    private decimal _pricePerUnit;
    private int _quantity;

    public Product(string name, string productId, decimal pricePerUnit, int quantity)
    {
        _name = name ?? "Unnamed";
        _productId = productId ?? "NA";
        _pricePerUnit = pricePerUnit < 0 ? 0 : pricePerUnit;
        _quantity = quantity < 0 ? 0 : quantity;
    }

    public string GetName() => _name;
    public string GetProductId() => _productId;
    public decimal GetPricePerUnit() => _pricePerUnit;
    public int GetQuantity() => _quantity;

    public decimal GetTotalCost()
    {
        return _pricePerUnit * _quantity;
    }
}
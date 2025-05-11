namespace SupplyChainManagement.src.Core.Domain.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(string sku, int available, int requested)
            : base($"Insufficient stock for {sku}. Available: {available}, Requested: {requested}") { }
    }
}

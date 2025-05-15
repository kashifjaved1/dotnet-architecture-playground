namespace SCM.Core.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public string Sku { get; }
        public int Available { get; }
        public int Requested { get; }

        public InsufficientStockException(string sku, int available, int requested)
            : base($"Insufficient stock for {sku}. Available: {available}, Requested: {requested}")
        {
            Sku = sku;
            Available = available;
            Requested = requested;
        }
    }
}

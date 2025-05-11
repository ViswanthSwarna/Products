namespace Products.API.Exceptions
{

namespace Products.Application.Exceptions
    {
        public class InsufficientStockException : Exception
        {
            public InsufficientStockException(int productId, int requested, int available)
                : base($"Product with ID {productId} has insufficient stock. Requested: {requested}, Available: {available}.")
            {
            }
        }
    }

}
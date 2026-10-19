using System.Runtime.CompilerServices;

namespace Ordering.Domain.Models
{
    public class Product: Entity<TProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;

        public static Product Create(TProductId id, string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var product = new Product
            {
                Id = id,
                Name = name,
                Price = price
            };

            return product;
        }
    }
}

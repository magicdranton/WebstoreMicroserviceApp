namespace Ordering.Domain.ValueObjects
{
    public record TProductId
    {
        public Guid Value { get; }

        private TProductId(Guid value) => this.Value = value;

        public static TProductId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new DomainException("OrderItemId cannot be empty.");
            }

            return new TProductId(value);
        }
    }
}

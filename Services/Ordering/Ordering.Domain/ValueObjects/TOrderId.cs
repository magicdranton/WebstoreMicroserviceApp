namespace Ordering.Domain.ValueObjects
{
    public record TOrderId
    {
        public Guid Value { get; }

        private TOrderId(Guid value) => Value = value;

        public static TOrderId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new DomainException("OrderId cannot be empty.");
            }

            return new TOrderId(value);
        }
    }
}

namespace Ordering.Domain.ValueObjects
{
    public record TCustomerId
    {
        public Guid Value { get; }

        private TCustomerId(Guid value) => Value = value;

        public static TCustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new DomainException("CustomerId cannot be empty.");
            }

            return new TCustomerId(value);
        }
    }
}

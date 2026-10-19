namespace Ordering.Domain.ValueObjects
{
    public record TOrderItemId
    {
        public Guid? Value { get; }

        private TOrderItemId(Guid? value) => this.Value = value;
        public static TOrderItemId Of(Guid? value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new DomainException("OrderItemId cannot be empty.");
            }

            return new TOrderItemId(value);
        }
    }
}

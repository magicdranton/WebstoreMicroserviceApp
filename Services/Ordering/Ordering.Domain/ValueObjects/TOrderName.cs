namespace Ordering.Domain.ValueObjects
{
    public record TOrderName
    {
        private const int CONST_DefaultLength = 5;
        public string Value { get; }

        private TOrderName(string value) => this.Value = value;

        public static TOrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            //ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, CONST_DefaultLength);

            return new TOrderName(value);
        }
    }
}

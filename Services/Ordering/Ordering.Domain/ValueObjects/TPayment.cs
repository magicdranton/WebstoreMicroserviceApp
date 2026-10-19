namespace Ordering.Domain.ValueObjects
{
    public record TPayment
    {
        public string? CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;

        protected TPayment()
        {
            //
        }

        private TPayment(string? cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            this.CardName = cardName;
            this.CardNumber = cardNumber;
            this.Expiration = expiration;
            this.CVV = cvv;
            this.PaymentMethod = paymentMethod;
        }

        public static TPayment Of(string? cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

            return new TPayment(cardName, cardNumber, expiration, cvv, paymentMethod);
        }
    }
}

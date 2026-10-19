namespace Ordering.Domain.ValueObjects
{
    public record TAddress
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string? EmailAddress { get; } = default!;
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string ZipCode { get; } = default!;

        protected TAddress()
        {
            
        }

        private TAddress(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
        {
            this.FirstName = firstName; 
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.AddressLine = addressLine; 
            this.Country = country;
            this.State = state;
            this.ZipCode = zipCode;
        }

        public static TAddress Of(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

            return new TAddress(firstName, lastName, emailAddress, addressLine, country, state, zipCode);
        }
    }
}

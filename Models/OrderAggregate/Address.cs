namespace Mataeem.Models.OrderAggregate
{
    public class Address : BaseEntity
    {
        public Address()
        {
        }
        public Address(string firstName, string lastName, string street, string city, string zipcode)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            Zipcode = zipcode;
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }
    }
}

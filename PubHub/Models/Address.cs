using System.Net;

namespace PubHub.Models
{
    public class Address
    {
        public int AddressID { get; set; }  // Primary Key
        public int CustomerID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public Customer Customer { get; set; }
    }
}

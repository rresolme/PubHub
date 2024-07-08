using System.Net;

namespace PubHub.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }  // Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }


}

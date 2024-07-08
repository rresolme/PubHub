namespace PubHub.Models
{
    public class Subscription
    {
        public int SubscriptionID { get; set; }  // Primary Key
        public int CustomerID { get; set; }
        public int PublicationID { get; set; }
        public int AddressID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } // Active or Inactive
        public Customer Customer { get; set; }
        public Publication Publication { get; set; }
        public Address Address { get; set; }
    }

}

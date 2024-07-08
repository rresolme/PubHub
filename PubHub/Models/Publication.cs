namespace PubHub.Models
{
    public class Publication
    {
        public int PublicationID { get; set; }  // Primary Key
        public string Title { get; set; }
        public string Category { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<DistributorPublication> DistributorPublications { get; set; }
    }

}

namespace PubHub.Models
{
    public class PrintDistributor
    {
        public int DistributorID { get; set; }  // Primary Key
        public string Name { get; set; }
        public string APIEndpoint { get; set; }
        public string APIKey { get; set; }
        public ICollection<DistributorPublication> DistributorPublications { get; set; }
    }

}

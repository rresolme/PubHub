namespace PubHub.Models
{
    public class DistributorPublication
    {
        public int DistributorPublicationID { get; set; }  // Primary Key
        public int DistributorID { get; set; }
        public int PublicationID { get; set; }
        public string Country { get; set; }
        public PrintDistributor Distributor { get; set; }
        public Publication Publication { get; set; }
    }

}

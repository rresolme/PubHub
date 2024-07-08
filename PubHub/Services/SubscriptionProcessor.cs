using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PubHub.Models;
using PubHub.Services;

public class SubscriptionProcessor
{
    private readonly PublishingContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<SubscriptionProcessor> _logger;

    public SubscriptionProcessor(PublishingContext context, IHttpClientFactory httpClientFactory, ILogger<SubscriptionProcessor> logger)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task ProcessSubscriptionsAsync()
    {
        try
        {
            var activeSubscriptions = await _context.Subscriptions
                .Include(s => s.Customer)
                .Include(s => s.Address)
                .Include(s => s.Publication)
                .Where(s => s.Status == "Active" && s.EndDate > DateTime.Now)
                .ToListAsync();

            foreach (var subscription in activeSubscriptions)
            {
                try
                {
                    var distributor = await _context.DistributorPublications
                        .Include(dp => dp.Distributor)
                        .FirstOrDefaultAsync(dp => dp.PublicationID == subscription.PublicationID && dp.Country == subscription.Address.Country);

                    if (distributor != null)
                    {
                        var client = _httpClientFactory.CreateClient();
                        var distributorService = new PrintDistributorService(client, distributor.Distributor);
                        await distributorService.SendPrintRequestAsync(new PrintRequest
                        {
                            CustomerName = subscription.Customer.Name,
                            Address = subscription.Address,
                            PublicationTitle = subscription.Publication.Title
                        });
                    }
                    else
                    {
                        _logger.LogWarning($"No distributor found for publication {subscription.PublicationID} in country {subscription.Address.Country}.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error processing subscription ID {subscription.SubscriptionID} for customer ID {subscription.CustomerID}.");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching active subscriptions.");
        }
    }
}

public class PrintRequest
{
    public string CustomerName { get; set; }
    public Address Address { get; set; }
    public string PublicationTitle { get; set; }
}

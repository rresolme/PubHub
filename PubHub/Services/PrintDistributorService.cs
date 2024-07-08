using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PubHub.Models;
using PubHub.Services;

public class PrintDistributorService : IPrintDistributorService
{
    private readonly HttpClient _httpClient;
    private readonly PrintDistributor _distributor;
    private readonly ILogger<PrintDistributorService> _logger;

    public PrintDistributorService(HttpClient httpClient, PrintDistributor distributor, ILogger<PrintDistributorService> logger)
    {
        _httpClient = httpClient;
        _distributor = distributor;
        _logger = logger;
    }

    public async Task SendPrintRequestAsync(PrintRequest request)
    {
        try
        {
            // Placeholder for actual API call
            var uri = new Uri($"{_distributor.APIEndpoint}/print");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_distributor.APIKey}");

            // Simulate API call
            await Task.Delay(1000); // Simulate network delay

            // Uncomment the following lines when actual implementation is done
            // var response = await _httpClient.PostAsJsonAsync(uri, request);
            // response.EnsureSuccessStatusCode();

            // For now, we just log the request
            _logger.LogInformation($"Simulated print request to {_distributor.Name} for {request.CustomerName} to {request.Address.Street}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending print request to distributor {_distributor.Name} for customer {request.CustomerName}.");
        }
    }
}

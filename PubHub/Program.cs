using Hangfire;
using Microsoft.EntityFrameworkCore;
using PubHub.Models;
using PubHub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework with SQL Server
builder.Services.AddDbContext<PublishingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add HttpClient
builder.Services.AddHttpClient();

// Configure Hangfire with SQL Server
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Configure Hangfire Dashboard
app.UseHangfireDashboard();

// Schedule the job
RecurringJob.AddOrUpdate<SubscriptionProcessor>(
    "process-subscriptions",
    processor => processor.ProcessSubscriptionsAsync(),
    Cron.Monthly);

app.Run();

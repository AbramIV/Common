﻿using Google.Apis.Services;
using System.SeviceModel.Discovery;

Console.WriteLine("Discovery API Sample");
Console.WriteLine("====================");

try
{
    Run().Wait();
}
catch (AggregateException ex)
{
    foreach (var e in ex.InnerExceptions)
        Console.WriteLine("ERROR: " + e.Message);
}

Console.WriteLine("Press any key to continue...");
Console.ReadKey();

async Task Run()
{
    // Create the service.
    var service = new DiscoveryService(new BaseClientService.Initializer
    {
        ApplicationName = "Discovery Sample",
        ApiKey = "[YOUR_API_KEY_HERE]",
    });

    // Run the request.
    Console.WriteLine("Executing a list request...");
    var result = await service.Apis.List().ExecuteAsync();

    // Display the results.
    if (result.Items != null)
    {
        foreach (DirectoryList.ItemsData api in result.Items)
        {
            Console.WriteLine(api.Id + " - " + api.Title);
        }
    }
}
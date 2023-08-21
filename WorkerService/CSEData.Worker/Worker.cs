using WorkerService.Application.Features.Training.Services;
using WorkerService.Domain.Entities;

namespace CSEData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICompanyService _companyService;
        private readonly IPriceService _priceService;
        private string url = "https://www.cse.com.bd/market/current_price";
        public Worker(ILogger<Worker> logger, ICompanyService companyService, IPriceService priceService)
        {
            _logger = logger;
            _companyService = companyService;
            _priceService = priceService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //sir instruct us that Company table will be inserted only once but Price table can be inserted as many times as we want to
                // Insert a new company
                // As we are calling method but by interface. So interface must hold the signature for the method to be called from here. Otherwise we can't call the methods from here.
                if (!_companyService.CompanyExist())
                {
                    _companyService.InsertCompany(url);
                    //As company table data should be inserted only once
                }
                //Company newCompany = _companyService.InsertCompany("Premio");//this will run once

                //// Insert a new price for the inserted company ; this method should run as many time as we want
                //_priceService.InsertPriceTableData(newCompany.Id, 100.0m, 500, 95.0m, 110.0m, 90.0m);

                //_companyService.UpdateCompany(newCompany.Id, "UpdatedPremio");
                //_priceService.UpdatePriceTableData(newCompany.Prices.First().Id, newCompany.Id, 110.0m, 700, 105.0m, 120.0m, 100.0m);
                //await Console.Out.WriteLineAsync(_priceService.GetCompanyId("1JANATAMF").ToString());
                _priceService.InsertPriceTableData(url);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow.ToLocalTime().ToString("hh:mm tt"));
                await Task.Delay(60000, stoppingToken);

            }
        }
    }
}
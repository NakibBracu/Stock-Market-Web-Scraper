using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Application;
using WorkerService.Application.Features.Training.Services;
using WorkerService.Domain.Entities;
using WorkerService.Persistence;

namespace WorkerService.Infrastructure.Features.Services
{
    public class PriceService : IPriceService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly ICompanyService _companyService;

        public PriceService(IApplicationUnitOfWork unitOfWork, ICompanyService companyService)
        {
            _unitOfWork = unitOfWork;
            _companyService = companyService;
        }

        public Guid GetCompanyId(string name)
        {
            var companyId = _unitOfWork.Company.CompanyIdCatcher(name);

            if (companyId != default(Guid))
            {
                return companyId;
            }
            else
            {
                //throw new InvalidOperationException($"Company with name '{name}' not found.");
                //let's just return empty string then by that condition, we will insert Company name
                //in the company table if the company was not inserted in the company table then
                return _companyService.InsertCompanyIfNotExits(name);
            }
        }
        private HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();//html document get use 
            HtmlDocument doc = web.Load(url);
            return doc;
        }

        private string MarketStatus(string url)
        {
            HtmlDocument doc = GetDocument(url);
            if (doc != null)
            {
                // Replace the XPath expression with the actual one you want to use
                string xPathExpression = "//*[@id=\"wrapper\"]/div/header/div/div/div[2]/div[1]/div[1]/span";
                HtmlNode node = doc.DocumentNode.SelectSingleNode(xPathExpression);

                if (node != null)
                {
                    return node.InnerText.Trim();
                }
            }
            return string.Empty;
        }
        public void InsertPriceTableData(string url)
        {
            if (MarketStatus(url) == "Open")
            {
                HtmlDocument doc = GetDocument(url);
                HtmlNodeCollection rows = doc.DocumentNode.SelectNodes("//*[@id=\"dataTable\"]/tbody/tr");
                if (rows != null)
                {
                    foreach (HtmlNode row in rows)
                    {
                        HtmlNodeCollection cells = row.SelectNodes("td");
                        if (cells != null && cells.Count >= 3) // Ensure there are at least 3 cells (LTP is in the third cell)
                        {
                            string companyIdtext = cells[1].InnerText; // Index 1 corresponds to stockCodeName
                                                                       // Retrieve the Company entity by Name

                            Guid companyId = GetCompanyId(companyIdtext);
                            string ltpText = cells[2].InnerText;
                            decimal ltp = decimal.Parse(ltpText);
                            string volumeText = cells[9].InnerText;
                            decimal volume = decimal.Parse(volumeText);
                            string openText = cells[3].InnerText;
                            decimal open = decimal.Parse(openText);
                            string highText = cells[4].InnerText;
                            decimal high = decimal.Parse(highText);
                            string lowText = cells[5].InnerText;
                            decimal low = decimal.Parse(lowText);
                            Price newPrice = new Price
                            {
                                CompanyId = companyId, // Assign the retrieved CompanyId
                                PriceLTP = ltp,
                                Volume = volume,
                                Open = open,
                                High = high,
                                Low = low,
                                Time = DateTimeOffset.UtcNow.ToLocalTime()
                            };

                            // Add the new price entity to the DbSet
                            _unitOfWork.PriceRepository.Add(newPrice);

                            // Commit the changes to the database
                            _unitOfWork.Save();

                        }
                    }
                }
            }
        
        }

       

    }

}

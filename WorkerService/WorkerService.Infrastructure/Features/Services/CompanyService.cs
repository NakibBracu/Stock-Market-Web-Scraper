using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WorkerService.Application;
using WorkerService.Application.Features.Training.Services;
using WorkerService.Domain.Entities;
using WorkerService.Persistence;

namespace WorkerService.Infrastructure.Features.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;


        public CompanyService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        private HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();//html document get use 
            HtmlDocument doc = web.Load(url);
            return doc;
        }

        public bool CompanyExist()
        {
            return _unitOfWork.Company.DoesCompanyDataExist();
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


        public void InsertCompany(string url)
        {
            if (MarketStatus(url) == "Open")
            {
                HtmlDocument doc = GetDocument(url);//full html document
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath: "//td/a");//selecting the specific nodes where we can have the required data
                foreach (HtmlNode node in nodes)
                {
                    // Create a new Company entity with the provided StockCodeName
                    Company newCompany = new Company
                    {
                        StockCodeName = node.InnerText
                    };
                    // Add the new company entity to the DbSet
                    _unitOfWork.Company.Add(newCompany);

                    // Commit the changes to the database
                    _unitOfWork.Save();
                }
            }
           
        }
        public Guid InsertCompanyIfNotExits(string NewcompanyName)
        {

            Company newCompany = new Company
            {
                StockCodeName = NewcompanyName
            };
            // Add the new company entity to the DbSet
            _unitOfWork.Company.Add(newCompany);

            // Commit the changes to the database
            _unitOfWork.Save();

            return newCompany.Id;//as it is newly inserted company so we must need this Id to insert this companies price datas.
        }


        }
}

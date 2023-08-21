using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Application;
using WorkerService.Application.Features.Training.Repositories;

namespace WorkerService.Persistence
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
      

        public ICompanyRepository Company { get; private set; }

        public IPriceRepository PriceRepository { get; private set; }

        public ApplicationUnitOfWork(IApplicationDbContext dbContext,
            ICompanyRepository companyRepository, IPriceRepository priceRepository) : base((DbContext)dbContext)
        {
            Company = companyRepository;
            PriceRepository = priceRepository;
        }
    }
}

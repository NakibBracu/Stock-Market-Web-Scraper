using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Application.Features.Training.Repositories;
using WorkerService.Domain.UnitOfWork;

namespace WorkerService.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        ICompanyRepository Company { get; }
        IPriceRepository PriceRepository { get; }
    }
}

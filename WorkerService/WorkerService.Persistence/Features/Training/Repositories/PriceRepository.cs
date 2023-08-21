using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Application.Features.Training.Repositories;
using WorkerService.Domain.Entities;

namespace WorkerService.Persistence.Features.Training.Repositories
{
    public class PriceRepository : Repository<Price, Guid>, IPriceRepository
    {
        public PriceRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}

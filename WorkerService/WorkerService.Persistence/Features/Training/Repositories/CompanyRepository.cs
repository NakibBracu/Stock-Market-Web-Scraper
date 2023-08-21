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
    public class CompanyRepository : Repository<Company, Guid>, ICompanyRepository
    {
        private readonly IApplicationDbContext _context;
        public CompanyRepository(IApplicationDbContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public Guid CompanyIdCatcher(string name)
        {
            var companyId = _context.Company
                 .Where(c => c.StockCodeName == name)
                 .Select(c => c.Id)
                 .FirstOrDefault();
            return companyId;
        }

        public bool DoesCompanyDataExist()
        {
            int count = _context.Company.Count(); // Assuming Company is your DbSet in DbContext

            return count > 0;
        }
    }
}

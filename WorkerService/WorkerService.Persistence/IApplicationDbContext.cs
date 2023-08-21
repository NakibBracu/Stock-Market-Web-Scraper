using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Domain.Entities;

namespace WorkerService.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Company> Company { get; set; }
        DbSet<Price> Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Domain.Entities;

namespace WorkerService.Application.Features.Training.Services
{
    public interface ICompanyService
    {
      
        void InsertCompany(string url);
        bool CompanyExist();
        Guid InsertCompanyIfNotExits(string NewcompanyName);
    }
}

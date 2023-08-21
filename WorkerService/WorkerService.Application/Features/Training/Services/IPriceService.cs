using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Application.Features.Training.Services
{
    public interface IPriceService
    {
        Guid GetCompanyId(string name);
        void InsertPriceTableData(string url);
       
    }

}

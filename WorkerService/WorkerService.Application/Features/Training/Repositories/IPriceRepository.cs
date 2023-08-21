using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Domain.Entities;
using WorkerService.Domain.Repositories;

namespace WorkerService.Application.Features.Training.Repositories
{
    public interface IPriceRepository: IRepositoryBase<Price,Guid>
    {

    }
}

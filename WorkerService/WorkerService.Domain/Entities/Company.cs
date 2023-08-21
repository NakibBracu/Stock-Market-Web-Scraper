using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Domain.Entities
{
    public class Company : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string StockCodeName { get; set; }

        // Navigation property to represent the relationship with the Price entities
        public ICollection<Price> Prices { get; set; }
    }

}

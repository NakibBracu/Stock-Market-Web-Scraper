using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace WorkerService.Domain.Entities
{
    public class Price : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; } // Foreign key referencing the Company table
        public decimal PriceLTP { get; set; }
        public decimal Volume { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public DateTimeOffset Time { get; set; }

        // Navigation property to represent the relationship with the Company entity
        public Company Company { get; set; }
    }

}

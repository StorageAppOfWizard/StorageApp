using StorageProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageProject.Domain.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public Product Product { get; set; }
        public int QuantityProduct { get; set; }


    }
}

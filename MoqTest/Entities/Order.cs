using MoqPOC.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoqPOC.Entities
{
   public class Order: IEntityBase 
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}

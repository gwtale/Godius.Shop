using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class Purchase
    {
		public Guid Id { get; set; }

		public Goods Item { get; set; }

		public ApplicationUser Purchaser { get; set; }

		public DateTime? Date { get; set; }
    }
}

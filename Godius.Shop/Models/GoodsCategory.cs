using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class GoodsCategory
    {
		public Guid Id { get; set; }

		[Display(Name = "코드")]
		public string SerialCode { get; set; }

		[Display(Name = "이름")]
		public string Name { get; set; }

		public virtual List<Goods> Goods { get; set; }
	}
}

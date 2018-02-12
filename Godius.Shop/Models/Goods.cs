using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class Goods
    {
		public Guid Id { get; set; }

		[Display(Name = "코드")]
		public string SerialCode { get; set; }

		[Display(Name = "이름")]
		public string Name { get; set; }

		[Display(Name = "금액")]
		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal Price { get; set; }

		[Display(Name = "설명")]
		public string Description { get; set; }

		[Display(Name = "이미지")]
		public string Image { get; set; }
	}
}

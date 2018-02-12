using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models.GoodsViewModels
{
    public class CreateGoodsViewModel
	{
		[Required]
		[Display(Name = "코드")]
		public string SerialCode { get; set; }

		[Required]
		[Display(Name = "이름")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "금액")]
		[DisplayFormat(DataFormatString = "{0:C0}")]
		public decimal Price { get; set; }

		[Required]
		[Display(Name = "이미지")]
		public IFormFile Image { get; set; }
	}
}
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models.GoodsViewModels
{
    public class EditGoodsViewModel
    {
		[Required]
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "코드")]
		public string SerialCode { get; set; }

		[Required]
		[Display(Name = "이름")]
		public string Name { get; set; }

		[Required, DataType(DataType.Currency)]
		[Display(Name = "금액")]
		public decimal Price { get; set; }
		
		public IFormFile Image { get; set; }

		[Required]
		[Display(Name = "이미지")]
		public string ImagePath { get; set; }
	}
}
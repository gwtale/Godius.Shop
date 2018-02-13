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
		[Display(Name = "카테고리")]
		public string CategoryId { get; set; }

		[Required]
		[Display(Name = "코드")]
		public string SerialCode { get; set; }

		[Required]
		[Display(Name = "이름")]
		public string Name { get; set; }

		[Required, DataType(DataType.Currency)]
		[Display(Name = "금액")]
		public decimal Price { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "설명")]
		public string Description { get; set; }

		public IFormFile Image { get; set; }
		
		[Display(Name = "이미지")]
		public string ImagePath { get; set; }
	}
}
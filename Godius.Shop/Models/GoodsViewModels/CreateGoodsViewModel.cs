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
		public string SerialCode { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public double Price { get; set; }

		[Required]
		public IFormFile Image { get; set; }
	}
}

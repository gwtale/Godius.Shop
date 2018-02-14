using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models.ItemViewModels
{
    public class EditItemViewModel
    {
		[Required]
		[Display(Name = "카테고리")]
		public ItemCategories Category { get; set; }

		[Required]
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "이름")]
		public string Name { get; set; }

		[Display(Name = "설명")]
		public string Description { get; set; }

		[Display(Name = "무게")]
		public int? Weight { get; set; }

		[Display(Name = "WC")]
		public int? WC { get; set; } = 0;

		[Display(Name = "AC")]
		public int? AC { get; set; } = 0;

		[Display(Name = "HC")]
		public int? HC { get; set; } = 0;

		[Display(Name = "DC")]
		public int? DC { get; set; } = 0;

		[Display(Name = "내구력")]
		public int? Durability { get; set; } = 0;

		[Required]
		[Display(Name = "세대")]
		public double Generation { get; set; } = 1;

		public IFormFile Image { get; set; }

		[Display(Name = "이미지")]
		public string ImagePath { get; set; }
	}
}

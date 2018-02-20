using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class Item
    {
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Display(Name = "카테고리")]
		public ItemCategories Category { get; set;}

		[Display(Name = "이름")]
		public string Name { get; set; }

		[Display(Name = "설명")]
		public string Description { get; set; }

		[Display(Name = "무게")]
		public int? Weight { get; set; }

		[Display(Name = "WC")]
		public int? WC { get; set; }

		[Display(Name = "AC")]
		public int? AC { get; set; }

		[Display(Name = "HC")]
		public int? HC { get; set; }

		[Display(Name = "DC")]
		public int? DC { get; set; }

		[Display(Name = "내구력")]
		public int? Durability { get; set; }

		[Display(Name = "이미지")]
		public string Image { get; set; }

		[Display(Name = "세대")]
		public double Generation { get; set; } = 1;

		public virtual List<ItemGoods> ItemGoods { get; set; }
	}

	public enum ItemCategories
	{
		Normal,
		Special,
		Event,
		Weapon,
		Clothe,
		Armor,
		Shoe,
		Shield
	}
}

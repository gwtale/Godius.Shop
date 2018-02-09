using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class Item
    {
		public Guid Id { get; set; }
		public ItemCategories Category { get; set;}
		public string Name { get; set; }

		public int DefaultOption { get; set; }

		public int DefaultDurability { get; set; }

		public int UpgradeOption { get; set; } = 1;
		public int UpgradeDurability { get; set; } = 1;

		public Purchase ByPurchase { get; set; }
	}

	public enum ItemCategories
	{
		Weapon,
		Clothe,
		Armor,
		Shoe,
		Shield
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class ResultItemGoods
    {
		public Guid Id { get; set; }

		public ItemGoods ItemGoods { get; set; }
		public Guid ItemGoodsId { get; set; }

		[Display(Name = "업그레이드 옵션")]
		public int UpgradeOption { get; set; } = 0;

		[Display(Name = "업그레이드 내구력")]
		public int UpgradeDurability { get; set; } = 0;

		public Purchase Purchase { get; set; }
		public Guid PurchaseId { get; set; }
	}
}

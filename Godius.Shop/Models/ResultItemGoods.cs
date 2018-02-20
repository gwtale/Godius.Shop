using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class ResultItemGoods
    {
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[ForeignKey("ItemGoodsId")]
		public ItemGoods ItemGoods { get; set; }
		public Guid? ItemGoodsId { get; set; }

		[ForeignKey("PurchaseId")]
		public Purchase Purchase { get; set; }
		public Guid PurchaseId { get; set; }

		[Display(Name = "업그레이드 옵션")]
		public int UpgradeOption { get; set; } = 0;

		[Display(Name = "업그레이드 내구력")]
		public int UpgradeDurability { get; set; } = 0;
	}
}

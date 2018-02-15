using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class ItemGoods
    {
		public Guid Id { get; set; }
		
		[Display(Name = "획득확률")]
		public double Probability { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "설명")]
		public string Description { get; set; }

		[Display(Name = "추가 업그레이드 여부")]
		public bool NeedAdditionalUpgrade { get; set; }

		[Display(Name = "상품")]
		public Goods Goods { get; set; }
		[Display(Name = "상품")]
		public Guid GoodsId { get; set; }

		[Display(Name = "아이템")]
		public Item Item { get; set; }
		[Display(Name = "아이템")]
		public Guid ItemId { get; set; }

		public virtual List<ResultItemGoods> ResultItemGoods { get; set; }
	}
}

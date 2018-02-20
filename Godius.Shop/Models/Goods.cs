using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class Goods
    {
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Display(Name = "코드")]
		public string SerialCode { get; set; }

		[Display(Name = "이름")]
		public string Name { get; set; }

		[DataType(DataType.Currency)]
		[Display(Name = "금액")]
		public decimal Price { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "설명")]
		public string Description { get; set; }

		[Display(Name = "이미지")]
		public string Image { get; set; }

		[ForeignKey("CategoryId")]
		[Display(Name = "카테고리")]
		public GoodsCategory Category { get; set; }
		public Guid CategoryId { get; set; }
		
		public virtual List<Purchase> PurchaseHistory { get; set; }

		/// <summary>
		/// 하위 아이템 상품
		/// </summary>
		public virtual List<ItemGoods> ItemsGoods { get; set; }
	}
}

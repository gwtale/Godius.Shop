using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
	public class Purchase
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[ForeignKey("GoodsId")]
		public Goods Goods { get; set; }
		public Guid GoodsId { get; set; }

		[ForeignKey("PurchaserId")]
		public ApplicationUser Purchaser { get; set; }
		public string PurchaserId { get; set; }
		
		public DateTime? Date { get; set; }

		public ResultItemGoods ResultItemGoods { get; set; }
	}
}

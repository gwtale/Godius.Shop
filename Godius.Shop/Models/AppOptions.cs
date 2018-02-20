using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class AppOptions
    {
		public AppOptions()
		{

		}

		public string ItemImagePath => "item/images/";
		public string GoodsImagePath => "goods/images/";
		public int PurchaseHistoryPageSize => 10;
	}
}

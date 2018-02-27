using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models.StatisticsViewModels
{
    public class PerUserStatisticsViewModel
    {
		public List<ExpandoObject> RichUsers { get; set; } = new List<ExpandoObject>();
		public List<ExpandoObject> LuckyUsers { get; set; } = new List<ExpandoObject>();
	}
}

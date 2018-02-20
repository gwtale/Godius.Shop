using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop
{
    public class BoardHelper
    {
		/// <summary>
		/// 매개변수 dateTime이 오늘 날짜면 시간을 표시하고, 그렇지않으면 날짜를 표시한다.
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static string GetShowTime(DateTime? dateTime)
		{
			if (dateTime != null)
			{
				if (dateTime.Value.Date == DateTime.Now.Date)
				{
					return dateTime.Value.ToString("HH:mm:ss");
				}
				else
				{
					return dateTime.Value.ToString();
				}
			}
			else
			{
				return "-";
			}
		}
    }
}

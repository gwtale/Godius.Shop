using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models
{
    public class Notice
    {
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Display(Name = "제목")]
		public string Title { get; set; }

		[Display(Name = "내용")]
		public string Content { get; set; }

		[Display(Name = "작성일")]
		public DateTime Date { get; set; }

		[ForeignKey("UserId")]
		[Display(Name = "작성자")]
		public ApplicationUser User { get; set; }
		public string UserId { get; set; }
    }
}

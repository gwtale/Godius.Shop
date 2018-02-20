using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

		[Required(ErrorMessage = "이메일 주소를 입력하세요.")]
		[EmailAddress(ErrorMessage = "올바른 이메일 주소 형식이 아닙니다.")]
		[Display(Name = "이메일")]
		public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}

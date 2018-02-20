using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models.AccountViewModels
{
    public class LoginViewModel
    {
		[Required(ErrorMessage = "이메일 주소를 입력하세요.")]
		[EmailAddress(ErrorMessage = "올바른 이메일 주소 형식이 아닙니다.")]
		[Display(Name = "이메일 아이디")]
		public string Email { get; set; }

		[Required(ErrorMessage = "비밀번호를 입력하세요.")]
		[DataType(DataType.Password)]
		[Display(Name = "비밀번호")]
		public string Password { get; set; }

        [Display(Name = "아이디 저장")]
        public bool RememberMe { get; set; }
    }
}

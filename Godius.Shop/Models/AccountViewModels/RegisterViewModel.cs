using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "이메일 주소를 입력하세요.")]
        [EmailAddress(ErrorMessage = "올바른 이메일 주소 형식이 아닙니다.")]
        [Display(Name = "이메일")]
        public string Email { get; set; }

        [Required(ErrorMessage = "비밀번호를 입력하세요.")]
        [StringLength(100, ErrorMessage = "{0}는 {2}자 이상 {1} 미만으로 설정 가능합니다.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "비밀번호")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "비밀번호 확인")]
        [Compare("Password", ErrorMessage = "비밀번호와 일치하지 않습니다.")]
        public string ConfirmPassword { get; set; }
    }
}

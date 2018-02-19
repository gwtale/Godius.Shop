using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Godius.Shop.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
		[Required(ErrorMessage = "비밀번호를 입력하세요.")]
		[DataType(DataType.Password)]
        [Display(Name = "현재 비밀번호")]
        public string OldPassword { get; set; }

		[Required(ErrorMessage = "비밀번호를 입력하세요.")]
		[StringLength(100, ErrorMessage = "{0}는 {2}자 이상 {1} 미만으로 설정 가능합니다.", MinimumLength = 4)]
		[DataType(DataType.Password)]
		[Display(Name = "새 비밀번호")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "새 비밀번호 확인")]
        [Compare("NewPassword", ErrorMessage = "새 비밀번호와 일치하지 않습니다.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Warehouse.Model.CreatedBy
{
    public class CreatedByModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "xin vui lòng nhập tài khoản !"), DataType(DataType.EmailAddress), MaxLength(20, ErrorMessage = "Tài khoản phải ít hơn 20 kí tự"), MinLength(5, ErrorMessage = "Mật khẩu phải nhiều hơn 4 kí tự")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu !"), DataType(DataType.Password), MaxLength(20, ErrorMessage = "Mật khẩu phải ít hơn 20 kí tự"), MinLength(5, ErrorMessage = "Mật khẩu phải nhiều hơn 4 kí tự")]
        public string? Password { get; set; }
        public string? Avarta { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateRegister { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
    }
}
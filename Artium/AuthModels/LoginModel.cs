using System.ComponentModel.DataAnnotations;

namespace Artium.AuthModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string inputData { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
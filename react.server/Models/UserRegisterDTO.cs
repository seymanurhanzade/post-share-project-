using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace react.server.Models
{
    #nullable disable
    public class UserRegisterDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-ZçğıöşüÇĞİÖŞÜ0-9\s]+$", ErrorMessage = "Türkçe karakterler, harfler, rakamlar ve boşluk kabul edilir.")]

        [JsonPropertyName("FullName")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Sadece harfler, rakamlar ve alt çizgi kabul edilir. Boşluk veya Türkçe karakter kullanılamaz.")]        
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [DataType(DataType.EmailAddress)]
        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Parola en az {2} ve en fazla {1} karakter olmalıdır.", MinimumLength = 6)]
        [JsonPropertyName("Password")]
        public string Password { get; set; }

        public string Image { get; set; }
    }
}

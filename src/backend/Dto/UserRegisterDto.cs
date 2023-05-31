using System.ComponentModel.DataAnnotations;

public class UserRegisterDto
{
    [Required(ErrorMessage = "Informe um nome")]
    public string userName { get; set; }
    [Required(ErrorMessage = "Informe um telefone")]
    [RegularExpression(@"^\(\d{2}\)\s\d{4,5}-\d{4}$", ErrorMessage = "Informe um telefone valido")]
    public string telephone { get; set; }
    [Required(ErrorMessage = "Informe um email")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Informe um email valido")]
    public string email { get; set; }
    [Required(ErrorMessage = "Informe uma senha")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "A senha deve conter pelo menos 8 caracteres, uma letra maiúscula, uma letra minúscula, um numero e um caractere especial")]
    public string password { get; set; }
    public string? userPhoto { get; set; }
    public IFormFile? userImageFile { get; set; }
}
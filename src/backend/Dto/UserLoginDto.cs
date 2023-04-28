using System.ComponentModel.DataAnnotations;
public class UserLoginDto
{
    [Required(ErrorMessage ="Informe um Email")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage ="Informe um email valido")]
    public string Email { get; set; }
    [Required(ErrorMessage ="Informe uma senha")]
    public string Senha {get; set;}
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_vendas_web.Models
{
    public class Login
    {
        [Key]
        [Display(Name = "Login")]
        [ForeignKey("Usuarios.login")]
        public string login { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha obrigatória!")]
        [StringLength(8, MinimumLength = 4, ErrorMessage =
            "Senha deve ter no mínimo 4 e no máximo 8 caracteres!")]
        public string senha { get; set; }

        [Display(Name = "Confirmar senha")]
        [DataType(DataType.Password)]
        [Compare("senha", ErrorMessage = "Campos diferentes!")]
        public string confirmarSenha { get; set; }

        public void SetUsuario(string u)
        {
            login = u;
        }
    }
}

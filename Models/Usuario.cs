using System.ComponentModel.DataAnnotations;

namespace UniGymFitness.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do usuário.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [StringLength(120, ErrorMessage = "O e-mail deve ter no máximo 120 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o telefone.")]
        [RegularExpression(@"\([0-9]{2}\) 9[0-9]{4}-[0-9]{4}", ErrorMessage = "Use o formato (61) 99999-9999.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione um plano.")]
        public string Plano { get; set; } = string.Empty;

        public string TipoUsuario { get; set; } = "Aluno";
    }
}

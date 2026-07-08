using System.ComponentModel.DataAnnotations;

namespace UniGymFitness.Models
{
    public class Plano
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do plano.")]
        [StringLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "999999.99", ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe a descrição.")]
        [StringLength(250, ErrorMessage = "A descrição deve ter no máximo 250 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe os benefícios.")]
        [StringLength(600, ErrorMessage = "Os benefícios devem ter no máximo 600 caracteres.")]
        public string Beneficios { get; set; } = string.Empty;
    }
}

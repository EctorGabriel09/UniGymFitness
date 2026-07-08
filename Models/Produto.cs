using System.ComponentModel.DataAnnotations;

namespace UniGymFitness.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "999999.99", ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Range(0, 999999, ErrorMessage = "O estoque não pode ser negativo.")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "Informe a descrição.")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;
    }
}

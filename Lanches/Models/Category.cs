using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanches.Models 
{
    [Table("Categories")]
    public class Category 
    {
        [Key]
        public int CategoriaId { get; set; }

        [StringLength(100, ErrorMessage ="O tamanho max é de 100 caracteres")]
        [Required(ErrorMessage ="Informe o nome da categoria")]
        [Display(Name ="Nome")]
         public string CategoriaNome { get; set; }

        [StringLength(200, ErrorMessage = "O tamanho max é de 200 caracteres")]
        [Required(ErrorMessage = "Informe a descrição da categoria")]
        [Display(Name = "Descricao")]
        public string Descricao { get; set; }

        //definindo propriedade Lista do tipo Lanches que identifica a categoria de relacionamento de lanche 1 para N (muitos).
        public List<Lanche> Lanches { get; set;}
    }
}

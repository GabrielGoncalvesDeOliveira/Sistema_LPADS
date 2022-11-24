using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SistemaGestaoCantinasIgrejas.Models
{
    public enum Categoria { LANCHE, SALGADO, BEBIDA, DOCE, BOLO }

    [Table("Produto")]
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id: ")]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo categoria é obrigatório")]
        [Display(Name = "Categoria: ")]
        public Categoria categoria { get; set; }

        [Required(ErrorMessage = "Campo descrição é obrigatório")]
        [Display(Name = "Descrição: ")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "Campo quantidade é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Quantidade: ")]
        public float quantidade { get; set; }

        [Required(ErrorMessage = "Campo valor é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor unitário: ")]
        public float valor { get; set; }

        public bool disponivel { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoCantinasIgrejas.Models
{
    [Table("Participante")]
    public class Participante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id: ")]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [Display(Name = "Nome: ")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo contato é obrigatório")]
        [Display(Name = "Contato: ")]
        public string contato { get; set; }

        [Required(ErrorMessage = "Campo email é obrigatório")]
        [Display(Name = "Email: ")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo endereço é obrigatório")]
        [Display(Name = "Endereço: ")]
        public string endereco { get; set; }
    }
}

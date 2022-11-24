using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestaoCantinasIgrejas.Models
{
    [Table("Igreja")]
    public class Igreja
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id: ")]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo denominação é obrigatório")]
        [Display(Name = "Denominação: ")]
        public string denominacao { get; set; }

        [Required(ErrorMessage = "Campo endereço é obrigatório")]
        [Display(Name = "Endereço: ")]
        public string endereco { get; set; }

        public ICollection<Evento> eventos { get; set; }
    }
}

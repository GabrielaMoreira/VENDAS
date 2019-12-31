using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistema_vendas_web.Models.Cliente
{
    public class Fisico : Cliente
    {
        [RegularExpression(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", ErrorMessage = "Use apenas caracteres alfabéticos.")]
        [Column("NOME", TypeName = "varchar(40)")]
        [Required(ErrorMessage = "Preencha o campo NOME")]
        public string NOME { get; set; }

        [StringLength(11, ErrorMessage = "O campo CPF permite no máximo 11 caracteres")]
        [Column("CPF", TypeName = "varchar(11)")]
        [Required(ErrorMessage = "Preencha o campo CPF")]
        public string CPF { get; set; }

        [Display(Name="DT. NASCIMENTO")]
        [Column("DATA_NASCIMENTO", TypeName = "date")]
        [Required(ErrorMessage = "Preencha o campo Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DATA_NASCIMENTO { get; set; }

        [StringLength(10, ErrorMessage = "O campo RG permite no máximo 10 caracteres")]
        [Column("RG", TypeName = "varchar(10)")]
        public string RG { get; set; }
    }
}

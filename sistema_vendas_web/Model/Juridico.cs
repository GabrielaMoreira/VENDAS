using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistema_vendas_web.Models.Cliente
{
    public class Juridico : Cliente
    {
        [Display(Name="RAZÃO SOCIAL")]
        [Column("RAZAO_SOCIAL", TypeName = "varchar(40)")]
        [Required(ErrorMessage = "Preencha o campo RAZÃO SOCIAL")]
        public string RAZAO_SOCIAL { get; set; }

        [StringLength(14, ErrorMessage = "O campo CNPJ permite no máximo 14 caracteres")]
        [Column("CNPJ", TypeName = "varchar(14)")]
        [Required(ErrorMessage = "Preencha o campo CNPJ")]
        public string CNPJ { get; set; }
        
        [RegularExpression(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", ErrorMessage = "Use apenas caracteres alfabéticos")]
        [Display(Name="NOME CONTATO")]
        [Column("NOME_CONTATO", TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Preencha o campo NOME CONTATO")]
        public string NOME_CONTATO { get; set; }

        [StringLength(14, ErrorMessage = "O campo INSCRIÇÃO ESTADUAL permite no máximo 14 caracteres")]
        [Display(Name="INSCRIÇÃO ESTADUAL")]
        [Column("INSCRICAO_ESTADUAL", TypeName = "varchar(14)")]
        public string INSCRICAO_ESTADUAL { get; set; }
    }
}

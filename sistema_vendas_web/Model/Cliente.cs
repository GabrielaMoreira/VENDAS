using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistema_vendas_web.Models.Cliente
{
    public class Cliente
    {
        [Key]
        [Column("COD_CLIENTE")]
        [DisplayName("COD")]
        public int COD_CLIENTE { get; set; }

        [Display(Name="DT. CADASTRO")]
        [DataType(DataType.Date)]
        public DateTime DATA_CADASTRO { get; set; }
        
        [Column("EMAIL", TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Preencha o campo E-MAIL")]
        public string EMAIL { get; set; }

        [Display(Name = "TELEFONE 1")]
        [Column("TELEFONE_1", TypeName = "varchar(14)")]
        public string TELEFONE_1 { get; set; }

        [Display(Name = "TELEFONE 2")]
        [Column("TELEFONE_2", TypeName = "varchar(14)")]
        public string TELEFONE_2 { get; set; }

        [Column("CEP", TypeName = "varchar(8)")]
        [Required(ErrorMessage = "Preencha o campo CEP")]
        public string CEP { get; set; }

        [Column("LOGRADOURO", TypeName = "varchar(40)")]
        [Required(ErrorMessage = "Preencha o campo LOGRADOURO")]
        public string LOGRADOURO { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Use apenas caracteres numéricos")]
        [Column("NUMERO", TypeName = "varchar(4)")]
        [Required(ErrorMessage = "Preencha o campo NUMERO")]
        public string NUMERO { get; set; }

        [Column("BAIRRO", TypeName = "varchar(30)")]
        [Required(ErrorMessage = "Preencha o campo BAIRRO")]
        public string BAIRRO { get; set; }

        [Column("COMPLEMENTO", TypeName = "varchar(20)")]
        public string COMPLEMENTO { get; set; }

        [Column("CIDADE", TypeName = "varchar(25)")]
        [Required(ErrorMessage = "Preencha o campo CIDADE")]
        public string CIDADE { get; set; }

        [Column("UF", TypeName = "varchar(2)")]
        [Required(ErrorMessage = "Preencha o campo UF")]
        public string UF { get; set; }

        [Column("STATUS", TypeName = "varchar(7)")]
        [Required(ErrorMessage = "O campo STATUS é obrigatório")]
        public string STATUS { get; set; }

       

    }
}

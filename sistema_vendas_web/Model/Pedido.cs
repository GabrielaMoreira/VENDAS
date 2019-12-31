using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistema_vendas_web.Models
{
    public class Pedido
    {
        [Key]
        [Column("COD_PEDIDO")]
        [DisplayName("COD")]
        public int COD_PEDIDO { get; set; }

        [Display(Name = "DT. EMISSÃO")]
        [DataType(DataType.Date)]
        public DateTime DATA_EMISSAO { get; set; }

        [Display(Name = "TABELA PREÇO")]
        [Column("TABELA_PRECO", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "O campo TABELA PREÇO é obrigatório")]
        public string TABELA_PRECO { get; set; }

        [Display(Name = "NT. OPERAÇÃO")]
        [Column("NATUREZA_OPERACAO", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "O campo NT. OPERAÇÃO é obrigatório")]
        public string NATUREZA_OPERACAO { get; set; }

        [Display(Name = "FORMA ENTREGA")]
        [Column("FORMA_ENTREGA", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "O campo FORMA ENTREGA é obrigatório")]
        public string FORMA_ENTREGA { get; set; }

        [Display(Name = "FORMA PAGAMENTO")]
        [Column("FORMA_PAGAMENTO", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "O campo FORMA PAGAMENTO é obrigatório")]
        public string FORMA_PAGAMENTO { get; set; }

        [Column("FRETE", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "O campo FRETE é obrigatório")]
        public string FRETE { get; set; }

        [RegularExpression("^[0-9]{1,2}([,.][0 - 9]{1, 2})?$", ErrorMessage = "Use apenas caracteres numéricos")]
        [Column("DESCONTO", TypeName = "decimal(7,2)")]
        [Required(ErrorMessage = "O campo DESCONTO é obrigatório")]
        public decimal DESCONTO { get; set; }

        [Display(Name = "QTD. PARCELA")]
        [Column("QTD_PARCELAS", TypeName = "int")]
        [Required(ErrorMessage = "O campo QTD. PARCELA é obrigatório")]
        public int QTD_PARCELAS { get; set; }

        [Display(Name = "SUB TOTAL")]
        [Column("SUB_TOTAL", TypeName = "decimal(7,2)")]
        public decimal SUB_TOTAL { get; set; }

        [Column("TOTAL", TypeName = "decimal(7,2)")]
        public decimal TOTAL { get; set; }

        [Display(Name = "OBSERVAÇÕES")]
        [Column("OBSERVACAO", TypeName = "varchar(180)")]
        public string OBSERVACAO { get; set; }

        [Column("STATUS", TypeName = "varchar(9)")]
        [Required(ErrorMessage = "Selecione um STATUS")]
        public string STATUS { get; set; }

        [Display(Name = "TIPO CLIENTE")]
        [Column("TIPO_CLIENTE", TypeName = "varchar(8)")]
        [Required(ErrorMessage = "Selecione um TIPO CLIENTE")]
        public string TIPO_CLIENTE { get; set; }


        [Column("COD_CLIENTE", TypeName = "int")]
        [Display(Name = "COD. CLIENTE")]
        public int COD_CLIENTE { get; set; }


        [Column("COD_USUARIO", TypeName = "int")]
        [Display(Name = "COD. USUARIO")]
        public int COD_USUARIO { get; set; }

        [Required]
        public string flag_item { get; set; }

    }
}

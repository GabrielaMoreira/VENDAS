using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistema_vendas_web.Models
{
    public class Produto
    {
        [Key]
        [Column("COD_PRODUTO")]
        [DisplayName("COD")]
        public int COD_PRODUTO { get; set; }

        [Column("DESCRICAO", TypeName = "varchar(180)")]
        [Required(ErrorMessage = "Preencha o campo DESCRIÇÃO")]
        public string DESCRICAO { get; set; }

        [Column("COR", TypeName = "varchar(50)")]
        public string COR { get; set; }
        
        [Column("UNIDADE_MEDIDA", TypeName = "varchar(50)")]
        [DisplayName("UN. MEDIDA")]
        public string UNIDADE_MEDIDA { get; set; }

        [RegularExpression("^[0-9]{1,2}([,.][0-9]{1,2})?$", ErrorMessage = "Use apenas caracteres numéricos")]
        [Column("VALOR_UNITARIO", TypeName = "decimal(7,2)")]
        [Required(ErrorMessage = "O campo VALOR UNITÁRIO é obrigatório")]
        [DisplayName("VALOR UNITÁRIO")]
        public decimal VALOR_UNITARIO { get; set; }

        
        [Column("QTD_ESTOQUE", TypeName = "int")]
        [Required(ErrorMessage = "O campo QTD. ESTOQUE é obrigatório")]
        [DisplayName("QTD. ESTOQUE")]
        public int QTD_ESTOQUE { get; set; }

        
        [Column("QTD_MIN_ESTOQUE", TypeName = "int")]
        [DisplayName("ESTOQUE MIN.")]
        public int QTD_MIN_ESTOQUE { get; set; }


        
        [Column("ALIQUOTA_IPI", TypeName = "decimal(7,2)")]
        [DisplayName("% IPI")]
        public decimal ALIQUOTA_IPI { get; set; }


        [RegularExpression("^[0-9]{1,2}([,.][0 - 9]{1, 2})?$", ErrorMessage = "Use apenas caracteres numéricos")]
        [Column("VALOR_IPI", TypeName = "decimal(7,2)")]
        [DisplayName("VALOR IPI")]
        public decimal VALOR_IPI { get; set; }

        [Column("STATUS", TypeName = "varchar(7)")]
        [Required(ErrorMessage = "Selecione um STATUS")]
        public string STATUS { get; set; }

        /* CAMPOS RELATORIO*/
        [Column("QTD_VENDIDA", TypeName = "int")]
        [DisplayName("QTD. VENDIDA")]
        public int QTD_VENDIDA { get; set; }

        [RegularExpression("^[0-9]{1,2}([,.][0-9]{1,2})?$", ErrorMessage = "Use apenas caracteres numéricos")]
        [Column("TOTAL_VENDIDO", TypeName = "decimal(7,2)")]        
        [DisplayName("VALOR TOTAL")]
        public decimal TOTAL_VENDIDO { get; set; }
    }
}

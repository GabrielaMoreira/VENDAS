using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistema_vendas_web.Models
{
    public class ItemPedido
    {
        
        [Key]
        [Column("COD_ITEM")]
        [DisplayName("COD. ITEM")]
        public int COD_ITEM { get; set; }

        [Column("COD_PRODUTO")]
        [DisplayName("COD. PRODUTO")]
        public int COD_PRODUTO { get; set; }

        [Column("PRECO_UNITARIO", TypeName = "decimal(7,2)")]
        [DisplayName("PREÇO UNIT.")]
        [Required(ErrorMessage = "O campo PREÇO UNIT. é obrigatório")]
        public decimal PRECO_UNITARIO { get; set; }

        [Column("QUANTIDADE", TypeName = "int")]
        [DisplayName("QTD.")]
        [Required(ErrorMessage = "O campo QTD. é obrigatório")]
        public int QUANTIDADE { get; set; }

        [Column("TOTAL", TypeName = "decimal(7,2)")]        
        [Required(ErrorMessage = "O campo TOTAL é obrigatório")]
        public decimal TOTAL { get; set; }

        [Column("COD_PEDIDO")]
        [DisplayName("COD. PEDIDO")]
        public int COD_PEDIDO { get; set; }
    }
}

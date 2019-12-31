using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistema_vendas_web.Models
{
    public class Usuario
    {
        [Key]
        [Column("COD_USUARIO")]
        [DisplayName("COD")]
        public int COD_USUARIO { get; set; }

        [StringLength(11, ErrorMessage = "O campo CPF permite no máximo 11 caracteres")]
        [Column("CPF", TypeName = "varchar(11)")]
        public string CPF { get; set; }

        [RegularExpression(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$", ErrorMessage = "Use apenas caracteres alfabéticos")]
        [Column("NOME", TypeName = "varchar(180)")]
        [Required(ErrorMessage = "Preencha o campo NOME")]
        public string NOME { get; set; }

        [Column("EMAIL", TypeName = "varchar(180)")]       
        [Required(ErrorMessage = "Preencha o campo  e-mail")]
        public string EMAIL { get; set; }

        [Column("LOGIN", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "O campo LOGIN é obrigatório")]
        public string LOGIN { get; set; }

        [StringLength(8, ErrorMessage = "O campo SENHA permite no máximo 8 caracteres")]
        [Column("SENHA", TypeName = "varchar(8)")]
        [Required(ErrorMessage = "O campo SENHA é obrigatório")]
        public string SENHA { get; set; }

        [Column("NIVEL_ACESSO", TypeName = "varchar(8)")]
        [Required(ErrorMessage = "Selecione um NIVEL de acesso")]
        [DisplayName("NIVEL")]
        public string NIVEL_ACESSO { get; set; }


        //REFERENCIA AO PEDIDO
        public virtual ICollection<Pedido> Pedidos { get; set; }

    }
}

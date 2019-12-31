using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistema_vendas_web.Models
{
    public class NotaFiscal
    {
        [Key]
        [Column("NUM_NOTA")]
        [DisplayName("NUM. NOTA")]
        public int NUM_NOTA { get; set; }

        [Column("SERIE")]
        [DisplayName("SERIE")]
        public int SERIE { get; set; }

        [Column("CHAVE_ACESSO")]
        [DisplayName("CHAVE_ACESSO")]
        public int CHAVE_ACESSO { get; set; }

        [Display(Name = "DT. EMISSÃO")]
        [DataType(DataType.Date)]
        public DateTime DATA_EMISSAO { get; set; }

        [Display(Name = "ESPECIE")]
        [Column("ESPECIE", TypeName = "varchar(2)")]
        [Required(ErrorMessage = "O campo espécie é obrigatório")]
        public string ESPECIE { get; set; }

        [Display(Name = "NATUREZA")]
        [Column("NATUREZA", TypeName = "varchar(25)")]
        [Required(ErrorMessage = "O campo natureza é obrigatório")]
        public string NATUREZA { get; set; }

        [Display(Name = "DT. ENTRADA")]
        [Column("DATA_ENTRADA", TypeName = "date")]
        [Required(ErrorMessage = "Preencha o campo Data Entrada")]
        [DataType(DataType.Date)]
        public DateTime DATA_ENTRADA { get; set; }

        [Display(Name = "DT. SAIDA")]
        [Column("DATA_SAIDA", TypeName = "date")]
        [Required(ErrorMessage = "Preencha o campo Data Saida")]
        [DataType(DataType.Date)]
        public DateTime DATA_SAIDA { get; set; }

        [Display(Name = "TIPO CLIENTE")]
        [Column("TIPO_CLIENTE", TypeName = "varchar(8)")]
        [Required(ErrorMessage = "Selecione um TIPO CLIENTE")]
        public string TIPO_CLIENTE { get; set; }

        [Column("COD_CLIENTE", TypeName = "int")]
        [Display(Name = "COD. CLIENTE")]
        public int COD_CLIENTE { get; set; }

        [Column("COD_PEDIDO", TypeName = "int")]
        [Display(Name = "NUM. PEDIDO")]
        public int COD_PEDIDO { get; set; }

        /*IMPOSTOS & TRIBUTOS*/

        [Display(Name = "BASE ICMS")]
        [Column("BASE_ICMS", TypeName = "decimal(7,2)")]
        public decimal BASE_ICMS { get; set; }

        [Display(Name = "VALOR ICMS")]
        [Column("VALOR_ICMS", TypeName = "decimal(7,2)")]
        public decimal VALOR_ICMS { get; set; }

        [Display(Name = "BASE ICMS_ST")]
        [Column("BASE_ICMS_ST", TypeName = "decimal(7,2)")]
        public decimal BASE_ICMS_ST { get; set; }

        [Display(Name = "VALOR ICMS_ST")]
        [Column("VALOR_ICMS_ST", TypeName = "decimal(7,2)")]
        public decimal VALOR_ICMS_ST { get; set; }

        [Display(Name = "VALOR PIS")]
        [Column("VALOR_PIS", TypeName = "decimal(7,2)")]
        public decimal VALOR_PIS { get; set; }

        [Display(Name = "VALOR FRETE")]
        [Column("VALOR_FRETE", TypeName = "decimal(7,2)")]
        public decimal VALOR_FRETE { get; set; }

        [Display(Name = "VALOR SEGURO")]
        [Column("VALOR_SEGURO", TypeName = "decimal(7,2)")]
        public decimal VALOR_SEGURO { get; set; }

        [Display(Name = "OUTRAS DESPESAS")]
        [Column("DESPESAS", TypeName = "decimal(7,2)")]
        public decimal DESPESAS { get; set; }

        [Display(Name = "VALOR COFINS")]
        [Column("VALOR_COFINS", TypeName = "decimal(7,2)")]
        public decimal VALOR_COFINS { get; set; }

        [Display(Name = "VALOR IPI")]
        [Column("VALOR_IPI", TypeName = "decimal(7,2)")]
        public decimal VALOR_IPI { get; set; }

        /*SUBTOTAL, TOTAL E DESCONTO SÃO BUSCADOS DO PEDIDO*/
        [Display(Name = "SUBTOTAL")]
        [Column("SUBTOTAL", TypeName = "decimal(7,2)")]
        public decimal SUBTOTAL { get; set; }

        [Display(Name = "TOTAL")]
        [Column("TOTAL", TypeName = "decimal(7,2)")]
        public decimal TOTAL { get; set; }

        [Display(Name = "DESCONTO")]
        [Column("DESCONTO", TypeName = "decimal(7,2)")]
        public decimal DESCONTO { get; set; }

        /*DADOS DA TRANSPORTADORA*/
        [StringLength(14, ErrorMessage = "O campo CNPJ permite no máximo 14 caracteres")]
        [Column("CNPJ", TypeName = "varchar(14)")]
        [Required(ErrorMessage = "Preencha o campo CNPJ")]
        public string CNPJ { get; set; }

        [StringLength(14, ErrorMessage = "O campo INSCRIÇÃO ESTADUAL permite no máximo 14 caracteres")]
        [Display(Name = "INSCRIÇÃO ESTADUAL")]
        [Column("INSCRICAO_ESTADUAL", TypeName = "varchar(14)")]
        public string INSCRICAO_ESTADUAL { get; set; }

        [Display(Name = "RAZÃO SOCIAL")]
        [Column("RAZAO_SOCIAL", TypeName = "varchar(40)")]
        [Required(ErrorMessage = "Preencha o campo RAZÃO SOCIAL")]
        public string RAZAO_SOCIAL { get; set; }

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

        [Column("BAIRRO", TypeName = "varchar(25)")]
        [Required(ErrorMessage = "Preencha o campo BAIRRO")]
        public string BAIRRO { get; set; }

        [Column("CIDADE", TypeName = "varchar(25)")]
        [Required(ErrorMessage = "Preencha o campo CIDADE")]
        public string CIDADE { get; set; }

        [Column("UF", TypeName = "varchar(2)")]
        [Required(ErrorMessage = "Preencha o campo UF")]
        public string UF { get; set; }

        [Display(Name = "ESPECIE TRANSPORTADORA")]
        [Column("ESPECIE_TRANSPORTADORA", TypeName = "varchar(40)")]
        [Required(ErrorMessage = "O campo espécie é obrigatório")]
        public string ESPECIE_TRANSPORTADORA { get; set; }

        [Display(Name = "PLACA VEICULO")]
        [Column("PLACA_VEICULO", TypeName = "varchar(8)")]
        [Required(ErrorMessage = "O campo espécie é obrigatório")]
        public string PLACA_VEICULO { get; set; }

        [Column("UF_VEICULO", TypeName = "varchar(2)")]
        [Required(ErrorMessage = "Preencha o campo UF Véiculo")]
        public string UF_VEICULO { get; set; }

        [Display(Name = "FRETE POR CONTA")]
        [Column("FRETE_CONTA", TypeName = "varchar(1)")]
        [Required(ErrorMessage = "Preencha o campo Frete por conta")]
        public string FRETE_CONTA { get; set; }

        [Display(Name = "FRETE TRANSPORTADORA")]
        [Column("FRETE_TRANSPORTADORA", TypeName = "decimal(7,2)")]
        [Required(ErrorMessage = "Preencha o campo Frete da transportadora")]
        public decimal FRETE_TRANSPORTADORA { get; set; }

        [Display(Name = "QTD. CARGA")]
        [Column("QTD_CARGA", TypeName = "int")]
        [Required(ErrorMessage = "O campo QTD. CARGA é obrigatório")]
        public int QTD_CARGA { get; set; }

        [Display(Name = "ESPECIE CARGA")]
        [Column("ESPECIE_CARGA", TypeName = "varchar(40)")]
        [Required(ErrorMessage = "O campo espécie é obrigatório")]
        public string ESPECIE_CARGA { get; set; }

        [Display(Name = "PESO BRUTO")]
        [Column("PESO_BRUTO", TypeName = "decimal(7,2)")]
        public decimal PESO_BRUTO { get; set; }

        [Display(Name = "PESO LÍQUIDO")]
        [Column("PESO_LIQUIDO", TypeName = "decimal(7,2)")]
        public decimal PESO_LIQUIDO { get; set; }

        [Display(Name = "INFORMAÇÕES COMPLEMENTARES")]
        [Column("INF_COMPLEMENTARES", TypeName = "varchar(300)")]
        public string INF_COMPLEMENTARES { get; set; }

        [Display(Name = "RESERVADO AO FISCO")]
        [Column("RESERVADO_FISCO", TypeName = "varchar(300)")]
        public string RESERVADO_FISCO { get; set; }

        [Column("STATUS", TypeName = "varchar(9)")]
        [Required(ErrorMessage = "Selecione um STATUS")]
        public string STATUS { get; set; }

    }
}

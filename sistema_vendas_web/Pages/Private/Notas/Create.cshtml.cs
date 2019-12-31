using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Notas
{
    public class CreateModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public CreateModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        //DECLARAÇÃO VARIAVÉIS
        [BindProperty]
        public NotaFiscal NotaFiscal { get; set; }        

        [BindProperty]
        public ItemNota ItemNota { get; set; }

        [BindProperty(SupportsGet = true)]
        public string subtotal { get; set; }

        [BindProperty(SupportsGet = true)]
        public string total { get; set; }

        [BindProperty(SupportsGet = true)]
        public string desconto { get; set; }

        [BindProperty(SupportsGet = true)]
        public string frete { get; set; }

        public List<string> lista_item { get; set; }

        [BindProperty(SupportsGet = true)]
        public string item { get; set; }

        struct Item
        {
            public int codigo_produto { get; set; }
            public string descricao { get; set; }
            public int quantidade { get; set; }
            public decimal preco_unitario { get; set; }
            public decimal total { get; set; }
        }
           
        // SALVAR NOTA
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var palavra = "";
            lista_item = new List<string>();

            /* Manipulando lista de itens pedido */
            for (int i = 0; i < item.Length; i++)
            {
                if (item[i] != ',')
                {
                    while (i != item.Length && item[i] != ',')
                    {
                        palavra += item[i];
                        i++;
                    }
                }
                lista_item.Add(palavra);
                palavra = null;
            }

            //SALVAR NOTA
            var DATAEMISSAO = DateTime.Now;
            int NUMERONOTA = 0;
            NotaFiscal.DATA_EMISSAO = DATAEMISSAO;
            NotaFiscal.SUBTOTAL = Convert.ToDecimal(subtotal.Replace(".", ","));
            NotaFiscal.TOTAL = Convert.ToDecimal(total.Replace(".", ","));
            NotaFiscal.DESCONTO = Convert.ToDecimal(desconto.Replace(".", ","));

            _context.NotaFiscais.Add(NotaFiscal);
            await _context.SaveChangesAsync();

            //SALVAR ITEM NOTA
            NUMERONOTA = _context.NotaFiscais.FirstOrDefault(p => p.DATA_EMISSAO.Equals(DATAEMISSAO)).NUM_NOTA;
            for (var i = 0; i < lista_item.Count; i += 5)
            {                
                ItemNota ITEMNOTA = new ItemNota();

                ITEMNOTA.COD_NOTA = NUMERONOTA;                
                ITEMNOTA.COD_PRODUTO = Convert.ToInt32(lista_item[i]);
                ITEMNOTA.PRECO_UNITARIO = Convert.ToDecimal(lista_item[i + 2].Replace(".", ","));
                ITEMNOTA.QUANTIDADE = Convert.ToInt32(lista_item[i + 3]);
                ITEMNOTA.TOTAL = Convert.ToDecimal(lista_item[i + 4].Replace(".", ","));                

                _context.ItemNotas.Add(ITEMNOTA);
                _context.SaveChanges();

            }
            return RedirectToPage("./Index");
        }


        //AUTOCOMPLETAR PEDIDO
        public IActionResult OnGetAutoComplete_Pedido()
        {
            var codigo = HttpContext.Request.Query["term"].ToString();
            var CODIGOPEDIDO = _context.Pedidos.Where(c => c.COD_PEDIDO == Convert.ToInt32(codigo)).Select(c => c.COD_PEDIDO).ToList();

            return new JsonResult(CODIGOPEDIDO);
        }

        //PREENCHER CODIGO DO CLIENTE COM BASE NO CODIGO DO PEDIDO
        public JsonResult OnGetPreencher_CodigoClientePedido(string codigo)
        {
            var CODCLIENTE = _context.Pedidos.Where(c => c.COD_PEDIDO == Convert.ToInt32(codigo)).Select(c => c.COD_CLIENTE).ToList();

            return new JsonResult(CODCLIENTE);
        }

        //PREENCHER TIPO DO CLIENTE COM BASE NO CODIGO DO PEDIDO
        public JsonResult OnGetPreencher_TipoClientePedido(string codigo)
        {
            var TIPOCLIENTE = _context.Pedidos.Where(c => c.COD_PEDIDO == Convert.ToInt32(codigo)).Select(c => c.TIPO_CLIENTE).ToList();

            return new JsonResult(TIPOCLIENTE);
        }

        //PREENCHER COM CNPJ/CPF DO CLIENTE COM BASE NO TIPO CLIENTE/PEDIDO      
        public JsonResult OnGetPreencher_CPFCNPJClientePedido(string codigo)
        {
            List<string> CNPJCPF = new List<string>();

            var TIPOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.TIPO_CLIENTE).Single();
            var CODIGOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.COD_CLIENTE).Single();
           
            if (TIPOCLIENTE == "Juridico")
            {

                CNPJCPF = _context.Juridicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.CNPJ).ToList();

            }
            if (TIPOCLIENTE == "Fisico")
            {
                CNPJCPF = _context.Fisicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.CPF).ToList();
            }

            return new JsonResult(CNPJCPF);
        }

        //PREENCHER COM NOME DO CLIENTE COM BASE NO TIPO CLIENTE/PEDIDO      
        public JsonResult OnGetPreencher_NomeCliente(string codigo)
        {            
            List<string> NOMECLIENTE = new List<string>();

            var TIPOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.TIPO_CLIENTE).Single();
            var CODIGOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.COD_CLIENTE).Single();

            if (TIPOCLIENTE == "Juridico")
            {

                NOMECLIENTE = _context.Juridicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.RAZAO_SOCIAL).ToList();

            }
            if (TIPOCLIENTE == "Fisico")
            {
                NOMECLIENTE = _context.Fisicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.NOME).ToList();
            }

            return new JsonResult(NOMECLIENTE);
        }        

        //PREENCHER COM TELEFONE DO CLIENTE COM BASE NO TIPO CLIENTE/PEDIDO        
        public JsonResult OnGetPreencher_TelefoneCliente(string codigo)
        {
            List<string> TELEFONE = new List<string>();

            var TIPOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.TIPO_CLIENTE).Single();
            var CODIGOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.COD_CLIENTE).Single();

            if (TIPOCLIENTE == "Juridico")
            {

                TELEFONE = _context.Juridicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.TELEFONE_1).ToList();

            }
            if (TIPOCLIENTE == "Fisico")
            {
                TELEFONE = _context.Fisicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.TELEFONE_1).ToList();
            }

            return new JsonResult(TELEFONE);
        }

        //PREENCHER COM CEP DO CLIENTE COM BASE NO TIPO CLIENTE/PEDIDO       
        public JsonResult OnGetPreencher_CepCliente(string codigo)
        {
            List<string> CEPCLIENTE = new List<string>();

            var TIPOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.TIPO_CLIENTE).Single();
            var CODIGOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.COD_CLIENTE).Single();

            if (TIPOCLIENTE == "Juridico")
            {

                CEPCLIENTE = _context.Juridicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.CEP).ToList();

            }
            if (TIPOCLIENTE == "Fisico")
            {
                CEPCLIENTE = _context.Fisicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.CEP).ToList();
            }

            return new JsonResult(CEPCLIENTE);
        }

        //PREENCHER COM LOGRADOURO DO CLIENTE COM BASE NO TIPO CLIENTE/PEDIDO        
        public JsonResult OnGetPreencher_LogradouroCliente(string codigo)
        {
            List<string> LOGRADOUROCLIENTE = new List<string>();

            var TIPOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.TIPO_CLIENTE).Single();
            var CODIGOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.COD_CLIENTE).Single();

            if (TIPOCLIENTE == "Juridico")
            {

                LOGRADOUROCLIENTE = _context.Juridicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.LOGRADOURO).ToList();

            }
            if (TIPOCLIENTE == "Fisico")
            {
                LOGRADOUROCLIENTE = _context.Fisicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.LOGRADOURO).ToList();
            }

            return new JsonResult(LOGRADOUROCLIENTE);
        }

        //PREENCHER COM NÚMERO DO CLIENTE COM BASE NO TIPO CLIENTE/PEDIDO        
        public JsonResult OnGetPreencher_NumeroCliente(string codigo)
        {
            List<string> NUMEROCLIENTE = new List<string>();

            var TIPOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.TIPO_CLIENTE).Single();
            var CODIGOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.COD_CLIENTE).Single();

            if (TIPOCLIENTE == "Juridico")
            {

                NUMEROCLIENTE = _context.Juridicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.NUMERO).ToList();

            }
            if (TIPOCLIENTE == "Fisico")
            {
                NUMEROCLIENTE = _context.Fisicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.NUMERO).ToList();
            }

            return new JsonResult(NUMEROCLIENTE);
        }

        //PREENCHER COM BAIRRO DO CLIENTE COM BASE NO TIPO CLIENTE/PEDIDO 
        public JsonResult OnGetPreencher_BairroCliente(string codigo)
        {
            List<string> BAIRROCLIENTE = new List<string>();

            var TIPOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.TIPO_CLIENTE).Single();
            var CODIGOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.COD_CLIENTE).Single();

            if (TIPOCLIENTE == "Juridico")
            {

                BAIRROCLIENTE = _context.Juridicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.BAIRRO).ToList();

            }
            if (TIPOCLIENTE == "Fisico")
            {
                BAIRROCLIENTE = _context.Fisicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.BAIRRO).ToList();
            }

            return new JsonResult(BAIRROCLIENTE);
        }

        //PREENCHER COM COMPLEMENTO DO CLIENTE COM BASE NO TIPO CLIENTE/PEDIDO        
        public JsonResult OnGetPreencher_ComplementoCliente(string codigo)
        {
            List<string> COMPLEMENTOCLIENTE = new List<string>();

            var TIPOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.TIPO_CLIENTE).Single();
            var CODIGOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.COD_CLIENTE).Single();

            if (TIPOCLIENTE == "Juridico")
            {

                COMPLEMENTOCLIENTE = _context.Juridicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.COMPLEMENTO).ToList();

            }
            if (TIPOCLIENTE == "Fisico")
            {
                COMPLEMENTOCLIENTE = _context.Fisicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.COMPLEMENTO).ToList();
            }

            return new JsonResult(COMPLEMENTOCLIENTE);
        }

        //PREENCHER COM CIDADE DO CLIENTE COM BASE NO TIPO CLIENTE/PEDIDO        
        public JsonResult OnGetPreencher_CidadeCliente(string codigo)
        {
            List<string> CIDADECLIENTE = new List<string>();

            var TIPOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.TIPO_CLIENTE).Single();
            var CODIGOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.COD_CLIENTE).Single();

            if (TIPOCLIENTE == "Juridico")
            {

                CIDADECLIENTE = _context.Juridicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.CIDADE).ToList();

            }
            if (TIPOCLIENTE == "Fisico")
            {
                CIDADECLIENTE = _context.Fisicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.CIDADE).ToList();
            }

            return new JsonResult(CIDADECLIENTE);
        }

        //PREENCHER COM UF DO CLIENTE COM BASE NO TIPO CLIENTE/PEDIDO        
        public JsonResult OnGetPreencher_UfCliente(string codigo)
        {
            List<string> UFCLIENTE = new List<string>();

            var TIPOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.TIPO_CLIENTE).Single();
            var CODIGOCLIENTE = _context.Pedidos.Where(t => t.COD_PEDIDO == Convert.ToInt32(codigo)).Select(t => t.COD_CLIENTE).Single();

            if (TIPOCLIENTE == "Juridico")
            {

                UFCLIENTE = _context.Juridicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.UF).ToList();

            }
            if (TIPOCLIENTE == "Fisico")
            {
                UFCLIENTE = _context.Fisicos.Where(c => c.COD_CLIENTE == Convert.ToInt32(CODIGOCLIENTE)).Select(c => c.UF).ToList();
            }

            return new JsonResult(UFCLIENTE);
        }



        /* ITENS DO PEDIDO */
        public JsonResult OnGetVisualizar_ItemPedido(int codigo)
        {            
            var COD_PRODUTO = _context.ItemPedidos.Where(c => c.COD_PEDIDO == codigo).Select(c => c.COD_PRODUTO).ToList();
            var QUANTIDADE = _context.ItemPedidos.Where(c => c.COD_PEDIDO == codigo).Select(c => c.QUANTIDADE).ToList();
            var PRECOUNITARIO = _context.ItemPedidos.Where(c => c.COD_PEDIDO == codigo).Select(c => c.PRECO_UNITARIO).ToList();
            var TOTAL = _context.ItemPedidos.Where(c => c.COD_PEDIDO == codigo).Select(c => c.TOTAL).ToList();

            var DESCRICAO = "";
            var linhas = COD_PRODUTO.Count;

            // Criando vetor da struct
            Item[] item = new Item[linhas];

            // adicionar itens uma a um em lista            
            for (int cont = 0; cont < linhas; cont++)
            {
                DESCRICAO = _context.Produtos.Where(c => c.COD_PRODUTO == COD_PRODUTO[cont]).Select(c => c.DESCRICAO).Single().ToString();

                item[cont].codigo_produto = COD_PRODUTO[cont];
                item[cont].descricao = DESCRICAO;
                item[cont].quantidade = QUANTIDADE[cont];
                item[cont].preco_unitario = PRECOUNITARIO[cont];
                item[cont].total = TOTAL[cont];
            }

            if(item.Length <= 0)
            {
                return new JsonResult("FALSE");
            }
            // transformar lista em json
            //var json = JsonConvert.SerializeObject(item); //versão 2.0
            var json = JsonSerializer.Serialize(item); //versão 3.0

            return new JsonResult(json);
        }
       

        //PREENCHER SUBTOTAL COM BASE NO CODIGO DO PEDIDO
        public JsonResult OnGetPreencher_SubtotalPedido(string codigo)
        {
            var SUBTOTAL = _context.Pedidos.Where(c => c.COD_PEDIDO == Convert.ToInt32(codigo)).Select(c => c.SUB_TOTAL).ToList();

            return new JsonResult(SUBTOTAL);
        }

        //PREENCHER TOTAL COM BASE NO CODIGO DO PEDIDO
        public JsonResult OnGetPreencher_TotalPedido(string codigo)
        {
            var TOTAL = _context.Pedidos.Where(c => c.COD_PEDIDO == Convert.ToInt32(codigo)).Select(c => c.TOTAL).ToList();

            return new JsonResult(TOTAL);
        }

        //PREENCHER DESCONTO COM BASE NO CODIGO DO PEDIDO
        public JsonResult OnGetPreencher_DescontoPedido(string codigo)
        {
            var DESCONTO = _context.Pedidos.Where(c => c.COD_PEDIDO == Convert.ToInt32(codigo)).Select(c => c.DESCONTO).ToList();

            return new JsonResult(DESCONTO);
        }

        //PREENCHER FRETE COM BASE NO CODIGO DO PEDIDO
        public JsonResult OnGetPreencher_FretePedido(string codigo)
        {
            var FRETE = _context.Pedidos.Where(c => c.COD_PEDIDO == Convert.ToInt32(codigo)).Select(c => c.FRETE).ToList();

            return new JsonResult(FRETE);
        }
    }
}

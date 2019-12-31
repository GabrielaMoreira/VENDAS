using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;
using Microsoft.AspNetCore.Http;

namespace sistema_vendas_web.Pages.Private.Pedidos
{
    public class CreateModel : PageModel
    {
        private readonly VendasContext _context;

        public CreateModel(VendasContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        //DECLARAÇÃO VARIAVÉIS
        [BindProperty]
        public Pedido Pedido { get; set; }

        [BindProperty]
        public ItemPedido ItemPedido { get; set; }        

        [BindProperty(SupportsGet = true)]
        public string total { get; set; }

        [BindProperty(SupportsGet = true)]
        public string subtotal { get; set; }

        [BindProperty(SupportsGet = true)]
        [RegularExpression(@"^[^,]*[^ ,][^,]*$")]
        public string desconto { get; set; }

        public List<string> lista_item { get; set; }

        [BindProperty(SupportsGet = true)]
        public string item { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            
            var palavra = "";
            lista_item = new List<string>();
            int estoque = 0;
            int qtd_vendida = 0;
            decimal total_vendido = 0;
            int QTDESTOQUE = 0;
            
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

            //SALVAR PEDIDO
            var DATAEMISSAO = DateTime.Now;
            int CODIGOPEDIDO = 0;
            Pedido.DATA_EMISSAO = DATAEMISSAO;
            Pedido.COD_USUARIO = Convert.ToInt32(HttpContext.Session.GetString("_ID"));
            Pedido.SUB_TOTAL = Convert.ToDecimal(subtotal.Replace(".", ","));
            Pedido.TOTAL = Convert.ToDecimal(total.Replace(".", ","));
            Pedido.DESCONTO = Convert.ToDecimal(desconto.Replace(".", ","));
            _context.Pedidos.Add(Pedido);
            await _context.SaveChangesAsync();

            //SALVAR ITEM PEDIDOS E ESTOQUE
            CODIGOPEDIDO = _context.Pedidos.FirstOrDefault(p => p.DATA_EMISSAO.Equals(DATAEMISSAO)).COD_PEDIDO;
            for (var i = 0; i < lista_item.Count; i += 4)
            {
                /* Item Pedido */
                ItemPedido ITEMPEDIDO = new ItemPedido();

                ITEMPEDIDO.COD_PEDIDO = CODIGOPEDIDO;
                ITEMPEDIDO.COD_PRODUTO = Convert.ToInt32(lista_item[i]);
                if(Pedido.TABELA_PRECO == "Atacado" && ((Convert.ToInt32(lista_item.Count) / 4) > 50))
                {
                    ITEMPEDIDO.PRECO_UNITARIO = Convert.ToDecimal(float.Parse(lista_item[i + 1].Replace(".", ",")) * float.Parse("0,97"));
                }
                else
                {
                    ITEMPEDIDO.PRECO_UNITARIO = Convert.ToDecimal(lista_item[i + 1].Replace(".", ","));
                }                
                ITEMPEDIDO.QUANTIDADE = Convert.ToInt32(lista_item[i + 2]);
                ITEMPEDIDO.TOTAL = Convert.ToDecimal(lista_item[i + 3].Replace(".", ","));
                QTDESTOQUE = _context.Produtos.FirstOrDefault(p => p.COD_PRODUTO == Convert.ToInt32(lista_item[i])).QTD_ESTOQUE;

                _context.ItemPedidos.Add(ITEMPEDIDO);
                _context.SaveChanges();

                /* Estoque */
                var produtos = _context.Produtos.Find(Convert.ToInt32(lista_item[i]));
                estoque = QTDESTOQUE - Convert.ToInt32(lista_item[i + 2]);
                produtos.QTD_ESTOQUE = estoque;

                /* Relatório Produto */
                qtd_vendida = _context.Produtos.FirstOrDefault(p => p.COD_PRODUTO == Convert.ToInt32(lista_item[i])).QTD_VENDIDA;
                total_vendido = _context.Produtos.FirstOrDefault(p => p.COD_PRODUTO == Convert.ToInt32(lista_item[i])).TOTAL_VENDIDO;
                produtos.QTD_VENDIDA = qtd_vendida + Convert.ToInt32(lista_item[i + 2]);
                produtos.TOTAL_VENDIDO = total_vendido + Convert.ToDecimal(lista_item[i + 3].Replace(".", ","));

                _context.Produtos.Update(produtos);
                _context.SaveChanges();

            }

            return RedirectToPage("./Index");
        }  


        //AUTOCOMPLETAR CLIENTE FISICO/JURIDICO      
        public IActionResult OnGetAutoComplete_Cliente()
        {

            var name = HttpContext.Request.Query["term"].ToString();
            var CNPJCLIENTE = _context.Juridicos.Where(c => c.CNPJ.Contains(name) && c.STATUS == "Ativo").Select(c => c.CNPJ).ToList();
            var CPFCLIENTE = _context.Fisicos.Where(c => c.CPF.Contains(name) && c.STATUS == "Ativo").Select(c => c.CPF).ToList();
            var LISTACLIENTES = CNPJCLIENTE.Concat(CPFCLIENTE).ToList();
            return new JsonResult(LISTACLIENTES);
        }

        //PREENCHER COM NOME DO CLIENTE COM BASE NO CPF/CNPJ ESCOLHIDO        
        public JsonResult OnGetPreencher_NomeCliente(string name, string type)
        {
            List<string> NOMECLIENTE = new List<string>();

            if (type == "Juridico")
            {
                
                NOMECLIENTE = _context.Juridicos.Where(c => c.CNPJ == name).Select(c => c.RAZAO_SOCIAL).ToList();
                
            }
            if (type == "Fisico")
            {
                NOMECLIENTE = _context.Fisicos.Where(c => c.CPF == name).Select(c => c.NOME).ToList();
            }

            return new JsonResult(NOMECLIENTE);
        }

        //PREENCHER COM CODIGO DO CLIENTE COM BASE NO CPF/CNPJ ESCOLHIDO        
        public JsonResult OnGetPreencher_CodigoCliente(string name, string type)
        {
            List<int> CODCLIENTE = new List<int>();

            if (type == "Juridico")
            {
                CODCLIENTE = _context.Juridicos.Where(c => c.CNPJ == name).Select(c => c.COD_CLIENTE).ToList();
            }
            if (type == "Fisico")
            {
                CODCLIENTE = _context.Fisicos.Where(c => c.CPF == name).Select(c => c.COD_CLIENTE).ToList();
            }

            return new JsonResult(CODCLIENTE);
        }

        //AUTOCOMPLETAR NOME PRODUTO      
        public IActionResult OnGetAutoComplete_NomeProduto()
        {
            var name = HttpContext.Request.Query["term"].ToString();
            var NOMEPRODUTO = _context.Produtos.Where(c => c.DESCRICAO.Contains(name) && c.STATUS == "Ativo").Select(c => c.DESCRICAO).ToList();
            
            return new JsonResult(NOMEPRODUTO);
        }

        //PREENCHER COM CODIGO DO PRODUTO      
        public JsonResult OnGetPreencher_CodigoProduto(string name)
        {                        
            var CODIGOPRODUTO = _context.Produtos.Where(c => c.DESCRICAO.Contains(name)).Select(c => c.COD_PRODUTO).ToList();

            return new JsonResult(CODIGOPRODUTO);
        }

        //PREENCHER COM ESTOQUE DO PRODUTO      
        public JsonResult OnGetPreencher_EstoqueProduto(string name)
        {
            var ESTOQUEPRODUTO = _context.Produtos.Where(c => c.DESCRICAO.Contains(name)).Select(c => c.QTD_ESTOQUE).ToList();

            return new JsonResult(ESTOQUEPRODUTO);
        }

        //PREENCHER COM VALOR UNITÁRIO DO PRODUTO      
        public JsonResult OnGetPreencher_UnitarioProduto(string name)
        {
            var UNITARIOPRODUTO = _context.Produtos.Where(c => c.DESCRICAO.Contains(name)).Select(c => c.VALOR_UNITARIO).ToList();

            return new JsonResult(UNITARIOPRODUTO);
        }


    }
}
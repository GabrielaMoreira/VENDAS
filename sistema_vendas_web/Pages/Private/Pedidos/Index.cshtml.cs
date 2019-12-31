using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Pedidos
{
    public class IndexModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public IndexModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        //DECLARAÇÃO VARIAVÉIS
        [BindProperty]
        public Pedido PedidoCSV { get; set; }

        [BindProperty]
        public ItemPedido ItemPedidoCSV { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        public IList<Pedido> Pedido { get; set; }

        public List<string> lista_pedido { get; set; }

        public List<string> lista_item { get; set; }


        //MÉTODO: MOSTRAR DADOS E RESULTADO DE FILTRO PESQUISA GRID 
        public async Task OnGetAsync()
        {
            var pedidos = from p in _context.Pedidos
                          select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                int number;
                bool success = Int32.TryParse(SearchString, out number);

                if(success == true)
                {
                    pedidos = pedidos.Where(s => s.COD_PEDIDO == Convert.ToInt32(SearchString));
                }
                
            }            
            if (!string.IsNullOrEmpty(Status))
            {
                pedidos = pedidos.Where(x => x.STATUS == Status);
            }

            Pedido = await pedidos.ToListAsync();
        }

        //MÉTODO: CANCELAR PEDIDO
        public async Task<IActionResult> OnGetCancelar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var pedidos = await _context.Pedidos.FindAsync(id);
            
            if (pedidos != null)
            {
                /* pedido */
                pedidos.STATUS = "Cancelado";                
                _context.Pedidos.Update(pedidos);
                await _context.SaveChangesAsync();
                                
                var CODIGOPRODUTO = _context.ItemPedidos.Where(p => p.COD_PEDIDO == id).Select(p => p.COD_PRODUTO).ToList();                

                for (int i = 0; i < CODIGOPRODUTO.Count; i++)
                {
                    /* Estoque */
                    int QUANTIDADEESTOQUE = _context.Produtos.FirstOrDefault(p => p.COD_PRODUTO == Convert.ToInt32(CODIGOPRODUTO[i])).QTD_ESTOQUE;
                    int QUANTIDADE = _context.ItemPedidos.FirstOrDefault(p => p.COD_PRODUTO == Convert.ToInt32(CODIGOPRODUTO[i]) && p.COD_PEDIDO == id).QUANTIDADE;
                    
                    var produtos = _context.Produtos.Find(CODIGOPRODUTO[i]);
                    int estoque = 0;
                    estoque = Convert.ToInt32(QUANTIDADEESTOQUE) + Convert.ToInt32(QUANTIDADE);

                    /* Relatorio Produto */
                    int qtd_vendida = 0;
                    decimal total_vendido = 0;
                    int QTDVENDIDA =  _context.Produtos.FirstOrDefault(p => p.COD_PRODUTO == Convert.ToInt32(CODIGOPRODUTO[i])).QTD_VENDIDA;
                    decimal TOTALVENDIDO = _context.Produtos.FirstOrDefault(p => p.COD_PRODUTO == Convert.ToInt32(CODIGOPRODUTO[i])).TOTAL_VENDIDO;
                    decimal TOTALITEM = _context.ItemPedidos.FirstOrDefault(p => p.COD_PRODUTO == Convert.ToInt32(CODIGOPRODUTO[i]) && p.COD_PEDIDO == id).TOTAL;
                    qtd_vendida = QTDVENDIDA - QUANTIDADE;
                    total_vendido = TOTALVENDIDO - TOTALITEM;


                    produtos.QTD_ESTOQUE = estoque;
                    produtos.QTD_VENDIDA = qtd_vendida;
                    produtos.TOTAL_VENDIDO = total_vendido;
                    _context.Produtos.Update(produtos);
                    await _context.SaveChangesAsync();
                }    
            }

            return RedirectToPage("./Index");
        }

        //MÉTODO: ATIVAR PEDIDO
        public async Task<IActionResult> OnGetAtivar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidos = await _context.Pedidos.FindAsync(id);

            if (pedidos != null)
            {
                pedidos.STATUS = "Ativo";
                _context.Pedidos.Update(pedidos);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        
        //MÉTODO IMPORTAR PEDIDO CSV
        public IActionResult OnGetImportar_Pedido(string file)
        {
            var result = "";
            var caminho = "C:\\VENDAS\\Pedido_CSV\\";

            string filePath = "";
            string csv = "";
            string palavra = "";
            lista_pedido = new List<string>();
            lista_item = new List<string>();

            /* Variáveis auxiliares */
            int CODIGOPEDIDO = 0;
            var DATAEMISSAO = DateTime.Now;            
            int CODIGOCLIENTE = 0;
            int CODIGOUSUARIO = Convert.ToInt32(HttpContext.Session.GetString("_ID"));
            int CODIGOPRODUTO = 0;

            decimal PRECOPRODUTO = 0;
            int QUANTIDADEESTOQUE = 0;
            float desconto = 0;
            float subtotal = 0;
            int qtdProdutos = 0;
            int estoque;
            int qtd_vendida = 0;
            decimal total_vendido = 0;
            double num;
            
            try
            {
                filePath = caminho + file;
                csv = System.IO.File.ReadAllText(filePath);
                
                /* Adicionando campos csv em lista_pedido.
                 * Obs.: 10 - new line; 13 - carriage return*/
                for (int i = 0; i < csv.Length; i++)
                {
                    while (i != csv.Length && csv[i] != ';' && csv[i] != 10 && csv[i] != 13)
                    {                        
                        palavra += csv[i];                        
                        i++;
                    }
                    
                    if (!String.IsNullOrEmpty(palavra))
                    {                        
                        lista_pedido.Add(palavra);
                    }   
                    palavra = null;
                }
                
                /* Pesquisando código do cliente */
                if (lista_pedido[5] == "Fisico")
                {
                    CODIGOCLIENTE = _context.Fisicos.FirstOrDefault(c => c.CPF.Equals(lista_pedido[1])).COD_CLIENTE;
                }
                else
                {
                    CODIGOCLIENTE = _context.Juridicos.FirstOrDefault(c => c.CNPJ.Equals(lista_pedido[1])).COD_CLIENTE;
                }

                /* Código cliente encontrado */
                if (CODIGOCLIENTE > 0)
                {
                    /* Adicionando campos lista_pedido em lista_item*/
                    for (int i = 15; i < (lista_pedido.Count - 2); i++)
                    {
                           
                        if (i%2 == 0 && double.TryParse(lista_pedido[i], out num))
                        {
                            CODIGOPRODUTO = _context.Produtos.FirstOrDefault(c => c.COD_PRODUTO == Convert.ToInt32(lista_pedido[i])).COD_PRODUTO;
                            PRECOPRODUTO = _context.Produtos.FirstOrDefault(c => c.COD_PRODUTO == Convert.ToInt32(lista_pedido[i])).VALOR_UNITARIO;
                            QUANTIDADEESTOQUE = _context.Produtos.FirstOrDefault(c => c.COD_PRODUTO == Convert.ToInt32(lista_pedido[i])).QTD_ESTOQUE;
                
                            /* Código produto encontrado */
                            if (CODIGOPRODUTO > 0)
                            {
                                /*codigo produto*/
                                lista_item.Add(lista_pedido[i]);
                                /*quantidade solicitada*/
                                if(QUANTIDADEESTOQUE > Convert.ToInt32(lista_pedido[i+2]))
                                {
                                    lista_item.Add(lista_pedido[i + 2]);
                                }
                                else
                                {
                                    result = "NULL";
                                    break;
                                }
                                /*preco unitario*/
                                lista_item.Add(Convert.ToString(PRECOPRODUTO));
                                /*quantidade estoque*/
                                lista_item.Add(Convert.ToString(QUANTIDADEESTOQUE));
                                /*total produto*/
                                lista_item.Add(Convert.ToString(PRECOPRODUTO * Convert.ToInt32(lista_pedido[i + 2])));
                
                                qtdProdutos += 1;
                                i += 2;
                            }
                            else
                            {
                                result = "FALSE";
                                break;
                            }                            
                        }                            
                    }

                    /* Calcular subtotal
                     * 
                       Regra de Negócio:
                       * Atacado e mais que 50 un. totais => desc. 3% no valor unit. de cada produto
                       * Atacado e menos que 50 un. totais => preco unit. de varejo
                       * Varejo => preco unit. cadastrado em sistema
                    */

                    if (result != "NULL" && result != "FALSE")
                    {                        
                        
                        if(lista_pedido[7] == "Atacado" && qtdProdutos > 50)
                        {
                            for (int i = 0; i < (lista_item.Count - 4); i++)
                            {
                                lista_item[i+4] = Convert.ToString(float.Parse(lista_item[i+4]) * float.Parse("0,97"));
                                subtotal += float.Parse(lista_item[i + 4]);
                                i += 4;
                            }
                        }                        
                        else
                        {
                            for (int i = 0; i < (lista_item.Count - 4); i++)
                            {
                                subtotal += float.Parse(lista_item[i + 4]);
                                i += 4;
                            }
                        }
                
                        //SALVAR PEDIDO
                        Pedido PEDIDO = new Pedido();

                        PEDIDO.COD_CLIENTE = CODIGOCLIENTE;
                        PEDIDO.COD_USUARIO = CODIGOUSUARIO;
                        PEDIDO.DATA_EMISSAO = DATAEMISSAO;

                        PEDIDO.TIPO_CLIENTE = lista_pedido[5];
                        PEDIDO.FORMA_ENTREGA = lista_pedido[9];
                        PEDIDO.FORMA_PAGAMENTO = lista_pedido[11];
                        PEDIDO.FRETE = lista_pedido[13];
                        PEDIDO.NATUREZA_OPERACAO = "Venda";
                        PEDIDO.QTD_PARCELAS = 1;
                        PEDIDO.TABELA_PRECO = lista_pedido[7];
                        PEDIDO.OBSERVACAO = lista_pedido[15];

                        desconto = 0; 
                        PEDIDO.DESCONTO = Convert.ToDecimal(desconto); 
                        PEDIDO.SUB_TOTAL = Convert.ToDecimal(subtotal);                        
                        PEDIDO.TOTAL = Convert.ToDecimal(Math.Round(subtotal, 2) - Math.Round(desconto, 2));
                        PEDIDO.STATUS = "Importado";
                        PEDIDO.flag_item = "S";
                        
                        
                        _context.Pedidos.Add(PEDIDO);
                        _context.SaveChanges();


                        //SALVAR ITEM PEDIDOS E ESTOQUE
                        CODIGOPEDIDO = _context.Pedidos.FirstOrDefault(p => p.DATA_EMISSAO.Equals(DATAEMISSAO)).COD_PEDIDO;
                        
                        for(int cont = 0; cont < lista_item.Count; cont++)
                        {
                            /* Item Pedido */
                            ItemPedido ITEMPEDIDO = new ItemPedido();

                            ITEMPEDIDO.COD_PEDIDO = CODIGOPEDIDO;
                            ITEMPEDIDO.COD_PRODUTO = Convert.ToInt32(lista_item[cont]);
                            ITEMPEDIDO.QUANTIDADE = Convert.ToInt32(lista_item[cont + 1]);
                            ITEMPEDIDO.PRECO_UNITARIO = Convert.ToDecimal(lista_item[cont + 2]);
                            ITEMPEDIDO.TOTAL = Convert.ToDecimal(lista_item[cont + 4]);

                            _context.ItemPedidos.Add(ITEMPEDIDO);
                            _context.SaveChanges();

                            /* Estoque */                
                            var produtos = _context.Produtos.Find(Convert.ToInt32(lista_item[cont]));
                            estoque = Convert.ToInt32(lista_item[cont + 3]) - Convert.ToInt32(lista_item[cont+1]);
                            produtos.QTD_ESTOQUE = estoque;

                            /* Relatório Produto */
                            qtd_vendida = _context.Produtos.FirstOrDefault(p => p.COD_PRODUTO == Convert.ToInt32(lista_item[cont])).QTD_VENDIDA;
                            total_vendido = _context.Produtos.FirstOrDefault(p => p.COD_PRODUTO == Convert.ToInt32(lista_item[cont])).TOTAL_VENDIDO;
                            produtos.QTD_VENDIDA = qtd_vendida + Convert.ToInt32(lista_item[cont + 1]);
                            produtos.TOTAL_VENDIDO = total_vendido + Convert.ToDecimal(lista_item[cont + 4].Replace(".", ","));

                            _context.Produtos.Update(produtos);
                            _context.SaveChanges();

                            cont += 4;
                        }
                        result = Convert.ToString(CODIGOPEDIDO);
                    }           
                }
                else
                {
                    result = "FALSE";                    
                }

            }
            catch
            {
                result = "FALSE";
            }

            return new JsonResult(result);
        }
    }
}

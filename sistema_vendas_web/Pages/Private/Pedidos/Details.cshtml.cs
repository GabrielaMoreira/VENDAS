using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Pedidos
{
    public class DetailsModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public DetailsModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        //DECLARAÇÃO VARIAVEIS
        public Pedido Pedido { get; set; }

        [Serializable]
        struct Item
        {
            public int codigo_produto { get; set; }
            public string descricao { get; set; }
            public int quantidade { get; set; }
            public decimal preco_unitario { get; set; }
            public decimal total { get; set; }
        }
        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pedido = await _context.Pedidos.FirstOrDefaultAsync(m => m.COD_PEDIDO == id);

            if (Pedido == null)
            {
                return NotFound();
            }
            return Page();
        }

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
            
            // transformar lista em json
            /*var json = JsonConvert.SerializeObject(item); //versão 2.0*/
            var json = JsonSerializer.Serialize(item); //versão 3.0           

            return new JsonResult(json);
        }
       
    }
}

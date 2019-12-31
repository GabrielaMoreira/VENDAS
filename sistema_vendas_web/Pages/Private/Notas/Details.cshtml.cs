using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Notas
{
    public class DetailsModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public DetailsModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        //DECLARAÇÃO VARIAVEIS
        public NotaFiscal NotaFiscal { get; set; }

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

            NotaFiscal = await _context.NotaFiscais.FirstOrDefaultAsync(m => m.NUM_NOTA == id);

            if (NotaFiscal == null)
            {
                return NotFound();
            }
            return Page();
        }

        public JsonResult OnGetVisualizar_ItemNota(int codigo)
        {
            var COD_PRODUTO = _context.ItemNotas.Where(c => c.COD_NOTA == codigo).Select(c => c.COD_PRODUTO).ToList();
            var QUANTIDADE = _context.ItemNotas.Where(c => c.COD_NOTA == codigo).Select(c => c.QUANTIDADE).ToList();
            var PRECOUNITARIO = _context.ItemNotas.Where(c => c.COD_NOTA == codigo).Select(c => c.PRECO_UNITARIO).ToList();
            var TOTAL = _context.ItemNotas.Where(c => c.COD_NOTA == codigo).Select(c => c.TOTAL).ToList();

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Produtos
{
    public class IndexModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public IndexModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        //DECLARAÇÃO VARIAVÉIS
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        public IList<Produto> Produto { get; set; }



        //MÉTODO: MOSTRAR DADOS E RESULTADO DE FILTRO PESQUISA GRID 
        public async Task OnGetAsync()
        {
            var produtos = from p in _context.Produtos
                           select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                produtos = produtos.Where(s => s.DESCRICAO.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(Status))
            {
                produtos = produtos.Where(x => x.STATUS == Status);
            }

            Produto = await produtos.ToListAsync();
        }

        //MÉTODO: INATIVAR PRODUTO
        public async Task<IActionResult> OnGetInativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos.FindAsync(id);

            if (produtos != null)
            {
                produtos.STATUS = "Inativo";
                _context.Produtos.Update(produtos);               
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        //MÉTODO: ATIVAR PRODUTO
        public async Task<IActionResult> OnGetAtivar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos.FindAsync(id);

            if (produtos != null)
            {
                produtos.STATUS = "Ativo";
                _context.Produtos.Update(produtos);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }


    }
}

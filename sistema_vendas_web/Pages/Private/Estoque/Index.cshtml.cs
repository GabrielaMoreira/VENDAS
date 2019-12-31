using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Estoque
{
    public class IndexModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public IndexModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        public IList<Produto> Produto { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var produtos = from p in _context.Produtos
                           select p;

            /*var produtosAtivos = from p in _context.Produtos
                           select p;*/

            if (!string.IsNullOrEmpty(SearchString))
            {
                produtos = produtos.Where(s => s.DESCRICAO.Contains(SearchString));
            }

            produtos = produtos.Where(x => x.STATUS == "Ativo");

            Produto = await produtos.ToListAsync();
            
        }
    }
}

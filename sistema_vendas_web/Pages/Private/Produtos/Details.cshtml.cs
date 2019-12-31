using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Produtos
{
    public class DetailsModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public DetailsModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        public Produto Produto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Produto = await _context.Produtos.FirstOrDefaultAsync(m => m.COD_PRODUTO == id);

            if (Produto == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

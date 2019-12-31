using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Models.Cliente;

namespace sistema_vendas_web.Pages.Private.Fisicos
{
    public class DetailsModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public DetailsModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        public Fisico Fisico { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fisico = await _context.Fisicos.FirstOrDefaultAsync(m => m.COD_CLIENTE == id);

            if (Fisico == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

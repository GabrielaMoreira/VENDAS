using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Models.Cliente;

namespace sistema_vendas_web.Pages.Private.Juridicos
{
    public class DetailsModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public DetailsModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        public Juridico Juridico { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Juridico = await _context.Juridicos.FirstOrDefaultAsync(m => m.COD_CLIENTE == id);

            if (Juridico == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

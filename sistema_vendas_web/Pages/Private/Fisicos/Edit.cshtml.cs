using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models.Cliente;

namespace sistema_vendas_web.Pages.Private.Fisicos
{
    public class EditModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public EditModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Fisico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FisicoExists(Fisico.COD_CLIENTE))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FisicoExists(int id)
        {
            return _context.Fisicos.Any(e => e.COD_CLIENTE == id);
        }
    }
}

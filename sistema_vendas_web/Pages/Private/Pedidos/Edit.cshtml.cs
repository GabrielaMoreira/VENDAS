using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Pedidos
{
    public class EditModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public EditModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pedido Pedido { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(Pedido.COD_PEDIDO))
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

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.COD_PEDIDO == id);
        }
    }
}

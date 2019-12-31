using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models.Cliente;

namespace sistema_vendas_web.Pages.Private.Fisicos
{
    public class CreateModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public CreateModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Fisico Fisico { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Fisico.DATA_CADASTRO = DateTime.Now;
            Fisico.STATUS = "Ativo";
            _context.Fisicos.Add(Fisico);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
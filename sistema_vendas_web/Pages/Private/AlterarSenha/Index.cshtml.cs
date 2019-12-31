using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.AlterarSenha
{
    public class IndexModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public IndexModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        public string SenhaAntiga { get; set; }

        [BindProperty]
        public string SenhaNova { get; set; }

        [BindProperty]
        public string Codigo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            if (Codigo == null)
            {
                return Page();
            }
           
            var usuario = await _context.Usuarios.FindAsync(Convert.ToInt32(Codigo));
            
            if (usuario != null)
            {                
                usuario.SENHA = SenhaNova;
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Private/Home/Home");
        }

        
    }
}

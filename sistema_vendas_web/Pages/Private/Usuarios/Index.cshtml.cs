using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public IndexModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        //DECLARAÇÃO VARIAVÉIS
        public IList<Usuario> Usuario { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }



        //MÉTODO: MOSTRAR DADOS E RESULTADO DE FILTRO PESQUISA GRID 
        public async Task OnGetAsync()
        {
            var usuarios = from u in _context.Usuarios
                           select u;

            if (!String.IsNullOrEmpty(SearchString))
            {
                usuarios = usuarios.Where(s => s.NOME.Contains(SearchString));
            }

            Usuario = await usuarios.ToListAsync();            
        }

        //MÉTODO: DELETAR USUÁRIO
        public async Task<IActionResult> OnGetDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.FindAsync(id);

            if (usuarios != null)
            {
                _context.Usuarios.Remove(usuarios);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

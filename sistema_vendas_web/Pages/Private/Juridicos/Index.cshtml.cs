using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models.Cliente;

namespace sistema_vendas_web.Pages.Private.Juridicos
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

        public IList<Juridico> Juridico { get;set; }



        //MÉTODO: MOSTRAR DADOS E RESULTADO DE FILTRO PESQUISA GRID 
        public async Task OnGetAsync()
        {
            var clientes = from p in _context.Juridicos
                           select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                clientes = clientes.Where(s => s.RAZAO_SOCIAL.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(Status))
            {
                clientes = clientes.Where(x => x.STATUS == Status);
            }

            Juridico = await clientes.ToListAsync();
        }

        //MÉTODO: INATIVAR CLIENTE JURIDICO
        public async Task<IActionResult> OnGetInativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Juridicos.FindAsync(id);

            if (clientes != null)
            {
                clientes.STATUS = "Inativo";
                _context.Juridicos.Update(clientes);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        //MÉTODO: ATIVAR CLIENTE JURIDICO
        public async Task<IActionResult> OnGetAtivar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.Juridicos.FindAsync(id);

            if (clientes != null)
            {
                clientes.STATUS = "Ativo";
                _context.Juridicos.Update(clientes);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Notas
{
    public class IndexModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public IndexModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? Data { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        public IList<NotaFiscal> NotaFiscal { get; set; }


        public async Task OnGetAsync()
        {
            var notas = from n in _context.NotaFiscais
                       select n;

            if (!string.IsNullOrEmpty(SearchString))
            {
                int number;
                bool success = Int32.TryParse(SearchString, out number);

                if (success == true)
                {
                    notas = notas.Where(s => s.NUM_NOTA == Convert.ToInt32(SearchString));
                }
            }
            if (Data != null)
            {
                notas = notas.Where(x => x.DATA_EMISSAO.Equals(Data));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                notas = notas.Where(d => d.STATUS.Equals(Status));
            }

            NotaFiscal = await notas.ToListAsync();
        }

        //MÉTODO: CANCELAR NOTA
        public async Task<IActionResult> OnGetCancelar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notas = await _context.NotaFiscais.FindAsync(id);

            if (notas != null)
            {
                notas.STATUS = "Cancelado";
                _context.NotaFiscais.Update(notas);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        //MÉTODO: ATIVAR NOTA
        public async Task<IActionResult> OnGetAtivar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notas = await _context.NotaFiscais.FindAsync(id);

            if (notas != null)
            {
                notas.STATUS = "Emitido";
                _context.NotaFiscais.Update(notas);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

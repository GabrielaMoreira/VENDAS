using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;


namespace sistema_vendas_web.Pages.Private.Relatorios
{
    public class RelatorioNotaModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public RelatorioNotaModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        //DECLARAÇÃO VARIAVÉIS
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? Inicial { get; set; }       

        public IList<NotaFiscal> NotaFiscal { get; set; }


        //MÉTODO: MOSTRAR DADOS E RESULTADO DE FILTRO PESQUISA GRID 
        public async Task OnGetAsync()
        {
            var notas = from p in _context.NotaFiscais
                          select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                int number;
                bool success = Int32.TryParse(SearchString, out number);

                if (success == true)
                {
                    notas = notas.Where(s => s.NUM_NOTA == Convert.ToInt32(SearchString));
                }
            }
            if (!string.IsNullOrEmpty(Status))
            {
                notas = notas.Where(x => x.STATUS == Status);
            }
            if (Inicial != null)
            {
                notas = notas.Where(x => x.DATA_EMISSAO >= Convert.ToDateTime(Inicial).AddHours(00).AddMinutes(00).AddSeconds(00) && x.DATA_EMISSAO <= Convert.ToDateTime(Inicial).AddHours(23).AddMinutes(59).AddSeconds(59));
            }

            NotaFiscal = await notas.ToListAsync();
        }
    }
}
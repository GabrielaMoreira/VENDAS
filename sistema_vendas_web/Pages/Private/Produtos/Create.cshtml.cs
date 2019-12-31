using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Produtos
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

        //DECLARAÇÃO VARIAVÉIS
        [BindProperty]
        public Produto Produto { get; set; }

        [BindProperty(SupportsGet = true)]
        [RegularExpression("^[0-9]{1,2}([,.][0-9]{1,2})?$", ErrorMessage = "Use apenas caracteres numéricos")]
        public string valor_unitaro { get; set; }

        [BindProperty(SupportsGet = true)]
        [RegularExpression("^[0-9]{1,2}([,.][0-9]{1,2})?$", ErrorMessage = "Use apenas caracteres numéricos")]
        public string aliquota_ipi { get; set; }

        [BindProperty(SupportsGet = true)]
        [RegularExpression("^[0-9]{1,2}([,.][0-9]{1,2})?$", ErrorMessage = "Use apenas caracteres numéricos")]
        public string valor_ipi { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Produto.VALOR_UNITARIO = Convert.ToDecimal(valor_unitaro.Replace(".", ","));
            Produto.VALOR_IPI = Convert.ToDecimal(valor_ipi.Replace(".", ","));
            Produto.ALIQUOTA_IPI = Convert.ToDecimal(aliquota_ipi.Replace(".", ","));
            Produto.QTD_VENDIDA = 0;
            Produto.TOTAL_VENDIDO = 0;
            Produto.STATUS = "Ativo";
            _context.Produtos.Add(Produto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
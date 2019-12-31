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
    public class RelatorioProdutoModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public RelatorioProdutoModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        //DECLARAÇÃO VARIAVÉIS
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        public IList<Produto> Produto { get; set; }


        //MÉTODO: MOSTRAR DADOS E RESULTADO DE FILTRO PESQUISA GRID 
        public async Task OnGetAsync()
        {
            var produtos = from p in _context.Produtos
                          select p;

            if (!string.IsNullOrEmpty(Status))
            {
                produtos = produtos.Where(x => x.STATUS == Status);
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                produtos = produtos.Where(s => s.DESCRICAO.Contains(SearchString));
            }

            Produto = await produtos.ToListAsync();
        }
    }
}
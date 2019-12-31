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
    public class RelatorioPedidoModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public RelatorioPedidoModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

        //DECLARAÇÃO VARIAVÉIS
        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? Inicial { get; set; }
        
        [BindProperty(SupportsGet = true)]
        [DataType(DataType.Date)]
        public DateTime? Final { get; set; }

        public IList<Pedido> Pedido { get; set; }


        //MÉTODO: MOSTRAR DADOS E RESULTADO DE FILTRO PESQUISA GRID 
        public async Task OnGetAsync()
        {
            var pedidos = from p in _context.Pedidos
                          select p;            

            if (!string.IsNullOrEmpty(Status))
            {
                pedidos = pedidos.Where(x => x.STATUS == Status);
            }           
            if (Inicial != null && Final != null)
            {              
                pedidos = pedidos.Where(x => x.DATA_EMISSAO >= Convert.ToDateTime(Inicial).AddHours(00).AddMinutes(00).AddSeconds(00) && x.DATA_EMISSAO <= Convert.ToDateTime(Final).AddHours(23).AddMinutes(59).AddSeconds(59));
            }            

            Pedido = await pedidos.ToListAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Account
{
    public class AutenticacaoUsuarioModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public AutenticacaoUsuarioModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }                    
        
        [BindProperty]
        [Required]
        public string Login {get; set;}

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Senha {get; set;}

        const string SessionID = "_ID";
        const string SessionLogin = "_Login";
        const string SessionName = "_Name";
        const string SessionPassword = "_Password";
        const string SessionRole = "_Role";


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            { 
                var LOGIN = _context.Usuarios.Where(c => c.LOGIN == Login).Select(c => c.LOGIN).Single().ToString();
                var SENHA = _context.Usuarios.Where(c => c.LOGIN == Login).Select(c => c.SENHA).Single().ToString();
               
                if (Login == LOGIN && Senha == SENHA )
                {
                    var NOME = _context.Usuarios.Where(c => c.LOGIN == Login).Select(c => c.NOME).Single().ToString();
                    var CODIGO = _context.Usuarios.Where(c => c.LOGIN == Login).Select(c => c.COD_USUARIO).Single().ToString();
                    var NIVEL = _context.Usuarios.Where(c => c.LOGIN == Login).Select(c => c.NIVEL_ACESSO).Single().ToString();

                    HttpContext.Session.SetString(SessionID, CODIGO);
                    HttpContext.Session.SetString(SessionLogin, LOGIN);
                    HttpContext.Session.SetString(SessionName, NOME); 
                    HttpContext.Session.SetString(SessionPassword, SENHA);
                    HttpContext.Session.SetString(SessionRole, NIVEL);

                    return RedirectToPage("/Private/Home/Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login ou senha inválidos!");
                    return Page();
                }                
                
            }
            else
            {
                ModelState.AddModelError("", "Prencha todos os campos corretamente!");
                return Page();
            } 
        }

        public IActionResult OnGet()
        {
            return Page();
        }
    }
}

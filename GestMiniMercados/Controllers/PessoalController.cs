using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestMiniMercados.Data;
using GestMiniMercados.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestMiniMercados.Controllers
{
    public class PessoalController : Controller
    {
        private readonly DatabaseContext _context;

        public PessoalController(DatabaseContext context)
        {
            _context = context;
        }
        
            public IActionResult Ferias(Feria ferias_func)
            {
                if (Request.Cookies["Sessao"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    try
                    {
                        ViewBag.NomeFunc = Request.Cookies["Sessao"];
        
                        if (ferias_func.DataInicioFerias.ToShortDateString() != "01/01/0001")
                        {
                            ferias_func.NContribuinte = (_context.Users.FirstOrDefault(_func => _func.NomeFuncionario == Request.Cookies["Sessao"])).NContribuinte;
        
                            _context.Ferias.Add(ferias_func);
                            _context.SaveChanges();
                        }
        
                        IEnumerable<Feria> ferias = _context.Ferias.ToList().Where(_ferias => (_context.Users.FirstOrDefault
                        (_func => _func.NomeFuncionario == Request.Cookies["Sessao"]))
                        .NContribuinte == _ferias.NContribuinte);
        
                        ViewBag.Ferias = ferias;
                    }
                    catch (Exception ex)
                    {
                        return View("404Error");
                    }
                }
        
                return View();
            }
        
            public IActionResult Perfil(User user)
            {
                if (Request.Cookies["Sessao"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    try
                    {
                        User info_user_logged;
        
                        if (user.NomeFuncionario != null)
                        {
                            info_user_logged = _context.Users.FirstOrDefault(_func => _func.NomeFuncionario == Request.Cookies["Sessao"]);
        
                            info_user_logged.NomeFuncionario= user.NomeFuncionario;
                            info_user_logged.DataNascimento= user.DataNascimento;
                            info_user_logged.EstadoCivil= user.EstadoCivil;
                            info_user_logged.Morada = user.Morada;
                            info_user_logged.Telemovel = user.Telemovel;
        
                            Response.Cookies.Delete("Sessao");
        
                            CookieOptions option = new CookieOptions { Expires = DateTime.Now.AddDays(1) };
        
                            Response.Cookies.Append("Sessao", user.NomeFuncionario, option);
        
                            _context.SaveChanges();
        
                            info_user_logged = _context.Users.FirstOrDefault(_func => _func.NomeFuncionario == user.NomeFuncionario);
        
                            ViewBag.NomeFunc = user.NomeFuncionario;
                        }
                        else
                        {
                            info_user_logged = _context.Users.FirstOrDefault(_func => _func.NomeFuncionario == Request.Cookies["Sessao"]);
        
                            ViewBag.NomeFunc = Request.Cookies["Sessao"];
                        }
        
                        ViewBag.UserLogged = info_user_logged;
                    }
                    catch (Exception ex)
                    {
                        return View("404Error");
                    }
                }
        
                return View();
            }
        }
}

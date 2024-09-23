using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestMiniMercados.Data;
using GestMiniMercados.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestMiniMercados.Controllers
{
    public class RHFController : Controller
    {
        private readonly DatabaseContext _context;

        public RHFController(DatabaseContext context)
        {
            _context = context;
        }

            public IActionResult Funcionarios(User func_para_registo)
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
        
                        if (func_para_registo.NomeFuncionario!= null)
                        {
                            func_para_registo.DataContrato= DateTime.Today;
                            func_para_registo.TpUtilizador= "Funcionário";
        
                            _context.Users.Add(func_para_registo);
                            _context.SaveChanges();
                        }
        
                        IEnumerable<User> funcionarios = _context.Users.ToList();
                        IEnumerable<HorarioDeTrabalho> horarioDeTrabalhos;
        
                        List<User> funcionarios_mes = new List<User>();
        
                        User user_com_login = _context.Users.FirstOrDefault(_func => _func.NomeFuncionario == Request.Cookies["Sessao"]);
        
                        double horas = 0;
        
                        foreach(User func in funcionarios)
                        {
                            horarioDeTrabalhos = _context.HorarioDeTrabalhos.ToList().Where(_hor => _hor.NContribuinte == func.NContribuinte && _hor.DataInicioTrabalho.Month == DateTime.Today.Month);
        
                            foreach(HorarioDeTrabalho horarioDeTrabalho in horarioDeTrabalhos)
                                horas += horarioDeTrabalho.DataFimTrabalho.Subtract(horarioDeTrabalho.DataInicioTrabalho).TotalHours;
        
                            if(horas >= (8.0 * horarioDeTrabalhos.Count()))
                                funcionarios_mes.Add(func);
                        }
        
                        ViewBag.Funcionarios = funcionarios;
                        ViewBag.FuncionarioComLogin = user_com_login;
                        ViewBag.FuncionariosDoMes = funcionarios_mes;
                    }
                    catch(Exception ex)
                    {
                        return View("404Error");
                    }
                }
        
                return View();
            }
        
            public IActionResult Financas()
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
        
                        IEnumerable<User> funcionarios = _context.Users.ToList();
        
                        double lucro = _context.Produtos.ToList().Sum(_prod => (_prod.QtdTotal * _prod.MargemUnitaria));
        
                        ViewBag.LucroMensal = lucro;
                        ViewBag.Funcionarios = funcionarios;
                    }
                    catch (Exception ex)
                    {
                        return View("404Error");
                    }
                }
        
                return View();
            }
        
            public IActionResult _modalRegistarFuncionario()
            {
                return View();
            }
        }
    }
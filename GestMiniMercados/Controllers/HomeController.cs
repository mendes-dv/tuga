using System.Diagnostics;
using GestMiniMercados.Data;
using GestMiniMercados.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestMiniMercados.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(UserLogin usr)
        {
            string temp = System.Web.HttpUtility.HtmlDecode("Joa&#225;o");
    
            if (Request.Cookies["Sessao"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                if (usr.Password != null)
                {
                    try
                    {
                        User user = _context.Users.FirstOrDefault(_user => _user.NContribuinte == usr.NContribuinte && _user.Password == usr.Password);
    
                        if (user != null)
                        {
                            CookieOptions option = new CookieOptions { Expires = DateTime.Now.AddDays(1) };
    
                            Response.Cookies.Append("Sessao", user.NomeFuncionario, option);
                            Response.Cookies.Append("Inicio_Sessao", DateTime.Now.ToString(), option);
    
                            return RedirectToAction("Dashboard");
                        }
                        else
                            ViewBag.Mensagem = "Número de contribuinte ou Password errada!";
                    }
                    catch (Exception ex)
                    {
                        return View("Login");
                    }
                }
    
                return View("Login");
            }
        }
    
        public IActionResult Dashboard()
        {
            if (Request.Cookies["Sessao"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.NomeFunc = Request.Cookies["Sessao"];
    
                try
                {
                    IEnumerable<Categorium> categorias = _context.Categoria.ToList();
                    IEnumerable<Produto> produtos;
                    IEnumerable<Entrega> entregas = _context.Entregas.ToList().Where(_ent => _ent.DataEntrega > DateTime.Today && _ent.DataEntrega < DateTime.Today.AddDays(7));
    
                    List<CategoriaPerc> categoriaPercs = new List<CategoriaPerc>();
                    List<InfoEntrega> info_entregas = GetInfoEntregas(entregas);
    
                    double total_produtos = 0.0;
                    double total_qtdTotal = 0.0;
    
                    double perc_categoria = 0.0;
    
                    double lucro = 0.0;
    
                    foreach(Categorium cat in categorias)
                    {
                        produtos = _context.Produtos.ToList().Where(_prod => _prod.IdCategoria == cat.IdCategoria);
    
                        foreach (Produto prod in produtos)
                        {
                            total_produtos += prod.QtdProduto;
                            total_qtdTotal += prod.QtdTotal;
    
                            lucro += prod.QtdProduto * prod.MargemUnitaria;
                        }
    
                        if (total_qtdTotal != 0)
                            perc_categoria = (total_produtos / total_qtdTotal) * 100;
                        else
                            perc_categoria = 0;
    
                        categoriaPercs.Add(new CategoriaPerc { NomeCategoria= cat.NomeCategoria, Percentagem = Math.Round(perc_categoria), Lucro = lucro });
    
                        total_produtos = 0.0;
                        total_qtdTotal = 0.0;
                        perc_categoria = 0.0;
                        lucro = 0.0;
                    }
    
                    ViewBag.CategoriasPercs = categoriaPercs;
                    ViewBag.Entregas = info_entregas;
                }
                catch (Exception ex)
                {
                    return View("404Error");
                }
    
                return View();
            }
        }
    
        private List<InfoEntrega> GetInfoEntregas(IEnumerable<Entrega> entregas)
        {
            List<InfoEntrega> info_entregas = new List<InfoEntrega>();
    
            foreach (Entrega etr in entregas)
            {
                info_entregas.Add(new InfoEntrega
                {
                    NomeFornecedor= _context.Fornecedors.FirstOrDefault(_forn => _forn.IdFornecedor == etr.IdFornecedor).NomeFornecedor,
                    DataEntrega= DateOnly.FromDateTime(etr.DataEntrega),
                    NomeProduto= _context.Produtos.FirstOrDefault(_prod => _prod.IdProduto== etr.IdProduto).NomeProduto
                });
            }
    
            return info_entregas;
        }
    
        public IActionResult Logout()
        {
            if (Request.Cookies["Sessao"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _context.HorarioDeTrabalhos.Add(new HorarioDeTrabalho
                {
                    NContribuinte= _context.Users.FirstOrDefault(_func => _func.NomeFuncionario == Request.Cookies["Sessao"]).NContribuinte,
                    DataInicioTrabalho= Convert.ToDateTime(Request.Cookies["Inicio_Sessao"]),
                    DataFimTrabalho= DateTime.Now
                });
                _context.SaveChanges();
    
                Response.Cookies.Delete("Sessao");
                Response.Cookies.Delete("Inicio_Sessao");
    
                return RedirectToAction("Index");
            }
        }
    
        public IActionResult Privacy()
        {
            return View();
        }
    
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
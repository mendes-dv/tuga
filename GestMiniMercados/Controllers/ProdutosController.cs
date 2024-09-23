using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GestMiniMercados.Data;
using GestMiniMercados.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace GestMiniMercados.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly DatabaseContext _context;

        public ProdutosController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Categorias(string id, Fornecedor fornecedor)
        {
            if (Request.Cookies["Sessao"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.NomeFunc = Request.Cookies["Sessao"];
    
                try
                {
                    if (fornecedor.NomeFornecedor != null)
                    {
                        _context.Fornecedors.Add(fornecedor);
                        _context.SaveChanges();
                    }
    
                    IEnumerable<Categorium> categorias = _context.Categoria.ToList();
                    IEnumerable<Produto> produtos;
                    IEnumerable<Fornecedor> fornecedores = _context.Fornecedors.ToList();
    
                    List<CategoriaPerc> categoriaPercs = new List<CategoriaPerc>();
                    List<InfoFornecedore> infoFornecedores = GetInfoFornecedores(fornecedores);
                    List<InfoFornecedore> infoFornecedoresCat = GetInfoFornecedores(fornecedores, id);
                    List<IvaCategorium> iva_per_categoria = new List<IvaCategorium>();
                    List<ProdsPerCat> prodsPerCats = new List<ProdsPerCat>();
                    List<InfoProd> infoProds = new List<InfoProd>();
    
                    double total_produtos = 0.0;
                    double total_qtdTotal = 0.0;
    
                    double perc_categoria = 0.0;
    
                    foreach (Categorium cat in categorias)
                    {
                        produtos = _context.Produtos.ToList().Where(_prod => _prod.IdCategoria == cat.IdCategoria);
    
                        if (cat.NomeCategoria == id.Replace('_', ' '))
                            infoProds = GetInfoProds(produtos);
    
                        foreach (Produto prod in produtos)
                        {
                            total_produtos += prod.QtdProduto;
                            total_qtdTotal += prod.QtdTotal;
                        }
    
                        if (total_qtdTotal != 0)
                            perc_categoria = (total_produtos / total_qtdTotal) * 100;
                        else
                            perc_categoria = 0;
    
                        categoriaPercs.Add(new CategoriaPerc { NomeCategoria = cat.NomeCategoria, Percentagem = Math.Round(perc_categoria), Lucro = 0.0 });
    
                        iva_per_categoria.Add(new IvaCategorium { NomeCategoria = cat.NomeCategoria, IvaCategoria= cat.Iva });
    
                        prodsPerCats.Add(new ProdsPerCat { NomeCategoria = cat.NomeCategoria, QtdProds= produtos.Count() });
    
                        total_produtos = 0.0;
                        total_qtdTotal = 0.0;
                        perc_categoria = 0.0;
                    }
    
                    ViewBag.CategoriasPercs = categoriaPercs;
                    ViewBag.InfoFornecedores = infoFornecedores;
                    ViewBag.Ivas = iva_per_categoria;
                    ViewBag.ProdsPerCat = prodsPerCats;
                    ViewBag.InfoProds = infoProds;
                    ViewBag.InfoFornecedoresCat = infoFornecedoresCat;
                    ViewBag.Categorias = categorias;
                }
                catch (Exception ex)
                {
                    return View("404Error");
                }
    
                if(id == "All")
                    return View();
                else
                    return View("CatSpec", id.Replace('_', ' '));
            }
        }
    
        public IActionResult _modalRegistarFornecedor()
        {
            return View();
        }
    
        public IActionResult Produtos(Produto produto_para_registo)
        {
            if (Request.Cookies["Sessao"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.NomeFunc = Request.Cookies["Sessao"];
    
                try
                {
                    if (produto_para_registo.NomeProduto != null)
                    {
                        _context.Produtos.Add(produto_para_registo);
                        _context.SaveChanges();
                    }
    
                    IEnumerable<InfoProd> produtos = GetInfoProds(_context.Produtos.ToList());
                    IEnumerable<Categorium> cats = _context.Categoria.ToList();
    
                    ViewBag.Produtos = produtos;
                    ViewBag.Categorias = cats;
                }
                catch(Exception ex)
                {
                    return View("404Error");
                }
            }
    
            return View();
        }
    
        public IActionResult _modalRegistarProduto()
        {
            return View();
        }
    
        private List<InfoProd> GetInfoProds(IEnumerable<Produto> produtos)
        {
            List<InfoProd> infoProds = new List<InfoProd>();
            
            foreach(Produto prod in produtos)
            {
                infoProds.Add(new InfoProd
                {
                    NomeProduto= prod.NomeProduto,
                    MargemLucro= prod.MargemUnitaria,
                    QtdAtualProduto= prod.QtdProduto,
                    QtdMaximaProduto= prod.QtdTotal,
                    PreçoVenda= prod.PrecoUnitarioVenda,
                    PreçoCompra= prod.PrecoUnitarioCompra,
                    PreçoCampanha =  prod.PrecoCampanha
                });
            }
    
            return infoProds;
        }
    
        private List<InfoFornecedore> GetInfoFornecedores(IEnumerable<Fornecedor> fornecedores)
        {
            List<InfoFornecedore> infoFornecedores = new List<InfoFornecedore>();
    
            foreach (Fornecedor forn in fornecedores)
            {
                infoFornecedores.Add(new InfoFornecedore()
                {
                    NomeFornecedor= forn.NomeFornecedor,
                    NomeCategoriaFornecida= _context.Categoria.FirstOrDefault(_cat => _cat.IdCategoria == forn.IdCategoria).NomeCategoria,
                    Morada = forn.Morada,
                    NContribuinte= forn.NContribuinte
                });
            }
    
            return infoFornecedores;
        }
    
        private List<InfoFornecedore> GetInfoFornecedores(IEnumerable<Fornecedor> fornecedores, string id)
        {
            List<InfoFornecedore> infoFornecedores = new List<InfoFornecedore>();
    
            foreach (Fornecedor forn in fornecedores)
            {
                if(_context.Categoria.FirstOrDefault(_cat => _cat.IdCategoria == forn.IdCategoria).NomeCategoria == id.Replace('_', ' '))
                {
                    infoFornecedores.Add(new InfoFornecedore() 
                    {
                        NomeFornecedor= forn.NomeFornecedor,
                        NomeCategoriaFornecida= _context.Categoria.FirstOrDefault(_cat => _cat.IdCategoria == forn.IdCategoria).NomeCategoria,
                        Morada = forn.Morada,
                        NContribuinte= forn.NContribuinte
                    });
                }
            }
    
            return infoFornecedores;
        }
    }
}

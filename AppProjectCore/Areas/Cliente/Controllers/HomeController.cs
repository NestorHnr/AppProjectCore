using AppProjectCore.AccesoDatos.Data.Repository.IRepository;
using AppProjectCore.Models;
using AppProjectCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppProjectCore.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedor;

        public HomeController(IContenedorTrabajo contenedor)
        {
            _contenedor = contenedor;
        }

        public IActionResult Index()
        {

            HomeVM homeVM = new HomeVM()
            {
                Slider = _contenedor.Slider.GetAll(),
                ListArticulos = _contenedor.Articulo.GetAll()
            };

            //Esta linea es para poder saber si estamos en el home o no

            ViewBag.HomeVM = true;

            return View(homeVM);
        }

        public IActionResult Details(int id) 
        {
            var articuloDedeDb = _contenedor.Articulo.Get(id);
            return View(articuloDedeDb);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
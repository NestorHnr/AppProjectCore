using AppProjectCore.AccesoDatos.Data.Repository.IRepository;
using AppProjectCore.Data;
using AppProjectCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppProjectCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IContenedorTrabajo _contenedor;
        private readonly ApplicationDbContext _context;
        public CategoriaController(IContenedorTrabajo contenedor, ApplicationDbContext context)
        {
            _contenedor = contenedor;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedor.Categoria.Add(categoria);
                _contenedor.save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]

        public IActionResult Update(int id) 
        {
            Categoria categoria = new Categoria();
            categoria = _contenedor.Categoria.Get(id);
            if (categoria == null) 
            {
                return NotFound();
            }    
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedor.Categoria.Update(categoria);
                _contenedor.save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }





        #region llamadas a la API

        [HttpGet]

        public IActionResult GetAll() 
        {
            return Json(new { data = _contenedor.Categoria.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var objFromDb = _contenedor.Categoria.Get(id);
            if (objFromDb == null) 
            {
                return Json(new { success = false, message = "Error borrano categoría" });
            }
            _contenedor.Categoria.Remove(objFromDb);
            _contenedor.save();
            return Json(new { success = true, message = "Categoría borrada con exito" });

        }

        #endregion
    }
}

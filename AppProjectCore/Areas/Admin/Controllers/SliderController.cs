using AppProjectCore.AccesoDatos.Data.Repository.IRepository;
using AppProjectCore.Models;
using AppProjectCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppProjectCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IContenedorTrabajo _contenedor;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SliderController(IContenedorTrabajo contenedor, IWebHostEnvironment hostEnvironment)
        {
            _contenedor = contenedor;
            _hostEnvironment = hostEnvironment;

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
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                
                    //Nuevo archivo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extencion = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extencion), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    slider.ImagenUrl = @"\imagenes\sliders\" + nombreArchivo + extencion;

                    _contenedor.Slider.Add(slider);
                    _contenedor.save();

                    return RedirectToAction(nameof(Index));
                
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id != null)
            {
                var slider = _contenedor.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }

            return View() ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var sliderDesdeDb = _contenedor.Slider.Get(slider.Id);

                if (archivos.Count() > 0)
                {
                    //Nuevo para el slider
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extencion = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtencion = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDesdeDb.ImagenUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extencion), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    slider.ImagenUrl = @"\imagenes\sliders\" + nombreArchivo + extencion;

                    _contenedor.Slider.Update(slider);
                    _contenedor.save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //cuando la imagen ya existe y se conserva
                    slider.ImagenUrl = sliderDesdeDb.ImagenUrl;
                }
                _contenedor.Slider.Update(slider);
                _contenedor.save();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }



        #region llamadas a la API

        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedor.Slider.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sliderDesdeDb = _contenedor.Slider.Get(id);

            if (sliderDesdeDb == null)
            {
                return Json(new { success = false, message = "Error borrano Slider" });
            }


            _contenedor.Slider.Remove(sliderDesdeDb);
            _contenedor.save();
            return Json(new { success = true, message = "Slider borrada con exito" });

        }

        #endregion
    }
}

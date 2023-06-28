using AppProjectCore.AccesoDatos.Data.Repository.IRepository;
using AppProjectCore.Data;
using AppProjectCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace AppProjectCore.Areas.Admin.Controllers
{
    
    public class ArticuloController : Controller
    {
        private readonly IContenedorTrabajo _contenedor;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ArticuloController(IContenedorTrabajo contenedor, IWebHostEnvironment hostEnvironment)
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
            ArticuloVM artivm = new ArticuloVM()
            { 
                Articulo = new AppProjectCore.Models.Articulo(),
                ListaCategorias = _contenedor.Categoria.GetListaCategorias()
            };
            return View(artivm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artiVM)
        {
            if(ModelState.IsValid) 
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if(artiVM.Articulo.Id == 0) 
                {
                    //Nuevo archivo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extencion = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas,nombreArchivo + extencion), FileMode.Create)) 
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.Articulo.UrlImage = @"\imagenes\articulos\" + nombreArchivo + extencion;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedor.Articulo.Add(artiVM.Articulo);
                    _contenedor.save();

                    return RedirectToAction(nameof(Index));
                }
            }
            artiVM.ListaCategorias = _contenedor.Categoria.GetListaCategorias();
            return View(artiVM);
        }

        [HttpGet]

        public IActionResult Update(int? id) 
        {
            ArticuloVM artivm = new ArticuloVM()
            {
                Articulo = new AppProjectCore.Models.Articulo(),
                ListaCategorias = _contenedor.Categoria.GetListaCategorias()
            };

            if (id != null) 
            {
                artivm.Articulo = _contenedor.Articulo.Get(id.GetValueOrDefault());
            }

            return View(artivm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesdeDb = _contenedor.Articulo.Get(artiVM.Articulo.Id);

                if (archivos.Count() > 0)
                {
                    //Nuevo para el articulo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extencion = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtencion = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeDb.UrlImage.TrimStart('\\'));
                    if (System.IO.File.Exists(rutaImagen)) 
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extencion), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.Articulo.UrlImage = @"\imagenes\articulos\" + nombreArchivo + extencion;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();

                    _contenedor.Articulo.Update(artiVM.Articulo);
                    _contenedor.save();

                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    //cuando la imagen ya existe y se conserva
                    artiVM.Articulo.UrlImage = articuloDesdeDb.UrlImage;
                }
                _contenedor.Articulo.Update(artiVM.Articulo);
                _contenedor.save();

                return RedirectToAction(nameof(Index));
            }
            return View(artiVM);
        }


        #region llamadas a la API

        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedor.Articulo.GetAll(includeProperties:"Categoria") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articuloDesdeDb = _contenedor.Articulo.Get(id);
            string rutaDirectorioPrincipal = _hostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeDb.UrlImage.TrimStart('\\'));
            
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (articuloDesdeDb == null) 
            {
                return Json(new { success = false, message = "Error borrano Artículo" });
            }


            _contenedor.Articulo.Remove(articuloDesdeDb);
            _contenedor.save();
            return Json(new { success = true, message = "Artículo borrada con exito" });

        }

        #endregion
    }
}

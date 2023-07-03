using AppProjectCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace AppProjectCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsuarioController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuarioController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }


        [HttpGet]
        public IActionResult Index()
        {
            //Opcion 1: Obtener todos los usuarios
            //return View(_contenedorTrabajo.Usuario.GetAll());

            //Opcion 2: Obtener todos los usuarios menos el que este logeado, para no bloquearse asi mismo
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_contenedorTrabajo.Usuario.GetAll(u => u.Id != usuarioActual.Value));
        }

        [HttpGet]
        public IActionResult Bloquear(string id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.BloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.DesbloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

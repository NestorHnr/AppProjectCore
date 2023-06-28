using AppProjectCore.AccesoDatos.Data.Repository.IRepository;
using AppProjectCore.Data;
using AppProjectCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProjectCore.AccesoDatos.Data.Repository
{
    internal class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeDb.LockoutEnd = DateTime.Now.AddYears(1000);
            _context.SaveChanges();
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeDb.LockoutEnd = DateTime.Now;
            _context.SaveChanges();
        }
    }
}

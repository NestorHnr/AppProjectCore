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
    internal class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _context.Categorias.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString(),
            });
        }

        public void Update(Categoria categoria)
        {
            var ObjDesdeDb = _context.Categorias.FirstOrDefault(c => c.Id == categoria.Id);
            ObjDesdeDb.Nombre = categoria.Nombre;
            ObjDesdeDb.Orden = categoria.Orden;

            _context.SaveChanges();
        }
    }
}

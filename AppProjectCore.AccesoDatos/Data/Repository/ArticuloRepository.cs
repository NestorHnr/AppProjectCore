using AppProjectCore.AccesoDatos.Data.Repository.IRepository;
using AppProjectCore.Data;
using AppProjectCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AppProjectCore.AccesoDatos.Data.Repository
{
    internal class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticuloRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Articulo articulo)
        {
            var ObjDesdeDb = _context.Articulos.FirstOrDefault(c => c.Id == articulo.Id);
            ObjDesdeDb.Nombre = articulo.Nombre;
            ObjDesdeDb.Descripcion = articulo.Descripcion;
            ObjDesdeDb.Costo = articulo.Costo;
            ObjDesdeDb.UrlImage = articulo.UrlImage;
            ObjDesdeDb.CategoriaId = articulo.CategoriaId;

            //_context.SaveChanges();
        }
    }
}

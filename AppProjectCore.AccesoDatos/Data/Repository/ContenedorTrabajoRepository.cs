using AppProjectCore.AccesoDatos.Data.Repository.IRepository;
using AppProjectCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProjectCore.AccesoDatos.Data.Repository
{
    public class ContenedorTrabajoRepository : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _context;

        public ContenedorTrabajoRepository(ApplicationDbContext context)
        {
            _context = context;
            Categoria = new CategoriaRepository(_context);
        }

        public ICategoriaRepository Categoria { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}

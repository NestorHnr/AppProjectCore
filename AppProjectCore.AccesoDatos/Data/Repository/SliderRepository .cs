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
    internal class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _context;

        public SliderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Slider slider)
        {
            var ObjDesdeDb = _context.Sliders.FirstOrDefault(c => c.Id == slider.Id);
            ObjDesdeDb.Nombre = slider.Nombre;
            ObjDesdeDb.Estado = slider.Estado;
            ObjDesdeDb.ImagenUrl = slider.ImagenUrl;

            _context.SaveChanges();
        }
    }
}

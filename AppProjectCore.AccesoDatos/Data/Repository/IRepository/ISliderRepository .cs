using AppProjectCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AppProjectCore.AccesoDatos.Data.Repository.IRepository
{
    public interface ISliderRepository : IRepository<Slider>
    {

        void Update(Slider slider);
    }
}

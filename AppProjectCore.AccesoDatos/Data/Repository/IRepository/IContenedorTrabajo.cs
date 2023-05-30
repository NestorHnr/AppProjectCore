using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProjectCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        //Aqui deben ir agregando los diferenctes repositorios
        ICategoriaRepository Categoria { get; }
        IArticuloRepository Articulo { get; }

        void save();
    }
}

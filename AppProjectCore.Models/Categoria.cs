using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProjectCore.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Ingrese u nombre para la categoria")]
        public string Nombre { get; set; }

        public int? Orden { get; set; }
    }
}

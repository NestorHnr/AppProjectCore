using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProjectCore.Models
{
    public class ApplicationUser: IdentityUser
    {

        [Required(ErrorMessage ="Nombre Requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Direccion Requerida")]
        public string Direccion { get; set; }

        [Required(ErrorMessage ="Pais Requerido")]
        public string Pais { get; set; }

        [Required(ErrorMessage ="Ciudad Requerida")]
        public string Ciudad { get; set; }
    }
}

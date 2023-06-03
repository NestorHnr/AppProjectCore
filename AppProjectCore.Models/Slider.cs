using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProjectCore.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre Slider")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name ="Estado")]
        public bool Estado { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string ImagenUrl { get; set; }
    }
}

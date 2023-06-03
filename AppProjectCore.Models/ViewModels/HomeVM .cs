﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProjectCore.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Slider { get; set; }
        public IEnumerable<Articulo> ListArticulos { get; set; }
    }
}

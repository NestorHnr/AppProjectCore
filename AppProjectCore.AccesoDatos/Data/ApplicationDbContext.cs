﻿using AppProjectCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppProjectCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Categoria> Categorias { get; set; }    
        public DbSet<Articulo> Articulos { get; set; }    
        public DbSet<Slider> Sliders { get; set; }    
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }    
    }
}
using GhibliAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GhibliAPI.Data;
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
    }
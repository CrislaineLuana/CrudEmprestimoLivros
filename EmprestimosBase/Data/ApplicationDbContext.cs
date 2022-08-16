using EmprestimosBase.Models;
using Microsoft.EntityFrameworkCore;

namespace EmprestimosBase.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EmprestimosModel> Emprestimos { get; set; }

    }
}

using GerenciadorAlunos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorAlunos.Context;

public class AppDbContext: DbContext
{

    protected readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(_configuration.GetConnectionString("defaultConnection"));

    public DbSet<Aluno> Alunos { get; set; }

}

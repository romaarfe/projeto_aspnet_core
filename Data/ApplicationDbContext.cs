using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebRPGCreation.Models;

namespace WebRPGCreation.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Personagem>? Personagem { get; set; }
    public DbSet<Grupo>? Grupo { get; set; }
    public DbSet<Equipamento>? Equipamento { get; set; }
    public DbSet<Especialidade>? Especialidade { get; set; }
    public DbSet<PoderProfano>? PoderProfano { get; set; }
}


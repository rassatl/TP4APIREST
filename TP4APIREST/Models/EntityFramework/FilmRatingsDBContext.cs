using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;


namespace TP4APIREST.Models.EntityFramework
{
    public class FilmRatingsDBContext : DbContext
    {
        public FilmRatingsDBContext()
        {
        }
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        public FilmRatingsDBContext(DbContextOptions<FilmRatingsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Notation> Notation { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                                .EnableSensitiveDataLogging()
                                .UseNpgsql("Server=localhost;port=5432;Database=FilmDBbis; uid=postgres; password=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notation>()
                  .HasKey(m => new { m.UtilisateurId, m.FilmId });
            modelBuilder.Entity<Utilisateur>(entity =>{
                    entity.Property(e => e.DateCreation).HasDefaultValueSql("now()");
            });
        }
    }
}

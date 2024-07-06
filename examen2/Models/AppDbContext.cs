using Microsoft.EntityFrameworkCore;

namespace examen2.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Producto");

                entity.ToTable("Producto");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Nombre");
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Descripcion");
                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18,2)")
                    .HasColumnName("Precio");
                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ImagenUrl");
                entity.Property(e => e.CategoriaId).HasColumnName("CategoriaId");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK_Producto_Categoria");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Categoria");

                entity.ToTable("Categoria");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using FileStorage.Core.Entities;
using Microsoft.EntityFrameworkCore;
using File = FileStorage.Core.Entities.File;

namespace FileStorage.DBContext;

public partial class FileStorageContext : DbContext
{
    public FileStorageContext()
    {
    }

    public FileStorageContext(DbContextOptions<FileStorageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<File> Files { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserFileUpload> UserFileUploads { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:FilesDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("files_pkey");

            entity.ToTable("files");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("created_at");
            entity.Property(e => e.FileBase64).HasColumnName("file_base64");
            entity.Property(e => e.Filename)
                .HasMaxLength(254)
                .HasColumnName("filename");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>().HasKey(e => e.Email);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

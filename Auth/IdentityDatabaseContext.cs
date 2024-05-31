using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Auth
{
    public class IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
                                        optionsBuilder.UseNpgsql("Name=ConnectionStrings:FilesDatabase");
    }
}

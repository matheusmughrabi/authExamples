using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityExample.Data;

public class AppDbContext : IdentityDbContext // Adds the Identity tables
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BusinessLogic.Contexts
{
    class WhitelistContextFactory : IDesignTimeDbContextFactory<WhitelistContext>
    {
        public WhitelistContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WhitelistContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-P99H00B\\SQLEXPRESS;Database=Whitelist;Trusted_Connection=True;");

            return new WhitelistContext(optionsBuilder.Options);
        }
    }
}

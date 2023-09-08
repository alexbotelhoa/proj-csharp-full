using Template.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Template.DataAccess.Context
{
    public class CoreContext : DbContext
    {
        public CoreContext(DbContextOptions<CoreContext> options) : base(options) { }

        #region Users
        public DbSet<UsersViewModel> Users { get; set; }
        #endregion
    }
}

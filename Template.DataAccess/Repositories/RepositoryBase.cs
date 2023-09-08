using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Template.DataAccess.Context;
using Template.DataAccess.Repositories.Interfaces;

namespace Template.DataAccess.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        private readonly ILogger _logger;
        private readonly CoreContext _context;
        protected DbSet<TEntity> _entities { get; }

        public RepositoryBase(CoreContext context, ILogger<RepositoryBase<TEntity>> logger)
        {
            _logger = logger;
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int skip, int take)
        {
            try
            {
                return await _entities.Skip(skip).Take(take).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                return await _entities.FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            try
            {
                var resp = await _entities.AddAsync(entity);
                await _context.SaveChangesAsync();
                return resp.Entity;
            }
            catch
            {
                return null;
            }            
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, int id)
        {
            try
            {
                _entities.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                return null;
            }
            
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var reg = await _entities.FindAsync(id);
                _entities.Remove(reg);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

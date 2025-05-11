using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Data;
using Products.Repository.Interfaces;
using Serilog;

namespace Products.Repositories.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ZeissAppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ZeissAppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            Log.Information("Fetching all records of {Entity}", typeof(T).Name);
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id) 
        {
            Log.Information("Fetching {Entity} by ID {Id}", typeof(T).Name, id);
            return await _dbSet.FindAsync(id); 
        }

        public async Task AddAsync(T entity)
        {
            Log.Information("Adding new {Entity}", typeof(T).Name);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Log.Information("Updating {Entity}", typeof(T).Name);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Log.Information("Deleting {Entity}", typeof(T).Name);
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }
    }
}
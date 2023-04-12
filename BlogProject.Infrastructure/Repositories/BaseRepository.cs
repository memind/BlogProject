using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BlogProject.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        // Context baglantisina ihtiyac var

        private readonly DbContext _ctx;
        protected DbSet<T> _table;
        public BaseRepository(AppDbContext ctx)
        {
            _ctx = ctx;
            _table = _ctx.Set<T>();
        }
        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _table.AnyAsync(expression);
        }

        public async Task Create(T entity)
        {
            _table.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            await _ctx.SaveChangesAsync(); // servis katmaninda entitysine gore pasif hale getirilecek
        }

        public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await _table.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return await _table.Where(expression).ToListAsync();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table; // select * from Posts

            if (where != null)
                query = query.Where(where);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                return await orderBy(query).Select(select).FirstOrDefaultAsync();
            else
                return await query.Select(select).FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table;

            if (where != null)
                query = query.Where(where);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                return await orderBy(query).Select(select).ToListAsync();
            else
                return await query.Select(select).ToListAsync();
        }

        public async Task Update(T entity)
        {
            _ctx.Entry<T>(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }
    }
}

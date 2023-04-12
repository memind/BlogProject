using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BlogProject.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity); // Veritabanindan silinmez, status pasife cekilir
        Task<bool> Any(Expression<Func< T, bool>> expression); // kayit var ? true : false
        Task<T> GetDefault(Expression<Func<T, bool>> expression); // Dinamik olarak where islemi saglar. Id'ye gore getirir.
        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression); // Filtreleme yapip coklu obej dondurur (GenreId = 5 olan postlari dondurur gibi)

        // Select, Where, Siralama, Join
        // Hem select hem orderby yapabilecegimiz. Post, Author, Comment, Like'lari birlikte cekmek icin include etmek gerekir. Bir sorguya birden fazla tablo girecek. Yani eagerloading yapacagiz.
        Task<TResult> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select, // Select
            Expression<Func<T, bool>> where, // Where
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // Order
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null // Join
            );

        Task<List<TResult>> GetFilteredList<TResult>(
            Expression<Func<T, TResult>> select, // Select
            Expression<Func<T, bool>> where, // Where
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // Order
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null // Join
            );
    }
}

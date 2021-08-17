using ExampleRepositoryPattern.BusinessLogic.Data;
using ExampleRepositoryPattern.Core;
using ExampleRepositoryPattern.Core.Interfaz;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleRepositoryPattern.BusinessLogic.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ClassBase
    {
        private readonly RepositoryPatternDbContext context;

        public GenericRepository(RepositoryPatternDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Add(T entity)
        {
            context.Set<T>().Add(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<int> Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return await context.SaveChangesAsync();
        }
    }
}

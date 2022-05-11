using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackCore.Base
{
    public class ContextSeed
    {
        public async Task SeedEntityAsync<T>(DbContext context, List<T> seedDataEntities) where T : class
        {
            if (!context.Set<T>().Any())
            {
                DbSet<T> set = context.Set<T>();
                foreach (var entity in seedDataEntities)
                {
                    await set.AddAsync(entity);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}

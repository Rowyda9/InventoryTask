
using BackCore.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackCore.Base
{
    public interface IRepositry<TEntity>
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllByAdmin();

        TEntity AddAsync(TEntity entity);

        List<TEntity> AddAsync(IEnumerable<TEntity> entityLst);

        bool UpdateAsync(TEntity entity);
        bool UpdateAsync(IEnumerable<TEntity> entityLst);

        bool Delete(TEntity entity);

        bool Delete(List<TEntity> entitylst);

        bool SoftDelete(TEntity entity);

        bool SoftDelete(List<TEntity> entityLst);

        int SaveChanges();
    }
}



using BackCore.Utilities.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackCore.Base
{
    public class Repositry<T> : IRepositry<T> where T : class, IBaseEntity
    {
        private DbContext _context;

        private IHttpContextAccessor httpContextAccessor ;

        public DbContext Context
        {
            get { return _context; }
        }
        private DbSet<T> _set;

        public Repositry(DbContext context , IHttpContextAccessor _httpContextAccessor)
        {
            _context = context;
            _set = _context.Set<T>();
            httpContextAccessor = _httpContextAccessor;
        }



        public virtual IQueryable<T> GetAll()
        {
            return _set.Where(x => x.IsDeleted == false).OrderByDescending(x=>x.CreatedDate);
        }

        public IQueryable<T> GetAllByAdmin()
        {
            return _set.OrderByDescending(x => x.CreatedDate);
        }

        public virtual T AddAsync(T entity)
        {
            T result = null;

            try
            {
                if (Validator.IsValid(entity))
                {
                  
                    entity.CreatedBy = GetCurrentUser_Name();
                    entity.IsDeleted = false;
                    entity = _set.Add(entity).Entity;
                    if (SaveChanges() > 0)
                    {
                        result = entity;
                    }
                }
                else
                {
                    StringBuilder exceptionMsgs = new StringBuilder();
                    List<string> errorMsgs = Validator.GetInvalidMessages(entity).ToList();
                    foreach (var errmsg in errorMsgs)
                    {
                        exceptionMsgs.Append(errmsg);
                        exceptionMsgs.Append("/n");
                    }
                    throw new Exception(exceptionMsgs.ToString());
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;

        }

        public virtual void AddAsync(IEnumerable<T> entityLst)
        {
            foreach (var entity in entityLst)
            {
               
                entity.CreatedBy = GetCurrentUser_Name();

                _set.Add(entity);
            }
            SaveChanges();
        }

        List<T> IRepositry<T>.AddAsync(IEnumerable<T> entityLst)
        {
            foreach (var entity in entityLst)
            {

                entity.CreatedBy = GetCurrentUser_Name();

                _set.Add(entity);
            }
            SaveChanges();

            return entityLst.ToList();


        }


        public virtual bool UpdateAsync(T entity)
        {

            bool result = false;
            if (Validator.IsValid(entity))
            {
                entity.UpdatedBy = GetCurrentUser_Name();

                _context.Entry<T>(entity).State = EntityState.Modified;
                _context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
        

            if (SaveChanges() > 0)
                {
                    _context.Entry<T>(entity).State = EntityState.Detached;
                    result = true;
                }

                _context.Entry<T>(entity).State = EntityState.Detached;

            }
            else
            {
                StringBuilder exceptionMsgs = new StringBuilder();
                List<string> errorMsgs = Validator.GetInvalidMessages(entity).ToList();
                foreach (var errmsg in errorMsgs)
                {
                    exceptionMsgs.Append(errmsg);
                    exceptionMsgs.Append("/n");
                }
                throw new Exception(exceptionMsgs.ToString());
            }
            return result;
        }

        public virtual bool UpdateAsync(IEnumerable<T> entityLst)
        {
            bool result = false;
            foreach (var entity in entityLst)
            {
                entity.UpdatedBy = GetCurrentUser_Name();

                if (Validator.IsValid(entity))
                {
                    _context.Entry<T>(entity).State = EntityState.Modified;
                    _context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    _context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;

                }

                else
                {
                    StringBuilder exceptionMsgs = new StringBuilder();
                    List<string> errorMsgs = Validator.GetInvalidMessages(entity).ToList();
                    foreach (var errmsg in errorMsgs)
                    {
                        exceptionMsgs.Append(errmsg);
                        exceptionMsgs.Append("/n");
                    }
                    throw new Exception(exceptionMsgs.ToString());
                }
                if (SaveChanges() > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        //Physical Delete
        public virtual bool Delete(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public virtual bool Delete(List<T> entitylst)
        {
            bool result = false;
            if (entitylst != null && entitylst.Count > 0)
            {

                foreach (var entity in entitylst)
                {
                    entity.DeletedBy= this.httpContextAccessor.HttpContext.User.Identity.Name;

                    result = Delete(entity);
                }
                SaveChanges();
            }
            return result;
        }
     

        public bool SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;
            entity.DeletedBy = GetCurrentUser_Name();
            return UpdateAsync(entity);
        }

        public bool SoftDelete(List<T> entityLst)
        {
            bool result = false;
            if (entityLst.Any())
            {
                entityLst.ForEach(entity => entity.IsDeleted = true);
                return UpdateAsync(entityLst);
            }
            return result;
        }


        public virtual void RollBack()
        {

        }
        public virtual void Commit()
        {


        }
        public virtual int SaveChanges()
        {
            // this method handle any exception so no need to put it in try
            BaseEntityManager.AddAuditingData(_context.ChangeTracker.Entries());
            return _context.SaveChanges();
        }

        private string GetCurrentUser_Name()
        {
            return this.httpContextAccessor.HttpContext.User.Identity.Name;
        }

       
    }


}

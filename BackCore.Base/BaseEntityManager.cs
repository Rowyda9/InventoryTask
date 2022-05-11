

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace BackCore.Base
{
    public class BaseEntityManager : IBaseEntityManager
    {
        public static void AddAuditingData(IEnumerable<EntityEntry> dbEntityEntries)
        {
            try
            {
                foreach (var entry in dbEntityEntries)
                {
                    if (entry.Entity as IBaseEntity != null)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            var entity = (entry.Entity as IBaseEntity);
                            if (entity != null)
                            {
                                entity.CreatedDate = DateTime.UtcNow;
                            }
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            var entity = (entry.Entity as IBaseEntity);
                            if (entity != null)
                            {
                                entity.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Handle saving auditing data exception.
            }
        }
    }
}

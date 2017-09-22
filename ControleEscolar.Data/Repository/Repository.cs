using ControleEscolar.Entities.Entity;
using ControleEscolar.Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace ControleEscolar.Data.Repository
{
    /// <summary>
    /// Repository provider for Entity Framework
    /// See base class for method comments
    /// </summary>
    public class Repository : IRepository
    {        
        private DbContext dbContext;

        public Repository()
        {
            dbContext = new ControleEscolarDbContext();
        }

        public IQueryable<T> All<T>(string[] includes = null) where T : class
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());

                foreach (var include in includes.Skip(1))
                    query = query.Include(include);

                return query.AsQueryable();
            }

            IQueryable<T> entidade;

            entidade = dbContext.Set<T>().AsQueryable();

            return entidade;
        }

        public T Get<T>(Expression<Func<T, bool>> expression, string[] includes = null) where T : class
        {
            return All<T>(includes).FirstOrDefault(expression);
        }

        public virtual T Find<T>(Expression<Func<T, bool>> predicate, string[] includes = null) where T : class
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault<T>(predicate);
            }

            T entidade;

            entidade = dbContext.Set<T>().FirstOrDefault<T>(predicate);

            return entidade;
        }

        public virtual IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate, string[] includes = null) where T : class
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return dbContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public virtual IQueryable<T> Filter<T>(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null) where T : class
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? dbContext.Set<T>().Where<T>(predicate).AsQueryable() : dbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public virtual T Create<T>(T TObject, string[] includes = null) where T : class, IEntityBase
        {
            /*var entry = dbContext.Entry(TObject);              
            entry.State = EntityState.Added;
            dbContext.SaveChanges();
            return TObject;*/            

            Save<T>(TObject, includes);

            return TObject;
        }

        public virtual int Delete<T>(T TObject) where T : class
        {
            dbContext.Set<T>().Remove(TObject);
            return dbContext.SaveChanges();
        }

        public virtual int Update<T>(T TObject, string[] includes = null) where T : class, IEntityBase
        {
            /*var entry = dbContext.Entry(TObject);            
            dbContext.Set<T>().Attach(TObject);
            entry.State = EntityState.Modified;
            return dbContext.SaveChanges();*/

            Save<T>(TObject, includes);

            return TObject.Id;
        }    
       
        public virtual object Save<T>(T entitySent, string[] includes = null) where T : class, IEntityBase
        {
            var set = dbContext.Set<T>();

            var updating = entitySent.Id != 0;

            T entityToSave = null;

            if (updating)
            {
                if (includes != null && includes.Count() > 0)
                {
                    var completeSet = set.Include(includes.First());
                    entityToSave = completeSet.FirstOrDefault(x => x.Id == entitySent.Id);
                }
                else
                {
                    entityToSave = set.FirstOrDefault(x => x.Id == entitySent.Id);
                }

                if (entityToSave == null)
                    throw new Exception("Entity not found in database.");
            }
            else
            {
                entityToSave = entitySent;
            }

            LoadValues(entityToSave, entitySent, updating);

            if (!updating)
                set.Add(entityToSave);

            return dbContext.SaveChanges();           
        }        

        public virtual int Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var objects = Filter<T>(predicate);
            foreach (var obj in objects)
                dbContext.Set<T>().Remove(obj);
            return dbContext.SaveChanges();
        }

        public bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbContext.Set<T>().Count<T>(predicate) > 0;
        }

        public virtual void ExecuteProcedure(String procedureCommand, params SqlParameter[] sqlParams)
        {
            dbContext.Database.ExecuteSqlCommand(procedureCommand, sqlParams);
        }

        public virtual void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public virtual DbContextTransaction BeginTran()
        {
            return dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        private void LoadValues(IEntityBase entityToSave, IEntityBase entitySent, bool updating = true)
        {
            
            var properties = entityToSave.GetType().GetProperties();
            foreach (var property in properties)
            {
                var sentProperty = entitySent.GetType().GetProperty(property.Name);

                var type = property.PropertyType;
                if (type.IsGenericType && typeof(IEntityBase).IsAssignableFrom(type.GetGenericArguments().First()))
                {
                    // Collections of entities
                    var entities = (IEnumerable<IEntityBase>)sentProperty.GetValue(entitySent, null);
                    var storedEntities = (IEnumerable<IEntityBase>)property.GetValue(entityToSave, null);

                    if (entities == null)
                    {
                        property.SetValue(entityToSave, null, null);
                    }
                    else
                    {
                        if (storedEntities == null)
                        {
                            storedEntities = (IEnumerable<IEntityBase>)Activator.CreateInstance(property.GetType());
                            property.SetValue(entityToSave, storedEntities, null);
                        }

                        List<int> idsToAdd, idsToRemove;

                        if (!updating)
                        {
                            idsToAdd = entities.Select(x => x.Id).ToList();
                            idsToRemove = new List<int>();

                            storedEntities.GetType().GetMethod("Clear").Invoke(storedEntities, null);
                        }
                        else
                        {
                            var selectedIds = entities.Select(x => x.Id).ToList();
                            var existantIds = storedEntities.Select(x => x.Id).ToList();
                            idsToAdd = selectedIds.Where(x => !existantIds.Contains(x)).ToList();
                            idsToRemove = existantIds.Where(x => !selectedIds.Contains(x)).ToList();
                        }

                        var set = GetSet(type.GetGenericArguments().First());

                        foreach (var id in idsToAdd)
                        {
                            var e = set.FirstOrDefault(x => x.Id == id);
                            storedEntities.GetType().GetMethod("Add").Invoke(storedEntities, new[] { e });
                        }
                        foreach (var id in idsToRemove)
                        {
                            var e = set.FirstOrDefault(x => x.Id == id);
                            storedEntities.GetType().GetMethod("Remove").Invoke(storedEntities, new[] { e });
                        }
                    }
                }
                else if (typeof(IEntityBase).IsAssignableFrom(type))
                {
                    // Entity
                    var entity = sentProperty.GetValue(entitySent, null) as IEntityBase;

                    if (entity == null)
                    {
                        property.SetValue(entityToSave, null, null);
                    }
                    else
                    {
                        var set = GetSet(type);
                        var storedEntity = set.FirstOrDefault(x => x.Id == entity.Id);

                        property.SetValue(entityToSave, storedEntity, null);
                    }
                }
                else
                {
                    // Other values
                    var value = sentProperty.GetValue(entitySent, null);
                    property.SetValue(entityToSave, value, null);
                }
            }
        }

        private IEnumerable<IEntityBase> GetSet(Type type)
        {
            // Gets the original type, in case it is a proxy
            type = ObjectContext.GetObjectType(type);

            var method = dbContext.GetType().GetMethods().Where(x => x.Name == "Set" && x.ContainsGenericParameters).FirstOrDefault();
            return (IEnumerable<IEntityBase>)method.MakeGenericMethod(type).Invoke(dbContext, null);
        }        
    }
}

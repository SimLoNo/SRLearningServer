using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SRLearningServer.Components.Interfaces.Repositories;

namespace SRLearningServer.Components.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        public BaseRepository(DbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Create a new entity in the database, if attached relations are not present, they will be created as well.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TEntity> Create(TEntity entity)
        {
            try
            {
                entity.GetType().GetProperty("LastUpdated").SetValue(entity, DateOnly.FromDateTime(DateTime.Now));
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        /// <summary>
        /// Deactivates an entry in the database by setting the Active property to false. This is used when an entity is not to be deleted but is to be hidden from the user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TEntity> Deactivate(int id)
        {
            try
            {

                TEntity entity = await _context.Set<TEntity>().FindAsync(id);
                if (entity == null)
                {
                    return null;
                }

                entity.GetType().GetProperty("Active").SetValue(entity, false);
                entity.GetType().GetProperty("LastUpdated").SetValue(entity, DateOnly.FromDateTime(DateTime.Now));
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        /// <summary>
        /// This deletes an entity from the database. This is a hard delete and should be used with caution.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TEntity> Delete(TEntity entity)
        {
            try
            {

                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        /// <summary>
        /// This gets a single entity with all active and inactive relations.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<TEntity> Get(int id)
        {
            try
            {

                TEntity entity = await _context.Set<TEntity>().FindAsync(id);
                return entity;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// Gets all the entities of a given type from the database, both active and inactive.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {

                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}

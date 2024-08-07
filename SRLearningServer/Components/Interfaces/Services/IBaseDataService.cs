namespace SRLearningServer.Components.Interfaces.Services
{


    public interface IBaseDataService<T, U>
    {
        /*/// <summary>
        /// Takes a list of DTOs and converts them to entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IEnumerable<T> ConvertFromDto(IEnumerable<U> entities);

        /// <summary>
        /// Takes a list of entities and converts them to DTOs. If convertRelations is true, it will also convert the entities' relations.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public IEnumerable<U> ConvertToDto(IEnumerable<T> entities, bool convertRelations = false);

        /// <summary>
        /// Takes a single Dto and convert it to an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T ConvertFromDto(U entity);

        /// <summary>
        /// Takes a single entity and converts it to a DTO. If convertRelations is true, it will also convert the entity's relations
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="convertRelations"></param>
        /// <returns></returns>
        public U ConvertToDto(T entity, bool convertRelations = false);*/

        /// <summary>
        /// Gets a single entity by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public U Get(int id);

        /// <summary>
        /// Creates a new entity in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public U Create(U entity);

        /// <summary>
        /// Deactivates an entity in the database, a soft delete where the entry is still in the database but is marked as inactive.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public U Deactivate(U entity);

        /// <summary>
        /// Deactivates an entity in the database, a soft delete where the entry is still in the database but is marked as inactive.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public U Deactivate(int id);

        /// <summary>
        /// A hard delete where the entity is removed from the database. USE WITH CAUTION
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public U Delete(U entity);

        /// <summary>
        /// Get all entities of the given type from the database, including inactive entities.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<U> GetAll();
    }
}

namespace SRLearningServer.Components.Interfaces.Services
{


    public interface IBaseDataService<U>
    {
        

        /// <summary>
        /// Gets a single entity by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<U> Get(int id);

        /// <summary>
        /// Creates a new entity in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<U> Create(U entity);

        /// <summary>
        /// Deactivates an entity in the database, a soft delete where the entry is still in the database but is marked as inactive.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<U> Deactivate(U entity);

        /// <summary>
        /// Deactivates an entity in the database, a soft delete where the entry is still in the database but is marked as inactive.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<U> Deactivate(int id);

        /// <summary>
        /// A hard delete where the entity is removed from the database. USE WITH CAUTION
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<U> Delete(int entity);

        /// <summary>
        /// Get all entities of the given type from the database, including inactive entities.
        /// </summary>
        /// <returns></returns>
        public Task<List<U>> GetAll();

        /// <summary>
        /// Updates an entity in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<U> Update(U entity);
    }
}

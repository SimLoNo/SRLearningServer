﻿using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface ITypeRepository : IBaseRepository<Models.Type>
    {
        //Task<IEnumerable<Models.Type>> GetMultiple(List<Models.Type> types);
        Task<Models.Type> Create(Models.Type entity);
        Task<Models.Type> Update(Models.Type type);
        Task<Models.Type> Get(int id);
    }
}

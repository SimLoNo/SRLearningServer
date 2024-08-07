﻿using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Repositories
{
    public interface ITypeCategoryListRepository : IBaseRepository<TypeCategoryList>
    {

        public Task<TypeCategoryList> GetByName(string name);
        public Task<TypeCategoryList> Update(TypeCategoryList typeCategoryList);
    }
}

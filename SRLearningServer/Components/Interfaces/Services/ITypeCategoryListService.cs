using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Interfaces.Services
{
    public interface ITypeCategoryListService : IBaseDataService<TypeCategoryList>
    {

        public TypeCategoryList GetByName(string name);
    }
}

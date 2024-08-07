using Microsoft.EntityFrameworkCore;
using SRLearningServer.Components.Context;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Repositories
{
    public class TypeCategoryListRepository : BaseRepository<TypeCategoryList>, ITypeCategoryListRepository
    {
        public TypeCategoryListRepository(SRContext context) : base(context)
        {
        }

        public async Task<TypeCategoryList> GetByName(string name)
        {
            if (name == null)
            {
                return null;
            }
            try
            {
                return await _context.Set<TypeCategoryList>()
                    .FirstOrDefaultAsync(tcl => tcl.TypeCategoryListName == name && tcl.Active == true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TypeCategoryList> Update(TypeCategoryList typeCategoryList)
        {
            try
            {
                TypeCategoryList trackedTypeCategoryList = await _context.Set<TypeCategoryList>().FirstOrDefaultAsync(tcl => tcl.TypeCategoryListId == typeCategoryList.TypeCategoryListId);
                if (trackedTypeCategoryList == null)
                {
                    return null;
                }
                trackedTypeCategoryList.TypeCategoryListName = typeCategoryList.TypeCategoryListName;
                trackedTypeCategoryList.Active = typeCategoryList.Active;

                var trackedTypeIds = new HashSet<int>(trackedTypeCategoryList.Types.Select(r => r.TypeId));
                var typeCategoryListTypeIds = new HashSet<int>(typeCategoryList.Types.Select(r => r.TypeId));

                // Add new results
                foreach (var type in typeCategoryList.Types)
                {
                    if (!trackedTypeIds.Contains(type.TypeId))
                    {
                        trackedTypeCategoryList.Types.Add(type);
                    }
                }

                // Remove old results
                foreach (var type in trackedTypeCategoryList.Types.ToList())
                {
                    if (!typeCategoryList.Types.Any(t => t.TypeId == type.TypeId))
                    {
                        trackedTypeCategoryList.Types.Remove(type);
                    }
                }
                //trackedTypeCategoryList.Types.ToList().RemoveAll(r => !typeCategoryListTypeIds.Contains(r.TypeId));

                trackedTypeCategoryList.LastUpdated = DateOnly.FromDateTime(DateTime.Now);

                await _context.SaveChangesAsync();
                return trackedTypeCategoryList;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}

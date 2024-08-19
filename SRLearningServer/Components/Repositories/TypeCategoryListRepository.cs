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

        public async Task<TypeCategoryList> Create(TypeCategoryList entity)
        {
            try
            {
                entity.LastUpdated = DateOnly.FromDateTime(DateTime.UtcNow);
                foreach (var type in entity.Types.ToList())
                {
                    var newType = _context.Types.FirstOrDefaultAsync(t => t.TypeId == type.TypeId);
                    entity.Types.Remove(type);
                    if (newType != null)
                    {
                        entity.Types.Add(newType.Result);
                    }
                }
                _context.TypeCategoryLists.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// Returns a TypeCategoryList and it's relations with the given id. If no TypeCategoryList is found, returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TypeCategoryList> Get(int id)
        {
            try
            {
                var foundEntity = await _context.TypeCategoryLists
                    .Include(tcl => tcl.Types)
                    .FirstOrDefaultAsync(tcl => tcl.TypeCategoryListId == id);
                if (foundEntity == null)
                {
                    return null;
                }
                return foundEntity;
            }
            catch (Exception ex)
            {

                return null;
            }
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
                    .Include(tcl => tcl.Types)
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

                // Add new Types
                foreach (var type in typeCategoryList.Types)
                {
                    if (!trackedTypeIds.Contains(type.TypeId))
                    {
                        trackedTypeCategoryList.Types.Add(type);
                    }
                }

                // Remove old Types
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

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SRLearningServer.Components.Context;
using SRLearningServer.Components.Interfaces.Repositories;
using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Repositories
{
    public class TypeRepository : BaseRepository<Models.Type>, ITypeRepository
    {
        public TypeRepository(SRContext context) : base(context)
        {
        }

        public async Task<Models.Type> Create(Models.Type entity)
        {
            try
            {
                entity.LastUpdated = DateOnly.FromDateTime(DateTime.UtcNow);
                foreach (var card in entity.Cards.ToList())
                {
                    var newCard = await _context.Cards.FirstOrDefaultAsync(c => c.CardId == card.CardId);
                    entity.Cards.Remove(card);
                    if (newCard != null)
                    {
                        entity.Cards.Add(newCard);
                    }
                }
                foreach (var typeCategory in entity.TypeCategoryLists.ToList())
                {
                    var newTypeCategory = _context.TypeCategoryLists.FirstOrDefaultAsync(tcl => tcl.TypeCategoryListId == typeCategory.TypeCategoryListId);
                    entity.TypeCategoryLists.Remove(typeCategory);
                    if (newTypeCategory != null)
                    {
                        entity.TypeCategoryLists.Add(newTypeCategory.Result);
                    }

                }
                _context.Types.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// Gets a type and all it's relations from the database. If no type is found, returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Models.Type> Get(int id)
        {
            try
            {
                var foundEntity = await _context.Types
                    .Include(t => t.Cards)
                    .Include(t => t.TypeCategoryLists)
                    .FirstOrDefaultAsync(t => t.TypeId == id);
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

        /*/// <summary>
        /// Gets a list of active types from the database. If no types are sent, all active types are returned.
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Models.Type>> GetMultiple(List<Models.Type> types)
        {
            List<Models.Type> typeList = new List<Models.Type>();
            if (types.IsNullOrEmpty())
            {
                try
                {

                    typeList = await _context.Set<Models.Type>().Where(t => t.Active == true).ToListAsync();
                    return typeList;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            try
            {
                var typeIds = types.Select(t => t.TypeId).ToList();
                var typeNames = types.Select(t => t.CardTypeName).ToList();
                typeList = await _context.Set<Models.Type>().Where(t => t.Active == true && (typeIds.Contains(t.TypeId) || typeNames.Contains(t.CardTypeName))).ToListAsync();
                return typeList;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }*/
        /// <summary>
        /// Takes a Type and if a Type with the same TypeId exists in the database, updates it with the new Type data, if no Type exists in the database returns null.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Models.Type> Update(Models.Type type)
        {
            try
            {
                Models.Type trackedType = await _context.Set<Models.Type>()
                    .Include(t => t.Cards)
                    .Include(t => t.TypeCategoryLists)
                    .FirstOrDefaultAsync(t => t.TypeId == type.TypeId);
                if (trackedType == null)
                {
                    return null;
                }
                trackedType.CardTypeName = type.CardTypeName;
                trackedType.Active = type.Active;

                //Add new cards
                foreach (Card card in type.Cards)
                {
                    if (!trackedType.Cards.Any(c => c.CardId == card.CardId))
                    {
                        trackedType.Cards.Add(card);
                    }
                }
                //Removes old cards
                foreach (Card card in trackedType.Cards.ToList())
                {
                    if (!type.Cards.Any(c => c.CardId == card.CardId))
                    {
                        trackedType.Cards.Remove(card);
                    }
                }

                //Add new TypeCategoryLists
                foreach (TypeCategoryList tcl in type.TypeCategoryLists)
                {
                    if (!trackedType.TypeCategoryLists.Any(c => c.TypeCategoryListId == tcl.TypeCategoryListId))
                    {
                        trackedType.TypeCategoryLists.Add(tcl);
                    }
                }
                //Removes old TypeCategoryLists
                foreach (TypeCategoryList tcl in trackedType.TypeCategoryLists.ToList())
                {
                    if (!type.TypeCategoryLists.Any(c => c.TypeCategoryListId == tcl.TypeCategoryListId))
                    {
                        trackedType.TypeCategoryLists.Remove(tcl);
                    }
                }

                trackedType.LastUpdated = DateOnly.FromDateTime(DateTime.Now);
                await _context.SaveChangesAsync();
                return trackedType;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}

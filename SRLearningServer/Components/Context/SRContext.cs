using Microsoft.EntityFrameworkCore;
using SRLearningServer.Components.Models;

namespace SRLearningServer.Components.Context
{
    public class SRContext : DbContext
    {
        public DbSet<Models.Attachment> Attachments { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<Models.TypeCategoryList> TypeCategoryLists { get; set; }


        public SRContext()
        {
        }

        public SRContext(DbContextOptions<SRContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Attachment> attachments = new() 
            {
                new()
                {
                    AttachmentId = 1,
                    AttachmentName = "Attachment1",
                    AttachmentUrl = "Icon1234.png",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new()
                {
                    AttachmentId = 2,
                    AttachmentName = "Attachment2",
                    AttachmentUrl = "Icon1235.png",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new()
                {
                    AttachmentId = 3,
                    AttachmentName = "Attachment3",
                    AttachmentUrl = "Icon1236.png",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                }
            };
            modelBuilder.Entity<Models.Attachment>().HasData(
                attachments
                );

            modelBuilder.Entity<Models.Type>().HasData(
                new
                { 
                    TypeId = 1,
                    CardTypeName = "Signal",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                {
                    TypeId = 2,
                    CardTypeName = "I signal",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                {
                    TypeId = 3,
                    CardTypeName = "SI signal",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                {
                    TypeId = 4,
                    CardTypeName = "PU signal",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                {
                    TypeId = 5,
                    CardTypeName = "SU signal",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                {
                    TypeId = 6,
                    CardTypeName = "U signal",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                {
                    TypeId = 11,
                    CardTypeName = "Kør",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                {
                    TypeId = 12,
                    CardTypeName = "Kør igennem",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                {
                    TypeId = 13,
                    CardTypeName = "Stop",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                {
                    TypeId = 14,
                    CardTypeName = "Kør med begrænset hastighed",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                }
                );

            modelBuilder.Entity<Result>().HasData(
                new
                {
                    ResultId = 1,
                    ResultText = "stands foran signalet",
                    LastUpdated = new System.DateOnly(),
                    Active = true
                },
                new
                { 
                    ResultId = 2,
                    ResultText = "er der foran signalet et standsningsmærke, skal der standses med forenden ud for mærket",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                { 
                    ResultId = 3,
                    ResultText = "viderekørsel må kun ske ved indrangering eller for rangertræk efter tilladelse fra stationsbestyreren",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                }
                );

            modelBuilder.Entity<Card>().HasData(
                new
                {
                    CardId = 1,
                    CardName = "I signal Kør uden SI",
                    CardText = "I signal uden efterfølgende SI signal med denne visning betyder?",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true,
                },
                new
                {
                    CardId = 2,
                    CardName = "I signal Kør med SI",
                    CardText = "I signal med efterfølgende SI signal med denne visning betyder?",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true,
                },
                new
                {
                    CardId = 3,
                    CardName = "I signal Kør med begrænset hastighed med SI",
                    CardText = "I signal med efterfølgende SI signal med denne visning betyder?",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true,
                },
                new
                {
                    CardId = 4,
                    CardName = "I signal stop",
                    CardText = "I signal med denne visning betyder?",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true,
                }
            );

            modelBuilder.Entity<TypeCategoryList>().HasData(
                new
                {
                    TypeCategoryListId = 1,
                    TypeCategoryListName = "Signaler",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                },
                new
                {
                    TypeCategoryListId = 2,
                    TypeCategoryListName = "SignalVisninger",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                }
            );

            modelBuilder.Entity<TypeCategoryList>().HasMany(tcl => tcl.Types).WithMany(t => t.TypeCategoryLists).UsingEntity(j => j.HasData(new[]
            {
                new { TypeCategoryListsTypeCategoryListId = 1, TypesTypeId = 2 },
                new { TypeCategoryListsTypeCategoryListId = 1, TypesTypeId = 3 },
                new { TypeCategoryListsTypeCategoryListId = 1, TypesTypeId = 4 },
                new { TypeCategoryListsTypeCategoryListId = 1, TypesTypeId = 5 },
                new { TypeCategoryListsTypeCategoryListId = 1, TypesTypeId = 6 },

                new { TypeCategoryListsTypeCategoryListId = 2, TypesTypeId = 11 },
                new { TypeCategoryListsTypeCategoryListId = 2, TypesTypeId = 12 },
                new { TypeCategoryListsTypeCategoryListId = 2, TypesTypeId = 13 },
                new { TypeCategoryListsTypeCategoryListId = 2, TypesTypeId = 14 },
            }));

            modelBuilder.Entity<Card>().HasMany(p => p.Results).WithMany(p => p.Cards).UsingEntity(j => j.HasData(new[]
            {
                new { CardsCardId = 1, ResultsResultId = 1 },
                new { CardsCardId = 1, ResultsResultId = 2 },
                new { CardsCardId = 1, ResultsResultId = 3 },
                new { CardsCardId = 2, ResultsResultId = 1 },
                new { CardsCardId = 2, ResultsResultId = 2 },
                new { CardsCardId = 2, ResultsResultId = 3 },
                new { CardsCardId = 3, ResultsResultId = 1 },
                new { CardsCardId = 3, ResultsResultId = 2 },
                new { CardsCardId = 3, ResultsResultId = 3 },
                new { CardsCardId = 4, ResultsResultId = 1 },
                new { CardsCardId = 4, ResultsResultId = 2 },
                new { CardsCardId = 4, ResultsResultId = 3 },
            }));
            modelBuilder.Entity<Card>().HasMany(p => p.Types).WithMany(p => p.Cards).UsingEntity(j => j.HasData(new[]
            {
                new { CardsCardId = 1, TypesTypeId = 1 },
                new { CardsCardId = 1, TypesTypeId = 2 },
                new { CardsCardId = 1, TypesTypeId = 11 },

                new { CardsCardId = 2, TypesTypeId = 1 },
                new { CardsCardId = 2, TypesTypeId = 2 },
                new { CardsCardId = 2, TypesTypeId = 11 },

                new { CardsCardId = 3, TypesTypeId = 1 },
                new { CardsCardId = 3, TypesTypeId = 2 },
                new { CardsCardId = 3, TypesTypeId = 14 },

                new { CardsCardId = 4, TypesTypeId = 1 },
                new { CardsCardId = 4, TypesTypeId = 2 },
                new { CardsCardId = 4, TypesTypeId = 13 }
            }));

        }
    }
}

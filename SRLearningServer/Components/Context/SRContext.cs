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
            modelBuilder.Entity<Models.Attachment>().HasData(
                new
                {
                    AttachmentId = 1,
                    AttachmentName = "Attachment1",
                    AttachmentUrl = "Icon1234.png",
                    LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                    Active = true
                }
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
                    CardTypeName = "Stop",
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

            modelBuilder.Entity<Card>().HasData(new
            {
                CardId = 1,
                CardName = "Signal 1",
                CardText = "Signal 1",
                //AttachmentId = 1,
                LastUpdated = DateOnly.FromDateTime(DateTime.Now),
                Active = true
            }
            );

            modelBuilder.Entity<Card>().HasMany(p => p.Results).WithMany(p => p.Cards).UsingEntity(j => j.HasData(new[]
            {
                new { CardsCardId = 1, ResultsResultId = 1 },
                new { CardsCardId = 1, ResultsResultId = 2 },
                new { CardsCardId = 1, ResultsResultId = 3 }
            }));
            modelBuilder.Entity<Card>().HasMany(p => p.Types).WithMany(p => p.Cards).UsingEntity(j => j.HasData(new[]
            {
                new { CardsCardId = 1, TypesTypeId = 1 },
                new { CardsCardId = 1, TypesTypeId = 2 }
            }));

        }
    }
}

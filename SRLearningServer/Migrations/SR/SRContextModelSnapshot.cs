﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SRLearningServer.Components.Context;

#nullable disable

namespace SRLearningServer.Migrations.SR
{
    [DbContext(typeof(SRContext))]
    partial class SRContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CardResult", b =>
                {
                    b.Property<int>("CardsCardId")
                        .HasColumnType("int");

                    b.Property<int>("ResultsResultId")
                        .HasColumnType("int");

                    b.HasKey("CardsCardId", "ResultsResultId");

                    b.HasIndex("ResultsResultId");

                    b.ToTable("CardResult");

                    b.HasData(
                        new
                        {
                            CardsCardId = 1,
                            ResultsResultId = 1
                        },
                        new
                        {
                            CardsCardId = 1,
                            ResultsResultId = 2
                        },
                        new
                        {
                            CardsCardId = 1,
                            ResultsResultId = 3
                        },
                        new
                        {
                            CardsCardId = 2,
                            ResultsResultId = 1
                        },
                        new
                        {
                            CardsCardId = 2,
                            ResultsResultId = 2
                        },
                        new
                        {
                            CardsCardId = 2,
                            ResultsResultId = 3
                        },
                        new
                        {
                            CardsCardId = 3,
                            ResultsResultId = 1
                        },
                        new
                        {
                            CardsCardId = 3,
                            ResultsResultId = 2
                        },
                        new
                        {
                            CardsCardId = 3,
                            ResultsResultId = 3
                        },
                        new
                        {
                            CardsCardId = 4,
                            ResultsResultId = 1
                        },
                        new
                        {
                            CardsCardId = 4,
                            ResultsResultId = 2
                        },
                        new
                        {
                            CardsCardId = 4,
                            ResultsResultId = 3
                        });
                });

            modelBuilder.Entity("CardType", b =>
                {
                    b.Property<int>("CardsCardId")
                        .HasColumnType("int");

                    b.Property<int>("TypesTypeId")
                        .HasColumnType("int");

                    b.HasKey("CardsCardId", "TypesTypeId");

                    b.HasIndex("TypesTypeId");

                    b.ToTable("CardType");

                    b.HasData(
                        new
                        {
                            CardsCardId = 1,
                            TypesTypeId = 1
                        },
                        new
                        {
                            CardsCardId = 1,
                            TypesTypeId = 2
                        },
                        new
                        {
                            CardsCardId = 1,
                            TypesTypeId = 11
                        },
                        new
                        {
                            CardsCardId = 2,
                            TypesTypeId = 1
                        },
                        new
                        {
                            CardsCardId = 2,
                            TypesTypeId = 2
                        },
                        new
                        {
                            CardsCardId = 2,
                            TypesTypeId = 11
                        },
                        new
                        {
                            CardsCardId = 3,
                            TypesTypeId = 1
                        },
                        new
                        {
                            CardsCardId = 3,
                            TypesTypeId = 2
                        },
                        new
                        {
                            CardsCardId = 3,
                            TypesTypeId = 14
                        },
                        new
                        {
                            CardsCardId = 4,
                            TypesTypeId = 1
                        },
                        new
                        {
                            CardsCardId = 4,
                            TypesTypeId = 2
                        },
                        new
                        {
                            CardsCardId = 4,
                            TypesTypeId = 13
                        });
                });

            modelBuilder.Entity("SRLearningServer.Components.Models.Attachment", b =>
                {
                    b.Property<int>("AttachmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttachmentId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("AttachmentName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AttachmentUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("LastUpdated")
                        .HasColumnType("date");

                    b.HasKey("AttachmentId");

                    b.ToTable("Attachments");

                    b.HasData(
                        new
                        {
                            AttachmentId = 1,
                            Active = true,
                            AttachmentName = "Attachment1",
                            AttachmentUrl = "Icon1234.png",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            AttachmentId = 2,
                            Active = true,
                            AttachmentName = "Attachment2",
                            AttachmentUrl = "Icon1235.png",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            AttachmentId = 3,
                            Active = true,
                            AttachmentName = "Attachment3",
                            AttachmentUrl = "Icon1236.png",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        });
                });

            modelBuilder.Entity("SRLearningServer.Components.Models.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CardId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CardText")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateOnly>("LastUpdated")
                        .HasColumnType("date");

                    b.HasKey("CardId");

                    b.HasIndex("AttachmentId");

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            CardId = 1,
                            Active = true,
                            AttachmentId = 1,
                            CardName = "I signal Kør uden SI",
                            CardText = "I signal uden efterfølgende SI signal med denne visning betyder?",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            CardId = 2,
                            Active = true,
                            AttachmentId = 1,
                            CardName = "I signal Kør med SI",
                            CardText = "I signal med efterfølgende SI signal med denne visning betyder?",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            CardId = 3,
                            Active = true,
                            AttachmentId = 1,
                            CardName = "I signal Kør med begrænset hastighed med SI",
                            CardText = "I signal med efterfølgende SI signal med denne visning betyder?",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            CardId = 4,
                            Active = true,
                            AttachmentId = 1,
                            CardName = "I signal stop",
                            CardText = "I signal med denne visning betyder?",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        });
                });

            modelBuilder.Entity("SRLearningServer.Components.Models.Result", b =>
                {
                    b.Property<int>("ResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResultId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<string>("ResultText")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ResultId");

                    b.HasIndex("AttachmentId");

                    b.ToTable("Results");

                    b.HasData(
                        new
                        {
                            ResultId = 1,
                            Active = true,
                            LastUpdated = new DateOnly(1, 1, 1),
                            ResultText = "stands foran signalet"
                        },
                        new
                        {
                            ResultId = 2,
                            Active = true,
                            LastUpdated = new DateOnly(2024, 8, 13),
                            ResultText = "er der foran signalet et standsningsmærke, skal der standses med forenden ud for mærket"
                        },
                        new
                        {
                            ResultId = 3,
                            Active = true,
                            LastUpdated = new DateOnly(2024, 8, 13),
                            ResultText = "viderekørsel må kun ske ved indrangering eller for rangertræk efter tilladelse fra stationsbestyreren"
                        });
                });

            modelBuilder.Entity("SRLearningServer.Components.Models.Type", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CardTypeName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateOnly>("LastUpdated")
                        .HasColumnType("date");

                    b.HasKey("TypeId");

                    b.ToTable("Types");

                    b.HasData(
                        new
                        {
                            TypeId = 1,
                            Active = true,
                            CardTypeName = "Signal",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            TypeId = 2,
                            Active = true,
                            CardTypeName = "I signal",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            TypeId = 3,
                            Active = true,
                            CardTypeName = "SI signal",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            TypeId = 4,
                            Active = true,
                            CardTypeName = "PU signal",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            TypeId = 5,
                            Active = true,
                            CardTypeName = "SU signal",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            TypeId = 6,
                            Active = true,
                            CardTypeName = "U signal",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            TypeId = 11,
                            Active = true,
                            CardTypeName = "Kør",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            TypeId = 12,
                            Active = true,
                            CardTypeName = "Kør igennem",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            TypeId = 13,
                            Active = true,
                            CardTypeName = "Stop",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        },
                        new
                        {
                            TypeId = 14,
                            Active = true,
                            CardTypeName = "Kør med begrænset hastighed",
                            LastUpdated = new DateOnly(2024, 8, 13)
                        });
                });

            modelBuilder.Entity("SRLearningServer.Components.Models.TypeCategoryList", b =>
                {
                    b.Property<int>("TypeCategoryListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeCategoryListId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<string>("TypeCategoryListName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TypeCategoryListId");

                    b.ToTable("TypeCategoryLists");

                    b.HasData(
                        new
                        {
                            TypeCategoryListId = 1,
                            Active = true,
                            LastUpdated = new DateOnly(2024, 8, 13),
                            TypeCategoryListName = "Signaler"
                        },
                        new
                        {
                            TypeCategoryListId = 2,
                            Active = true,
                            LastUpdated = new DateOnly(2024, 8, 13),
                            TypeCategoryListName = "SignalVisninger"
                        });
                });

            modelBuilder.Entity("TypeTypeCategoryList", b =>
                {
                    b.Property<int>("TypeCategoryListsTypeCategoryListId")
                        .HasColumnType("int");

                    b.Property<int>("TypesTypeId")
                        .HasColumnType("int");

                    b.HasKey("TypeCategoryListsTypeCategoryListId", "TypesTypeId");

                    b.HasIndex("TypesTypeId");

                    b.ToTable("TypeTypeCategoryList");

                    b.HasData(
                        new
                        {
                            TypeCategoryListsTypeCategoryListId = 1,
                            TypesTypeId = 2
                        },
                        new
                        {
                            TypeCategoryListsTypeCategoryListId = 1,
                            TypesTypeId = 3
                        },
                        new
                        {
                            TypeCategoryListsTypeCategoryListId = 1,
                            TypesTypeId = 4
                        },
                        new
                        {
                            TypeCategoryListsTypeCategoryListId = 1,
                            TypesTypeId = 5
                        },
                        new
                        {
                            TypeCategoryListsTypeCategoryListId = 1,
                            TypesTypeId = 6
                        },
                        new
                        {
                            TypeCategoryListsTypeCategoryListId = 2,
                            TypesTypeId = 11
                        },
                        new
                        {
                            TypeCategoryListsTypeCategoryListId = 2,
                            TypesTypeId = 12
                        },
                        new
                        {
                            TypeCategoryListsTypeCategoryListId = 2,
                            TypesTypeId = 13
                        },
                        new
                        {
                            TypeCategoryListsTypeCategoryListId = 2,
                            TypesTypeId = 14
                        });
                });

            modelBuilder.Entity("CardResult", b =>
                {
                    b.HasOne("SRLearningServer.Components.Models.Card", null)
                        .WithMany()
                        .HasForeignKey("CardsCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SRLearningServer.Components.Models.Result", null)
                        .WithMany()
                        .HasForeignKey("ResultsResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CardType", b =>
                {
                    b.HasOne("SRLearningServer.Components.Models.Card", null)
                        .WithMany()
                        .HasForeignKey("CardsCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SRLearningServer.Components.Models.Type", null)
                        .WithMany()
                        .HasForeignKey("TypesTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SRLearningServer.Components.Models.Card", b =>
                {
                    b.HasOne("SRLearningServer.Components.Models.Attachment", "Attachment")
                        .WithMany("Cards")
                        .HasForeignKey("AttachmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attachment");
                });

            modelBuilder.Entity("SRLearningServer.Components.Models.Result", b =>
                {
                    b.HasOne("SRLearningServer.Components.Models.Attachment", "Attachment")
                        .WithMany("Results")
                        .HasForeignKey("AttachmentId");

                    b.Navigation("Attachment");
                });

            modelBuilder.Entity("TypeTypeCategoryList", b =>
                {
                    b.HasOne("SRLearningServer.Components.Models.TypeCategoryList", null)
                        .WithMany()
                        .HasForeignKey("TypeCategoryListsTypeCategoryListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SRLearningServer.Components.Models.Type", null)
                        .WithMany()
                        .HasForeignKey("TypesTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SRLearningServer.Components.Models.Attachment", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Results");
                });
#pragma warning restore 612, 618
        }
    }
}

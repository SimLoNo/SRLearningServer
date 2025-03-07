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
                });

            modelBuilder.Entity("SRLearningServer.Components.Models.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CardId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("AttachmentId")
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
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("ResultId");

                    b.HasIndex("AttachmentId");

                    b.ToTable("Results");
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
                        .HasForeignKey("AttachmentId");

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

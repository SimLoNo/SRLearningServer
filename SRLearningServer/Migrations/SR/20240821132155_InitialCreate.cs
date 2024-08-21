using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRLearningServer.Migrations.SR
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttachmentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateOnly>(type: "date", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.AttachmentId);
                });

            migrationBuilder.CreateTable(
                name: "TypeCategoryLists",
                columns: table => new
                {
                    TypeCategoryListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeCategoryListName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCategoryLists", x => x.TypeCategoryListId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardTypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastUpdated = table.Column<DateOnly>(type: "date", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CardText = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: true),
                    LastUpdated = table.Column<DateOnly>(type: "date", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "AttachmentId");
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Results_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "AttachmentId");
                });

            migrationBuilder.CreateTable(
                name: "TypeTypeCategoryList",
                columns: table => new
                {
                    TypeCategoryListsTypeCategoryListId = table.Column<int>(type: "int", nullable: false),
                    TypesTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTypeCategoryList", x => new { x.TypeCategoryListsTypeCategoryListId, x.TypesTypeId });
                    table.ForeignKey(
                        name: "FK_TypeTypeCategoryList_TypeCategoryLists_TypeCategoryListsTypeCategoryListId",
                        column: x => x.TypeCategoryListsTypeCategoryListId,
                        principalTable: "TypeCategoryLists",
                        principalColumn: "TypeCategoryListId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypeTypeCategoryList_Types_TypesTypeId",
                        column: x => x.TypesTypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardType",
                columns: table => new
                {
                    CardsCardId = table.Column<int>(type: "int", nullable: false),
                    TypesTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardType", x => new { x.CardsCardId, x.TypesTypeId });
                    table.ForeignKey(
                        name: "FK_CardType_Cards_CardsCardId",
                        column: x => x.CardsCardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardType_Types_TypesTypeId",
                        column: x => x.TypesTypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardResult",
                columns: table => new
                {
                    CardsCardId = table.Column<int>(type: "int", nullable: false),
                    ResultsResultId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardResult", x => new { x.CardsCardId, x.ResultsResultId });
                    table.ForeignKey(
                        name: "FK_CardResult_Cards_CardsCardId",
                        column: x => x.CardsCardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardResult_Results_ResultsResultId",
                        column: x => x.ResultsResultId,
                        principalTable: "Results",
                        principalColumn: "ResultId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardResult_ResultsResultId",
                table: "CardResult",
                column: "ResultsResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AttachmentId",
                table: "Cards",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CardType_TypesTypeId",
                table: "CardType",
                column: "TypesTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_AttachmentId",
                table: "Results",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTypeCategoryList_TypesTypeId",
                table: "TypeTypeCategoryList",
                column: "TypesTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardResult");

            migrationBuilder.DropTable(
                name: "CardType");

            migrationBuilder.DropTable(
                name: "TypeTypeCategoryList");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "TypeCategoryLists");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Attachments");
        }
    }
}

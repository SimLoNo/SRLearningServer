using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    LastUpdated = table.Column<DateOnly>(type: "date", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: true)
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
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateOnly>(type: "date", nullable: false),
                    AttachmentId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Attachments",
                columns: new[] { "AttachmentId", "Active", "AttachmentName", "AttachmentUrl", "LastUpdated" },
                values: new object[,]
                {
                    { 1, true, "Attachment1", "Icon1234.png", new DateOnly(2024, 8, 8) },
                    { 2, true, "Attachment2", "Icon1235.png", new DateOnly(2024, 8, 8) },
                    { 3, true, "Attachment3", "Icon1236.png", new DateOnly(2024, 8, 8) }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardId", "Active", "AttachmentId", "CardName", "CardText", "LastUpdated" },
                values: new object[,]
                {
                    { 1, true, null, "I signal Kør uden SI", "I signal uden efterfølgende SI signal med denne visning betyder?", new DateOnly(2024, 8, 8) },
                    { 2, true, null, "I signal Kør med SI", "I signal med efterfølgende SI signal med denne visning betyder?", new DateOnly(2024, 8, 8) },
                    { 3, true, null, "I signal Kør med begrænset hastighed med SI", "I signal med efterfølgende SI signal med denne visning betyder?", new DateOnly(2024, 8, 8) },
                    { 4, true, null, "I signal stop", "I signal med denne visning betyder?", new DateOnly(2024, 8, 8) }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "ResultId", "Active", "AttachmentId", "LastUpdated", "ResultText" },
                values: new object[,]
                {
                    { 1, true, null, new DateOnly(1, 1, 1), "stands foran signalet" },
                    { 2, true, null, new DateOnly(2024, 8, 8), "er der foran signalet et standsningsmærke, skal der standses med forenden ud for mærket" },
                    { 3, true, null, new DateOnly(2024, 8, 8), "viderekørsel må kun ske ved indrangering eller for rangertræk efter tilladelse fra stationsbestyreren" }
                });

            migrationBuilder.InsertData(
                table: "TypeCategoryLists",
                columns: new[] { "TypeCategoryListId", "Active", "LastUpdated", "TypeCategoryListName" },
                values: new object[,]
                {
                    { 1, true, new DateOnly(2024, 8, 8), "Signaler" },
                    { 2, true, new DateOnly(2024, 8, 8), "SignalVisninger" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "TypeId", "Active", "CardTypeName", "LastUpdated" },
                values: new object[,]
                {
                    { 1, true, "Signal", new DateOnly(2024, 8, 8) },
                    { 2, true, "I signal", new DateOnly(2024, 8, 8) },
                    { 3, true, "SI signal", new DateOnly(2024, 8, 8) },
                    { 4, true, "PU signal", new DateOnly(2024, 8, 8) },
                    { 5, true, "SU signal", new DateOnly(2024, 8, 8) },
                    { 6, true, "U signal", new DateOnly(2024, 8, 8) },
                    { 11, true, "Kør", new DateOnly(2024, 8, 8) },
                    { 12, true, "Kør igennem", new DateOnly(2024, 8, 8) },
                    { 13, true, "Stop", new DateOnly(2024, 8, 8) },
                    { 14, true, "Kør med begrænset hastighed", new DateOnly(2024, 8, 8) }
                });

            migrationBuilder.InsertData(
                table: "CardResult",
                columns: new[] { "CardsCardId", "ResultsResultId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "CardType",
                columns: new[] { "CardsCardId", "TypesTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 11 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 11 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 14 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 13 }
                });

            migrationBuilder.InsertData(
                table: "TypeTypeCategoryList",
                columns: new[] { "TypeCategoryListsTypeCategoryListId", "TypesTypeId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 2, 11 },
                    { 2, 12 },
                    { 2, 13 },
                    { 2, 14 }
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

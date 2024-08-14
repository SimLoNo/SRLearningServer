using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRLearningServer.Migrations.SR
{
    /// <inheritdoc />
    public partial class newContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Attachments_AttachmentId",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "AttachmentId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "AttachmentId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "AttachmentId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 1,
                columns: new[] { "AttachmentId", "LastUpdated" },
                values: new object[] { 1, new DateOnly(2024, 8, 13) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 2,
                columns: new[] { "AttachmentId", "LastUpdated" },
                values: new object[] { 1, new DateOnly(2024, 8, 13) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 3,
                columns: new[] { "AttachmentId", "LastUpdated" },
                values: new object[] { 1, new DateOnly(2024, 8, 13) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 4,
                columns: new[] { "AttachmentId", "LastUpdated" },
                values: new object[] { 1, new DateOnly(2024, 8, 13) });

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "ResultId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "ResultId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "TypeCategoryLists",
                keyColumn: "TypeCategoryListId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "TypeCategoryLists",
                keyColumn: "TypeCategoryListId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 5,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 6,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 11,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 12,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 13,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 14,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 13));

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Attachments_AttachmentId",
                table: "Cards",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "AttachmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Attachments_AttachmentId",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                table: "Cards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "AttachmentId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "AttachmentId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Attachments",
                keyColumn: "AttachmentId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 1,
                columns: new[] { "AttachmentId", "LastUpdated" },
                values: new object[] { null, new DateOnly(2024, 8, 8) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 2,
                columns: new[] { "AttachmentId", "LastUpdated" },
                values: new object[] { null, new DateOnly(2024, 8, 8) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 3,
                columns: new[] { "AttachmentId", "LastUpdated" },
                values: new object[] { null, new DateOnly(2024, 8, 8) });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: 4,
                columns: new[] { "AttachmentId", "LastUpdated" },
                values: new object[] { null, new DateOnly(2024, 8, 8) });

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "ResultId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "ResultId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "TypeCategoryLists",
                keyColumn: "TypeCategoryListId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "TypeCategoryLists",
                keyColumn: "TypeCategoryListId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 5,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 6,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 11,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 12,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 13,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "TypeId",
                keyValue: 14,
                column: "LastUpdated",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Attachments_AttachmentId",
                table: "Cards",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "AttachmentId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SRLearningServer.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6a7abd03-6822-4ab0-b669-ce87d85f2fce", "95e78cc7-aa7d-4c0f-b01f-93952dc3bffa", "Admin", "ADMIN" },
                    { "9ee04b1d-6ba1-4729-bdf0-65f3f76bfa34", "d5c2d488-a175-4605-957a-95b07279ef34", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a7abd03-6822-4ab0-b669-ce87d85f2fce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ee04b1d-6ba1-4729-bdf0-65f3f76bfa34");
        }
    }
}

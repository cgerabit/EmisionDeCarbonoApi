using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmisionDeCarbonoApi.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmpresas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Apple" },
                    { 2, "Google" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

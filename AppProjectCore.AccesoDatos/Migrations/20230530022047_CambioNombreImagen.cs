using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjectCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class CambioNombreImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Sliders",
                newName: "ImagenUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagenUrl",
                table: "Sliders",
                newName: "ImageUrl");
        }
    }
}

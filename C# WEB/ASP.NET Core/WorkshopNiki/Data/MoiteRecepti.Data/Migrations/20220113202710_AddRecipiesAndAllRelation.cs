using Microsoft.EntityFrameworkCore.Migrations;

namespace MoiteRecepti.Data.Migrations
{
    public partial class AddRecipiesAndAllRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipies_CaTegories_CategoryId",
                table: "Recipies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaTegories",
                table: "CaTegories");

            migrationBuilder.RenameTable(
                name: "CaTegories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_CaTegories_IsDeleted",
                table: "Categories",
                newName: "IX_Categories_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipies_Categories_CategoryId",
                table: "Recipies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipies_Categories_CategoryId",
                table: "Recipies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "CaTegories");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_IsDeleted",
                table: "CaTegories",
                newName: "IX_CaTegories_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaTegories",
                table: "CaTegories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipies_CaTegories_CategoryId",
                table: "Recipies",
                column: "CategoryId",
                principalTable: "CaTegories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

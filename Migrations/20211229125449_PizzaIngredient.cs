using Microsoft.EntityFrameworkCore.Migrations;

namespace AppWeb_Proiect.Migrations
{
    public partial class PizzaIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Pret",
                table: "Pizza",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gramaj",
                table: "Pizza",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "SpecificatieID",
                table: "Pizza",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeIngredient = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Specificatie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeSpecificatie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specificatie", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PizzaIngredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaIngredient", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PizzaIngredient_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaIngredient_Pizza_PizzaID",
                        column: x => x.PizzaID,
                        principalTable: "Pizza",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_SpecificatieID",
                table: "Pizza",
                column: "SpecificatieID");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaIngredient_IngredientID",
                table: "PizzaIngredient",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaIngredient_PizzaID",
                table: "PizzaIngredient",
                column: "PizzaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Specificatie_SpecificatieID",
                table: "Pizza",
                column: "SpecificatieID",
                principalTable: "Specificatie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Specificatie_SpecificatieID",
                table: "Pizza");

            migrationBuilder.DropTable(
                name: "PizzaIngredient");

            migrationBuilder.DropTable(
                name: "Specificatie");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_SpecificatieID",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "SpecificatieID",
                table: "Pizza");

            migrationBuilder.AlterColumn<decimal>(
                name: "Pret",
                table: "Pizza",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gramaj",
                table: "Pizza",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");
        }
    }
}

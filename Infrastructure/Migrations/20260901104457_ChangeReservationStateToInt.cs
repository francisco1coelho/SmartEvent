using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEvent.Infrastructure.Migrations
{
    public partial class ChangeReservationStateToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Adiciona coluna temporária int
            migrationBuilder.AddColumn<int>(
                name: "StateTemp",
                table: "reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // 2. Converte string → int
            migrationBuilder.Sql("""
                UPDATE "reservations"
                SET "StateTemp" =
                    CASE "State"
                        WHEN 'Active' THEN 0
                        WHEN 'Cancelled' THEN 1
                        WHEN 'Completed' THEN 2
                        ELSE 0
                    END;
                """);

            // 3. Remove coluna antiga
            migrationBuilder.DropColumn(
                name: "State",
                table: "reservations");

            // 4. Renomeia temporária para State
            migrationBuilder.RenameColumn(
                name: "StateTemp",
                table: "reservations",
                newName: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverte: adiciona coluna string temporária
            migrationBuilder.AddColumn<string>(
                name: "StateTemp",
                table: "reservations",
                type: "text",
                nullable: true);

            // Converte int → string
            migrationBuilder.Sql("""
                UPDATE "reservations"
                SET "StateTemp" =
                    CASE "State"
                        WHEN 0 THEN 'Active'
                        WHEN 1 THEN 'Cancelled'
                        WHEN 2 THEN 'Completed'
                        ELSE 'Active'
                    END;
                """);

            migrationBuilder.DropColumn(
                name: "State",
                table: "reservations");

            migrationBuilder.RenameColumn(
                name: "StateTemp",
                table: "reservations",
                newName: "State");
        }
    }
}
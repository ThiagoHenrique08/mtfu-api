using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoreThanFollowUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncluindotabelaResponsible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Responsible",
                columns: table => new
                {
                    ResponsibleId = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsible", x => x.ResponsibleId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responsible");
        }
    }
}

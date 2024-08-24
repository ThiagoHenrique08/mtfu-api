using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoreThanFollowUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoIdRelacionamentoProjectUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ProjectUsers",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProjectUsers",
                newName: "UsersId");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuIntialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblMenuItem",
                columns: table => new
                {
                    MenuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    MenuTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MenuUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MenuDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsPop = table.Column<bool>(type: "bit", nullable: false),
                    UrlMenuPath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MenuOrder = table.Column<int>(type: "int", nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IconName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AuthAdd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthLstEdit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthDel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddOnDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditOnDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelOnDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblMenuItem", x => x.MenuID);
                    table.ForeignKey(
                        name: "FK_TblMenuItem_TblMenuItem_ParentID",
                        column: x => x.ParentID,
                        principalTable: "TblMenuItem",
                        principalColumn: "MenuID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblMenuItem_ParentID",
                table: "TblMenuItem",
                column: "ParentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblMenuItem");
        }
    }
}

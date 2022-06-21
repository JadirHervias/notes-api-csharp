using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class CreateInitialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "text", unicode: false, nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notes_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "FullName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("839825b0-017b-4d68-9d66-d270f9c5167d"), "john_doe@gmail.com", "John Doe", "$2y$10$NP.PLhBrErGtsj4fDX6DAOqEkBfMksZ1IN1G3T5zFYZQ5CZtZiTMO", "john123" },
                    { new Guid("d5b5d210-bb70-4368-a7ab-a2c85d42dd9b"), "johancruyff_47@gmail.com", "Johan Cruyff", "$2y$10$NP.PLhBrErGtsj4fDX6DAOqEkBfMksZ1IN1G3T5zFYZQ5CZtZiTMO", "johanCF" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_notes_Title",
                table: "notes",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_notes_UserId",
                table: "notes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class intialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "Idtodos");

            migrationBuilder.CreateSequence<int>(
                name: "Idusers");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR Idusers"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR Idtodos"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Mohamed" });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "DeadLine", "Description", "FileDescription", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 16, 14, 43, 29, 801, DateTimeKind.Local).AddTicks(4975), "i want to know information", "1.pdf", "i want to read this pdf", 1 },
                    { 2, new DateTime(2024, 10, 17, 14, 43, 29, 801, DateTimeKind.Local).AddTicks(5080), "i want to know information", "2.pdf", "i want to do what in pdf", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_UserId",
                table: "TodoItems",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropSequence(
                name: "Idtodos");

            migrationBuilder.DropSequence(
                name: "Idusers");
        }
    }
}

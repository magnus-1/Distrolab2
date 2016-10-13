using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace community.Data.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupDB",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupDB_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageDB",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    GroupDBId = table.Column<int>(nullable: true),
                    SenderId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageDB_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageDB_GroupDB_GroupDBId",
                        column: x => x.GroupDBId,
                        principalTable: "GroupDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageDB_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupDB_ApplicationUserId",
                table: "GroupDB",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDB_ApplicationUserId",
                table: "MessageDB",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDB_GroupDBId",
                table: "MessageDB",
                column: "GroupDBId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDB_SenderId",
                table: "MessageDB",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageDB");

            migrationBuilder.DropTable(
                name: "GroupDB");
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace community.Data.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupDB_AspNetUsers_ApplicationUserId",
                table: "GroupDB");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageDB_AspNetUsers_ApplicationUserId",
                table: "MessageDB");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageDB_GroupDB_GroupDBId",
                table: "MessageDB");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageDB_AspNetUsers_SenderId",
                table: "MessageDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageDB",
                table: "MessageDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupDB",
                table: "GroupDB");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MessageDB",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "MessageDB",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "MessageDB",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "GroupDB",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_ApplicationUserId",
                table: "GroupDB",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ApplicationUserId",
                table: "MessageDB",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Groups_GroupDBId",
                table: "MessageDB",
                column: "GroupDBId",
                principalTable: "GroupDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "MessageDB",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_MessageDB_SenderId",
                table: "MessageDB",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageDB_GroupDBId",
                table: "MessageDB",
                newName: "IX_Messages_GroupDBId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageDB_ApplicationUserId",
                table: "MessageDB",
                newName: "IX_Messages_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupDB_ApplicationUserId",
                table: "GroupDB",
                newName: "IX_Groups_ApplicationUserId");

            migrationBuilder.RenameTable(
                name: "MessageDB",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "GroupDB",
                newName: "Groups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_ApplicationUserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ApplicationUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Groups_GroupDBId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Messages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageDB",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupDB",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupDB_AspNetUsers_ApplicationUserId",
                table: "Groups",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDB_AspNetUsers_ApplicationUserId",
                table: "Messages",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDB_GroupDB_GroupDBId",
                table: "Messages",
                column: "GroupDBId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDB_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                newName: "IX_MessageDB_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_GroupDBId",
                table: "Messages",
                newName: "IX_MessageDB_GroupDBId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ApplicationUserId",
                table: "Messages",
                newName: "IX_MessageDB_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_ApplicationUserId",
                table: "Groups",
                newName: "IX_GroupDB_ApplicationUserId");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "MessageDB");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "GroupDB");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Migrations
{
    public partial class AlterTableTaskTodoTypeUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTodos_Users_UserId",
                table: "TaskTodos");

            migrationBuilder.DropIndex(
                name: "IX_TaskTodos_UserId",
                table: "TaskTodos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TaskTodos",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "TaskTodos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskTodos_UserId1",
                table: "TaskTodos",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTodos_Users_UserId1",
                table: "TaskTodos",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTodos_Users_UserId1",
                table: "TaskTodos");

            migrationBuilder.DropIndex(
                name: "IX_TaskTodos_UserId1",
                table: "TaskTodos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TaskTodos");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "TaskTodos",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_TaskTodos_UserId",
                table: "TaskTodos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTodos_Users_UserId",
                table: "TaskTodos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

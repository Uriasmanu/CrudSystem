using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudSystem.Migrations
{
    /// <inheritdoc />
    public partial class Correcao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "TimeTrackers",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "TimeTrackers",
                newName: "EndTime");

            migrationBuilder.AddColumn<Guid>(
                name: "TarefasId1",
                table: "TimeTrackers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TimeTrackers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId1",
                table: "Tarefas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Projects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Collaborators",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeTrackers_TarefasId1",
                table: "TimeTrackers",
                column: "TarefasId1");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTrackers_UserId",
                table: "TimeTrackers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_ProjectId1",
                table: "Tarefas",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_ProjectId",
                table: "Collaborators",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Projects_ProjectId",
                table: "Collaborators",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Projects_ProjectId1",
                table: "Tarefas",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTrackers_Tarefas_TarefasId1",
                table: "TimeTrackers",
                column: "TarefasId1",
                principalTable: "Tarefas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTrackers_Users_UserId",
                table: "TimeTrackers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Projects_ProjectId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Projects_ProjectId1",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTrackers_Tarefas_TarefasId1",
                table: "TimeTrackers");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTrackers_Users_UserId",
                table: "TimeTrackers");

            migrationBuilder.DropIndex(
                name: "IX_TimeTrackers_TarefasId1",
                table: "TimeTrackers");

            migrationBuilder.DropIndex(
                name: "IX_TimeTrackers_UserId",
                table: "TimeTrackers");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_ProjectId1",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_ProjectId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "TarefasId1",
                table: "TimeTrackers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimeTrackers");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Collaborators");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "TimeTrackers",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "TimeTrackers",
                newName: "EndDate");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}

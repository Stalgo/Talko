using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalkoWeb.Migrations
{
    /// <inheritdoc />
    public partial class ReadonlyRefPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentRefereces",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "Comments",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "CommentRefereces",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}

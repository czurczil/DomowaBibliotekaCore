using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DomowaBibliotekaCore.Data.Migrations
{
    public partial class LibraryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BIO = table.Column<string>(nullable: true),
                    birthDate = table.Column<DateTime>(nullable: true),
                    birthPlace = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    photo = table.Column<string>(nullable: true),
                    sex = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cover = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: false),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    genre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    series = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Book_Authors",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<long>(nullable: false),
                    BookId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Authors", x => x.id);
                    table.ForeignKey(
                        name: "FK_Book_Authors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Authors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Books",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    isFavorite = table.Column<bool>(nullable: false),
                    isOnWishList = table.Column<bool>(nullable: false),
                    isRead = table.Column<bool>(nullable: false),
                    rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Books", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Books_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Books_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Book_Genres",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<long>(nullable: false),
                    GenreId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Genres", x => x.id);
                    table.ForeignKey(
                        name: "FK_Book_Genres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Genres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book_Series",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<long>(nullable: false),
                    SeriesId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Series", x => x.id);
                    table.ForeignKey(
                        name: "FK_Book_Series_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Series_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_Authors_AuthorId",
                table: "Book_Authors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Authors_BookId",
                table: "Book_Authors",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Genres_BookId",
                table: "Book_Genres",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Genres_GenreId",
                table: "Book_Genres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Series_BookId",
                table: "Book_Series",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Series_SeriesId",
                table: "Book_Series",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Books_BookId",
                table: "User_Books",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Books_UserId",
                table: "User_Books",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book_Authors");

            migrationBuilder.DropTable(
                name: "Book_Genres");

            migrationBuilder.DropTable(
                name: "Book_Series");

            migrationBuilder.DropTable(
                name: "User_Books");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}

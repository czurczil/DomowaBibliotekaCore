using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DomowaBibliotekaCore.Models;

namespace DomowaBibliotekaCore.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("DomowaBibliotekaCore.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.Authors", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BIO");

                    b.Property<DateTime?>("birthDate");

                    b.Property<string>("birthPlace");

                    b.Property<string>("firstName")
                        .IsRequired();

                    b.Property<string>("lastName")
                        .IsRequired();

                    b.Property<string>("photo");

                    b.Property<string>("sex");

                    b.HasKey("id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.Book_Authors", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AuthorId");

                    b.Property<long>("BookId");

                    b.HasKey("id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("Book_Authors");
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.Book_Genres", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BookId");

                    b.Property<long>("GenreId");

                    b.HasKey("id");

                    b.HasIndex("BookId");

                    b.HasIndex("GenreId");

                    b.ToTable("Book_Genres");
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.Book_Series", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BookId");

                    b.Property<long>("SeriesId");

                    b.HasKey("id");

                    b.HasIndex("BookId");

                    b.HasIndex("SeriesId");

                    b.ToTable("Book_Series");
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.Books", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("cover");

                    b.Property<string>("description");

                    b.Property<string>("title")
                        .IsRequired();

                    b.Property<int>("year");

                    b.HasKey("id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.Genres", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("genre")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.Series", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("series");

                    b.HasKey("id");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.User_Books", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BookId");

                    b.Property<string>("UserId");

                    b.Property<string>("comment");

                    b.Property<bool>("isFavorite");

                    b.Property<bool>("isOnWishList");

                    b.Property<bool>("isRead");

                    b.Property<int>("rating");

                    b.HasKey("id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("User_Books");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.Book_Authors", b =>
                {
                    b.HasOne("DomowaBibliotekaCore.Models.Data_Models.Authors", "Author")
                        .WithMany("Book_Authors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomowaBibliotekaCore.Models.Data_Models.Books", "Book")
                        .WithMany("Book_Authors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.Book_Genres", b =>
                {
                    b.HasOne("DomowaBibliotekaCore.Models.Data_Models.Books", "Book")
                        .WithMany("Book_Genres")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomowaBibliotekaCore.Models.Data_Models.Genres", "Genre")
                        .WithMany("Book_Genres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.Book_Series", b =>
                {
                    b.HasOne("DomowaBibliotekaCore.Models.Data_Models.Books", "Book")
                        .WithMany("Book_Series")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomowaBibliotekaCore.Models.Data_Models.Series", "Series")
                        .WithMany("Book_Series")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomowaBibliotekaCore.Models.Data_Models.User_Books", b =>
                {
                    b.HasOne("DomowaBibliotekaCore.Models.Data_Models.Books", "Book")
                        .WithMany("User_Books")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomowaBibliotekaCore.Models.ApplicationUser", "User")
                        .WithMany("User_Books")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DomowaBibliotekaCore.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DomowaBibliotekaCore.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomowaBibliotekaCore.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

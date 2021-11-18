﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PriceObserver.Data;

namespace PriceObserver.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211118132515_AlterUsersTableRemoveCanAwaitInputColumn")]
    partial class AlterUsersTableRemoveCanAwaitInputColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("PriceObserver.Model.Data.Command", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("MenuToRedirectId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MenuToRedirectId");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("ShopId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.HasIndex("UserId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.ItemPriceChange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int>("NewPrice")
                        .HasColumnType("integer");

                    b.Property<int>("OldPrice")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemPriceChanges");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("CanExpectInput")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.MenuCommand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CommandId")
                        .HasColumnType("integer");

                    b.Property<int>("MenuId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CommandId");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuCommands");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("MenuId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Expired")
                        .HasColumnType("boolean");

                    b.Property<Guid>("Token")
                        .HasColumnType("uuid");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.Command", b =>
                {
                    b.HasOne("PriceObserver.Model.Data.Menu", "MenuToRedirect")
                        .WithMany()
                        .HasForeignKey("MenuToRedirectId");

                    b.Navigation("MenuToRedirect");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.Item", b =>
                {
                    b.HasOne("PriceObserver.Model.Data.Shop", "Shop")
                        .WithMany("Items")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PriceObserver.Model.Data.User", "User")
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.ItemPriceChange", b =>
                {
                    b.HasOne("PriceObserver.Model.Data.Item", "Item")
                        .WithMany("PriceChanges")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.Menu", b =>
                {
                    b.HasOne("PriceObserver.Model.Data.Menu", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.MenuCommand", b =>
                {
                    b.HasOne("PriceObserver.Model.Data.Command", "Command")
                        .WithMany("CommandMenus")
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PriceObserver.Model.Data.Menu", "Menu")
                        .WithMany("MenuCommands")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Command");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.User", b =>
                {
                    b.HasOne("PriceObserver.Model.Data.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.UserToken", b =>
                {
                    b.HasOne("PriceObserver.Model.Data.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.Command", b =>
                {
                    b.Navigation("CommandMenus");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.Item", b =>
                {
                    b.Navigation("PriceChanges");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.Menu", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("MenuCommands");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.Shop", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("PriceObserver.Model.Data.User", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}

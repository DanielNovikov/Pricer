﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pricer.Data;
using Pricer.Data.Persistent;

namespace Pricer.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211205211113_AddResourcesTable")]
    partial class AddResourcesTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Command", b =>
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

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Item", b =>
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

            modelBuilder.Entity("Pricer.Data.Persistent.Models.ItemPriceChange", b =>
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

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Menu", b =>
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

            modelBuilder.Entity("Pricer.Data.Persistent.Models.MenuCommand", b =>
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

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Executed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Planned")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Key")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Shop", b =>
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

            modelBuilder.Entity("Pricer.Data.Persistent.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValueSql("true");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("MenuId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.UserToken", b =>
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

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Command", b =>
                {
                    b.HasOne("Pricer.Data.Persistent.Models.Menu", "MenuToRedirect")
                        .WithMany()
                        .HasForeignKey("MenuToRedirectId");

                    b.Navigation("MenuToRedirect");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Item", b =>
                {
                    b.HasOne("Pricer.Data.Persistent.Models.Shop", "Shop")
                        .WithMany("Items")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pricer.Data.Persistent.Models.User", "User")
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.ItemPriceChange", b =>
                {
                    b.HasOne("Pricer.Data.Persistent.Models.Item", "Item")
                        .WithMany("PriceChanges")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Menu", b =>
                {
                    b.HasOne("Pricer.Data.Persistent.Models.Menu", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.MenuCommand", b =>
                {
                    b.HasOne("Pricer.Data.Persistent.Models.Command", "Command")
                        .WithMany("CommandMenus")
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pricer.Data.Persistent.Models.Menu", "Menu")
                        .WithMany("MenuCommands")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Command");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.User", b =>
                {
                    b.HasOne("Pricer.Data.Persistent.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.UserToken", b =>
                {
                    b.HasOne("Pricer.Data.Persistent.Models.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Command", b =>
                {
                    b.Navigation("CommandMenus");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Item", b =>
                {
                    b.Navigation("PriceChanges");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Menu", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("MenuCommands");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.Shop", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Pricer.Data.Persistent.Models.User", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using eCommerceSite.Models;

namespace eCommerceSite.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200718011738_Database7")]
    partial class Database7
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("eCommerceSite.Models.Cetagory.Cetagory", b =>
                {
                    b.Property<int>("CID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Details")
                        .HasColumnType("character varying(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Photo")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("CID");

                    b.ToTable("Cetagorie");
                });

            modelBuilder.Entity("eCommerceSite.Models.Joining.Cetagory_Owner_Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Approve")
                        .HasColumnType("varchar(4)")
                        .HasMaxLength(4);

                    b.Property<int>("CID")
                        .HasColumnType("integer");

                    b.Property<int>("OID")
                        .HasColumnType("integer");

                    b.Property<int>("PID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("CID");

                    b.HasIndex("OID");

                    b.HasIndex("PID");

                    b.ToTable("Cetagorie_Owner_Post");
                });

            modelBuilder.Entity("eCommerceSite.Models.Owner.Owner", b =>
                {
                    b.Property<int>("OID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("JWT_Token")
                        .IsRequired()
                        .HasColumnType("character varying(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Photo")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("OID");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("eCommerceSite.Models.Post.PhotoGellary", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("PID")
                        .HasColumnType("integer");

                    b.Property<string>("Picture_Name")
                        .IsRequired()
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.HasIndex("PID");

                    b.ToTable("PhotoGellaries");
                });

            modelBuilder.Entity("eCommerceSite.Models.Post.Post", b =>
                {
                    b.Property<int>("PID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Approve")
                        .HasColumnType("character varying(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Details")
                        .HasColumnType("character varying(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Title")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("PID");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("eCommerceSite.Models.Joining.Cetagory_Owner_Post", b =>
                {
                    b.HasOne("eCommerceSite.Models.Cetagory.Cetagory", "Cetagory")
                        .WithMany("Cetagory_Owner_Post")
                        .HasForeignKey("CID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eCommerceSite.Models.Owner.Owner", "Owner")
                        .WithMany("Cetagory_Owner_Post")
                        .HasForeignKey("OID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eCommerceSite.Models.Post.Post", "Post")
                        .WithMany("Cetagory_Owner_Post")
                        .HasForeignKey("PID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eCommerceSite.Models.Post.PhotoGellary", b =>
                {
                    b.HasOne("eCommerceSite.Models.Post.Post", "Post")
                        .WithMany("Photos")
                        .HasForeignKey("PID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
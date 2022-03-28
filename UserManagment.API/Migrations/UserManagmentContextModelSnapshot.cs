﻿// <auto-generated />
using System;
using Books.API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserManagment.API.Migrations
{
    [DbContext(typeof(UserManagmentContext))]
    partial class UserManagmentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Books.API.Entities.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7646143d-7781-4566-bf49-5aba3751bfa4"),
                            Code = "R--",
                            Description = "only read permission"
                        },
                        new
                        {
                            Id = new Guid("5248737e-7105-4b59-be5d-47c3bc72a767"),
                            Code = "-W-",
                            Description = "only write permission"
                        },
                        new
                        {
                            Id = new Guid("ac0d40af-f0dd-44bd-ad5d-83ee2bcfa051"),
                            Code = "--X",
                            Description = "only execute permission"
                        },
                        new
                        {
                            Id = new Guid("35d77900-e205-4118-9e0a-5b4da53f741e"),
                            Code = "RWX",
                            Description = "all permissions granted"
                        },
                        new
                        {
                            Id = new Guid("aef3c363-dbcb-43b9-99ff-197f2dbf9e12"),
                            Code = "---",
                            Description = "no permissions"
                        });
                });

            modelBuilder.Entity("Books.API.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("76b3712d-dcad-4755-b7be-c960ce44c395"),
                            Email = "John@gmail.com",
                            FirstName = "John",
                            LastName = "Wayne",
                            Password = "john123",
                            Status = 2,
                            Username = "john123"
                        },
                        new
                        {
                            Id = new Guid("437201cc-8f91-41aa-9b15-99b97de0c229"),
                            Email = "Ana@gmail.com",
                            FirstName = "Ana",
                            LastName = "Smith",
                            Password = "anaSmith1",
                            Status = 0,
                            Username = "ana4"
                        },
                        new
                        {
                            Id = new Guid("3c8f1da3-9a18-4281-827f-6e4cad52d675"),
                            Email = "Sasa@gmail.com",
                            FirstName = "Joe",
                            LastName = "Doe",
                            Password = "Joe23",
                            Status = 1,
                            Username = "Joe45"
                        });
                });

            modelBuilder.Entity("Books.API.Entities.UserPermission", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("UserPermissions");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("76b3712d-dcad-4755-b7be-c960ce44c395"),
                            PermissionId = new Guid("aef3c363-dbcb-43b9-99ff-197f2dbf9e12")
                        },
                        new
                        {
                            UserId = new Guid("437201cc-8f91-41aa-9b15-99b97de0c229"),
                            PermissionId = new Guid("35d77900-e205-4118-9e0a-5b4da53f741e")
                        },
                        new
                        {
                            UserId = new Guid("3c8f1da3-9a18-4281-827f-6e4cad52d675"),
                            PermissionId = new Guid("5248737e-7105-4b59-be5d-47c3bc72a767")
                        },
                        new
                        {
                            UserId = new Guid("3c8f1da3-9a18-4281-827f-6e4cad52d675"),
                            PermissionId = new Guid("ac0d40af-f0dd-44bd-ad5d-83ee2bcfa051")
                        });
                });

            modelBuilder.Entity("Books.API.Entities.UserPermission", b =>
                {
                    b.HasOne("Books.API.Entities.Permission", "Permission")
                        .WithMany("UserPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Books.API.Entities.User", "User")
                        .WithMany("UserPermissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

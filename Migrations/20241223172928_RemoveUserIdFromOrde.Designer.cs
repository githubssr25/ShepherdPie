﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShepherdPie.Data;

#nullable disable

namespace ShepherdPie.Migrations
{
    [DbContext(typeof(ShepherdPieDbContext))]
    [Migration("20241223172928_RemoveUserIdFromOrde")]
    partial class RemoveUserIdFromOrde
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ShepherdPie.Models.Condiment", b =>
                {
                    b.Property<int>("CondimentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CondimentId"));

                    b.Property<string>("CondimentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CondimentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.HasKey("CondimentId");

                    b.ToTable("Condiments");

                    b.HasData(
                        new
                        {
                            CondimentId = 1,
                            CondimentName = "Red Sauce",
                            CondimentType = "Sauce",
                            Cost = 0.50m
                        },
                        new
                        {
                            CondimentId = 2,
                            CondimentName = "Garlic White Sauce",
                            CondimentType = "Sauce",
                            Cost = 0.75m
                        },
                        new
                        {
                            CondimentId = 3,
                            CondimentName = "Sausage",
                            CondimentType = "Meat",
                            Cost = 1.50m
                        },
                        new
                        {
                            CondimentId = 4,
                            CondimentName = "Pepperoni",
                            CondimentType = "Meat",
                            Cost = 1.75m
                        },
                        new
                        {
                            CondimentId = 5,
                            CondimentName = "Buffalo Mozzarella",
                            CondimentType = "Cheese",
                            Cost = 1.25m
                        },
                        new
                        {
                            CondimentId = 6,
                            CondimentName = "Parmesan",
                            CondimentType = "Cheese",
                            Cost = 1.50m
                        },
                        new
                        {
                            CondimentId = 7,
                            CondimentName = "Mushroom",
                            CondimentType = "Vegetable Topping",
                            Cost = 0.75m
                        },
                        new
                        {
                            CondimentId = 8,
                            CondimentName = "Jalapeno",
                            CondimentType = "Vegetable Topping",
                            Cost = 0.50m
                        },
                        new
                        {
                            CondimentId = 9,
                            CondimentName = "Green Pepper",
                            CondimentType = "Vegetable Topping",
                            Cost = 0.50m
                        },
                        new
                        {
                            CondimentId = 10,
                            CondimentName = "Black Olive",
                            CondimentType = "Vegetable Topping",
                            Cost = 0.75m
                        });
                });

            modelBuilder.Entity("ShepherdPie.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            Name = "Alice"
                        },
                        new
                        {
                            EmployeeId = 2,
                            Name = "Bob"
                        },
                        new
                        {
                            EmployeeId = 3,
                            Name = "Charlie"
                        },
                        new
                        {
                            EmployeeId = 4,
                            Name = "Diana"
                        },
                        new
                        {
                            EmployeeId = 5,
                            Name = "Edward"
                        });
                });

            modelBuilder.Entity("ShepherdPie.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<decimal>("DeliveryFee")
                        .HasColumnType("numeric");

                    b.Property<int?>("DeliveryPersonEmployeeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("OrderTakerEmployeeId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("TipLeftCustomer")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.HasKey("OrderId");

                    b.HasIndex("DeliveryPersonEmployeeId");

                    b.HasIndex("OrderTakerEmployeeId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            DeliveryFee = 5.00m,
                            DeliveryPersonEmployeeId = 2,
                            OrderDate = new DateTime(2024, 12, 14, 19, 0, 0, 0, DateTimeKind.Utc),
                            OrderStatus = "Completed",
                            OrderTakerEmployeeId = 1,
                            TipLeftCustomer = 5.00m,
                            TotalAmount = 50.00m
                        },
                        new
                        {
                            OrderId = 2,
                            DeliveryFee = 5.00m,
                            DeliveryPersonEmployeeId = 3,
                            OrderDate = new DateTime(2024, 12, 15, 19, 0, 0, 0, DateTimeKind.Utc),
                            OrderStatus = "Pending",
                            OrderTakerEmployeeId = 2,
                            TipLeftCustomer = 3.00m,
                            TotalAmount = 30.00m
                        },
                        new
                        {
                            OrderId = 3,
                            DeliveryFee = 5.00m,
                            DeliveryPersonEmployeeId = 4,
                            OrderDate = new DateTime(2024, 12, 16, 19, 0, 0, 0, DateTimeKind.Utc),
                            OrderStatus = "Completed",
                            OrderTakerEmployeeId = 1,
                            TipLeftCustomer = 4.00m,
                            TotalAmount = 40.00m
                        },
                        new
                        {
                            OrderId = 4,
                            DeliveryFee = 5.00m,
                            DeliveryPersonEmployeeId = 2,
                            OrderDate = new DateTime(2024, 12, 17, 19, 0, 0, 0, DateTimeKind.Utc),
                            OrderStatus = "Pending",
                            OrderTakerEmployeeId = 3,
                            TipLeftCustomer = 2.00m,
                            TotalAmount = 25.00m
                        },
                        new
                        {
                            OrderId = 5,
                            DeliveryFee = 5.00m,
                            DeliveryPersonEmployeeId = 1,
                            OrderDate = new DateTime(2024, 12, 18, 19, 0, 0, 0, DateTimeKind.Utc),
                            OrderStatus = "Pending",
                            OrderTakerEmployeeId = 2,
                            TipLeftCustomer = 3.50m,
                            TotalAmount = 35.00m
                        },
                        new
                        {
                            OrderId = 6,
                            DeliveryFee = 5.00m,
                            DeliveryPersonEmployeeId = 4,
                            OrderDate = new DateTime(2024, 12, 19, 19, 0, 0, 0, DateTimeKind.Utc),
                            OrderStatus = "Pending",
                            OrderTakerEmployeeId = 1,
                            TipLeftCustomer = 5.50m,
                            TotalAmount = 55.00m
                        },
                        new
                        {
                            OrderId = 7,
                            DeliveryFee = 5.00m,
                            DeliveryPersonEmployeeId = 2,
                            OrderDate = new DateTime(2024, 12, 20, 7, 0, 0, 0, DateTimeKind.Utc),
                            OrderStatus = "Pending",
                            OrderTakerEmployeeId = 3,
                            TipLeftCustomer = 1.50m,
                            TotalAmount = 20.00m
                        });
                });

            modelBuilder.Entity("ShepherdPie.Models.Pizza", b =>
                {
                    b.Property<int>("PizzaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PizzaId"));

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("numeric");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PizzaId");

                    b.HasIndex("OrderId");

                    b.ToTable("Pizzas");

                    b.HasData(
                        new
                        {
                            PizzaId = 1,
                            BasePrice = 15.00m,
                            OrderId = 1,
                            Size = "Large"
                        },
                        new
                        {
                            PizzaId = 2,
                            BasePrice = 12.00m,
                            OrderId = 1,
                            Size = "Medium"
                        },
                        new
                        {
                            PizzaId = 3,
                            BasePrice = 10.00m,
                            OrderId = 2,
                            Size = "Small"
                        },
                        new
                        {
                            PizzaId = 4,
                            BasePrice = 15.00m,
                            OrderId = 3,
                            Size = "Large"
                        },
                        new
                        {
                            PizzaId = 5,
                            BasePrice = 12.00m,
                            OrderId = 4,
                            Size = "Medium"
                        },
                        new
                        {
                            PizzaId = 6,
                            BasePrice = 10.00m,
                            OrderId = 5,
                            Size = "Small"
                        },
                        new
                        {
                            PizzaId = 7,
                            BasePrice = 15.00m,
                            OrderId = 6,
                            Size = "Large"
                        },
                        new
                        {
                            PizzaId = 8,
                            BasePrice = 12.00m,
                            OrderId = 7,
                            Size = "Medium"
                        });
                });

            modelBuilder.Entity("ShepherdPie.Models.PizzaCondiment", b =>
                {
                    b.Property<int>("PizzaCondimentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PizzaCondimentId"));

                    b.Property<int>("CondimentId")
                        .HasColumnType("integer");

                    b.Property<int>("PizzaId")
                        .HasColumnType("integer");

                    b.HasKey("PizzaCondimentId");

                    b.HasIndex("CondimentId");

                    b.HasIndex("PizzaId");

                    b.ToTable("PizzaCondiments");

                    b.HasData(
                        new
                        {
                            PizzaCondimentId = 1,
                            CondimentId = 1,
                            PizzaId = 1
                        },
                        new
                        {
                            PizzaCondimentId = 2,
                            CondimentId = 2,
                            PizzaId = 1
                        },
                        new
                        {
                            PizzaCondimentId = 3,
                            CondimentId = 3,
                            PizzaId = 2
                        },
                        new
                        {
                            PizzaCondimentId = 4,
                            CondimentId = 4,
                            PizzaId = 2
                        },
                        new
                        {
                            PizzaCondimentId = 5,
                            CondimentId = 5,
                            PizzaId = 3
                        },
                        new
                        {
                            PizzaCondimentId = 6,
                            CondimentId = 6,
                            PizzaId = 4
                        },
                        new
                        {
                            PizzaCondimentId = 7,
                            CondimentId = 7,
                            PizzaId = 5
                        },
                        new
                        {
                            PizzaCondimentId = 8,
                            CondimentId = 8,
                            PizzaId = 6
                        },
                        new
                        {
                            PizzaCondimentId = 9,
                            CondimentId = 1,
                            PizzaId = 7
                        },
                        new
                        {
                            PizzaCondimentId = 10,
                            CondimentId = 2,
                            PizzaId = 8
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShepherdPie.Models.Order", b =>
                {
                    b.HasOne("ShepherdPie.Models.Employee", "DeliveryPerson")
                        .WithMany("DeliveredOrders")
                        .HasForeignKey("DeliveryPersonEmployeeId");

                    b.HasOne("ShepherdPie.Models.Employee", "OrderTaker")
                        .WithMany("TakenOrders")
                        .HasForeignKey("OrderTakerEmployeeId");

                    b.Navigation("DeliveryPerson");

                    b.Navigation("OrderTaker");
                });

            modelBuilder.Entity("ShepherdPie.Models.Pizza", b =>
                {
                    b.HasOne("ShepherdPie.Models.Order", "Order")
                        .WithMany("Pizzas")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ShepherdPie.Models.PizzaCondiment", b =>
                {
                    b.HasOne("ShepherdPie.Models.Condiment", "Condiment")
                        .WithMany("PizzaCondiments")
                        .HasForeignKey("CondimentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShepherdPie.Models.Pizza", "Pizza")
                        .WithMany("PizzaCondiments")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Condiment");

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("ShepherdPie.Models.Condiment", b =>
                {
                    b.Navigation("PizzaCondiments");
                });

            modelBuilder.Entity("ShepherdPie.Models.Employee", b =>
                {
                    b.Navigation("DeliveredOrders");

                    b.Navigation("TakenOrders");
                });

            modelBuilder.Entity("ShepherdPie.Models.Order", b =>
                {
                    b.Navigation("Pizzas");
                });

            modelBuilder.Entity("ShepherdPie.Models.Pizza", b =>
                {
                    b.Navigation("PizzaCondiments");
                });
#pragma warning restore 612, 618
        }
    }
}
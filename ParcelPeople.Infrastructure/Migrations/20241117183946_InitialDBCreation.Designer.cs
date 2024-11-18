﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParcelPeople.Infrastructure.DbContexts;

#nullable disable

namespace ParcelPeople.Infrastructure.Migrations
{
    [DbContext(typeof(ShipmentDbContext))]
    [Migration("20241117183946_InitialDBCreation")]
    partial class InitialDBCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("ParcelPeople.Domain.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("OriginCost")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "United Kingdom",
                            Name = "London",
                            OriginCost = 10m
                        },
                        new
                        {
                            Id = 2,
                            Country = "Ireland",
                            Name = "Dublin",
                            OriginCost = 20m
                        },
                        new
                        {
                            Id = 3,
                            Country = "Scotland",
                            Name = "Edinburgh",
                            OriginCost = 10m
                        });
                });

            modelBuilder.Entity("ParcelPeople.Domain.Entities.Parcel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double?>("Dimensions")
                        .HasColumnType("REAL");

                    b.Property<Guid>("ShipmentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ShipmentId");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("ParcelPeople.Domain.Entities.ParcelSurcharge", b =>
                {
                    b.Property<double>("DimensionThreshold")
                        .HasColumnType("REAL");

                    b.Property<decimal>("Surcharge")
                        .HasColumnType("TEXT");

                    b.HasKey("DimensionThreshold");

                    b.ToTable("ParcelSurcharges");

                    b.HasData(
                        new
                        {
                            DimensionThreshold = 0.0,
                            Surcharge = 0m
                        },
                        new
                        {
                            DimensionThreshold = 50.0,
                            Surcharge = 0.2m
                        },
                        new
                        {
                            DimensionThreshold = 100.0,
                            Surcharge = 0.4m
                        });
                });

            modelBuilder.Entity("ParcelPeople.Domain.Entities.Shipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Cost")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReceiverName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("ParcelPeople.Domain.Entities.ShipmentCity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Destination")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Origin")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ShipmentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("TimeOfArrival")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ShipmentId");

                    b.ToTable("ShipmentCities");
                });

            modelBuilder.Entity("ParcelPeople.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email", "ContactNumber")
                        .IsUnique();

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c2e9e363-2e20-433a-88d6-3c1090d85d52"),
                            ContactNumber = "+4477788899911",
                            Email = "kolaoj@test.com",
                            FirstName = "Kola",
                            LastName = "Oj"
                        });
                });

            modelBuilder.Entity("ParcelPeople.Domain.Entities.Parcel", b =>
                {
                    b.HasOne("ParcelPeople.Domain.Entities.Shipment", null)
                        .WithMany("Parcels")
                        .HasForeignKey("ShipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ParcelPeople.Domain.Entities.Shipment", b =>
                {
                    b.HasOne("ParcelPeople.Domain.Models.Customer", null)
                        .WithMany("Shipments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ParcelPeople.Domain.Entities.ShipmentCity", b =>
                {
                    b.HasOne("ParcelPeople.Domain.Entities.Shipment", null)
                        .WithMany("Cities")
                        .HasForeignKey("ShipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ParcelPeople.Domain.Entities.Shipment", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Parcels");
                });

            modelBuilder.Entity("ParcelPeople.Domain.Models.Customer", b =>
                {
                    b.Navigation("Shipments");
                });
#pragma warning restore 612, 618
        }
    }
}

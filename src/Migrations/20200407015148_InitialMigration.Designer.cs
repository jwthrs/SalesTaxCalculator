﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalesTaxCalculator.Context;

namespace SalesTaxCalculator.Migrations
{
    [DbContext(typeof(SalesTaxContext))]
    [Migration("20200407015148_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SalesTaxCalculator.Models.CountyTax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StateSalesTaxId")
                        .HasColumnType("int");

                    b.Property<string>("TaxRate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StateSalesTaxId");

                    b.ToTable("CountyTax");
                });

            modelBuilder.Entity("SalesTaxCalculator.Models.StateSalesTax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxRate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("stateSalesTaxes");
                });

            modelBuilder.Entity("SalesTaxCalculator.Models.CountyTax", b =>
                {
                    b.HasOne("SalesTaxCalculator.Models.StateSalesTax", null)
                        .WithMany("countyTaxes")
                        .HasForeignKey("StateSalesTaxId");
                });
#pragma warning restore 612, 618
        }
    }
}
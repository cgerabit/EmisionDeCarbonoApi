﻿// <auto-generated />
using System;
using EmisionDeCarbonoApi.Infraestructure.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmisionDeCarbonoApi.Infraestructure.Migrations
{
    [DbContext(typeof(EmisionesDbContext))]
    [Migration("20241006040938_AgregarEntidadesParaEmisionDeCarbono")]
    partial class AgregarEntidadesParaEmisionDeCarbono
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EmisionDeCarbonoApi.Domain.Entidades.EmisionCarbono", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(1200)
                        .HasColumnType("varchar(1200)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaEmision")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TipoEmision")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("EmisionesDeCarbono");
                });

            modelBuilder.Entity("EmisionDeCarbonoApi.Domain.Entidades.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("EmisionDeCarbonoApi.Domain.Entidades.EmisionCarbono", b =>
                {
                    b.HasOne("EmisionDeCarbonoApi.Domain.Entidades.Empresa", "Empresa")
                        .WithMany("EmisionesDeCarbono")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("EmisionDeCarbonoApi.Domain.Entidades.Empresa", b =>
                {
                    b.Navigation("EmisionesDeCarbono");
                });
#pragma warning restore 612, 618
        }
    }
}

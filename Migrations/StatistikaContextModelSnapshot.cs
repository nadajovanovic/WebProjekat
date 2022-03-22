﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Projekat.Migrations
{
    [DbContext(typeof(StatistikaContext))]
    partial class StatistikaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Models.Igrac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Godina")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<long>("JMBG")
                        .HasColumnType("bigint");

                    b.Property<int?>("PozicijaIgracaID")
                        .HasColumnType("int");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("TimIgracaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PozicijaIgracaID");

                    b.HasIndex("TimIgracaID");

                    b.ToTable("Igraci");
                });

            modelBuilder.Entity("Models.Liga", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Lige");
                });

            modelBuilder.Entity("Models.Pozicija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Pozicije");
                });

            modelBuilder.Entity("Models.Statistika", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CrveniKartoni")
                        .HasColumnType("int");

                    b.Property<int>("Golovi")
                        .HasColumnType("int");

                    b.Property<int>("Korneri")
                        .HasColumnType("int");

                    b.Property<int>("OdbraneGolmana")
                        .HasColumnType("int");

                    b.Property<int>("Ofsajdi")
                        .HasColumnType("int");

                    b.Property<int>("Prekrsaji")
                        .HasColumnType("int");

                    b.Property<int>("SlobodniUdarci")
                        .HasColumnType("int");

                    b.Property<int>("SuteviUOkrvi")
                        .HasColumnType("int");

                    b.Property<int>("SuteviVanOkvrira")
                        .HasColumnType("int");

                    b.Property<int?>("TimID")
                        .HasColumnType("int");

                    b.Property<int>("UkupnoSuteva")
                        .HasColumnType("int");

                    b.Property<int?>("UtakmicaID")
                        .HasColumnType("int");

                    b.Property<int>("ZutiKartoni")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TimID");

                    b.HasIndex("UtakmicaID");

                    b.ToTable("Statistike");
                });

            modelBuilder.Entity("Models.Tim", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("LigaTimaID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Predsednik")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Trener")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("LigaTimaID");

                    b.ToTable("Timovi");
                });

            modelBuilder.Entity("Models.Utakmica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LigaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("LigaID");

                    b.ToTable("Utakmice");
                });

            modelBuilder.Entity("TimUtakmica", b =>
                {
                    b.Property<int>("TimoviID")
                        .HasColumnType("int");

                    b.Property<int>("UtakmiceID")
                        .HasColumnType("int");

                    b.HasKey("TimoviID", "UtakmiceID");

                    b.HasIndex("UtakmiceID");

                    b.ToTable("TimUtakmica");
                });

            modelBuilder.Entity("Models.Igrac", b =>
                {
                    b.HasOne("Models.Pozicija", "PozicijaIgraca")
                        .WithMany()
                        .HasForeignKey("PozicijaIgracaID");

                    b.HasOne("Models.Tim", "TimIgraca")
                        .WithMany("Igraci")
                        .HasForeignKey("TimIgracaID");

                    b.Navigation("PozicijaIgraca");

                    b.Navigation("TimIgraca");
                });

            modelBuilder.Entity("Models.Statistika", b =>
                {
                    b.HasOne("Models.Tim", "Tim")
                        .WithMany()
                        .HasForeignKey("TimID");

                    b.HasOne("Models.Utakmica", "Utakmica")
                        .WithMany()
                        .HasForeignKey("UtakmicaID");

                    b.Navigation("Tim");

                    b.Navigation("Utakmica");
                });

            modelBuilder.Entity("Models.Tim", b =>
                {
                    b.HasOne("Models.Liga", "LigaTima")
                        .WithMany("Timovi")
                        .HasForeignKey("LigaTimaID");

                    b.Navigation("LigaTima");
                });

            modelBuilder.Entity("Models.Utakmica", b =>
                {
                    b.HasOne("Models.Liga", "Liga")
                        .WithMany("Utakmice")
                        .HasForeignKey("LigaID");

                    b.Navigation("Liga");
                });

            modelBuilder.Entity("TimUtakmica", b =>
                {
                    b.HasOne("Models.Tim", null)
                        .WithMany()
                        .HasForeignKey("TimoviID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Utakmica", null)
                        .WithMany()
                        .HasForeignKey("UtakmiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Liga", b =>
                {
                    b.Navigation("Timovi");

                    b.Navigation("Utakmice");
                });

            modelBuilder.Entity("Models.Tim", b =>
                {
                    b.Navigation("Igraci");
                });
#pragma warning restore 612, 618
        }
    }
}

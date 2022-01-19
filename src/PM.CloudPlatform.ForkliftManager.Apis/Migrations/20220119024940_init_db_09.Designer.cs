﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PM.CloudPlatform.ForkliftManager.Apis.Data;

namespace PM.CloudPlatform.ForkliftManager.Apis.Migrations
{
    [DbContext(typeof(ForkliftManagerDbContext))]
    [Migration("20220119024940_init_db_09")]
    partial class init_db_09
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ModuleRole", b =>
                {
                    b.Property<Guid>("ModulesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid");

                    b.HasKey("ModulesId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("ModuleRole");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Brand")
                        .HasColumnType("text");

                    b.Property<DateTime?>("BuyTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CarModel")
                        .HasColumnType("text");

                    b.Property<Guid>("CarTypeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ElectronicFenceId")
                        .HasColumnType("uuid");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<int?>("LastLengthOfUse")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastMaintenanceOfTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("LengthOfUse")
                        .HasColumnType("integer");

                    b.Property<string>("LicensePlateNumber")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("NumberOfMaintenance")
                        .HasColumnType("integer");

                    b.Property<Guid?>("RentalCompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.Property<Guid?>("TerminalId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CarTypeId");

                    b.HasIndex("ElectronicFenceId");

                    b.HasIndex("RentalCompanyId");

                    b.HasIndex("TerminalId")
                        .IsUnique();

                    b.ToTable("Car");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.CarMaintenanceRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<string>("Maintainer")
                        .HasColumnType("text");

                    b.Property<string>("MaintainerContent")
                        .HasColumnType("text");

                    b.Property<string>("MaintainerTel")
                        .HasColumnType("text");

                    b.Property<int>("MaintenanceDateLength")
                        .HasColumnType("integer");

                    b.Property<DateTime>("MaintenanceTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Remarks")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("CarMaintenanceRecord");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.CarType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CarType");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.ElectronicFence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<string>("LngLats")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RentalCompanyId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RentalCompanyId");

                    b.ToTable("ElectronicFence");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.GpsPositionRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte?>("AccState")
                        .HasColumnType("smallint");

                    b.Property<int>("CellId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<byte?>("DataReportingMode")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<int>("EorWLon")
                        .HasColumnType("integer");

                    b.Property<int?>("GpsInfoLength")
                        .HasColumnType("integer");

                    b.Property<int?>("GpsLocatedFunc")
                        .HasColumnType("integer");

                    b.Property<int?>("GpsRealTimeHeadIn")
                        .HasColumnType("integer");

                    b.Property<int?>("GpsSatelliteCount")
                        .HasColumnType("integer");

                    b.Property<int?>("Heading")
                        .HasColumnType("integer");

                    b.Property<int?>("IsGpsLocated")
                        .HasColumnType("integer");

                    b.Property<int>("LAC")
                        .HasColumnType("integer");

                    b.Property<decimal>("Lat")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Lon")
                        .HasColumnType("numeric");

                    b.Property<int>("MCC")
                        .HasColumnType("integer");

                    b.Property<byte>("MNC")
                        .HasColumnType("smallint");

                    b.Property<long?>("Mileage")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("SorNLat")
                        .HasColumnType("integer");

                    b.Property<byte>("Speed")
                        .HasColumnType("smallint");

                    b.Property<Guid?>("TerminalId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TerminalId");

                    b.ToTable("GpsPositionRecord");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentModuleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentModuleId");

                    b.ToTable("Module");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.RentalCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RentalCompany");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.RentalRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CarId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReturn")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("RentalCompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("RentalEmployeeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RentalEmployeeTel")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RentalEndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("RentalStartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ReturnAckEmployeeName")
                        .HasColumnType("text");

                    b.Property<string>("ReturnAckEmployeeTel")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ReturnTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("RentalCompanyId");

                    b.ToTable("RentalRecord");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.Terminal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CarId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<string>("IMEI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Terminal");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.TerminalBindRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TerminalId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("TerminalId");

                    b.ToTable("TerminalBindRecord");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.TerminalLoginRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TerminalId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TerminalId");

                    b.ToTable("TerminalLoginRecord");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.UseRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("LengthOfTime")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("UseRecord");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreateUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreateUserName")
                        .HasColumnType("text");

                    b.Property<bool>("DeleteMark")
                        .HasColumnType("boolean");

                    b.Property<bool>("EnableMark")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ModifyUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ModifyUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("ModuleRole", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Module", null)
                        .WithMany()
                        .HasForeignKey("ModulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.Car", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.CarType", "CarType")
                        .WithMany("Cars")
                        .HasForeignKey("CarTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.ElectronicFence", null)
                        .WithMany("Cars")
                        .HasForeignKey("ElectronicFenceId");

                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.RentalCompany", null)
                        .WithMany("Cars")
                        .HasForeignKey("RentalCompanyId");

                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Terminal", "Terminal")
                        .WithOne("Car")
                        .HasForeignKey("PM.CloudPlatform.ForkliftManager.Apis.Entities.Car", "TerminalId");

                    b.Navigation("CarType");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.CarMaintenanceRecord", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Car", "Car")
                        .WithMany("CarMaintenanceRecords")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.ElectronicFence", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.RentalCompany", "RentalCompany")
                        .WithMany("ElectronicFences")
                        .HasForeignKey("RentalCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RentalCompany");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.GpsPositionRecord", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Terminal", "Terminal")
                        .WithMany("GpsPositionRecords")
                        .HasForeignKey("TerminalId");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.Module", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Module", "ParentModule")
                        .WithMany()
                        .HasForeignKey("ParentModuleId");

                    b.Navigation("ParentModule");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.RentalRecord", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Car", "Car")
                        .WithMany("RentalRecords")
                        .HasForeignKey("CarId");

                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.RentalCompany", "RentalCompany")
                        .WithMany()
                        .HasForeignKey("RentalCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("RentalCompany");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.TerminalBindRecord", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Car", "Car")
                        .WithMany("TerminalBindRecords")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Terminal", "Terminal")
                        .WithMany("TerminalBindRecords")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.TerminalLoginRecord", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Terminal", "Terminal")
                        .WithMany()
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.UseRecord", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Car", "Car")
                        .WithMany("UseRecords")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PM.CloudPlatform.ForkliftManager.Apis.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.Car", b =>
                {
                    b.Navigation("CarMaintenanceRecords");

                    b.Navigation("RentalRecords");

                    b.Navigation("TerminalBindRecords");

                    b.Navigation("UseRecords");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.CarType", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.ElectronicFence", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.RentalCompany", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("ElectronicFences");
                });

            modelBuilder.Entity("PM.CloudPlatform.ForkliftManager.Apis.Entities.Terminal", b =>
                {
                    b.Navigation("Car");

                    b.Navigation("GpsPositionRecords");

                    b.Navigation("TerminalBindRecords");
                });
#pragma warning restore 612, 618
        }
    }
}

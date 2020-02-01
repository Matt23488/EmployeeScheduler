﻿// <auto-generated />
using System;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeScheduler.Lib.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeScheduler.Lib.Migrations
{
    [DbContext(typeof(SchedulerContext))]
    [Migration("20200201034834_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("EmployeeScheduler.Lib.DAL.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<int>("TypicalWeekID")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeeScheduler.Lib.DAL.EmployeeSchedule", b =>
                {
                    b.Property<int>("EmployeeScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("From")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsOff")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LunchType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ScheduleDayID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("To")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TypicalWeekID")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmployeeScheduleID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("ScheduleDayID");

                    b.HasIndex("TypicalWeekID");

                    b.ToTable("EmployeeSchedules");
                });

            modelBuilder.Entity("EmployeeScheduler.Lib.DAL.ScheduleDay", b =>
                {
                    b.Property<int>("ScheduleDayID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ScheduleWeekID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ScheduleDayID");

                    b.HasIndex("ScheduleWeekID");

                    b.ToTable("ScheduleDays");
                });

            modelBuilder.Entity("EmployeeScheduler.Lib.DAL.ScheduleWeek", b =>
                {
                    b.Property<long>("ScheduleWeekID")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.HasKey("ScheduleWeekID");

                    b.ToTable("ScheduleWeeks");
                });

            modelBuilder.Entity("EmployeeScheduler.Lib.DAL.Token", b =>
                {
                    b.Property<int>("TokenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("TEXT");

                    b.Property<string>("IpAddress")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TokenValue")
                        .HasColumnType("TEXT");

                    b.HasKey("TokenID");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("EmployeeScheduler.Lib.DAL.TypicalWeek", b =>
                {
                    b.Property<int>("TypicalWeekID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("INTEGER");

                    b.HasKey("TypicalWeekID");

                    b.HasIndex("EmployeeID")
                        .IsUnique();

                    b.ToTable("TypicalWeek");
                });

            modelBuilder.Entity("EmployeeScheduler.Lib.DAL.EmployeeSchedule", b =>
                {
                    b.HasOne("EmployeeScheduler.Lib.DAL.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeScheduler.Lib.DAL.ScheduleDay", "ScheduleDay")
                        .WithMany("EmployeeSchedules")
                        .HasForeignKey("ScheduleDayID");

                    b.HasOne("EmployeeScheduler.Lib.DAL.TypicalWeek", null)
                        .WithMany("Days")
                        .HasForeignKey("TypicalWeekID");
                });

            modelBuilder.Entity("EmployeeScheduler.Lib.DAL.ScheduleDay", b =>
                {
                    b.HasOne("EmployeeScheduler.Lib.DAL.ScheduleWeek", "ScheduleWeek")
                        .WithMany("Days")
                        .HasForeignKey("ScheduleWeekID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeScheduler.Lib.DAL.TypicalWeek", b =>
                {
                    b.HasOne("EmployeeScheduler.Lib.DAL.Employee", "Employee")
                        .WithOne("TypicalSchedule")
                        .HasForeignKey("EmployeeScheduler.Lib.DAL.TypicalWeek", "EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
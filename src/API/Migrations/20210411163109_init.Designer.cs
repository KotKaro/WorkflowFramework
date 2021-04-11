﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace API.Migrations
{
    [DbContext(typeof(WorkflowFrameworkDbContext))]
    [Migration("20210411163109_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.ProcessAggregate.Argument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MemberDescriptorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProcessRunId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ValueString")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueString");

                    b.Property<string>("ValueType")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueType");

                    b.HasKey("Id");

                    b.HasIndex("MemberDescriptorId");

                    b.HasIndex("ProcessRunId");

                    b.ToTable("Argument");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.Expectation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AndExpectationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrExpectationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StepNavigatorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AndExpectationId");

                    b.HasIndex("OrExpectationId");

                    b.HasIndex("StepNavigatorId");

                    b.ToTable("Expectation");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Expectation");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.MemberDescriptor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ValueAccessorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ValueAccessorId");

                    b.ToTable("MemberDescriptor");

                    b.HasDiscriminator<string>("Discriminator").HasValue("MemberDescriptor");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Process", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Process");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.ProcessRun", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CurrentStepId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProcessId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CurrentStepId");

                    b.HasIndex("ProcessId");

                    b.ToTable("ProcessRun");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Step", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProcessId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId");

                    b.ToTable("Step");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.StepNavigator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StepId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TargetStepId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StepId");

                    b.HasIndex("TargetStepId");

                    b.ToTable("StepNavigator");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.TypeMetadata", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Type");

                    b.HasKey("Type");

                    b.ToTable("TypeMetadata");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.AggregateExpectations.AndExpectation", b =>
                {
                    b.HasBaseType("Domain.ProcessAggregate.Expectations.Expectation");

                    b.Property<string>("DescribedTypeFullName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DescribedTypeFullName");

                    b.HasDiscriminator().HasValue("AndExpectation");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.AggregateExpectations.OrExpectation", b =>
                {
                    b.HasBaseType("Domain.ProcessAggregate.Expectations.Expectation");

                    b.Property<string>("DescribedTypeFullName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DescribedTypeFullName");

                    b.HasDiscriminator().HasValue("OrExpectation");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.CompareExpectations.BiggerThanExpectation", b =>
                {
                    b.HasBaseType("Domain.ProcessAggregate.Expectations.Expectation");

                    b.Property<string>("DescribedTypeFullName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DescribedTypeFullName");

                    b.Property<Guid?>("ValueAccessorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BiggerThanExpectation_ValueAccessorId");

                    b.Property<string>("ValueString")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueString");

                    b.Property<string>("ValueType")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueType");

                    b.HasIndex("ValueAccessorId");

                    b.HasDiscriminator().HasValue("BiggerThanExpectation");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.CompareExpectations.EqualExpectation", b =>
                {
                    b.HasBaseType("Domain.ProcessAggregate.Expectations.Expectation");

                    b.Property<string>("DescribedTypeFullName")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DescribedTypeFullName");

                    b.Property<Guid?>("ValueAccessorId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EqualExpectation_ValueAccessorId");

                    b.Property<string>("ValueString")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueString");

                    b.Property<string>("ValueType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueType");

                    b.HasIndex("ValueAccessorId");

                    b.HasDiscriminator().HasValue("EqualExpectation");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.CompareExpectations.LessThanExpectation", b =>
                {
                    b.HasBaseType("Domain.ProcessAggregate.Expectations.Expectation");

                    b.Property<string>("DescribedTypeFullName")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DescribedTypeFullName");

                    b.Property<Guid?>("ValueAccessorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ValueString")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueString");

                    b.Property<string>("ValueType")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ValueType");

                    b.HasIndex("ValueAccessorId");

                    b.HasDiscriminator().HasValue("LessThanExpectation");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.ValueAccessor", b =>
                {
                    b.HasBaseType("Domain.ProcessAggregate.MemberDescriptor");

                    b.Property<string>("OwningType")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OwningType");

                    b.Property<string>("TypeMetadataType")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("TypeMetadataType");

                    b.HasDiscriminator().HasValue("ValueAccessor");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Argument", b =>
                {
                    b.HasOne("Domain.ProcessAggregate.MemberDescriptor", "MemberDescriptor")
                        .WithMany()
                        .HasForeignKey("MemberDescriptorId");

                    b.HasOne("Domain.ProcessAggregate.ProcessRun", null)
                        .WithMany("Arguments")
                        .HasForeignKey("ProcessRunId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("MemberDescriptor");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.Expectation", b =>
                {
                    b.HasOne("Domain.ProcessAggregate.Expectations.AggregateExpectations.AndExpectation", null)
                        .WithMany("Expectations")
                        .HasForeignKey("AndExpectationId");

                    b.HasOne("Domain.ProcessAggregate.Expectations.AggregateExpectations.OrExpectation", null)
                        .WithMany("Expectations")
                        .HasForeignKey("OrExpectationId");

                    b.HasOne("Domain.ProcessAggregate.StepNavigator", null)
                        .WithMany("Expectations")
                        .HasForeignKey("StepNavigatorId");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.MemberDescriptor", b =>
                {
                    b.HasOne("Domain.ProcessAggregate.ValueAccessor", null)
                        .WithMany("MethodArguments")
                        .HasForeignKey("ValueAccessorId");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Process", b =>
                {
                    b.OwnsOne("Domain.Common.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProcessId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("ProcessId");

                            b1.ToTable("Process");

                            b1.WithOwner()
                                .HasForeignKey("ProcessId");
                        });

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.ProcessRun", b =>
                {
                    b.HasOne("Domain.ProcessAggregate.Step", "CurrentStep")
                        .WithMany()
                        .HasForeignKey("CurrentStepId");

                    b.HasOne("Domain.ProcessAggregate.Process", "Process")
                        .WithMany()
                        .HasForeignKey("ProcessId");

                    b.Navigation("CurrentStep");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Step", b =>
                {
                    b.HasOne("Domain.ProcessAggregate.Process", null)
                        .WithMany("Steps")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Domain.Common.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("StepId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("StepId");

                            b1.ToTable("Step");

                            b1.WithOwner()
                                .HasForeignKey("StepId");
                        });

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.StepNavigator", b =>
                {
                    b.HasOne("Domain.ProcessAggregate.Step", null)
                        .WithMany("StepNavigators")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.ProcessAggregate.Step", "TargetStep")
                        .WithMany()
                        .HasForeignKey("TargetStepId");

                    b.Navigation("TargetStep");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.CompareExpectations.BiggerThanExpectation", b =>
                {
                    b.HasOne("Domain.ProcessAggregate.ValueAccessor", "ValueAccessor")
                        .WithMany()
                        .HasForeignKey("ValueAccessorId");

                    b.Navigation("ValueAccessor");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.CompareExpectations.EqualExpectation", b =>
                {
                    b.HasOne("Domain.ProcessAggregate.ValueAccessor", "ValueAccessor")
                        .WithMany()
                        .HasForeignKey("ValueAccessorId");

                    b.Navigation("ValueAccessor");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.CompareExpectations.LessThanExpectation", b =>
                {
                    b.HasOne("Domain.ProcessAggregate.ValueAccessor", "ValueAccessor")
                        .WithMany()
                        .HasForeignKey("ValueAccessorId");

                    b.Navigation("ValueAccessor");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.ValueAccessor", b =>
                {
                    b.HasOne("Domain.ProcessAggregate.TypeMetadata", null)
                        .WithMany("ValueAccessors")
                        .HasForeignKey("TypeMetadataType")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Process", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.ProcessRun", b =>
                {
                    b.Navigation("Arguments");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Step", b =>
                {
                    b.Navigation("StepNavigators");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.StepNavigator", b =>
                {
                    b.Navigation("Expectations");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.TypeMetadata", b =>
                {
                    b.Navigation("ValueAccessors");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.AggregateExpectations.AndExpectation", b =>
                {
                    b.Navigation("Expectations");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.Expectations.AggregateExpectations.OrExpectation", b =>
                {
                    b.Navigation("Expectations");
                });

            modelBuilder.Entity("Domain.ProcessAggregate.ValueAccessor", b =>
                {
                    b.Navigation("MethodArguments");
                });
#pragma warning restore 612, 618
        }
    }
}
﻿// <auto-generated />
using System;
using LawyersFirm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LawyersFirm.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220313165018_Update-PriceTable")]
    partial class UpdatePriceTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("LawyersFirm.Models.DbTables.About", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Writer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Abouts");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Advantage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Award")
                        .HasColumnType("int");

                    b.Property<string>("CustomerCount")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<int>("Expert")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Advantages");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.AdvantageDesc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AdvantageId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdvantageId");

                    b.ToTable("AdvantageDescs");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Attorney", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("Education")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jobname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SummarySentence")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Attorneys");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.AttorneyAward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AttorneyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("AttorneyId");

                    b.ToTable("AttorneyAwards");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.AttorneyContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AttorneyId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facebook")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Linkedin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Twitter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AttorneyId");

                    b.ToTable("AttorneyContacts");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.AttorneyPractice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AttorneyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("AttorneyId");

                    b.ToTable("AttorneyPractices");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.FAQ", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("FAQs");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.FaqImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("FAQId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FAQId");

                    b.ToTable("FaqImages");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.FaqQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FAQId")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FAQId");

                    b.ToTable("FaqQuestions");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.FirmInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Earned")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FirmInfos");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.OfficeImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("FirmInfoId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FirmInfoId");

                    b.ToTable("OfficeImages");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Practice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Explonation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Practices");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Package")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceContentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PriceContentId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.PriceContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Writer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PriceContents");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.PriceToPractice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("PracticeId")
                        .HasColumnType("int");

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PracticeId");

                    b.HasIndex("PriceId");

                    b.ToTable("PriceToPractices");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.SliderImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("SliderId")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SliderId");

                    b.ToTable("SliderImages");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AboutId")
                        .HasColumnType("int");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AboutId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Testimonial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Componyname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Testimonials");
                });

            modelBuilder.Entity("LawyersFirm.Models.InfoDesc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("FirmInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FirmInfoId");

                    b.ToTable("InfoDescs");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.AdvantageDesc", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.Advantage", "Advantage")
                        .WithMany("AdvantageDescs")
                        .HasForeignKey("AdvantageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advantage");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.AttorneyAward", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.Attorney", "Attorney")
                        .WithMany("AttorneyAwards")
                        .HasForeignKey("AttorneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attorney");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.AttorneyContact", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.Attorney", "Attorney")
                        .WithMany("AttorneyContacts")
                        .HasForeignKey("AttorneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attorney");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.AttorneyPractice", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.Attorney", "Attorney")
                        .WithMany("AttorneyPractices")
                        .HasForeignKey("AttorneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attorney");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.FaqImage", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.FAQ", "FAQ")
                        .WithMany("FaqImages")
                        .HasForeignKey("FAQId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FAQ");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.FaqQuestion", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.FAQ", "FAQ")
                        .WithMany("FaqQuestions")
                        .HasForeignKey("FAQId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FAQ");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.OfficeImage", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.FirmInfo", "FirmInfo")
                        .WithMany("OfficeImages")
                        .HasForeignKey("FirmInfoId");

                    b.Navigation("FirmInfo");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Price", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.PriceContent", "PriceContent")
                        .WithMany("Prices")
                        .HasForeignKey("PriceContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriceContent");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.PriceToPractice", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.Practice", "Practice")
                        .WithMany("PriceToPractices")
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LawyersFirm.Models.DbTables.Price", "Price")
                        .WithMany("PriceToPractices")
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Practice");

                    b.Navigation("Price");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.SliderImage", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.Slider", "Slider")
                        .WithMany("SliderImages")
                        .HasForeignKey("SliderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Slider");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Subject", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.About", "About")
                        .WithMany("Subjects")
                        .HasForeignKey("AboutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("About");
                });

            modelBuilder.Entity("LawyersFirm.Models.InfoDesc", b =>
                {
                    b.HasOne("LawyersFirm.Models.DbTables.FirmInfo", "FirmInfo")
                        .WithMany("InfoDescs")
                        .HasForeignKey("FirmInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FirmInfo");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.About", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Advantage", b =>
                {
                    b.Navigation("AdvantageDescs");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Attorney", b =>
                {
                    b.Navigation("AttorneyAwards");

                    b.Navigation("AttorneyContacts");

                    b.Navigation("AttorneyPractices");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.FAQ", b =>
                {
                    b.Navigation("FaqImages");

                    b.Navigation("FaqQuestions");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.FirmInfo", b =>
                {
                    b.Navigation("InfoDescs");

                    b.Navigation("OfficeImages");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Practice", b =>
                {
                    b.Navigation("PriceToPractices");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Price", b =>
                {
                    b.Navigation("PriceToPractices");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.PriceContent", b =>
                {
                    b.Navigation("Prices");
                });

            modelBuilder.Entity("LawyersFirm.Models.DbTables.Slider", b =>
                {
                    b.Navigation("SliderImages");
                });
#pragma warning restore 612, 618
        }
    }
}

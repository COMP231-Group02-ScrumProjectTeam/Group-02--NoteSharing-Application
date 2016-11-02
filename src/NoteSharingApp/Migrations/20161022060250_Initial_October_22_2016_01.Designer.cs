using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NoteSharingApp.Models;

namespace NoteSharingApp.Migrations
{
    [DbContext(typeof(NoteSharingContext))]
    [Migration("20161022060250_Initial_October_22_2016_01")]
    partial class Initial_October_22_2016_01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NoteSharingApp.Models.Document", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DocumentTypeID");

                    b.Property<string>("Extension");

                    b.Property<string>("FileName");

                    b.Property<DateTime>("ModifiedData");

                    b.Property<byte[]>("Size");

                    b.Property<string>("Title");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("DocumentTypeID");

                    b.HasIndex("UserID");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("NoteSharingApp.Models.DocumentType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("NoteSharingApp.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("College_org");

                    b.Property<string>("EmailId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Occupation");

                    b.Property<string>("Program_field");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NoteSharingApp.Models.UserComment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<int>("DocumentID");

                    b.Property<int>("UserID");

                    b.Property<int?>("Vote");

                    b.HasKey("ID");

                    b.HasIndex("DocumentID");

                    b.HasIndex("UserID");

                    b.ToTable("UserComments");
                });

            modelBuilder.Entity("NoteSharingApp.Models.Document", b =>
                {
                    b.HasOne("NoteSharingApp.Models.DocumentType", "DocumentType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NoteSharingApp.Models.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NoteSharingApp.Models.UserComment", b =>
                {
                    b.HasOne("NoteSharingApp.Models.Document", "Document")
                        .WithMany("UserComments")
                        .HasForeignKey("DocumentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NoteSharingApp.Models.User", "User")
                        .WithMany("UserComments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SampleApplication.Server.Data;

namespace SampleApplication.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class SampleApplicationServerModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("SampleApplication.Server.Models.Employee", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd();

                b.Property<string>("FirstName").HasMaxLength(100);

                b.Property<string>("LastName").HasMaxLength(100);

                b.Property<string>("Email").HasMaxLength(200);

                b.Property<DateTime>("DateOfJoining");

                b.Property<string>("Position").HasMaxLength(100);

                b.HasKey("Id");

                b.ToTable("Employees");
            });
        }
    }
}

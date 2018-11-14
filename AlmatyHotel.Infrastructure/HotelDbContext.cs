using AlmatyHotel.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmatyHotel.Infrastructure
{
    public class HotelDbContext : DbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomDetail> RoomDetails { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(p => p.Bookings)
                .WithRequired(p=>p.OwnerUser)
                .HasForeignKey(p=>p.OwnerUserId);

            modelBuilder.Entity<Room>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Room>()
                .HasMany(p => p.Bookings)
                .WithRequired(p=>p.Room)
                .HasForeignKey(p=>p.RoomId);

            modelBuilder.Entity<Room>()
                .HasRequired(p => p.RoomDetail)
                .WithRequiredPrincipal(p => p.Room);

            base.OnModelCreating(modelBuilder);
        }


        public HotelDbContext() : base($"name=DefaultHotelDbConnection")
        {

        }
    }
}

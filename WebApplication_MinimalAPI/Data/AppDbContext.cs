﻿using Microsoft.EntityFrameworkCore;
using CouponAPI.Models;

namespace CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(
            new Coupon()
            {
                ID = 1,
                Name = "10% OFF",
                Precent = 10,
                IsActive = true,
            },
            new Coupon()
            {
                ID = 2,
                Name = "15% OFF",
                Precent = 15,
                IsActive = true,
            },
            new Coupon()
            {
                ID = 3,
                Name = "20% OFF",
                Precent = 20,
                IsActive = false,
            },
            new Coupon()
            {
                ID = 4,
                Name = "25% OFF",
                Precent = 25,
                IsActive = true,
            }
            );
      
        }
    }
}

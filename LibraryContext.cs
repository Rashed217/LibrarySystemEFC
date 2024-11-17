﻿using LibrarySystemEFC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemEFC
{
    public class LibraryContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(" Data Source=(local); Initial Catalog=LibrarySystemEFC; Integrated Security=true; TrustServerCertificate=True ");
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
            .Property(b => b.BID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>()
            .Property(c => c.CID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Admin>()
            .Property(a => a.AID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
            .Property(u => u.UID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Borrowing>()
            .Property(b => b.BorID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Borrowing>()
                .HasKey(b => new { b.UserId, b.BookId });

            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.User)
                .WithMany(u => u.Borrowings)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.Book)
                .WithMany(bk => bk.Borrowings)
                .HasForeignKey(b => b.BookId);
        }

        public LibraryContext(DbContextOptions options) : base(options)
        {
        }

        public LibraryContext()
        {
        }
    }
}
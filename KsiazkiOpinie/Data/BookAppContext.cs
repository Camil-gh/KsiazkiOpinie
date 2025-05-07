using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using KsiazkiOpinie.Models;

namespace KsiazkiOpinie.Data
{
    public class BookAppContext : DbContext
    {
        public BookAppContext(DbContextOptions<BookAppContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<CategoryBook> CategoryBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-many: Books ↔ Authors
            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorId, ab.BookId });

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorId);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookId);

            // Many-to-many: Books ↔ Categories
            modelBuilder.Entity<CategoryBook>()
                .HasKey(cb => new { cb.CategoryId, cb.BookId });

            modelBuilder.Entity<CategoryBook>()
                .HasOne(cb => cb.Category)
                .WithMany(c => c.CategoryBooks)
                .HasForeignKey(cb => cb.CategoryId);

            modelBuilder.Entity<CategoryBook>()
                .HasOne(cb => cb.Book)
                .WithMany(b => b.CategoryBooks)
                .HasForeignKey(cb => cb.BookId);

            // Book - User (one-to-many)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.User)
                .WithMany(u => u.Books)
                .HasForeignKey(b => b.UserId);

            // Book - Comment (one-to-many)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Book)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Book - Rating (one-to-many)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Ratings)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}

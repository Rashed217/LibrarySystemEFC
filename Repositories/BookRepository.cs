using LibrarySystemEFC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemEFC.Repositories
{
    public class BookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context; // Parameter for LibraryContext
        }

        public IEnumerable<Book> GetAll() // IEnumerable = Foreach loop
        {
            return _context.Books.Include(b => b.Category).ToList();
        }
        public Book GetById(int id) // Retrieves a book by ID
        {
            return _context.Books.Include(b => b.Category)
                                 .FirstOrDefault(b => b.BID == id);
        }
        public Book GetByName(string name) // Retrieves a book by name
        {
            return _context.Books.Include(b => b.Category)
                                 .FirstOrDefault(b => b.BName == name);
        }

        public void Insert(Book book) // Adds a new book
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public void UpdateById(int id, Book updatedBook)
        {
            var book = GetById(id); // Retrieves a book by ID
            if (book != null) // Checks if book is not null
            {
                book.BName = updatedBook.BName; // Replace book name with new one
                book.Author = updatedBook.Author; // Replace author name with new one
                book.TotalCopies = updatedBook.TotalCopies; // Replace number of total copies with new one
                book.BorrowedCopies = updatedBook.BorrowedCopies; // Replace number of borrowed copies with new one
                book.CopyPrice = updatedBook.CopyPrice; // Replace copy price with new one
                book.AllowedBorrowingPeriod = updatedBook.AllowedBorrowingPeriod; // Replace due date with new one
                book.CID = updatedBook.CID; // Replace category ID with new one
                _context.SaveChanges();
            }
        }

        public void UpdateByName(string name, Book updatedBook)
        {
            var book = GetByName(name); // Retrieves book by name
            if (book != null)
            {
                book.BName = updatedBook.BName; // Replace book name with new one
                book.Author = updatedBook.Author; // Replace author name with new one
                book.TotalCopies = updatedBook.TotalCopies; // Replace number of total copies with new one
                book.BorrowedCopies = updatedBook.BorrowedCopies; // Replace number of borrowed copies with new one
                book.CopyPrice = updatedBook.CopyPrice; // Replace copy price with new one
                book.AllowedBorrowingPeriod = updatedBook.AllowedBorrowingPeriod; // Replace due date with new one
                book.CID = updatedBook.CID; // Replace category ID with new one
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var book = _context.Books.Find(id); // Find book by ID
            if (book != null)
            {
                _context.Books.Remove(book); // Removes the book from collection
                _context.SaveChanges();
            }
        }

        public decimal GetTotalPrice() // Method to get the total price
        {
            return _context.Books.Sum(b => b.CopyPrice); // Sum the prices of the copies
        }

        public decimal GetMaxPrice() // Method to get the max price of the copies
        {
            return _context.Books.Max(b => b.CopyPrice); // Return the max price
        }

        public int GetTotalBorrowedBooks() // Method to get the total number of the borrowed books by a customer
        {
            return _context.Books.Sum(b => b.BorrowedCopies); //  Sum the number of the borrowed copies
        }

        public int GetTotalBooksPerCategoryName(string categoryName)
        {
            return _context.Books
                           .Where(b => b.Category.CName == categoryName) // Check the category name
                           .Sum(b => b.TotalCopies); // Get the total copies of the category
        }
    }
}

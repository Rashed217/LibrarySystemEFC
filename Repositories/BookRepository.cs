using LibrarySystemEFC.Models;
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
            _context = context;
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book GetByName(string name)
        {
            return _context.Books.FirstOrDefault(b => b.BName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void Insert(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            UpdateCategoryBookCount(book.CID);
        }

        public void UpdateByName(string name, Book updatedBook)
        {
            var book = GetByName(name);
            if (book != null)
            {
                book.Author = updatedBook.Author;
                book.TotalCopies = updatedBook.TotalCopies;
                book.CopyPrice = updatedBook.CopyPrice;
                book.AllowedBorrowingPeriod = updatedBook.AllowedBorrowingPeriod;
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                UpdateCategoryBookCount(book.CID);
            }
        }

        public decimal GetTotalPrice()
        {
            return _context.Books.Sum(b => b.CopyPrice);
        }

        public decimal GetMaxPrice()
        {
            return _context.Books.Max(b => b.CopyPrice);
        }

        public int GetTotalBorrowedBooks()
        {
            return _context.Books.Sum(b => b.BorrowedCopies);
        }

        public int GetTotalBooksPerCategoryName(string name)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CName.Equals(name, StringComparison.OrdinalIgnoreCase));
            return category != null ? _context.Books.Count(b => b.BID == category.CID) : 0;
        }

        private void UpdateCategoryBookCount(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category != null)
            {
                category.NumberOfBooks = _context.Books.Count(b => b.CID == categoryId);
                _context.SaveChanges();
            }
        }
    }
}

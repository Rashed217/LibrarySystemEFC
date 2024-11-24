using LibrarySystemEFC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemEFC.Repositories
{
    public class BorrowingRepository
    {
        private readonly LibraryContext _context;

        public BorrowingRepository(LibraryContext context)
        {
            _context = context; // Parameter for LibraryContext
        }

        public IEnumerable<Borrowing> GetAll() // IEnumerable = Foreach loop
        {
            return _context.Borrowings.Include(b => b.User).Include(b => b.Book).ToList();
        }

        public Borrowing GetById(int id)
        {
            return _context.Borrowings.Include(b => b.User).Include(b => b.Book)
                                      .FirstOrDefault(b => b.BorID == id);
        }

        public void Insert(Borrowing borrowing) // adds to the borrowing collection
        {
            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();
        }

        public void UpdateById(int id, Borrowing updatedBorrowing)
        {
            var borrowing = GetById(id); // Retrieves borrowing by ID
            if (borrowing != null)
            {
                borrowing.BorrowingDate = updatedBorrowing.BorrowingDate;
                borrowing.PredictedReturnDate = updatedBorrowing.PredictedReturnDate;
                borrowing.ActualReturnDate = updatedBorrowing.ActualReturnDate;
                borrowing.Rating = updatedBorrowing.Rating;
                borrowing.IsReturned = updatedBorrowing.IsReturned;
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var borrowing = _context.Borrowings.Find(id); // Find book by ID
            if (borrowing != null)
            {
                _context.Borrowings.Remove(borrowing);
                _context.SaveChanges();
            }
        }

        public void ReturnBook(int borrowingId, DateTime actualReturnDate, int rating)
        {
            var borrowing = GetById(borrowingId); // Get book by ID
            if (borrowing != null && !borrowing.IsReturned)
            {
                borrowing.ActualReturnDate = actualReturnDate;
                borrowing.Rating = rating;
                borrowing.IsReturned = true;
                var book = borrowing.Book;
                book.BorrowedCopies--;

                _context.SaveChanges();
            }
        }
    }

}

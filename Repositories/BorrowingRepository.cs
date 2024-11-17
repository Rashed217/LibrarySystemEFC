using LibrarySystemEFC.Models;
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
            _context = context;
        }

        public List<Borrowing> GetAll()
        {
            return _context.Borrowings.ToList();
        }

        public void Insert(Borrowing borrowing)
        {
            _context.Borrowings.Add(borrowing);
            var book = _context.Books.Find(borrowing.BookId);
            if (book != null)
            {
                book.BorrowedCopies++;
                _context.SaveChanges();
            }
        }

        public void ReturnBook(int borrowingId)
        {
            var borrowing = _context.Borrowings.Find(borrowingId);
            if (borrowing != null && !borrowing.IsReturned)
            {
                borrowing.IsReturned = true;
                borrowing.ActualReturnDate = DateTime.Now;
                var book = _context.Books.Find(borrowing.BookId);
                if (book != null)
                {
                    book.BorrowedCopies--;
                    _context.SaveChanges();
                }
            }
        }
    }
}

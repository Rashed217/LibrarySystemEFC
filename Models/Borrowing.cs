using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemEFC.Models
{
    public class Borrowing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //  Generate Identity in the database
        public int BorID { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime PredictedReturnDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public int Rating { get; set; }
        public bool IsReturned { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}

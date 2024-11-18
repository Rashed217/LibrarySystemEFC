using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemEFC.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BID { get; set; }
        public string BName { get; set; }
        public string Author { get; set; }
        public int TotalCopies { get; set; }
        public int BorrowedCopies { get; set; }
        public decimal CopyPrice { get; set; }
        public int AllowedBorrowingPeriod { get; set; }

        [ForeignKey(nameof(Category))]
        public int CID { get; set; }
        public Category Category { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; }
    }
}

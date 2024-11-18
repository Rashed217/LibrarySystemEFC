using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemEFC.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CID { get; set; }
        public string CName { get; set; }
        public int NumberOfBooks { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}

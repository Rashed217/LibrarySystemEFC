using LibrarySystemEFC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemEFC.Repositories
{
    public class CategoryRepository
    {
        private readonly LibraryContext _context;

        public CategoryRepository(LibraryContext context)
        {
            _context = context; // Parameter for LibraryContext
        }

        public IEnumerable<Category> GetAll() //  IEnumerable = Foreach loop
        {
            return _context.Categories.Include(c => c.Books).ToList();
        }

        public Category GetByName(string name) // Get category by name
        {
            return _context.Categories.Include(c => c.Books)
                                      .FirstOrDefault(c => c.CName == name);
        }

        public void Insert(Category category) // Adds a new category
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateByName(string name, Category updatedCategory)
        {
            var category = GetByName(name); // Retrieves by name
            if (category != null)
            {
                category.CName = updatedCategory.CName;
                category.NumberOfBooks = updatedCategory.NumberOfBooks;
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var category = _context.Categories.Find(id); // Find category by ID
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}

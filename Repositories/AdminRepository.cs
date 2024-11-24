using LibrarySystemEFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemEFC.Repositories
{
    public class AdminRepository
    {
        private readonly LibraryContext _context; // Interacting with the database

        public AdminRepository(LibraryContext context)
        {
            _context = context; // Parameter for LibraryContext
        }

        public IEnumerable<Admin> GetAll() // IEnumerable = Foreach loop
        {
            return _context.Admins.ToList(); // Retrieve all admins data from database
        }

        public Admin GetByEmail(string email) // Retrieves only one admin based on the email
        {
            return _context.Admins.FirstOrDefault(a => a.Email == email); // FirstOrDefault = Retrieves the first one it finds
        }

        public void Insert(Admin admin) // Adds a new admin to the database
        {
            _context.Admins.Add(admin); // Adds admin to the collection
            _context.SaveChanges();
        }

        public void UpdateByEmail(string email, Admin updatedAdmin)
        {
            var admin = GetByEmail(email); // Retrieves only one admin based on the email
            if (admin != null) // Checks if admin is not null
            {
                admin.AName = updatedAdmin.AName; // Replace admin name with the new one
                admin.Password = updatedAdmin.Password; // Replace admin password with the new one
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var admin = _context.Admins.Find(id); // Retrieves only one admin based on the ID
            if (admin != null) // Checks if admin is not null
            {
                _context.Admins.Remove(admin); // Removes admin from the collection
                _context.SaveChanges();
            }
        }
    }
}

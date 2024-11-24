using LibrarySystemEFC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemEFC.Repositories
{
    public class UserRepository
    {
        private readonly LibraryContext _context;

        public UserRepository(LibraryContext context)
        {
            _context = context; // Parameter for LibraryContext
        }

        public IEnumerable<User> GetAll() // IEnumerable = Foreach loop
        {
            return _context.Users.ToList();
        }

        public User GetByName(string name) // Get user by name
        {
            return _context.Users.FirstOrDefault(u => u.UName == name);
        }

        public void Insert(User user) // Adds a new user
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateByName(string name, User updatedUser)
        {
            var user = GetByName(name); // Retrieves user by name
            if (user != null)
            {
                user.UName = updatedUser.UName;
                user.Gender = updatedUser.Gender;
                user.Passcode = updatedUser.Passcode;
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var user = _context.Users.Find(id); // Finds user by ID
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public int CountByGender(string gender) // Count the number of users by their gender
        {
            return _context.Users.Count(u => u.Gender == gender);
        }

        public bool Passcode(string passcode)
        {
            return _context.Users.Any(passc => passc.Passcode == passcode);
        }
        public string RegisterUser(User newUser) // Register a new user
        {

            if (_context.Users.Any(u => u.UName == newUser.UName))
            {
                return "Error: Username already exists. Please choose a different username.";
            }


            if (_context.Users.Any(u => u.Passcode == newUser.Passcode))
            {
                return "Error: Passcode already in use. Please choose a different passcode.";
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return "Registration successful!";
        }

        public User GetByPass(string passcode) // Get user by passcode
        {
            return _context.Users.FirstOrDefault(u => u.Passcode == passcode);
        }
    }
}

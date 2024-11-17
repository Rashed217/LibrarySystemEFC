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
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.UName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void Insert(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateByName(string name, User updatedUser)
        {
            var user = GetByName(name);
            if (user != null)
            {
                user.Gender = updatedUser.Gender;
                user.Passcode = updatedUser.Passcode;
                _context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public int CountByGender(string gender)
        {
            return _context.Users.Count(u => u.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase));
        }
    }
}

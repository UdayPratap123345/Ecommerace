using BookRental.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.DAL
{
   
   public class AuthenticationRepository : IAuthenticationRepository
        {
            private readonly BookRentalDbContext _dbContext;

            public AuthenticationRepository(BookRentalDbContext context)
            {
                _dbContext = context;
            }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public User? CheckCredentials(User user)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
                var userCredentials = _dbContext.Users?.SingleOrDefault(u => u.Email == user.Email && u.Password== user.Password);
                return userCredentials;
            }

            public string GetUserRole(int roleId)
            {
                return _dbContext.Roles?.SingleOrDefault(r => r.RoleId == roleId).RoleName;
            }

            public int RegisterUser(User user)
            {
                try
                {
                    _dbContext.Users.Add(user);
                    return _dbContext.SaveChanges();
                }
                catch (Exception error)
                {
                    Console.Error.WriteLine(error.Message);
                    return 0;
                }

            }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public async Task<User?> GetUserById(int userId)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<int> UpdateUser(User user)
        {
            try
            {
                _dbContext.Users.Update(user);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception error)
            {
                Console.Error.WriteLine(error.Message);
                return 0;
            }
        }
    }
    }


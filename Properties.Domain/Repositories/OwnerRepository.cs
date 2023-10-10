using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Properties.Domain.IRepositories;
using Properties.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly PropertiesDbContext _context;

        public OwnerRepository(PropertiesDbContext context)
        {
            _context=context;
        }

        public bool Create(Owner owner)
        {
            try
            {
                var nameParameter = new SqlParameter("@name", owner.Name);
                var addressParameter = new SqlParameter("@address", owner.Address);
                var birthdayParameter = new SqlParameter("@birthday", owner.Birthday);
                var sqlQuery = @"INSERT INTO OWNER (name, address, birthday) 
                                 VALUES (@name, @address, @birthday)";
                _context.Database.ExecuteSqlRaw(sqlQuery, nameParameter, addressParameter, birthdayParameter);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to create an owner went wront!"+ e.Message);
            }
        }

        public Owner Get(Guid ownerId)
        {
            try
            {
                var idOwnerParameter = new SqlParameter("@ownerId", ownerId);
                var sqlQuery = @"SELECT * FROM OWNER WHERE ownerId=@ownerId";
                var result = _context.Owners.FromSqlRaw(sqlQuery, idOwnerParameter).FirstOrDefault();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain an owner went wront!"+ e.Message);
            }
        }

        public List<Owner> GetAll()
        {
            try
            {
                return _context.Owners.Include(owner => owner.Properties)
                    .ThenInclude(property => property.PropertyImages)
                    .Include(owner => owner.Properties)
                    .ThenInclude(property => property.PropertyTraces)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Something in the request to obtain the owners went wront! "+ e.Message);
            }
        }
    }
}
